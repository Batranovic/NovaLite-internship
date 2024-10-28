import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MatToolbar, MatToolbarModule} from "@angular/material/toolbar";
import { MatIcon, MatIconModule} from '@angular/material/icon';
import { MatAnchor, MatButtonModule, MatIconButton} from '@angular/material/button';
import { MaterialModule} from '../../shared/material/material.module';
import { MatMenuModule } from '@angular/material/menu';
import { RouterLink } from '@angular/router';

@NgModule({
  declarations: [
    NavBarComponent,
    ConfirmDialogComponent
  ],
  exports: [
    NavBarComponent
  ],
  imports: [
    CommonModule,
    MatToolbar,
    MatIcon,
    MatIconButton,
    MatAnchor,
    MaterialModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    RouterLink
  ]
})
export class LayoutModule { }
