import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Answer, CreateQuestionCommand, QuestionsClient, UpdateQuestionCommand } from '../../../api/api-reference';
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
  answerForm!: FormGroup;
  showAnswerForm: boolean = false;
  answers: Answer[] = [];
  isEditMode: boolean = false;
  questionId: number | null = null;
  isAnswerEditMode: boolean = false;
  editingAnswerIndex: number | null = null;
  displayAnswers: Answer[] = [];
  isSubmitted = false;
  isAnswerSubmitted = false;
  questionCategories = [
    { value: 1, viewValue: 'OOP' },
    { value: 2, viewValue: 'General' },
    { value: 3, viewValue: 'Git' },
    { value: 4, viewValue: 'Testing' },
    { value: 5, viewValue: 'Sql' },
    { value: 6, viewValue: 'Csharp' },
  ];
  questionTypes = [
    { value: 1, viewValue: 'RadioButton' },
    { value: 2, viewValue: 'CheckBox' }
  ]

  constructor(private formBuilder: FormBuilder, private questionClient: QuestionsClient, private router: Router, private route: ActivatedRoute, private dialog: MatDialog, private snackBar: MatSnackBar){}

  ngOnInit() {
    this.questionForm = this.formBuilder.group({
      text:['', Validators.required],
      category: ['', Validators.required],
      type: ['', [Validators.required]],
    });
    this.answerForm = this.formBuilder.group({
      text: ['', Validators.required],
      isCorrect: [false, Validators.required],
      isDeleted: [false]
    });
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if(id){
        this.isEditMode = true;
        this.questionId = +id;
        this.getQuestionById(this.questionId);
      }
    })
  }

  openSnackBar(message: string, action: string) {
    this.snackBar,open(message, action)
  }

  onSubmit() {
    this.isSubmitted = true;
    const correctAnswersCount = this.answers.filter(answer => answer.isCorrect && !answer.isDeleted).length;
    if (correctAnswersCount > 1 && this.questionForm.value.type === 1) {
      this.questionForm.get('type')?.setErrors({ multipleCorrect: true }); 
      return;
    }
    console.log(this.questionForm.valid)
    if (this.questionForm.valid) {
      const hasCorrectAnswer = this.answers.some(answer => answer.isCorrect);
      if (!hasCorrectAnswer) {
        this.snackBar.open('At least one correct answer is required', 'Ok', {
          duration: 3000,
          panelClass: 'red-snackbar'
        });
        return;
      }
      if (this.isEditMode) {
        this.updateQuestion();
      } else {
        this.createQuestion();
      }
    } else {
     return;
    }
  }
  
  private createQuestion() {
    const command = new CreateQuestionCommand();
    command.text = this.questionForm.value.text;
    command.category = this.questionForm.value.category;
    command.questionType = this.questionForm.value.type;
    command.answers = this.answers;
    this.questionClient.create(command).subscribe({
      next: (response) => {
        this.snackBar.open('Question successfully created', 'Ok', {
          duration: 2000
        })
        this.router.navigate(['/question-overview/', response.id]);
      },
      error: (err) => {
        console.error('Error creating question', err);
      }
    });
  }
  
  private updateQuestion() {
    if (this.questionId === null) {
      console.error('Question ID is null. Cannot update the question.');
      return; 
    }
    const command = new UpdateQuestionCommand();
    command.id = this.questionId;
    command.text = this.questionForm.value.text;
    command.category = this.questionForm.value.category;
    command.type = this.questionForm.value.type;
    command.answers = this.answers;
    this.questionClient.updateQuestion(command).subscribe({
      next: () => {
        this.snackBar.open('Question successfully updated', 'Ok', {
          duration: 2000
        })
        this.router.navigate(['/question-overview/', this.questionId]);
      },
      error: (err) => {
        console.error('Error updating question', err);
      }
    });
  }
  
  onAnswerSubmit(){
    this.isAnswerSubmitted = true;
    if(this.answerForm.valid){
      const newAnswer = new Answer();  
      newAnswer.text = this.answerForm.value.text;
      newAnswer.isCorrect = this.answerForm.value.isCorrect;
      newAnswer.isDeleted = false;
      this.answers.push(newAnswer);
      this.displayAnswers.push(newAnswer)
      this.answerForm.reset({ text: '', isCorrect: false });
      this.showAnswerForm =  false;
      this.isAnswerSubmitted = false;
    }
  }

  getQuestionById(id: number): void {
    this.questionClient.getQuestionById(id).subscribe({
      next: (response) => {
        this.questionForm.patchValue({
          text: response.text,
          category: response.category,
          type: response.type
        });
        this.answers = response.answers ?? []; 
        this.displayAnswers = [...this.answers]; 
      },
      error: (err) => {
        console.error('Error loading question', err);
      }
    });
  }

  onAnswerEdit(index: number){
    this.isAnswerEditMode = true;
    this.editingAnswerIndex = index;
  }

  onCancelEditAnswer(){
    this.isAnswerEditMode = false;
    this.editingAnswerIndex = null;
  }

  updateAnswer(index: number){
    if (index >= 0 && index < this.displayAnswers.length) {
      const updatedAnswer = this.displayAnswers[index];
      const answerIndexInMainList = this.answers.findIndex(a => a.text === updatedAnswer.text);
      if (answerIndexInMainList !== -1) {
        this.answers[answerIndexInMainList] = updatedAnswer;
      }
      this.questionForm.get('type')?.updateValueAndValidity();
      this.isAnswerEditMode = false;
      this.editingAnswerIndex = null;
    }
  }

  onAnswerDelete(index: number) {
    console.log(this.answers)
    if (index >= 0 && index < this.displayAnswers.length) {
      const confirmDialog = this.dialog.open(DeleteAnswerDialogComponent);
      confirmDialog.afterClosed().subscribe(result => {
        if (result) {
          const answerIndexInMainList = this.answers.findIndex(a => a.text === this.displayAnswers[index].text);
          if (answerIndexInMainList !== -1) {
            this.answers[answerIndexInMainList].isDeleted = true; 
          }
          this.displayAnswers = this.displayAnswers.filter((_, i) => i !== index);
          if(!this.isEditMode){
            this.answers = this.answers.filter((_, i) => i !== answerIndexInMainList);
          }
          this.questionForm.get('type')?.updateValueAndValidity();
        }
      });
    }
  }

  errorMessage = {
    multipleCorrect: 'You can only select one correct answer for RadioButton question type.',
  };  
}