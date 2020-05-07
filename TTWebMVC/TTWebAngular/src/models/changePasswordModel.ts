export class ChangePasswordModel {
  oldPassword: string;
  newPassword: string;
  confirmNewPassword: string;

  public constructor(init?: Partial<ChangePasswordModel>) {
    Object.assign(this, init);
  }
}
