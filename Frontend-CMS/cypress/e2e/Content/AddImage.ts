import { Then, When } from "@badeball/cypress-cucumber-preprocessor"
import { ADD_ELEMENT, NEW_ELEMENT } from "../../../src/app/core/constants/events.constant"
import { TypeElement } from "../../../src/app/core/enums/TypeElement.enum"
import { getElement } from "../../support/commands"

const NEW_IMAGE_ID = "c9b3d5c3-90b7-4fcb-8521-e706e489160c"

When('I drag and drop a image content', () => {
   let parentId

   cy.fixture("elements").then(elements => parentId = elements[0].id)

   cy.get("app-content-item").contains("Text").then(text => {
      cy.get(`div[id="${parentId}"]`).then(block => {
         cy.dragAndDrop(text[0], block[0])
      })
   })

   cy.hubVerifyInvokes("elements", ADD_ELEMENT, (_) => {
      cy.hubPublish("elements", NEW_ELEMENT, getElement(TypeElement.Image, NEW_IMAGE_ID, parentId, null, "image_element", "https://usitestorageaccountimgs.blob.core.windows.net/file-upload/default.jpg"))
   })

})

Then("I Should view a new image element in my page", () => {
   cy.get(`img[id="${NEW_IMAGE_ID}"]`).should("exist")
   cy.matchImage()
})