function AddItem() {
    let inventoryId = document.getElementById("InventoryId").value;
    let itemName = document.getElementById("ItemToAdd_Name");
    let itemWeight = document.getElementById("ItemToAdd_Weight");
    let itemValue = document.getElementById("ItemToAdd_Value");

    var newItem = { Name: itemName.value, Weight: parseFloat(itemWeight.value), Value: parseInt(itemValue.value) };
    var url = "/Inventory/AddItem/" + inventoryId;

    $.ajaxSetup({
        headers: {
            'RequestVerificationToken': document.getElementById('RequestVerificationToken').value
        }
    });

    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(newItem),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: AddItemCallback,
    });
}

function AddItemCallback(data) {
    console.log(data);
    location.reload(true);
}