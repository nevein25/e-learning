import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { InstructorWithImg } from '../_models/Instructor';
import { InstructorApplication } from '../_models/instructorApplication';

@Injectable({
  providedIn: 'root'
})
export class InstructorService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInstructors(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Course/GetAllInstructors');
  }

  getTopInstructors(number: number) {
    return this.http.get<InstructorWithImg[]>(`${this.baseUrl}Instructors/top-instructor/${number}`);
  }

  uploadPaper(file: any, username: any) {
    const formData = new FormData();
    formData.append("file", file, file.name);
    return this.http.post(this.baseUrl + 'Instructors/add-photo/' + username, formData)
  }


  getApplications() {
    return this.http.get<InstructorApplication[]>(`${this.baseUrl}Instructors/applications`);
  }

  getApplication(instructorId: number) {
    return this.http.get<InstructorApplication>(`${this.baseUrl}Instructors/application/${instructorId}`);
  }

  verifyApplication(instructorId: number) {
    return this.http.post(`${this.baseUrl}Instructors/verify/${instructorId}`,{});
  }
}
