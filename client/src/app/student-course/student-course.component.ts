import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { StudentCourseService } from '../_services/student-course.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DownloadCertificateComponent } from "../download-certificate/download-certificate.component";
import { CertificateService } from '../_services/certificate.service';

@Component({
  selector: 'app-student-course',
  templateUrl: './student-course.component.html',
  styleUrls: ['./student-course.component.css'],
  standalone: true,
  imports: [CommonModule, DownloadCertificateComponent]
})
export class StudentCourseComponent implements OnInit {
  courseContent: any;
  lessonLink: string | undefined;
  videoUrl: string | undefined;
  id: any;
  isStudentFinished = false;
  private toastr = inject(ToastrService);
  private certificateService = inject(CertificateService);


  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;
  constructor(private studentCourseService: StudentCourseService, private activatedRoute: ActivatedRoute,) { }

  ngOnInit(): void {

    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    // this.getCourseContent(90);
    this.getCourseContent(this.id);
    this.isStudentFinishedCourse();
  }

  getCourseContent(id: number): void {
    this.studentCourseService.GetModulesAndLessons(id).subscribe(
      (response) => {
        if (!response.isSuccess) {
          this.toastr.error(response.message);
        }
        else {
          //this.toastr.success("Success Show Content....");
          this.courseContent = response.data;
          if (this.courseContent.modules.length > 0 && this.courseContent.modules[0].lessons.length > 0) {
            this.setVideoUrl(this.courseContent.name, this.courseContent.modules[0].moduleNumber, this.courseContent.modules[0].lessons[0].lessonNumber);
          }
        }

      },
      (error) => {
        console.error('Error fetching course content:', error);
      }
    );
  }

  getPathLesson(courseName: string, moduleNumber: number, lessonNumber: number): Observable<any> {
    const path = `courses_videos/${courseName}/Chapter_${moduleNumber}/Lesson_${lessonNumber}`;
    return this.studentCourseService.GetPathLesson(path);
  }

  setVideoUrl(courseName: string, moduleNumber: number, lessonNumber: number): void {
    this.getPathLesson(courseName, moduleNumber, lessonNumber).subscribe(
      (response) => {

        if (!response.isSuccess) {
          this.toastr.error(response.message);
        }
        else {
          this.videoUrl = response.data;
          if (this.videoPlayer) {
            this.videoPlayer.nativeElement.load();
          }
        }
      },
      (error) => {
        console.error('Error fetching lesson link:', error);
      }
    );
  }

  onLinkClick(event: Event, courseName: string, moduleNumber: number, lessonNumber: number): void {
    event.preventDefault(); // Prevent the default link behavior
    this.setVideoUrl(courseName, moduleNumber, lessonNumber);
    this.isStudentFinishedCourse();
  }

  isStudentFinishedCourse() {
    this.certificateService.isStudentFinishedCourse(this.id).subscribe({
        next: res => {
          console.log(res.isFinished);
          this.isStudentFinished = res.isFinished;
        }
    });
  }
}
