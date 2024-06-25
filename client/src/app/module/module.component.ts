import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseService } from '../_services/course.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-module',
  standalone: true,
  imports: [ ReactiveFormsModule,
    HttpClientModule,
    CommonModule],
  templateUrl: './module.component.html',
  styleUrl: './module.component.css'
})
export class ModuleComponent implements OnInit {

  moduleForm: FormGroup;
 
  courses: any[] = [];

  constructor(
    private fb: FormBuilder,
    private courseService: CourseService,
    private route: Router
  ) {
    this.moduleForm = this.fb.group({
      mName: ['', Validators.required],
      mNumber: ['', Validators.required],
      mCourse: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.getCourses();
  }

  getCourses(): void {
    this.courseService.getCourses().subscribe(data => {
      this.courses = data;
    });
  }

  onSubmit(): void {
    if (this.moduleForm.valid) {
      const module = {
        name: this.moduleForm.value.mName,
        moduleNumber: this.moduleForm.value.mNumber,
      
        courseId: this.moduleForm.value.mCourse,
      };

      this.courseService.addModule(module).subscribe({
        next: () => {
          // Navigate to another route if needed, e.g., course list
           this.route.navigate(['/lesson']);
          console.log('Module added successfully');
        },
        error: (err) => {
          console.error('Error adding module:', err);
        }
      });
    }
  }
}
