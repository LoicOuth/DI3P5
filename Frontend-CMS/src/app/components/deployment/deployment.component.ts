import { Component, Optional } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { first } from 'rxjs';
import { DeploymentActions } from 'src/app/core/store/deployment/deployment.actions';
import { DeploymentFeature } from 'src/app/core/store/deployment/deployment.feature';

@Component({
  selector: 'app-deployment',
  templateUrl: './deployment.component.html',
})
export class DeploymentComponent {
  progress = this.store.select(DeploymentFeature.selectDeploymentState);
  comment = '';

  constructor(
    private store: Store,
    @Optional() public dialogRef?: MatDialogRef<DeploymentComponent>
  ) {}

  publish(): void {
    this.progress.pipe(first()).subscribe((_) => {
      this.store.dispatch(
        DeploymentActions.startDeployment({ comment: this.comment })
      );

      if (this.dialogRef) {
        this.dialogRef.close();
      }
    });
  }
}
