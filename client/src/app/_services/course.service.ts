import { Injectable , inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private http = inject(HttpClient); 
  baseUrl = environment.apiUrl;

  addCourse(courseObj : any){
    return this.http.post<any>(`${this.baseUrl}Course/create-new-course` , courseObj)
  }

  getCourses(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Course/GetAllCourses');
  }

  addModule(moduleObj : any){
    return this.http.post<any>(`${this.baseUrl}Course/create-new-module` , moduleObj)
  }

  getModules(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Course/GetAllModules');
  }

  addLesson(LessonObj : any){
    return this.http.post<any>(`${this.baseUrl}Course/create-new-Lesson` , LessonObj)
  }
}
