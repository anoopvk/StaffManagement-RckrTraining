function showAddStaffForm() {
    showAddStaffFormFields()
    document.getElementById("addStaffContainer").style.display = "block";
}

function showAddStaffFormFields() {
    document.getElementById("nameForAdd").value = ""
    document.getElementById("sectionForAdd").value = "";
    document.getElementById("buildingForAdd").value = "";
    document.getElementById("subjectHandledForAdd").value = "";

    document.getElementById("sectionFieldForAdd").style.display = "none";
    document.getElementById("subjectHandledFieldForAdd").style.display = "none";
    document.getElementById("buildingFieldForAdd").style.display = "none";

    let staffTypeSelectedForAdd = document.getElementById("staffTypeForAdd").value;

    if (staffTypes[staffTypeSelectedForAdd] == "AdministrativeStaff") {
        document.getElementById("sectionFieldForAdd").style.display = "";
    }
    else if (staffTypes[staffTypeSelectedForAdd] == "SupportStaff") {
        document.getElementById("subjectHandledFieldForAdd").style.display = "";

    }
    else if (staffTypes[staffTypeSelectedForAdd] == "TeachingStaff") {
        document.getElementById("buildingFieldForAdd").style.display = "";

    }
    else {
        console.error("value for dropdown selection not found!");
    }
}
function closeAddForm() {
    document.getElementById("addStaffContainer").style.display = "none";

}

async function addStaff() {
    const form = document.getElementById("addForm");
    console.log("name from addStaff=", form["nameForAdd"].value);
    var staffDetails = {};
    if (form["nameForAdd"].value== "") {
        alert("name is required");
        return false;
    }
    staffDetails["name"] = form["nameForAdd"].value;
    staffDetails["staffType"] = form["staffTypeForAdd"].value;
    staffDetails["section"] = form["sectionForAdd"].value;
    staffDetails["subjectHandled"] = form["subjectHandledForAdd"].value;
    staffDetails["building"] = form["buildingForAdd"].value;


    response = await postStaffRequest(JSON.stringify(staffDetails))

    closeAddForm()
    resetStaffTable()
}