async function displaySupportStaffs() {
    data = await getAllStaffByTypeRequest("SupportStaff");
    tableHeadings = ["id", "name", "building"];
    staffFields=["id", "name", "building"];
    generateTable(tableHeadings,staffFields,data);

}

function showAddSupportStaffForm(){
    console.log("inside showAddSupportStaffForm")
    emptyStaffForm();
    hideSecondaryStaffFormFields();
    document.getElementById("buildingField").style.display = "";
}



function showUpdateSupportStaffForm(){
    hideSecondaryStaffFormFields();
    document.getElementById("buildingField").style.display = "";
}