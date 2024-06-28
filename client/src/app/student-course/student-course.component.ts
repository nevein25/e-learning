import { Component, OnInit } from '@angular/core';
import { StudentCourseService } from '../_services/student-course.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-student-course',
  templateUrl: './student-course.component.html',
  styleUrls: ['./student-course.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class StudentCourseComponent implements OnInit {
  courseContent: any;
  lessonLink: string | undefined;
  

  constructor(private studentCourseService: StudentCourseService) {}

  ngOnInit(): void {
    this.getCourseContent(22);
  }

  getCourseContent(id: number): void {
    this.studentCourseService.GetModulesAndLessons(id).subscribe(
      (response) => {
       
        this.courseContent = response;
      },
      (error) => {
        console.error('Error fetching course content:', error);
      }
    );
  }

  getPathLesson(courseName: string, id: number, lessonNumber: number): Observable<any> {
    const path = `courses_videos/${courseName}/Chapter_${id}/Lesson_${lessonNumber}`;
    return this.studentCourseService.GetPathLesson(path);
  }

  onLinkClick(event: Event, courseName: string, moduleId: number, lessonNumber: number): void {
    event.preventDefault(); // Prevent the default link behavior

    this.getPathLesson(courseName, moduleId, lessonNumber).subscribe(
      (response) => {
        const lessonLink = response.link; // Assuming your response has a 'link' property
        window.open(lessonLink, '_blank'); // Open the link in a new tab
      },
      (error) => {
        console.error('Error fetching lesson link:', error);
      }
    );
  }
}
