import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ExamClient, GenerateExamCommand, GenerateExamResponse } from '../../../api/api-reference';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-exam-registration',
  templateUrl: './exam-registration.component.html',
  styleUrl: './exam-registration.component.css'
})
export class ExamRegistrationComponent {
  errorMessage: string | null = null;

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
        candidateName: this.examForm.value.candidateName ?? '', 
        candidateSurname: this.examForm.value.candidateSurname ?? '', 
        candidateEmail: this.examForm.value.candidateEmail ?? '',  
        candidateFaculty: this.examForm.value.candidateFaculty ?? ''  
      });
     
      this.examClient.generateExam(command).subscribe(
        (response: GenerateExamResponse) => {
          this.router.navigate(['exam-overview'], { state: { examResponse: response } });
        },
        (error: HttpErrorResponse) => {
          if (error.status === 500) {
            this.errorMessage = "You already took the quiz!";
          } else {
            this.errorMessage = "An unexpected error occurred.";
          }
        }
      );
    } 
  }
  
}
