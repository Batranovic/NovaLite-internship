import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import {GetAllQuestionsResponse, QuestionPageCountResponse, QuestionsClient} from '../../../api/api-reference';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-questions-overview',
  templateUrl: './questions-overview.component.html',
  styleUrls: ['./questions-overview.component.css'],
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule],
})
export class QuestionsOverviewComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['id', 'text', 'category'];
  dataSource = new MatTableDataSource<GetAllQuestionsResponse>();
  pageNum: number = 1;
  pageSize: number = 5;
  pageCount: number = 100;
  loading = false;
  errorMessage: string | null = null;

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  constructor(private questionService: QuestionsClient) {}

  ngAfterViewInit() {
    if (this.paginator) {
``
      this.paginator.page.subscribe(() => {
        if (!this.paginator) {
          return;
        }
        this.pageNum = this.paginator.pageIndex + 1;
        this.pageSize = this.paginator.pageSize;
        this.fetchQuestions();
        this.fetchPageCount();
      });
    }
  }

  ngOnInit() {
    this.fetchPageCount();
    this.fetchQuestions();
  }

  fetchQuestions() {
    this.loading = true;
    this.errorMessage = null;

    this.questionService.paginate(this.pageNum, this.pageSize).subscribe({
      next: (data) => {
        this.dataSource.data = data;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error fetching data. Please try again.';
        this.loading = false;
        console.error('Error fetching data:', error);
      },
    });
  }

  private fetchPageCount() {
    this.questionService.getPageCount(this.pageSize).subscribe({
      next: (data : QuestionPageCountResponse) => {
        if (!data.pageCount){
          return;
        }
        this.pageCount = data.pageCount;
        if (this.paginator) {
          this.paginator.length = this.pageCount * this.pageSize;
        }
      },
      error: (error) => {
        console.error('Error fetching page count:', error);
      },
    });
  }
}
