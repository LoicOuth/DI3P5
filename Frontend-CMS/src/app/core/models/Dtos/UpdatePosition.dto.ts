export default class UpdatePositionDto {
   public elementId: string
   public positionCounter: number
   public pageId: string

   constructor(elementId: string, positionCounter: number, pageId: string) {
     this.elementId = elementId
     this.positionCounter = positionCounter
     this.pageId = pageId
   }
}