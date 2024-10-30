import { Component, OnInit } from '@angular/core';
import { ExamsClient, GetExamResponse } from '../../../api/api-reference';
import { MatTableDataSource } from '@angular/material/table';
import { NotificationsService } from '../../../shared/notifications.service';

@Component({
  selector: 'app-exams-overview',
  templateUrl: './exams-overview.component.html',
  styleUrls: ['./exams-overview.component.css']
})
export class ExamsOverviewComponent implements OnInit {
  displayedColumns: string[] = ['candidate', 'status', 'score'];
  dataSource = new MatTableDataSource<GetExamResponse>();
  searchText: string | null = "";

  constructor(private examsClient: ExamsClient, private notificationsService: NotificationsService) { }

  ngOnInit() {
    this.fetchExams();
    this.notificationsService.messageReceived.subscribe((message: GetExamResponse) => {
      this.updateExamInDataSource(message);
    });
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

  private updateExamInDataSource(newExam: GetExamResponse) {
    const data = this.dataSource.data;
    const index = data.findIndex(exam => exam.id === newExam.id);

    if (index !== -1) {
      data[index] = newExam;
    } else {
      data.unshift(newExam);
    }
    this.dataSource.data = [...data];
  }
}
