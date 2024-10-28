import { Component, OnInit } from '@angular/core';
import { ExamsClient, GetAllExamsResponse } from '../../../api/api-reference';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-exams-overview',
  templateUrl: './exams-overview.component.html',
  styleUrls: ['./exams-overview.component.css']
})
export class ExamsOverviewComponent implements OnInit {
  displayedColumns: string[] = ['candidate', 'status', 'score'];
  dataSource = new MatTableDataSource<GetAllExamsResponse>();
  searchText: string | null = "";

  constructor(private examsClient: ExamsClient) { }

  ngOnInit() {
    this.fetchExams();
  }

  fetchExams() {
    this.examsClient.getAllExams(this.searchText).subscribe((response) => {
      this.dataSource.data = response ?? [];
    });
  }

  onSearchChanged(searchText: string) {
    this.searchText = searchText;
    this.fetchExams();
  }
}
