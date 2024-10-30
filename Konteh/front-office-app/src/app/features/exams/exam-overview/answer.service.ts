import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AnswerService {
  private selectedAnswers: { [questionId: number]: number[] } = {};

  getAnswers(questionId: number): number[] {
    return this.selectedAnswers[questionId] || [];
  }

  addAnswer(questionId: number, answerId: number): void {
    if (!this.selectedAnswers[questionId]) {
      this.selectedAnswers[questionId] = [];
    }
    if (!this.selectedAnswers[questionId].includes(answerId)) {
      this.selectedAnswers[questionId].push(answerId);
    }
  }

  removeAnswer(questionId: number, answerId: number): void {
    if (this.selectedAnswers[questionId]) {
      this.selectedAnswers[questionId] = this.selectedAnswers[questionId].filter(id => id !== answerId);
    }
  }
}
