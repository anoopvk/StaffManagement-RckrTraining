CREATE PROCEDURE Proc_GetAllStaff
AS
	Select StaffDetails.Id, StaffDetails.Name, StaffDetails.StaffTypeId , AdminStaffDetails.Section,TeachingStaffDetails.Id as TeachingStaffId,Subjects.SubjectName,SupportStaffDetails.Building 
	from StaffDetails
		full outer join AdminStaffDetails on StaffDetails.Id=AdminStaffDetails.StaffId
			full outer join TeachingStaffDetails on StaffDetails.Id = TeachingStaffDetails.StaffId
				full outer join SupportStaffDetails on StaffDetails.Id= SupportStaffDetails.StaffId
					full outer join TeachingStaffToSubjectMapping on TeachingStaffDetails.Id=TeachingStaffToSubjectMapping.TeachingStaffId
						full outer join Subjects on TeachingStaffToSubjectMapping.SubjectId = Subjects.Id;