import { Component } from '@angular/core';
import { GenerateExamResponse, QuestionType } from '../../../api/api-reference';
import { Router } from '@angular/router';


@Component({
  selector: 'app-exam-overview',
  templateUrl: './exam-overview.component.html',
  styleUrl: './exam-overview.component.css'
})
export class ExamOverviewComponent {
  QuestionType = QuestionType; 
  examResponse: Partial<GenerateExamResponse> = {examQuestions: []};
  currentQuestionIndex = 0;

  constructor(private router: Router) {
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
}
