async function displaySupportStaffs() {
    data = await getAllStaffByTypeRequest("SupportStaff");
    tableHeadings = ["id", "name", "building"];
    staffFields=["id", "name", "building"];
    generateTable(tableHeadings,staffFields,data);

}

function showAddSupportStaffForm(){
    console.log("inside showAddSupportStaffForm")

    document.getElementById("nameForAdd").value = ""
    document.getElementById("sectionForAdd").value = "";
    document.getElementById("buildingForAdd").value = "";
    document.getElementById("subjectHandledForAdd").value = "";

    document.getElementById("sectionFieldForAdd").style.display = "none";
    document.getElementById("subjectHandledFieldForAdd").style.display = "none";
    document.getElementById("buildingFieldForAdd").style.display = "";

    
}



function showUpdateSupportStaffForm(){
    document.getElementById("sectionFieldForUpdate").style.display = "none";
    document.getElementById("subjectHandledFieldForUpdate").style.display = "none";
    document.getElementById("buildingFieldForUpdate").style.display = "";
}