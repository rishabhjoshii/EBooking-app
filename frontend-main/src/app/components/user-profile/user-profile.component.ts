import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../services/user-profile.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [FormsModule,CommonModule,ReactiveFormsModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class ProfileComponent {
    userProfile: any = {};
    errorMessage: string = '';
  
    constructor(private userService: UserService, private router: Router) {}
  
    ngOnInit(): void {
      const user = localStorage.getItem('currentUser');
      if(user)
      {
        var usernew= JSON.parse(user);
        var token=usernew.token;
      } // Retrieve token from localStorage
  
      if (token) {
        this.userService.getUserProfile(token).subscribe({
          next: (response) => {
            // Display the rest of the profile data
            this.userProfile = {
              userName: response.userName,
              email: response.email,
              phoneNumber: response.phoneNumber
            };
          },
          error: (error) => {
            this.errorMessage = 'Failed to load user profile.';
          }
        });
      } else {
        this.errorMessage = 'No token found. Please login first.';
      }
    }
  
    // Navigate to the Edit Profile page
    onEditProfile() {
      this.router.navigate(['/profile/edit']);
    }
  }


