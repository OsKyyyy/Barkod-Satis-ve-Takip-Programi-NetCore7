var Request = {

    Init: function () {
        $("#fetchDataBtn").on("click", function () {
            Request.PriceComparison();
        })
        $("#savePhoto").on("click", function () {
            Request.SavePhoto();
        })
    },
    PriceComparison: function () {
        
        var requestMarket;
        var barcodeNumber = $("#barcodeNumber").val();

        if (barcodeNumber == null || barcodeNumber == undefined || barcodeNumber == "") {

            Request.ToastrError("Barkod Numarası Hatalı veya Boş");
            return;
        }        

        $.each($("#marketItems a"),function (key, value) {
            
            var classList = value.classList;
            var lastElement = classList[classList.length - 1];

            if (lastElement == "active") {

                requestMarket = Request.GetMarketByKeyValue(key);

                var priceComparisonRequestModel = { "Barcode": barcodeNumber };

                $.ajax({
                    type: "POST",
                    url: "PriceComparison?handler=" + requestMarket,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(priceComparisonRequestModel),
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    success: function (response) {
                        
                        if (response.data.length <= 0) {
                            Request.ToastrError("Bu barkodlu ürünün fiyat bilgisi bulunamadı!");
                            return;
                        }

                        $("#listMarketTable").show();

                        $("#productName").html(response.data[0].productName);
                        $("#productLogo").css('background-image', 'url("' + response.data[0].productLogo + '")');
                        $("#savePhoto").attr("data-src", response.data[0].productLogo);

                        $("#barcodeLogo").hide();

                        if (response.data[0].barcodeLogo != "") {
                            $("#barcodeLogo").show();
                            $("#barcodeLogo").attr("src", "https://marketkarsilastir.com/" + response.data[0].barcodeLogo);
                        }

                        var table = $('#kt_datatable_example').DataTable();
                        table.clear().draw();
                        $.each(response.data, function (key, value) {
                            table.row.add([
                                '<img src="' + value.marketLogo + '" style="max-width: 50%;height: auto;">',
                                '' + value.productName +'',
                                '' + value.lastPriceChangeDate + '',
                                '<b>' + value.productPrice + '&#8378</b>',
                            ]).draw(false);
                        })                        
                    },
                })
                return;
            }
        })
    },
    SavePhoto: function () {

        var imgUrl = $("#savePhoto").attr("data-src");
        var barcode = $("#barcodeNumber").val();

        var savePhotoRequestModel = { "ImgUrl": imgUrl, "Barcode": barcode };

        $.ajax({
            type: "POST",
            url: "PriceComparison?handler=SavePhoto",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(savePhotoRequestModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {                

                if (response.status != undefined) {

                    if (response.status) {
                        Request.ToastrSuccess(response.message);
                        return;
                    }

                    Request.ToastrError(response.message);
                    return;
                }

                if (!response) {
                    Request.ToastrError("Bu Barkod Numaralı Ürün Sistemde Bulunamadı");
                }               
            },
        })
    },
    GetMarketByKeyValue: function (key) {

        if (key == 0) {
            return "MarketOne"
        }
        else if (key == 1) {
            return "MarketTwo"
        }
        else if (key == 2) {
            return "MarketThree"
        }
        else if (key == 3) {
            return "MarketFour"
        }
        else if (key == 4) {
            return "MarketFive"
        }
        else {
            return "MarketSix"
        }
    },
    ToastrError: function (msg) {       

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

        toastr.error(msg, "Hata!");        
    },
    ToastrSuccess: function (msg) {

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

        toastr.success(msg, "Başarılı!");        
    },
}
$(document).ready(function () {
    Request.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
})