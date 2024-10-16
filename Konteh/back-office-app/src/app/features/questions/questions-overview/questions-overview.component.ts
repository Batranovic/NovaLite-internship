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

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  private filteredText: string = "";

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
    this.questionService.paginate(this.pageNum, this.pageSize, this.filteredText).subscribe({
      next: (data) => {
        this.dataSource.data = data;
        this.pageCount = data[0].pageCount;
      },
      error: (error) => {
        console.error('Error fetching data: Error fetching data. Please try again.');
      },
    });
  }

  editQuestion(question : PaginateQuestionsResponse) {
    //TO DO: redirect to edit page
  }

  deleteQuestion(id : number) {
    //TO DO: call api for deletion of the item and call this.resetPaginator();
  }

  onFilterChanged(filterData: { text: string; category: QuestionCategory | null } | any) {
    this.filteredText = filterData.text;
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
