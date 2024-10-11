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
      phoneNumber: [''],
      oldPassWord: [''],
      newPassWord: ['']
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    const formValue = this.profileForm.value;
    
    // Remove empty fields
    const updateData = Object.keys(formValue).reduce((acc: any, key) => {
      if (formValue[key]) {
        acc[key] = formValue[key];
      }
      return acc;
    }, {});

    // Check if both password fields are filled if either is present
    if ((updateData.oldPassWord && !updateData.newPassWord) || 
        (!updateData.oldPassWord && updateData.newPassWord)) {
      this.updateError = 'Both old and new passwords are required to update password';
      return;
    }

    this.userService.updateProfile(updateData).subscribe({
      next: (response) => {
        this.updateSuccess = true;
        this.updateError = '';
        this.profileForm.reset();
        alert("profile updated successfully");
        this.router.navigate(['/profile']);
      },
      error: (error) => {
        this.updateError = error.error.message || 'An error occurred while updating profile';
        this.updateSuccess = false;
      }
    });
  }
}

