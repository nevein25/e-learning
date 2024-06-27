import { Component, OnInit, inject } from '@angular/core';
import { BoughtCourseService } from '../_services/bought-course.service';
import { CoursePurshased } from '../_models/coursesBought';
import { CommonModule } from '@angular/common';
import { EnrollComponent } from "../payments/enroll/enroll.component";

@Component({
    selector: 'app-bought-courses-list',
    standalone: true,
    templateUrl: './bought-courses-list.component.html',
    styleUrl: './bought-courses-list.component.css',
    imports: [CommonModule, EnrollComponent]
})
export class BoughtCoursesListComponent implements OnInit {

  courses: CoursePurshased[] = [];
  boughtCourses = inject(BoughtCourseService);


  ngOnInit(): void {
    this.getCourses();

  }

  getCourses() {
    this.boughtCourses.coursesBoughtList().subscribe({
      next: res => {
        this.courses = res;
        console.log(this.courses);

      },
      error: error => console.error(error)

    });
  }


}
