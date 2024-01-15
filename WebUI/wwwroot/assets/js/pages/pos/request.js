﻿var Request_ = {
    Init: function () {     
        $("#findPrice").on('shown.bs.modal', function () {
            $('#findPriceBarcodeNo').focus();
        });
        $("#findPrice").on('hidden.bs.modal', function () {
            $("#findPriceBarcodeNo").val("");
            $("#findPriceTable").hide();
            $("#barcodeNo").focus();
        });  

        $("#searchProduct").on('shown.bs.modal', function () {
            $('#searchProductName').focus();
        });
        $("#searchProduct").on('hidden.bs.modal', function () {
            $("#searchProductName").val("");
            $("#searchProductTable").hide();
            $("#barcodeNo").focus();
        });
    },
    SearchProduct: function () {

        var value = $("#searchProductName").val();

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

            toastr.error("Lütfen Ürün Adı Girin", "Hata!");

            return;
        }

        var searchProductModel = { "Name": value };

        $.ajax({
            type: "POST",
            url: "Sale?handler=SearchProduct",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(searchProductModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                if (response.status && response.data.length > 0) {
                    Request_.CreateTable(response.data);                
                    $("#searchProductTable").show();
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
        else if (method == 2) {
            var barcode = $("#favoriteAddBarcode_" + counter).val();
            var quantity = $("#favoriteAddQuantity_" + counter).val();
            var addModel = { "Barcode": barcode, "Quantity": quantity, "Basket": basket };
        }
        else {
            var value = $("#searchProductTableBarcode_" + counter).html();
            var addModel = { "Barcode": value, "Basket": basket };
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
    ComplateSale: function () {
        
        var customerId;
        var amount;
        var paymentType = parseInt($('input[name="paymentType"]:checked').val());
        var basket = localStorage.getItem("basket");

        if (basket == 1) {
            customerId = parseInt(localStorage.getItem("customerId"));
            amount = $("#caseTotal").html().split(" ")[0].replace(",", ".");
        }
        else {
            customerId = parseInt(localStorage.getItem("customerId2"));
            amount = $("#caseTotal2").html().split(" ")[0].replace(",", ".");
        }

        if (amount > 1) {

            if (paymentType == 1) { // NAKİT ÖDEME YÖNETİMİ

                var saleModel = { "Basket": basket, "CustomerId": customerId, "Amount": amount, "PaymentType": paymentType };

                $.ajax({
                    type: "POST",
                    url: "Sale?handler=ComplateSale",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(saleModel),
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    success: function (response) {
                        
                        if (response.message == "Authentication Error") {

                            window.location.reload();
                        }
                        if (response.status) {

                            if (customerId != null && basket == 1) {
                                localStorage.removeItem("customerId");
                            }
                            if (customerId != null && basket == 2) {
                                localStorage.removeItem("customerId2");
                            }

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
            }
        }
                
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
    },
    CancelSaleConfirm: function () {

        Swal.fire({
            title: 'Emin misin?',
            text: 'Satışı iptal etmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: "Onaylıyorum, İptal Et",
            cancelButtonText: 'Vazgeç',
            customClass: {
                confirmButton: 'btn btn-primary',
                cancelButton: 'btn btn-danger'
            }
        }).then((willDelete) => {

            if (willDelete.isConfirmed) {
                Request_.CancelSale();
            }
        });
    },
    CreateTable: function (data) {
        $("#searchProductTBody").html("");
        var i = 0;
        $.each(data, function (index, value) {
            i++;
            var salePrice = (Math.round(value.salePrice * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;"
            var row =
                "<tr>" +
                    "<td class='pe-0'>" +
                        "<div class='d-flex align-items-center'>" +
                            "<span class='fw-bold text-gray-600 fs-3 me-1' >" + value.name + "</span>" +
                        "</div>" +
                    "</td>"+
                    "<td class='pe-0'>"+
                        "<div class='d-flex align-items-center'>" +
                            "<span class='fw-bold text-gray-600 fs-3 me-1' id='searchProductTableBarcode_" + i + "'>" + value.barcode +"</span>" +
                        "</div>" +
                    "</td>" +
                    "<td class='pe-0'>" +
                        "<div class='d-flex align-items-center'>" +
                            "<span class='fw-bold text-gray-600 fs-1 me-1'>" + salePrice +"</span>" +
                        "</div>" +
                    "</td>" +
                    "<td class='pe-0'>" +
                        "<button class='btn btn-light me-2 mb-2 d-flex align-items-center justify-content-center' onclick=' Request_.AddAjax(3," + i + ")'>"+
                            "<i class='ki-duotone ki-handcart fs-1'></i>"+
                        "</button>" +
                    "</td>" +
                "</tr>";
            $("#searchProductTBody").append(row);
        })
    },
}
$(document).ready(function () {
    Request_.Init();
})