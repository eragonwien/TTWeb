use ttweb;
drop table if exists
	schedulejobdetail,
    schedulejobpartner,
    partner,
    jobweekday,
    weekday,
    schedulejobdef,
    appuserrole,
	appuser,
	role
;

create table Role (
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(16) UNIQUE NOT NULL,
    description VARCHAR(256),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE AppUser (
	id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(64) UNIQUE NOT NULL,
    password VARCHAR(256) NOT NULL,
    title VARCHAR(64),
    firstname VARCHAR(64),
    lastname VARCHAR(64),
    refresh_token VARCHAR(64),
    disabled bit(1) DEFAULT 0 NOT NULL,
    active bit(1)  DEFAULT 0 NOT NULL,
    facebook_user VARCHAR(128),
    facebook_password VARCHAR(256),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table AppUserRole (
	role_id INT NOT NULL,
    appuser_id INT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES role(id),
    FOREIGN KEY (appuser_id) REFERENCES appuser(id),
    CONSTRAINT appuserrole_pk PRIMARY KEY (role_id, appuser_id)
);

create table ScheduleJobDef (
	id INT AUTO_INCREMENT PRIMARY KEY,
    appuser_id INT NOT NULL,
    name VARCHAR(256),
    type VARCHAR(16),
    interval_type VARCHAR(16),
    time_from VARCHAR(16),
    time_to VARCHAR(16),
    time_offset VARCHAR(8),
    active bit(1) DEFAULT 0 NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
	FOREIGN KEY (appuser_id) REFERENCES appuser(id)
);

create table WeekDay (
	id INT auto_increment primary key,
    name varchar(16),
    CONSTRAINT weekday_unique UNIQUE (name)
);

create table JobWeekDay (
    schedulejobdef_id int not null,
    weekday_id int not null,
    FOREIGN KEY (schedulejobdef_id) REFERENCES schedulejobdef(id),
    FOREIGN KEY (weekday_id) REFERENCES WeekDay(id),
    CONSTRAINT jobweekday_pk PRIMARY KEY (schedulejobdef_id, weekday_id)
);

create table Partner (
	id int auto_increment primary key,
    appuser_id int not null,
    name varchar(128),
    facebook_user varchar(128),
    foreign key (appuser_id) references appuser(id)
);

create table ScheduleJobPartner (
	schedulejob_id int not null,
    partner_id int not null,
    foreign key (schedulejob_id) references schedulejobdef(id),
    foreign key (partner_id) references partner(id)
);

create table ScheduleJobDetail (
	id int auto_increment primary key,
    schedulejobdef_id int not null,
    execution_time varchar(16) not null,
    status varchar(16) not null default 'NEW',
    foreign key (schedulejobdef_id) references schedulejobdef(id)
);



