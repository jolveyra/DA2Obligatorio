export class Flat {
    constructor(
        public id: string,
        public buildingId: string,
        public number: number,
        public floor: number,
        public ownerName: string,
        public ownerSurname: string,
        public ownerEmail: string,
        public rooms: number,
        public bathrooms: number,
        public hasBalcony: boolean
    ) {}
}