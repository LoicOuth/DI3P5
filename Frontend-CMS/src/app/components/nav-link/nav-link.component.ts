import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'nav-link',
  templateUrl: './nav-link.component.html',
  styleUrls: ['./nav-link.component.css'],
})
export class NavLinkComponent {
  @Input() icon = '';
  @Input() label = '';
  @Input() popup?: any;
  @Input() width = '80%';
  @Input() height = '80%';

  public show = false;
  constructor(private dialog: MatDialog) {}

  showPopup() {
    this.dialog.open(this.popup!, {
      width: this.width,
      height: this.height,
      disableClose: true,
    });
  }
}
