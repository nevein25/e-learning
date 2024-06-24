import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { IMemberShipPlan } from '../../_models/IMemberShipPlan';
import { MembershipService } from '../../_services/membership.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-membership-checkout',
  standalone: true,
  imports: [FormsModule, RouterLink, CommonModule],
  templateUrl: './membership-checkout.component.html',
  styleUrl: './membership-checkout.component.css'
})
export class MembershipCheckoutComponent {
  $membership: Observable<IMemberShipPlan> | undefined;
  constructor(private membershipService: MembershipService) {}

  ngOnInit(): void {
    this.$membership = this.membershipService.getMembership();
  }

  onSubmit(f: NgForm) {
    this.membershipService.requestMemberSession(f.value.priceId);
  }
}
