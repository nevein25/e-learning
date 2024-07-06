import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ICourse } from '../../_models/IMemberShipPlan';
import { BuyCoursesService } from '../../_services/buy-courses.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-course-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './course-checkout.component.html',
  styleUrl: './course-checkout.component.css'
})
export class CourseCheckoutComponent {
  course$: Observable<ICourse> | undefined;

  constructor(private courseService: BuyCoursesService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    //const courseId = this.route.snapshot.paramMap.get('id');
   // if (courseId) 
     // this.course$ = this.courseService.getCourse("7");
   this.purchaseCourse();
  }


  purchaseCourse(): void {
   
      this.courseService.requestCourseSession("7");
    
  }
}
