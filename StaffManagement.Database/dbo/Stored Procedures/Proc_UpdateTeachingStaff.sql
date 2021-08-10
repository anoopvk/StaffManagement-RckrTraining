

CREATE PROCEDURE [dbo].[Proc_UpdateTeachingStaff]
	@id INT,
	@name VARCHAR(255),
	@subjectHandled VARCHAR(255)
AS 
	UPDATE StaffDetails SET Name=@name WHERE StaffDetails.Id=@id;
	UPDATE TeachingStaffDetails SET SubjectHandled=@subjectHandled WHERE StaffId=@id;