import { Component, OnInit, computed, inject } from '@angular/core';
import { CourseService } from '../_services/course.service';
import { ActivatedRoute } from '@angular/router';
import { Course } from '../_models/course';
import { CommonModule } from '@angular/common';
import { EnrollComponent } from "../payments/enroll/enroll.component";
import { FormsModule } from '@angular/forms';
import { WishlistService } from '../_services/wishlist.service';
import { ToastrService } from 'ngx-toastr';
import { RateComponent } from "../rate/rate.component";
import { ReviewComponent } from "../review/review.component";
import { BoughtCourseService } from '../_services/bought-course.service';
import { RateService } from '../_services/rate.service';
import { RatingModule } from 'ngx-bootstrap/rating';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-course-main-page',
  standalone: true,
  templateUrl: './course-main-page.component.html',
  styleUrls: ['./course-main-page.component.css'],
  imports: [
    CommonModule,
    EnrollComponent, 
    FormsModule, 
    RateComponent, 
    ReviewComponent,
    RatingModule
  ]
})
export class CourseMainPageComponent implements OnInit {
  courseId: any;
  course: Course | undefined;
  isInWishlist = false;
  isCourseBought = false;
  avgRating = 0;
  isInstructor = computed(() => this.authService.role() === 'Instructor');

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
    private wishlistService: WishlistService,
    private boughtCourseService: BoughtCourseService,
    private rateService: RateService,
    private authService: AccountService

  ) { }

  ngOnInit(): void {
    this.courseId = this.activatedRoute.snapshot.paramMap.get('id');
    console.log("Is Instructor:", this.isInstructor());
    console.log("Role from authService:", this.authService.role());
    this.getCourseById();
    this.checkWishlist();
    this.checkIfCourseBought();
    this.getAvgCourseRate();
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

  checkIfCourseBought() {
    this.boughtCourseService.isCourseBought(this.courseId).subscribe({
      next: res => {
        this.isCourseBought = res.isBought;
        console.log(res.isBought);

      },
      error: error => console.log(error)


    });
  }

  getAvgCourseRate() {
    this.rateService.getAvgCourseRate(this.courseId).subscribe({
      next: res => {
        this.avgRating = res.avgRating;
        console.log(res);


      }
    })
  }
}
