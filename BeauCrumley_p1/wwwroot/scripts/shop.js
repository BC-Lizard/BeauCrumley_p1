
getUserStore();

var activeStore;
let cartDisplay = document.querySelector("#cart-window");
let cart = {
    storeId: signedInUser.defaultStore,
    userId: signedInUser.accountNo,
    items: [/*{partNo: #, price: #.##, quantity: #},
              {partNo: #, price: #.##, quantity: #}*/],
    subtotal: function() {
        let subtotal = 0;
        for (let i = 0; i < this.items.length; i++) {
            subtotal += (this.items[i].price * this.items[i].quantity);
        }
        return parseFloat(subtotal.toFixed(2));
    },
    tax: function() {
        let taxrate = activeStore.storeState.taxRate;
        let tax = this.subtotal() * taxrate;
        return parseFloat(tax.toFixed(2));
    },
    total: function() {
        total = this.subtotal() + this.tax();
        return parseFloat(total.toFixed(2));
    }
};

function getUserStore()
{
    let storeNo = signedInUser.defaultStore;
    fetch(`api/Stores/${storeNo}`)
        .then(response => response.json())
        .then(data => {
            console.log("GET store response data: ", data)
            updatePage(data);
            activeStore = data;
            updateTitle();
        });
}

function updatePage(storeObj) {
    let inv = storeObj.inventory;
    let stock = storeObj.invLevels;
    for (let i = 0; i < inv.length; i++) {
        document.querySelector("#product-display").innerHTML += `
        <div class="product-box">
            <div class="product-container">
                <h3>${inv[i].partName}</h3>
                <div class="part-visualization-container">
                    <img src="${inv[i].partImage}" alt="image of ${inv[i].partName}" />
                    <div class="buy-box">
                        <input type="button" value="Add To Cart" onclick="addToCart(${inv[i].partNo})" />
                        <div class="price-tag">$${inv[i].partPrice.toString().substr(0, inv[i].partPrice.toString().indexOf("."))}<span class="and-change">${inv[i].partPrice.toString().substr(inv[i].partPrice.toString().indexOf("."), inv[i].partPrice.toString().length)}</span></div>
                        <div class="product-stock">${stock[i]} in stock!</div>
                    </div>
                </div>
                <div class="product-description">
                    ${inv[i].partDescription}
                </div>
            </div>
        </div>`;
    }
}

function updateTitle() {
    document.querySelector("#title").innerHTML = activeStore.storeName;
}

function addToCart(partId)
{
    let newItem = {
        partNo: partId,
        price: getPartPrice(partId),
        quantity: 1
    }
    if (itemAlreadyInCart(partId) == true) {
        incrementQuantity(partId);
    } else {
        cart.items.push(newItem);
    }
    updateCart();
}

function updateCart() {
    let container = document.querySelector("#items-container");
    container.innerHTML = "";
    for (let i = 0; i < cart.items.length; i++) {
        container.innerHTML += 
        `<div class="cart-item">
            <img src="${getPartImage(cart.items[i].partNo)}" alt="image of ${getPartName(cart.items[i].partNo)}" />
            <div class="cart-item-title">${getPartName(cart.items[i].partNo)}</div>
            <div class="quantity-container">
                <div class="cart-quantity">Quantity: ${cart.items[i].quantity}</div>
                <div class="quantity-controls-container">
                    <div class="quantity-control" onclick="decrementQuantity(${cart.items[i].partNo})"><img class="qc" src="Images/qcd.png" /></div>
                    <div class="quantity-control" onclick="incrementQuantity(${cart.items[i].partNo}, true)"><img class="qc" src="Images/qci.png" /></div>
                    <div class="quantity-control" onclick="removeItemFromCart(${cart.items[i].partNo})"><img class="qc" src="Images/qcrm.png" /></div>
                </div>
            </div>
        </div>`;
    }
    document.querySelector("#cart-total").innerHTML = `Total: ${cart.total()}`;
}

function getPartPrice(Id) {
    for (let i = 0; i < activeStore.inventory.length; i++) {
        if (activeStore.inventory[i].partNo == Id) {
            return activeStore.inventory[i].partPrice;
        }
    }
}

function getPartName(Id) {
    for (let i = 0; i < activeStore.inventory.length; i++) {
        if (activeStore.inventory[i].partNo == Id) {
            return activeStore.inventory[i].partName;
        }        
    }
}

function getPartImage(Id) {
    for (let i = 0; i < activeStore.inventory.length; i++) {
        if (activeStore.inventory[i].partNo == Id) {
            return activeStore.inventory[i].partImage;
        }        
    }
}

function itemAlreadyInCart(Id) {
    for (let i = 0; i < cart.items.length; i++) {
        if (cart.items[i].partNo == Id) {
            return true;
        }
    }
    return false;
}

function incrementQuantity(Id, update = false) {
    for (let i = 0; i < cart.items.length; i++) {
        if (cart.items[i].partNo == Id) {
            cart.items[i].quantity += 1;
        }
    }
    if (update == true) {
        updateCart();
    }
}

function decrementQuantity(Id) {
    for (let i = 0; i < cart.items.length; i++) {
        if (cart.items[i].partNo == Id) {
            cart.items[i].quantity -= 1;
            if (cart.items[i].quantity == 0) {
                removeItemFromCart(Id)
            }
        }
    }
    updateCart();
}

function removeItemFromCart(Id) {
    for (let i = 0; i < cart.items.length; i++) {
        if (cart.items[i].partNo == Id) {
            cart.items.splice(i, 1);
        }
    }
    updateCart();
}

function toggleCart() {
    if (cartDisplay.classList.contains("display-off")) {
        cartDisplay.classList.remove("display-off");
    } else {
        cartDisplay.classList.add("display-off");
    }
}

function sendOrder() {
    //make sure order is valid (not empty and not violating available stock)
    if (cart.items.length <= 0 || orderQuantitiesGood() == false) {
        console.warn("Bad order. Canceling submission. Check cart for errors and try again.");
    } else {
        stringifiedItemData = buildItemDataString(cart.items);
        stringifiedOrderData = buildOrderDataString();
        console.log("Item Data String: ", stringifiedItemData);
        console.log("Order Data String: ", stringifiedOrderData);
        fetch(`api/Orders/${stringifiedOrderData}/${stringifiedItemData}`, {method: 'POST'})
            .then(response => response.text())
            .then(data => {
                console.log(data);
                cart.items = [];
                updateCart();
                location.reload();
                return data;
            });
    }
}

function orderQuantitiesGood() {
    for (let i = 0; i < cart.items.length; i++) {
        for (let j = 0; j < activeStore.inventory.length; j++) {
            if (cart.items[i].partNo == activeStore.inventory[j].partNo) {
                if (cart.items[i].quantity <= 0 || cart.items[i].quantity > activeStore.invLevels[j]) {
                    return false;
                }
            }
        }
    }
    return true;
}

function buildItemDataString(items) {
    let str = "";
    for (let i = 0; i < items.length; i++) {
        str += `${items[i].partNo}-${items[i].price}-${items[i].quantity}`
        if (i < items.length - 1) {
            str += "_";
        }
    }
    return str;
}

function buildOrderDataString() {
    let currentDate = Date.now();
    let str = "";
    str += `${activeStore.storeNo}-`;
    str += `${signedInUser.accountNo}-`;
    str += `${currentDate}-`;
    str += `${cart.subtotal()}-`;
    str += `${cart.tax()}-`;
    str += `${cart.total()}`;
    return str;
}