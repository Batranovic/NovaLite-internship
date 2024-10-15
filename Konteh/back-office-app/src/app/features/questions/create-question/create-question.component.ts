import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Answer, CreateQuestionCommand, QuestionsClient } from '../../../api/api-reference';

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

  constructor(private formBuilder: FormBuilder, private questionClient: QuestionsClient){
    this.questionForm = this.formBuilder.group({
      text: ['', Validators.required],
      category: ['', Validators.required],
      type: ['', Validators.required]
    })
    this.answerForm = this.formBuilder.group({
      text: ['', Validators.required],
      isCorrect: ['', Validators.required]
    })
  }

  onSubmit(){
    console.log(this.questionForm.value)
    if(this.questionForm.valid){
      const hasCorrectAnswer = this.answers.some(answer => answer.isCorrect);

      if (!hasCorrectAnswer) {
        alert('At least one correct answer is required!');
        return;
      }
      const command = new CreateQuestionCommand();
      command.text = this.questionForm.value.text;
      command.category = this.questionForm.value.category;
      command.questionType = this.questionForm.value.type;
      command.answers = this.answers;

      this.questionClient.create(command).subscribe({
        next: (response) => {
          console.log('Success: ', response)
          alert('Question successfully submitted!')
          this.questionForm.reset();
          this.answers = [];
        },
        error: (err) => {
          console.error('Error', err)
        }
      });
    }else{
      alert('All fields are required!')
    }
  }

  onAnswerSubmit(){
    if(this.answerForm.valid){
      const newAnswer = new Answer();  
      newAnswer.text = this.answerForm.value.text;
      newAnswer.isCorrect = this.answerForm.value.isCorrect;
         
      this.answers.push(newAnswer);
      this.answerForm.reset();
      this.showAnswerForm =  false;
      alert('Answer successfully submitted!')
    }
    else{
      alert('All fields for answer are required!')
    }
  }

}
