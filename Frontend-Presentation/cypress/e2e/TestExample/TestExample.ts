import { Then, When } from "@badeball/cypress-cucumber-preprocessor"

When("I go to google page", () => {
    cy.visit("https://google.com")
})

Then("Url should contain google.com", () => {
    cy.url().should("include", "google.com")
})
