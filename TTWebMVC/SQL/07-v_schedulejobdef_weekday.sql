CREATE OR REPLACE VIEW v_schedulejobdef_weekday AS
select 
	jobweekday.schedulejobdef_id,
	group_concat(jobweekday.scheduleweekday_id order by jobweekday.scheduleweekday_id asc separator ',') as scheduleweekday_ids
from jobweekday
group by jobweekday.schedulejobdef_id
;