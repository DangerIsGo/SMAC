use db993ac12f90b045a99682a45d00246cf2;

create table Genders
(
	GenderType varchar(7) NOT NULL,
	primary key (GenderType)
);

create table Schools
(
	SchoolId int NOT NULL IDENTITY(1,1),
	SchoolName varchar(125) NOT NULL,
	StreetAddress varchar(250) NOT NULL,
	City varchar(100) NOT NULL,
	State varchar(50) NOT NULL,
	ZipCode varchar(10) NOT NULL,
	PhoneNumber varchar(20) NOT NULL,
	primary key (SchoolId)
);

create table Users
(
	UserId varchar(20) NOT NULL,
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	MiddleName varchar(50) NULL,
	EmailAddress varchar(100) NULL,
	PhoneNumber varchar(20) NULL,
	Gender varchar(7) NOT NULL,
	StartDate date NOT NULL,
	EndDate date NULL,
	IsActive bit NOT NULL,
	LastLoggedIn datetime NULL,
	LastLoggedOut datetime NULL,
	primary key (UserId),
	foreign key (Gender) references Genders (GenderType)
);

create table UserCredentials
(
	UserId varchar(20) NOT NULL,
	UserName varchar(100) NOT NULL,
	Password varchar(500) NOT NULL,
	primary key (UserId),
	foreign key (UserId) references Users (UserId)
		on delete cascade on update cascade,
	unique (UserName)
);

create table SchoolRoster
(
	UserId varchar(20) NOT NULL,
	SchoolId int NOT NULL,
	primary key (UserId, SchoolId),
	foreign key (UserId) references Users (UserId)
		on delete cascade on update cascade,
	foreign key (SchoolId) references Schools (SchoolId)
		on delete cascade on update cascade
);

create table Admins
(
	UserId varchar(20) NOT NULL,
	primary key (UserId),
	foreign key (UserId) references Users (UserId)
		on delete cascade on update cascade
);

create table Staff
(
	UserId varchar(20) NOT NULL,
	primary key (UserId),
	foreign key (UserId) references Users (UserId)
		ON DELETE CASCADE on update cascade
);

create table Teachers
(
	UserId varchar(20) NOT NULL,
	primary key (UserId),
	foreign key (UserId) references Users (UserId)
		ON DELETE CASCADE on update cascade
);

create table Students
(
	UserId varchar(20) NOT NULL,
	primary key (UserId),
	foreign key (UserId) references Users (UserId)
		ON DELETE CASCADE on update cascade
);

create table Subjects
(
	SubjectId int NOT NULL IDENTITY(1,1),
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	primary key (SubjectId, SchoolId),
	foreign key (SchoolId) references Schools (SchoolId)
		ON DELETE CASCADE on update cascade
);

create table Classes
(
	ClassId int NOT NULL IDENTITY(1,1),
	ClassName varchar(100) NOT NULL,
	SubjectId int NOT NULL,
	SchoolId int NOT NULL,
	Description varchar(250) NULL,
	primary key (ClassId, SubjectId, SchoolId),
	foreign key (SubjectId, SchoolId) 
		references Subjects (SubjectId, SchoolId)
		on update cascade,
	unique(ClassName, SubjectId, SchoolId)
);

create table Sections
(
	SectionId int NOT NULL IDENTITY(1,1),
	SectionName varchar(100) NOT NULL,
	ClassId int NOT NULL,
	SubjectId int NOT NULL,
	SchoolId int NOT NULL,
	Description varchar(250) NULL,
	primary key (SectionId, ClassId, SubjectId, SchoolId),
	foreign key (ClassId, SubjectId, SchoolId) 
		references Classes (ClassId, SubjectId, SchoolId)
		on update cascade,
	unique(SectionName, ClassId, SubjectId, SchoolId)
);

create table Clubs
(
	ClubId int NOT NULL IDENTITY(1,1),
	ClubName varchar(150) NOT NULL,
	SchoolId int NOT NULL,
	Description varchar(250) NULL,
	primary key (ClubId),
	foreign key (SchoolId) references Schools (SchoolId)
		on update cascade
);

