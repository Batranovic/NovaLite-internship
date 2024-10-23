import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { PageEvent } from '@angular/material/paginator';
import { QuestionsClient } from '../../../api/api-reference';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../layout/confirm-dialog/confirm-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { filter } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-questions-overview',
  templateUrl: './questions-overview.component.html',
  styleUrls: ['./questions-overview.component.css']
})
export class QuestionsOverviewComponent implements OnInit {
  displayedColumns: string[] = ['id', 'text', 'category', 'actions'];
  dataSource = new MatTableDataSource();
  pageNumber: number = 0;
  pageSize: number = 5;
  itemCount: number = 5;
  private filteredText: string | null = "";

  constructor(private snackBar: MatSnackBar, private questionService: QuestionsClient, public dialog: MatDialog, private router: Router) { }

  ngOnInit() {
    this.fetchQuestions();
  }

  fetchQuestions() {
    this.questionService.paginate(this.filteredText, this.pageNumber, this.pageSize).subscribe(data => {
      this.dataSource.data = data.items ?? [];
      this.itemCount = data.pageCount ?? 0;
    });
  }

  editQuestion(id: number): void {
    this.router.navigate(['/edit-question', id])
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

  onFilterChanged(filteredText: string) {
    this.filteredText = filteredText;
    this.resetPaginator();
  }

  resetPaginator = () => {
    this.pageNumber = 0;
    this.itemCount = 0;
    this.fetchQuestions();
  }

  onPageChange = (_event: PageEvent) => {
    this.pageNumber = _event.pageIndex;
    this.pageSize = _event.pageSize;
    this.fetchQuestions();
  };

  openConfirmDialog(actionToConfirm: Function): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed()
      .pipe(filter(result => result === true))
      .subscribe(_ => {
        actionToConfirm();
      });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
      horizontalPosition: 'center',
    });
  }

}
