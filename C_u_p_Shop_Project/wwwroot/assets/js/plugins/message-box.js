const messageBox = document.getElementById('messageBox');
const closeButton = document.getElementById('closeButton');
const showMessageBox = document.getElementById('show-message-box');
const autoHideTime = 5000; // 5 seconds

// Function to show the message box with animation
function showMessage() {
    messageBox.classList.add('show');
    setTimeout(hideMessage, autoHideTime);
}

// Function to hide the message box
function hideMessage() {
    messageBox.classList.remove('show');
}

// Function to hide the message box when the close button is clicked
closeButton.addEventListener('click', function () {
    hideMessage();
});
//showMessageBox.addEventListener('click', function (event) {
//    // Show the message box only if the clicked element is not the message box itself
//    if (!messageBox.contains(event.target)) {
//        showMessage();
//    }
//});