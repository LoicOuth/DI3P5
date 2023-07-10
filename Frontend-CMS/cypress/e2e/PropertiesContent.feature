Feature: Properties Content

Background: 
   When I am in Dashboard
   Given Text element is selected

   Scenario: Success: Edit text alignment
      When I change the text alignment in center
      Then I Should view a text in center

   Scenario: Success: Edit content
      When I Change the content
      Then I Should view a new content

