import { Component, OnInit , inject } from '@angular/core';
import { LessonType } from '../_models/LessonType';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseService } from '../_services/course.service';
import { Router, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { CourseDataService } from '../shared/course-data.service';



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
  LessonType = LessonType;  // Reference the enum here
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
      // lLessonNumber: ['', Validators.required],
      lType: ['', Validators.required],
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
    this.courseService.getModules(this.courseId).subscribe(
      (data: any[]) => 
      {
        this.Modules = data;
      },
      (error) => 
      {
        console.error('Error fetching modules:', error);
      }
    );
    console.log("Modles: ",this.Modules);
  }

  /*getModules(): void {
    this.courseService.getModules("2").subscribe(data => {
      this.Modules = data;
      console.log("DATATTTTTTTTTT",data);
    });
  }*/

  onFileChange(event: any): void {
    
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }
  
  onSubmit(): void {
    if (this.lessonForm.valid && this.selectedFile) {
      const formData = new FormData();
      formData.append('name', this.lessonForm.get('lName')?.value);
      formData.append('type', this.lessonForm.get('lType')?.value);
      formData.append('content', this.lessonForm.get('lContent')?.value);
      // formData.append('lessonNumber', this.lessonForm.get('lLessonNumber')?.value);
      formData.append('moduleId', this.lessonForm.get('cModules')?.value);
      formData.append('videoContent', this.selectedFile);

      this.courseService.addLesson(formData).subscribe({
        next: () => {
           this.toastr.success("Lesson added successfully")
          console.log('Lesson added successfully');
          this.lessonForm.reset();
          this.selectedFile = null;
          
        },
        error: (err) => {
          this.toastr.error(err.error)
          console.error('Error adding Lesson:', err);
          console.error('Detailed error:', err.error);
      
        }

      });
    }
  }
}
