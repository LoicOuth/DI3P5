import { StyleProperty } from "../../enums/StyleProperty.enum"

export default class AddStyleDto {
   public elementId: string
   public property: StyleProperty
   public value: string

   constructor(elemenetId: string, property: StyleProperty, value: string) {
      this.elementId = elemenetId
      this.property = property
      this.value = value
   }
}