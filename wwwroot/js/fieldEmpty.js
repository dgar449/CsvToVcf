function IsEmpty() {
    if (document.forms['input-form'].file.value === "") {
        alert("No CSV file found. Please choose a csv.");
        return false;
    }
    return true;
}