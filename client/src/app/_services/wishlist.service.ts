// src/app/_services/wishlist.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Course } from '../_models/course';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
    baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getWishlist(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.baseUrl}wishlist`);
  }

  addToWishlist( courseId: number): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}wishlist/add`, { courseId });
  }

  removeFromWishlist(courseId: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}wishlist/${courseId}`);
  }
}
