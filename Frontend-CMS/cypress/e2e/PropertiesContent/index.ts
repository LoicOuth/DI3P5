import { Given, When } from "@badeball/cypress-cucumber-preprocessor"

export const TEXT_TO_SELECT = "c8f0657b-df99-40c7-98d6-29964803408e"

When('I am in Dashboard', () => {
   cy.navigateToDashboard()
})

Given("Text element is selected", () => {
   cy.openProperties(TEXT_TO_SELECT)
})