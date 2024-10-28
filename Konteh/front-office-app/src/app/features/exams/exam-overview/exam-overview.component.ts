import { Component } from '@angular/core';
import { ExamClient, GetExamResponse, SubmitExamCommand, SubmitExamExamQuestionDto } from '../../../api/api-reference';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-exam-overview',
  templateUrl: './exam-overview.component.html',
  styleUrls: ['./exam-overview.component.css']
})
export class ExamOverviewComponent {
  exam!: GetExamResponse;
  currentQuestionIndex = 0;

  constructor(private activatedRoute: ActivatedRoute, private examClient: ExamClient, private snackBar: MatSnackBar, private router: Router) {
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

  async submitExam() {
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
      },
      error: (err) => {
        console.error('Error submitting exam', err);
      }
    });
  }
}
