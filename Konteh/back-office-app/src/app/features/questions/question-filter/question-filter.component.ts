import {Component, EventEmitter, Output} from '@angular/core';
import {QuestionCategory} from '../../../api/api-reference';

@Component({
  selector: 'app-question-filter',
  templateUrl: './question-filter.component.html',
  styleUrl: './question-filter.component.css'
})
export class QuestionFilterComponent {
  @Output() filterChange = new EventEmitter<{ text: string | null; category: QuestionCategory | null }>();

  categories = [
    { value: null, viewValue: 'All Categories' },
    { value: QuestionCategory.OOP, viewValue: 'OOP' },
    { value: QuestionCategory.General, viewValue: 'General' },
    { value: QuestionCategory.Git, viewValue: 'Git' },
    { value: QuestionCategory.Testing, viewValue: 'Testing' },
    { value: QuestionCategory.Sql, viewValue: 'SQL' },
    { value: QuestionCategory.Csharp, viewValue: 'C#' },
  ];

  selectedCategory: QuestionCategory | null = null;
  questionText: string | null = '';

  onFilterChange() {
    if(this.questionText && this.questionText.trim() === "") {
      this.questionText = null;
    }
    this.filterChange.emit({ text: this.questionText, category: this.selectedCategory });
  }
}
