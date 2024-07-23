function MakeUpdateQtyButtonVisible(id, visible) {
    const updateQtyButton = document.querySelector("div[data-itemId='" + id + "']");

    if (visible == true) {
        updateQtyButton.style.display = "inline-block";
    }
    else {
        updateQtyButton.style.display = "none";
    }


}