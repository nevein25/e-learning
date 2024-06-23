import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../_models/course';
import { environment } from '../../environments/environment';
import { CourseInput } from '../_models/courseSearchInput';


@Injectable({
  providedIn: 'root'
})
export class CourseService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + 'course/search');
  }

  searchCourses(name?: string, minPrice?: number, maxPrice?: number, categoryId?: number): Observable<Course[]> {
    let params = new HttpParams();
    if (name) params = params.append('name', name);
    if (minPrice) params = params.append('minPrice', minPrice.toString());
    if (maxPrice) params = params.append('maxPrice', maxPrice.toString());
    if (categoryId) params = params.append('categoryId', categoryId.toString());

    return this.http.get<Course[]>(this.baseUrl + 'course/search', { params });
  }

  getCourseById(id: number) {
    return this.http.get<CourseInput>(this.baseUrl + 'course/' + id)
  }
}
