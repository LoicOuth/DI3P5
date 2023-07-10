import { Component, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { Store } from '@ngrx/store'
import { AuthActions } from 'src/app/core/store/auth/auth.action'

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html'
})
export class CallbackComponent implements OnInit {

  constructor(private route: ActivatedRoute, private store: Store) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(el => {
      this.store.dispatch(
        AuthActions.auth({
          code: el["code"],
          verifier: el["verifier"],
          siteId: el["site"]
        })
      )
    })
  }

}
