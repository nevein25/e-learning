import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Course } from '../_models/course';
import { CourseInput } from '../_models/courseSearchInput';
import { Category } from '../_models/Category';
import { CourseWithInstructor } from '../_models/courseWithInstructor';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  addCourse(courseObj: FormData) {
    return this.http.post<any>(`${this.baseUrl}Course/create-new-course`, courseObj)
  }

  getCourses(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Course/GetAllCourses');
  }

  addModule(moduleObj: any) {
    return this.http.post<any>(`${this.baseUrl}Course/create-new-module`, moduleObj)
  }

  getModules(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Course/GetAllModules');
  }

  addLesson(LessonObj: FormData) {
    return this.http.post<any>(`${this.baseUrl}Course/create-new-Lesson`, LessonObj)
  }

  getCoursesForSearch(): Observable<Course[]> {
    return this.http.get<Course[]>(this.baseUrl + 'course/search');
  }

  searchCourses(name?: string, minPrice?: number, maxPrice?: number, categoryId?: number, pageNumber: number = 1, pageSize: number = 10): Observable<{ courses: Course[], totalCourses: number }> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (name) params = params.append('name', name);
    if (minPrice) params = params.append('minPrice', minPrice.toString());
    if (maxPrice) params = params.append('maxPrice', maxPrice.toString());
    if (categoryId) params = params.append('categoryId', categoryId.toString());

    return this.http.get<{ courses: Course[], totalCourses: number }>(this.baseUrl + 'course/search', { params });
  }

  getCourseById(id: number): Observable<Course> {
    return this.http.get<Course>(`${this.baseUrl}course/Course/${id}`);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'GetAllCategories');
  }

  getTopCourses(number: number)
  {
    return this.http.get<CourseWithInstructor[]>(`${this.baseUrl}course/top-courses/${number}`);
  }
}
