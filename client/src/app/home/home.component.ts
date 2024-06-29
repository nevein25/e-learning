import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CourseService } from '../_services/course.service';
import { CourseWithInstructor } from '../_models/courseWithInstructor';
import { InstructorWithImg } from '../_models/Instructor';
import { InstructorService } from '../_services/instructor.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  numOfCoursesToGet = 3;
  courses: CourseWithInstructor[] = [];
  coursesService = inject(CourseService);
  numOfInstructorsToGet = 4;
  instructors: InstructorWithImg[] = [];
  instructorService = inject(InstructorService);

  ngOnInit(): void {
    this.getCourses();
    this.getInstructors();
  }

  getCourses() {
    this.coursesService.getTopCourses(this.numOfCoursesToGet).subscribe({
      next: res => {
        this.courses = res;
        console.log(res);
      
      },
      error: error => console.log(error)
    });
  }

  getInstructors() {
    this.instructorService.getTopInstructors(this.numOfInstructorsToGet).subscribe({
      next: res => {
        this.instructors = res;
        console.log(res);
    
      },
      error: error => console.log(error)
    });
  }
}
