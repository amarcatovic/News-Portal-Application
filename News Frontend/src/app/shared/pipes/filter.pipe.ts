import { News } from './../models/News';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, searchTerm: string): any {
    if(searchTerm === '' || value.length === 0){
      return value;
    }

    let result: any[] = [];
    for(let item of value){
      if(<string>(item['title']).toLowerCase().includes(searchTerm.toLowerCase())){
        result.push(item);
      }
    }

    return result;
  }

}
