import { Component, OnInit } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { Course } from '../_models/course';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [FormsModule, NgFor],
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
  courses: Course[] = [];
  searchName: string = '';
  searchMinPrice?: number;
  searchMaxPrice?: number;
  searchCategoryId?: number;
  pageNumber: number = 1;
  pageSize: number = 9;
  totalCourses: number = 0;

  constructor(private courseService: CourseService) { }

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
}