create table Grades
(
	Grade varchar(3) NOT NULL,
	SchoolId int NOT NULL,
	primary key (Grade),
	foreign key (SchoolId) references Schools (SchoolId)
		on update cascade
		on delete cascade,
	unique (Grade, SchoolId)
);

create table Days
(
	Day varchar(10) NOT NULL,
	primary key (Day)
);

create table TimeSlots
(
	TimeSlotId int NOT NULL IDENTITY(1,1),
	StartTime time NOT NULL,
	EndTime time NOT NULL,
	SchoolId int NOT NULL,
	primary key (TimeSlotId),
	foreign key (SchoolId) references Schools (SchoolId)
		on update cascade
		on delete cascade,
	unique (TimeSlotId, SchoolId)
);

create table SchoolYears
(
	SchoolYearId int NOT NULL IDENTITY(1,1),
	SchoolId int NOT NULL,
	Year varchar(30) NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	primary key (SchoolYearId),
	foreign key (SchoolId) references Schools (SchoolId)
		on update cascade
		on delete cascade,
	unique (Year, SchoolId)
);

create table MarkingPeriods
(
	MarkingPeriodId int NOT NULL IDENTITY(1,1),
	SchoolYearId int NOT NULL,
	MarkingPeriod varchar(50) NULL,
	FullYear bit NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	primary key (MarkingPeriodId),
	foreign key (SchoolYearId) references SchoolYears (SchoolYearId),
	unique (MarkingPeriodId, SchoolYearId)
);

create table ClubSchedules
(
	ClubId int NOT NULL,
	TimeSlotId int NOT NULL,
	Day varchar(10) NOT NULL,
	SchoolYearId int NOT NULL,
	primary key (ClubId, TimeSlotId, Day),    
	foreign key (ClubId) 
		references Clubs (ClubId) 
		on delete cascade 
		on update cascade,
	foreign key (TimeSlotId) references TimeSlots (TimeSlotId),
	foreign key (Day) references Days (Day),
	foreign key (SchoolYearId) references SchoolYears (SchoolYearId)
);

create table ClubEnrollments
(
	ClubId int NOT NULL,
	UserId varchar(20) NOT NULL,
	IsLeader bit NOT NULL,
	primary key (ClubId, UserId),
	foreign key (ClubId) 
		references Clubs (ClubId) 
		on delete cascade 
		on update cascade,
	foreign key (UserId) references Users (UserId) 
		on delete cascade 
		on update cascade
);

create table SectionSchedules
(
	SectionId int NOT NULL,
	ClassId int NOT NULL,
	SubjectId int NOT NULL,
	SchoolId int NOT NULL,
	TimeSlotId int NOT NULL,
	Day varchar(10) NOT NULL,
	MarkingPeriodId int NOT NULL,
	primary key (SectionId, ClassId, SubjectId, SchoolId, TimeSlotId, Day, MarkingPeriodId),
	foreign key (SectionId, ClassId, SubjectId, SchoolId) 
		references Sections (SectionId, ClassId, SubjectId, SchoolId) 
		on delete cascade
		on update cascade,
	foreign key (TimeSlotId) references TimeSlots (TimeSlotId),
	foreign key (Day) references Days (Day),
	foreign key (MarkingPeriodId) references MarkingPeriods (MarkingPeriodId),
);

create table Enrollments
(
	UserId varchar(20) NOT NULL,
	SectionId int NOT NULL,
	ClassId int NOT NULL,
	SubjectId int NOT NULL,
	SchoolId int NOT NULL,
	MarkingPeriodId int NOT NULL,
	Grade varchar(3) NULL,
	primary key (UserId, SectionId, ClassId, SubjectId, SchoolId, MarkingPeriodId),
	foreign key (UserId) references Students (UserId)  
		on delete cascade 
		on update cascade,
	foreign key (SectionId, ClassId, SubjectId, SchoolId) 
		references Sections (SectionId, ClassId, SubjectId, SchoolId) 
		on update cascade,
	foreign key (MarkingPeriodId) references MarkingPeriods (MarkingPeriodId),
	foreign key (Grade) references Grades (Grade)
);

