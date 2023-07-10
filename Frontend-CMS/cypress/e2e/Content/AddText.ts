import { Then, When } from "@badeball/cypress-cucumber-preprocessor"
import { ADD_ELEMENT, NEW_ELEMENT } from "../../../src/app/core/constants/events.constant"
import { TypeElement } from "../../../src/app/core/enums/TypeElement.enum"
import { getElement } from "../../support/commands"

const NEW_TEXT_ID = "3a96426e-c997-4f90-b810-f67bc61c1cf8"

When('I drag and drop a text content', () => {
   let parentId

   cy.fixture("elements").then(elements => parentId = elements[0].id)

   cy.get("app-content-item").contains("Text").then(text => {
      cy.get(`div[id="${parentId}"]`).then(block => {
         cy.dragAndDrop(text[0], block[0])
      })
   })

   cy.hubVerifyInvokes("elements", ADD_ELEMENT, (_) => {
      cy.hubPublish("elements", NEW_ELEMENT, getElement(TypeElement.H1, NEW_TEXT_ID, parentId, "text"))
   })

})

Then("I should view a new text element in my page", () => {
   cy.get(`h1[id="${NEW_TEXT_ID}"]`).should("exist").should("contain.text", "text")
   cy.matchImage()
})