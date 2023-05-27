var logout = document.querySelector('.log-out');

// Add a click event listener to the button
logout.addEventListener('click', function () {
    // Perform logout logic here
    // For example, you can clear user session and redirect to the login page

    // Clear user session
    clearUserSession();
});

// Function to clear user session
function clearUserSession() {
    // Clear any stored session data, such as user tokens or credentials
    // For example, you can use localStorage or session storage to store session data
    localStorage.removeItem('userToken');
    sessionStorage.clear();
}
