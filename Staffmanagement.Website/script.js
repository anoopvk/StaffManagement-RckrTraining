
async function init() {
    populateSelectStaffTypeDropdownForDisplay();
    populateSelectStaffTypeDropdownForAdd();

    let data = await getAllStaffRequest();
    let selectedType=document.getElementById("selectStaffTypeToDisplay").value;
    showStaffTable(selectedType,data);
}
init()