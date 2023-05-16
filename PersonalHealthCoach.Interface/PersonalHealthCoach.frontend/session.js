function checkSession() {
    const token = getCookie('userId');
    if (!token) {
        window.location.href = '/auth/login.html';
    }
}

function getCookie(name) {
    const cookies = document.cookie.split('; ').reduce((acc, cookie) => {
        const [name, value] = cookie.split('=');
        acc[name] = value;
        return acc;
    }, {});

    return cookies[name];
}