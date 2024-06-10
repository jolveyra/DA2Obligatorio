import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { BuildingResponseData } from './building.service';
import { Building } from '../shared/building.model';
import { environment } from './environment';

@Injectable({
  providedIn: 'root'
})
export class BuildingImportService {

  constructor(
    private httpClient: HttpClient
  ) { }
  
  importBuildings(dllName: string, fileName: string): Observable<BuildingResponseData[]> {
    return this.httpClient.post<BuildingResponseData[]>(`${environment.api}/api/v2/buildingImports`,
      {
        dllName,
        fileName
      }
    )
    .pipe(
      map((response: BuildingResponseData[]) => response.map(building => new Building(
        building.id,
        building.name,
        building.sharedExpenses,
        building.street,
        building.doorNumber,
        building.cornerStreet,
        building.constructorCompanyId,
        building.managerId,
        building.latitude,
        building.longitude
      )))
    );
  }

}
