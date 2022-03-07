import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../../../environments/environment';
import {User} from "../../Models/user";
import {loginInput} from "../../Models/loginInput";

const tokenName = 'idToken';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private isLogged$ = new BehaviorSubject(false);
  private url = `${environment.apiBaseUrl}/api/auth`;
  private user;

  constructor(private http: HttpClient) {

  }

  public get isLoggedIn(): boolean {
    return this.isLogged$.value;
  }

  public login(data: loginInput): Observable<User> {
    return this.http.post<User>(this.url + '/login', data).pipe(map( user => {
      this.isLogged$.next(true);
      return user;
    }));
  }


  public logout() {
        localStorage.clear();
        this.user = null;
        this.isLogged$.next(false);
        return of(false);
      };


  public signup(data: loginInput): Observable<User> {
    return this.http.post<User>(this.url + '/register', data).pipe(map( user => {
      this.isLogged$.next(true);
      return user;
    }));
  }

  public get authToken(): string {
    return localStorage.getItem(tokenName);
  }

  public get userData(): Observable<any> {
    return this.loadUser();
  }

  private loadUser(): Observable<any> {
    // Get's email from localStorage
    if (localStorage.getItem('email')) {
      this.user = {
        email: localStorage.getItem('email'),
      };
    }
    return of(this.user);
  }
}
