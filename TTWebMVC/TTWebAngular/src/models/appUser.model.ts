export class AppUser {
  id: number;
  email: string;
  password: string;
  title: string;
  firstname: string;
  lastname: string;
  accessToken: string;
  refreshToken: string;

  public constructor(init?: Partial<AppUser>) {
    Object.assign(this, init);
  }
}
