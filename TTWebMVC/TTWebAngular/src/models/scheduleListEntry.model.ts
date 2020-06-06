export class ScheduleJob {
  id: number;
  name: string;
  appUserId: number;
  type: ScheduleJobType;
  parameters: ScheduleJobParameter[];
  plannedDate: string;

  constructor(init?: Partial<ScheduleJob>) {
    Object.assign(this, init);
  }
}

export class ScheduleJobParameter {
  id: number;
  type: ScheduleJobParameterType;
  value: string;
}

export class ScheduleJobParameterType {
  id: number;
  name: string;
}

export class ScheduleJobType {
  id: number;
  name: string;
}

export class ScheduleJobDetail {
  id: number;
  name: string;
  appUserId: number;
  type: string;
  plannedDate: string;
  interval: ScheduleInterval;
}

export class ScheduleInterval {
  type: string;
  days: string[];
  from: string;
  to: string;
}
