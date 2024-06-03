import { Injectable } from '@angular/core';
import { Building } from '../shared/building.model';
import { Flat } from '../shared/flat.model';
import { BuildingFlats } from '../shared/buildingFlats.model';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

interface BuildingResponseData {
  id: string;
  name: string,
  sharedExpenses: number,
  amountOfFlats: number,
  street: string,
  doorNumber: number,
  cornerStreet: string,
  constructorCompanyId: string,
  managerId: string,
  latitude: number,
  longitude: number
}

interface BuildingFlatsResponseData {
  id: string,
  name: string,
  sharedExpenses: number,
  flats: Flat[],
  street: string,
  doorNumber: number,
  cornerStreet: string,
  constructorCompanyId: string,
  managerId: string,
  latitude: number,
  longitude: number,
  maintenanceEmployeeIds: string[]
}

interface FlatResponseData {
  id: string,
  buildingId: string,
  number: number,
  floor: number,
  ownerName: string,
  ownerSurname: string,
  ownerEmail: string,
  rooms: number,
  bathrooms: number,
  hasBalcony: boolean
}

@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchManagerBuildings(): Observable<BuildingResponseData[]> {
    return this.httpClient.get<BuildingResponseData[]>('https://localhost:7122/api/v2/buildings')
      .pipe(
        map((response: BuildingResponseData[]) => response.map(building => new Building(
            building.id,
            building.name,
            building.sharedExpenses,
            building.amountOfFlats,
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

  fetchBuildingFlats(id: string): Observable<BuildingFlatsResponseData> {
    return this.httpClient.get<BuildingFlats>(`https://localhost:7122/api/v2/buildings/${id}`)
      .pipe(
        map((response: BuildingFlatsResponseData) => {
          const buildingWithFlats = new BuildingFlats(
          response.id,
          response.name,
          response.sharedExpenses,
          response.flats.map(flat => new Flat(
            flat.id,
            flat.buildingId,
            flat.number,
            flat.floor,
            flat.ownerName,
            flat.ownerSurname,
            flat.ownerEmail,
            flat.rooms,
            flat.bathrooms,
            flat.hasBalcony)),
          response.street,
          response.doorNumber,
          response.cornerStreet,
          response.constructorCompanyId,
          response.managerId,
          response.latitude,
          response.longitude,
          response.maintenanceEmployeeIds
        );
        buildingWithFlats.flats.sort(flat => flat.number);
        return buildingWithFlats;
      })
      );
  }

  fetchFlat(buildingId: string, flatId: string): Observable<FlatResponseData> {
    return this.httpClient.get<FlatResponseData>(`https://localhost:7122/api/v2/buildings/${buildingId}/flats/${flatId}`)
      .pipe(
        map((response: FlatResponseData) => new Flat(
          response.id,
          response.buildingId,
          response.number,
          response.floor,
          response.ownerName,
          response.ownerSurname,
          response.ownerEmail,
          response.rooms,
          response.bathrooms,
          response.hasBalcony))
      );
  }

  updateBuilding(buildingId: string, sharedExpenses: number, maintenanceEmployeeIds: string[]): Observable<BuildingResponseData> {
    return this.httpClient.put<BuildingResponseData>(`https://localhost:7122/api/v2/buildings/${buildingId}`, // 
      {
        sharedExpenses,
        maintenanceEmployeeIds
      }
    );
  }

  updateFlat(flat: Flat, changeOwner: boolean): Observable<FlatResponseData> {
    return this.httpClient.put<FlatResponseData>(`https://localhost:7122/api/v2/buildings/${flat.buildingId}/flats/${flat.id}`,
      {
        floor: flat.floor,
        number: flat.number,
        ownerName: flat.ownerName,
        ownerSurname: flat.ownerSurname,
        ownerEmail: flat.ownerEmail,
        rooms: flat.rooms,
        bathrooms: flat.bathrooms,
        hasBalcony: flat.hasBalcony,
        changeOwner: changeOwner
      }
    )
      .pipe(
        map((response: FlatResponseData) => new Flat(
          response.id,
          response.buildingId,
          response.number,
          response.floor,
          response.ownerName,
          response.ownerSurname,
          response.ownerEmail,
          response.rooms,
          response.bathrooms,
          response.hasBalcony))
      );
  }
}