CREATE PROCEDURE Proc_UpdateSupportStaff
	@id INT,
	@name VARCHAR(255),
	@building VARCHAR(255)
AS
	UPDATE StaffDetails SET Name=@name WHERE Id=@id;
	UPDATE SupportStaffDetails SET Building=@building WHERE StaffId=@id;