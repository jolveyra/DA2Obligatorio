export class Building {
    constructor(
        public id: string,
        public name: string,
        public sharedExpenses: number,
        public amountOfFlats: number,
        public street: string,
        public doorNumber: number,
        public cornerStreet: string,
        public constructorCompany: string, // FIXME: check if this is correct when merging with new version of api
        public latitude: number,
        public longitude: number,
    ) {}
}