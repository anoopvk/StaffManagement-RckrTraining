--CREATE TYPE StaffDetailsBulk AS TABLE  
--(  
--	TempId INT,
--	Name VARCHAR(255),
--	StaffTypeId INT,
--	Section VARCHAR(255),
--	Building VARCHAR(255)
--  SubjectHandled VARCHAR(255)
--)


CREATE PROCEDURE AddStaffInBulk
	@StaffDetailsInBulk AS StaffDetailsBulk READONLY
AS
	Declare @InputToStaffDetailsMapping TABLE(
		StaffId INT,
		TempId INT
	);

	BEGIN
	MERGE StaffDetails AS Target
	USING @StaffDetailsInBulk AS Source
	ON 1=0
	When NOT MATCHED THEN 
		INSERT (Name,StaffTypeId)
		Values (Source.Name,Source.StaffTypeId)
		Output Inserted.Id,Source.TempId into @InputToStaffDetailsMapping;
	END;

	INSERT INTO AdminStaffDetails(Section,StaffId) SELECT Section,StaffId 
	From @StaffDetailsInBulk as inp inner join @InputToStaffDetailsMapping as map on inp.TempId=map.TempId
	Where StaffTypeId = 1;

	INSERT INTO SupportStaffDetails(Building,StaffId) SELECT Building,StaffId 
	From @StaffDetailsInBulk as inp inner join @InputToStaffDetailsMapping as map on inp.TempId=map.TempId
	Where StaffTypeId = 2;

	INSERT INTO TeachingStaffDetails(SubjectHandled,StaffId) SELECT SubjectHandled,StaffId
	From @StaffDetailsInBulk as inp inner join @InputToStaffDetailsMapping as map on inp.TempId=map.TempId
	Where StaffTypeId = 3;