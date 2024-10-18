import {Component, ViewChild, AfterViewInit, OnInit, inject} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import {
  QuestionCategory,
  QuestionsClient
} from '../../../api/api-reference';
import {MatDialog} from '@angular/material/dialog';
import {ConfirmDialogComponent} from '../../layout/confirm-dialog/confirm-dialog.component';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-questions-overview',
  templateUrl: './questions-overview.component.html',
  styleUrls: ['./questions-overview.component.css']
})
export class QuestionsOverviewComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['id', 'text', 'category', 'actions'];
  dataSource = new MatTableDataSource();
  pageNum: number = 1;
  pageSize: number = 5;
  itemCount: number = 5;
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  private filteredText: string | null = "";

  constructor(private _snackBar : MatSnackBar, private questionService: QuestionsClient, public dialog: MatDialog) {
  }

  ngOnInit() {
    this.fetchQuestions();
  }

  ngAfterViewInit() {
    if (this.paginator) {
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

  fetchQuestions() {
    this.questionService.paginate(this.pageNum, this.pageSize, this.filteredText).subscribe({
      next: (data) => {
        this.dataSource.data = data;
        if (data.length !== 0 && data[0].pageCount){
          this.itemCount = data[0].pageCount;
        }else{
          this.itemCount = this.pageSize;
        }
      },
      error: (error) => {
      },
    });
  }

  editQuestion(question : any) {
    // TO DO: redirect to edit page
  }

  deleteQuestion(id: number) {
    this.openConfirmDialog(() => {
      this.questionService.deleteById(id).subscribe({
        next: () => {
          this.resetPaginator();
          this.openSnackBar('Successfully deleted the question.', 'Close');
        },
        error: () => {
          this.openSnackBar('Unsuccessfully deleted the question.', 'Close');
        },
      });
    });
  }

  onFilterChanged(filterData: { text: string } | any) {
    this.filteredText = filterData.text;
    this.resetPaginator();
  }

  resetPaginator = () => {
    if (this.paginator) {
      this.paginator.pageIndex = 0;
      this.pageNum = 1;
      if (this.dataSource.data.length === 0) {
        this.paginator.length = this.pageSize;
      }
    }
    this.fetchQuestions();
  }

  openConfirmDialog(actionToConfirm: Function): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        actionToConfirm();
      }
    });
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
      horizontalPosition: 'center',
    });
  }
}
