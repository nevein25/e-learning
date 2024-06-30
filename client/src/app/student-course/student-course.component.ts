import { Component, ElementRef, OnInit, ViewChild,inject} from '@angular/core';
import { StudentCourseService } from '../_services/student-course.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

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
  selectedLesson: any = null;
  loading: boolean = false;
  private toastr = inject(ToastrService);
  
  

  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;
  constructor(private studentCourseService: StudentCourseService ,  private activatedRoute: ActivatedRoute,) {}

  ngOnInit(): void {  

   this.id = this.activatedRoute.snapshot.paramMap.get('id');
   this.getCourseContent(this.id);
  }

  getCourseContent(id: number): void {
    this.studentCourseService.GetModulesAndLessons(id).subscribe(
      (response) => {
          if(!response.isSuccess)
          {
            //this.toastr.error(response.message);
          }
          else
          {
            this.courseContent = response.data;
            if (this.courseContent.modules.length > 0 && this.courseContent.modules[0].lessons.length > 0) {
              this.toastr.success(`${this.courseContent.name} Course Content`);
              this.selectedLesson = this.courseContent.modules[0].lessons[0];
              this.setVideoUrl(this.courseContent.name, this.courseContent.modules[0].moduleNumber, this.courseContent.modules[0].lessons[0].lessonNumber);
            }
            else this.toastr.error(`Course Content Not Exist`);
          }
      },
      (error) => {}
    );
  }

  getPathLesson(courseName: string, moduleNumber: number, lessonNumber: number): Observable<any> {
    const path = `courses_videos/${courseName}/Chapter_${moduleNumber}/Lesson_${lessonNumber}`;
    return this.studentCourseService.GetPathLesson(path);
  }

  setVideoUrl(courseName: string, moduleNumber: number, lessonNumber: number): void {
    this.loading = true; 
    setTimeout(() => { 
      this.getPathLesson(courseName, moduleNumber, lessonNumber).subscribe(
        (response) => {
          this.loading = false;
          if (!response.isSuccess) 
          {
            //this.toastr.error(response.message);
          } 
          else 
          {
            this.videoUrl = response.data;
            if (this.videoPlayer) {
              this.videoPlayer.nativeElement.load(); 
            }
          }
        },
        (error) => {
          this.loading = false; 
        }
      );
    }, 1000);
  }


  onLinkClick(event: Event, courseName: string, moduleNumber: number, lessonNumber: number,lesson:any): void {
    this.selectedLesson = lesson; 
    event.preventDefault();
    this.setVideoUrl(courseName, moduleNumber, lessonNumber);
  }
}
