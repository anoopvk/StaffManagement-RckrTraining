async function init() {
    populateSelectStaffTypeDropdownForDisplay();
    populateSelectStaffTypeDropdownForAdd();
    // document.getElementById("rowsPerPage").value=5;
    showStaffByTypeTable()

}

var tableOrderAcsending=true;
var tableOrderBy="id";


init()