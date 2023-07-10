import { Then, When } from "@badeball/cypress-cucumber-preprocessor"

When('I am in Dashboard', () => {
   cy.navigateToDashboard()

   cy.intercept("GET", "**/api/template**", { fixture: "templates.json" }).as("getTemplates")
})

When('I click on template menu', () => {
   cy.clickOnSidebarMenu("From template")
})

Then("I should view all template with pagination", () => {
   cy.get("div[class*=card]").first().contains("Template 1").should("exist")
})