import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { AddReview, Review } from '../_models/Review';
 
@Injectable({
  providedIn: 'root'
})
export class RateReviewService {
 
  private http = inject(HttpClient); 
  baseUrl = environment.apiUrl;

  rate(courseId: number, stars: number) {
    return this.http.post(`${this.baseUrl}rates`, {courseId, stars});
  }
  GetAll(){
    return this.http.get<Review[]>(`${this.baseUrl}reviews`);
  }
  AddReview(review : AddReview){
    return this.http.post(`${this.baseUrl}reviews`, review);
  }
  UpdateReview(review:Review){
    return this.http.put(`${this.baseUrl}reviews`, review);
  }
  DeleteReview(review:Review){
    return this.http.delete(`${this.baseUrl}reviews/${review.id}`);
  }

}
 
