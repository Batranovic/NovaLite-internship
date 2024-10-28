import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'app-exams-search',
  templateUrl: './exams-search.component.html',
  styleUrl: './exams-search.component.css'
})
export class ExamsSearchComponent implements OnInit{
  @Output() searchChange = new EventEmitter<string>();

  searchForm = new FormGroup({
    candidate: new FormControl(''),
  });

  ngOnInit(): void {
    this.initSearchDebouncing();
  }

  initSearchDebouncing(): void {
    this.searchForm.get('candidate')!.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged()
      )
      .subscribe((searchText: string | null) => {
        this.searchChange.emit(searchText?.trim());
      });
  }
}
