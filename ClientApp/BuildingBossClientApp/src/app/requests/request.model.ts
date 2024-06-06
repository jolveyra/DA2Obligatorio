import { Building } from "../shared/building.model";
import { Flat } from "../shared/flat.model";
import { User } from "../shared/user.model";

export class ManagerRequest {
    constructor(
        public id: string,
        public description: string,
        public flat: Flat,
        public building: Building,
        public categoryName: string,
        public assignedEmployee: User | undefined,
        public status: string,
    ) {}
}

export class EmployeeRequest {
    constructor(
        public id: string,
        public description: string,
        public flat: Flat,
        public building: Building,
        public categoryName: string,
        public status: string,
    ) {}
}