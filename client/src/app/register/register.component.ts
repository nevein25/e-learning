import { Component, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { UserRegister } from '../_models/UserRegister';
import { Role } from '../_models/Roles.enum';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';
import { InstructorService } from '../_services/instructor.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  private instructorService = inject(InstructorService);
  private toaster = inject(ToastrService);
  router = inject(Router);


  model: UserRegister = {
    email: '',
    name: '',
    password: '',
    username: '',
    role: Role.Student,
    paper: ''

  }
  file: File | undefined;

  roles = Object.values(Role);
  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {
        this.toaster.success("Registration Successful!");
        this.UploadPaper();
        this.router.navigate(['/', 'login']);

      },
      error: error => {
        let returnedError = error;
        if (Array.isArray(error)) {
          const containsPasswordError = error.some(msg => msg.toLowerCase().includes('password'));
          if (!containsPasswordError) {
            this.toaster.error(returnedError);
          }
        }

      }
    })
  }

  validatePassword(control: any): { [key: string]: boolean } | null {
    const value = control.value;
    if (!value) {
      return null;
    }

    const hasUpperCase = /[A-Z]+/.test(value);
    const hasLowerCase = /[a-z]+/.test(value);
    const hasNumeric = /[0-9]+/.test(value);
    const validLength = value.length >= 6 && value.length <= 12;

    const passwordValid = hasUpperCase && hasLowerCase && hasNumeric && validLength;

    return !passwordValid ? { passwordStrength: true } : null;
  }

  // OnClick of button Upload
  UploadPaper() {
    console.log(this.file);
    this.instructorService.uploadPaper(this.file, this.model.username).subscribe({
      next: _ => {
        //this.router.navigate(['/', 'course']);

      },
      error: error => console.log(error)

    })
  }
  onChangePaper(event: any) {
    this.file = event.target.files[0];
  }



}
