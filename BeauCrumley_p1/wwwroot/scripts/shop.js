
getUserStore();

var activeStore;
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
        return subtotal;
    },
    tax: function() {
        let taxrate = activeStore.storeState.taxRate;
        return this.subtotal * taxrate;
    },
    total: function() {
        return this.subtotal + this.tax;
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
    console.log(cart);
}

function getPartPrice(Id) {
    for (let i = 0; i < activeStore.inventory.length; i++) {
        if (activeStore.inventory[i].partNo == Id) {
            return activeStore.inventory[i].partPrice;
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

function incrementQuantity(Id) {
    for (let i = 0; i < cart.items.length; i++) {
        if (cart.items[i].partNo == Id) {
            cart.items[i].quantity += 1;
        }
    }
}