import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { ReviewInput, Review } from '../_models/Review';
import { Rate } from '../_models/rate';

@Injectable({
  providedIn: 'root'
})
export class RateReviewService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  rate(rate: Rate) {
    return this.http.post(`${this.baseUrl}rates`, rate);
  }
  GetAll() {
    return this.http.get<Review[]>(`${this.baseUrl}reviews`);
  }
  AddReview(review: ReviewInput) {
    return this.http.post(`${this.baseUrl}reviews`, review);
  }
  UpdateReview(review: Review) {
    return this.http.put(`${this.baseUrl}reviews`, review);
  }
  DeleteReview(review: Review) {
    return this.http.delete(`${this.baseUrl}reviews/${review.id}`);
  }

}

