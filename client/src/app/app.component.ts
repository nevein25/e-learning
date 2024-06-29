import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { LoginComponent } from "./login/login.component";
import { CourseComponent } from './course/course.component';
import { ModuleComponent } from './module/module.component';
import { LessonComponent } from './lesson/lesson.component';
import { CourseListComponent } from "./course-list/course-list.component";
import { RouterLink } from '@angular/router';
import { RateandreviewComponent } from './rateandreview/rateandreview.component';
import { RegisterComponent } from "./register/register.component";
import { NavComponent } from "./nav/nav.component";
import { MembershipService } from './_services/membership.service';
import { StudentCourseComponent } from './student-course/student-course.component';
import { FooterComponent } from "./footer/footer.component";
import { LoaderComponent } from "./loader/loader.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [
        RouterOutlet,
        BsDropdownModule,
        LoginComponent,
        CourseComponent,
        ModuleComponent,
        LessonComponent,
        RouterModule,
        CourseListComponent,
        RouterLink,
        RateandreviewComponent,
        RegisterComponent,
        NavComponent,
        StudentCourseComponent,
        HttpClientModule,
        FooterComponent,
        LoaderComponent
    ]
})
export class AppComponent implements OnInit {

  title = 'client';
  // apiKey = "pk_test_51PV8p82MXG6xva1oS47Y7OFjxjWUIZXW1OHMUD8LyvhYGgY8p1QeXGXG4wR9aYdMVu3gju2Y8V3RGcshg82cPm0k00HmyI18Om"
  /* this is the new way of injecting
     http = inject(HttpClient);
    instead of
      constructor(private http: HttpClient) {}
  */
  http = inject(HttpClient);
  /// JUST FOR TESTING!!!!!
  // ngOnInit(): void {
  //   this.http.get("https://localhost:7154/WeatherForecast")
  //   .subscribe({
  //     next: response => console.log(response)

  //   });
  // }

  // constructor() { }
  // handler: any = null;
  // ngOnInit() {

  //   this.loadStripe();
  // }

  // pay(amount: any) {

  //   var handler = (<any>window).StripeCheckout.configure({
  //     key: this.apiKey,
  //     locale: 'auto',
  //     token: function (token: any) {
  //       // You can access the token ID with `token.id`.
  //       // Get the token ID to your server-side code for use.
  //       console.log(token)
  //       alert('Token Created!!');
  //     }
  //   });

  //   handler.open({
  //     name: 'Demo Site',
  //     description: '2 widgets',
  //     amount: amount * 100
  //   });

  // }

  // loadStripe() {

  //   if (!window.document.getElementById('stripe-script')) {
  //     var s = window.document.createElement("script");
  //     s.id = "stripe-script";
  //     s.type = "text/javascript";
  //     s.src = "https://checkout.stripe.com/checkout.js";
  //     s.onload = () => {
  //       this.handler = (<any>window).StripeCheckout.configure({
  //         key: this.apiKey,
  //         locale: 'auto',
  //         token: function (token: any) {
  //           // You can access the token ID with `token.id`.
  //           // Get the token ID to your server-side code for use.
  //           console.log(token)
  //           alert('Payment Success!!');
  //         }
  //       });
  //     }

  //     window.document.body.appendChild(s);
  //   }

  // }

  /// new
  constructor(
    private membershipService: MembershipService
  ) { }

  ngOnInit(): void { }



  goToBillingPortal() {
    this.membershipService.redirectToCustomerPortal();
  }

  // isSubscriber(): boolean {

  // }
}