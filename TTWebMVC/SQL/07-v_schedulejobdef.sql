CREATE OR REPLACE VIEW v_schedulejobdef AS
select 
	d.id, 
    d.appuser_id, 
    d.friend_id, 
    d.facebookcredential_id, 
    d.name, 
    d.type, 
    d.interval_type, 
    d.time_from, 
    d.time_to, 
    d.timezone_id, 
    d.active,
    a.email as appuser_email,
    a.title as appuser_title,
    a.firstname as appuser_firstname,
    a.lastname as appuser_lastname,
    a.disabled as appuser_disabled,
    a.active as appuser_active,
    a.role_name as appuser_role,
    f.name as friend_name,
    f.profile_link as friend_profile_link,
    f.disabled as friend_disabled,
    jwd.scheduleweekday_ids
from schedulejobdef d
inner join v_appuser a on d.appuser_id=a.appuser_id
left join friend f on d.friend_id=f.id and d.appuser_id=f.appuser_id
left join v_schedulejobdef_weekday jwd on d.id=jwd.schedulejobdef_id
;