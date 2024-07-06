import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ICustomerPortal, IMemberShipPlan, ISession } from '../_models/IMemberShipPlan';

declare const Stripe: (arg0: string) => any;

@Injectable({
  providedIn: 'root'
})
export class MembershipService {

  baseUrl: string = environment.apiUrl;
  //successUrl = environment.
  constructor(private http: HttpClient) {}

  getMembership(): Observable<IMemberShipPlan> {
    return of({
      id: 'prod_QLrAEOjNYjd1TJ',
      priceId: 'price_1PV9SM2MXG6xva1oTuiQTLjC',
      name: 'Awesome Membership Plan',
      price: '$9.00',
      features: [
        'Up to 5 users',
        'Basic support on Github',
        'Monthly updates',
        'Free cancelation',
      ],
    });
  }

  requestMemberSession(priceId: string): void {
    this.http
      .post<ISession>(this.baseUrl + 'payments/create-checkout-session', {
        priceId: priceId,
        successUrl: environment.successUrl,
        failureUrl: environment.cancelUrl,
        courseId:"7"
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
        this.baseUrl + 'payments/customer-portal',
        { returnUrl: environment.homeUrl }
      )
      .subscribe((data) => {
        window.location.href = data.url;
      });
  }
}
