import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../services/user-profile.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  profileForm: FormGroup;
  profileImage: File | null = null; // To hold the selected file
  updateSuccess = false;
  updateError = '';

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.profileForm = this.fb.group({
      userName: [''],
      email: [''],
      firstName: [''],
      lastName: [''],
      phoneNumber: [''],
      preferredCurrency: [''],
      preferredLanguage: [''],
      oldPassWord: [''],
      newPassWord: ['']
    });
  }

  ngOnInit(): void {}

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.profileImage = file;
    }
  }

  onSubmit(): void {
    const formValue = this.profileForm.value;

    // Check if both password fields are filled if either is present
    if ((formValue.oldPassWord && !formValue.newPassWord) || 
        (!formValue.oldPassWord && formValue.newPassWord)) {
      this.updateError = 'Both old and new passwords are required to update password';
      return;
    }

    const formData = new FormData();
    formData.append('userName', formValue.userName);
    formData.append('email', formValue.email);
    formData.append('firstName', formValue.firstName);
    formData.append('lastName', formValue.lastName);
    formData.append('phoneNumber', formValue.phoneNumber);
    formData.append('preferredCurrency', formValue.preferredCurrency);
    formData.append('preferredLanguage', formValue.preferredLanguage);
    formData.append('oldPassWord', formValue.oldPassWord);
    formData.append('newPassWord', formValue.newPassWord);
    formData.append('profileImage', this.profileImage ? this.profileImage : '');

    this.userService.updateProfile(formData).subscribe({
      next: (response) => {
        this.updateSuccess = true;
        this.updateError = '';
        this.profileForm.reset();
        this.profileImage = null;
        alert("profile updated successfully");
        this.router.navigate(['/profile']);
      },
      error: (err) => {
        console.log("updatePRofile error:", err);
        this.updateError = err.error.message || 'An error occurred while updating profile';
        this.updateSuccess = false;
      }
    });
  }
}

