import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  submitted = false;
  errorMessage: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      service: ['', Validators.required], 
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      acceptTerms: [false, Validators.requiredTrue]
    }, {
      validator: this.mustMatch('password', 'confirmPassword')
    });
  }

  get f() { return this.registerForm.controls; }

  mustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
        return;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  onSubmit() {
    this.submitted = true;
    this.errorMessage = null;

    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;

    const userRegistrationData = {
      fullName: this.f['fullName'].value,
      email: this.f['email'].value,
      password: this.f['password'].value
    };

    const API_URL = 'https://localhost:7197/api/Users';

    this.http.post<string>(API_URL, userRegistrationData, { responseType: 'text' as 'json' }).subscribe({
      next: (responseString) => { 
        
        let userId = responseString;
        if (userId.startsWith('"') && userId.endsWith('"')) {
          userId = userId.substring(1, userId.length - 1); 
        }

        this.loading = false;
        const fullName = this.f['fullName'].value;

        


        if (fullName && userId) {
          this.router.navigate(['/welcome', fullName, userId]);
        } else {
          console.warn('fullName ou userId está vazio, redirecionando sem os dados completos.');
          this.router.navigate(['/welcome']);
        }
      },
      error: (error) => {
        console.error('Erro ao cadastrar usuário:', error);
        this.loading = false;
        
        if (error.error && typeof error.error === 'object' && error.error.message) {
            this.errorMessage = error.error.message;
        } else if (typeof error.error === 'string') { 
            this.errorMessage = error.error;
        } else {
            this.errorMessage = 'Erro ao cadastrar. Tente novamente.';
        }

        if (error.status === 400 && error.error?.errors) {
            this.errorMessage = Object.values(error.error.errors).flat().join('; ');
        } else if (error.status === 409) {
            this.errorMessage = 'Este e-mail já está em uso. Por favor, use outro.';
        }
      }
    });
  }
}