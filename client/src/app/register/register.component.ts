import { Component, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { UserRegister } from '../_models/UserRegister';
import { Role } from '../_models/Roles.enum';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  private toaster = inject(ToastrService);
  model: UserRegister = {
    email: '',
    name: '',
    password: '',
    username: '',
    role: Role.Student
  }
  roles = Object.values(Role);
  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => this.toaster.error(error)
    })
  }

}
