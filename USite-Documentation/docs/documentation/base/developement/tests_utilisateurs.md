---
layout: Part
author: "Loïc OUTHIER"
---

# Tests utilisateurs

---

| Testeur              | Date       | Comment     |
|----------------------|------------|-------------|
| Loïc & Alexandre    | 14/06/2023 | Test on dev environment|

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|----------------------------------------------------|--------------------------|
| Register user with local account     | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Login with local account             | <span style="color: white; background-color:green">Pass</span>  | OK                       |
| Login just after email confirm       | <span style="color: white; background-color:red">Failed</span>   | Not redirect after emailconfirm `#757`|
| Logout                               | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Update username                      | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Update Password                      | <span style="color: white; background-color:red">Failed</span>   | Password update don't work `#758`|
| Create a new site                    | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Update site informations             | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Redirect on CMS                      | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new block to site              | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new text to site               | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new button to site             | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new image to site              | <span style="color: white; background-color:red">Failed</span>   | AzureStorage env var not set `#760`|
| Update detail page                   | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add new menu to site                 | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add new link to menu                 | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add new page to site                 | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Change flex direction of a block     | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Change content of a text / button    | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Change margin/padding of an element   | <span style="color: white; background-color:green">Pass</span> | Ok                       |
| Change text decoration of a text / button   | <span style="color: white; background-color:green">Pass</span>  | Ok                |
| Change text alignement of a text / button   | <span style="color: white; background-color:green">Pass</span>  | Ok                | 
| Change font size of a text / button   | <span style="color: white; background-color:green">Pass</span>  | Ok                      |
| Change width of an element   | <span style="color: white; background-color:green">Pass</span>           | Ok                      |
| Change shadow of an element   | <span style="color: white; background-color:green">Pass</span>          | Ok                      |
| Change border width of an element   | <span style="color: white; background-color:red">Failed</span>    | Border style don't appear on page `#759`|
| Change border color of an element   | <span style="color: white; background-color:red">Failed</span>    | Border style don't appear on page `#759`|
| Import new template in app          | <span style="color: white; background-color:green">Pass</span>    | Ok                      |
| Get all templates in CMS            | <span style="color: white; background-color:red">Failed</span>    | Get an unauthorized `#761`    |
| Delete an element                   | <span style="color: white; background-color:red">Failed</span>    | AzureStorage env var not set `#760`|
| Get a preview of a page             | <span style="color: white; background-color:green">Failed</span>  | OK                      |




---



| Testeur              | Date       | Comment     |
|----------------------|------------|-------------|
| Ruhiau    | 30/06/2023 | Test on dev environment|

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|----------------------------------------------------|--------------------------|
| Register user with local account     | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Login with local account             | <span style="color: white; background-color:green">Pass</span>  | OK                       |
| Login just after email confirm       | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Logout                               | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Update username                      | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Update Password                      | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Create a new site                    | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Update site informations             | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Redirect on CMS                      | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new block to site              | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new text to site               | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new button to site             | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add a new image to site              | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Update detail page                   | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add new menu to site                 | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add new link to menu                 | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Add new page to site                 | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Change flex direction of a block     | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Change content of a text / button    | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Change margin/padding of an element   | <span style="color: white; background-color:green">Pass</span> | Ok                       |
| Change text decoration of a text / button   | <span style="color: white; background-color:green">Pass</span>  | Ok                |
| Change text alignement of a text / button   | <span style="color: white; background-color:green">Pass</span>  | Ok                | 
| Change font size of a text / button   | <span style="color: white; background-color:green">Pass</span>  | Ok                      |
| Change width of an element   | <span style="color: white; background-color:green">Pass</span>           | Ok                      |
| Change shadow of an element   | <span style="color: white; background-color:green">Pass</span>          | Ok                      |
| Change border width of an element   | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Change border color of an element   | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Import new template in app          | <span style="color: white; background-color:green">Pass</span>    | Ok                      |
| Get all templates in CMS            | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Delete an element                   | <span style="color: white; background-color:green">Pass</span>  | Ok                       |
| Get a preview of a page             | <span style="color: white; background-color:red">Failed</span>  | in preview website, menu is not at the right place #776 |




---
