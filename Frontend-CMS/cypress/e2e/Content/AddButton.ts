import { Then, When } from "@badeball/cypress-cucumber-preprocessor"
import { getElement } from "../../support/commands"
import { ADD_ELEMENT, NEW_ELEMENT } from "../../../src/app/core/constants/events.constant"
import { TypeElement } from "../../../src/app/core/enums/TypeElement.enum"

const NEW_BUTTON_ID = "a7bfd7a4-9139-499c-82ca-541e7087f119"


When('I drag and drop a button content', () => {
   let parentId

   cy.fixture("elements").then(elements => parentId = elements[0].id)

   cy.get("app-content-item").contains("Text").then(text => {
      cy.get(`div[id="${parentId}"]`).then(block => {
         cy.dragAndDrop(text[0], block[0])
      })
   })

   cy.hubVerifyInvokes("elements", ADD_ELEMENT, (_) => {
      cy.hubPublish("elements", NEW_ELEMENT, getElement(TypeElement.Button, NEW_BUTTON_ID, parentId, "button"))
   })

})

Then("I Should view a new button element in my page", () => {
   cy.get(`button[id="${NEW_BUTTON_ID}"]`).should("exist").should("contain.text", "button")
   cy.matchImage()
})