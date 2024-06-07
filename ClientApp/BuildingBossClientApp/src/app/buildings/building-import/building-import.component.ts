import { Component } from '@angular/core';
import { Importer } from '../../shared/importer.model';
import { Router } from '@angular/router';
import { ImporterService } from '../../services/importer.service';
import { ImporterBuildingsService } from '../../services/import-buildings.service';

@Component({
  selector: 'app-building-import',
  templateUrl: './building-import.component.html',
  styleUrl: './building-import.component.css'
})
export class BuildingImportComponent {

  selectedImporterName: string = '';
  importers: Importer[] = [];
  fileName: string = '';
  error: string = '';
  isLoading: boolean = false;


  constructor(
    private router: Router,
    private importerService: ImporterService,
    private importBuildingService: ImporterBuildingsService
  ) { }

  ngOnInit(): void {
    this.importerService.fetchImporters()
      .subscribe(
        response => {
          this.importers = response;
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

  onSubmit(){
    this.isLoading = true;
    this.importBuildingService.importBuildings(this.selectedImporterName, this.fileName)
      .subscribe(
        () => {
          this.isLoading = false;
          this.router.navigate(['/buildings']);
        },
        error => {
          this.isLoading = false;
          let errorMessage = "An unexpected error has occured, please retry later."
          if (error.error && error.error.errorMessage) {
            this.error = error.error.errorMessage;
          } else {
            this.error = errorMessage;
          }
        }
      );
  }

  selectImporter(importer: Importer){
    this.selectedImporterName = importer.name;
  }
}
