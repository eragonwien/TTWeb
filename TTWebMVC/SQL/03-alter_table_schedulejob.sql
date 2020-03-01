alter table ScheduleJob add process_date TIMESTAMP null default null;
alter table ScheduleJob change process_date planned_date TIMESTAMP null default null;