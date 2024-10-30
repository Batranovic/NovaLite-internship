import { Component } from '@angular/core';
import { ExamClient, GetExamResponse, SubmitExamCommand, SubmitExamExamQuestionDto } from '../../../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { SubmitDialogComponent } from '../submit-dialog/submit-dialog.component';
import { filter } from 'rxjs';

@Component({
  selector: 'app-exam-overview',
  templateUrl: './exam-overview.component.html',
  styleUrls: ['./exam-overview.component.css']
})
export class ExamOverviewComponent {
  exam!: GetExamResponse;
  currentQuestionIndex = 0;
  isTimerExpired = false;

  constructor(private activatedRoute: ActivatedRoute, private examClient: ExamClient, private snackBar: MatSnackBar, private router: Router, private dialog: MatDialog) {
    const examId = this.activatedRoute.snapshot.paramMap.get('id');
    if (examId) {
      this.examClient.getById(+examId).subscribe(response => this.exam = response);
    }
  }

  get isFirstQuestion(): boolean {
    return this.currentQuestionIndex === 0;
  }

  get isLastQuestion(): boolean {
    return !!this.exam?.questions && this.currentQuestionIndex === (this.exam?.questions?.length ?? 0) - 1;
  }

  get currentQuestion() {
    return this.exam?.questions?.[this.currentQuestionIndex];
  }

  nextQuestion() {
    if (!this.isLastQuestion) {
      this.currentQuestionIndex++;
    }
  }

  previousQuestion() {
    if (!this.isFirstQuestion) {
      this.currentQuestionIndex--;
    }
  }

  onTimerExpired() {
    this.isTimerExpired = true;
    this.submitExam();
  }

  async submitExam() {
    const dialogRef = this.dialog.open(SubmitDialogComponent, {
      data: { disableCancel: this.isTimerExpired }
    });
    dialogRef.afterClosed()
      .pipe(filter(result => result))
      .subscribe(() => {
        const command = new SubmitExamCommand({
          examId: this.exam.id,
          examQuestions: this.exam.questions?.map(question => new SubmitExamExamQuestionDto({
            examQuestionId: question.id,
            submittedAnswers: question.answers?.filter(answer => answer.isSelected).map(answer => answer.id!)
          }))
        });

        this.examClient.submitExam(command).subscribe({
          next: _ => {
            this.snackBar.open('Exam submitted successfully', 'Ok', {
              duration: 3000,
            });
            this.router.navigate(['/']);
          }
        });
      });
  }

}
