import { Then, When } from "@badeball/cypress-cucumber-preprocessor"
import { UPDATE_ELEMENT, UPDATE_ELEMENT_CONTENT } from "../../../src/app/core/constants/events.constant"
import { getElementById } from "../../../src/app/utils/ElementUtils"
import { IElement } from "../../../src/app/core/interfaces/IElement.interface"
import { TEXT_TO_SELECT } from "."

When("I Change the content", () => {

   let element: IElement

   cy.fixture("elements").then(el => {
      element = getElementById(el, TEXT_TO_SELECT)
   })

   cy.get('textarea[ng-reflect-value="test E2E"]').type("new Content")

   cy.hubVerifyInvokes("elements", UPDATE_ELEMENT_CONTENT, (_) => {
      element.content = "new Content"
      cy.hubPublish("elements", UPDATE_ELEMENT, element)
   })
})

Then("I Should view a new content", () => {
   cy.get(`h1[id="${TEXT_TO_SELECT}"]`).should("contain.text", "new Content")
   cy.matchImage()
})