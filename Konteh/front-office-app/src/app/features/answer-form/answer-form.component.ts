import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { AnswerService } from '../exams/exam-overview/answer.service';
import { GetExamAnswerItem, GetExamExamQuestionItem, GetExamResponse, QuestionType, } from '../../api/api-reference';

@Component({
  selector: 'app-answer-form',
  templateUrl: './answer-form.component.html',
  styleUrls: ['./answer-form.component.css']
})
export class AnswerFormComponent {
  @Input() question!: GetExamExamQuestionItem;
  @Input() exam!: GetExamResponse;
  @Input() isTimerExpired!: boolean;
  QuestionType = QuestionType;

  get selectedAnswer() {
    const selectedAnswers = this.question.answers?.filter(x => x.isSelected);
    return selectedAnswers && selectedAnswers.length > 0 ? selectedAnswers[0] : undefined;
  }

  onAnswerSelected = (answer: GetExamAnswerItem) => {
    const selectedAnswer = this.question.answers?.filter(x => x === answer)[0];
    selectedAnswer!.isSelected = true;
  }

  onAnswerChecked = (checked: boolean, answer: GetExamAnswerItem) => {
    const selectedAnswer = this.question.answers?.filter(x => x === answer)[0];
    selectedAnswer!.isSelected = checked;
  }

 
}
