---
layout: Part
author: "Alexandre CZIPACK"
---


# E2E Tests

## Feature: Block

### Scenario: Success: Add a block
- Background: When I am in Dashboard
- Test Steps:
    1. When I click on Add Block button
    2. When I click on add button
    3. Then A dialog appears with a form
    4. When I fill all fields
    5. When I click on the create button
    6. Then A new block appears under the last block
    7. Then A new block appears in the Add block menu
- Result: Pass


## Feature: Content

### Scenario: Success: Add text in a block
- Background: When I am in Dashboard and I click on the content menu
- Test Steps:
    1. When I drag and drop a text content
    2. Then I should view a new text element in my page
- Result: Pass


## Feature: Dashboard

### Scenario: Get first page name
- Background: When I am in Dashboard
- Test Steps:
    1. Then I should view the current page name
- Result: Pass

### Scenario: Get first block
- Background: When I am in Dashboard
- Test Steps:
    1. When I click on Add Block button
    2. Then I should view a list of blocks in my page
- Result: Pass

### Scenario: Get first text
- Background: When I am in Dashboard
- Test Steps:
    1. Then I should view a text with test E2EFeature: Properties Content
- Result: Pass

## Feature: Properties Content

### Scenario: Success: Edit text alignment
- Background: When I am in Dashboard and the Text element is selected
- Test Steps:
    1. When I change the text alignment to center
    2. Then I should view the text in the center
- Result: Pass

### Scenario: Success: Edit content
- Background: When I am in Dashboard and the Text element is selected
- Test Steps:
    1. When I change the content
    2. Then I should view the new content
- Result: Pass

## Feature: Template

### Scenario: Success: Add new template
- Background: When I am in Dashboard and I click on the template menu
- Test Steps:
    1. When I hover over a template
    2. Then I want to see an overlay
    3. When I click on the add button
    4. Then I want to see a new block in my page
- Result: Pass