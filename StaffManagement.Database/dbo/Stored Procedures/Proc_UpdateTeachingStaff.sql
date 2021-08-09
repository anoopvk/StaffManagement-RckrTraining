--CREATE TYPE SubjectTypeTable AS TABLE  
--(  
--   Subjects VARCHAR(255)
--)  


CREATE PROCEDURE Proc_UpdateTeachingStaff
	@id INT,
	@name VARCHAR(255),
	@subjects AS SubjectTypeTable READONLY
AS 
	UPDATE StaffDetails SET Name=@name WHERE StaffDetails.Id=@id;
	DELETE FROM TeachingStaffToSubjectMapping WHERE TeachingStaffToSubjectMapping.TeachingStaffId= (SELECT TeachingStaffDetails.Id FROM TeachingStaffDetails WHERE TeachingStaffDetails.StaffId=@id);
	Declare @maxSubjectId INT;
	SET @maxSubjectId = (Select MAX(Id) from Subjects);
	Insert into Subjects(SubjectName) Select * from @subjects;
	Insert into TeachingStaffToSubjectMapping(SubjectId,TeachingStaffId) (select Subjects.Id, (SELECT TeachingStaffDetails.Id FROM TeachingStaffDetails WHERE TeachingStaffDetails.StaffId=@id) from Subjects where Subjects.Id>@maxSubjectId);

--select * from TeachingStaffDetails where TeachingStaffDetails full outer join teaching

--Declare @subjects SubjectTypeTable;
--Insert into @subjects(Subjects) Values ('aaaaa');
--Insert into @subjects(Subjects) Values ('bbbbb');
--Insert into @subjects(Subjects) Values ('ccccc');

--Declare @subjects2 SubjectTypeTable;
--Insert into @subjects2(Subjects) Values ('ddddd');
--Insert into @subjects2(Subjects) Values ('eeeee');
--Insert into @subjects2(Subjects) Values ('fffff');


--select * from @subjects;
--select * from @subjects2;
--Insert into @subjects2(Subjects) select * from @subjects;
--select * from @subjects2;

--EXEC Proc_UpdateTeachingStaff  26, testingteach2, @subjects;