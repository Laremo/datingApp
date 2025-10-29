import { Component, inject, OnInit, signal } from '@angular/core';
import { MembersService } from '../../../core/services/members-service';
import { ActivatedRoute, NavigationEnd, RouterOutlet } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { filter, Observable } from 'rxjs';
import { Member } from '../../../types/Member';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
@Component({
  selector: 'app-member-detail',
  imports: [RouterLink, RouterLinkActive, RouterOutlet],
  templateUrl: './member-detail.html',
  styleUrl: './member-detail.css',
})
export class MemberDetail implements OnInit {
  private membersService = inject(MembersService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  protected member$?: Observable<Member>;
  protected member = signal<Member | undefined>(undefined);
  protected title = signal<string | undefined>('Profile');

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.member.set(data['member']);
      },
    });
    this.title.set(this.route.firstChild?.snapshot?.title);

    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe({
      next: () => {
        this.title.set(this.route.firstChild?.snapshot?.title);
      },
    });
  }
}
