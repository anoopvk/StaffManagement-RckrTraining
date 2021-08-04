CREATE PROCEDURE AddTeachingStaff 
	@name VARCHAR(255)
AS
	DECLARE @Staff Table(
		StaffId INT
	);
	DECLARE @TeachingStaff Table(
		TeachingStaffId INT
	);
	DECLARE @MyId INT;
	INSERT INTO StaffDetails(Name,StaffTypeId) OUTPUT inserted.Id INTO @Staff Values (@name,(SELECT Id FROM StaffTypes WHERE StaffTypes.StaffType='TeachingStaff'));
	INSERT INTO TeachingStaffDetails(StaffId) OUTPUT inserted.Id INTO @TeachingStaff  VALUES ((SELECT StaffId FROM @Staff));
	SELECT TeachingStaffId FROM @TeachingStaff;