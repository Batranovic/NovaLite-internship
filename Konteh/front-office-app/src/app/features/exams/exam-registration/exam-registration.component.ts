import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ExamClient, GenerateExamCommand, GenerateExamResponse } from '../../../api/api-reference';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { setServerSideValidationErrors } from '../../../shared/validation';

@Component({
  selector: 'app-exam-registration',
  templateUrl: './exam-registration.component.html',
  styleUrl: './exam-registration.component.css'
})
export class ExamRegistrationComponent {

  constructor(private examClient: ExamClient, private router: Router){}

  examForm = new FormGroup({
    candidateName: new FormControl('', Validators.required),
    candidateSurname: new FormControl('', Validators.required),
    candidateEmail: new FormControl('', Validators.required),
    candidateFaculty: new FormControl('', Validators.required)
  });

  onSubmit() {
    if (this.examForm.valid) {
      const command = new GenerateExamCommand({
        candidateName: this.examForm.value.candidateName!, 
        candidateSurname: this.examForm.value.candidateSurname!, 
        candidateEmail: this.examForm.value.candidateEmail!,  
        candidateFaculty: this.examForm.value.candidateFaculty!  
      });
     
      this.examClient.generateExam(command).subscribe({
        next: (response: GenerateExamResponse) => {
            this.router.navigate(['exam-overview'], { state: { examResponse: response } });
        },
        error: errors => setServerSideValidationErrors(errors, this.examForm)
    });
    } 
  }
}
