async function displayAllStaffs() {
    data = await getAllStaffByTypeRequest("");
    var table = document.getElementById("myTable");

    //reset table
    table.innerHTML = "";
    var headingRow = table.insertRow(0);

    //add headings 
    tableHeadings = ["id", "name", "staffType", "section", "subjectHandled", "building"];

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
        let cell3 = row.insertCell(3);
        let cell4 = row.insertCell(4);
        let cell5 = row.insertCell(5);
        cell0.onclick = function () { showUpdateStaffForm(staff) };
        cell1.onclick = function () { showUpdateStaffForm(staff) };
        cell2.onclick = function () { showUpdateStaffForm(staff) };
        cell3.onclick = function () { showUpdateStaffForm(staff) };
        cell4.onclick = function () { showUpdateStaffForm(staff) };
        cell5.onclick = function () { showUpdateStaffForm(staff) };
        cell0.innerHTML = staff["id"];
        cell1.innerHTML = staff["name"];
        cell2.innerHTML = staffTypes[staff["staffType"]];
        cell3.innerHTML = staff["section"];
        cell4.innerHTML = staff["subjectHandled"];
        cell5.innerHTML = staff["building"];


        let cellDeleteBtn = row.insertCell(6);
        let deleteBtn = document.createElement("button")
        deleteBtn.innerHTML = "delete";
        deleteBtn.onclick = function () { showDeleteStaffConfirmation(staff) };
        cellDeleteBtn.appendChild(deleteBtn);

    }
}