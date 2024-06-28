import { Component, OnInit, inject } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { ActivatedRoute } from '@angular/router';
import { Course } from '../_models/course';
import { CommonModule } from '@angular/common';
import { EnrollComponent } from "../payments/enroll/enroll.component";
import { FormsModule } from '@angular/forms';
import { WishlistService } from '../_services/wishlist.service';
import { RateandreviewComponent } from '../rateandreview/rateandreview.component';
import { ToastrService } from 'ngx-toastr';
import { RateComponent } from "../rate/rate.component";

@Component({
  selector: 'app-course-main-page',
  standalone: true,
  templateUrl: './course-main-page.component.html',
  styleUrls: ['./course-main-page.component.css'],
  imports: [CommonModule, RateandreviewComponent, CommonModule, EnrollComponent, FormsModule, RateandreviewComponent, RateComponent]
})
export class CourseMainPageComponent implements OnInit {
  courseId: any;
  course: Course | undefined;
  isInWishlist = false;

  private _toastr = inject(ToastrService);

  public get toastr() {
    return this._toastr;
  }
  public set toastr(value) {
    this._toastr = value;
  }

  constructor(
    private courseService: CourseService,
    private activatedRoute: ActivatedRoute,
    private wishlistService: WishlistService

  ) { }

  ngOnInit(): void {
    this.courseId = this.activatedRoute.snapshot.paramMap.get('id');
    this.getCourseById();
    this.checkWishlist();
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

  addToWishlist(courseId: number): void {
    this.wishlistService.addToWishlist(courseId)
      .subscribe(
        () => {
          // Handle success (e.g., show a success message)
          console.log('Course added to wishlist successfully');
          this.toastr.success("Course Added To WishList");
          this.isInWishlist = true;

        },
        (error) => {
          console.error('Error adding course to wishlist:', error);
          // Handle error (e.g., show an error message)
        }
      );
  }

  removeFromWishlist(courseId: number): void {
    this.wishlistService.removeFromWishlist(courseId)
      .subscribe(
        () => {
          // Handle success (e.g., show a success message)
          console.log('Course removed from wishlist successfully');
          this.toastr.success("Course removed from wishlist");

          this.isInWishlist = false;

        },
        (error) => {
          console.error('Error removing course from wishlist:', error);
          // Handle error (e.g., show an error message)
        }
      );
  }

  checkWishlist() {
    this.wishlistService.checkCourseExistenceInWishlist(this.courseId).subscribe({
      next: (exist: boolean) => {
        this.isInWishlist = exist;
      },
      error: _ => this.toastr.error("Something went wrong while checking the wishlist")
    });
  }
}
