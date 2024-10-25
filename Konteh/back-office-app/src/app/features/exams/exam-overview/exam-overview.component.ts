import { Component, OnInit } from '@angular/core';
import { ExamsClient, GetAllExamsResponse } from '../../../api/api-reference';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-exam-overview',
  templateUrl: './exam-overview.component.html',
  styleUrls: ['./exam-overview.component.css']
})
export class ExamOverviewComponent implements OnInit {
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
