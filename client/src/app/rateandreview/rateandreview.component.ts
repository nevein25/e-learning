import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit, inject, input } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { RateReviewService } from '../_services/rate-review.service';
import { ToastrService } from 'ngx-toastr';
import { RatingModule } from 'ngx-bootstrap/rating';
import { Review } from '../_models/Review';

@Component({
  selector: 'app-rateandreview',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    RatingModule
  ],
  templateUrl: './rateandreview.component.html',
  styleUrl: './rateandreview.component.css'
})
export class RateandreviewComponent implements OnInit{
  AllReviews:Review[]=[];
  rateReviewService = inject(RateReviewService);
  tostr = inject(ToastrService);
  id = input.required<any>();
  text:any;
  newReview:any;
  
  ngOnInit(): void {
    this.rateReviewService.GetAll().subscribe({
      next: res => this.AllReviews=res,
      error: _ => this.tostr.success("Something went wrong")
    })
  }

  max = 5;
  rate = 5;
 
  rateCourse() {
    this.rateReviewService.rate(this.rate, this.id()).subscribe({
      next: _ => this.tostr.success("Rate successfuly"),
      error: _ => this.tostr.success("Something went wrong")
    })
  }
  AddReview(){
    this.rateReviewService.AddReview({
      text: this.text,courseId: this.id()}).subscribe({
        next: _ => this.tostr.success("Review successfuly"),
      error: _ => this.tostr.success("Something went wrong")
      })
  }
  UpdateReview(id:number){ const review: Review = {id:id,
    text: this.text,
    courseId: this.id()
  };

  this.rateReviewService.UpdateReview(review).subscribe({
    next: _ => this.tostr.success("Review updated successfully"),
    error: _ => this.tostr.error("Something went wrong")
  });}

  DeleteReview(id:number) {
    const review: Review = {
      id: id,
      text: this.text,
      courseId: this.id()
    };

    this.rateReviewService.DeleteReview(review).subscribe({
      next: _ => this.tostr.success("Review deleted successfully"),
      error: _ => this.tostr.error("Something went wrong")
    });
  }

}