async function displayTeachingStaffs() {
    data = await getAllStaffByTypeRequest("TeachingStaff");
    tableHeadings = ["id", "name", "subjectHandled"];
    staffFields=["id", "name", "subjectHandled"];
    generateTable(tableHeadings,staffFields,data);


}

function showAddTeachingStaffForm(){
    console.log("inside showAddTeachingStaffForm")

    emptyStaffForm();
    hideSecondaryStaffFormFields();
    document.getElementById("subjectHandledField").style.display = "";

    
}


function showUpdateTeachingStaffForm(){
    hideSecondaryStaffFormFields();
    document.getElementById("subjectHandledField").style.display = "";
    
}