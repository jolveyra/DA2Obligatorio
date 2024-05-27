export class Building {
    constructor(
        public id: string,
        public name: string,
        public sharedExpenses: number,
        public amountOfFlats: number,
        public street: string,
        public doorNumber: number,
        public CornerStreet: string,
        public Latitude: number,
        public Longitude: number
    ) {}
}