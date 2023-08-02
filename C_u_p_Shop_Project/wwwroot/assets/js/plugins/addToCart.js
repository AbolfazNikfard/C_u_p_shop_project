const productIdElement = document.getElementById("productIdSection");
const The_number_of_items_added_to_the_shopping_cart = document.getElementById("input-quantity");
const itemNumber = The_number_of_items_added_to_the_shopping_cart.value;
document.getElementById("show-message-box").addEventListener("click", async () => {
    try {
        console.log(itemNumber);
        console.log(productIdElement.value);
        const requestUrl = `/Buy/addToCart?productID=${productIdElement.value}` + `?number=ab`;
        console.log(requestUrl);
        const response = await fetch(requestUrl, {
            method: "POST",
        });
        const data = await response.json();
        if (data.statusCode === 400)
            alert("تعداد کالا وارد شده صحیح نمیباشد");
    }
    catch (err) {
        alert("مشکلی پیش آمده است لطفا بعدا تلاش کنید");
    }
})