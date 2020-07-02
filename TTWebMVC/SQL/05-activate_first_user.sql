select * from v_appuser;

update appuser set active=1 where email='eragonwien@gmail.com';
insert into appuserrole(role_id, appuser_id) values((select id from role where name='ACTIVATE_USER'), (select id from appuser where email='eragonwien@gmail.com'));
insert into appuserrole(role_id, appuser_id) values((select id from role where name='VIEW_USERS'), (select id from appuser where email='eragonwien@gmail.com'));