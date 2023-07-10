import { Component } from '@angular/core'
import { environment } from 'src/environments/environment'

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent {
  returnUrl = environment.home_url
}
