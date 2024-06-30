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
  private toastr = inject(ToastrService);
  loading: boolean = false;
  selectedModule:any=null;

  @ViewChild('videoPlayer') videoPlayer!: ElementRef<HTMLVideoElement>;
  constructor(private studentCourseService: StudentCourseService ,  private activatedRoute: ActivatedRoute,) {}

  ngOnInit(): void {  

    this.id = this.activatedRoute.snapshot.paramMap.get('id');
   // this.getCourseContent(90);
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
            //this.toastr.success("Success Show Content....");
            this.courseContent = response.data;
            if (this.courseContent.modules.length > 0 && this.courseContent.modules[0].lessons.length > 0) {
              this.selectedLesson = this.courseContent.modules[0].lessons[0];
              this.setVideoUrl(this.courseContent.name, this.courseContent.modules[0].moduleNumber, this.courseContent.modules[0].lessons[0].lessonNumber);
            }
          }
      },
      (error) => {
        //console.error('Error fetching course content:', error);
      }
    );
  }

  getPathLesson(courseName: string, moduleNumber: number, lessonNumber: number): Observable<any> {
    
    const path = `courses_videos/${courseName}/Chapter_${moduleNumber}/Lesson_${lessonNumber}`;
    return this.studentCourseService.GetPathLesson(path);
  }

  setVideoUrl(courseName: string, moduleNumber: number, lessonNumber: number): void {
    this.loading = true; // Set loading to true to show the loader

    setTimeout(() => {  // Simulate loading delay with setTimeout
      this.getPathLesson(courseName, moduleNumber, lessonNumber).subscribe(
        (response) => {
          this.loading = false; // Set loading to false after response
          if (!response.isSuccess) {
            //this.toastr.error(response.message);
          } else {
            this.videoUrl = response.data;
            if (this.videoPlayer) {
              this.videoPlayer.nativeElement.load(); 
            }
          }
        },
        (error) => {
          this.loading = false; // Set loading to false on error
          //console.error('Error fetching lesson link:', error);
        }
      );
    }, 1000);  // Adjust timeout duration as needed (in milliseconds)
  }


  onLinkClick(event: Event, courseName: string, moduleNumber: number, lessonNumber: number,lesson:any): void {
    this.selectedLesson = lesson; 
    this.selectedModule=moduleNumber;   
    console.log("Coursename",courseName);
    console.log("modulenumber",moduleNumber);
    console.log("lessonnumber",lessonNumber);
    event.preventDefault(); // Prevent the default link behavior
    this.setVideoUrl(courseName, moduleNumber, lessonNumber);
    
  }
}
