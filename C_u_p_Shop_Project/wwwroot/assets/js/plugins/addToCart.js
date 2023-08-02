
document.getElementById("show-message-box").addEventListener("click", async () => {
    try {
        const productIdElement = document.getElementById("productIdSection");
        const The_number_of_items_added_to_the_shopping_cart = document.getElementById("input-quantity");
        const itemNumber = The_number_of_items_added_to_the_shopping_cart.value;
        // console.log(itemNumber);
        // console.log(productIdElement.value);
        const requestBody = {
            productID: productIdElement.value,
            number: itemNumber
        };
        //const requestUrl = `/Buy/addToCart?productID=${productIdElement.value}` + `&number=${itemNumber}`;
        //console.log(requestUrl);
        console.log(requestBody);
        const response = await fetch("/Buy/addToCart", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(requestBody)
        });
        const data = await response.json();
        if (data.statusCode === 400)
            alert("تعداد کالا وارد شده صحیح نمیباشد");
    }
    catch (err) {
        alert("مشکلی پیش آمده است لطفا بعدا تلاش کنید");
    }
})