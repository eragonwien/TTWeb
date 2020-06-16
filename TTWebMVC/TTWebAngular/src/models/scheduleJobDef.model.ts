export class ScheduleJobDef {
  id: number;
  name: string;
  appUserId: number;
  type: string;
  intervalType: string;
  timeFrom: string;
  timeTo: string;
  active: boolean;
  jobWeekDays: string[];

  public constructor(init?: Partial<ScheduleJobDef>) {
    Object.assign(this, init);
  }
}
