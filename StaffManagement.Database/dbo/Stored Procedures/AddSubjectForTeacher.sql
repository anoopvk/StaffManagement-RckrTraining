CREATE PROCEDURE AddSubjectForTeacher 
	@teachingStaffId INT,
	@subjectName VARCHAR(255)
AS
	DECLARE @Subject Table(
		NewSubjectId INT
	);
	INSERT INTO Subjects(SubjectName) OUTPUT inserted.Id INTO @Subject Values (@subjectName);
	INSERT INTO TeachingStaffToSubjectMapping(TeachingStaffId,SubjectId)  Values (@teachingStaffId , (SELECT NewSubjectId FROM @Subject));