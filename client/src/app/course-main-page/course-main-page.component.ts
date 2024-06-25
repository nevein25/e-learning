import { Component, OnInit } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { ActivatedRoute } from '@angular/router';
import { Course } from '../_models/course';
import { CommonModule } from '@angular/common';
import { WishlistService } from '../_services/wishlist.service';
import { RateandreviewComponent } from '../rateandreview/rateandreview.component';

@Component({
  selector: 'app-course-main-page',
  standalone: true,
  imports: [CommonModule,RateandreviewComponent],
  templateUrl: './course-main-page.component.html',
  styleUrls: ['./course-main-page.component.css']
})
export class CourseMainPageComponent implements OnInit {
  courseId: any;
  course: Course | undefined;

  constructor(
    private courseService: CourseService,
    private activatedRoute: ActivatedRoute,
    private wishlistService: WishlistService

  ) {}

  ngOnInit(): void {
    this.courseId = this.activatedRoute.snapshot.paramMap.get('id');
    this.getCourseById();
  }

  getCourseById(): void {
    this.courseService.getCourseById(this.courseId)
      .subscribe(
        (course: Course) => {
          this.handleCourseSuccess(course);
        },
        (error) => {
          this.handleCourseError(error);
        }
      );
  }
  
  handleCourseSuccess(course: Course): void {
    this.course = course;
  }
  
  handleCourseError(error: any): void {
    console.error('Error fetching course:', error);
  }
  

  // Example method to get instructor name if needed
  getInstructorName(instructorId: number): string {
    // Implement logic to get instructor name based on instructorId
    return 'Instructor Name'; // Replace with actual logic
  }

  addToWishlist( courseId: number): void {
    this.wishlistService.addToWishlist(courseId)
      .subscribe(
        () => {
          // Handle success (e.g., show a success message)
          console.log('Course added to wishlist successfully');
        },
        (error) => {
          console.error('Error adding course to wishlist:', error);
          // Handle error (e.g., show an error message)
        }
      );
  }

  removeFromWishlist( courseId: number): void {
    this.wishlistService.removeFromWishlist(courseId)
      .subscribe(
        () => {
          // Handle success (e.g., show a success message)
          console.log('Course removed from wishlist successfully');
        },
        (error) => {
          console.error('Error removing course from wishlist:', error);
          // Handle error (e.g., show an error message)
        }
      );
  }
}
