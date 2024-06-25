import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InstructorService } from '../_services/instructor.service';
import { CategoryService } from '../_services/category.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CourseService } from '../_services/course.service';
import { Router, RouterModule } from '@angular/router';
import { CourseDataService } from '../shared/course-data.service';


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
  selectedFile: File | null = null;
  constructor(
    private fb: FormBuilder,
    private instructorService: InstructorService,
    private categoryService: CategoryService,
    private courseService: CourseService,
    private route: Router,
    private courseDataService: CourseDataService
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

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }

  onSubmit(): void {
    if (this.courseForm.valid && this.selectedFile) {
      const formData = new FormData();
      formData.append('name', this.courseForm.get('cName')?.value);
      formData.append('duration', this.courseForm.get('cDuration')?.value);
      formData.append('description', this.courseForm.get('cDescription')?.value);
      formData.append('price', this.courseForm.get('cPrice')?.value);
      formData.append('language', this.courseForm.get('cLanguage')?.value);
      formData.append('instructorId', this.courseForm.get('cInstructor')?.value);
      formData.append('categoryId', this.courseForm.get('cCategory')?.value);
      formData.append('thumbnail', this.selectedFile);
  
      console.log('Form Data:');
      formData.forEach((value, key) => {
        console.log(`${key}: ${value}`);
      });
  
      this.courseService.addCourse(formData).subscribe({
        next: (response) => {
          console.log('Course added successfully');
          const courseId = response.id;
          console.log(courseId)
          this.courseDataService.setCourseId(courseId);
          this.route.navigate(['/module']);
        },
        error: (err) => {
          console.error('Error adding course:', err);
        }
      });
    }
  }

}
