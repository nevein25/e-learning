import { Component, OnInit , inject } from '@angular/core';
import { LessonType } from '../_models/LessonType';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseService } from '../_services/course.service';
import { Router, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { CourseDataService } from '../_services/course-data.service';



@Component({
  selector: 'app-lesson',
  standalone: true,
  imports: [ 
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule
  ],
  templateUrl: './lesson.component.html',
  styleUrl: './lesson.component.css'
})
export class LessonComponent implements OnInit {
  lessonForm: FormGroup;
  Modules: any[] = [];
  selectedFile: File | null = null;
  courseId: string | null = null;
  private toastr = inject(ToastrService);

  constructor(
    private fb: FormBuilder, 
    private courseService: CourseService,
    private router: Router,
    private courseDataService: CourseDataService
  ) {
    this.lessonForm = this.fb.group({
      lName: ['',Validators.required],
      lContent: ['',Validators.required],
    
      cModules: ['', Validators.required],
      video: ['', Validators.required],

    });
  }

  ngOnInit(): void {
    this.getModules();
  }
 
  getCourseId()
  {
    this.courseDataService.currentCourseId.subscribe(courseId => {
      this.courseId = courseId;
    });
    
  }

  getModules(): void {
    this.getCourseId();
    this.courseService.getModules(this.courseId).subscribe({
        next: (response) => {
            if(!response.isSuccess)
            {
              this.toastr.error(response.message);
            }
            else 
            {
              this.Modules = response.data;
              this.toastr.success("Module added successfully");
            }
        },
        error: (err) => {
          this.toastr.error(err.error)
        }
      });
      console.log("Modles: ",this.Modules);
  }

  onFileChange(event: any): void {
    
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }
  
  onSubmit(): void {
    if (this.lessonForm.valid && this.selectedFile) {
      const formData = new FormData();
      formData.append('name', this.lessonForm.get('lName')?.value);
      formData.append('content', this.lessonForm.get('lContent')?.value);
      formData.append('moduleId', this.lessonForm.get('cModules')?.value);
      formData.append('videoContent', this.selectedFile);

      this.courseService.addLesson(formData).subscribe({
        next: (response) => {
          if(!response.isSuccess)
          {
            this.toastr.error(response.message);
          }
          else 
          {
            this.toastr.success("Lesson added successfully");
            this.lessonForm.reset();
            this.selectedFile = null;
          }
        },
        error: (err) => {
          this.toastr.error(err.error);
        }

      });
    }
  }
}
