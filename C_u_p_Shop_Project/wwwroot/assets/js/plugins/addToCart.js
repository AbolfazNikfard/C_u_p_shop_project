const closeButton = document.getElementById('closeButton');
const messageBox = document.getElementById('messageBox');
const messageBoxText = document.getElementById("message-box-text");
function showMessage() {
    messageBox.classList.add('show');
    setTimeout(hideMessage, 5000);
}
function hideMessage() {
    messageBox.classList.remove('show');
}
closeButton.addEventListener('click', function () {
    hideMessage();
});
document.getElementById("addToCart-submit-btn").addEventListener("click", async () => {
    try {


        const productIdElement = document.getElementById("productIdSection");
        const The_number_of_items_added_to_the_shopping_cart = document.getElementById("input-quantity");
        const itemNumber = The_number_of_items_added_to_the_shopping_cart.value;
        const requestBody = {
            productId: productIdElement.value,
            number: itemNumber
        }
        console.log(requestBody);
        const response = await fetch("/Buy/addToCart", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(requestBody)
        });
        console.log("response: ", response);
        
        if (response.status === 400) {
            messageBoxText.innerText = "تعداد کالای وارد شده صحیح نیست";
            showMessage();
        }
        else if (response.status === 200) {
            const data = await response.json();
            console.log("response: ", data);
            if (data.message === "More than stock") {
                messageBoxText.innerText = "موجودی کافی نیست";
                showMessage();
            }
            else if (data.message === "Too large") {
                messageBoxText.innerText = "تعداد کالا وارد شده بیش از حد مجاز است";
                showMessage();
            }
            else if (data.message === "Stock not enough") {
                messageBoxText.innerText = "موجودی کافی نیست";
                showMessage();
            }
            else if (data.message === "Success") {
                messageBox.style.backgroundColor = green;
                messageBox.style.Color = #fff;
                messageBoxText.innerText = "محصول با موفقیت به سبد شما افزوده شد";
                showMessage();
            }

        }
    }
    catch (err) {
        messageBoxText.innerText = "مشکلی پیش آمده است لطفا بعدا تلاش کنید";
        showMessage();
        //console.log("Catched error: ", err);
    }
})