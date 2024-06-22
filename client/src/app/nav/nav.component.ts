import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterLink],
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
