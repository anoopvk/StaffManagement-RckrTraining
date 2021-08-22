async function displayAdministrativeStaffs() {
    data = await getAllStaffByTypeRequest("AdministrativeStaff");
    var table = document.getElementById("myTable");

    //reset table
    table.innerHTML = "";
    var headingRow = table.insertRow(0);

    //add headings 
    tableHeadings = ["id", "name", "section"];
    for (let i = 0; i < tableHeadings.length; i++) {
        var cell = headingRow.insertCell(i)
        cell.innerHTML = tableHeadings[i];
    }

    for (let index = 0; index < data.length; index++) {
        const staff = data[index];
        var row = table.insertRow(index + 1);

        let cell0 = row.insertCell(0);
        let cell1 = row.insertCell(1);
        let cell2 = row.insertCell(2);
        cell0.onclick = function () { showUpdateStaffForm(staff) };
        cell1.onclick = function () { showUpdateStaffForm(staff) };
        cell2.onclick = function () { showUpdateStaffForm(staff) };
        cell0.innerHTML = staff["id"];
        cell1.innerHTML = staff["name"];
        cell2.innerHTML = staff["section"];

        let cellDeleteBtn = row.insertCell(3);
        let deleteBtn = document.createElement("button")
        deleteBtn.innerHTML = "delete";
        deleteBtn.onclick = function () { showDeleteStaffConfirmation(staff) };
        cellDeleteBtn.appendChild(deleteBtn);

    }
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
