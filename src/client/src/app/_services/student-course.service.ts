import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';


export interface EnrollmentDto {
  courseId: string;
  isFinished: boolean;
  progress: number;
  visitedLessons: number[];
}

@Injectable({
  providedIn: 'root'
})
export class StudentCourseService 
{
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  GetModulesAndLessons(courseid: number): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}Video/GetModulesAndLessonCourse`, courseid ,{ headers: { 'Content-Type': 'application/json' } });
  }

  GetPathLesson(publicId: string): Observable<any> {
    // Sending the publicId as a string with proper headers
    return this.http.post<any>(`${this.baseUrl}Video/GetLessonVideo`, JSON.stringify(publicId), { headers: { 'Content-Type': 'application/json' } });
  }

  
  //-----------Progress---------------

  getLessonCount(id: number)
  {
    return this.http.get<any>(`${this.baseUrl}Lesson/lesson-count/${id}`);
  }


  addOrUpdateEnrollment(enrollmentDto: EnrollmentDto): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}enrollment`, enrollmentDto, { headers: { 'Content-Type': 'application/json' }});
  }

  getVisitedLessons(courseId: number): Observable<number[]> {
    return this.http.get<number[]>(`${this.baseUrl}Enrollment/visited-lessons/${courseId}`);
  }

  getEnrollment(courseId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}Enrollment/enrollment/${courseId}`);
  }
}
