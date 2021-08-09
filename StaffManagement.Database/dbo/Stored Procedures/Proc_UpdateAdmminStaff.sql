CREATE PROCEDURE Proc_UpdateAdmminStaff
	@id INT,
	@name VARCHAR(255),
	@section VARCHAR(255)
AS
	UPDATE StaffDetails SET Name=@name WHERE Id=@id;
	UPDATE AdminStaffDetails SET Section=@section WHERE StaffId=@id;