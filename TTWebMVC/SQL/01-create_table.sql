use ttweb;

drop table if exists ScheduleJobParameter, ScheduleJobParameterType, ScheduleJob, ScheduleJobDef, AppUser;

CREATE TABLE AppUser (
	id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(64) UNIQUE NOT NULL,
    firstname VARCHAR(64),
    lastname VARCHAR(64),
    facebook_id VARCHAR(64),
	access_token VARCHAR(1024),
    access_token_expiration_date TIMESTAMP NULL DEFAULT NULL ,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJobDef (
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(16) UNIQUE NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJob (
	id INT AUTO_INCREMENT PRIMARY KEY,
    schedulejobdef_id INT NOT NULL,
    appuser_id INT NOT NULL,
    FOREIGN KEY (schedulejobdef_id) REFERENCES schedulejobdef(id),
    FOREIGN KEY (appuser_id) REFERENCES appuser(id),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
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
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);




