import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  signInForm: FormGroup;
  loading = false;
  returnUrl: string;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.signInForm = this.formBuilder.group({
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required]
  });

  this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
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
        this.router.navigate([this.returnUrl]);
      },
      err => {
        this.loading = false;
      }
    );
  }

}
