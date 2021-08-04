CREATE PROCEDURE AddSupportStaff 
	@name VARCHAR(255),
	@building VARCHAR(255)
AS
	DECLARE @Staff Table(
		StaffId INT
	);
	INSERT INTO StaffDetails(Name,StaffTypeId) OUTPUT inserted.Id INTO @Staff Values (@name,(SELECT Id FROM StaffTypes WHERE StaffTypes.StaffType='SupportStaff'));
	INSERT INTO SupportStaffDetails(StaffId,Building) VALUES ((SELECT StaffId FROM @Staff) , @building);