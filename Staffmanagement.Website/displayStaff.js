// function showAllStaffTable(data) {


//     console.log(data);

//     var table = document.getElementById("myTable");
//     table.innerHTML = "";

//     var headingRow = table.insertRow(0);
//     tableHeadings = ["id", "name", "staffType", "section", "subjectHandled", "building"]
//     tableRowCells = [];
//     for (let i = 0; i < tableHeadings.length; i++) {
//         var cell = headingRow.insertCell(i)
//         cell.innerHTML = tableHeadings[i];
//         tableRowCells.push(cell);
//     }

//     for (let index = 0; index < data.length; index++) {
//         var row = table.insertRow(index + 1);
//         const staff = data[index];
//         tableRowCells = [];

//         for (let i = 0; i < tableHeadings.length; i++) {
//             let cell = row.insertCell(i);
//             cell.onclick = function () { showUpdateStaffForm(staff) };
//             tableRowCells.push(cell);
//         }

//         tableRowCells[0].innerHTML = staff["id"];
//         tableRowCells[1].innerHTML = staff["name"];
//         tableRowCells[2].innerHTML = staffTypes[staff["staffType"]];
//         tableRowCells[3].innerHTML = staff["section"];
//         tableRowCells[4].innerHTML = staff["subjectHandled"];
//         tableRowCells[5].innerHTML = staff["building"];


//         let cellDeleteBtn = row.insertCell(tableHeadings.length);
//         let deleteBtn = document.createElement("button")
//         deleteBtn.innerHTML = "delete";
//         deleteBtn.onclick = function () { showDeleteStaffConfirmation(staff) };
//         cellDeleteBtn.appendChild(deleteBtn);
//     }
// }


function showStaffTable(selectedType, data) {

    console.log("inside showStaffTable");
    console.log("selectedtype=",selectedType);
    console.log("data=",data);
    console.log("-----------");



    var table = document.getElementById("myTable");
    table.innerHTML = "";

    var headingRow = table.insertRow(0);
    if (selectedType == staffTypes[1]) {
        tableHeadings = ["id", "name", "section"];

    }
    else if (selectedType == staffTypes[2]) {
        tableHeadings = ["id", "name", "building"];

    }
    else if (selectedType == staffTypes[3]) {
        tableHeadings = ["id", "name", "subjectHandled"];

    }
    else {

        tableHeadings = ["id", "name", "staffType", "section", "subjectHandled", "building"];
    }
    tableRowCells = [];
    for (let i = 0; i < tableHeadings.length; i++) {
        var cell = headingRow.insertCell(i)
        cell.innerHTML = tableHeadings[i];
        tableRowCells.push(cell);
    }

    for (let index = 0; index < data.length; index++) {
        var row = table.insertRow(index + 1);
        const staff = data[index];
        tableRowCells = [];

        for (let i = 0; i < tableHeadings.length; i++) {
            let cell = row.insertCell(i);
            cell.onclick = function () { showUpdateStaffForm(staff) };
            tableRowCells.push(cell);
        }
        if (selectedType == "AdministrativeStaff") {
            tableRowCells[0].innerHTML = staff["id"];
            tableRowCells[1].innerHTML = staff["name"];
            tableRowCells[2].innerHTML = staff["section"];

        }
        else if (selectedType == "SupportStaff") {
            tableRowCells[0].innerHTML = staff["id"];
            tableRowCells[1].innerHTML = staff["name"];
            tableRowCells[2].innerHTML = staff["building"];

        }
        else if (selectedType == "TeachingStaff") {
            tableRowCells[0].innerHTML = staff["id"];
            tableRowCells[1].innerHTML = staff["name"];
            tableRowCells[2].innerHTML = staff["subjectHandled"];

        }
        else {
            tableRowCells[0].innerHTML = staff["id"];
            tableRowCells[1].innerHTML = staff["name"];
            tableRowCells[2].innerHTML = staffTypes[staff["staffType"]];
            tableRowCells[3].innerHTML = staff["section"];
            tableRowCells[4].innerHTML = staff["subjectHandled"];
            tableRowCells[5].innerHTML = staff["building"];
        }
        


        let cellDeleteBtn = row.insertCell(tableHeadings.length);
        let deleteBtn = document.createElement("button")
        deleteBtn.innerHTML = "delete";
        deleteBtn.onclick = function () { showDeleteStaffConfirmation(staff) };
        cellDeleteBtn.appendChild(deleteBtn);
    }
}







async function showStaffByTypeTable() {
    selectedType = document.getElementById("selectStaffTypeToDisplay").value;
    console.log(selectedType);
    let data=[]
    if (selectedType == "AdministrativeStaff" || selectedType == "SupportStaff" ||selectedType == "TeachingStaff") {
        data = await getAllStaffByTypeRequest(selectedType);
    }
    else {
        data = await getAllStaffRequest();
    }

    showStaffTable(selectedType, data)

}
