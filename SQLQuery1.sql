USE MASTER
GO

IF db_id('db_Students') is not null
DROP DATABASE db_Students	
GO

CREATE DATABASE db_Students
GO

USE db_Students
GO


CREATE TABLE Roles (
	Id int identity NOT NULL primary key,
	Label nvarchar(50) NOT NULL,
	Description nvarchar(50) NOT NULL
)
GO

CREATE TABLE People (
	Id int identity NOT NULL primary key,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	BirthDate date NOT NULL,
	Roles int NOT NULL
		references Roles(Id)
)
GO

CREATE TABLE CurricularUnits (
	Id int identity NOT NULL primary key,
	Name nvarchar(50) NOT NULL,
	Objectives nvarchar(100) NOT NULL
)
GO

CREATE TABLE ClassDetails (
	Id int identity NOT NULL primary key,
	Name nvarchar(50) NOT NULL,
	Year int NOT NULL,
	CurricularUnit int NOT NULL
		references CurricularUnits(Id),
	Teacher int NOT NULL
		references People(Id)
)
GO

CREATE TABLE Class (
	Id int NOT NULL
		references ClassDetails(Id),
	Student int NOT NULL 
		references People(Id)
)
GO

INSERT INTO Roles 
	VALUES
		('Student','Person to graduate'),
		('Teacher','Person who teaches')
GO

INSERT INTO People 
	VALUES
		('Diogo','Silva', '1999-06-01', 1),
		('Diogo','Olival', '1985-08-18', 2),
		('Sarah', 'Miller', '1997-06-18', 2),   
		('David', 'Brown', '1985-01-30', 1),   
		('Linda', 'Wilson', '1993-12-04', 2),  
		('Mark', 'Taylor', '1988-07-14', 1),  
		('Karen', 'Anderson', '1981-09-08', 2), 
		('James', 'Clark', '1996-02-25', 1),   
		('Jennifer', 'Davis', '1987-03-21', 2), 
		('Kevin', 'White', '1994-05-12', 1),   
		('Emily', 'Moore', '1983-10-02', 2),   
		('Richard', 'Wilson', '1992-11-07', 1),
		('Maria', 'Lee', '1986-04-26', 2),    
		('Daniel', 'Harris', '1999-08-09', 1), 
		('Susan', 'Garcia', '1990-07-31', 2),
		('Thomas', 'Roberts', '1984-05-19', 1), 
		('Patricia', 'Hall', '1991-12-14', 2), 
		('William', 'Martinez', '1982-06-11', 1), 
		('Nancy', 'Young', '1989-09-28', 2),    
		('Edward', 'Scott', '1998-03-05', 1), 
		('Margaret', 'Lopez', '1985-01-15', 2);
GO

INSERT INTO CurricularUnits 
	VALUES
		('Web Programming', 'Programming dynamic pages on WEB Servers and Implementation of WEB Programming using MVC'),
		('Database Management', 'Managing and designing relational databases, including SQL queries and database administration'),
		('Software Engineering', 'Software development methodologies, project management, and quality assurance'),
		('Data Structures and Algorithms', 'Fundamental data structures, algorithms, and their analysis'),
		('Computer Networks', 'Networking protocols, configuration, and network security'),
		('Artificial Intelligence', 'Machine learning, natural language processing, and AI applications'),
		('Operating Systems', 'Design, implementation, and management of computer operating systems'),
		('Computer Graphics', 'Graphics programming, rendering, and computer-generated imagery'),
		('Cybersecurity', 'Cyber threats, security measures, and ethical hacking techniques'),
		('Data Science', 'Data analysis, machine learning, and big data techniques')
GO

INSERT INTO ClassDetails
	VALUES
		('TPSI', 2022, 1, 2)
GO

INSERT INTO Class
	VALUES
		(1, 2)
GO