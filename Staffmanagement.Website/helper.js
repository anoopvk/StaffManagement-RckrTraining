
function populateSelectStaffTypeDropdownForDisplay() {
    staffTypesDropdown = document.getElementById("selectStaffTypeToDisplay");
    for (let index = 1; index <= Object.keys(staffTypes).length; index++) {
        let option = document.createElement("option");
        option.text = staffTypes[index];
        option.value = staffTypes[index];

        staffTypesDropdown.add(option);
    }
}

function populateSelectStaffTypeDropdownForAdd() {
    staffTypesDropdown = document.getElementById("staffTypeForAdd");
    for (let index = 1; index <= Object.keys(staffTypes).length; index++) {
        let option = document.createElement("option");
        option.text = staffTypes[index];
        option.value = index;

        staffTypesDropdown.add(option);
    }
}

async function showStaffByTypeTable() {
    selectedType = document.getElementById("selectStaffTypeToDisplay").value;

    if (selectedType == "AdministrativeStaff") {
        displayAdministrativeStaffs()
    }
    else if (selectedType == "SupportStaff") {
        displaySupportStaffs()
    }
    else if (selectedType == "TeachingStaff") {
        displayTeachingStaffs()
    }
    else {
        displayAllStaffs();
    }

}

function showAddStaffForm() {
    let staffTypeSelectedForAdd = document.getElementById("staffTypeForAdd").value;

    if (staffTypes[staffTypeSelectedForAdd] == "AdministrativeStaff") {
        showAddAdministrativeStaffForm()
    }
    else if (staffTypes[staffTypeSelectedForAdd] == "SupportStaff") {
        showAddSupportStaffForm()
    }
    else if (staffTypes[staffTypeSelectedForAdd] == "TeachingStaff") {
        showAddTeachingStaffForm()
    }
    else {
        console.error("value for dropdown selection not found!");
    }
    document.getElementById("addStaffContainer").style.display = "block";
}

function closeAddForm() {
    document.getElementById("addStaffContainer").style.display = "none";

}

async function addStaff() {
    const form = document.getElementById("addForm");
    console.log("name from addStaff=", form["nameForAdd"].value);
    var staffDetails = {};
    if (form["nameForAdd"].value == "") {
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
    showStaffByTypeTable()
}

function showDeleteStaffConfirmation(staff) {
    // event.stopPropagation();
    console.log(staff);
    console.log(staff["id"]);
    console.log(document.getElementById("idForDelete"));
    document.getElementById("idForDelete").value = staff["id"];
    document.getElementById("deleteConfirmationContainer").style.display = "block";
    console.log("delete btn clicked", staff["id"])
}

function closeDeleteStaffConfirmation() {
    document.getElementById("deleteConfirmationContainer").style.display = "none";

}

async function deleteStaff() {
    let id = document.getElementById("idForDelete").value
    document.getElementById("idForDelete").value = "";
    response = await deleteStaffRequest(id);
    console.log(response);
    closeDeleteStaffConfirmation();
    showStaffByTypeTable()
}


function showUpdateStaffForm(staff) {
    document.getElementById("idForUpdate").value = staff["id"];
    document.getElementById("staffTypeIdForUpdate").value = staff["staffTypeId"];
    document.getElementById("nameForUpdate").value = staff["name"];
    document.getElementById("sectionForUpdate").value = staff["section"];
    document.getElementById("buildingForUpdate").value = staff["building"];
    document.getElementById("subjectHandledForUpdate").value = staff["subjectHandled"];

    if (staff["staffType"] == 1) {
        showUpdateAdministrativeStaffForm()
    }
    if (staff["staffType"] == 2) {
        showUpdateSupportStaffForm()
    }
    if (staff["staffType"] == 3) {
        showUpdateTeachingStaffForm()
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
    showStaffByTypeTable()
}
