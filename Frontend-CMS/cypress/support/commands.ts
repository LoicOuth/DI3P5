import 'cypress-signalr-mock'
import "@frsource/cypress-plugin-visual-regression-diff"
import { getElementById } from './../../src/app/utils/ElementUtils'
import { IElement } from './../../src/app/core/interfaces/IElement.interface'
import { TypeElement } from './../../src/app/core/enums/TypeElement.enum'
import { StyleProperty } from '../../src/app/core/enums/StyleProperty.enum'
import { UPDATE_ELEMENT, UPDATE_ELEMENT_STYLE } from '../../src/app/core/constants/events.constant'

/// <reference types="cypress" />
declare global {
   namespace Cypress {
      interface Chainable {
         navigateToDashboard(): Chainable<void>,
         clickOnSidebarMenu(name: string): Chainable<void>,
         dragAndDrop(draggable: HTMLElement, droppable: HTMLElement): Chainable<void>,
         openProperties(id: string): Chainable<void>,
         mockStyleChange(elementId: string, property: StyleProperty, value: string)
      }
   }
}

export const PAGEID = "973bf5eb-2ada-47e8-8afd-aac0ef9f131d"
export const SITEID = "a36b5402-9f51-4f62-9a1e-d45d19902ebe"

Cypress.Commands.add("navigateToDashboard", () => {
   cy.intercept('GET', `**/api/page?siteId=${SITEID}`, { fixture: 'pages.json' }).as('getPages')
   cy.intercept('GET', `**/api/site/${SITEID}`, { fixture: 'site.json' }).as('getSite')
   cy.intercept('GET', `**/api/element?pageId=${PAGEID}`, { fixture: 'elements.json' }).as('getElements')

   cy.intercept('GET', `**/api/menu?siteId=${SITEID}`, { fixture: 'menu.json' }).as('getMenu')

   cy.visit(`http://localhost:4200/site/${SITEID}/edit`)

   cy.wait(["@getPages", "@getSite", "@getElements", "@getMenu"])

   cy.get("img").first().invoke("attr", "src").should('eq', "../../assets/images/usite-logo-only-red.svg")
})

Cypress.Commands.add("clickOnSidebarMenu", (name: string) => {
   cy.get("h4").contains(name).first().parent().click()
})

Cypress.Commands.add("dragAndDrop", (draggable: HTMLElement, droppable: HTMLElement) => {

   const coords = droppable.getBoundingClientRect()
   draggable.dispatchEvent(new MouseEvent('mousedown'))
   draggable.dispatchEvent(new MouseEvent('mousemove', { clientX: 10, clientY: 0 }))
   draggable.dispatchEvent(new MouseEvent('mousemove', {
      clientX: coords.x + 10,
      clientY: coords.y + 10  // A few extra pixels to get the ordering right
   }))
   draggable.dispatchEvent(new MouseEvent('mouseup'))
})

Cypress.Commands.add("openProperties", (id: string) => {
   cy.get(`#${id}`).then(el => {
      cy.window().its('store').invoke("dispatch", { htmlElement: el[0], type: "[Element] Set Selected Element" })

      cy.wait(1000)

      cy.get('mat-drawer[position="end"]').should("have.css", "visibility", "visible")
   })
})

Cypress.Commands.add("mockStyleChange", (elementId: string, property: StyleProperty, value: string) => {
   let element: IElement

   cy.fixture("elements").then(el => {
      element = getElementById(el, elementId)
   })

   cy.hubVerifyInvokes("elements", UPDATE_ELEMENT_STYLE, (_) => {
      element.styles.push({
         property,
         value
      })
      cy.hubPublish("elements", UPDATE_ELEMENT, element)
   })
})

export const getElement = (type: TypeElement, id: string, parentId: string | null = null, content: string | null = null, alt: string | null = null, src: string | null = null): IElement => {
   const element = {
      id: id,
      type: type,
      name: content ? null : "Block 2",
      description: content ? null : "desc du block 2",
      pageId: parentId ? null : PAGEID,
      parentId: parentId,
      content: content,
      position: 0,
      menuId: null,
      url: src,
      alt: alt,
      styles: [],
      elementsChilds: []
   }

   return element
}