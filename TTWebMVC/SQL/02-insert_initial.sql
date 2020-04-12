INSERT INTO ScheduleJobType(name) values('POST');
INSERT INTO ScheduleJobParameterType(name) values('TEXT');
INSERT INTO LoginUserRole(name) values('ACTIVATE_USER');
INSERT INTO LoginUserRole(name) values('VIEW_USERS');
insert into loginuser(email, password, firstname, lastname) values('eragonwien@gmail.com', '', 'test', 'son');
insert into loginuserrolemapping(loginuser_id, loginuserrole_id) values((select id from loginuser where email='eragonwien@gmail.com'), (select id from loginuserrole where name='ACTIVATE_USER'));
insert into loginuserrolemapping(loginuser_id, loginuserrole_id) values((select id from loginuser where email='eragonwien@gmail.com'), (select id from loginuserrole where name='VIEW_USERS'));