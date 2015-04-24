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
	StartDate datetime NOT NULL,
	EndDate datetime NULL,
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
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	primary key (SubjectName, SchoolId),
	foreign key (SchoolId) references Schools (SchoolId)
		ON DELETE CASCADE on update cascade
);

create table Classes
(
	ClassName varchar(100) NOT NULL,
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	Description varchar(250) NULL,
	primary key (ClassName, SubjectName, SchoolId),
	foreign key (SubjectName, SchoolId) 
		references Subjects (SubjectName, SchoolId)
		on update cascade
);

create table Sections
(
	SectionName varchar(100) NOT NULL,
	ClassName varchar(100) NOT NULL,
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	Description varchar(250) NULL,
	primary key (SectionName, ClassName, SubjectName, SchoolId),
	foreign key (ClassName, SubjectName, SchoolId) 
		references Classes (ClassName, SubjectName, SchoolId)
		on update cascade
);

create table Clubs
(
	ClubName varchar(150) NOT NULL,
	SchoolId int NOT NULL,
	Description varchar(250) NULL,
	primary key (ClubName, SchoolId),
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
	StartTime datetime NOT NULL,
	EndTime datetime NOT NULL,
	SchoolId int NOT NULL,
	primary key (TimeSlotId),
	foreign key (SchoolId) references Schools (SchoolId)
		on update cascade
		on delete cascade,
	unique (TimeSlotId, SchoolId)
);

create table SchoolYears
(
	Year varchar(20) NOT NULL,
	SchoolId int NOT NULL,
	primary key (Year),
	foreign key (SchoolId) references Schools (SchoolId)
		on update cascade
		on delete cascade,
	unique (Year, SchoolId)
);

create table MarkingPeriods
(
	MarkingPeriodId int NOT NULL IDENTITY(1,1),
	SchoolId int NOT NULL,
	MarkingPeriod varchar(50) NULL,
	FullYear bit NOT NULL,
	primary key (MarkingPeriodId),
	foreign key (SchoolId) references Schools (SchoolId)
		on update cascade
		on delete cascade,
	unique (MarkingPeriodId, SchoolId)
);

create table ClubSchedules
(
	ClubName varchar(150) NOT NULL,
	SchoolId int NOT NULL,
	TimeSlotId int NOT NULL,
	Day varchar(10) NOT NULL,
	primary key (ClubName, SchoolId, TimeSlotId, Day),    
	foreign key (ClubName, SchoolId) 
		references Clubs (ClubName, SchoolId) 
		on delete cascade 
		on update cascade,
	foreign key (TimeSlotId) references TimeSlots (TimeSlotId),
	foreign key (Day) references Days (Day)
);

create table ClubEnrollments
(
	ClubName varchar(150) NOT NULL,
	SchoolId int NOT NULL,
	UserId varchar(20) NOT NULL,
	IsLeader bit NOT NULL,
	primary key (ClubName, SchoolId, UserId),
	foreign key (ClubName, SchoolId) 
		references Clubs (ClubName, SchoolId) 
		on delete cascade 
		on update cascade,
	foreign key (UserId) references Users (UserId) 
		on delete cascade 
		on update cascade
);

create table SectionSchedules
(
	SectionName varchar(100) NOT NULL,
	ClassName varchar(100) NOT NULL,
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	TimeSlotId int NOT NULL,
	Day varchar(10) NOT NULL,
	primary key (SectionName, ClassName, SubjectName, SchoolId, TimeSlotId, Day),
	foreign key (SectionName, ClassName, SubjectName, SchoolId) 
		references Sections (SectionName, ClassName, SubjectName, SchoolId) 
		on delete cascade
		on update cascade,
	foreign key (TimeSlotId) references TimeSlots (TimeSlotId),
	foreign key (Day) references Days (Day)
);

create table Enrollments
(
	UserId varchar(20) NOT NULL,
	SectionName varchar(100) NOT NULL,
	ClassName varchar(100) NOT NULL,
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	MarkingPeriodId int NOT NULL,
	SchoolYear varchar(20) NOT NULL,
	Grade varchar(3) NULL,
	primary key (UserId, SectionName, ClassName, SubjectName, SchoolId, MarkingPeriodId, SchoolYear),
	foreign key (UserId) references Students (UserId)  
		on delete cascade 
		on update cascade,
	foreign key (SectionName, ClassName, SubjectName, SchoolId) 
		references Sections (SectionName, ClassName, SubjectName, SchoolId) 
		on update cascade,
	foreign key (MarkingPeriodId) references MarkingPeriods (MarkingPeriodId),
	foreign key (SchoolYear) references SchoolYears (Year),
	foreign key (Grade) references Grades (Grade)
);

