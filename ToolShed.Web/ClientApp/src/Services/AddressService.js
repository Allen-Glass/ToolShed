export const AddAddress = function (address) {
    fetch("https://toolshed-api.azurewebsites.net", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Address: address
        })
    })
        .then(response => response.json())
        .then(data => {
            return data;
        });
}

export const GetAddresses = function (userInformation) {
    fetch("https://toolshed-api.azurewebsites.net", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            UserInformation: userInformation
        })
    })
        .then(response => response.json())
        .then(data => {
            return data;
        });
}

export const DeleteAddress = function (address) {
    fetch("https://toolshed-api.azurewebsites.net", {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Address: address
        })
    })
        .then(response => response.json())
        .then(data => {
            return data;
        });
}