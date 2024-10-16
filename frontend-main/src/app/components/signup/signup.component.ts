import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

//custom validatory for password
export function passwordStrengthValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const password = control.value;
    if (!password) return null; // Skip validation if empty

    // Regex for checking password requirements
    const hasUpperCase = /[A-Z]/.test(password);
    const hasLowerCase = /[a-z]/.test(password);
    const hasDigit = /\d/.test(password);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);

    const valid = hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;

    return valid ? null : { passwordStrength: true }; // Return null if valid, else return an error object
  };
}

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule,CommonModule,ReactiveFormsModule,RouterModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  signupForm: FormGroup;
  error: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.signupForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      password: ['', [Validators.required, Validators.minLength(4), passwordStrengthValidator()]]
    });
  }

  convertToLowercase(controlName: string) {
    const currentValue = this.signupForm.get(controlName)?.value;
    if (currentValue) {
      this.signupForm.get(controlName)?.setValue(currentValue.toLowerCase(), { emitEvent: false });
    }
  }

  onSubmit() {
    if (this.signupForm.valid) {
      this.authService.signup(this.signupForm.value).subscribe({
        next: () => {
          this.router.navigate(['/login']);
        },
        error: (err) => {
          console.log("error here:" ,err);
          this.error = err.error;
        }
      });
    }
  }
}