create table TeacherSchedules
(
	UserId varchar(20) NOT NULL,
	SectionName varchar(100) NOT NULL,
	ClassName varchar(100) NOT NULL,
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	MarkingPeriodId int NOT NULL,
	SchoolYear varchar(20) NOT NULL,
	primary key (UserId, SectionName, ClassName, SubjectName, SchoolId, MarkingPeriodId, SchoolYear),
	foreign key (UserId) references Teachers (UserId)
		on delete cascade 
		on update cascade,
	foreign key (SectionName, ClassName, SubjectName, SchoolId) 
		references Sections (SectionName, ClassName, SubjectName, SchoolId) 
		on delete cascade
		on update cascade,
	foreign key (MarkingPeriodId) references MarkingPeriods (MarkingPeriodId),
	foreign key (SchoolYear) references SchoolYears (Year)
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
	SectionName varchar(100) NOT NULL,
	ClassName varchar(100) NOT NULL,
	SubjectName varchar(50) NOT NULL,
	SchoolId int NOT NULL,
	ThreadTitle varchar(250) NOT NULL,
	Content text NOT NULL,
	DateTimePosted datetime NOT NULL,
	RepliedTo int NULL,
	primary key (ThreadId),
	foreign key (UserId) references Users (UserId),
	foreign key (RepliedTo) references Threads (ThreadId),
	foreign key (SectionName, ClassName, SubjectName, SchoolId) 
		references Sections (SectionName, ClassName, SubjectName, SchoolId) 
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

insert into Users values ('1', 'Devon', 'Ostrowski', NULL, 'DangerIsGo@gmail.com', '(978) 464-1114', 'Male', '2015-03-20 00:00:00.000', NULL, 1, NULL, NULL);
insert into UserCredentials values ('1', 'dangerisgo', 'RTWN2Fh/LeJ47HQRpWC654cgwTqqNp0M2VtncVrbRFw=');
insert into Students values ('1');

insert into Schools values ('Chatham Middle School', '480 Main Street', 'Chatham', 'NJ', '07928', '(973) 457-2506');
insert into Schools values ('Jonas Salk Middle School', '155 West Greystone Road', 'Old Bridge', 'NJ', '08857', '(732) 360-4523');
insert into Schools values ('St. John Vianney High School', '540A Line Road', 'Holmdel', 'NJ', '07733', '(732) 739-0800');
insert into Schools values ('Northeastern University', '360 Huntington Ave', 'Boston', 'MA', '02115', ' (617) 373-2000');

insert into SchoolRoster values ('1', 1);
insert into SchoolRoster values ('1', 4);
insert into SchoolRoster values ('1', 3);

insert into LatestNews values (1, 'School cancelled today due to snow so get out there and enjoy the winter weather', '1', '2015-03-10 06:50:32.000');
insert into LatestNews values (1,	'Delayed opening, classes start at 10:30',	'1',	'2015-03-11 08:10:22.000');
insert into LatestNews values (1,	'School cancelled today due to snow so get out there and enjoy the winter weather',	'1',	'2015-03-20 08:10:22.000');
insert into LatestNews values (1,	'Delayed opening, classes start at 10:30',	'1',	'2015-03-21 06:50:32.000');

insert into Subjects values ('Mathematics', 1);
insert into Subjects values ('Science', 1);
insert into Subjects values ('English', 1);
insert into Subjects values ('History', 1);
insert into Subjects values ('STEM', 1);

insert into Classes values ('Honors Algebra II', 'Mathematics', 1, NULL);
insert into Classes values ('Trigonometry', 'Mathematics', 1, NULL);
insert into Classes values ('Geometry', 'Mathematics', 1, NULL);
insert into Classes values ('Calculus I', 'Mathematics', 1, NULL);

insert into Classes values ('Chemistry I', 'Science', 1, NULL);
insert into Classes values ('Physics I', 'Science', 1, NULL);
insert into Classes values ('Biology II', 'Science', 1, NULL);
insert into Classes values ('Anatomy IV', 'Science', 1, NULL);

insert into Classes values ('English I', 'English', 1, NULL);
insert into Classes values ('English II', 'English', 1, NULL);
insert into Classes values ('English III', 'English', 1, NULL);
insert into Classes values ('English IV', 'English', 1, NULL);

insert into Classes values ('History I', 'History', 1, NULL);
insert into Classes values ('History II', 'History', 1, NULL);
insert into Classes values ('History III', 'History', 1, NULL);
insert into Classes values ('Honors History IV', 'History', 1, NULL);

insert into Classes values ('STEM I', 'STEM', 1, NULL);
insert into Classes values ('STEM II', 'STEM', 1, NULL);
insert into Classes values ('STEM III', 'STEM', 1, NULL);
insert into Classes values ('STEM IV', 'STEM', 1, NULL);



insert into Sections values ('11010', 'Honors Algebra II', 'Mathematics', 1, NULL);
insert into Sections values ('25678', 'Honors Algebra II', 'Mathematics', 1, NULL);
insert into Sections values ('95145', 'Honors Algebra II', 'Mathematics', 1, NULL);

insert into Sections values ('89784', 'Trigonometry', 'Mathematics', 1, NULL);
insert into Sections values ('56231', 'Trigonometry', 'Mathematics', 1, NULL);
insert into Sections values ('78451', 'Trigonometry', 'Mathematics', 1, NULL);

insert into Sections values ('12341', 'Geometry', 'Mathematics', 1, NULL);
insert into Sections values ('32132', 'Geometry', 'Mathematics', 1, NULL);
insert into Sections values ('99784', 'Geometry', 'Mathematics', 1, NULL);

insert into Sections values ('55645', 'Calculus I', 'Mathematics', 1, NULL);
insert into Sections values ('64543', 'Calculus I', 'Mathematics', 1, NULL);
insert into Sections values ('51784', 'Calculus I', 'Mathematics', 1, NULL);



insert into Sections values ('98653', 'Chemistry I', 'Science', 1, NULL);
insert into Sections values ('56231', 'Chemistry I', 'Science', 1, NULL);
insert into Sections values ('42421', 'Chemistry I', 'Science', 1, NULL);

insert into Sections values ('54891', 'Physics I', 'Science', 1, NULL);
insert into Sections values ('08794', 'Physics I', 'Science', 1, NULL);
insert into Sections values ('11211', 'Physics I', 'Science', 1, NULL);

insert into Sections values ('22541', 'Biology II', 'Science', 1, NULL);
insert into Sections values ('32568', 'Biology II', 'Science', 1, NULL);
insert into Sections values ('95159', 'Biology II', 'Science', 1, NULL);

insert into Sections values ('12457', 'Anatomy IV', 'Science', 1, NULL);
insert into Sections values ('15951', 'Anatomy IV', 'Science', 1, NULL);
insert into Sections values ('35753', 'Anatomy IV', 'Science', 1, NULL);



insert into Sections values ('45654', 'English I', 'English', 1, NULL);
insert into Sections values ('78987', 'English I', 'English', 1, NULL);
insert into Sections values ('98789', 'English I', 'English', 1, NULL);

insert into Sections values ('55894', 'English II', 'English', 1, NULL);
insert into Sections values ('66897', 'English II', 'English', 1, NULL);
insert into Sections values ('77798', 'English II', 'English', 1, NULL);

insert into Sections values ('11154', 'English III', 'English', 1, NULL);
insert into Sections values ('66454', 'English III', 'English', 1, NULL);
insert into Sections values ('99998', 'English III', 'English', 1, NULL);

insert into Sections values ('11125', 'English IV', 'English', 1, NULL);
insert into Sections values ('22225', 'English IV', 'English', 1, NULL);
insert into Sections values ('00231', 'English IV', 'English', 1, NULL);



insert into Sections values ('45611', 'History I', 'History', 1, NULL);
insert into Sections values ('45622', 'History I', 'History', 1, NULL);
insert into Sections values ('45633', 'History I', 'History', 1, NULL);

insert into Sections values ('65455', 'History II', 'History', 1, NULL);
insert into Sections values ('99870', 'History II', 'History', 1, NULL);
insert into Sections values ('23100', 'History II', 'History', 1, NULL);

insert into Sections values ('00011', 'History III', 'History', 1, NULL);
insert into Sections values ('77780', 'History III', 'History', 1, NULL);
insert into Sections values ('77781', 'History III', 'History', 1, NULL);

insert into Sections values ('22230', 'Honors History IV', 'History', 1, NULL);
insert into Sections values ('88890', 'Honors History IV', 'History', 1, NULL);
insert into Sections values ('77840', 'Honors History IV', 'History', 1, NULL);



insert into Sections values ('00998', 'STEM I', 'STEM', 1, NULL);
insert into Sections values ('00997', 'STEM I', 'STEM', 1, NULL);
insert into Sections values ('00996', 'STEM I', 'STEM', 1, NULL);

insert into Sections values ('00564', 'STEM II', 'STEM', 1, NULL);
insert into Sections values ('00001', 'STEM II', 'STEM', 1, NULL);
insert into Sections values ('00101', 'STEM II', 'STEM', 1, NULL);

insert into Sections values ('00111', 'STEM III', 'STEM', 1, NULL);
insert into Sections values ('11002', 'STEM III', 'STEM', 1, NULL);
insert into Sections values ('11980', 'STEM III', 'STEM', 1, NULL);

insert into Sections values ('32845', 'STEM IV', 'STEM', 1, NULL);
insert into Sections values ('97840', 'STEM IV', 'STEM', 1, NULL);
insert into Sections values ('08947', 'STEM IV', 'STEM', 1, NULL);

insert into SchoolYears values ('2012 - 2013', 1);
insert into SchoolYears values ('2013 - 2014', 1);
insert into SchoolYears values ('2014 - 2015', 1);

insert into MarkingPeriods values (1, '1', 0);
insert into MarkingPeriods values (1, '2', 0);
insert into MarkingPeriods values (1, '3', 0);
insert into MarkingPeriods values (1, '4', 0);
insert into MarkingPeriods values (1, NULL, 1);

--First Semester / 2012-2013
insert into Enrollments values ('1', '00998', 'STEM I', 'STEM', 1, 1, '2012 - 2013', NULL);
insert into Enrollments values ('1', '45611', 'History I', 'History', 1, 1, '2012 - 2013', NULL);
insert into Enrollments values ('1', '45654', 'English I', 'English', 1, 1, '2012 - 2013', NULL);
insert into Enrollments values ('1', '54891', 'Physics I', 'Science', 1, 1, '2012 - 2013', NULL);
insert into Enrollments values ('1', '89784', 'Trigonometry', 'Mathematics', 1, 1, '2012 - 2013', NULL);

--Third Semester / 2012-2013
insert into Enrollments values ('1', '00001', 'STEM II', 'STEM', 1, 3, '2012 - 2013', NULL);
insert into Enrollments values ('1', '23100', 'History II', 'History', 1, 3, '2012 - 2013', NULL);
insert into Enrollments values ('1', '55894', 'English II', 'English', 1, 3, '2012 - 2013', NULL);
insert into Enrollments values ('1', '98653', 'Chemistry I', 'Science', 1, 3, '2012 - 2013', NULL);
insert into Enrollments values ('1', '12457', 'Anatomy IV', 'Science', 1, 3, '2012 - 2013', NULL);

--First Semester / 2013-2014
insert into Enrollments values ('1', '00111', 'STEM III', 'STEM', 1, 1, '2013 - 2014', NULL);
insert into Enrollments values ('1', '77780', 'History III', 'History', 1, 1, '2013 - 2014', NULL);
insert into Enrollments values ('1', '11154', 'English III', 'English', 1, 1, '2013 - 2014', NULL);
insert into Enrollments values ('1', '32568', 'Biology II', 'Science', 1, 1, '2013 - 2014', NULL);
insert into Enrollments values ('1', '89784', 'Trigonometry', 'Mathematics', 1, 1, '2013 - 2014', NULL);

--Third Semester / 2013-2014
insert into Enrollments values ('1', '97840', 'STEM IV', 'STEM', 1, 3, '2013 - 2014', NULL);
insert into Enrollments values ('1', '88890', 'Honors History IV', 'History', 1, 3, '2013 - 2014', NULL);
insert into Enrollments values ('1', '11125', 'English IV', 'English', 1, 3, '2013 - 2014', NULL);
insert into Enrollments values ('1', '51784', 'Calculus I', 'Mathematics', 1, 3, '2013 - 2014', NULL);
insert into Enrollments values ('1', '95145', 'Honors Algebra II', 'Mathematics', 1, 3, '2013 - 2014', NULL);