function getCookie(name) {
    const cookies = document.cookie.split('; ').reduce((acc, cookie) => {
        const [name, value] = cookie.split('=');
        acc[name] = value;
        return acc;
    }, {});

    return cookies[name];
}
async function getDailyTip() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/plans/tips";
    console.log("sadasd");
    await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(async response => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error);
        }

        return response.json();
    })
    .then(data => {
        const dietPlanContainer = document.querySelector('.dailytip');
    dietPlanContainer.innerHTML = data.TipText;
    const dietPlanContainer2 = document.querySelector('.dailytip2');
    dietPlanContainer2.innerHTML = data.TipText;
    })
    .catch(error => {
        //displayError(errorMessages[error]);
    });

    
}
