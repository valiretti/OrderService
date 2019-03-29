import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  signInForm: FormGroup
  loading = false;

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  onSubmit(): void {
    if (this.signInForm.invalid) {
      return;
    }
    this.doSignIn(
      this.signInForm.value.email,
      this.signInForm.value.password,
    );
  }

  doSignIn(email: string, password: string): void {
    this.authService.signIn(email, password).subscribe(
      res => {
        this.signInForm.reset();
        this.router.navigate(['/']);
      },
      err => {
        this.loading = false;
      }
    );
  }

}
