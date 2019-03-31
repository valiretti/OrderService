import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService, JwksValidationHandler, OAuthStorage } from 'angular-oauth2-oidc';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { User } from '../models/user';
import { from } from 'rxjs/observable/from';
import { of } from 'rxjs/observable/of';

export const authConfig: AuthConfig = {
  issuer: 'OrderService',
  redirectUri: window.location.origin + '/counter',
  clientId: 'angular',
  dummyClientSecret: 'secret',
  scope: 'openid profile email api offline_access',
  oidc: false,
  strictDiscoveryDocumentValidation: false
};


@Injectable()
export class AuthService {
  baseUrl: string = 'https://localhost:55340';
  userAuthenticated = false;
  private loggedInSubject: BehaviorSubject<boolean>;
  private currentUserSubject: BehaviorSubject<any>;

  constructor(
    private authService: OAuthService,
    private http: HttpClient
  ) {
    this.configureWithNewConfigApi();

    this.loggedInSubject = new BehaviorSubject<boolean>(this.isAuthenticated());
    this.currentUserSubject = new BehaviorSubject<any>(
        this.authService.getIdentityClaims() as any
    );
  }

  private configureWithNewConfigApi() {
    this.authService.configure(authConfig);
    this.authService.tokenValidationHandler = new JwksValidationHandler();
    this.authService.loadDiscoveryDocument(`${this.baseUrl}/.well-known/openid-configuration`);
  }

  get accessToken() {
    return this.authService.getAccessToken();
  }

  isAuthenticated(): boolean {
    return this.authService.hasValidAccessToken();
  }

  register(model: User) {
    return this.http.post(`${this.baseUrl}/api/users/register`, model);
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
        this.userAuthenticated = true;
        this.loggedInSubject.next(true);
        this.currentUserSubject.next(res);
      }
    );

    return signinObservable;
  }

  signOut(): Observable<any> {
    this.authService.logOut(true);
    const logoutObservable = of(false);
    logoutObservable.subscribe(
      res => {
        this.userAuthenticated = res;
        this.loggedInSubject.next(res);
      },
    );

    return logoutObservable;
  }

  refreshUserInfo() {
    this.authService
      .loadUserProfile()
      .then(info => {
        this.currentUserSubject.next(info);
      })
      .catch(err => {
        console.error('error refreshed ' + err);
      });
  }

  refreshToken() {
    this.authService
      .refreshToken()
      .then(() => {
        this.refreshUserInfo();
      })
      .catch(err => {
        console.error('error refreshed Token' + err);
      });
  }
}
