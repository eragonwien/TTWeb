use ttweb;

drop table if exists UserGroupMapping, UserGroup, Partner, AppUser;

create table AppUser (
	id int auto_increment primary key,
    email varchar(64) unique not null,
    firstname varchar(64),
    lastname varchar(64),
    facebook_id varchar(64),
	access_token varchar(1024),
    access_token_expiration_date date,
    create_date date not null,
    update_date date
);

create table Partner (
	id int auto_increment primary key,
    email varchar(64) unique not null,
    firstname varchar(64),
    lastname varchar(64),
    facebook_id varchar(64),
    create_date date not null,
    update_date date
);

create table UserGroup (
	id int auto_increment primary key,
    appuser_id int not null,
    title varchar(16),
    create_date date not null,
    update_date date,
    foreign key (appuser_id) references AppUser(id)
);

create table UserGroupMapping (
	id int auto_increment primary key,
	usergroup_id int not null,
    partner_id int not null,
    create_date date not null,
    update_date date,
	foreign key (usergroup_id) references UserGroup(id),
    foreign key (partner_id) references Partner(id),
    constraint Unique_usergroup_partner unique (usergroup_id, partner_id)
);

