﻿{
    const closeButton = document.getElementById('closeButton');
    const messageBox = document.getElementById('messageBox');
    const IdentityElement = document.getElementById("UserIdentityName");
    messageBox.style.Color = "white";
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
            if (IdentityElement.innerText == "") {
                const currentUrl = window.location.href;
                const urlObject = new URL(currentUrl);
                window.location.href = urlObject.origin + "/Account/Login?message=firstLoginToYourAccount";
            }
            else {
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
                    messageBox.style.backgroundColor = "#ef2f2f";
                    messageBox.style.border = "2px solid #ef2f2f";
                    if (closeButton.classList.contains("success"))
                        closeButton.classList.remove("success");
                    closeButton.classList.add("error");
                    messageBoxText.innerText = "تعداد کالای وارد شده صحیح نیست";
                    showMessage();
                }
                else if (response.status === 500) {
                    messageBox.style.backgroundColor = "#ef2f2f";
                    messageBox.style.backgroundColor = "#ef2f2f";
                    if (closeButton.classList.contains("success"))
                        closeButton.classList.remove("success");
                    closeButton.classList.add("error");
                    messageBoxText.innerText = "مشکلی پیش آمده است لطفا بعدا تلاش کنید";
                    showMessage();
                }
                else if (response.status === 200) {
                    const data = await response.json();
                    console.log("response: ", data);
                    if (data.message === "More than stock") {
                        messageBox.style.backgroundColor = "#ef2f2f";
                        messageBox.style.border = "2px solid #ef2f2f";
                        if (closeButton.classList.contains("success"))
                            closeButton.classList.remove("success");
                        closeButton.classList.add("error");
                        messageBoxText.innerText = "موجودی کافی نیست";
                        showMessage();
                    }
                    else if (data.message === "Too large") {
                        messageBox.style.backgroundColor = "#ef2f2f";
                        messageBox.style.border = "2px solid #ef2f2f";
                        if (closeButton.classList.contains("success"))
                            closeButton.classList.remove("success");
                        closeButton.classList.add("error");
                        messageBoxText.innerText = "تعداد کالا مجاز برای خرید دارای محدودیت است";
                        showMessage();
                    }
                    else if (data.message === "Stock not enough") {
                        messageBox.style.backgroundColor = "#ef2f2f";
                        messageBox.style.border = "2px solid #ef2f2f";
                        if (closeButton.classList.contains("success"))
                            closeButton.classList.remove("success");
                        closeButton.classList.add("error");
                        messageBoxText.innerText = "موجودی کافی نیست";
                        showMessage();
                    }
                    else if (data.message === "Success") {
                        messageBox.style.backgroundColor = "#079e1b";
                        messageBox.style.border = "2px solid #079e1b";
                        if (closeButton.classList.contains("error"))
                            closeButton.classList.remove("error");
                        closeButton.classList.add("success");
                        messageBoxText.innerText = "محصول با موفقیت به سبد شما افزوده شد. در حال انتقال به صفحه سبد خرید ...";
                        showMessage();
                        setTimeout(() => {
                            window.location.replace("http://localhost:5048/Buy/userCart");
                        }, 4000)
                    }
                }
            }
        }
        catch (err) {
            console.log("catched error: ", err.message);
            messageBox.style.backgroundColor = "#ef2f2f";
            messageBox.style.backgroundColor = "#ef2f2f";
            if (closeButton.classList.contains("success"))
                closeButton.classList.remove("success");
            closeButton.classList.add("error");
            messageBoxText.innerText = "مشکلی پیش آمده است لطفا بعدا تلاش کنید";
            showMessage();
        }
    });
}