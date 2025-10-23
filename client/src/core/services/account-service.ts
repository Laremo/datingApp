import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { inject, Injectable, signal } from '@angular/core';
import { RegisterCreds, User } from '../../types/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);
  baseUrl = 'https://localhost:7121/api/';

  register(creds: RegisterCreds) {
    return this.http.post<User>(this.baseUrl + 'account/register', creds).pipe(
      tap((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  login(creds: RegisterCreds): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'account/login', creds).pipe(
      tap((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }
  
  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
    console.log(this.currentUser())
  }

  logout(): void {
    this.currentUser.set(null);
    localStorage.removeItem('user');
  }
}
