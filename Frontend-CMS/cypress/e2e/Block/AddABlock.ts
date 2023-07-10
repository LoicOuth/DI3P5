import { TypeElement } from '../../../src/app/core/enums/TypeElement.enum'
import { getElement } from '../../support/commands'
import { ADD_ELEMENT, NEW_ELEMENT } from './../../../src/app/core/constants/events.constant'
import { Then, When } from "@badeball/cypress-cucumber-preprocessor"

const NEW_BLOCK_ID = "bf832066-e09c-4c9e-9282-30db963f16e8"

When('I click on Add Block button', () => {
   cy.clickOnSidebarMenu("Add a block")
})

When('I click on add button', () => {
   cy.get('mat-icon[data-mat-icon-name="add_circle_outline"]').first().click()
})

Then("A dialog appear with form", () => {
   cy.get("app-add-block").first().children().first().should("contain.text", "Add a block")
})

When("I fill all fields", () => {
   cy.get('input[formcontrolname="name"]').type("Block 2")
   cy.get('textarea[formcontrolname="description"]').type("desc block 2")
})

When("I click on create button", () => {
   cy.get("button > span").contains("Create").first().click()

   cy.hubVerifyInvokes("elements", ADD_ELEMENT, (_) => {
      cy.hubPublish("elements", NEW_ELEMENT, getElement(TypeElement.Block, NEW_BLOCK_ID))
   })
})

Then("A new block appear under last block", () => {
   cy.get(`div[id="${NEW_BLOCK_ID}"]`).should("exist")
})

Then("A new block appear in Add block menu", () => {
   cy.clickOnSidebarMenu("Add a block")

   cy.get('mat-icon[data-mat-icon-name^="drag_indicator"').last().parent().children("div").first().children().first().should("contain.text", "Block 2")
   cy.matchImage()
})





