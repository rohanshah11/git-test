
$("#savemenu").click(function () {
    event.preventDefault();

    var model = {};
    var item = [];
    var name = $('#full_name').val();
    if (name.length == 0) {
        alert("name is required")
        return false;
    }
    var address = $('#address').val();
    if (address.length == 0) {
        alert("address is required")
        return false;
    }
    var contact = $('#primary_contact').val();
    if (contact.length == 0) {
        alert("contact is required")
        return false;
    }


    model.customer_name = $("#full_name").val();
    model.address = $("#address").val();
    model.primary_contact = $("#primary_contact").val();
    model.address = $("#address").val();
    model.secondary_contact = $("#secondary_contact").val();
    model.email = $("#email").val();
    model.total_amount = +($(".total-cart").eq(0).text())
    var cartArray = shoppingCart.listCart();
    for (var i in cartArray) {
        var data = {};
        data.menu_id = cartArray[i].id;
        data.rate = cartArray[i].price;
        data.quantity = cartArray[i].count;
        item.push(data);
    }
    model["order_details"] = item;

    console.log('data', model);


    if (item.length > 0) {
        $.ajax({
            // contentType: 'application/json; charset=utf-8',
            type: "POST",
            url: "/order",

            dataType: "json",
            data: model,

            success: function (response) {

                if (response.success === true) {
                    window.location.href = '/';
                    alert("Your ordered has been placed")
                    shoppingCart.clearCart();
                    displayCart();

                }
                else {
                    alert(response.msg);
                    //  alert("Something went wrong");
                }
            },
            error: function (response) {

                alert('error text')

            }
        });
    }
    else {
        alert("Items Not Selected.");
    }

});

// ************************************************
// Shopping Cart API
// ************************************************

var shoppingCart = (function () {
    // =============================
    // Private methods and propeties
    // =============================
    cart = [];

    // Constructor
    function Item(id, name, price, count) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.count = count;
    }

    // Save cart
    function saveCart() {
        sessionStorage.setItem('shoppingCart', JSON.stringify(cart));
    }

    // Load cart
    function loadCart() {
        cart = JSON.parse(sessionStorage.getItem('shoppingCart'));
    }
    if (sessionStorage.getItem("shoppingCart") != null) {
        loadCart();

    }


    // =============================
    // Public methods and propeties
    // =============================
    var obj = {};

    // Add to cart
    obj.addItemToCart = function (id, name, price, count) {
        for (var item in cart) {
            if (cart[item].id === id) {
                alert(" Item added to cart")
                cart[item].count++;
                saveCart();
               
                return;
            }
        }
        var item = new Item(id, name, price, count);
        cart.push(item);
        saveCart();

    }
    // Set count from item
    obj.setCountForItem = function (id, count) {
        for (var i in cart) {
            if (cart[i].id === id) {
                cart[i].count = count;

                break;
            }
        }
    };
    // Remove item from cart
    obj.removeItemFromCart = function (id) {
        for (var item in cart) {
            if (cart[item].id === id) {
                cart[item].count--;
                if (cart[item].count === 0) {
                    cart.splice(item, 1);
                }
                break;
            }
        }
        saveCart();
    }

    // Remove all items from cart
    obj.removeItemFromCartAll = function (id) {
        for (var item in cart) {
            if (cart[item].id === id) {
                cart.splice(item, 1);
                break;
            }
        }
        saveCart();
    }

    // Clear cart
    obj.clearCart = function () {
        cart = [];
        saveCart();
    }

    // Count cart
    obj.totalCount = function () {
        var totalCount = 0;
        for (var item in cart) {
            totalCount += cart[item].count;
        }

        return totalCount;

    }

    // Total cart
    obj.totalCart = function () {
        var totalCart = 0;
        for (var item in cart) {
            totalCart += cart[item].price * cart[item].count;
        }
        return Number(totalCart.toFixed(2));

    }

    // List cart
    obj.listCart = function () {
        var cartCopy = [];
        for (i in cart) {
            item = cart[i];
            itemCopy = {};
            for (p in item) {
                itemCopy[p] = item[p];

            }
            itemCopy.total = Number(item.price * item.count).toFixed(2);
            cartCopy.push(itemCopy)
        }
        return cartCopy;
    }

    // cart : Array
    // Item : Object/Class
    // addItemToCart : Function
    // removeItemFromCart : Function
    // removeItemFromCartAll : Function
    // clearCart : Function
    // countCart : Function
    // totalCart : Function
    // listCart : Function
    // saveCart : Function
    // loadCart : Function
    return obj;
})();


// *****************************************
// Triggers / Events
// *****************************************
// Add item
$('.add-to-cart').click(function (event) {
    event.preventDefault();

    var id = Number($(this).data('id'));
    var name = $(this).data('name');
    var price = Number($(this).data('price'));
    shoppingCart.addItemToCart(id, name, price, 1);
    displayCart();

});
// Clear items
$('.clear-cart').click(function () {
    shoppingCart.clearCart();
    displayCart();
});


function displayCart() {
    var cartArray = shoppingCart.listCart();
    var output = "  <tr style='background-color:black; color:white;'>"
        + " <th> Name</th>"
        + "<th>Price</th>"
        + "<th>Quantity</th>"
        + "<th>Delete</th>"
        + "<th>Total</th>"
        + "</tr >";
    for (var i in cartArray) {
        output += "<tr>"
            + "<td>" + cartArray[i].name + "</td>"
            + "<td>" + cartArray[i].price + "</td>"
            + "<td><div class='input-group input-group-sm' width='10px'>"
            + "<input type='number' class='item-count form-control' data-id='" + cartArray[i].id + "' value='" + cartArray[i].count + "'> </td>"
            + "<td width:'10px'><button class='delete-item btn btn-danger' data-id=" + cartArray[i].id + "><i class='fa fa-trash' aria-hidden='true'></i></button></td>"
            + " = "
            + "<td>" + cartArray[i].total + "</td>"
            + "</tr>";
    }
    $('.show-cart').html(output);
    $('.total-cart').html(shoppingCart.totalCart());
    $('.total-count').html(shoppingCart.totalCount());
}

// Delete item button

$('.show-cart').on("click", ".delete-item", function (event) {
    var id = $(this).data('id')
    shoppingCart.removeItemFromCartAll(id);
    displayCart();
})


// -1
$('.show-cart').on("click", ".minus-item", function (event) {
    var id = $(this).data('id')
    shoppingCart.removeItemFromCart(id);
    displayCart();
})
// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    var id = $(this).data('id')
    shoppingCart.addItemToCart(id);
    displayCart();
})

// Item count input
$('.show-cart').on("change", ".item-count", function (event) {
    var id = $(this).data('id');
    var count = Number($(this).val());
    shoppingCart.setCountForItem(id, count);
    displayCart();
});

displayCart();

