import { Component, Inject } from '@angular/core';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialogContent,
  MatDialogActions,
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-welcome-dialog',
  imports: [
    MatInputModule,
    MatFormFieldModule,
    MatDialogContent,
    MatDialogActions,
    MatDialogTitle,
    MatButtonModule,
    FormsModule,
    CommonModule,
  ],
  templateUrl: './welcome-dialog.component.html',
  styleUrl: './welcome-dialog.component.css',
})
export class WelcomeDialogComponent {
  userName: string = '';

  constructor(
    public dialogRef: MatDialogRef<WelcomeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  onSubmit(): void {
    if (this.userName && this.userName.trim().length > 0) {
      this.dialogRef.close(this.userName.trim());
    }
  }
}
