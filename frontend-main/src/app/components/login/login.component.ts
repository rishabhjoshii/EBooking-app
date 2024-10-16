import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,RouterModule,ReactiveFormsModule,FormsModule,RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  loginForm: FormGroup;
  error: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      usernameOrEmail: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  convertToLowercase(controlName: string) {
    const currentValue = this.loginForm.get(controlName)?.value;
    if (currentValue) {
      this.loginForm.get(controlName)?.setValue(currentValue.toLowerCase(), { emitEvent: false });
    }
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: () => {
          this.router.navigate(['/']);
        },
        error: (err) => {
          console.log("login error: ", err);
          this.error = err.error;
        }
      });
    }
  }
}