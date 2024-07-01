import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InstructorService } from '../_services/instructor.service';
import { CategoryService } from '../_services/category.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CourseService } from '../_services/course.service';
import { Router, RouterModule } from '@angular/router';
import { CourseDataService } from '../_services/course-data.service';
import { ToastrService } from 'ngx-toastr';


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
 
  categories: any[] = [];
  selectedFile: File | null = null;
  private toastr = inject(ToastrService);

  constructor(
    private fb: FormBuilder,
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
      cCategory: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  
    this.getCategories();
  }


  getCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (response) => 
      {
          if(!response.isSuccess)
          {
            this.toastr.error(response.message);
          }
          else 
          {
            this.categories = response.data;
          }
      },
      error: (err) => {}
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
      formData.append('categoryId', this.courseForm.get('cCategory')?.value);
      formData.append('thumbnail', this.selectedFile);
  
      console.log('Form Data:');
      formData.forEach((value, key) => {
        console.log(`${key}: ${value}`);
      });
  
      this.courseService.addCourse(formData).subscribe({
        next: (response) => 
        {
            if(!response.isSuccess)
            {
              this.toastr.error(response.message);
            }
            else 
            {
              this.toastr.success("Course added successfully");
              //console.log("CourseID", response.data.id);
              this.courseDataService.setCourseId(response.data.id);
              this.route.navigate(['/module']);
            }
        },
        error: (err) => 
        {
          //this.toastr.remove(err.error);
          //console.log('Error adding course:', err.error);
        }
      });
    }
  }

}
