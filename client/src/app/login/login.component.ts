import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { UserLogin } from '../_models/UserLogin';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  router = inject(Router);
  private toaster = inject(ToastrService);


  model: UserLogin = {
    emailOrUsername: '',
    password: ''
  };

  login() {
    if (!this.model.emailOrUsername || !this.model.password) {
      console.log('Form is incomplete');
      return;
    }

    this.accountService.login(this.model).subscribe({
      next: _ => {
        this.toaster.success("Registration Successful!");

        this.router.navigate(['/', 'home'])
      },
      error: error => this.toastr.error(error.error)

    });
  }
}
