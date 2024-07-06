import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MembershipService } from '../../_services/membership.service';
import { IMemberShipPlan } from '../../_models/IMemberShipPlan';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-membership-options',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './membership-options.component.html',
  styleUrl: './membership-options.component.css'
})
export class MembershipOptionsComponent implements OnInit {
  $membership: Observable<IMemberShipPlan> | undefined;
  constructor(private membershipService: MembershipService) {}

  ngOnInit(): void {
    this.$membership = this.membershipService.getMembership();
  }
}
