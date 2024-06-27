import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentCourseService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  GetModulesAndLessons(courseid: number): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}Video/GetModulesAndLessonCourse`, courseid );
  }

  GetPathLesson(publicId: string): Observable<any> {
    // Sending the publicId as a string with proper headers
    return this.http.post<any>(`${this.baseUrl}Video/GetLessonVideo`, JSON.stringify(publicId), { headers: { 'Content-Type': 'application/json' } });
  }
}
