import { Component, Input } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { GetExamResponse } from '../../../api/api-reference';

@Component({
  selector: 'app-submit-dialog',
  templateUrl: './submit-dialog.component.html',
  styleUrl: './submit-dialog.component.css'
})
export class SubmitDialogComponent {
  @Input() exam! : GetExamResponse;
  constructor(private dialogRef: MatDialogRef<SubmitDialogComponent>) { }
  examEndTime = localStorage.getItem('examEndTime')

  get hasTimePassed(): boolean {
    const currentTime = this.exam.startTime?.getTime()
    if (parseInt(this.examEndTime!, 10) <= currentTime!) {
      return true;
    } else {
      return false;
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }
}
