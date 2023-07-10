---
layout: Part
author: "Alexandre CZIPACK"
---


# Integrations Tests

## Delete Element

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidElement            | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDeleteElement                  | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## UpdateELement

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## GetElementWithPageId

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldReturnAllPagesAndElements      | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## CreateSubDomain

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldExistingSite                   | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldUpdateSiteAndCreateSubdomain   | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## GetSubdomainAvailability

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldReturnUnavailable              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldReturnAvailable                | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## CreatePage

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldHaveAName                      | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldCreatePage                     | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## UpdatePage

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidPageId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldUpdatePage                     | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldSetRandomPageAsFirstIfIsFirstSetToFalse | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldNotSetRandomPageAsFirstIfOnlyOnePageExists | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## GetPageWithSiteId

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldReturnAllPages                 | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## CreateSite

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireMinimumFields           | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldRequireUniqueName              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldCreateSite                     | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## DeleteSite

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDeleteSite                     | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## UpdateSite

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldRequireUniqueTitle             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldUpdateSite                     | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## GetLastDeployment

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## GetSiteFromSiteId

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldRequireValidSiteId             | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldReturnSite                     | <span style="color: white; background-color:green">Pass</span>  | Ok        |

## GetSites

| Test name                            | Result                                           | Comment                  |
|--------------------------------------|--------------------------------------------------|--------------------------|
| ShouldReturnList                     | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldDenyAnonymousUser              | <span style="color: white; background-color:green">Pass</span>  | Ok        |
| ShouldReturnAllSites                 | <span style="color: white; background-color:green">Pass</span>  | Ok        |