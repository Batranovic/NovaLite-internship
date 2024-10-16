import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {
  PaginateQuestionsResponse,
  QuestionCategory,
  QuestionsClient
} from '../../../api/api-reference';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import {MatButton} from '@angular/material/button';
import {QuestionsModule} from '../questions.module';
import {MatLabel} from '@angular/material/form-field';

@Component({
  selector: 'app-questions-overview',
  templateUrl: './questions-overview.component.html',
  styleUrls: ['./questions-overview.component.css']
})
export class QuestionsOverviewComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['id', 'text', 'category', 'actions'];
  dataSource = new MatTableDataSource<PaginateQuestionsResponse>();
  pageNum: number = 1;
  pageSize: number = 5;
  pageCount: number | undefined = 100;
  loading = false;
  errorMessage: string | null = null;

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  private filteredText: string = "";
  private filteredCategory: QuestionCategory | null = null;

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
      });
    }
  }

  ngOnInit() {
    this.fetchQuestions();
  }

  fetchQuestions() {
    this.loading = true;
    this.errorMessage = null;

    this.questionService.paginate(this.pageNum, this.pageSize, this.filteredText).subscribe({
      next: (data) => {
        this.dataSource.data = data;
        this.loading = false;
        this.pageCount = data[0].pageCount;
      },
      error: (error) => {
        this.errorMessage = 'Error fetching data. Please try again.';
        this.loading = false;
        console.error('Error fetching data:', error);
      },
    });
  }

  editQuestion(question : PaginateQuestionsResponse) {

  }

  deleteQuestion(id : number) {

  }

  onFilterChanged(filterData: { text: string; category: QuestionCategory | null } | any) {
    this.filteredText = filterData.text;
    this.filteredCategory = filterData.category;
    this.resetPaginator();
  }

  resetPaginator = () =>{
    if (this.paginator) {
      this.paginator.pageIndex = 0;
      this.pageNum = 1;
      if (this.dataSource.data.length === 0){
        this.paginator.length = this.pageSize;
      }
    }
    this.fetchQuestions();
  }
}
