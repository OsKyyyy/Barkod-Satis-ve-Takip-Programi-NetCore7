var CustomerRequest = {

    Init: function () {
        CustomerRequest.GetLastCustomerWithDebt();      
        CustomerRequest.GetLastCustomerWithDebtPayment();
        CustomerRequest.GetCustomerTotalDebt();
        CustomerRequest.GetCustomerDebt();
        CustomerRequest.GetCustomerNonPayers();
    },

    GetLastCustomerWithDebt: function () {       

        $.ajax({
            type: "POST",
            url: "CustomerReport?handler=GetLastCustomerWithDebt",
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

                        $("#lastCustomerWithDebtFirstLetter").html(response.data[0].customerName.charAt(0).toUpperCase());
                        $("#lastCustomerWithDebtFirstLetter").parent().attr("href","/Customer/Movements/" + response.data[0].customerId);
                        $("#lastCustomerWithDebtFullName").html(response.data[0].customerName);
                        $("#lastCustomerWithDebtFullName").attr("href", "/Customer/Movements/" + response.data[0].customerId);
                        $("#lastCustomerWithDebtShoppingDate").html(formattedCreateDate);
                        $("#lastCustomerWithDebtShoppingAmount").html((Math.round(response.data[0].amount * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
                    }
                    else {
                        $("#lastCustomerWithDebtFirstLetter").html("-");
                        $("#lastCustomerWithDebtFullName").html("-");
                        $("#lastCustomerWithDebtFullName").attr("href", "");
                        $("#lastCustomerWithDebtShoppingDate").html("-");
                        $("#lastCustomerWithDebtShoppingAmount").html("-");
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
    GetLastCustomerWithDebtPayment: function () {

        $.ajax({
            type: "POST",
            url: "CustomerReport?handler=GetLastCustomerWithDebtPayment",
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
                        var createDate = new Date(response.data[0].updateDate);
                        var formattedCreateDate = new Intl.DateTimeFormat('tr-TR', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }).format(createDate);

                        $("#lastCustomerWithDebtPaymentFirstLetter").html(response.data[0].customerName.charAt(0).toUpperCase());
                        $("#lastCustomerWithDebtPaymentFirstLetter").parent().attr("href", "/Customer/Movements/" + response.data[0].customerId);
                        $("#lastCustomerWithDebtPaymentFullName").html(response.data[0].customerName);
                        $("#lastCustomerWithDebtPaymentFullName").attr("href", "/Customer/Movements/" + response.data[0].customerId);
                        $("#lastCustomerWithDebtPaymentRefundDate").html(formattedCreateDate);
                        $("#lastCustomerWithDebtPaymentRefundAmount").html((Math.round(response.data[0].amount * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
                    }
                    else {
                        $("#lastCustomerWithDebtPaymentFirstLetter").html("-");
                        $("#lastCustomerWithDebtPaymentFullName").html("-");
                        $("#lastCustomerWithDebtPaymentFullName").attr("href", "");
                        $("#lastCustomerWithDebtPaymentRefundDate").html("-");
                        $("#lastCustomerWithDebtPaymentRefundAmount").html("-");
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
    GetCustomerNonPayers: function () {

        $.ajax({
            type: "POST",
            url: "CustomerReport?handler=GetCustomerNonPayers",
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
                            debugger;
                            if (i < 5) {

                                var lastPaymentDate = new Date(response.data[i].lastPaymentDate);
                                var formattedLastPaymentDate = new Intl.DateTimeFormat('tr-TR', { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }).format(lastPaymentDate);

                                $("#customerNonPayersFirstLetter_" + i).html(response.data[i].customerName.charAt(0).toUpperCase());
                                $("#customerNonPayersFirstLetter_" + i).parent().attr("href", "/Customer/Movements/" + response.data[i].customerId);

                                $("#customerNonPayersFullName_" + i).html(response.data[i].customerName);
                                $("#customerNonPayersFullName_" + i).attr("href", "/Customer/Movements/" + response.data[i].customerId);

                                $("#customerNonPayersTotalDebt_" + i).html("Toplam Borç : " + response.data[i].totalDebt + " &#8378;");
                                $("#customerNonPayersDaysSinceLastPayment_" + i).html(response.data[i].daysSinceLastPayment + " Gün");
                                $("#customerNonPayersLastPaymentDate_" + i).html(formattedLastPaymentDate);                                
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
    GetCustomerTotalDebt: function () {

        $.ajax({
            type: "POST",
            url: "CustomerReport?handler=GetCustomerTotalDebt",
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
                        $("#customerTotalDebt").html((Math.round(response.data.totalDebt * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
                    }
                    else {
                        $("#customerTotalDebt").html("0,00 &#8378;");
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
    GetCustomerDebt: function () {

        $.ajax({
            type: "POST",
            url: "CustomerReport?handler=GetCustomerDebt",
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
                                arrayName.push(value.customerName);
                            }
                            i++;
                        })
                        var newArrayDebt = CustomerRequest.CreateArrayWithZeros(arrayDebt);
                        var newArrayName = CustomerRequest.CreateArrayWithNames(arrayName);
                        CustomerRequest.GetCustomerTotalDebtChart(newArrayDebt, newArrayName);
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
    GetCustomerTotalDebtChart: function (arrayDebt, arrayName) {
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
    }
}
$(document).ready(function () {
    CustomerRequest.Init();
});