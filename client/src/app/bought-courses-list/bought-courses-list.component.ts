import { Component, OnInit, inject } from '@angular/core';
import { BoughtCourseService } from '../_services/bought-course.service';
import { CoursePurshased } from '../_models/coursesBought';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-bought-courses-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './bought-courses-list.component.html',
  styleUrl: './bought-courses-list.component.css'
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
