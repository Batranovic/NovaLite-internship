import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'app-answer-form',
  templateUrl: './answer-form.component.html',
  styleUrl: './answer-form.component.css'
})
export class AnswerFormComponent {
  @Input() answerForm!: UntypedFormGroup;
  @Output() delete = new EventEmitter<void>();

  onAnswerDelete() {
    this.delete.emit();
  }
}
