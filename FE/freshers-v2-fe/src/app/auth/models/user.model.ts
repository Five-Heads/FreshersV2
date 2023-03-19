export class User {
  public role: string = '';
  constructor(
    public id: string,
    public userName: string,
    public facultyNumber: string,
    public token: string
  ) {}
}
