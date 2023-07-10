import { BuildResult } from "../enums/BuildResult.enum"

export interface IProgress {
   progress: number,
   step: number,
   result: BuildResult
}