Feature: Dashboard

Background: 
   When I am in Dashboard

   Scenario: Get first page name
      Then I should view the current page name

   Scenario: Get first block
      When I click on Add Block button
      Then I should view a list of block in my page

   Scenario: Get first text
      Then I should view a text with test E2E