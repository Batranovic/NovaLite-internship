import { Component } from '@angular/core';
import { ExamClient,ExecuteExamCommand,ExecuteExamExamQuestionDto,GenerateExamResponse,  QuestionType } from '../../../api/api-reference';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatRadioChange } from '@angular/material/radio';
import { MatCheckboxChange } from '@angular/material/checkbox';

@Component({
  selector: 'app-exam-overview',
  templateUrl: './exam-overview.component.html',
  styleUrl: './exam-overview.component.css'
})
export class ExamOverviewComponent {
  QuestionType = QuestionType
  examResponse: Partial<GenerateExamResponse> = {examQuestions: []}
  examQuestion : ExecuteExamExamQuestionDto | undefined 
  currentQuestionIndex = 0
  selectedAnswers : { [questionId: number]: number[]} = {}

  constructor(private router: Router, private examClient: ExamClient, private snackBar: MatSnackBar) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.examResponse = navigation.extras.state['examResponse'];
    }
  }

  get isFirstQuestion(): boolean {
    return this.currentQuestionIndex === 0;
  }

  get isLastQuestion(): boolean {
    return !!this.examResponse?.examQuestions && this.currentQuestionIndex === (this.examResponse?.examQuestions?.length ?? 0) - 1;
  }

  get currentQuestion() {
    return this.examResponse?.examQuestions?.[this.currentQuestionIndex];
  }
  nextQuestion() {
    if (!this.isLastQuestion){
      this.currentQuestionIndex++;
    }
  }

  previousQuestion(){
    if(!this.isFirstQuestion){
      this.currentQuestionIndex--;
    }
  }

  onAnswerChange(event: MatRadioChange | MatCheckboxChange, answerId: number, questionId: number) {
    let isChecked: boolean;

    if ('checked' in event) {
      isChecked = event.checked; 
    } else {
      isChecked = true; 
    }

    if (!this.selectedAnswers[questionId]) {
      this.selectedAnswers[questionId] = [];
    }

    if (isChecked) {
      this.addAnswer(questionId, answerId);
    } else {
      this.removeAnswer(questionId, answerId);
    }
  }
  
  addAnswer(questionId: number, answerId: number) {
    if (!this.selectedAnswers[questionId]) {
      this.selectedAnswers[questionId] = [];
    }
  
    if (!this.selectedAnswers[questionId].includes(answerId)) {
      this.selectedAnswers[questionId].push(answerId);
    }
  }
  
  removeAnswer(questionId: number, answerId: number) {
    if (this.selectedAnswers[questionId]) {
      this.selectedAnswers[questionId] = this.selectedAnswers[questionId].filter(id => id !== answerId);
    }
  }

  isAnswerSelected(answerId: number, questionId: number): boolean {
    return this.selectedAnswers[questionId]?.includes(answerId) || false;
  }
  
 submitExam() {
    const examQuestions = this.examResponse.examQuestions?.map(question => {
    //  const submittedAnswers = this.selectedAnswers[question.id!]?.map(answerId => 
        //new ExecuteSubmissionAnswerDto({ id: answerId })
    //  ) || [];

      return new ExecuteExamExamQuestionDto({
        examQuestionId: question.id,
      //  submittedAnswers
      });
    }) || [];

    const command = new ExecuteExamCommand({
      examId: this.examResponse.id,
      examQuestions
    });

    this.examClient.executeExam(command).subscribe({
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
