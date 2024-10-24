import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ReactiveFormsModule, UntypedFormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
    selector: 'form-errors',
    template: `
    <div *ngIf="form.errors">
      <mat-error *ngFor="let error of errors">
        <span class="mat-body-2">{{error}}</span>
      </mat-error>
    </div>
  `,
    changeDetection: ChangeDetectionStrategy.Default,
    standalone: true,
    imports: [MatFormFieldModule, ReactiveFormsModule, CommonModule]
})
export class FormErrorsComponent {
    /** A form group for which the validation errors are being displayed. */
    @Input() form!: UntypedFormGroup;

    get errors(){
        return this.form.errors!['general'];
    }
}