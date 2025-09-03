import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private htttp = inject(HttpClient);
  protected readonly title = signal('Dating App');
  ngOnInit(): void {
    this.htttp.get('https://localhost:7121/api/members').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.error(error),
      complete: () => {
        console.log('completed!');
      },
    });
  }
}
