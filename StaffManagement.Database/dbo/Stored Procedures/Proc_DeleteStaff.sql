
CREATE procedure [dbo].[Proc_DeleteStaff] 
	@id Int
As
	DELETE FROM AdminStaffDetails WHERE AdminStaffDetails.StaffId=@id;
	DELETE FROM SupportStaffDetails WHERE SupportStaffDetails.StaffId=@id;
	DELETE FROM TeachingStaffDetails WHERE TeachingStaffDetails.StaffId=@id;
	DELETE FROM StaffDetails WHERE StaffDetails.Id=@id;