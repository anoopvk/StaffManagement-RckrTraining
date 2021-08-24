// async function getAllStaffRequest() {
//     const response = await fetch(appsettings.url, {
//         mode: "cors"
//     });
//     var data = await response.json();
//     return data;
// }
async function getAllStaffByTypeRequest(staffType, doPagination = true) {
    const response = await fetch(appsettings.url + "?staffType=" + staffType, {
        mode: "cors"
    });
    // console.log(response["status"]);
    if(response["status"]=="404"){
        return [];
    }
    var data = await response.json();

    sortedData = sortData(data);
    if (doPagination == true) {
        return paginate(data);
    }
    else {
        return data;
    }



}

async function postStaffRequest(body) {
    return (await fetch(appsettings.url, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: body
    }))
}

async function putStaffRequest(id, body) {
    return (await fetch(appsettings.url + "/" + id, {
        method: 'PUT',
        headers: {
            "Content-Type": "application/json"
        },
        body: body
    }))
}

async function deleteStaffRequest(id) {
    return (await fetch(appsettings.url + "/" + id, {
        method: 'DELETE',
        headers: {
            "Content-Type": "application/json"
        }
    }))
}
