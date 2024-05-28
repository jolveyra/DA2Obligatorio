import { Injectable } from '@angular/core';
import { Building } from './building.model';
import { BuildingFlats } from './building-flats/buildingFlats.model';
import { Flat } from './building-flats/flat.model';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {
  private buildings: Building[] = [ // FIXME: leave empty and make a request for real data
    new Building('1', 'B1', 1000, 2, 'Calle 1', 1, 'Calle 2', 40.416500, -3.702560),
    new Building('2', 'B2', 2000, 3, 'Calle 3', 2, 'Calle 4', 40.416500, -3.702560),
    new Building('3', 'B3', 3000, 4, 'Calle 5', 3, 'Calle 6', 40.416500, -3.702560),
    new Building('4', 'B4', 4000, 5, 'Calle 7', 4, 'Calle 8', 40.416500, -3.702560),
    new Building('5', 'B5', 5000, 6, 'Calle 9', 5, 'Calle 10', 40.416500, -3.702560),
    new Building('6', 'B6', 6000, 7, 'Calle 11', 6, 'Calle 12', 40.416500, -3.702560)
  ];
  private buildingFlats: BuildingFlats = new BuildingFlats('1', 'B1', 1000, [
    new Flat('1', 1, 1, 'Owner 1', 'Surname 1', 'owner1@mail.com', 2, 1, true),
    new Flat('1', 1, 1, 'Owner 2', 'Surname 1', 'owner1@mail.com', 2, 1, true),
    new Flat('1', 1, 1, 'Owner 1', 'Surname 1', 'owner1@mail.com', 2, 1, true)
  ], 'Calle 1', 1, 'Calle 2', '', 40.416500, -3.702560);

  constructor() { }

  getBuildings(): Building[] {
    return this.buildings.slice();
  }

  getBuildingFlats(id: string): BuildingFlats {
    return this.buildingFlats;
  }
}