import { Component, OnInit, inject, input } from '@angular/core';
import { BuyCoursesService } from '../../_services/buy-courses.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-enroll',
  standalone: true,
  imports: [],
  templateUrl: './enroll.component.html',
  styleUrl: './enroll.component.css'
})
export class EnrollComponent implements OnInit {

  id = input.required<any>(); // p to c, the new way

  buyCoursesService = inject(BuyCoursesService);
  //route = inject(ActivatedRoute);

  ngOnInit(): void {
    // this.courseId = this.route.snapshot.paramMap.get("id");


  }
  buy(){
    this.buyCoursesService.requestCourseSession(this.id());
    
  }


}
