export class UserLogged {
    constructor(
        public name: string,
        private _token: string,
        public role: string,
    ) {}

    get token() {
        return this._token;
    }
}