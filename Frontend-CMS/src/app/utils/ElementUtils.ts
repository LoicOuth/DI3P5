import { IElement } from "../core/interfaces/IElement.interface"

export const updateElement = (elements: Array<IElement>, searchElement: IElement): Array<IElement> => {
  return elements.map((currentElement) => {
    if (currentElement.id === searchElement.id) {
      return searchElement
    }

    const hasChildElements = currentElement.elementsChilds.length > 0
    const updatedChildElements = hasChildElements
      ? updateElement(currentElement.elementsChilds, searchElement)
      : currentElement.elementsChilds

    return {
      ...currentElement,
      elementsChilds: updatedChildElements,
    }
  })
}

export const newElement = (elements: Array<IElement>, newerElement: IElement): Array<IElement> => {

  if (newerElement.pageId) {
    return [...elements, newerElement]
  }

  return elements.map((currentElement) => {
    const hasMatchingParentId = currentElement.id === newerElement.parentId
    const shouldUpdateChildElements = currentElement.elementsChilds.length > 0

    const updatedChildElements = shouldUpdateChildElements
      ? newElement(currentElement.elementsChilds, newerElement)
      : currentElement.elementsChilds

    return hasMatchingParentId
      ? {
        ...currentElement,
        elementsChilds: [...updatedChildElements, newerElement],
      }
      : {
        ...currentElement,
        elementsChilds: updatedChildElements,
      }
  })
}

export const removeElement = (elements: Array<IElement>, elementIdToRemove: string): Array<IElement> => {
  return elements
    .filter((element) => element.id !== elementIdToRemove)
    .map((currentElement) => {
      const shouldUpdateChildElements = currentElement.elementsChilds.length > 0

      const updatedChildElements = shouldUpdateChildElements
        ? removeElement(currentElement.elementsChilds, elementIdToRemove)
        : currentElement.elementsChilds

      return {
        ...currentElement,
        elementsChilds: updatedChildElements,
      }
    })
}

export const getElementById = (elements: Array<IElement>, id: string): IElement | null => {
  const foundElement = elements.find((currentElement) => currentElement.id === id)

  if (foundElement) return foundElement

  for (const currentElement of elements) {
    if (currentElement.elementsChilds.length > 0) {
      const foundChildElement = getElementById(currentElement.elementsChilds, id)

      if (foundChildElement) return foundChildElement
    }
  }

  return null // L'élément n'a pas été trouvé dans le tableau
}