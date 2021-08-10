CREATE PROCEDURE [dbo].[Proc_GetStaffWithId] 
	@id INT
AS
	Select StaffDetails.Id, StaffDetails.Name, StaffDetails.StaffTypeId , AdminStaffDetails.Section,TeachingStaffDetails.SubjectHandled,SupportStaffDetails.Building   
 from StaffDetails  
  full outer join AdminStaffDetails on StaffDetails.Id=AdminStaffDetails.StaffId  
   full outer join TeachingStaffDetails on StaffDetails.Id = TeachingStaffDetails.StaffId  
    full outer join SupportStaffDetails on StaffDetails.Id= SupportStaffDetails.StaffId
where StaffDetails.Id=@Id;