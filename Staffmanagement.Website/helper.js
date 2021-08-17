


async function resetStaffTable() {
    let selectedType = document.getElementById("selectStaffTypeToDisplay").value;
    let data = await getAllStaffRequest();
    showStaffTable(selectedType,data);
}
function populateSelectStaffTypeDropdownForDisplay() {
    staffTypesDropdown=document.getElementById("selectStaffTypeToDisplay");
    for (let index = 1; index <= Object.keys(staffTypes).length; index++) {
        let option = document.createElement("option");
        option.text = staffTypes[index];
        option.value = staffTypes[index];

        staffTypesDropdown.add(option);
    }
}
function populateSelectStaffTypeDropdownForAdd(){
    staffTypesDropdown=document.getElementById("staffTypeForAdd");
    for (let index = 1; index <= Object.keys(staffTypes).length; index++) {
        let option = document.createElement("option");
        option.text = staffTypes[index];
        option.value = index;

        staffTypesDropdown.add(option);
    }
}
