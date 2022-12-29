import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

import { User } from '../models/user';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenSub = new BehaviorSubject<string | undefined>(undefined);
  public tokenChanged = this.tokenSub.asObservable();
  public get token() {
    return this.tokenSub.value;
  }

  constructor(private http: HttpClient, private router: Router) {
    const token = localStorage.getItem('access_token') ?? undefined;
    if (!!token) {
      this.refreshToken(token);
    }
  }

  public register(user: User) {
    return this.http
      .post<Token>('/api/Accounts/register', user)
      .subscribe((res) => {
        if (res.token) {
          localStorage.setItem('access_token', res.token);
          this.tokenSub.next(res.token);
          this.router.navigate(['/admin']);
        }
      });
  }

  public login(user: User) {
    return this.http
      .post<Token>('/api/Accounts/login', user)
      .subscribe((res) => {
        if (res.token) {
          localStorage.setItem('access_token', res.token);
          this.tokenSub.next(res.token);
          this.router.navigate(['/admin']);
        }
      });
  }

  public logout() {
    this.tokenSub.next(undefined);
    localStorage.removeItem('access_token');
    this.router.navigate(['/']);
  }

  public refreshToken(token: string) {
    this.http
      .get<Token>('/api/Accounts/refresh', {
        headers: { Authorization: 'bearer ' + token },
      })
      .subscribe({
        next: (res: any) => {
          if (res.token) {
            localStorage.setItem('access_token', res?.token);
            this.tokenSub.next(res.token);
            this.router.navigate(['/admin']);
          }
        },
        error: (err) => {
          console.error(err);
          this.tokenSub.next(undefined);
          localStorage.removeItem('access_token');
          this.router.navigate(['/']);
        },
        complete: () => this.tokenSub.next(token),
      });
  }
}
type Token = { token?: string };
