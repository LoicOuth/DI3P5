import { When } from "@badeball/cypress-cucumber-preprocessor"

When('I am in Dashboard', () => {
   cy.navigateToDashboard()
})