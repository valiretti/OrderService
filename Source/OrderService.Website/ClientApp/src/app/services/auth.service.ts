import { environment } from './../../environments/environment';
import { AuthConfig, OAuthService, JwksValidationHandler, OAuthStorage } from 'angular-oauth2-oidc';
import { Observable, BehaviorSubject, from, of } from 'rxjs';
import { User } from '../models/user';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export const authConfig: AuthConfig = {
  issuer: 'OrderService',
  redirectUri: window.location.origin + '/counter',
  clientId: 'angular',
  dummyClientSecret: 'secret',
  scope: 'openid profile email api advanced offline_access',
  oidc: false,
  strictDiscoveryDocumentValidation: false
};

@Injectable()
export class AuthService {
  private loggedInSubject: BehaviorSubject<boolean>;
  private currentUserSubject: BehaviorSubject<any>;
  private userRoleSubject: BehaviorSubject<string>;
  private baseUrl: string = environment.baseUrl;

  constructor(
    private authService: OAuthService,
    private http: HttpClient
  ) {
    this.configureWithNewConfigApi();

    this.loggedInSubject = new BehaviorSubject<boolean>(this.isAuthenticated());
    const user: any = this.authService.getIdentityClaims();
    this.userRoleSubject = new BehaviorSubject<string>(user && user.role);
    this.currentUserSubject = new BehaviorSubject<any>(user);
  }

  private configureWithNewConfigApi() {
    this.authService.configure(authConfig);
    this.authService.setupAutomaticSilentRefresh();
    this.authService.tokenValidationHandler = new JwksValidationHandler();
    this.authService.loadDiscoveryDocument(`${this.baseUrl}/.well-known/openid-configuration`);
  }

  get accessToken() {
    return this.authService.getAccessToken();
  }

  isLoggedInObservable(): Observable<boolean> {
    return this.loggedInSubject;
  }

  isAuthenticated(): boolean {
    return this.authService.hasValidAccessToken();
  }

  register(model: User) {
    return this.http.post(`${this.baseUrl}/api/users/register`, model);
  }

  currentUserObservable(): Observable<any> {
    return this.currentUserSubject.asObservable();
}

userRoleObservable(): Observable<string> {
  return this.userRoleSubject.asObservable();
}

  signIn(email: string, password: string): Observable<any> {
    const signinObservable = from(
      this.authService.fetchTokenUsingPasswordFlowAndLoadUserProfile(
        email,
        password
      )
    );

    signinObservable.subscribe(
      (res: any) => {
        this.loggedInSubject.next(true);
        this.currentUserSubject.next(res);
        this.userRoleSubject.next(res.role);
      }
    );

    return signinObservable;
  }

  signOut(): Observable<any> {
    this.authService.logOut(true);
    const logoutObservable = of(false);
    logoutObservable.subscribe(
      res => {
        this.loggedInSubject.next(res);
      },
    );

    return logoutObservable;
  }

  async refreshUserInfo() {
    try {
      const info: any = await this.authService
        .loadUserProfile();
      this.currentUserSubject.next(info);
      this.userRoleSubject.next(info.role);
    } catch (err) {
      console.error('error refreshed ' + err);
    }
  }

  async refreshToken() {
    try {
      await this.authService
        .refreshToken();
      this.refreshUserInfo();
    } catch (err) {
      console.error('error refreshed Token' + err);
    }
  }
}
