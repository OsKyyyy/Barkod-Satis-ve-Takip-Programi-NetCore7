var WholeSalerRequest = {

    Init: function () {
        WholeSalerRequest.GetLastWholeSalerWithDebt();      
        WholeSalerRequest.GetLastWholeSalerWithDebtPayment();
        WholeSalerRequest.GetWholeSalerTotalDebt();
        WholeSalerRequest.GetWholeSalerDebt();
        WholeSalerRequest.GetWholeSalerNonPayers();
        WholeSalerRequest.GetWholeSalerThisMonthDebt();
        WholeSalerRequest.GetWholeSalerPreviousMonthDebt();
        WholeSalerRequest.GetWholeSalerMonthlyDebtOfOneYear();
    },

    GetLastWholeSalerWithDebt: function () {       

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetLastWholeSalerWithDebt",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
               
                if (response.status) {

                    if (response.data.length > 0) {
                        var createDate = new Date(response.data[0].createDate);
                        var formattedCreateDate = new Intl.DateTimeFormat('tr-TR', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }).format(createDate);

                        $("#lastWholeSalerWithDebtFirstLetter").html(response.data[0].wholeSalerName.charAt(0).toUpperCase());
                        $("#lastWholeSalerWithDebtFirstLetter").parent().attr("href","/WholeSaler/Movements/" + response.data[0].wholeSalerId);
                        $("#lastWholeSalerWithDebtFullName").html(response.data[0].wholeSalerName);
                        $("#lastWholeSalerWithDebtFullName").attr("href", "/WholeSaler/Movements/" + response.data[0].wholeSalerId);
                        $("#lastWholeSalerWithDebtShoppingDate").html(formattedCreateDate);
                        $("#lastWholeSalerWithDebtShoppingAmount").html((Math.round(response.data[0].amount * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
                    }
                    else {
                        $("#lastWholeSalerWithDebtFirstLetter").html("-");
                        $("#lastWholeSalerWithDebtFullName").html("-");
                        $("#lastWholeSalerWithDebtFullName").attr("href", "");
                        $("#lastWholeSalerWithDebtShoppingDate").html("-");
                        $("#lastWholeSalerWithDebtShoppingAmount").html("-");
                    }
                    
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
    GetLastWholeSalerWithDebtPayment: function () {

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetLastWholeSalerWithDebtPayment",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.status) {

                    if (response.data.length > 0) {
                        var createDate = new Date(response.data[0].createDate);
                        var formattedCreateDate = new Intl.DateTimeFormat('tr-TR', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }).format(createDate);

                        $("#lastWholeSalerWithDebtPaymentFirstLetter").html(response.data[0].wholeSalerName.charAt(0).toUpperCase());
                        $("#lastWholeSalerWithDebtPaymentFirstLetter").parent().attr("href", "/WholeSaler/Movements/" + response.data[0].wholeSalerId);
                        $("#lastWholeSalerWithDebtPaymentFullName").html(response.data[0].wholeSalerName);
                        $("#lastWholeSalerWithDebtPaymentFullName").attr("href", "/WholeSaler/Movements/" + response.data[0].wholeSalerId);
                        $("#lastWholeSalerWithDebtPaymentRefundDate").html(formattedCreateDate);
                        $("#lastWholeSalerWithDebtPaymentRefundAmount").html((Math.round(response.data[0].amount * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
                    }
                    else {
                        $("#lastWholeSalerWithDebtPaymentFirstLetter").html("-");
                        $("#lastWholeSalerWithDebtPaymentFullName").html("-");
                        $("#lastWholeSalerWithDebtPaymentFullName").attr("href", "");
                        $("#lastWholeSalerWithDebtPaymentRefundDate").html("-");
                        $("#lastWholeSalerWithDebtPaymentRefundAmount").html("-");
                    }

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
    GetWholeSalerNonPayers: function () {

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetWholeSalerNonPayers",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.status) {          

                    if (response.data.length > 0) {

                        var i = 0;
                        $.each(response.data, function () {

                            if (i < 5) {

                                if (response.data[i].lastPaymentDate != null) {
                                    var lastPaymentDate = new Date(response.data[i].lastPaymentDate);
                                    var formattedLastPaymentDate = new Intl.DateTimeFormat('tr-TR', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }).format(lastPaymentDate);
                                    $("#wholeSalerNonPayersLastPaymentDate_" + i).html(formattedLastPaymentDate);                                
                                }                               
                                
                                $("#wholeSalerNonPayersFirstLetter_" + i).html(response.data[i].wholeSalerName.charAt(0).toUpperCase());
                                $("#wholeSalerNonPayersFirstLetter_" + i).parent().attr("href", "/WholeSaler/Movements/" + response.data[i].wholeSalerId);

                                $("#wholeSalerNonPayersFullName_" + i).html(response.data[i].wholeSalerName);
                                $("#wholeSalerNonPayersFullName_" + i).attr("href", "/WholeSaler/Movements/" + response.data[i].wholeSalerId);

                                $("#wholeSalerNonPayersTotalDebt_" + i).html("Toplam Borç : " + response.data[i].totalDebt.toFixed(2) + " &#8378;");
                                $("#wholeSalerNonPayersDaysSinceLastPayment_" + i).html(response.data[i].daysSinceLastPayment + " Gün");
                            }
                            else {
                                return false;
                            }
                            i++;
                        })
                              
                    }
                    else {

                    }
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
    GetWholeSalerTotalDebt: function () {

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetWholeSalerTotalDebt",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                if (response.status) {

                    if (response.data != undefined && response.data != "" && response.data != null) {
                        $("#wholeSalerTotalDebt").html((Math.round(response.data.totalDebt * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
                    }
                    else {
                        $("#wholeSalerTotalDebt").html("0,00 &#8378;");
                    }
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
    GetWholeSalerDebt: function () {

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetWholeSalerDebt",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                if (response.status) {

                    const arrayDebt = [];
                    const arrayName = [];

                    if (response.data.length > 0) {

                        var i = 1;
                        $.each(response.data, function(key, value){

                            if (i <= 5) {
                                arrayDebt.push(value.totalDebt);
                                arrayName.push(value.wholeSalerName);
                            }
                            i++;
                        })
                        var newArrayDebt = WholeSalerRequest.CreateArrayWithZeros(arrayDebt);
                        var newArrayName = WholeSalerRequest.CreateArrayWithNames(arrayName);
                        WholeSalerRequest.GetWholeSalerTotalDebtChart(newArrayDebt, newArrayName);
                    }
                    else {
                        WholeSalerRequest.GetWholeSalerTotalDebtChart([0,0,0,0,0], ["-","-","-","-","-"]);
                    }
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
    GetWholeSalerTotalDebtChart: function (arrayDebt, arrayName) {
        var t = function () {
            var t = document.getElementById("kt_charts_widget_27");
            if (t) {
                var a = KTUtil.getCssVariableValue("--bs-gray-300"),
                    l = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                    r = {
                        series: [{ name: "Toplam", data: arrayDebt }],
                        chart: { type: "bar", height: 350, toolbar: { show: !1 } },
                        plotOptions: { bar: { borderRadius: 4, horizontal: !0, distributed: !0, barHeight: 45 } },
                        dataLabels: {
                            enabled: !0,
                            formatter: function (e, t) {   
                                e = e.toFixed(2);
                                return e + " ₺";
                            },
                            style: { fontSize: "15px", fontWeight: "600", align: "left" },
                        },
                        legend: { show: !1 },
                        colors: ["#3E97FF", "#F1416C", "#50CD89", "#FFC700", "#7239EA", "#50CDCD", "#3F4254"],
                        xaxis: {
                            categories: arrayName,
                            labels: {
                                formatter: function (e) {
                                    return e + "₺";
                                },
                                style: { colors: KTUtil.getCssVariableValue("--bs-gray-400"), fontSize: "14px", fontWeight: "600", align: "left" },
                            },
                            axisBorder: { show: !1 },
                        },
                        yaxis: {
                            labels: {
                                style: { colors: KTUtil.getCssVariableValue("--bs-gray-800"), fontSize: "14px", fontWeight: "600" },
                                offsetY: 2,
                                align: "left",
                            },
                        },
                        grid: { borderColor: a, xaxis: { lines: { show: !0 } }, yaxis: { lines: { show: !1 } }, strokeDashArray: 4 },
                    };
                new ApexCharts(t, r).render();
            }
        };
        t();
    },
    GetWholeSalerThisMonthDebt: function () {

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetWholeSalerThisMonthDebt",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.status) {
                    
                    if (response.data != undefined && response.data != "" && response.data != null) {
                        $("#wholeSalerThisMonthDebt").html((Math.round(response.data.totalDebt * 100) / 100).toFixed(2).replace(".", ","));
                    }
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
    GetWholeSalerPreviousMonthDebt: function () {

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetWholeSalerPreviousMonthDebt",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.status) {

                    if (response.data != undefined && response.data != "" && response.data != null) {
                        $("#wholeSalerPreviousMonthDebt").html((Math.round(response.data.totalDebt * 100) / 100).toFixed(2).replace(".", ","));
                    }
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
    GetWholeSalerMonthlyDebtOfOneYear: function () {

        $.ajax({
            type: "POST",
            url: "WholeSalerReport?handler=GetWholeSalerMonthlyDebtOfOneYear",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                if (response.status) {

                    const arrayMonths = [""];
                    const arrayValues = [0];

                    if (response.data.length > 0) {
                        $.each(response.data, function (key, value) {
                            
                            var monthName = WholeSalerRequest.GenerateMonthName(value.month);
                            arrayMonths.push(monthName);
                            arrayValues.push(value.totalDebt);
                        })
                        WholeSalerRequest.GetWholeSalerMonthlyDebtChart(arrayMonths, arrayValues);
                    }                       
                    else {
                        WholeSalerRequest.GetWholeSalerMonthlyDebtChart([0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], ["", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-"]);
                    }
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
    GetWholeSalerMonthlyDebtChart: function (arrayMonths, arrayValues) {
        var t = function () {
            var t = document.getElementById("kt_charts_widget_20");
            if (t) {
                var a = parseInt(KTUtil.css(t, "height")),
                    l = KTUtil.getCssVariableValue("--bs-gray-500"),
                    r = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                    o = KTUtil.getCssVariableValue("--bs-danger"),
                    i = KTUtil.getCssVariableValue("--bs-danger"),
                    s = {
                        series: [{ name: "Toplam", data: arrayValues }],
                        chart: { fontFamily: "inherit", type: "area", height: a, toolbar: { show: !1 } },
                        plotOptions: {},
                        legend: { show: !1 },
                        dataLabels: {
                            enabled: !1,
                            formatter: function (e, t) {
                                e = e.toFixed(2);
                                return e + " ₺";
                            },
                            style: { fontSize: "15px", fontWeight: "600", align: "left" },
                        },
                        fill: { type: "gradient", gradient: { shadeIntensity: 1, opacityFrom: 0.4, opacityTo: 0, stops: [0, 80, 100] } },
                        stroke: { curve: "smooth", show: !0, width: 3, colors: [o] },
                        xaxis: {
                            categories: arrayMonths,
                            axisBorder: { show: !1 },
                            axisTicks: { show: !1 },
                            tickAmount: 6,

                            labels: { rotate: 0, rotateAlways: !0, style: { colors: l, fontSize: "12px" } },
                            crosshairs: { position: "front", stroke: { color: o, width: 1, dashArray: 3 } },
                            tooltip: { enabled: !0, formatter: void 0, offsetY: 0, style: { fontSize: "12px" } },
                        },
                        yaxis: {                            
                            tickAmount: 6,
                            labels: {
                                style: { colors: l, fontSize: "12px" },
                                formatter: function (e) {
                                    return e + " ₺";
                                },
                            },
                        },
                        states: { normal: { filter: { type: "none", value: 0 } }, hover: { filter: { type: "none", value: 0 } }, active: { allowMultipleDataPointsSelection: !1, filter: { type: "none", value: 0 } } },
                        tooltip: {
                            style: { fontSize: "12px" },
                            y: {
                                formatter: function (e) {
                                    return e + "₺";
                                },
                            },
                        },
                        colors: [i],
                        grid: { borderColor: r, strokeDashArray: 4, yaxis: { lines: { show: !0 } } },
                        markers: { strokeColor: o, strokeWidth: 3 },
                    };
                new ApexCharts(t, s).render();
            }
        };
        t();
    },
    CreateArrayWithZeros: function (array) {

        if (array.length >= 5) {
            return array;
        }
        else {
            var missingElements = 5 - array.length;

            for (var i = 0; i < missingElements; i++) {
                array.push(0);
            }

            return array;
        }
    },
    CreateArrayWithNames: function (array) {

        if (array.length >= 5) {
            return array;
        }
        else {
            var missingElements = 5 - array.length;

            for (var i = 0; i < missingElements; i++) {
                array.push("-");
            }

            return array;
        }
    },
    GenerateMonthName: function (monthNumber) {

        const months = [
            "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
            "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
        ];

        return months[monthNumber - 1];
    }
}
$(document).ready(function () {
    WholeSalerRequest.Init();
});