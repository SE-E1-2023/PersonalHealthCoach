let daysCount = 1;
var data1 = { "days": [] };

function AddData(){
    const day = parseInt(document.getElementById("dayType0").value);
    const hearthbeats = parseInt(document.getElementById("heartBeats0").value);
    const breathing = parseInt(document.getElementById("breathingRate0").value);
    const sleeping = parseFloat(document.getElementById("sleepingHours0").value);
    var dataToSend = {
        "day":day,
        "heartrate":hearthbeats,
        "breathingrate":breathing,
        "sleepinhours":sleeping,
    };
    data1.days.push(dataToSend);
    console.log(dataToSend);
    console.log(data1);
}



async function StressForm() {
    const url = "http://localhost:8000/StressDetection";
    
    const day = parseInt(document.getElementById("dayType0").value);
    const hearthbeats = parseInt(document.getElementById("heartBeats0").value);
    const breathing = parseInt(document.getElementById("breathingRate0").value);
    const sleeping = parseFloat(document.getElementById("sleepingHours0").value);
    var dataToSend = {
        "day":day,
        "heartrate":hearthbeats,
        "breathingrate":breathing,
        "sleepinhours":sleeping,
    };
    data1.days.push(dataToSend);

    serverResponse = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'mode':'no-cors',
            'Access-Control-Allow-Origin' :'*'
        },
        body:JSON.stringify(data1)
    })
    .then(async response => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error);
        }

        return response.json();
    })
    .then(dataThing => {
        return dataThing;
    })
    .catch(error => {
    });   
    const ancor = document.getElementById("Anchor");
    console.log(serverResponse);
    for (element in serverResponse) {
        for (element2 in serverResponse[element]){
            console.log(serverResponse[element][element2]);
            const box = document.createElement("div");
            box.id = "textResponse";
            const txt = document.createTextNode(serverResponse[element][element2]);
            box.appendChild(txt);
            ancor.appendChild(box);
        }
    }
}
