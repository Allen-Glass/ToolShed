export const PlaceRental = function (rental) {
    fetch("", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Rental: rental
        })
    })
        .then(data => {
            return data;
        });
}

export const StartRental = function (rental) {
    fetch("", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Rental: rental
        })
    })
        .then(data => {
            return data;
        });
}

export const ReturnRental = function (rental) {
    fetch("", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Rental: rental
        })
    })
        .then(data => {
            return data;
        });
}