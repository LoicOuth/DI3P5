---
layout: Part
author: "Loïc OUTHIER"
---

# Git flow

[[toc]]

## Branche 

Branch name contains the type of branch (features / fix / refactor / chore)
And need the functionality name
### Example : 
features/addUser
fix/bugAddUser
etc...

## Commit description

Commit must start with the type of branch (feat / fix / refact / chore)
And need the description of functionality with all steps

### Example :
feat: add user command and frontend interface
etc...

## Pull request

On dev and on test branch : Need minimum 2 reviewers except the pull request creator
On main branch : Need minimum 3 reviewers except the pull request creator

Build pipeline need to be complete successfully

All work items need to be link to the pull request