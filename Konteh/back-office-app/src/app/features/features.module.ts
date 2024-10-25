import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatRadioButton, MatRadioGroup } from '@angular/material/radio';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    CommonModule,
    MatCardModule,
    MatRadioButton,
    MatRadioGroup,
    MatListModule
  ]
})
export class FeaturesModule { }