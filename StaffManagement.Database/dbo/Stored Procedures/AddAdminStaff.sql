CREATE PROCEDURE AddAdminStaff 
	@name VARCHAR(255),
	@section VARCHAR(255)
AS
	DECLARE @Staff Table(
		StaffId INT
	);
	INSERT INTO StaffDetails(Name,StaffTypeId) OUTPUT inserted.Id INTO @Staff Values (@name,(SELECT Id FROM StaffTypes WHERE StaffTypes.StaffType='AdministrativeStaff'));
	INSERT INTO AdminStaffDetails(StaffId,Section) VALUES ((SELECT StaffId FROM @Staff) , @section);