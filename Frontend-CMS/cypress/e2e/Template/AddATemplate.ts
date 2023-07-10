import { Then, When } from "@badeball/cypress-cucumber-preprocessor"
import { NEW_ELEMENT } from "../../../src/app/core/constants/events.constant"
import { PAGEID } from "../../support/commands"

const NEW_TEMPLATE_ID = "6a27043e-8a7d-4368-a84a-9a3cb5975d9f"

When("I hover a template", () => {
   cy.get("div[class*=card]").first().trigger('mouseover')
})

Then("I wan't to see overlay", () => {
   cy.get("div[class*=card] > div[class*=overlay]").first().should("have.css", "opacity", "1")
})

When("I click on add button", () => {
   cy.intercept({ method: "POST", url: "**/api/template/**" }, {}).as("newTemplate")

   cy.get("button > span").contains("Add template to page").first().click({ force: true })

   cy.wait(["@newTemplate"])

   cy.fixture("templates").then(el => {
      let elements = el.items[0]
      elements.pageId = PAGEID
      elements.id = NEW_TEMPLATE_ID

      cy.hubPublish("elements", NEW_ELEMENT, elements)
   })
})

Then("I wan't to show a new block in my page", () => {
   cy.get(`div[id="${NEW_TEMPLATE_ID}"]`).should("exist")
   cy.matchImage()
})