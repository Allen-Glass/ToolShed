export const CreateUserAccount = function (userInformation) {
    fetch("", {
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