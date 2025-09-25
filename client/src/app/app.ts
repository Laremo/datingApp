import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';
import { Nav } from '../layout/nav/nav';
import { AccountService } from '../core/services/account-service';
import { Home } from '../features/home/home';
import { User } from '../types/user';

@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  private htttp = inject(HttpClient);
  protected readonly title = signal('Dating App');
  protected members = signal<User[]>([]);
  async ngOnInit(): Promise<void> {
    this.members.set(await this.getMembers());
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;

    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  async getMembers(): Promise<User[]> {
    try {
      return lastValueFrom(this.htttp.get<User[]>('https://localhost:7121/api/members'));
    } catch (error: unknown) {
      console.log(error);
      throw error;
    }
  }
}
