import { Component, OnInit } from '@angular/core';
import { UploadedCoursesService } from '../_services/uploaded-course.service'; 
import { CourseUploaded } from '../_models/courseUploaded'; 
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-uploaded-courses-list',
  templateUrl: './uploaded-courses-list.component.html',
  styleUrls: ['./uploaded-courses-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class UploadedCoursesListComponent implements OnInit {

  courses: CourseUploaded[] = [];
  numOfStudents = 7;
  constructor(private uploadedCoursesService: UploadedCoursesService,private router: Router) { }

  ngOnInit(): void {
    this.getUploadedCourses();
  }

  getUploadedCourses(): void {
    this.uploadedCoursesService.getUploadedCourses().subscribe({
      next: (res: CourseUploaded[]) => {
        this.courses = res;
        console.log('Uploaded Courses:', this.courses);
      },
      error: (error: any) => {
        console.error('Error fetching uploaded courses:', error);
      }
    });
  }

  navigateToCourse(courseId: number): void {
    // Navigate to the course main page URL
    this.router.navigateByUrl(`/student-course/${courseId}`);
  }
}
