import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function radioButtonRestriction(): ValidatorFn{
    return (control: AbstractControl) : ValidationErrors | null => {
        const questionType = control.get('type')?.value;
        const answers = control.get('answers')?.value;
        if (questionType === 1 && Array.isArray(answers)) {
            const correctAnswerCount = answers.filter((answer: any) => answer.isCorrect).length;
            
            if (correctAnswerCount > 1) {
              return { radioButtonRestriction: true };
            }
          }
        return null;
    }
}