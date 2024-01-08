var Request_ = {
    Init: function () {     
        $("#findPrice").on('shown.bs.modal', function () {
            $('#findPriceBarcodeNo').focus();
        });
        $("#findPrice").on('hidden.bs.modal', function () {
            $("#findPriceBarcodeNo").val("");
            $("#findPriceTable").hide();
            $("#barcodeNo").focus();
        });        
    },
    FindPrice: function () {
        
        var value = $("#findPriceBarcodeNo").val();

        if (value == "") {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toastr-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };

            toastr.error("Lütfen Barkod Numarası Girin", "Hata!");

            return;
        }

        var findPriceModel = { "Barcode": value };

        $.ajax({
            type: "POST",
            url: "Sale?handler=FindPrice",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(findPriceModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                if (response.status) {
                    $("#findPriceTable").show();
                    $("#findPriceTableProductName").html(response.data.name);
                    $("#findPriceTableBarcode").html(response.data.barcode);
                    $("#findPriceTableSalePrice").html((Math.round(response.data.salePrice * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
                }
                if (response.message == "Authentication Error") {
                    window.location.reload();
                }
                if (!response.status) {

                    if (response.message == "Ürün Bulunamadı") {
                        var audio = new Audio("../assets/media/music/barcode-beep.m4a")
                        audio.play()
                    }
                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": true,
                        "positionClass": "toastr-top-right",
                        "preventDuplicates": false,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    };

                    toastr.error(response.message, "Hata!");
                }
            },        
        })
    },
    AddAjax: function (method, counter) {

        basket = localStorage.getItem("basket");
        
        if (method == 1) {
            var value = $("#findPriceTableBarcode").html();
            var addModel = { "Barcode": value, "Basket": basket };
        }
        else {
            var barcode = $("#favoriteAddBarcode_" + counter).val();
            var quantity = $("#favoriteAddQuantity_" + counter).val();
            var addModel = { "Barcode": barcode, "Quantity": quantity, "Basket": basket };
        }

        $.ajax({
            type: "POST",
            url: "Sale?handler=AddAjax",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(addModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.message == "Authentication Error" || response.status) {
                    window.location.reload();
                }
                else {
                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": true,
                        "positionClass": "toastr-top-right",
                        "preventDuplicates": false,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    };

                    toastr.error(response.message, "Hata!");
                }
            },
        })

    },
    CancelSale: function () {

        basket = localStorage.getItem("basket");
        var data = { basket: parseInt(basket) };

        $.ajax({
            type: "POST",
            url: "Sale?handler=CancelSale",
            contentType: 'application/x-www-form-urlencoded',
            data: data,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {                
                if (!response.status) {
                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": true,
                        "positionClass": "toastr-top-right",
                        "preventDuplicates": false,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    };

                    toastr.error(response.message, "Hata!");
                    return;
                }
                else {
                    if (response.status) {
                        if (basket == 1) {
                            localStorage.removeItem("customerId");
                            localStorage.removeItem("customerInfo");
                        }
                        else {
                            localStorage.removeItem("customerId2");
                            localStorage.removeItem("customerInfo2");
                        }
                    }
                    window.location.reload();
                }
            },
        })
    }
}
$(document).ready(function () {
    Request_.Init();
})