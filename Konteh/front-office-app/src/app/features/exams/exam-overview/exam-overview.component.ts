import { Component } from '@angular/core';
import { ExamClient, ExecuteExamAnswerDto, ExecuteExamCommand, ExecuteExamExamQuestionDto, GenerateExamResponse,  QuestionType } from '../../../api/api-reference';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatRadioChange } from '@angular/material/radio';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { AnswerService } from './answet.service';

@Component({
  selector: 'app-exam-overview',
  templateUrl: './exam-overview.component.html',
  styleUrl: './exam-overview.component.css'
})
export class ExamOverviewComponent {
  QuestionType = QuestionType
  examResponse: GenerateExamResponse = new GenerateExamResponse(); 
  currentQuestionIndex = 0
  selectedAnswers : { [questionId: number]: number[]} = {}

  constructor(private router: Router, private examClient: ExamClient, private snackBar: MatSnackBar,  private answerService: AnswerService) {
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
    const isChecked = 'checked' in event ? event.checked : true;

    if (isChecked) {
      this.answerService.addAnswer(questionId, answerId);
    } else {
      this.answerService.removeAnswer(questionId, answerId);
    }
  }

  isAnswerSelected(answerId: number, questionId: number): boolean {
    return this.answerService.getAnswers(questionId).includes(answerId);
  }
  
 async submitExam() {
    const examQuestions = this.examResponse.examQuestions?.map(question => {
      const submittedAnswers = this.answerService.getAnswers(question.id!).map(answerId => 
        new ExecuteExamAnswerDto({ answerId })
      ) || [];
      
      return new ExecuteExamExamQuestionDto({
        examQuestionId: question.id,
        submittedAnswers
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
