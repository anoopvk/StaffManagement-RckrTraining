
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
    // console.log(staff);
    // console.log(staff["id"]);
    // console.log(document.getElementById("idForDelete"));
    document.getElementById("idForDelete").value = staff["id"];
    document.getElementById("deleteConfirmationContainer").style.display = "block";
    // console.log("delete btn clicked", staff["id"])
    document.getElementById("yesBottonDeleteConfirmation").onclick = function () { deleteStaff(); }
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
    if (form["nameForUpdate"].value == "") {
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

async function showDeleteSelectedConfirmation() {
    document.getElementById("deleteConfirmationContainer").style.display = "block";
    document.getElementById("yesBottonDeleteConfirmation").onclick = function () { deleteSelected(); }
}

async function deleteSelected() {
    let tableRows = document.getElementById("myTable").rows;
    for (let index = 1; index < tableRows.length; index++) {
        let checkbox = tableRows[index].getElementsByTagName("input")[0]
        if (checkbox.checked == true) {
            response = await deleteStaffRequest(checkbox.value);
            console.log(response);
        }
    }
    closeDeleteStaffConfirmation();
    showStaffByTypeTable()
}

async function showDeleteAllConfirmation() {
    document.getElementById("deleteConfirmationContainer").style.display = "block";
    document.getElementById("yesBottonDeleteConfirmation").onclick = function () { deleteAll(); }
}

async function deleteAll() {
    data = await getAllStaffByTypeRequest("", false);
    for (const staff of data) {
        response = await deleteStaffRequest(staff["id"]);
        console.log(response);
    }

    closeDeleteStaffConfirmation();
    showStaffByTypeTable()
}


function resetPageDropDown(pagesRequired = 1) {
    let dropDown = document.getElementById("currentPage");
    currentPageSelection = dropDown.value;
    // console.log("currentPageSelection=", currentPageSelection)
    // console.log(dropDown.childNodes.length);
    dropDown.innerHTML = "";
    for (let index = 0; index < pagesRequired; index++) {
        let opt = document.createElement("option");
        opt.value = index + 1;
        opt.innerHTML = index + 1;
        if (index + 1 == currentPageSelection) {
            opt.selected = true;
        }

        dropDown.appendChild(opt);
    }
    // dropDown.selectedIndex=currentPageSelection-1;

}

function paginate(data) {

    rowsPerPage = document.getElementById("rowsPerPage").value;
    sizeOfData = data.length;
    pagesRequired = Math.ceil(sizeOfData / rowsPerPage);
    resetPageDropDown(pagesRequired);
    currentPage = document.getElementById("currentPage").value;
    start = (currentPage - 1) * rowsPerPage;
    end = currentPage * rowsPerPage;

    // console.log("pagesRequired=", pagesRequired);
    // console.log("start=", start);
    // console.log("end=", end);
    // console.log("sizeOfData=", sizeOfData);

    return data.slice(start, Math.min(sizeOfData, end));

}

function sortData(data) {
    // tableOrderBy
    // tableOrderAcsending
    // console.log(tableOrderBy, tableOrderAcsending);
    // data.sort((a, b) => ((a[tableOrderBy] == null) ? 1 : (a[tableOrderBy] > b[tableOrderBy]) ? 1 : -1));
    data.sort((a, b) => ((b[tableOrderBy] == null) ? 1 : (a[tableOrderBy] > b[tableOrderBy]) ? 1 : -1));
     
    if (!tableOrderAcsending) {
        data.reverse();

    }
    return data;
}






function generateTable(tableHeadings, staffFields, data) {
    console.log("generating table");
    // console.log(null < "asd")

    // tableHeadings = ["id", "name", "staffType", "section", "subjectHandled", "building"];

    // data = await getAllStaffByTypeRequest("");
    var table = document.getElementById("myTable");

    //reset table
    table.innerHTML = "";
    var headingRow = table.insertRow(0);

    //add headings 
    // let i = 0
    // tableHeadings.forEach(heading => {
    //     let cell = headingRow.insertCell(i)
    //     cell.innerHTML = heading;
    //     cell.onclick=function(){
    //         console.log("i=",i);
    //         tableOrderBy=staffFields[i];
    //         tableOrderAcsending=!tableOrderAcsending;
    //         console.log("---",tableOrderAcsending,tableOrderBy,i)
    //         showStaffByTypeTable();
    //     }
    //     i++;
    // });
    for (let index = 0; index < tableHeadings.length; index++) {
        let cell = headingRow.insertCell(index)
        cell.innerHTML = tableHeadings[index];
        cell.onclick = function () {
            console.log("index=", index);
            
            tableOrderBy = staffFields[index];
            tableOrderAcsending = !tableOrderAcsending;
            console.log("---*******", tableOrderAcsending, tableOrderBy, index)
            showStaffByTypeTable();
        }

    }

    let index = 0;
    data.forEach(staff => {
        var row = table.insertRow(index + 1);
        let i = 0;

        staffFields.forEach(field => {
            let cell = row.insertCell(i);
            cell.onclick = function () { showUpdateStaffForm(staff) };
            if (field == "staffType") {
                cell.innerHTML = staffTypes[staff[field]]
            }
            else {
                cell.innerHTML = staff[field];
            }
            i++;
        });

        let cellDeleteBtn = row.insertCell(staffFields.length);
        let deleteBtn = document.createElement("button")
        deleteBtn.innerHTML = "delete";
        deleteBtn.onclick = function () { showDeleteStaffConfirmation(staff) };
        cellDeleteBtn.appendChild(deleteBtn);

        let cellSelectStaffCheckbox = row.insertCell(staffFields.length + 1);
        let selectStaffCheckbox = document.createElement("input");
        selectStaffCheckbox.type = "checkbox";
        selectStaffCheckbox.className = "selectStaffCheckbox";
        selectStaffCheckbox.value = staff["id"];
        cellSelectStaffCheckbox.appendChild(selectStaffCheckbox);

        index++;
    });
}