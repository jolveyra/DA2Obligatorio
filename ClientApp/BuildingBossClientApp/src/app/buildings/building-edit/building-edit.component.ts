import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Building } from '../../shared/building.model';
import { BuildingService } from '../../services/building.service';
import { User } from '../../shared/user.model';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-building-edit',
  templateUrl: './building-edit.component.html',
  styleUrl: './building-edit.component.css'
})
export class BuildingEditComponent {

  // Tengo que obtener el manager del building y los managers del sistema.
  // Para eso primero tengo que obtener el building
  // En el html tengo que mostrar el nombre del building como título, y los datos del manager actual o un mensaje de que no tiene manager asignado.
  // Si tiene manager asignado, tengo que mostrar un desplegable con los managers del sistema para que pueda cambiarlo.
  // Al cambiarlo, tengo que actualizar el building con el nuevo manager y volver a la lista de buildings.

  error: string = '';
  isLoading: boolean = false;
  building: Building = new Building('', '', 0, '', 0, '', '', '', 0, 0);
  managerId: string = '';
  manager: User = new User('', '', '', '');
  managers: User[] = [];
  selectedManagerId: string = '';
  selectedManagerName: string = '';
  selectedManagerSurname: string = '';
  selectedManagerEmail: string = '';
  newName: string = '';


  constructor(
    private buildingService: BuildingService,
    private managerService: ManagerService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void{
    this.route.params.subscribe(params => {
      this.buildingService.fetchConstructorCompanyBuilding(params['buildingId'])
        .subscribe(
          response => {
            this.building = response;
            this.newName = response.name;
            this.managerId = this.building.managerId;

            this.managerService.fetchManagers()
              .subscribe(
                response => {
                  this.managers = response;
                  const managerInList = this.managers.find(manager => manager.id === this.managerId);
                  
                  if(managerInList)
                  {
                    this.manager = managerInList;
                    this.selectedManagerName = this.manager.name;
                    this.selectedManagerEmail = this.manager.email;
                    this.selectedManagerSurname = this.manager.surname;
                    this.selectedManagerId = this.manager.id;
                  }else{
                    this.selectedManagerName = '';
                    this.selectedManagerEmail = '';
                    this.selectedManagerSurname = '';
                    this.selectedManagerId = '';
                  }
                }
              );
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
    });
  }

  onSubmit(): void{
    this.isLoading = true;
    this.buildingService.updateConstructorCompanyBuilding(this.building.id, this.selectedManagerId, this.newName)
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

  selectManager(manager: User){
    this.selectedManagerId = manager.id;
    this.selectedManagerName = manager.name;
    this.selectedManagerSurname = manager.surname;
    this.selectedManagerEmail = manager.email;
  }
}
