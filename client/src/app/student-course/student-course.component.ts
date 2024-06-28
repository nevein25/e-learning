import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { StudentCourseService } from '../_services/student-course.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

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
  videoUrl: string | undefined;
  id: any;

  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;
  constructor(private studentCourseService: StudentCourseService ,  private activatedRoute: ActivatedRoute,) {}

  ngOnInit(): void {  

    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.getCourseContent(this.id);
  }

  getCourseContent(id: number): void {
    this.studentCourseService.GetModulesAndLessons(id).subscribe(
      (response) => {
        this.courseContent = response;
        if (this.courseContent.modules.length > 0 && this.courseContent.modules[0].lessons.length > 0) {
          this.setVideoUrl(this.courseContent.name, this.courseContent.modules[0].id, this.courseContent.modules[0].lessons[0].lessonNumber);
        }
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

  setVideoUrl(courseName: string, moduleId: number, lessonNumber: number): void {
    this.getPathLesson(courseName, moduleId, lessonNumber).subscribe(
      (response) => {
        this.videoUrl = response.link;
        if (this.videoPlayer) {
          this.videoPlayer.nativeElement.load(); 
        }
      },
      (error) => {
        console.error('Error fetching lesson link:', error);
      }
    );
  }

  onLinkClick(event: Event, courseName: string, moduleId: number, lessonNumber: number): void {
    event.preventDefault(); // Prevent the default link behavior
    this.setVideoUrl(courseName, moduleId, lessonNumber);
  }
}
