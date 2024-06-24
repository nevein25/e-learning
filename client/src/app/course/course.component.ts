import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InstructorService } from '../_services/instructor.service';
import { CategoryService } from '../_services/category.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CourseService } from '../_services/course.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-course',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule
  ],
  templateUrl: './course.component.html',
  styleUrl: './course.component.css'
})
export class CourseComponent implements OnInit {

  courseForm: FormGroup;
  instructors: any[] = [];
  categories: any[] = [];

  constructor(
    private fb: FormBuilder,
    private instructorService: InstructorService,
    private categoryService: CategoryService,
    private courseService: CourseService,
    private route: Router
  ) {
    this.courseForm = this.fb.group({
      cName: ['', Validators.required],
      cDuration: ['', Validators.required],
      cDescription: ['', Validators.required],
      cPrice: ['', Validators.required],
      cLanguage: ['', Validators.required],
      cThumbnail: ['', Validators.required],
      cInstructor: ['', Validators.required],
      cCategory: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.getInstructors();
    this.getCategories();
  }

  getInstructors(): void {
    this.instructorService.getInstructors().subscribe(data => {
      this.instructors = data;
    });
  }

  getCategories(): void {
    this.categoryService.getCategories().subscribe(data => {
      this.categories = data;
    });
  }

  onSubmit(): void {
    if (this.courseForm.valid) {
      const course = {
        name: this.courseForm.value.cName,
        duration: this.courseForm.value.cDuration,
        description: this.courseForm.value.cDescription,
        price: this.courseForm.value.cPrice,
        language: this.courseForm.value.cLanguage,
        thumbnail: this.courseForm.value.cThumbnail,
        instructorId: this.courseForm.value.cInstructor,
        categoryId: this.courseForm.value.cCategory
      };

      this.courseService.addCourse(course).subscribe({
        next: () => {
          // Navigate to another route if needed, e.g., course list
           this.route.navigate(['/module']);
          console.log('Course added successfully');
        },
        error: (err) => {
          console.error('Error adding course:', err);
        }
      });
    }
  }
}
