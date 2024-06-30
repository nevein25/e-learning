import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { UserLogin } from '../_models/UserLogin';
import { Router, RouterLink } from '@angular/router';
import { InstructorService } from '../_services/instructor.service';

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

  role: any;


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
        this.role = this.accountService.getLogedInUserRole();

        if (this.role === "Student")
          this.router.navigate(['/', 'course-list']);

        else if (this.role === "Instructor") {
          if (this.accountService.isInstructorVerfied() === "True") {
            console.log("trrrrr");
            console.log(typeof this.accountService.isInstructorVerfied());


            this.router.navigate(['/', 'course']);
          }
          else {
            console.log("ffffffff");

            this.router.navigate(['/', 'instructor-home']);

          }
          console.log(this.accountService.isInstructorVerfied());

        }


        else
          this.router.navigate(['/', 'home']); // change it later

        this.toaster.success("Login Successful!");


      },
      error: error => this.toastr.error(error.error)

    });
  }

  // isLoggedInInstructorVerified() {
  //   this.instructorService.isLoggedInInstructorVerified().subscribe({
  //     next: res => {
  //       console.log(res);
  //       this.isInstructorVerfied = res.isVerified;
  //     }
  //   });
  // }
}
