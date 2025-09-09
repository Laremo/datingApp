import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private htttp = inject(HttpClient);
  protected readonly title = signal('Dating App');
  protected members = signal<any>([]);
  async ngOnInit(): Promise<void> {
    this.members.set(await this.getMembers());
  }

  async getMembers() : Promise<object> {
    try {
      return lastValueFrom(this.htttp.get('https://localhost:7121/api/members'));
    } catch (error: unknown) {
      console.log(error);
      throw error;
    }
  }
}
