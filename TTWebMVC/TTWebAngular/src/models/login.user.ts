export class LoginUser {
  id: number;
  email: string;
  title: string;
  firstname: string;
  lastname: string;
  accessToken: string;
  refreshToken: string;
  changePasswordRequired: boolean;

  public constructor(init?: Partial<LoginUser>) {
    Object.assign(this, init);
  }
}
