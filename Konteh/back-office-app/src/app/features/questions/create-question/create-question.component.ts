import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Answer, CreateQuestionCommand, QuestionsClient, UpdateQuestionCommand } from '../../../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteAnswerDialogComponent } from '../delete-answer-dialog/delete-answer-dialog.component';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrl: './create-question.component.css'
})
export class CreateQuestionComponent {

  questionForm: FormGroup;
  answerForm: FormGroup;
  showAnswerForm: boolean = false;
  answers: Answer[] = [];
  isEditMode: boolean = false;
  questionId: number | null = null;
  isAnswerEditMode: boolean = false;
  editingAnswerIndex: number | null = null;

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

  constructor(private formBuilder: FormBuilder, private questionClient: QuestionsClient, private router: Router, private route: ActivatedRoute, private dialog: MatDialog){
    this.questionForm = this.formBuilder.group({
      text: ['', Validators.required],
      category: ['', Validators.required],
      type: ['', Validators.required]
    });
    this.answerForm = this.formBuilder.group({
      text: ['', Validators.required],
      isCorrect: [false, Validators.required]
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

  // onSubmit(){
  //   console.log(this.questionForm.value)
  //   if(this.questionForm.valid){
  //     const hasCorrectAnswer = this.answers.some(answer => answer.isCorrect);

  //     if (!hasCorrectAnswer) {
  //       alert('At least one correct answer is required!');
  //       return;
  //     }
  //     const command = new CreateQuestionCommand();
  //     command.text = this.questionForm.value.text;
  //     command.category = this.questionForm.value.category;
  //     command.questionType = this.questionForm.value.type;
  //     command.answers = this.answers;

  //     this.questionClient.create(command).subscribe({
  //       next: (response) => {
  //         const createdQuestionId = response.id;
  //         console.log(createdQuestionId);
  //         alert('Question successfully submitted!')
  //         this.router.navigate(['/question-overview/', response.id])
  //       },
  //       error: (err) => {
  //         console.error('Error', err)
  //       }
  //     });
  //   }else{
  //     alert('All fields are required!')
  //   }
  // }

  onSubmit() {
    if (this.questionForm.valid) {
      const hasCorrectAnswer = this.answers.some(answer => answer.isCorrect);
      if (!hasCorrectAnswer) {
        alert('At least one correct answer is required!');
        return;
      }
      if (this.isEditMode) {
        this.updateQuestion();
      } else {
        this.createQuestion();
      }
    } else {
      alert('All fields are required!');
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
        alert('Question successfully created!');
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
        alert('Question successfully updated!');
        this.router.navigate(['/question-overview/', this.questionId]);
      },
      error: (err) => {
        console.error('Error updating question', err);
      }
    });
  }
  
  onAnswerSubmit(){
    if(this.answerForm.valid){
      const newAnswer = new Answer();  
      newAnswer.text = this.answerForm.value.text;
      newAnswer.isCorrect = this.answerForm.value.isCorrect;
         
      this.answers.push(newAnswer);
      this.answerForm.reset({ text: '', isCorrect: false });
      this.showAnswerForm =  false;
      alert('Answer successfully submitted!')
    }
    else{
      alert('All fields for answer are required!')
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
        this.answers = response.answers ?? []; // empty array if undefined/null
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
    if (index >= 0 && index < this.answers.length) {
      const updatedAnswer = this.answers[index];
      this.isAnswerEditMode = false;
      this.editingAnswerIndex = null;
      console.log(`Updated Answer ${index}:`, updatedAnswer);
    }
  }

  onAnswerDelete(index: number) {
    if(index >= 0 && index < this.answers.length){
      const confirmDialog = this.dialog.open(DeleteAnswerDialogComponent);
      confirmDialog.afterClosed().subscribe(result => {
        if(result){
          this.answers[index].isDeleted = true;
          this.answers = this.answers.filter(answer => !answer.isDeleted)
        }
      })
    }
  }

}
