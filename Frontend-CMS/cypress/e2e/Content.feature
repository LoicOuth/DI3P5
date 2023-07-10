Feature: Content

Background: 
   When I am in Dashboard
   When I click on content menu
   Then I should view expanded menu

   Scenario: Success: Add text in a block
      When I drag and drop a text content
      Then I should view a new text element in my page
   
   Scenario: Success: Add an image
      When I drag and drop a image content
      Then I Should view a new image element in my page

   Scenario: Success: Add a button
      When I drag and drop a button content
      Then I Should view a new button element in my page
 