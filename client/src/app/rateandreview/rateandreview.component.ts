import { CommonModule } from '@angular/common';
import { Component, NgModule } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-rateandreview',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    BrowserModule 
    
  ],
  templateUrl: './rateandreview.component.html',
  styleUrl: './rateandreview.component.css'
})
export class RateandreviewComponent {
  max = 10;
  rate = 7;
}
