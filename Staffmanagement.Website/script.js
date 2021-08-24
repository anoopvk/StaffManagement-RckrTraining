async function init() {
    populateSelectStaffTypeDropdownForDisplay();
    populateSelectStaffTypeDropdownForAdd();
    
    showStaffByTypeTable()
}

var tableOrderAcsending=true;
var tableOrderBy="building";

init()