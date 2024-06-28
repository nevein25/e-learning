import { Component, OnInit, inject, input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ReviewService } from '../_services/review.service';
import { ReviewInput } from '../_models/Review';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReviewWithRates } from '../_models/reviewWithRates';
import { BoughtCourseService } from '../_services/bought-course.service';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent implements OnInit{

  id = input.required<any>(); //courseId
  reviewService = inject(ReviewService);
  toastr = inject(ToastrService);
  boughtCourseService = inject(BoughtCourseService);

  allReviews: ReviewWithRates[] = [];
  isCourseBought = false;

  review: ReviewInput = {
    courseId: 0,
    text: ''
  };


  ngOnInit(): void {
    this.getAllReviews();
  }

  getAllReviews() {
    this.reviewService.getReviews(this.id()).subscribe({
      next: res => {
        this.allReviews = res
        console.log(res);
      },
      error: error => console.error(error)     
    });
  }

  addReview() {
    this.review.courseId = this.id();
    this.reviewService.addReviw(this.review).subscribe({
      next: _ => {
        this.toastr.success('Review added successfully');
        this.getAllReviews();
      },
      error: error => console.error(error)

    });
  }


  checkIfCourseBought() {
    this.boughtCourseService.isCourseBought(this.id()).subscribe({
      next: res => {
        this.isCourseBought = res.isBought;
        console.log(res.isBought);

      },
      error: error => console.log(error)


    });
  }
}
