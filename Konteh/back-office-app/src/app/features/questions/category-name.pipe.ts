import { Pipe, PipeTransform } from '@angular/core';
import {QuestionCategory} from '../../api/api-reference';

@Pipe({
  name: 'categoryName'
})
export class CategoryNamePipe implements PipeTransform {
  transform(value: QuestionCategory): string {
    return QuestionCategory[value];
  }
}
