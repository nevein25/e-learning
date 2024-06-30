import { Component, OnInit, inject } from '@angular/core';
import { Course } from '../_models/course';
import { WishlistService } from '../_services/wishlist.service';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { EnrollComponent } from "../payments/enroll/enroll.component";
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-wishlist',
    standalone: true,
    templateUrl: './wishlist.component.html',
    styleUrl: './wishlist.component.css',
    imports: [CommonModule, EnrollComponent, RouterLink]
})
export class WishlistComponent implements OnInit {
  wishlist: any[] = []; // Assuming Course model is defined

  toastr = inject(ToastrService);
  constructor(private wishlistService: WishlistService) { }

  ngOnInit(): void {
    this.loadWishlist();
  }

  loadWishlist(): void {
    // Replace with logic to fetch studentId from AuthService or another source
    const studentId = 1; // Example studentId

    this.wishlistService.getWishlist()
      .subscribe(
        (wishlist: Course[]) => {
          this.wishlist = wishlist;
          console.log(this.wishlist)
        },
        (error) => {
          console.error('Error loading wishlist:', error);
        }
      );
  }

  addToWishlist(courseId: number): void {
    if (courseId) {
      this.wishlistService.addToWishlist(courseId)
        .subscribe(
          () => {
            console.log('Course added to wishlist successfully');
            // Optionally reload the wishlist or show a success message
            this.loadWishlist();
          },
          (error) => {
            console.error('Error adding course to wishlist:', error);
          }
        );
    } else {
      console.error('Invalid courseId:', courseId);
    }
  }

  removeFromWishlist(courseId: number): void {
    if (!courseId) {
      console.error('CourseId is undefined or null.');
      return;
    }

    this.wishlistService.removeFromWishlist(courseId)
      .subscribe(
        () => {
          // Remove the course from the local wishlist array
          this.wishlist = this.wishlist.filter(course => course.id !== courseId);
          this.loadWishlist();

        },
        (error) => {
          console.error('Error removing course from wishlist:', error);
        }
      );
  }
}
