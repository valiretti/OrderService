import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    isLogIn: boolean;

    constructor(private router: Router,
        private auth: AuthService) {
        this.auth.isLoggedInObservable()
            .subscribe(res => {
                this.isLogIn = res;
            });
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.isLogIn) {
            return true;
        }

        this.router.navigate(['/sign-in'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
