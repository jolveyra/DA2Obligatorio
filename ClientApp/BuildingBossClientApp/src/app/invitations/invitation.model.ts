export class Invitation {
    constructor(
        public id: string,
        public name: string,
        public email: string,
        public expirationDate: Date,
        public role: string,
        public isAccepted: boolean,
        public isAnswered: boolean
    ) {}
}