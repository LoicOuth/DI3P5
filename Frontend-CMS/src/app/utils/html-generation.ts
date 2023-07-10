import { Renderer2, Injectable, ElementRef } from '@angular/core'
import { IElement } from '../core/interfaces/IElement.interface'
import { TypeElement } from '../core/enums/TypeElement.enum'
import { IStyle } from '../core/interfaces/IStyle.interface'
import { getColorFromClass, getValueInString } from './StyleUtils'
import { StyleProperty } from '../core/enums/StyleProperty.enum'

@Injectable()
export class HtmlGeneration {
  constructor(private renderer: Renderer2) { }

  generateHtml(
    element: IElement,
    pageContainer: ElementRef,
    parent: any = null,
    elementSelectedId: undefined | string = undefined,
    menuContainer: ElementRef | null = null,
    preview: boolean = false
  ): void {
    if (!parent && element.menuId && menuContainer) {
      this.generateMenusHtml(element, elementSelectedId, menuContainer)
    } else {
      let part = this.createElement(element.type)

      this.renderer.setProperty(part, 'id', element.id)

      element.styles.forEach((el) => {
        this.setStyleForElement(el, part)
      })

      if (elementSelectedId === element.id)
        this.renderer.addClass(part, 'selected')

      if (!preview)
        this.renderer.addClass(part, 'usite-element-item')

      if (element.content)
        this.renderer.setProperty(part, 'innerHTML', element.content.replace(/\n/g, "<br />"))

      if (element.type === TypeElement.Image) {
        this.renderer.setAttribute(part, 'src', element.url!)
        this.renderer.setAttribute(part, 'alt', element.alt!)
      }

      this.renderer.appendChild(
        parent != null ? parent : pageContainer.nativeElement,
        part
      )

      element.elementsChilds.forEach((el) =>
        this.generateHtml(
          el,
          pageContainer,
          part,
          elementSelectedId,
          menuContainer,
          preview
        )
      )
    }
  }

  private generateMenusHtml(
    element: IElement,
    elementSelectedId: undefined | string,
    menuContainer: ElementRef
  ): void {
    let part: any
    part = this.renderer.createElement('DIV')
    this.renderer.setProperty(part, 'id', element.id)

    this.renderer.addClass(part, 'usite-menu')

    element.styles.forEach((el) => {
      this.setStyleForElement(el, part)
    })

    if (elementSelectedId === element.id)
      this.renderer.addClass(part, 'selected')

    this.renderer.appendChild(menuContainer.nativeElement, part)

    element.elementsChilds.forEach((link) => {
      let partLink = this.renderer.createElement('A')
      this.renderer.setProperty(partLink, 'id', link.id)

      link.styles.forEach((el) => {
        this.setStyleForElement(el, partLink)
      })

      if (elementSelectedId === link.id)
        this.renderer.addClass(partLink, 'selected')

      if (link.content)
        this.renderer.setProperty(partLink, 'innerHTML', link.content)

      this.renderer.appendChild(
        part != null ? part : menuContainer.nativeElement,
        partLink
      )
    })
  }

  private createElement(type: TypeElement): HTMLElement {
    switch (type) {
      case TypeElement.Block:
        let part = this.renderer.createElement('DIV')
        this.renderer.addClass(part, 'usite-block')
        return part
      case TypeElement.H1:
        return this.renderer.createElement('H1')
      case TypeElement.Button:
        return this.renderer.createElement('BUTTON')
      case TypeElement.Image:
        return this.renderer.createElement('IMG')
      default:
        throw new Error(`Unsupported element type: ${type}`)
    }
  }

  private generateSpacingStyle(style: string, cla: string): string {
    const test = cla.replace(style, "")
    if (test.includes('t')) return `${style}-top`
    if (test.includes('b')) return `${style}-bottom`
    if (test.includes('l')) return `${style}-left`
    if (test.includes('r')) return `${style}-right`

    return style
  }

  private setStyleForElement(elementStyle: IStyle, partHtml: any) {
    const setStyle = (property: string, value: string, extra: string = "") => {
      if (/\s/.test(value)) {
        value.split(' ').forEach((x) => {
          this.renderer.setStyle(
            partHtml,
            this.generateSpacingStyle(property, x) + extra,
            `${getValueInString(x)}px`
          )
        })
      } else {
        this.renderer.setStyle(
          partHtml,
          property + extra,
          `${getValueInString(value)}px`
        )
      }
    }

    switch (elementStyle.property) {
      case StyleProperty.TextDecoration:
        elementStyle.value
          .split(' ')
          .forEach((value) => this.renderer.addClass(partHtml, value))
        break
      case StyleProperty.TextColor:
        this.renderer.setStyle(
          partHtml,
          'color',
          getColorFromClass(elementStyle.value)
        )
        break
      case StyleProperty.BackgroundColor:
        this.renderer.setStyle(
          partHtml,
          'background-color',
          getColorFromClass(elementStyle.value)
        )
        break
      case StyleProperty.BorderColor:
        this.renderer.setStyle(partHtml, 'border-color', getColorFromClass(elementStyle.value))
        break
      case StyleProperty.Border:
        setStyle('border', elementStyle.value, "-width")
        this.renderer.setStyle(partHtml, 'border-style', 'solid')
        break
      case StyleProperty.Padding:
        setStyle('padding', elementStyle.value)
        break
      case StyleProperty.Margin:
        setStyle('margin', elementStyle.value)
        break
      default:
        this.renderer.addClass(partHtml, elementStyle.value)
        break
    }
  }
}
