export class Building {
    constructor(
        public id: string,
        public name: string,
        public sharedExpenses: number,
        public amountOfFlats: number,
        public street: string,
        public doorNumber: number,
        public cornerStreet: string,
        public constructorCompanyId: string,
        public managerId: string,
        public latitude: number,
        public longitude: number,
    ) {}
}