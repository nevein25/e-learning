import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { InstructorWithImg } from '../_models/Instructor';

@Injectable({
  providedIn: 'root'
})
export class InstructorService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInstructors(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + 'Course/GetAllInstructors');
  }

  getTopInstructors(number: number)
  {
    return this.http.get<InstructorWithImg[]>(`${this.baseUrl}Instructors/top-instructor/${number}`);
  }
}
