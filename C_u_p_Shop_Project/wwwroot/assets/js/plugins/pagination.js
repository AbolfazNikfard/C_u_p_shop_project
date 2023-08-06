{
    const currentUrl = window.location.href;
    const urlObject = new URL(currentUrl);
    const searchParams = urlObject.searchParams;
    let url, paramName = "page";
    let liElement, aElement, i;
    const ulElementThatContainPages = document.getElementById("pagesContainer");
    const currentPageElement = document.getElementById("activePage");
    const currentPageInNumber = parseInt(currentPageElement.innerText);
    const pagesCountInNumber = parseInt(document.getElementById("pagesCount").innerText);
    for (i = 0; i < ulElementThatContainPages.childElementCount; i++) {
        liElement = ulElementThatContainPages.children[i];
        aElement = liElement.children[0];
        if (!isNaN(aElement.innerHTML)) {
            if (searchParams.has(paramName))
                searchParams.set(paramName, aElement.innerHTML);
            else
                searchParams.append(paramName, aElement.innerHTML);

            url = urlObject.origin + urlObject.pathname + "?" + searchParams.toString()

            aElement.setAttribute("href", url);
        }
    }
    function previousPage() {
        if (currentPageInNumber != 1) {
            if (searchParams.has(paramName))
                searchParams.set(paramName, currentPageInNumber - 1);
            else
                searchParams.append(paramName, currentPageInNumber - 1);

            url = urlObject.origin + urlObject.pathname + "?" + searchParams.toString();
            window.location.href = url;
        }
    }
    function nextpage() {
        if (currentPageInNumber != pagesCountInNumber) {
            if (searchParams.has(paramName))
                searchParams.set(paramName, currentPageInNumber + 1);
            else
                searchParams.append(paramName, currentPageInNumber + 1);

            url = urlObject.origin + urlObject.pathname + "?" + searchParams.toString();
            window.location.href = url;
        }
    }
}
