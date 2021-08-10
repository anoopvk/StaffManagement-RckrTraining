CREATE PROCEDURE [dbo].[AddSupportStaff] 
	@name VARCHAR(255),
	@building VARCHAR(255)
AS
	INSERT INTO StaffDetails(Name,StaffTypeId) Values (@name,(SELECT Id FROM StaffTypes WHERE StaffTypes.StaffType='SupportStaff'));
	INSERT INTO SupportStaffDetails(StaffId,Building) VALUES (@@IDENTITY , @building);