create table TeacherSchedules
(
	UserId varchar(20) NOT NULL,
	SectionId int NOT NULL,
	ClassId int NOT NULL,
	SubjectId int NOT NULL,
	SchoolId int NOT NULL,
	MarkingPeriodId int NOT NULL,
	primary key (UserId, SectionId, ClassId, SubjectId, SchoolId, MarkingPeriodId),
	foreign key (UserId) references Teachers (UserId)
		on delete cascade 
		on update cascade,
	foreign key (SectionId, ClassId, SubjectId, SchoolId) 
		references Sections (SectionId, ClassId, SubjectId, SchoolId) 
		on delete cascade
		on update cascade,
	foreign key (MarkingPeriodId) references MarkingPeriods (MarkingPeriodId)
);

create table PrivateMessages
(
	PrivateMessageId int NOT NULL IDENTITY(1,1),
	ToUser varchar(20) NOT NULL,
	FromUser varchar(20) NOT NULL,
	Content text NOT NULL,
	DateSent datetime NOT NULL,
	DateRead datetime NULL,
	primary key (PrivateMessageId),
	foreign key (ToUser) references Users (UserId),
	foreign key (FromUser) references Users (UserId)
);

create table KhanShares
(
	KhanShareId int NOT NULL IDENTITY(1,1),
	Url varchar(200) NOT NULL,
	ApiId varchar(50) NOT NULL,
	Title varchar(100) NOT NULL,
	primary key (KhanShareId)
);

create table Threads
(
	ThreadId int NOT NULL IDENTITY(1,1),
	UserId varchar(20) NULL,
	SectionId int NOT NULL,
	ClassId int NOT NULL,
	SubjectId int NOT NULL,
	SchoolId int NOT NULL,
	ThreadTitle varchar(250) NOT NULL,
	Content text NOT NULL,
	DateTimePosted datetime NOT NULL,
	RepliedTo int NULL,
	primary key (ThreadId),
	foreign key (UserId) references Users (UserId),
	foreign key (RepliedTo) references Threads (ThreadId),
	foreign key (SectionId, ClassId, SubjectId, SchoolId) 
		references Sections (SectionId, ClassId, SubjectId, SchoolId) 
		on delete cascade
		on update cascade,
);

create table KhanShareThreadMaps
(
	ThreadId int NOT NULL,
	KhanShareId int NOT NULL,
	primary key (ThreadId, KhanShareId),
	foreign key (ThreadId) references Threads (ThreadId)
		on delete cascade 
		on update cascade,
	foreign key (KhanShareId) references KhanShares (KhanShareId)
		on delete cascade 
		on update cascade
);

create table LatestNews
(
	LatestNewsId int NOT NULL IDENTITY(1,1),
	SchoolId int NOT NULL,
	Content text NOT NULL,
	UserId varchar(20) NOT NULL,
	PostedAt datetime NOT NULL,
	primary key (LatestNewsId),
	foreign key (SchoolId) references Schools (SchoolId)
		on delete cascade
		on update cascade,
	foreign key (UserId) references Users (UserId)
);


insert into Genders values ('Male');
insert into Genders values ('Female');

insert into Days values ('Monday');
insert into Days values ('Tuesday');
insert into Days values ('Wednesday');
insert into Days values ('Thursday');
insert into Days values ('Friday');
insert into Days values ('Saturday');
insert into Days values ('Sunday');

