using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Microsoft.AspNetCore.Razor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using ClosedXML.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;

namespace WebProjectll.Repositories
{
    public class ProjectRepository: ControllerBase
    {
        private readonly DatabaseContext _context;
        public ProjectRepository(DatabaseContext context){
            _context = context;
        }

        public IEnumerable<Project> filter(ResolveFieldContext<object> graphqlContext, HttpContext accesor){


            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }

            var results = from projects in _context.Projects select projects;
            if(graphqlContext.HasArgument("id")){
                var id = graphqlContext.GetArgument<int>("id");
                results = results.Where(p => p.id == id);   
            }
            if(graphqlContext.HasArgument("name")){
                var name = graphqlContext.GetArgument<string>("name");
                results = results.Where(p => p.Name.Contains(name));
            }
            if(graphqlContext.HasArgument("description")){
                var name = graphqlContext.GetArgument<string>("description");
                results = results.Where(p => p.Description.Contains(name));
            }
            return results;
        }
        
        public IEnumerable<User> getUsers(long id){

            


            var results = _context.Project_user.Where(u => u.Projectsid == id).ToList();
            List<User> userResults = new List<User>();
            foreach (var projectuser in results)
            {
                var user = _context.Users.Find(projectuser.Usersid);
                userResults.Add(user);
            }

            return userResults;

        }
        public Project Create(Project project, HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }


            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }
        public Project addUser(ProjectUser info, HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }

            var project = _context.Projects.Find(info.Projectsid);
            var user = _context.Users.Find(info.Usersid);
            project.Users = new List<User>();
            project.Users.Add(user);
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return project;
            
        }
        public Project deleteProject(long id, HttpContext accesor){
            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }
            var project = _context.Projects.Find(id);
            var relations = from projectUser in _context.Project_user select projectUser;
            foreach (var relation in relations)
            {
               if(relation.Projectsid == id){
                   _context.Project_user.Remove(relation);
               } 
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return project;

        }
        public Project deleteUser(ProjectUser info,HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }


            var project = _context.Projects.Find(info.Projectsid);
            var user = _context.Users.Find(info.Usersid);
            project.Users = getUsers(project.id).ToList();
            project.Users.Remove(user);
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return project;
        }
        public Project update(long id, Project project, HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }

            if(id != project.id){
                return null;
            }
            _context.Entry(project).State = EntityState.Modified;
            _context.SaveChanges();
            return project;
        }
        public IEnumerable<TimeReport> getReports(long id){
            
            var results = from timeReports in _context.TimeReports select timeReports;
            return results.Where(t => t.ProyectId == id);
        }

        public String CSV(ResolveFieldContext<object> graphqlContext, HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }


            var results = from timeReports in _context.TimeReports select timeReports;
            var id = graphqlContext.GetArgument<int>("id");
            results = results.Where((tr => tr.ProyectId == id));
            if(graphqlContext.HasArgument("inicialDate") && graphqlContext.HasArgument("endDate")){
                var initialDateString = graphqlContext.GetArgument<string>("inicialDate");
                var initialDate = DateTime.Parse(initialDateString);
                var endDateString = graphqlContext.GetArgument<string>("endDate");
                var endDate = DateTime.Parse(endDateString);
                results = results.Where(tr => tr.date > initialDate && tr.date < endDate);
                
            }else{
                if(graphqlContext.HasArgument("inicialDate")){
                    var initialDateString = graphqlContext.GetArgument<string>("inicialDate");
                    var initialDate = DateTime.Parse(initialDateString);
                    results = results.Where(tr => tr.date > initialDate);
                }
                if(graphqlContext.HasArgument("endDate")){
                    var endDateString = graphqlContext.GetArgument<string>("endDate");
                    var endDate = DateTime.Parse(endDateString);
                    results = results.Where(tr => tr.date < endDate);
                }
            }
            
            using (var workbook = new XLWorkbook()){
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "hours";
                worksheet.Cell(currentRow, 2).Value = "date";
                foreach (var report in results)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = report.Hours;
                    worksheet.Cell(currentRow, 2).Value = report.date;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    
                    var content = stream.ToArray();
                    var base64 = Convert.ToBase64String(content);

                    return base64;
                }
                
            }

        }
        public string PDF(ResolveFieldContext<object> graphqlContext, HttpContext accesor){

           
            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }
            var results = from timeReports in _context.TimeReports select timeReports;
            

            var id = graphqlContext.GetArgument<int>("id");
            results = results.Where((tr => tr.ProyectId == id));
            if(graphqlContext.HasArgument("inicialDate") && graphqlContext.HasArgument("endDate")){
                var initialDateString = graphqlContext.GetArgument<string>("inicialDate");
                var initialDate = DateTime.Parse(initialDateString);
                var endDateString = graphqlContext.GetArgument<string>("endDate");
                var endDate = DateTime.Parse(endDateString);
                results = results.Where(tr => tr.date > initialDate && tr.date < endDate);
                
            }else{
                if(graphqlContext.HasArgument("inicialDate")){
                    var initialDateString = graphqlContext.GetArgument<string>("inicialDate");
                    var initialDate = DateTime.Parse(initialDateString);
                    results = results.Where(tr => tr.date > initialDate);
                }
                if(graphqlContext.HasArgument("endDate")){
                    var endDateString = graphqlContext.GetArgument<string>("endDate");
                    var endDate = DateTime.Parse(endDateString);
                    results = results.Where(tr => tr.date < endDate);
                }
            }

            Document doc = new Document(PageSize.Letter);
            PdfWriter writer = PdfWriter.GetInstance(doc,
                            new MemoryStream());

            doc.AddTitle("Reporte");
            doc.Open();
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 8, iTextSharp.text.Font.NORMAL, BaseColor.Black);
            doc.Add(new Paragraph("Reporte de tiempos"));
            doc.Add(Chunk.Newline);
            PdfPTable tblPrueba = new PdfPTable(2);
            tblPrueba.WidthPercentage = 100;

            PdfPCell clHour = new PdfPCell(new Phrase("Hour", _standardFont));
            clHour.BorderWidth = 0;
            clHour.BorderWidthBottom = 0.75f;
            
            PdfPCell clDate = new PdfPCell(new Phrase("Date", _standardFont));
            clDate.BorderWidth = 0;
            clDate.BorderWidthBottom = 0.75f;
            
            tblPrueba.AddCell(clHour);
            tblPrueba.AddCell(clDate);

            foreach (var item in results)
            {
                PdfPCell hour = new PdfPCell(new Phrase(item.Hours.ToString(), _standardFont));
                clHour.BorderWidth = 0;
                clHour.BorderWidthBottom = 0.75f;
                
                PdfPCell date = new PdfPCell(new Phrase(item.date.ToString(), _standardFont));
                clDate.BorderWidth = 0;
                clDate.BorderWidthBottom = 0.75f;

                tblPrueba.AddCell(hour);
                tblPrueba.AddCell(date);
            }
            doc.Add(tblPrueba);
 
            doc.Close();
            writer.CreateXmpMetadata();
            var x = Convert.ToBase64String(writer.XmpMetadata);
            writer.Close();
            
            return x;
        }

        private bool validateToken(HttpContext accesor){

            var headers = accesor.Request.Headers.ToList();
            var accesToken = headers.Where(h => h.Key == "Authorization").Single();
            var users = _context.Users.ToList();
            User user = users.Find(u => u.token == accesToken.Value.ToString());
            if(user == null){
                return false;
            }else{
                return true;
            }
        }
        
    }
}