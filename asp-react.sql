create table dbo.Student(
Student_Id int identity(1,1),
Student_Name nvarchar(100),
Student_Dateofbirth date,
Student_Email nvarchar(100),
Student_Age int,
Class_Id nvarchar(100),
)


select * from dbo.Student;
insert into dbo.Student values('Muizz','1995/10/04','muizz@email.com',26,1);
delete from dbo.Student where Student_Id = 2;

create table dbo.Class(
Class_Id int identity(1,1),
Class_Name nvarchar(100),
Class_year nvarchar(100),
)

select * from dbo.Class;
insert into dbo.Class values('Science','11C');