import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProjectsComponent } from './projects/projects.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BarChartComponent } from './components/bar-chart/bar-chart.component';
import { LineChartComponent } from './components/line-chart/line-chart.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NotificationDropdownComponent } from './components/notification-dropdown/notification-dropdown.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { UserDropdownComponent } from './components/user-dropdown/user-dropdown.component';
import { ProjectsBoardComponent } from './projects-board/projects-board.component';

@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    DashboardComponent,
    BarChartComponent,
    LineChartComponent,
    NavbarComponent,
    NotificationDropdownComponent,
    SidebarComponent,
    UserDropdownComponent,
    ProjectsBoardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
