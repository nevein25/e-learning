import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { CoursePurshased } from '../_models/coursesBought';

@Injectable({
  providedIn: 'root'
})
export class BoughtCourseService {
 
  private http = inject(HttpClient); 
  baseUrl = environment.apiUrl;

  coursesBoughtList() {
    return this.http.get<CoursePurshased[]>(`${this.baseUrl}CoursesPurshases`);
  }

  isCourseBought(courseId: number){
    return this.http.get<any>(`${this.baseUrl}CoursesPurshases/${courseId}`);
  }
}
