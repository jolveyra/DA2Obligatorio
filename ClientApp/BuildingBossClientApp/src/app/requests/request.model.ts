export class Request {
    constructor(
        public id: string,
        public description: string,
        public flatId: string,
        public categoryId: string,
        public assignedEmployeeId: string,
        public status: string,
    ) {}
}