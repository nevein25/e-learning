import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { NavComponent } from "./nav/nav.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [
        RouterOutlet,
        BsDropdownModule,
        LoginComponent,
        RegisterComponent,
        NavComponent
    ]
})
export class AppComponent implements OnInit {
  
  title = 'client';

  /* this is the new way of injecting
     http = inject(HttpClient);
    instead of
      constructor(private http: HttpClient) {}
  */
  http = inject(HttpClient);
  /// JUST FOR TESTING!!!!!
  ngOnInit(): void {
    this.http.get("https://localhost:7154/WeatherForecast")
    .subscribe({
      next: response => console.log(response)
      
    });
  }

  
}
