import { Component, OnInit, inject } from '@angular/core';
import { InstructorService } from '../../_services/instructor.service';
import { InstructorApplication } from '../../_models/instructorApplication';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-instructors-applications',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './instructors-applications.component.html',
  styleUrl: './instructors-applications.component.css'
})

export class InstructorsApplicationsComponent implements OnInit{
  instructorService = inject(InstructorService);
  applications: InstructorApplication[] = [];
  
  ngOnInit(): void {
    this.getAllApplications();
  }

  getAllApplications() {
    this.instructorService.getApplications().subscribe({
      next: res => {
        this.applications = res
      },
      error: error => console.log(error)

    });
  }
}
