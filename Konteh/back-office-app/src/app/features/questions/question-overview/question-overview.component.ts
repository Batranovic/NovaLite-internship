import { Component } from '@angular/core';
import { GetQuestionByIdResponse, QuestionCategory, QuestionsClient, QuestionType } from '../../../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-question-overview',
  templateUrl: './question-overview.component.html',
  styleUrl: './question-overview.component.css'
})
export class QuestionOverviewComponent {
  questionId!: number;
  question: GetQuestionByIdResponse | null = null;

  constructor(private route: ActivatedRoute, private questionsClient: QuestionsClient, private router: Router){}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.questionId =+ params['id'];
      this.getQuestionById(this.questionId);
    });
  }

  getQuestionById(id: number): void {
    this.questionsClient.getQuestionById(id).subscribe({
      next: (response) => {
        this.question = response;
        console.log(this.question);
      },
      error: (err) => {
        console.error('Error fetching question', err);
      }
    });
  }

  getCategoryText(categoryId: any): string {
    return QuestionCategory[categoryId] || 'Unknown Category';
  }
  
  getTypeText(typeId: any): string {
    return QuestionType[typeId] || 'Unknown Type';
  }
  
  editQuestion(): void {
    this.router.navigate(['/edit-question', this.question?.id])
  }
}