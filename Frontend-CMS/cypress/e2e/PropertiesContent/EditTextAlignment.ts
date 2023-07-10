import { Then, When } from "@badeball/cypress-cucumber-preprocessor"
import { StyleProperty } from "../../../src/app/core/enums/StyleProperty.enum"
import { TEXT_TO_SELECT } from "."

When("I change the text alignment in center", () => {
   cy.get('mat-button-toggle[mattooltip="Align center"]').first().click()

   cy.mockStyleChange(TEXT_TO_SELECT, StyleProperty.TextAlign, "text-center")
})

Then("I Should view a text in center", () => {
   cy.get(`h1[id="${TEXT_TO_SELECT}"]`).should("have.class", "text-center")
   cy.matchImage()
})