async function displayTeachingStaffs() {
    data = await getAllStaffByTypeRequest("TeachingStaff");
    tableHeadings = ["id", "name", "subjectHandled"];
    staffFields=["id", "name", "subjectHandled"];
    generateTable(tableHeadings,staffFields,data);


}

function showAddTeachingStaffForm(){
    console.log("inside showAddTeachingStaffForm")

    document.getElementById("nameForAdd").value = ""
    document.getElementById("sectionForAdd").value = "";
    document.getElementById("buildingForAdd").value = "";
    document.getElementById("subjectHandledForAdd").value = "";

    document.getElementById("sectionFieldForAdd").style.display = "none";
    document.getElementById("subjectHandledFieldForAdd").style.display = "";
    document.getElementById("buildingFieldForAdd").style.display = "none";

    
}


function showUpdateTeachingStaffForm(){
    document.getElementById("sectionFieldForUpdate").style.display = "none";
    document.getElementById("subjectHandledFieldForUpdate").style.display = "";
    document.getElementById("buildingFieldForUpdate").style.display = "none";

}