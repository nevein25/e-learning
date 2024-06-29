
import { Component, OnInit } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { Course } from '../_models/course';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';
import { Category } from '../_models/Category';
import { Router } from '@angular/router';
import { EnrollComponent } from "../payments/enroll/enroll.component";

@Component({
  selector: 'app-instructor-courses',
  standalone: true,
  imports: [FormsModule, NgFor, EnrollComponent],
  templateUrl: './instructor-courses.component.html',
  styleUrl: './instructor-courses.component.css'
})
export class InstructorCoursesComponent 
{
  courses: Course[] = [];
  categories: Category[] = [];  // Add an array to store categories
  searchName: string = '';
  searchCategoryName: string = '';

  searchMinPrice?: number;
  searchMaxPrice?: number;
  searchCategoryId?: number;
  pageNumber: number = 1;
  pageSize: number = 9;
  totalCourses: number = 0;

  constructor(private courseService: CourseService , private router: Router) { }
  ngOnInit(): void {
    this.searchCourses();
  }

  searchCourses(): void {
    this.courseService.getInstructorCourses(this.searchName, this.searchMinPrice, this.searchMaxPrice, this.searchCategoryId, this.pageNumber, this.pageSize)
      .subscribe(response => {
        this.courses = response.courses;
        this.totalCourses = response.totalCourses;
        console.log("COURSES",this.courses);
      });
  }
  onPageChange(page: number): void {
    this.pageNumber = page;
    this.searchCourses();
  }
  getTotalPages(): number {
    return Math.ceil(this.totalCourses / this.pageSize);
  }

  navigateToCourse(courseId: number): void {
    // Navigate to the course main page URL
    this.router.navigateByUrl(`/course-main-page/${courseId}`);
  }
}
