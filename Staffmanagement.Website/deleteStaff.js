
function showDeleteStaffConfirmation(staff){
    // event.stopPropagation();
    console.log(staff);
    console.log(staff["id"]);
    console.log(document.getElementById("idForDelete"));
    document.getElementById("idForDelete").value = staff["id"];
    document.getElementById("deleteConfirmationContainer").style.display = "block";
    console.log("delete btn clicked",staff["id"])
}

function closeDeleteStaffConfirmation(){
    document.getElementById("deleteConfirmationContainer").style.display = "none";

}
async function deleteStaff(){
    let id = document.getElementById("idForDelete").value
    document.getElementById("idForDelete").value="";
    response=await deleteStaffRequest(id);
    console.log(response);
    closeDeleteStaffConfirmation();
}