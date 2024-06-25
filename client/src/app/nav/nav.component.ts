import { Component, OnInit, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { HasRoleDirective } from '../_directives/has-role.directive';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterLink, HasRoleDirective, RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit {

  accountService = inject(AccountService);
  ngOnInit(): void {
    this.accountService.setCurrentUserOnOpenApp();
  }

  logout() {
    this.accountService.logout();
  }
}
