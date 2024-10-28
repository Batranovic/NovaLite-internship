import { Pipe, PipeTransform } from '@angular/core';
import {ExamStatus} from '../../api/api-reference';

@Pipe({
  name: 'examStatus'
})
export class ExamStatusPipe implements PipeTransform {
  transform(value: ExamStatus): string {
    return ExamStatus[value];
  }
}