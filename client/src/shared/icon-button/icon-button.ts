import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-icon-button',
  imports: [],
  templateUrl: './icon-button.html',
  styleUrl: './icon-button.css',
})
export class IconButton {
  clickEvent = output<Event>();
  selected = input<boolean>();
  disabled = input<boolean>();

  onClick(event: Event) {
    this.clickEvent.emit(event);
  }
}
