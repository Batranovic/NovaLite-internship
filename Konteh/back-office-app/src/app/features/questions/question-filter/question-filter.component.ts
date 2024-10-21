import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-question-filter',
  templateUrl: './question-filter.component.html',
  styleUrls: ['./question-filter.component.css']
})
export class QuestionFilterComponent implements OnInit {
  @Output() filterChange = new EventEmitter<{ text: string | null }>();

  filterForm: FormGroup;

  constructor() {
    this.filterForm = new FormGroup({
      questionText: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.initFilterDebouncing();
  }

  private initFilterDebouncing(): void {
    this.filterForm.get('questionText')!.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged()
      )
      .subscribe((filterText: string) => {
        this.filterChange.emit({ text: filterText.trim() === '' ? null : filterText });
      });
  }

}
