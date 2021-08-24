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
    staffTypesDropdown = document.getElementById("staffType");
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

function generateTable(tableHeadings, staffFields, data) {
    console.log("generating table");
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

function emptyStaffForm(){
    document.getElementById("name").value = ""
    document.getElementById("section").value = "";
    document.getElementById("building").value = "";
    document.getElementById("subjectHandled").value = "";
}

function hideSecondaryStaffFormFields(){
    document.getElementById("sectionField").style.display = "none";
    document.getElementById("subjectHandledField").style.display = "none";
    document.getElementById("buildingField").style.display = "none";
}



function closeForm() {
    document.getElementById("StaffFormContainer").style.display = "none";

}

//add
function showAddStaffForm(){
    let staffTypeSelectedForAdd = document.getElementById("staffType").value;

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
    document.getElementById("staffType").style.display="";

    document.getElementById("formSubmitBtn").onclick=function(){addStaff()}
    document.getElementById("StaffFormContainer").style.display = "block";
}

async function addStaff() {
    const form = document.getElementById("staffForm");
    console.log("name from addStaff=", form["name"].value);
    var staffDetails = {};
    if (form["name"].value == "") {
        alert("name is required");
        return false;
    }
    staffDetails["name"] = form["name"].value;
    staffDetails["staffType"] = form["staffType"].value;
    staffDetails["section"] = form["section"].value;
    staffDetails["subjectHandled"] = form["subjectHandled"].value;
    staffDetails["building"] = form["building"].value;


    response = await postStaffRequest(JSON.stringify(staffDetails))

    closeForm();
    showStaffByTypeTable()
}

//update
function showUpdateStaffForm(staff) {
    document.getElementById("id").value = staff["id"];
    document.getElementById("staffTypeId").value = staff["staffTypeId"];
    document.getElementById("name").value = staff["name"];
    document.getElementById("section").value = staff["section"];
    document.getElementById("building").value = staff["building"];
    document.getElementById("subjectHandled").value = staff["subjectHandled"];
    
    if (staff["staffType"] == 1) {
        showUpdateAdministrativeStaffForm()
    }
    if (staff["staffType"] == 2) {
        showUpdateSupportStaffForm()
    }
    if (staff["staffType"] == 3) {
        showUpdateTeachingStaffForm()
    }
    document.getElementById("staffType").style.display="none";
    document.getElementById("formSubmitBtn").onclick=function(){updateStaff()}
    document.getElementById("StaffFormContainer").style.display = "block";
}

async function updateStaff() {
    const form = document.getElementById("staffForm");
    console.log(form["name"].value);
    if (form["name"].value == "") {
        alert("name is required");
        return false;
    }
    var staffDetails = {};
    staffDetails["name"] = form["name"].value;
    staffDetails["section"] = form["section"].value;
    staffDetails["subjectHandled"] = form["subjectHandled"].value;
    staffDetails["building"] = form["building"].value;
    console.log(staffDetails);
    
    response = await putStaffRequest(form["id"].value, JSON.stringify(staffDetails))
    console.log(response);
    
    closeForm()
    showStaffByTypeTable()
}

//delete
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

//pagination related
function resetPageDropDown(pagesRequired = 1) {
    let dropDown = document.getElementById("currentPage");
    currentPageSelection = dropDown.value;
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
    // let pageNumberSpan = document.getElementById("pageNumberContainer");
    // currentPageSelection = dropDown.value;
    // dropDown.innerHTML = "";
    // for (let index = 0; index < pagesRequired; index++) {
    //     let opt = document.createElement("option");
    //     opt.value = index + 1;
    //     opt.innerHTML = index + 1;
    //     if (index + 1 == currentPageSelection) {
    //         opt.selected = true;
    //     }

    //     dropDown.appendChild(opt);
    // }

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

//sort
function sortData(data) {
    // console.log(tableOrderBy, tableOrderAcsending);
    data.sort((a, b) => ((b[tableOrderBy] == null) ? 1 : (a[tableOrderBy] > b[tableOrderBy]) ? 1 : -1));
    if (!tableOrderAcsending) {
        data.reverse();
    }
    return data;
}

