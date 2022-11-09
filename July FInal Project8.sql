Create database JulyFinalProject8
use JulyFinalProject8

CREATE TABLE TblUser
(
	UserID int primary key  NOT NULL,
	Username varchar(50) NULL,
	UserPass varchar(50) NULL,
	UserType varchar(50) NULL
);
insert into TblUser(UserID,Username,UserPass,UserType) values ('1','Admin','1234','SuperAdmin');

CREATE TABLE TblUserRole
(
	UserRoleID int primary key NOT NULL,
	UserID int references TblUser(UserID),
	PageName varchar(50) NULL,
	IsCreate bit NULL,
	IsRead bit NULL,
	IsUpdate bit NULL,
	IsDelete bit NULL
);

CREATE TABLE Traineertblvm
(
    TraineerId   INT   Primary key  IDENTITY (1, 1) NOT NULL,
    TraineerName VARCHAR (50)   NOT NULL,
    Address     VARCHAR (50)   NOT NULL,
    Email       VARCHAR (50)   NOT NULL,
    Contact     VARCHAR (50)   NOT NULL,
    TraineerDOB  DATETIME       NOT NULL,
    ImageName   NVARCHAR (600) NULL,
    ImageUrl    NVARCHAR (600) NULL,    
);

CREATE TABLE Area
(
    AreaId   INT     Primary key   IDENTITY (1, 1) NOT NULL,
    AreaName NVARCHAR (50) NULL,
);

CREATE TABLE Candidate
(
    CandidateId   INT  Primary key IDENTITY (1, 1) NOT NULL,
    CandidateName NVARCHAR (50)  NULL,
    Salary  DECIMAL (18, 2) NOT NULL,
    ImagePath   NVARCHAR (600)  NULL,
    Applydate  DATETIME NULL,
    Target    INT   NOT NULL,
    AreaId  INT  NOT NULL,    
);

CREATE TABLE Depertment
(
    DepertmentId   INT Primary key  IDENTITY (1, 1) NOT NULL,
    DepertmentName NVARCHAR (50) NULL,
);

Create table Designation
(
DesignationId int primary key IDENTITY(1,1),
DesignationName NVARCHAR (50) NULL,
);

Create table Employee
(
	EmployeeId   INT  Primary key IDENTITY (1, 1) NOT NULL,
    EmployeeName NVARCHAR (50)  NULL,
    ImagePath   NVARCHAR (600)  NULL,
	Email varchar(50),
	Age int NOT NULL,
    JoinningDate  DATETIME NOT NULL,
    Gender VARCHAR(50) Not Null,
    DepertmentId  INT  NOT NULL References Depertment(DepertmentId),
	DesignationId  INT  NOT NULL References Designation(DesignationId),
);

CREATE TABLE [dbo].[supplier]
(
	[supplier_id] [int] IDENTITY(1,1) primary key NOT NULL,
	[supplier_name] [NVARCHAR](MAX) NULL,
	[mobile] [decimal](18, 0) NULL,
	[address] [varchar](50) NULL,
	[JoinDate] [date] NULL
);

CREATE TABLE [dbo].[multiProduct] (
    [ProductId]   INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [ProductName] NVARCHAR (MAX)  NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [ImagePath]   NVARCHAR (MAX)  NULL,
    [SupplyDate]  DATETIME  NULL,
    [Quantity]    INT NOT NULL,
    [supplier_id] [int] REFERENCES [supplier] ([supplier_id])
);

