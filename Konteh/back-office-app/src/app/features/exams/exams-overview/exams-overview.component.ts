import { Component, OnInit } from '@angular/core';
import { ExamsClient } from '../../../api/api-reference';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-exams-overview',
  templateUrl: './exams-overview.component.html',
  styleUrls: ['./exams-overview.component.css']
})
export class ExamsOverviewComponent implements OnInit {
  displayedColumns: string[] = ['id', 'candidate', 'status', 'score'];
  dataSource =  new MatTableDataSource(); 

  constructor(private examsClient: ExamsClient) {}

  ngOnInit() {
    this.fetchExams(); 
  }

  fetchExams() {
    this.examsClient.getAllExams().subscribe(response => {
      this.dataSource.data = response.items ?? []; 
    });
  }
}
