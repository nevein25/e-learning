import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CourseUploaded } from '../_models/courseUploaded'; // Replace with your model
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UploadedCoursesService {

 private http = inject(HttpClient); 
  baseUrl = environment.apiUrl;

  constructor() { }

  getUploadedCourses(): Observable<CourseUploaded[]> {
    return this.http.get<CourseUploaded[]>(`${this.baseUrl}CoursesPurshases/uploaded`);
  }
}
