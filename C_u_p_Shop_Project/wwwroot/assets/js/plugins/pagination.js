{
    const currentUrl = window.location.href;
    const urlObject = new URL(currentUrl);
    const searchParams = urlObject.searchParams;
    let url, paramName = "page";
    let liElement, aElement, i;
    const ulElementThatContainPages = document.getElementById("pagesContainer");
    for (i = 1; i < ulElementThatContainPages.childElementCount - 1; i++) {
        liElement = ulElementThatContainPages.children[i];
        aElement = liElement.children[0];

        if (searchParams.has(paramName))
            searchParams.set(paramName, aElement.innerHTML);
        else
            searchParams.append(paramName, aElement.innerHTML);

        url = urlObject.origin + urlObject.pathname + "?" + searchParams.toString()

        aElement.setAttribute("href", url);
    }
}
