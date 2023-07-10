export default class UpdateContentDto {
   public elementId: string
   public content: string

   constructor(elemenetId: string, content: string) {
      this.elementId = elemenetId
      this.content = content
   }
}