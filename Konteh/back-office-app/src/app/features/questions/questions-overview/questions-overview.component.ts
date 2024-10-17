import { Component, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {
  PaginateQuestionsResponse,
  QuestionCategory,
  QuestionsClient
} from '../../../api/api-reference';
import {MatDialog} from '@angular/material/dialog';
import {ConfirmDialogComponent} from '../../layout/confirm-dialog/confirm-dialog.component';

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

  constructor(private questionService: QuestionsClient, public dialog: MatDialog) {}

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
    this.openConfirmDialog(()=> {
      this.questionService.deleteById(id).subscribe({
        next: () => {
          this.resetPaginator();
        },
        error: () => {

        },
      });
    });
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

  openConfirmDialog(functionToBeDone: Function): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        functionToBeDone();
      } else {
        console.log('User canceled deletion');
      }
    });
  }
}
