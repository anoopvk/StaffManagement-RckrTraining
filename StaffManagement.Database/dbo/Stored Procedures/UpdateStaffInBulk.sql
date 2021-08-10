--CREATE TYPE StaffDetailsBulk AS TABLE  
--(  
--	TempId INT,
--	Name VARCHAR(255),
--	StaffTypeId INT,
--	Section VARCHAR(255),
--	Building VARCHAR(255)
--  SubjectHandled VARCHAR(255)
--)


CREATE PROCEDURE UpdateStaffInBulk
	@StaffDetailsInBulk AS StaffDetailsBulk READONLY
AS
	Declare @InputToStaffDetailsMapping TABLE(
		StaffId INT,
		TempId INT
	);

	BEGIN
	MERGE StaffDetails AS Target
	USING @StaffDetailsInBulk AS Source
	ON Target.Id = Source.TempId
	When MATCHED THEN UPDATE SET
		Target.Name=Source.Name;
	END;

	BEGIN
	MERGE AdminStaffDetails AS Target
	USING @StaffDetailsInBulk AS Source
	ON Target.StaffId = Source.TempId
	When MATCHED THEN UPDATE SET
		Target.Section=Source.Section;
	END;

	BEGIN
	MERGE SupportStaffDetails AS Target
	USING @StaffDetailsInBulk AS Source
	ON Target.StaffId = Source.TempId
	When MATCHED THEN UPDATE SET
		Target.Building=Source.Building;
	END;

	BEGIN
	MERGE TeachingStaffDetails AS Target
	USING @StaffDetailsInBulk AS Source
	ON Target.StaffId = Source.TempId
	When MATCHED THEN UPDATE SET
		Target.SubjectHandled=Source.SubjectHandled;
	END;