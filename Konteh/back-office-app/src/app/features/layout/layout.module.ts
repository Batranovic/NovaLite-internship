import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { MatToolbar, MatToolbarModule} from "@angular/material/toolbar";
import { MatIcon, MatIconModule} from '@angular/material/icon';
import { MatAnchor, MatButtonModule, MatIconButton} from '@angular/material/button';
import { MaterialModule} from '../../shared/material/material.module';
import { MatMenuModule } from '@angular/material/menu';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [
    NavBarComponent,
    ConfirmDialogComponent,
    NotFoundComponent
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
    MatMenuModule
  ]
})
export class LayoutModule { }
