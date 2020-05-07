export class LoginViewModel {
  email: string;
  password: string;

  public constructor(init?: Partial<LoginViewModel>) {
    Object.assign(this, init);
  }
}
