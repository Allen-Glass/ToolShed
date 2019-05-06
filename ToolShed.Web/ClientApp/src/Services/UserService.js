export const createUserAccount = function (userInformation) {
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

export const deleteUserAccount = function (userInformation) {
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