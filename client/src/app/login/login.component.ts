import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { UserLogin } from '../_models/UserLogin';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  accountService = inject(AccountService); 
  private toastr = inject(ToastrService);


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
      next: _ => this.toastr.success("success"),
      error: error => this.toastr.error(error.error)

    });
  }
}
