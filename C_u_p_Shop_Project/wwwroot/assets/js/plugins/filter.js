{
    let currentUrl, urlObject, searchParams, selectedValue, updatedUrl, paramName = "sort";
    const selectElement = document.getElementById("Filter");
    const searchBtn = document.getElementById("search_button");
    const searchBtnResponsive = document.getElementById("search_button_responsive");
    const searchInput = document.getElementById("search_input");
    const searchInputResponsive = document.getElementById("search_input_responsive");
    selectedValue = selectElement.value;
    currentUrl = window.location.href;
    urlObject = new URL(currentUrl);
    searchParams = urlObject.searchParams;
    if (searchParams.has(paramName)) {
        let paramValue = searchParams.get(paramName);
        let optionElement;
        switch (paramValue) {
            case "Newest":
                optionElement = getOptionByValue("Newest");
                break;
            case "Oldest":
                optionElement = getOptionByValue("Oldest");
                break;
            case "ExpensiveToCheap":
                optionElement = getOptionByValue("ExpensiveToCheap");
                break;
            case "CheapToExpensive":
                optionElement = getOptionByValue("CheapToExpensive");
                break;
            case "AlphabetAscending":
                optionElement = getOptionByValue("AlphabetAscending");
                break;
            case "AlphabetDescending":
                optionElement = getOptionByValue("AlphabetDescending");
                break;
            default:
                optionElement = getOptionByValue("null");
        }
        selectElement.children[0].removeAttribute("selected");
        optionElement.setAttribute("selected", "");
    }
    selectElement.addEventListener("change", function () {
        selectedValue = selectElement.value;
        currentUrl = window.location.href;
        urlObject = new URL(currentUrl);
        searchParams = urlObject.searchParams;

        if (searchParams.has(paramName)) searchParams.set(paramName, selectedValue);
        else searchParams.append(paramName, selectedValue);

        updatedUrl = urlObject.origin + urlObject.pathname + "?" + searchParams.toString();
        window.location.href = updatedUrl;
    });
    searchBtn.addEventListener("click", () => {
        currentUrl = window.location.href;
        urlObject = new URL(currentUrl);
        searchParams = urlObject.searchParams;
        if (searchParams.has("search")) searchParams.set("search", searchInput.value);
        else searchParams.append("search", searchInput.value);
        updatedUrl = urlObject.origin + urlObject.pathname + "?" + searchParams.toString();
        window.location.href = updatedUrl;
    });
    searchBtnResponsive.addEventListener("click", () => {
        currentUrl = window.location.href;
        urlObject = new URL(currentUrl);
        searchParams = urlObject.searchParams;
        if (searchParams.has("search")) searchParams.set("search", searchInputResponsive.value);
        else searchParams.append("search", searchInputResponsive.value);
        updatedUrl = urlObject.origin + urlObject.pathname + "?" + searchParams.toString();
        window.location.href = updatedUrl;
    })
    function getOptionByValue(value) {
        for (const option of selectElement.options) {
            if (option.value === value) {
                return option;
            }
        }
    }
}