var RequestProduceMarket = {

    Init: function () {
        RequestProduceMarket.List();
    },
    List: function () {

        $.ajax({
            type: "GET",
            url: "PriceProduceMarket?handler=List",
            contentType: "application/json; charset=utf-8",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
               
                if (response.data.length <= 0) {

                    Request.ToastrError("Sebze ve Meyve Hal fiyatları getirilemedi!");
                    return;
                }

                var table = $('#kt_datatable_example').DataTable();
                table.clear().draw();
                $.each(response.data, function (key, value) {

                    if (value.productName != "" && value.productName != undefined && value.productName != null) {

                        table.row.add([
                            '' + value.productName + '',
                            '' + value.unitName + '',                            
                            '<b>' + value.productPrice + '&#8378</b>',
                        ]).draw(false);
                    }                   
                })            

                $("#pageLoader").attr("style", "display:none");
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
    RequestProduceMarket.Init();
});

$(window).on("load", function () {
    //$("#pageLoader").attr("style", "display:none");
})