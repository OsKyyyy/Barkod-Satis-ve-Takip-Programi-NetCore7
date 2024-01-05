﻿var Pos = {

    Init: function () {
        Pos.ToastrError();
        Pos.ToastrSuccess();

        $("#barcodeNo").on("keyup", function (event) {

            var value = $(this).val();
            if (event.keyCode == 120) {
                if (value.charAt(0) == "*") {
                    var substr = value.substr(1);
                    if (substr != "") {
                        $("#quantity").val(substr);
                        $("#barcodeNo").val("");
                    }
                }
            }
        })
        
        if (localStorage.getItem("basket") != null) {
            
            if (localStorage.getItem("basket") == 1) {
                $("#basket_1_item").trigger("click");

                $("a[href='#basket_1']").addClass("active");
                $("a[href='#basket_2']").removeClass("active");

                $("#basket_1").addClass("active show");
                $("#basket_2").removeClass("active show");
            }
            else {
                $("#basket_2_item").trigger("click");

                $("a[href='#basket_1']").removeClass("active");
                $("a[href='#basket_2']").addClass("active");

                $("#basket_1").removeClass("active show");
                $("#basket_2").addClass("active show");
            }
        }
        else {
            $("#basket_1_item").trigger("click");

            $("a[href='#basket_1']").addClass("active");
            $("a[href='#basket_2']").removeClass("active");

            $("#basket_1").addClass("active show");
            $("#basket_2").removeClass("active show");
        }
    },

    ToastrError: function () {

        let msg = $('#ToastrError').val();
        if (msg == "Ürün Bulunamadı") {
            var audio = new Audio("../assets/media/music/barcode-beep.m4a")
            audio.play()                       
        }
        if (msg != undefined && msg.length > 0) {

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
        }
    },
    ToastrSuccess: function () {

        let msg = $('#ToastrSuccess').val();
        if (msg != undefined && msg.length > 0) {

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
        }
    },    
    ChangeBasket: function (value) {

        if (value == 1) {
            localStorage.removeItem("basket");
            localStorage.setItem("basket", "1");

            $("#currentBasket").val(1);
            $("#currentBasketModal").val(1);

            $("#caseTotal").addClass("d-block");
            $("#caseTotal").removeClass("d-none");
            $("#caseTotal2").addClass("d-none");
            $("#caseTotal2").removeClass("d-block");

            $("#extraMoney").addClass("d-block");
            $("#extraMoney").removeClass("d-none");
            $("#extraMoney2").addClass("d-none");
            $("#extraMoney2").removeClass("d-block");

            $("#receivedMoneyNumpadSpan").addClass("d-block");
            $("#receivedMoneyNumpadSpan").removeClass("d-none");
            $("#receivedMoneyNumpadSpan2").addClass("d-none");
            $("#receivedMoneyNumpadSpan2").removeClass("d-block");

            $("#remainderMoneyNumpadSpan").addClass("d-block");
            $("#remainderMoneyNumpadSpan").removeClass("d-none");
            $("#remainderMoneyNumpadSpan2").addClass("d-none");
            $("#remainderMoneyNumpadSpan2").removeClass("d-block");

        }
        else {
            localStorage.removeItem("basket");
            localStorage.setItem("basket", "2");

            $("#currentBasket").val(2);
            $("#currentBasketModal").val(2);

            $("#caseTotal").removeClass("d-block");
            $("#caseTotal").addClass("d-none");
            $("#caseTotal2").addClass("d-block");
            $("#caseTotal2").removeClass("d-none");

            $("#extraMoney").removeClass("d-block");
            $("#extraMoney").addClass("d-none");
            $("#extraMoney2").addClass("d-block");
            $("#extraMoney2").removeClass("d-none");

            $("#receivedMoneyNumpadSpan").removeClass("d-block");
            $("#receivedMoneyNumpadSpan").addClass("d-none");
            $("#receivedMoneyNumpadSpan2").addClass("d-block");
            $("#receivedMoneyNumpadSpan2").removeClass("d-none");

            $("#remainderMoneyNumpadSpan").removeClass("d-block");
            $("#remainderMoneyNumpadSpan").addClass("d-none");
            $("#remainderMoneyNumpadSpan2").addClass("d-block");
            $("#remainderMoneyNumpadSpan2").removeClass("d-none");

        }
    }
}
$(document).ready(function () {
    Pos.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
})