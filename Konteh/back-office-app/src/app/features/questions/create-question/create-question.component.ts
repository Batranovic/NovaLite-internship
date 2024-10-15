import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateQuestionCommand, QuestionsClient } from '../../../api/api-reference';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrl: './create-question.component.css'
})
export class CreateQuestionComponent {

  questionForm: FormGroup;

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
  }

  onSubmit(){
    console.log(this.questionForm.value)
    if(this.questionForm.valid){
      const command = new CreateQuestionCommand();
      command.text = this.questionForm.get('text')?.value;
      command.category = this.questionForm.get('category')?.value;
      command.questionType = this.questionForm.get('type')?.value;

      this.questionClient.create(command).subscribe({
        next: (response) => {
          console.log('Success: ', response)

         this.questionForm.reset();
        },
        error: (err) => {
          console.error('Error', err)
        }
      });
    }else{
      alert('All fields are required!')
    }
  }

}
