import { Flat } from "./flat.model";

export class BuildingFlats {
    constructor(
        public id: string,
        public name: string,
        public sharedExpenses: number,
        public flats: Flat[],
        public street: string,
        public doorNumber: number,
        public cornerStreet: string,
        public constructorCompanyId: string,
        public managerId: string,
        public latitude: number,
        public longitude: number,
        public maintenanceEmployeeIds: string[]
    ) {}
}