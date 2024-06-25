import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RateReviewService {

  private http = inject(HttpClient); 
  baseUrl = environment.apiUrl;

  rate(courseId: number, stars: number) {
    return this.http.post(`${this.baseUrl}rates`, {courseId, stars});
  }
}
