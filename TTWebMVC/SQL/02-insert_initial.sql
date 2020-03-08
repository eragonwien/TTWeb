INSERT INTO ScheduleJobType(name) values('POST');
INSERT INTO ScheduleJobParameterType(name) values('TEXT');
INSERT INTO LoginUserRole(name) values('REGULAR');
INSERT INTO LoginUserRole(name) values('ADMIN');
insert into loginuser(email, password, firstname, lastname, loginuserrole_id) values('eragonwien@gmail.com', '', 'test', 'son', (select id from loginuserrole where name='ADMIN'));