export class ConstructorCompanyAdministrator {
  constructor(
      public id: string,
      public name: string,
      public surname: string,
      public email: string,
      public constructorCompanyId: string,
      public constructorCompanyName: string
  ) {}
}