import { Component } from '@angular/core'
import { Store } from '@ngrx/store'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Frontend-CMS';


  constructor(private store: Store) {
    if ((window as any).Cypress) {
      (window as any).store = this.store
    }
  }
}

