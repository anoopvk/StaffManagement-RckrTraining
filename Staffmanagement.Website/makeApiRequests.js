// async function getAllStaffRequest() {
//     const response = await fetch(appsettings.url, {
//         mode: "cors"
//     });
//     var data = await response.json();
//     return data;
// }
async function getAllStaffByTypeRequest(staffType){
    const response = await fetch(appsettings.url+"?staffType="+staffType, {
        mode: "cors"
    });
    var data = await response.json();

    return paginate(data);


    
}

async function postStaffRequest(body) {
    return (await fetch(appsettings.url , {
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
