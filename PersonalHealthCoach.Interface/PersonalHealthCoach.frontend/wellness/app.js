/*async function getWellness(){
    const userId = getCookie("userId");
    const url = "http://localhost:7071/v1/users/" + userId + "/plans/wellness";

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
        //document.getElementById("title").innerHTML = data.Action.Title;
        //document.getElementById("description").innerHTML = data.Action.Description;
        //document.getElementById("categories").innerHTML = data.Categories;
    })
    .catch(error => {
        if (errorMessages[error.message] == undefined) {
            displayError("Something went wrong. Please try again.");
        }
        else {
            displayError(errorMessages[error.message]);
        }
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
errorMessages = {
    //nu stiu daca sunt niste erori specifice aruncate din backend ;-;
}
function displayError(errorMessage) {
    const errorContainer = getFormItem('error-container');
    errorContainer.textContent = errorMessage;
    errorContainer.style.display = 'block';

    setTimeout(() => {
        errorContainer.style.display = 'none';
    }, 5000);
}
*/

//Test pentru parsarea unui json
/*const txt = ' {"Action": {"Title": "Mindfulness Walk", "Description": "While mindful walking has no single definition, the goal is clear: to be consciously aware while moving through the environment.  The journey becomes less about the destination and more about an awareness of what is outside and inside us. When moving - perhaps to an even greater degree - it is possible to find stillness, to become aware, and be present. Find somewhere safe to walk where you will not be disturbed: your garden, a city park, country lane, or a busy street and wear comfortable clothing and shoes for walking. Stand still and become aware of how you feel. Consider your posture, the weight of your body, feet in your shoes, and your muscles as you balance. Take a few deep breaths and slowly bring your awareness into the present. Begin walking, a little slower than normal. Maintain awareness of each footstep as it rolls from heel to toe, the muscles and tendons in your feet and legs and the movement and muscles elsewhere in your body. Pay attention to your senses as you walk: Hear the wind blow in the trees, smell the cut grass, feel the light touch of rain on your face, see the car lights reflected on the windows of shops, the shadows moving as you walk Be aware of each breath. Breathe easily, but deeply. When your mind drifts from walking and breathing, gently guide your thoughts back. Continue walking for as long as you feel safe and comfortable. Remember, wherever you walk, whether a bustling street or a windy hillside, there is a myriad of stimuli with which to flood your senses. Become aware; savor the sensations.  When your meditation is at an end, stop and stand still. Take a few deep breaths."},"Categories": ["Physical","Spiritual","Environmental"]}';
const obj = JSON.parse(txt);
document.getElementById("title").innerHTML = obj.Action.Title;
document.getElementById("description").innerHTML = obj.Action.Description;
document.getElementById("categories").innerHTML = obj.Categories;
*/