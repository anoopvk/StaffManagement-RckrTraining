
CREATE procedure Proc_DeleteStaff 
	@id Int
As
	DELETE FROM AdminStaffDetails WHERE AdminStaffDetails.StaffId=@id;
	DELETE FROM SupportStaffDetails WHERE SupportStaffDetails.StaffId=@id;
	DECLARE @teachingStaffId INT;
	SET @teachingStaffId = (SELECT TeachingStaffDetails.Id FROM TeachingStaffDetails WHERE TeachingStaffDetails.StaffId=@id);
	DELETE FROM TeachingStaffToSubjectMapping WHERE TeachingStaffToSubjectMapping.TeachingStaffId=@teachingStaffId;
	DELETE FROM TeachingStaffDetails WHERE TeachingStaffDetails.StaffId=@id;
	DELETE FROM StaffDetails WHERE StaffDetails.Id=@id;