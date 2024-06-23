import { Component, OnInit } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { Course } from '../_models/course';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [FormsModule,NgFor],
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: Course[] = [];
  searchName: string = '';
  searchMinPrice?: number;
  searchMaxPrice?: number;
  searchCategoryId?: number;

  constructor(private courseService: CourseService) { }

  ngOnInit(): void {
    this.getCourses();
  }

  getCourses(): void {
    this.courseService.searchCourses().subscribe(courses => this.courses = courses);
  }

  searchCourses(): void {
    this.courseService.searchCourses(this.searchName, this.searchMinPrice, this.searchMaxPrice, this.searchCategoryId)
      .subscribe(courses => this.courses = courses);
  }
}
