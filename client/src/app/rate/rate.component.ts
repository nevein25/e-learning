import { Component, OnInit, inject, input } from '@angular/core';
import { RateService } from '../_services/rate.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { Rate } from '../_models/rate';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RatingModule } from 'ngx-bootstrap/rating';

@Component({
  selector: 'app-rate',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    RatingModule],
  templateUrl: './rate.component.html',
  styleUrl: './rate.component.css'
})

export class RateComponent implements OnInit {
  id = input.required<any>(); //courseId
  rateService = inject(RateService);
  toastr = inject(ToastrService);
  activatedRoute = inject(ActivatedRoute);

  max = 5;
  rate = 5;
  rateing: Rate = {
    stars: 0,
    courseId: 0
  }

  oldRate!: number;
  newRate!: number;

  ngOnInit(): void {
    this.getRate();
  }

  rateCourse() {
    this.newRate = this.rate;
    console.log(`new ${this.newRate}`);
    this.rateing.stars = this.rate;
    this.rateing.courseId = this.id();

    this.rateService.rate(this.rateing).subscribe({
      next: _ => {
        // if (this.newRate === 0)
        //   this.toastr.success("Rate removed successfully");

         if (this.newRate !== this.oldRate)
          this.toastr.success("Rated successfully");

        else {
          this.toastr.info("Rate removed successfuly");
          this.rate = 0;
        }

        this.oldRate = this.newRate; 
      },
      error: _ => this.toastr.error("Something went wrong")
    });
  }

  getRate() {
    this.rateService.getCourseRate(this.id()).subscribe({
      next: res => {
        this.rate = res.stars;
        this.oldRate = res.stars;
        console.log(`old ${this.oldRate}`);
      }
    });
  }
}
