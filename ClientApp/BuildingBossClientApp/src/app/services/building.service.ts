import { Injectable } from '@angular/core';
import { Building } from '../shared/building.model';
import { Flat } from '../shared/flat.model';
import { BuildingFlats } from '../shared/buildingFlats.model';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from './environment';

export interface BuildingResponseData {
  id: string;
  name: string,
  sharedExpenses: number,
  street: string,
  doorNumber: number,
  cornerStreet: string,
  constructorCompanyId: string,
  managerId: string,
  latitude: number,
  longitude: number
}

export interface UpdateBuildingResponseData {
  id: string,
  name: string,
  street: string,
  doorNumber: number,
  managerName: string
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

export interface FlatResponseData {
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

  fetchConstructorCompanyBuildings(): Observable<BuildingResponseData[]> {
    return this.httpClient.get<BuildingResponseData[]>(`${environment.api}/api/v2/constructorCompanyBuildings`)
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

  fetchManagerBuildings(): Observable<BuildingFlatsResponseData[]> {
    return this.httpClient.get<BuildingFlatsResponseData[]>(`${environment.api}/api/v2/buildings`)
      .pipe(
        map((response: BuildingFlatsResponseData[]) => { 
          return response.map(building => new BuildingFlats(
            building.id,
            building.name,
            building.sharedExpenses,
            building.flats.map(flat => new Flat(
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
            building.street,
            building.doorNumber,
            building.cornerStreet,
            building.constructorCompanyId,
            building.managerId,
            building.latitude,
            building.longitude,
            building.maintenanceEmployeeIds
          ))
        })
      );
  }

  fetchManagerBuilding(id: string): Observable<BuildingFlatsResponseData> {
    return this.httpClient.get<BuildingFlats>(`${environment.api}/api/v2/buildings/${id}`)
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

  fetchConstructorCompanyBuilding(id: string): Observable<BuildingResponseData> {
    return this.httpClient.get<BuildingResponseData>(`${environment.api}/api/v2/constructorCompanyBuildings/${id}`)
      .pipe(
        map((response: BuildingResponseData) => new Building(
            response.id,
            response.name,
            response.sharedExpenses,
            response.street,
            response.doorNumber,
            response.cornerStreet,
            response.constructorCompanyId,
            response.managerId,
            response.latitude,
            response.longitude
        ))
      );
  }

  createConstructorCompanyBuilding(building: Building, amountOfFlats: number): Observable<BuildingResponseData> {
    return this.httpClient.post<BuildingResponseData>(`${environment.api}/api/v2/constructorCompanyBuildings`,
      {
        name: building.name,
        sharedExpenses: building.sharedExpenses,
        street: building.street,
        doorNumber: building.doorNumber,
        cornerStreet: building.cornerStreet,
        constructorCompanyId: building.constructorCompanyId,
        amountOfFlats: amountOfFlats,
        latitude: building.latitude,
        longitude: building.longitude
      }
    )
      .pipe(
        map((response: BuildingResponseData) => new Building(
            response.id,
            response.name,
            response.sharedExpenses,
            response.street,
            response.doorNumber,
            response.cornerStreet,
            response.constructorCompanyId,
            response.managerId,
            response.latitude,
            response.longitude
        ))
      );
  }

  fetchFlat(buildingId: string, flatId: string): Observable<FlatResponseData> {
    return this.httpClient.get<FlatResponseData>(`${environment.api}/api/v2/buildings/${buildingId}/flats/${flatId}`)
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

  updateBuilding(buildingId: string, sharedExpenses: number, maintenanceEmployees: string[]): Observable<BuildingFlatsResponseData> {
    return this.httpClient.put<BuildingFlatsResponseData>(`${environment.api}/api/v2/buildings/${buildingId}`, // 
      {
        sharedExpenses,
        maintenanceEmployees
      }).pipe(
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


  
  updateConstructorCompanyBuilding(buildingId: string, managerId: string, name: string): Observable<BuildingResponseData> {
    return this.httpClient.put<BuildingResponseData>(`${environment.api}/api/v2/constructorCompanyBuildings/${buildingId}`,
    {
      managerId,
      name
    })
      .pipe(
        map((response: BuildingResponseData) => new Building(
            response.id,
            response.name,
            response.sharedExpenses,
            response.street,
            response.doorNumber,
            response.cornerStreet,
            response.constructorCompanyId,
            response.managerId,
            response.latitude,
            response.longitude
        ))
      );
  }

  deleteBuilding(buildingId: string): Observable<void> {
    return this.httpClient.delete<void>(`${environment.api}/api/v2/constructorCompanyBuildings/${buildingId}`);
  }

  updateFlat(flat: Flat, changeOwner: boolean): Observable<FlatResponseData> {
    return this.httpClient.put<FlatResponseData>(`${environment.api}/api/v2/buildings/${flat.buildingId}/flats/${flat.id}`,
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