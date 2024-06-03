export class Request {
    constructor(
        public id: string,
        public description: string,
        public flat: number,
        public building: string,
        public categoryName: string,
        public assignedEmployee: string,
        public status: string,
    ) {}
}