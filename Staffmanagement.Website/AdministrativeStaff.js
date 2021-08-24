async function displayAdministrativeStaffs() {
    data = await getAllStaffByTypeRequest("AdministrativeStaff");
    tableHeadings = ["id", "name", "section"];
    staffFields=["id", "name", "section"];
    generateTable(tableHeadings,staffFields,data);

}

function showAddAdministrativeStaffForm(){
    console.log("inside showAddAdministrativeStaffForm")
    document.getElementById("nameForAdd").value = ""
    document.getElementById("sectionForAdd").value = "";
    document.getElementById("buildingForAdd").value = "";
    document.getElementById("subjectHandledForAdd").value = "";

    document.getElementById("sectionFieldForAdd").style.display = "";
    document.getElementById("subjectHandledFieldForAdd").style.display = "none";
    document.getElementById("buildingFieldForAdd").style.display = "none";

    
}

function showUpdateAdministrativeStaffForm(){
    document.getElementById("sectionFieldForUpdate").style.display = "";
    document.getElementById("subjectHandledFieldForUpdate").style.display = "none";
    document.getElementById("buildingFieldForUpdate").style.display = "none";
}