insert into Users values ('1', 'Devon', 	'Ostrowski',	NULL,	'Devon.Ostrowski@gmail.com',		'(912) 413-1154',	'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('2', 'Jessica',	'Liatys',		NULL,	NULL,					NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('3', 'Brandon',	'Liatys',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('4', 'Daniel',	'Gold',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('5', 'Gail',	'Francis',		'Anne',	NULL,					NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);

insert into Users values ('6', 'Steve',	'Bernier',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('7', 'Danielle',	'Student',		NULL,	'Danielle@yahoo.com',			NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('8', 'Frank',	'McLarty',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('9', 'Pamela',	'Focker',		'Martha',	'Pam.Martha@gmail.com',		NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('10', 'Tina',	'Patacki',		'Rose',	NULL,					NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);

insert into Users values ('11', 'Greg',	'Bendl',		NULL,	NULL,					NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('12', 'Amanda',	'Maldo',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('13', 'Gaby',	'Pao',		'Patricia',NULL,					'(785) 894-9802',		'Female', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('14', 'Danielle',	'Brock',		NULL,	NULL,					NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('15', 'Richard',	'Green',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);

insert into Users values ('16', 'Jonathan',	'Lipnicki',	NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('17', 'Yelena',	'Liw',		NULL,	NULL,					NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('18', 'Clark',	'Kent',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('19', 'Steve',	'Mason',		NULL,	NULL,					NULL,				'Male', '2015-03-20', NULL, 1, NULL, NULL);
insert into Users values ('20', 'Natasha',	'Romanov',		NULL,	NULL,					NULL,				'Female', '2015-03-20', NULL, 1, NULL, NULL);

insert into UserCredentials values ('1',	'devon',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('2',	'jessica',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('3',	'brandon',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('4',	'daniel',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('5',	'gail',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('6',	'steve',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('7',	'danielle',	'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('8',	'frank',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('9',	'pamela',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('10',	'tina',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('11',	'greg',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('12',	'amanda',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('13',	'gaby',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('14',	'danielle1',	'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('15',	'richard',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('16',	'jonathan',	'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('17',	'yelena',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('18',	'clark',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('19',	'steve1',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');
insert into UserCredentials values ('20',	'natasha',		'XohImNooBHFR0OVvjcYpJ3NgPQ1qq73WKhHvch0VQtg=');

insert into Admins values ('1');
insert into Teachers values ('2');
insert into Teachers values ('3');
insert into Teachers values ('4');
insert into Teachers values ('5');
insert into Teachers values ('6');
insert into Staff values ('7');
insert into Staff values ('8');
insert into Students values ('9');
insert into Students values ('10');
insert into Students values ('11');
insert into Students values ('12');
insert into Students values ('13');
insert into Students values ('14');
insert into Students values ('15');
insert into Students values ('16');
insert into Students values ('17');
insert into Students values ('18');
insert into Students values ('19');
insert into Students values ('20');

insert into Schools values ('Chatham Middle School',		'480 Main Street',			'Chatham',		'NJ', '07928', '(973) 457-2506');
insert into Schools values ('Jonas Salk Middle School',		'155 West Greystone Road',	'Old Bridge',	'NJ', '08857', '(732) 360-4523');
insert into Schools values ('St. John Vianney High School', '540A Line Road',			'Holmdel',		'NJ', '07733', '(732) 739-0800');
insert into Schools values ('Northeastern University',		'360 Huntington Ave',		'Boston',		'MA', '02115', '(617) 373-2000');

insert into LatestNews values (1,	'School cancelled today due to snow so get out there and enjoy the winter weather', '1',	'2015-03-10 06:50:32.000');
insert into LatestNews values (1,	'Delayed opening, classes start at 10:30',											'1',	'2015-03-11 08:10:22.000');
insert into LatestNews values (1,	'School cancelled today due to snow so get out there and enjoy the winter weather',	'1',	'2015-03-20 08:10:22.000');
insert into LatestNews values (1,	'Delayed opening, classes start at 10:30',											'1',	'2015-03-21 06:50:32.000');

insert into Subjects values ('Mathematics', 1);							--1
insert into Subjects values ('Science',		1);							--2
insert into Subjects values ('English',		1);							--3
insert into Subjects values ('History',		1);							--4
insert into Subjects values ('STEM',		1);							--5

insert into Classes values ('Calculus I', 1, 1, NULL);					--1
insert into Classes values ('Calculus II', 1, 1, NULL);					--2
insert into Classes values ('Calculus III', 1, 1, NULL);				--3
insert into Classes values ('Calculus IV', 1, 1, NULL);					--4

insert into Classes values ('Physics I', 2, 1, NULL);					--5
insert into Classes values ('Physics II', 2, 1, NULL);					--6
insert into Classes values ('Physics III', 2, 1, NULL);					--7
insert into Classes values ('Physics IV', 2, 1, NULL);					--8

insert into Classes values ('English I', 3, 1, NULL);					--9
insert into Classes values ('English II', 3, 1, NULL);					--10
insert into Classes values ('English III', 3, 1, NULL);					--11
insert into Classes values ('English IV', 3, 1, NULL);					--12

insert into Classes values ('History I', 4, 1, NULL);					--13
insert into Classes values ('History II', 4, 1, NULL);					--14
insert into Classes values ('History III', 4, 1, NULL);					--15
insert into Classes values ('Honors History IV', 4, 1, NULL);			--16

insert into Classes values ('STEM I', 5, 1, NULL);						--17
insert into Classes values ('STEM II', 5, 1, NULL);						--18
insert into Classes values ('STEM III', 5, 1, NULL);					--19
insert into Classes values ('STEM IV', 5, 1, NULL);						--20



insert into Sections values ('11010', 1, 1, 1, NULL);					--1
insert into Sections values ('25678', 1, 1, 1, NULL);					--2
insert into Sections values ('95145', 1, 1, 1, NULL);					--3

insert into Sections values ('89784', 2, 1, 1, NULL);					--4
insert into Sections values ('56231', 2, 1, 1, NULL);					--5
insert into Sections values ('78451', 2, 1, 1, NULL);					--6

insert into Sections values ('12341', 3, 1, 1, NULL);					--7
insert into Sections values ('32132', 3, 1, 1, NULL);					--8
insert into Sections values ('99784', 3, 1, 1, NULL);					--9

insert into Sections values ('55645', 4, 1, 1, NULL);					--10
insert into Sections values ('64543', 4, 1, 1, NULL);					--11
insert into Sections values ('51784', 4, 1, 1, NULL);					--12



insert into Sections values ('98653', 5, 2, 1, NULL);					--13
insert into Sections values ('56231', 5, 2, 1, NULL);					--14
insert into Sections values ('42421', 5, 2, 1, NULL);					--15

insert into Sections values ('54891', 6, 2, 1, NULL);					--16
insert into Sections values ('08794', 6, 2, 1, NULL);					--17
insert into Sections values ('11211', 6, 2, 1, NULL);					--18

insert into Sections values ('22541', 7, 2, 1, NULL);					--19
insert into Sections values ('32568', 7, 2, 1, NULL);					--20
insert into Sections values ('95159', 7, 2, 1, NULL);					--21

insert into Sections values ('12457', 8, 2, 1, NULL);					--22
insert into Sections values ('15951', 8, 2, 1, NULL);					--23
insert into Sections values ('35753', 8, 2, 1, NULL);					--24



insert into Sections values ('45654', 9, 3, 1, NULL);					--25
insert into Sections values ('78987', 9, 3, 1, NULL);					--26
insert into Sections values ('98789', 9, 3, 1, NULL);					--27

insert into Sections values ('55894', 10, 3, 1, NULL);					--28
insert into Sections values ('66897', 10, 3, 1, NULL);					--29
insert into Sections values ('77798', 10, 3, 1, NULL);					--30

insert into Sections values ('11154', 11, 3, 1, NULL);					--31
insert into Sections values ('66454', 11, 3, 1, NULL);					--32
insert into Sections values ('99998', 11, 3, 1, NULL);					--33

insert into Sections values ('11125', 12, 3, 1, NULL);					--34
insert into Sections values ('22225', 12, 3, 1, NULL);					--35
insert into Sections values ('00231', 12, 3, 1, NULL);					--36



insert into Sections values ('45611', 13, 4, 1, NULL);					--37
insert into Sections values ('45622', 13, 4, 1, NULL);					--38
insert into Sections values ('45633', 13, 4, 1, NULL);					--39

insert into Sections values ('65455', 14, 4, 1, NULL);					--40
insert into Sections values ('99870', 14, 4, 1, NULL);					--41
insert into Sections values ('23100', 14, 4, 1, NULL);					--42

insert into Sections values ('00011', 15, 4, 1, NULL);					--43
insert into Sections values ('77780', 15, 4, 1, NULL);					--44
insert into Sections values ('77781', 15, 4, 1, NULL);					--45

insert into Sections values ('22230', 16, 4, 1, NULL);					--46
insert into Sections values ('88890', 16, 4, 1, NULL);					--47
insert into Sections values ('77840', 16, 4, 1, NULL);					--48



insert into Sections values ('00998', 17, 5, 1, NULL);					--49
insert into Sections values ('00997', 17, 5, 1, NULL);					--50
insert into Sections values ('00996', 17, 5, 1, NULL);					--51

insert into Sections values ('00564', 18, 5, 1, NULL);					--52
insert into Sections values ('00001', 18, 5, 1, NULL);					--53
insert into Sections values ('00101', 18, 5, 1, NULL);					--54

insert into Sections values ('00111', 19, 5, 1, NULL);					--55
insert into Sections values ('11002', 19, 5, 1, NULL);					--56
insert into Sections values ('11980', 19, 5, 1, NULL);					--57

insert into Sections values ('32845', 20, 5, 1, NULL);					--58
insert into Sections values ('97840', 20, 5, 1, NULL);					--59
insert into Sections values ('08947', 20, 5, 1, NULL);					--60

insert into SchoolYears values (1, '2012 - 2013', '8-30-2012', '5-30-2013');
insert into SchoolYears values (1, '2013 - 2014', '8-31-2013', '5-31-2014');
insert into SchoolYears values (1, '2014 - 2015', '9-01-2014', '5-29-2015');

insert into MarkingPeriods values (1, '1', 0, '8-30-2012', '10-22-2012');
insert into MarkingPeriods values (1, '2', 0, '10-23-2012', '12-23-2012');
insert into MarkingPeriods values (1, '3', 0, '1-2-2013', '3-15-2013');
insert into MarkingPeriods values (1, '4', 0, '3-16-2013', '5-30-2013');
insert into MarkingPeriods values (1, NULL, 1, '8-30-2012', '5-30-2013');

insert into MarkingPeriods values (2, '1', 0, '8-30-2013', '10-22-2013');
insert into MarkingPeriods values (2, '2', 0, '10-23-2013', '12-23-2013');
insert into MarkingPeriods values (2, '3', 0, '1-2-2014', '3-15-2014');
insert into MarkingPeriods values (2, '4', 0, '3-16-2014', '5-30-2014');
insert into MarkingPeriods values (2, NULL, 1, '8-30-2013', '5-30-2014');

insert into MarkingPeriods values (3, '1', 0, '8-30-2014', '10-22-2014');
insert into MarkingPeriods values (3, '2', 0, '10-23-2014', '12-23-2014');
insert into MarkingPeriods values (3, '3', 0, '1-2-2015', '3-15-2015');
insert into MarkingPeriods values (3, '4', 0, '3-16-2015', '5-30-2015');
insert into MarkingPeriods values (3, NULL, 1, '8-30-2014', '5-30-2015');

--First Semester / 2012-2013
insert into Enrollments values ('9',		1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('9',		15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('9',		26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('9',		39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('9',		49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('10',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('10',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('10',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('10',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('10',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('11',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('11',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('11',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('11',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('11',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('12',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('12',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('12',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('12',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('12',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('13',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('13',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('13',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('13',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('13',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('14',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('14',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('14',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('14',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('14',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('15',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('15',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('15',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('15',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('15',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('16',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('16',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('16',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('16',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('16',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('17',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('17',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('17',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('17',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('17',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('18',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('18',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('18',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('18',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('18',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('19',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('19',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('19',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('19',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('19',	49,	17, 5,	1,	1,	NULL);

insert into Enrollments values ('20',	1,	1,	1,	1,	1,	NULL);
insert into Enrollments values ('20',	15,	5,	2,	1,	1,	NULL);
insert into Enrollments values ('20',	26,	9,	3,	1,	1,	NULL);
insert into Enrollments values ('20',	39,	13, 4,	1,	1,	NULL);
insert into Enrollments values ('20',	49,	17, 5,	1,	1,	NULL);

--Third Semester / 2012-2013
insert into Enrollments values ('9',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('9',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('9',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('9',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('9',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('10',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('10',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('10',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('10',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('10',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('11',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('11',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('11',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('11',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('11',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('12',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('12',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('12',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('12',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('12',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('13',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('13',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('13',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('13',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('13',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('14',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('14',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('14',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('14',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('14',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('15',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('15',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('15',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('15',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('15',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('16',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('16',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('16',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('16',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('16',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('17',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('17',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('17',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('17',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('17',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('18',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('18',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('18',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('18',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('18',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('19',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('19',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('19',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('19',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('19',	54,	18,	5,	1,	2, NULL);

insert into Enrollments values ('20',	4,	2,	1,	1,	2, NULL);
insert into Enrollments values ('20',	16,	6,	2,	1,	2, NULL);
insert into Enrollments values ('20',	30,	10,	3,	1,	2, NULL);
insert into Enrollments values ('20',	41,	14,	4,	1,	2, NULL);
insert into Enrollments values ('20',	54,	18,	5,	1,	2, NULL);

--First Semester / 2013-2014
insert into Enrollments values ('9',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('9',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('9',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('9',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('9',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('10',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('10',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('10',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('10',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('10',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('11',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('11',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('11',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('11',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('11',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('12',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('12',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('12',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('12',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('12',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('13',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('13',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('13',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('13',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('13',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('14',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('14',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('14',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('14',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('14',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('15',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('15',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('15',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('15',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('15',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('16',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('16',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('16',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('16',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('16',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('17',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('17',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('17',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('17',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('17',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('18',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('18',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('18',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('18',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('18',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('19',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('19',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('19',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('19',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('19',	57, 19,	5,	1,	3,	NULL);

insert into Enrollments values ('20',	7,	3,	1,	1,	3,	NULL);
insert into Enrollments values ('20',	19, 7,	2,	1,	3,	NULL);
insert into Enrollments values ('20',	33, 11,	3,	1,	3,	NULL);
insert into Enrollments values ('20',	43, 15,	4,	1,	3,	NULL);
insert into Enrollments values ('20',	57, 19,	5,	1,	3,	NULL);

--Third Semester / 2013-2014
insert into Enrollments values ('9',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('9',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('9',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('9',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('9',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('10',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('10',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('10',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('10',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('10',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('11',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('11',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('11',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('11',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('11',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('12',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('12',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('12',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('12',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('12',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('13',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('13',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('13',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('13',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('13',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('14',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('14',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('14',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('14',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('14',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('15',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('15',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('15',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('15',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('15',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('16',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('16',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('16',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('16',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('16',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('17',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('17',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('17',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('17',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('17',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('18',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('18',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('18',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('18',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('18',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('19',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('19',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('19',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('19',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('19',	60, 20,	5,	1,	4,	NULL);

insert into Enrollments values ('20',	11,	4,	1,	1,	4,	NULL);
insert into Enrollments values ('20',	23, 8,	2,	1,	4,	NULL);
insert into Enrollments values ('20',	36, 12,	3,	1,	4,	NULL);
insert into Enrollments values ('20',	47, 16,	4,	1,	4,	NULL);
insert into Enrollments values ('20',	60, 20,	5,	1,	4,	NULL);

--STEM
insert into TeacherSchedules values ('2', 49,	17, 5,	1,	1);
insert into TeacherSchedules values ('2', 54,	18,	5,	1,	2);
insert into TeacherSchedules values ('2', 57, 19,	5,	1,	3);
insert into TeacherSchedules values ('2', 60, 20,	5,	1,	4);

--Science
insert into TeacherSchedules values ('3', 15,	5,	2,	1,	1);
insert into TeacherSchedules values ('3', 16,	6,	2,	1,	2);
insert into TeacherSchedules values ('3', 19, 7,	2,	1,	3);
insert into TeacherSchedules values ('3', 23, 8,	2,	1,	4);

--Mathematics
insert into TeacherSchedules values ('4', 11,	4,	1,	1,	4);
insert into TeacherSchedules values ('4', 7,	3,	1,	1,	3);
insert into TeacherSchedules values ('4', 4,	2,	1,	1,	2);
insert into TeacherSchedules values ('4', 1,	1,	1,	1,	1);

--English
insert into TeacherSchedules values ('5', 26,	9,	3,	1,	1);
insert into TeacherSchedules values ('5', 30,	10,	3,	1,	2);
insert into TeacherSchedules values ('5', 33, 11,	3,	1,	3);
insert into TeacherSchedules values ('5', 36, 12,	3,	1,	4);

--History
insert into TeacherSchedules values ('6', 39,	13, 4,	1,	1);
insert into TeacherSchedules values ('6', 41,	14,	4,	1,	2);
insert into TeacherSchedules values ('6', 43, 15,	4,	1,	3);
insert into TeacherSchedules values ('6', 47, 16,	4,	1,	4);


insert into SchoolRoster values ('1', 1);
insert into SchoolRoster values ('2', 1);
insert into SchoolRoster values ('3', 1);
insert into SchoolRoster values ('4', 1);
insert into SchoolRoster values ('5', 1);
insert into SchoolRoster values ('6', 1);
insert into SchoolRoster values ('7', 1);
insert into SchoolRoster values ('8', 1);
insert into SchoolRoster values ('9', 1);
insert into SchoolRoster values ('10', 1);
insert into SchoolRoster values ('11', 1);
insert into SchoolRoster values ('12', 1);
insert into SchoolRoster values ('13', 1);
insert into SchoolRoster values ('14', 1);
insert into SchoolRoster values ('15', 1);
insert into SchoolRoster values ('16', 1);
insert into SchoolRoster values ('17', 1);
insert into SchoolRoster values ('18', 1);
insert into SchoolRoster values ('19', 1);
insert into SchoolRoster values ('20', 1);

insert into Clubs values ('Chess Club', 1, 'Come learn chess with us');
insert into Clubs values ('Math Club', 1, 'Add a little you, subtract your clothes, divide your legs and lets multiply');
insert into Clubs values ('Gaming Club', 1, 'Lets pwn sum n00bs!');
insert into Clubs values ('Physics Club', 1, 'It makes the world work');
insert into Clubs values ('Birdwatching Club', 1, 'We watch birds all day');

insert into ClubEnrollments values (1, 1, 0);
insert into ClubEnrollments values (2, 1, 0);
insert into ClubEnrollments values (3, 1, 0);

insert into TimeSlots values ('16:00', '17:00', 1);
insert into TimeSlots values ('17:00', '18:00', 1);
insert into TimeSlots values ('12:00', '13:00', 1);

insert into ClubSchedules values (1, 1, 'Monday', 1);
insert into ClubSchedules values (2, 2, 'Wednesday', 1);
insert into ClubSchedules values (3, 3, 'Friday', 1);

insert into Grades values ('A+', 1);
insert into Grades values ('A', 1);
insert into Grades values ('A-', 1);
insert into Grades values ('B+', 1);
insert into Grades values ('B', 1);
insert into Grades values ('B-', 1);
insert into Grades values ('C+', 1);
insert into Grades values ('C', 1);
insert into Grades values ('C-', 1);
insert into Grades values ('D', 1);
insert into Grades values ('F', 1);