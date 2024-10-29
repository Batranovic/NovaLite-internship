import { Component, OnInit } from '@angular/core';
import { ExamsClient, GetAllExamsResponse } from '../../../api/api-reference';
import { MatTableDataSource } from '@angular/material/table';
import { NotificationsService } from '../../../shared/notifications.service';

@Component({
  selector: 'app-exams-overview',
  templateUrl: './exams-overview.component.html',
  styleUrls: ['./exams-overview.component.css']
})
export class ExamsOverviewComponent implements OnInit {
  displayedColumns: string[] = ['candidate', 'status', 'score'];
  dataSource = new MatTableDataSource<GetAllExamsResponse>();
  searchText: string | null = "";

  constructor(private examsClient: ExamsClient, private notificationsService: NotificationsService) { }

  ngOnInit() {
    this.fetchExams();
    this.notificationsService.messageReceived.subscribe((message: GetAllExamsResponse) => {
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

  private updateExamInDataSource(newExam: GetAllExamsResponse) {
    let flag = true;
    for (let exam of this.dataSource.data) {
      if (exam.id === newExam.id){
        exam = newExam;
        flag = false;
      }
    }
    if (flag){
      this.dataSource.data.push(newExam);
    }
    this.dataSource.data = this.dataSource.data;
  }
}
