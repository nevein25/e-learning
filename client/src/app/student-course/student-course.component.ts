import { Component, ElementRef, OnInit, ViewChild,inject} from '@angular/core';
import { EnrollmentDto, StudentCourseService } from '../_services/student-course.service';
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
  LessonCount:number = 0
  lessonPercentage: number = 0;
  visitedLessons: number[] = [] 
  id: any;
  selectedLesson: any = null;
  loading: boolean = false;
  progress: number = 0;

  private toastr = inject(ToastrService);
  
  

  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;
  constructor(private studentCourseService: StudentCourseService ,  private activatedRoute: ActivatedRoute,) {}

  ngOnInit(): void {  

   this.id = this.activatedRoute.snapshot.paramMap.get('id');
   this.getCourseContent(this.id);
  this.getVisitedLessons();
  this.loadEnrollment();
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
             // this.toastr.success(`${this.courseContent.name} Course Content`);
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


  onLinkClick(event: Event, courseName: string, moduleNumber: number, lessonNumber: number,lesson:any,id:number): void {
    this.selectedLesson = lesson; 
    event.preventDefault();
    this.setVideoUrl(courseName, moduleNumber, lessonNumber);
    this.markLessonAsVisited(id);
  }


  //--------- Progress --------

  markLessonAsVisited(lessonId: number): void {
    if (!this.visitedLessons.includes(lessonId)) {
      this.visitedLessons.push(lessonId);
      this.updateEnrollmentProgress();
    }
  }


  updateEnrollmentProgress(): void {
    this.studentCourseService.getLessonCount(this.id).subscribe(
      (response) => {
        const totalLessons = response.data;
        const lessonsVisited = this.visitedLessons.length;
        let progress: number;

        if (lessonsVisited === 0) {
          progress = 0;
        } else if (lessonsVisited === totalLessons) {
          progress = 100;
        } else {       
          progress = (lessonsVisited / totalLessons) * 100;
        }

        this.progress = progress;
        
        const enrollmentDto: EnrollmentDto = {
          courseId: this.id,
          isFinished: lessonsVisited === totalLessons,
          progress: progress,
          visitedLessons: this.visitedLessons
        };
    
        this.studentCourseService.addOrUpdateEnrollment(enrollmentDto).subscribe(
          (response) => {
            if (response.isSuccess) {
              //this.toastr.success('Enrollment updated successfully.');
            } else {
             // this.toastr.error('Failed to update enrollment.');
            }
          },
          (error) => {
            console.error('Error updating enrollment:', error);
           // this.toastr.error('Failed to update enrollment.');
          }
        );
      },
      (error) => {
        console.error('Error fetching lesson count:', error);
        //this.toastr.error('Failed to fetch lesson count.');
      }
    );
  }

  getVisitedLessons(): void {

    this.studentCourseService.getVisitedLessons(this.id).subscribe(
      (response: number[]) => {
        this.visitedLessons = response || [];
      },
      (error) => {
        console.error('Error fetching visited lessons:', error);
        this.toastr.error('Failed to fetch visited lessons.');
      }
    );
  }
  
  loadEnrollment(): void {
    this.studentCourseService.getEnrollment(this.id).subscribe(
      (response) => {
        if (response.isSuccess && response.data) {
          const enrollment = response.data;
          this.progress = enrollment.progress != null ? enrollment.progress : 0;
        } else {
          console.error('Enrollment data is not available:', response.message);
          this.progress = 0;
        }
      },
      (error) => {
        console.error('Error fetching enrollment:', error);
      }
    );
  }
  

}
