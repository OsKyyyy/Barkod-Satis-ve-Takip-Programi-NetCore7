var Request = {
    Init: function () {     
        $("#findPrice").on('shown.bs.modal', function () {
            $('#findPriceBarcodeNo').focus();
        });
        $("#findPrice").on('hidden.bs.modal', function () {
            $("#findPriceBarcodeNo").val("");
            $("#findPriceTable").hide();
            $("#barcodeNo").focus();
        });
        $("#barcodeNo").on("keyup", function (event) {
            
            var value = $(this).val();
            if (event.keyCode == 39) {
                if (value.charAt(0) == "*") {
                    var substr = value.substr(1);
                    if (substr != "") {
                        $("#quantity").val(substr);
                        $("#barcodeNo").val("");
                    }
                }
            }            
        })
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
    AddAjax: function (method,counter) {
        debugger;
        if (method == 1) {
            var value = $("#findPriceTableBarcode").html();
            var addModel = { "Barcode": value };
        }
        else {
            var barcode = $("#favoriteAddBarcode_" + counter).val();
            var quantity = $("#favoriteAddQuantity_" + counter).val();
            var addModel = { "Barcode": barcode, "Quantity": quantity };
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
    
}
$(document).ready(function () {
    Request.Init();
})