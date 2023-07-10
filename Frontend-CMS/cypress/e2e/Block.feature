Feature: Block

Background: 
   When I am in Dashboard

   Scenario: Success: Add a block
      When I click on Add Block button
      When I click on add button
      Then A dialog appear with form
      When I fill all fields
      When I click on create button
      Then A new block appear under last block
      Then A new block appear in Add block menu
