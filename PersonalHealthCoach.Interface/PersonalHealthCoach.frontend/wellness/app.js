async function getWellness(){
    const userId = getCookie("userId");
    const url = " http://localhost:7071/api/v1/users/" + userId + "/plans/wellness";

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
        document.getElementById("title").innerHTML = data.Title;
        document.getElementById("description").innerHTML = data.Description;
        document.getElementById("categories").innerHTML = data.Categories;
    })
    .catch(error => {
        document.getElementById("title").innerHTML = "Something went wrong. Please try again later.";
    });
}

function getCookie(name) {
    const cookies = document.cookie.split('; ').reduce((acc, cookie) => {
        const [name, value] = cookie.split('=');
        acc[name] = value;
        return acc;
    }, {});

    return cookies[name];
}
