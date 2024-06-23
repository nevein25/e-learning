import { Component, OnInit, inject } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-course-main-page',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './course-main-page.component.html',
  styleUrl: './course-main-page.component.css'
})
export class CourseMainPageComponent implements OnInit {
  courseId: any;

  ngOnInit(): void {
    this.courseId = this.activatedRoute.snapshot.paramMap.get('id')
    this.getCourseById()
  }
  courseService = inject(CourseService);
  activatedRoute = inject(ActivatedRoute);

  getCourseById() {
    this.courseService.getCourseById(this.courseId).subscribe({next:course=>console.log(course)})
  }

}
