import { HttpClient } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { map } from 'rxjs';
import { UserRegister } from '../_models/UserRegister';
import { UserLogin } from '../_models/UserLogin';
import { User } from '../_models/User';
import { Token } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http = inject(HttpClient); // new way, instead of ctor injection
  currentUser = signal<User | null>(null); // so i can use it any where (the new way, instead of observable)
  baseUrl = environment.apiUrl;



  // so i can get value from the currentUser signal
  role = computed(() => {
    const user = this.currentUser();
    if (user && user.token) {
      const role = JSON.parse(atob(user.token.split('.')[1])).role;
      return role;
    }
    return;
  })

  isInstructorVerfied = computed(() => {
    const user = this.currentUser();
    if (user && user.token) {

      const isverified = JSON.parse(atob(user.token.split('.')[1])).isInstructorVerified;

      user.isInstructorVerified

      return user.isInstructorVerified;
    }
    return;
  })

  isSubscriber = computed(() => {
    const user = this.currentUser();
    if (user && user.token) {
      const subscriber = JSON.parse(atob(user.token.split('.')[1])).isSubscriber;
      return subscriber;
    }
    return;
  })

  login(model: UserLogin) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  register(model: UserRegister) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        //if (user)
        //this.setCurrentUser(user);

        return user;
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

  setCurrentUserOnOpenApp() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.setCurrentUser(user);
  }

  getLogedInUserRole() {
    return this.role();
  }

  getVerification() {
    return this.isInstructorVerfied();
  }
  // isLoggedInInstructorVerified() {
  //   return this.http.get<any>(`${this.baseUrl}Instructors/is-verified/`).subscribe({
  //     next: res => {
  //       console.log(res);
  //       this.isInstructorVerfied.set(res.isVerified);
  //     }
  //   });
  // }

}
