import { IStyle } from './IStyle.interface';
import { TypeElement } from './../enums/TypeElement.enum';

export interface IElement {
  id: string;
  parentId?: string;
  pageId?: string;
  menuId?: string;
  content: string | null;
  type: TypeElement;
  position: number;
  name: string | null;
  description: string | null;
  styles: Array<IStyle>;
  elementsChilds: Array<IElement>;
  url: string | null;
  alt: string | null;
}
