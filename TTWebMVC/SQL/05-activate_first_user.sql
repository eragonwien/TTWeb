update appuser set active=1, disabled=0 where email='eragonwien@gmail.com';
update appuser set role_id=(select id from role where name='ADMIN') where email='eragonwien@gmail.com';