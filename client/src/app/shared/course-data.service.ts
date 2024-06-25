import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseDataService
{
  private courseIdSource = new BehaviorSubject<string | null>(null);
  currentCourseId = this.courseIdSource.asObservable();

  setCourseId(courseId: string): void {
    this.courseIdSource.next(courseId);
  }
}
