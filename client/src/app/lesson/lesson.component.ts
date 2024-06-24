import { Component, OnInit } from '@angular/core';
import { LessonType } from '../_models/LessonType';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseService } from '../_services/course.service';
import { Router, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

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
  constructor(
    private fb: FormBuilder, 
    private courseService: CourseService,
    private router: Router
  ) {
    this.lessonForm = this.fb.group({
      lName: [''],
      lContent: [''],
      lLessonNumber: ['', Validators.required],
      lType: ['', Validators.required],
      cModules: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getModules();
  }

  getModules(): void {
    this.courseService.getModules().subscribe(data => {
      this.Modules = data;
    });
  }

  onFileChange(event: any): void {
    
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
    setTimeout(() =>
      {
      
    }, 7000); // Simulate 2 seconds delay for each upload process
  }
  //this.course.chapters[chapterIndex].ifDone=true;

  



  onSubmit(): void {
    if (this.lessonForm.valid) {
      const lesson = {
        name: this.lessonForm.value.lName,
        type: +this.lessonForm.value.lType,
        content: this.lessonForm.value.lContent,      
        lessonNumber: this.lessonForm.value.lLessonNumber,
        moduleId: this.lessonForm.value.cModules,
        videoContent: this.selectedFile
      };
  
      console.log('Lesson data to be submitted:', lesson);

      this.courseService.addLesson(lesson).subscribe({
        next: () => {
          console.log('Lesson added successfully');
        },
        error: (err) => {
          console.error('Error adding Lesson:', err);
          console.error('Detailed error:', err.error);
        }
      });
    }
  }
  
}
