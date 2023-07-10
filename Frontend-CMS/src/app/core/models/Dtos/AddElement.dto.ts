import { TypeElement } from "../../enums/TypeElement.enum"

export default class AddElementDto {
   public name: string
   public description: string | null
   public idParent: string
   public typeElement: TypeElement

   constructor(name: string, description: string | null, idParent: string, typeElement: TypeElement) {
      this.name = name
      this.description = description
      this.idParent = idParent
      this.typeElement = typeElement
   }
}