import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { Subscription } from 'rxjs/Subscription';
import { AccountClient, LoginRequest, LoginResponse } from '../../api/epharmacy';

const jwtTokenKey = 'jwt_token';

export function getJwtToken() {
  return localStorage.getItem(jwtTokenKey);
}

@Injectable()
export class AuthService {
  loginStatusChanged = new EventEmitter();

  _tokenRenewalSubscription: Subscription;
  
  constructor(private jwtHelper: JwtHelperService, private account: AccountClient) {
    if (this.isLoggedIn()) {
      this.account.renewToken().subscribe(response => {
        this.subscribeForPeriodicTokenRenewal();
        this.setToken(response.token);
      });
    }
  }

  logout() {
    localStorage.removeItem(jwtTokenKey);
    this.loginStatusChanged.emit();
  }

  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    const loginSubscriber = (observer) => {
      this.account.login(loginRequest).subscribe(
        response => {
          this.subscribeForPeriodicTokenRenewal();
          this.setToken(response.token);
          console.log(`[DEBUG] ${this.getUserEmail()} login with role: ${this.getUserRole()}`);
          observer.next(response);
          observer.complete();
        },
        err => {
          observer.error(err);
          observer.complete();
        }
      );
    };
    return new Observable<LoginResponse>(loginSubscriber);
  }

  getUserEmail() {
    return this.isLoggedIn() ? this.jwtHelper.decodeToken()['email'] : '';
  }

  getUserRole() {
    return this.isLoggedIn() ? this.jwtHelper.decodeToken()['role'] : null;
  }

  isLoggedIn() {
   return !this.jwtHelper.isTokenExpired();
  }
  isLoggedInAsUser() {
    if (!this.jwtHelper.isTokenExpired()) {
      return  this.getUserRole() === 'User';
     } else {
       return false;
     }
  }
  isLoggedInAsEmployee() {
    if (!this.jwtHelper.isTokenExpired()) {
      return this.getUserRole() === 'Worker';
     } else {
       return false;
     }
  }
  isLoggedInAsAdmin() {
    if (!this.jwtHelper.isTokenExpired()) {
      return this.getUserRole() === 'Admin';
     } else {
       return false;
     }
  }

  private setToken(token: string) {
    localStorage.setItem(jwtTokenKey, token);
    this.loginStatusChanged.emit();
  }

  private subscribeForPeriodicTokenRenewal() {
    if (this._tokenRenewalSubscription && !this._tokenRenewalSubscription.closed) {
      this._tokenRenewalSubscription.unsubscribe();
    }
    this._tokenRenewalSubscription = Observable.interval(19 * 60 * 1000).subscribe(
      () => {
        this.account.renewToken().subscribe(response => this.setToken(response.token), () => { });
      }
    );
  }
}
