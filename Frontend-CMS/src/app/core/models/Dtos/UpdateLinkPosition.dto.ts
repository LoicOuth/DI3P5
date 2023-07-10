export class UpdateLinkPositionDto {
    constructor(
        public pageId: string,
        public elementId: string,
        public positionCounter: number
    ) { }
}