async function getData() {
    const url = "http://localhost:8000/StressDetection";

        const day = document.getElementById("dayType0").value;
        const hearthbeats = document.getElementById("heartBeats0").value;
        const breathing = document.getElementById("breathingRate0").value;
        const sleeping = document.getElementById("sleepingHours0").value;
        const dataToSend = {
            day:day,
            heartrate:hearthbeats,
            breathingrate:breathing,
            sleepinhours:sleeping,
        };
    serverResponse = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body:JSON.stringify(dataToSend)
        })
        .then(async response => {
            if (!response.ok) {
                const error = await response.json();
                throw new Error(error);
            }

            return response.json();
        })
        .then(data => {
            return data;
        })
        .catch(error => {
        });   
    
    console.log(serverResponse);

        
}