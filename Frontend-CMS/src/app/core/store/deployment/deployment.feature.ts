import { createFeature, createReducer, on } from '@ngrx/store';
import { ISite } from '../../interfaces/ISite.interface';
import { DeploymentActions } from './deployment.actions';
import { IProgress } from '../../interfaces/IProgress.interface';
import { BuildResult } from '../../enums/BuildResult.enum';

export interface DeploymentState {
  site: ISite | null;
  isPublishing: boolean;
  progress: IProgress;
}

const initialState: DeploymentState = {
  site: null,
  isPublishing: false,
  progress: {
    progress: 0,
    step: 1,
    result: BuildResult.None,
  },
};

export const DeploymentFeature = createFeature({
  name: 'deployment',
  reducer: createReducer(
    initialState,
    on(DeploymentActions.loadSuccess, (state: DeploymentState, { site }) => ({
      ...state,
      site,
    })),

    on(
      DeploymentActions.changePublishing,
      (state: DeploymentState, { isPublishing }) => ({
        ...state,
        isPublishing,
      })
    ),

    on(DeploymentActions.onProgress, (state: DeploymentState, { progress }) => {
      console.log(progress);

      return { ...state, progress };
    })
  ),
});

export const {
  name,
  reducer,
  selectDeploymentState,
  selectSite,
  selectProgress,
  selectIsPublishing,
} = DeploymentFeature;
