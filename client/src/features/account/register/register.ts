import { Component, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../core/services/account-service';
import { RegisterCreds } from '../../../types/user';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private accoutService = inject(AccountService);
  cancelRegister = output<boolean>();
  protected creds = {} as RegisterCreds;

  register(): void {
    this.accoutService.register(this.creds).subscribe({
      next: (response) => {
        console.log(response);
        this.cancel();
      },
    });
  }

  cancel(): void {
    this.cancelRegister.emit(false);
  }
}
