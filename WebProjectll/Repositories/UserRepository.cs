using Microsoft.EntityFrameworkCore;
using WebProjectll.Models;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using ClosedXML.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;

namespace WebProjectll.Repositories
{
    public class UserRepository
    {
    
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(DatabaseContext  context, IConfiguration configuration){
            _context = context;
            _configuration = configuration;
        }

        public IEnumerable<User> filter(ResolveFieldContext<object> graphqlContext, HttpContext accesor){


            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }


            var results = from users in _context.Users select users;
            
            if(graphqlContext.HasArgument("id")){
                var id = graphqlContext.GetArgument<int>("id");
                results = results.Where(p => p.id == id);
            }
            return results;
        }
        public IEnumerable<Project> getProjects(long id){


            


            var results = _context.Project_user.Where(u => u.Usersid == id).ToList();
            List<Project> projectResults = new List<Project>();
            foreach (var projectuser in results)
            {
                var project = _context.Projects.Find(projectuser.Projectsid);
                projectResults.Add(project);
            }

            return projectResults;

        }
        
        public User deleteUser(long id, HttpContext accesor){


            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }
            
            var user = _context.Users.Find(id);
            if (user == null) {
                return null;
            }
            var relations = from projectUser in _context.Project_user select projectUser;
            foreach (var relation in relations)
            {
               if(relation.Usersid == id){
                   _context.Project_user.Remove(relation);
               } 
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

        
        public User Create(User user,HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public IEnumerable<TimeReport> getReports(long id){
            var results = from timeReports in _context.TimeReports select timeReports;
            return results.Where(t => t.ProyectId == id);
        }

        public User login(string name, string password){
            
            var user = _context.Users.Where(u => u.Name == name && u.Password == password).Single();

            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            { 
                Subject = claims,
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);


            user.token = tokenHandler.WriteToken(createdToken);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }
        public String CSV(ResolveFieldContext<object> graphqlContext, HttpContext accesor){


            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }


            var results = from timeReports in _context.TimeReports select timeReports;
            var id = graphqlContext.GetArgument<int>("id");
            results = results.Where((tr => tr.UserId == id));
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
        public string PDF(ResolveFieldContext<object> graphqlContext,HttpContext accesor){

            var validation = this.validateToken(accesor);
            if(!validation){
                return null;
            }

            
            var results = from timeReports in _context.TimeReports select timeReports;
            var id = graphqlContext.GetArgument<int>("id");
            results = results.Where((tr => tr.UserId == id));
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