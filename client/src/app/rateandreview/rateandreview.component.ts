import { CommonModule } from '@angular/common';
import { Component, NgModule, inject, input } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RateReviewService } from '../_services/rate-review.service';
import { ToastrService } from 'ngx-toastr';
import { RatingModule } from 'ngx-bootstrap/rating';

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
export class RateandreviewComponent {

  rateReviewService = inject(RateReviewService);
  tostr = inject(ToastrService);
  id = input.required<any>();

  max = 10;
  rate = 7;

  rateCourse() {
    this.rateReviewService.rate(this.rate, this.id()).subscribe({
      next: _ => this.tostr.success("Rate successfuly"),
      error: _ => this.tostr.success("Something went wrong")
    })
  }

}
