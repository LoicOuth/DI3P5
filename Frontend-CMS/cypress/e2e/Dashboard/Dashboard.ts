import { Then, When } from '@badeball/cypress-cucumber-preprocessor'

When('I am in Dashboard', () => {
   cy.navigateToDashboard()
})

Then('I should view the current page name', () => {
   cy.get('span[class^="mat-select-min-line"]').first().should("contain.text", "Home")
})

When('I click on Add Block button', () => {
   cy.clickOnSidebarMenu("Add a block")
})

Then("I should view a list of block in my page", () => {
   cy.get('mat-icon[data-mat-icon-name^="drag_indicator"').first().parent().children("div").first().children().first().should("contain.text", "Block 1")
})

Then("I should view a text with test E2E", () => {
   cy.get('h1[id="c8f0657b-df99-40c7-98d6-29964803408e"]').should("contain.text", "test E2E")
})