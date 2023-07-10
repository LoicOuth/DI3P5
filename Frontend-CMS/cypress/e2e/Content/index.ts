import { Then, When } from "@badeball/cypress-cucumber-preprocessor"

When('I am in Dashboard', () => {
   cy.navigateToDashboard()
})

When("I click on content menu", () => {
   cy.clickOnSidebarMenu("Add content")
})

Then("I should view expanded menu", () => {
   cy.get("add-content").should("exist")
})