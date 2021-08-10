CREATE PROCEDURE [dbo].[AddAdminStaff] 
	@name VARCHAR(255),
	@section VARCHAR(255)
AS
	INSERT INTO StaffDetails(Name,StaffTypeId) Values (@name,(SELECT Id FROM StaffTypes WHERE StaffTypes.StaffType='AdministrativeStaff'));
	INSERT INTO AdminStaffDetails(StaffId,Section) VALUES (@@IDENTITY , @section);