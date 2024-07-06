import { Component, OnInit, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { HasRoleDirective } from '../_directives/has-role.directive';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterLink, HasRoleDirective, RouterLinkActive, NgIf],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {

  accountService = inject(AccountService);
  router = inject(Router);


  ngOnInit(): void {
    this.accountService.setCurrentUserOnOpenApp();
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['/', 'home']);
  }
}
