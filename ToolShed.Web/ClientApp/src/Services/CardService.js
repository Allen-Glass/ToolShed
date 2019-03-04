export const SubmitCard = function (card) {
    fetch("", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            CardNumber: card.CardNumber,
            CCV: card.CCV,
            CardHolderName: card.CardHolderName,
            BillingAddress: {
                StreetName: card.StreetName,
                StreeName2: card.StreetName2,
                AptNumber: card.AptNumber,
                Country: card.Country,
                State: card.State,
                City: card.City,
                ZipCode: card.ZipCode
            }
        })
    })
    .catch(error => console.error('Error:', error));
}

export const GetCardInformation = function () {

}

export const UpdateAddress = function () {

}

export const UpdateCard = function () {

}