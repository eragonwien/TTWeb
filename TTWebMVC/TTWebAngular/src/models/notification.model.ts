export class NotificationAlert {
  type: NotificationAlertType;
  texts: string[] = [];

  public constructor(type: NotificationAlertType, texts: string[]) {
    this.type = type;
    this.texts = texts;
  }
}

export enum NotificationAlertType {
  success,
  error,
  warning,
}
