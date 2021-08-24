async function displayAllStaffs() {
    data = await getAllStaffByTypeRequest("");
    tableHeadings = ["id", "name", "staffType", "section", "subjectHandled", "building"];
    staffFields=["id", "name", "staffType", "section", "subjectHandled", "building"];

    generateTable(tableHeadings,staffFields,data);

}