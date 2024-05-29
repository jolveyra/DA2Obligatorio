import { Injectable } from '@angular/core';
import { Building } from '../shared/building.model';
import { Flat } from '../shared/flat.model';
import { BuildingFlats } from '../shared/buildingFlats.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface BuildingResponseData {
  id: string;
  name: string,
  sharedExpenses: number,
  amountOfFlats: number,
  street: string,
  doorNumber: number,
  CornerStreet: string,
  Latitude: number,
  Longitude: number
}

@Injectable({
  providedIn: 'root'
})
export class BuildingService {
  private buildings: Building[] = [];
  private buildingFlats: BuildingFlats = new BuildingFlats('1', 'B1', 1000, [
    new Flat('1', 1, 1, 'Owner 1', 'Surname 1', 'owner1@mail.com', 2, 1, true),
    new Flat('1', 1, 1, 'Owner 2', 'Surname 1', 'owner1@mail.com', 2, 1, true),
    new Flat('1', 1, 1, 'Owner 1', 'Surname 1', 'owner1@mail.com', 2, 1, true)
  ], 'Calle 1', 1, 'Calle 2', '', 40.416500, -3.702560);

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchBuildings(): Observable<BuildingResponseData[]> {
    return this.httpClient.get<BuildingResponseData[]>('https://localhost:7122/api/v1/buildings');
  }

  getBuildings(): Building[] {
    return this.buildings.slice();
  }

  getBuildingFlats(id: string): BuildingFlats {
    return this.buildingFlats;
  }
 
  setBuildings(buildings: BuildingResponseData[]): void {
    this.buildings = buildings.map(building => new Building(
      building.id,
      building.name,
      building.sharedExpenses,
      building.amountOfFlats,
      building.street,
      building.doorNumber,
      building.CornerStreet,
      building.Latitude,
      building.Longitude
    ));
    console.log(this.buildings);
  }
}