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
  examResponse: GenerateExamResponse | undefined;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.examResponse = navigation.extras.state['examResponse'];
    }
  }
}
