select * from v_appuser;

update appuser set active=1, disabled=1 where email='eragonwien@gmail.com';
update appuser set role_id=(select id from role where name='ADMIN') where email='eragonwien@gmail.com';