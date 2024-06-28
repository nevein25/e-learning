import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { ReviewInput } from '../_models/Review';
import { ReviewWithRates } from '../_models/reviewWithRates';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;


  addReviw(review: ReviewInput) {
    return this.http.post(`${this.baseUrl}reviews`, review);
  }

  getReviews(courseId: number)
  {
    return this.http.get<ReviewWithRates[]>(`${this.baseUrl}reviews/${courseId}`);
  }
}
