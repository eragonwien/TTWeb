CREATE OR REPLACE VIEW v_appuser AS
SELECT 
	appuser.id AS appuser_id,
	appuser.email AS email,
	appuser.title AS title,
	appuser.firstname AS firstname,
	appuser.lastname AS lastname,
	appuser.disabled AS disabled,
	appuser.active AS active,
	role.id AS role_id,
	role.name AS role_name,
	role.description AS role_description,
	facebookcredential.id as fb_credential_id,
	facebookcredential.fb_username,
	facebookcredential.fb_password
FROM
	appuser
	LEFT JOIN facebookcredential on appuser.id=facebookcredential.appuser_id
	LEFT JOIN role on appuser.role_id=role.id;
        
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
left join v_schedulejobdef_weekday jwd on d.id=jwd.schedulejobdef_id;

CREATE OR REPLACE VIEW v_schedulejobdef_weekday AS
select 
	jobweekday.schedulejobdef_id,
	group_concat(jobweekday.scheduleweekday_id order by jobweekday.scheduleweekday_id asc separator ',') as scheduleweekday_ids
from jobweekday
group by jobweekday.schedulejobdef_id;

CREATE OR REPLACE VIEW v_open_schedulejob AS
SELECT 
	def.id as id,
    def.appuser_id as appuser_id,
    def.friend_id as friend_id, 
    def.facebookcredential_id as facebookcredential_id, 
    def.name as name,
    def.type as type,
    def.interval_type as interval_type,
    def.time_from as time_from,
    def.time_to as time_to,
    def.timezone_id as timezone_id,
    def.active as active
FROM 
	schedulejobdef def 
	LEFT JOIN schedulejobdetail detail ON def.id=detail.schedulejobdef_id
WHERE 
	def.active=1
	AND (detail.execution_time IS NULL OR DATE(detail.execution_time) < UTC_DATE());