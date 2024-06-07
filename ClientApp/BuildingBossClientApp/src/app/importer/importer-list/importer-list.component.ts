import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ImporterService } from '../../services/importer.service';
import { Importer } from '../../shared/importer.model';

@Component({
  selector: 'app-importer-list',
  templateUrl: './importer-list.component.html',
  styleUrl: './importer-list.component.css'
})
export class ImporterListComponent {
  importers: Importer[] = [];
  error: string = '';
  noImporters: boolean = false;

  constructor(
    private importerService: ImporterService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.importerService.fetchImporters()
      .subscribe(
        response => {
          this.importers = response;
          if (this.importers.length === 0) {
            this.noImporters = true;
          }
        },
        error => {
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
        }
      );
    
  }

  onNewImporter(): void {
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
