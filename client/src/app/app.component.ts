import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HttpClientModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  
  title = 'client';

  constructor(private http: HttpClient) {
   
    
  }
  /// JUST FOR TESTING!!!!!
  ngOnInit(): void {
    this.http.get("https://localhost:7154/WeatherForecast")
    .subscribe({
      next: response => console.log(response)
      
    });
  }

  
}
