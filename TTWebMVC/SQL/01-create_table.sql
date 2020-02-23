use ttweb;

drop table if exists ScheduleJobParameter, ScheduleJobParameterType, ScheduleJob, ScheduleJobType, AppUser;

CREATE TABLE AppUser (
	id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(64) UNIQUE NOT NULL,
    password VARCHAR(256) NOT NULL,
    firstname VARCHAR(64),
    lastname VARCHAR(64),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJobType (
	id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(16) UNIQUE NOT NULL,
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

create table ScheduleJob (
	id INT AUTO_INCREMENT PRIMARY KEY,
    schedulejobtype_id INT NOT NULL,
    appuser_id INT NOT NULL,
    name VARCHAR(16),
    FOREIGN KEY (schedulejobtype_id) REFERENCES schedulejobtype(id),
    FOREIGN KEY (appuser_id) REFERENCES appuser(id),
    create_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    update_date TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    process_date TIMESTAMP NULL DEFAULT NULL,
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




