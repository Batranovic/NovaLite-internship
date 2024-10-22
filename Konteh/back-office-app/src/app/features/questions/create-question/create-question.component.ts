import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, UntypedFormGroup, Validators } from '@angular/forms';
import { CreateUpdateQuestionAnswerDto, CreateUpdateQuestionCommand, QuestionCategory, QuestionsClient, QuestionType } from '../../../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteAnswerDialogComponent } from '../delete-answer-dialog/delete-answer-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrl: './create-question.component.css'
})
export class CreateQuestionComponent implements OnInit {
  questionForm!: FormGroup;
  showAnswerForm: boolean = false;
  questionId: number | null = null;
  isSubmitted = false;
  questionCategories: number[] = [1, 2, 3, 4, 5, 6];
  questionTypes: number[] = [1, 2];

  constructor(private questionClient: QuestionsClient, private router: Router, private route: ActivatedRoute, private dialog: MatDialog, private snackBar: MatSnackBar, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.questionForm = new FormGroup({
      text: new FormControl('', Validators.required),
      category: new FormControl(QuestionCategory.Csharp, Validators.required),
      type: new FormControl(QuestionType.RadioButton, Validators.required),
      answers: new FormArray<UntypedFormGroup>([])
    });
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.questionId = +id;
        this.getQuestionById(this.questionId);
      }
    })
    this.answersArray.valueChanges.subscribe(() => {
      this.checkForMultipleCorrectAnswers();
    });
  }

  checkForMultipleCorrectAnswers() {
    const correctAnswersCount = this.answersArray.controls.filter(control => control.value.isCorrect && !control.value.isDeleted).length;
    if (correctAnswersCount > 1 && this.questionForm.get('type')?.value === 1) {
      this.questionForm.get('type')?.setErrors({ multipleCorrect: true });
    } else {
      this.questionForm.get('type')?.setErrors(null);
    }
    this.cdr.detectChanges();
  }
  

  openSnackBar(message: string, action: string, duration: number = 2000) {
    this.snackBar.open(message, action)
  }

  onSubmit() {
    this.isSubmitted = true;
    this.checkForMultipleCorrectAnswers();
    if (this.questionForm.valid) {
      const hasCorrectAnswer = this.answersArray.controls.some(control => control.value.isCorrect);
      if (!hasCorrectAnswer) {
        this.snackBar.open('At least one correct answer is required', 'Ok', { panelClass: 'red-snackbar' });
        return;
      }
      this.createOrUpdateQuestion();
    } else {
      return;
    }
  }

  private createOrUpdateQuestion() {
    const answersToSubmit: CreateUpdateQuestionAnswerDto[] = this.answersArray.controls.map(control => {
      return new CreateUpdateQuestionAnswerDto({
        id: control.value.id,
        text: control.value.text,
        isCorrect: control.value.isCorrect,
        isDeleted: control.value.isDeleted
      });
    });
    const command = new CreateUpdateQuestionCommand({
      id: this.questionId!,
      text: this.questionForm.value.text,
      category: this.questionForm.value.category,
      type: this.questionForm.value.type,
      answers: answersToSubmit
    });
    this.questionClient.createOrUpdateQuestion(command).subscribe({
      next: (response) => {
        this.router.navigate(['/questions-overview']);
      },
      error: (err) => {
        console.error('Error creating question', err);
      }
    });
  }

  getQuestionById(id: number): void {
    this.questionClient.getQuestionById(id).subscribe({
      next: (response) => {
        this.questionForm.patchValue({
          text: response.text,
          category: response.category,
          type: response.type
        });
        response.answers?.map(x => this.answersArray.push(new FormGroup({
          id: new FormControl(x.id),
          text: new FormControl(x.text, Validators.required),
          isCorrect: new FormControl(x.isCorrect),
          isDeleted: new FormControl(x.isDeleted)
        })));
      },
      error: (err) => {
        console.error('Error loading question', err);
      }
    });
  }
  
  onDeleteAnswer(index: number) {
    const confirmDialog = this.dialog.open(DeleteAnswerDialogComponent);
    confirmDialog.afterClosed().subscribe(result => {
      if (result) {
        const answerGroup = this.answersArray.at(index);
        answerGroup.patchValue({ isDeleted: true });
        this.cdr.detectChanges();
      }
    });
  }
  
  errorMessage = {
    multipleCorrect: 'You can only select one correct answer for RadioButton question type.',
  };

  getCategoryText(categoryId: any): string {
    return QuestionCategory[categoryId] || 'Unknown Category';
  }

  getTypeText(typeId: any): string {
    return QuestionType[typeId] || 'Unknown Type';
  }

  get answersArray(): FormArray<UntypedFormGroup> {
    return this.questionForm.controls['answers'] as FormArray<UntypedFormGroup>;
  }

  addAnswer(): void {
    const answer = new FormGroup({
      isCorrect: new FormControl(false),
      text: new FormControl('', Validators.required),
      isDeleted: new FormControl(false) 
    });
    this.answersArray.push(answer)
  }
}