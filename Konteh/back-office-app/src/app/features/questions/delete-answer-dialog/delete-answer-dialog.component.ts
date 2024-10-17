import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-answer-dialog',
  templateUrl: './delete-answer-dialog.component.html',
  styleUrl: './delete-answer-dialog.component.css'
})
export class DeleteAnswerDialogComponent {
  constructor(private dialog: MatDialogRef<DeleteAnswerDialogComponent>){}

  onCancel() {
    this.dialog.close(false);
  }

  onConfirm(){
    this.dialog.close(true);
  }
}
