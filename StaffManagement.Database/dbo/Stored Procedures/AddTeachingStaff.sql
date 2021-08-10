CREATE PROCEDURE [dbo].[AddTeachingStaff] 
	@name VARCHAR(255),
	@subjectHandled VARCHAR(255)
AS
	INSERT INTO StaffDetails(Name,StaffTypeId) Values (@name,(SELECT Id FROM StaffTypes WHERE StaffTypes.StaffType='TeachingStaff'));
	INSERT INTO TeachingStaffDetails(StaffId,SubjectHandled) VALUES (@@IDENTITY,@subjectHandled);