
$('#btnContinue').off('click').on('click', function () {
    window.location.href = "/";


});

$('#btnUpdate').off('click').on('click', function () {
    debugger;
    var listProduct = $('.txtQuantity');
    var cartList = [];
    $.each(listProduct, function (i, item) {
        cartList.push({
            Quantity: $(item).val(),
            Product: {
                ID: $(item).data('id')
            }
        })
    });

    $.ajax({
        url: '/Cart/Update',
        data: { cartModel:JSON.stringify(cartList) } ,
        dataType: 'json',
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                window.location.href = "/gio-hang";
            }
        }
    })
});

$('#btnDeleteAll').off('click').on('click', function () {
    $.ajax({
        url: '/Cart/DeleteAll',
        dataType: 'json',
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                window.location.href = "/gio-hang";
            }
        }
    })
});


$('.DeleteById').off('click').on('click', function () {
      $.ajax({
        url: '/Cart/DeleteById',
        data: { id: $(this).data("id") },
        dataType: 'json',
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                window.location.href = "/gio-hang";
            }
        }
    })
});

$('#btnPayment').off('click').on('click', function () {
    debugger;
    window.location.href = "/thanh-toan";
    
});



