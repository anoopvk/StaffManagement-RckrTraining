
function showUpdateStaffForm(staff) {
    document.getElementById("idForUpdate").value = staff["id"];
    document.getElementById("staffTypeIdForUpdate").value = staff["staffTypeId"];
    document.getElementById("nameForUpdate").value = staff["name"];

    document.getElementById("sectionFieldForUpdate").style.display = "none";
    document.getElementById("buildingFieldForUpdate").style.display = "none";
    document.getElementById("subjectHandledFieldForUpdate").style.display = "none";

    document.getElementById("sectionForUpdate").value = staff["section"];
    document.getElementById("buildingForUpdate").value = staff["building"];
    document.getElementById("subjectHandledForUpdate").value = staff["subjectHandled"];

    if (staff["staffType"] == 1) {
        document.getElementById("sectionFieldForUpdate").style.display = "";
    }
    if (staff["staffType"] == 2) {
        document.getElementById("buildingFieldForUpdate").style.display = "";
    }
    if (staff["staffType"] == 3) {
        document.getElementById("subjectHandledFieldForUpdate").style.display = "";
    }

    document.getElementById("updateStaffContainer").style.display = "block";
}



function closeUpdateForm() {
    document.getElementById("updateStaffContainer").style.display = "none";
}

async function updateStaff() {
    const form = document.getElementById("updateForm");
    console.log(form["nameForUpdate"].value);
    if (form["nameForUpdate"].value==""){
        alert("name is required");
        return false;
    }
    var staffDetails = {};
    staffDetails["name"] = form["nameForUpdate"].value;
    staffDetails["section"] = form["sectionForUpdate"].value;
    staffDetails["subjectHandled"] = form["subjectHandledForUpdate"].value;
    staffDetails["building"] = form["buildingForUpdate"].value;
    console.log(staffDetails);

    response = await putStaffRequest(form["idForUpdate"].value, JSON.stringify(staffDetails))
    console.log(response);

    closeUpdateForm()
    resetStaffTable()
}
