import { Component, OnInit,inject } from '@angular/core';
import { FormBuilder, FormGroup,ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseService } from '../_services/course.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CourseDataService } from '../shared/course-data.service';
import { ToastrService } from 'ngx-toastr';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-module',
  standalone: true,
  imports: [ ReactiveFormsModule,
    HttpClientModule,
    CommonModule,RouterModule],
  templateUrl: './module.component.html',
  styleUrl: './module.component.css'
  
})
export class ModuleComponent implements OnInit {

  moduleForm: FormGroup;
  courseId: string | null = null;
  courses: any[] = [];
  private toastr = inject(ToastrService);

  constructor(
    private fb: FormBuilder,
    private courseService: CourseService,
    private route: Router,
    private courseDataService: CourseDataService
  ) {
    this.moduleForm = this.fb.group({
      mName: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.getCourses();
    this.courseDataService.currentCourseId.subscribe(courseId => {
      console.log('Received courseId:', courseId);
      this.courseId = courseId;
    });
  }

  getCourses(): void {
    this.courseService.getCourses().subscribe(data => {
      this.courses = data;
    });
  }

  onSubmit(): void {
    
    if (this.moduleForm.valid) 
    {
      const module = {
        name: this.moduleForm.value.mName,
        courseId: this.courseId,
      };
      this.courseService.addModule(module).subscribe({

        next: () => {
          this.moduleForm.reset();
          console.log('Module added successfully');
          this.toastr.success("Module added successfully");
        },
        error: (err) => {
          console.error('Error adding module:', err);
          this.toastr.error(err.error)
        }
      });
    }
  }
}
