var RequestTracking = {

    Init: function () {
        RequestTracking.MarketList();
    },
    MarketList: function () {
         
        $.ajax({
            type: "GET",
            url: "PriceTracking?handler=MarketList",
            contentType: "application/json; charset=utf-8",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                $.each(response.data, function (key, value) {

                    var marketUrl = value.marketUrl.split("market/")[1];

                    $('#marketItems').append(
                        '<li class="nav-item mb-3 me-0" role="presentation">' +
                        '<a href="/WebScraping/PriceTrackingMarket/' + marketUrl +'" class="nav-link nav-link-border-solid btn btn-outline btn-flex btn-active-color-primary flex-column flex-stack pt-9 pb-7 page-bg show" style="width: 204px;height: 180px" aria-selected="true" role="tab">' +
                        '<div class="nav-icon mb-3">' +
                        '<img src="' + value.marketLogo+'" class="w-75px" alt="">'+
                        '</div>' +
                        '<span class="text-gray-800 fw-bold fs-3 d-block">' + value.marketName + '</span>'+
                        '</a>' +
                        '</li>'
                    );       
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
    RequestTracking.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
})