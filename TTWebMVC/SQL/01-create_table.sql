use ttweb;

drop table if exists ScheduleJobParameter, ScheduleJobParameterType, ScheduleJob, ScheduleJobStatus, ScheduleJobType, AppUser, LoginUser_LoginUserRole, LoginUser, LoginUserRole;

create table LoginUserRole (
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(16) UNIQUE NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE LoginUser (
	id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(64) UNIQUE NOT NULL,
    password VARCHAR(256) NOT NULL,
    title VARCHAR(64),
    firstname VARCHAR(64),
    lastname VARCHAR(64),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE LoginUser_LoginUserRole (
	id INT AUTO_INCREMENT PRIMARY KEY,
    loginuser_id INT NOT NULL,
    loginuserrole_id INT NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (loginuser_id) REFERENCES loginuser(id),
    FOREIGN KEY (loginuserrole_id) REFERENCES loginuserrole(id),
    CONSTRAINT loginuser_loginuserrole_unique UNIQUE (loginuser_id, loginuserrole_id)
);

CREATE TABLE AppUser (
	id INT AUTO_INCREMENT PRIMARY KEY,
    loginuser_id INT NOT NULL,
    email VARCHAR(64) UNIQUE NOT NULL,
    password VARCHAR(256) NOT NULL,
    title VARCHAR(64),
    firstname VARCHAR(64),
    lastname VARCHAR(64),
    FOREIGN KEY (loginuser_id) REFERENCES loginuser(id),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJobType (
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(16) UNIQUE NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJobStatus (
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(32) UNIQUE NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJob (
	id INT AUTO_INCREMENT PRIMARY KEY,
    schedulejobtype_id INT NOT NULL,
    schedulejobstatus_id INT NOT NULL,
    appuser_id INT NOT NULL,
    name VARCHAR(16),
    FOREIGN KEY (schedulejobtype_id) REFERENCES schedulejobtype(id),
    FOREIGN KEY (schedulejobstatus_id) REFERENCES schedulejobstatus(id),
    FOREIGN KEY (appuser_id) REFERENCES appuser(id),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    planned_date TIMESTAMP NULL DEFAULT NULL,
    CONSTRAINT appuser_type_unique UNIQUE (schedulejobtype_id, appuser_id)
);

create table ScheduleJobParameterType (
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(16) UNIQUE NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJobParameter (
	id INT AUTO_INCREMENT PRIMARY KEY,
	schedulejob_id INT NOT NULL,
    schedulejobparametertype_id INT NOT NULL,
    value VARCHAR(64),
    FOREIGN KEY (schedulejob_id) REFERENCES schedulejob(id),
    FOREIGN KEY (schedulejobparametertype_id) REFERENCES schedulejobparametertype(id),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT job_paramter_unique UNIQUE (schedulejob_id, schedulejobparametertype_id)
);




