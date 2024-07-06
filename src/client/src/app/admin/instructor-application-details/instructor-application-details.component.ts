import { Component, OnInit, inject } from '@angular/core';
import { InstructorApplication } from '../../_models/instructorApplication';
import { InstructorService } from '../../_services/instructor.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-instructor-application-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './instructor-application-details.component.html',
  styleUrl: './instructor-application-details.component.css'
})
export class InstructorApplicationDetailsComponent implements OnInit {

  instructorService = inject(InstructorService);
  activatedRoute = inject(ActivatedRoute);
  toastr = inject(ToastrService);
  instructorId: any;

  application: InstructorApplication = {
    username: '',
    paper: '',
    instructorId: 0
  };

  ngOnInit(): void {
    this.instructorId = this.activatedRoute.snapshot.paramMap.get("id");
    this.loadApplication();
  }

  loadApplication() {
    this.instructorService.getApplication(this.instructorId).subscribe({
      next: res => {

        this.application = res
      },
      error: error => console.log(error)

    });
  }

  verifyApplication() {
    this.instructorService.verifyApplication(this.instructorId).subscribe({
      next: res => {

        this.toastr.success("Application Verified Successfuly")
      },
      error: error => console.log(error)


    });
  }

}
