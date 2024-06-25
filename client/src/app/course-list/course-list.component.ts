import { Component, OnInit } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { Course } from '../_models/course';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';
import { Category } from '../_models/Category';
import { Router } from '@angular/router';


@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [FormsModule, NgFor],
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
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
    this.courseService.searchCourses(this.searchName, this.searchMinPrice, this.searchMaxPrice, this.searchCategoryId, this.pageNumber, this.pageSize)
      .subscribe(response => {
        this.courses = response.courses;
        this.totalCourses = response.totalCourses;
      });
  }

  onPageChange(page: number): void {
    this.pageNumber = page;
    this.searchCourses();
  }

  getTotalPages(): number {
    return Math.ceil(this.totalCourses / this.pageSize);
  }

  getCategories(): void {
    this.courseService.getCategories().subscribe(categories => {
      console.log(categories);  // Debugging line
      this.categories = categories;
    });
  }

  navigateToCourse(courseId: number): void {
    // Navigate to the course main page URL
    this.router.navigateByUrl(`/course-main-page/${courseId}`);
  }
}
