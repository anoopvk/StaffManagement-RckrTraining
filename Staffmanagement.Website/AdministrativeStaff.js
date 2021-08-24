async function displayAdministrativeStaffs() {
    data = await getAllStaffByTypeRequest("AdministrativeStaff");
    tableHeadings = ["id", "name", "section"];
    staffFields=["id", "name", "section"];
    generateTable(tableHeadings,staffFields,data);

}

function showAddAdministrativeStaffForm(){
    console.log("inside showAddAdministrativeStaffForm")
    emptyStaffForm();
    hideSecondaryStaffFormFields();
    document.getElementById("sectionField").style.display = "";
    
}

function showUpdateAdministrativeStaffForm(){
    hideSecondaryStaffFormFields();
    document.getElementById("sectionField").style.display = "";
}
