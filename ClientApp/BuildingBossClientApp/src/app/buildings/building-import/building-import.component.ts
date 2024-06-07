import { Component } from '@angular/core';
import { Importer } from '../../shared/importer.model';

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


  constructor() { }

  ngOnInit(): void {
    
  }

  onSubmit(){
    console.log('Submit');
  }

  selectImporter(importer: Importer){
    console.log('Select importer');
  }
}
