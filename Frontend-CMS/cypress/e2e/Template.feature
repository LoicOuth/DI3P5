Feature: Template

Background: 
   When I am in Dashboard
   When I click on template menu
   Then I should view all template with pagination

   Scenario: Success: Add new template
      When I hover a template
      Then I wan't to see overlay
      When I click on add button
      Then I wan't to show a new block in my page