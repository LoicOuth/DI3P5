import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {

  transform(data: Object) : Array<string> {
    return Object.values(data).filter((value) => typeof value === "string");
  }
}
