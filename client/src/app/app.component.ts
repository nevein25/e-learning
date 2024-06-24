import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { LoginComponent } from "./login/login.component";
import { CourseComponent } from './course/course.component';
import { ModuleComponent } from './module/module.component';
import { LessonComponent } from './lesson/lesson.component';
import { CourseListComponent } from "./course-list/course-list.component";
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [
        RouterOutlet,
        BsDropdownModule,
        LoginComponent,
        CourseComponent,
        ModuleComponent,
        LessonComponent,
        RouterModule,
        CourseListComponent,
        RouterLink,
    ]
})
export class AppComponent implements OnInit {
  
  title = 'client';

  /* this is the new way of injecting
     http = inject(HttpClient);
    instead of
      constructor(private http: HttpClient) {}
  */
  http = inject(HttpClient);
  /// JUST FOR TESTING!!!!!
  ngOnInit(): void {
    this.http.get("https://localhost:7154/WeatherForecast")
    .subscribe({
      next: response => console.log(response)
      
    });
  }

  
}
