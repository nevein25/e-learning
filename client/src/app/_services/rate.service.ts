import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { Rate } from '../_models/rate';
import { RateByUser } from '../_models/rateByUser';

@Injectable({
  providedIn: 'root'
})
export class RateService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;


  rate(rate: Rate) {
    return this.http.post(`${this.baseUrl}rates`, rate);
  }

  getCourseRate(courseId: number) {
    return this.http.get<RateByUser>(`${this.baseUrl}rates/${courseId}`);
  }

}
