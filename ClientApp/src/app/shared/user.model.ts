export class User {
  public password: string | undefined;

  constructor(
      public id: string,
      public name: string,
      public surname: string,
      public email: string,
  ) {}
}