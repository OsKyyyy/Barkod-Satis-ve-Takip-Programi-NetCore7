var RequestTrackingMarket = {

    Init: function () {
        RequestTrackingMarket.MarketList();
    },
    MarketList: function () {

        var marketName = window.location.href.split("PriceTrackingMarket/")[1];

        var model = { "MarketName": marketName };

        $.ajax({
            type: "POST",
            url: "PriceTrackingMarket?handler=Market",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(model),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                if (response.data.length == 0) {
                    $("#marketInfo").html("Bu market ile ilgili fiyat artışı veya indirimi olan ürün bulunamadı");
                    return;
                }

                $("#marketInfo").html(response.data[0].marketInfo);

                $.each(response.data, function (key, value) {                    

                    if (value.productName != "") {

                        var oldPrice = parseFloat(value.oldPrice.split("Eski Fiyatı :")[1].trim());
                        var newPrice = parseFloat(value.newPrice.split("Yeni Fiyatı :")[1].trim());
                        var color = "danger";
                        var arrow = "up";
                        var rate;

                        if (newPrice > oldPrice) {
                            rate = ((newPrice - oldPrice) / oldPrice) * 100;
                            rate = rate.toFixed(2) + '%';
                        }
                        else {
                            rate = ((oldPrice - newPrice) / oldPrice) * 100;
                            rate = rate.toFixed(2) + '%';
                            color = "success";
                            arrow = "down";
                        }

                        $('#productItems').append(
                            '<li class="nav-item mb-3 me-0" role="presentation">' +
                            '<a class="nav-link nav-link-border-solid btn btn-outline btn-flex btn-active-color-primary flex-column flex-stack pt-9 pb-7 page-bg show" style="width: 250px;height: 350px;justify-content: space-around !important;" aria-selected="true" role="tab">' +
                            '<div class="nav-icon mb-3">' +
                            '<img src="' + value.productLogo + '" class="w-75px" alt="">' +
                            '</div>' +
                            '<span class="text-gray-800 fw-bold fs-3 d-block">' + value.productName + '</span><br>' +
                            '<span class="badge badge-light-' + color + ' fs-base"><i class="ki-duotone ki-arrow-' + arrow + ' fs-5 text-' + color + ' ms-n1"><span class="path1"></span><span class="path2"></span></i>' + rate + '</span>'+
                            '<span class="fw-bold fs-4 d-block"><span class="text-success ">' + value.newPrice + '</span><br><span class="text-danger ">' + value.oldPrice + '</span></span>' +
                            '</a>' +
                            '</li>'
                        );
                    }                   
                })
            },
        })
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
    RequestTrackingMarket.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
})