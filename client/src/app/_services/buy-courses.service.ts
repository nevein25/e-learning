import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ISession, ICustomerPortal, ICourse } from '../_models/IMemberShipPlan';

declare const Stripe: (arg0: string) => any;

@Injectable({
  providedIn: 'root'
})

export class BuyCoursesService {

  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCourse(courseId: string): Observable<ICourse> {
    return this.http.get<ICourse>(`${this.baseUrl}courses/${courseId}`);
  }

  requestCourseSession(courseId: string): void {
    this.http
      .post<ISession>(this.baseUrl + 'buycourses/create-checkout-session', {
        courseId: courseId,
        priceId: "60000",
        successUrl: environment.successUrl,
        failureUrl: environment.cancelUrl,
      })
      .subscribe((session) => {
        this.redirectToCheckout(session);
      });
  }

  redirectToCheckout(session: ISession) {
    const stripe = Stripe(session.publicKey);
    stripe.redirectToCheckout({
      sessionId: session.sessionId,
    });
  }

  redirectToCustomerPortal() {
    this.http
      .post<ICustomerPortal>(
        this.baseUrl + 'buycourses/customer-portal',
        { returnUrl: environment.homeUrl }
      )
      .subscribe((data) => {
        window.location.href = data.url;
      });
  }
}
