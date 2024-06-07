import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ImporterService } from '../../services/importer.service';
import { Importer } from '../../shared/importer.model';

@Component({
  selector: 'app-importer-new',
  templateUrl: './importer-new.component.html',
  styleUrl: './importer-new.component.css'
})
export class ImporterNewComponent {
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private importerService: ImporterService,
    private router: Router
  ) {}

  onSubmit(form: NgForm): void {
    if (!form.valid) {
      return;
    }

    this.isLoading = true;

    this.importerService.createImporter(form.value.name)
      .subscribe(
        response => {
          this.isLoading = false;
          this.router.navigate(['/importers']);
        },
        error => {
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
          this.isLoading = false;
        }
      );
  }
}
