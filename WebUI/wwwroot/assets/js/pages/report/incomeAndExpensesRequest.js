var IncomeAndExpensesRequest = {

    MonthlyExternalIncomeValue: 0,
    MonthlyExternalExpensesValue: 0,
    MonthlySalesIncomeValue: 0,
    MonthlyWholeSalerExpensesValue: 0,

    MonthlyExternalIncomeValuePrevious: 0,
    MonthlyExternalExpensesValuePrevious: 0,
    MonthlySalesIncomeValuePrevious: 0,
    MonthlyWholeSalerExpensesValuePrevious: 0,

    YearlyExternalIncomeArray: [],
    YearlyExternalExpensesArray: [],
    YearlySalesIncomeArray: [],
    YearlyWholeSalerExpensesArray: [],

    Init: async function () {
        await IncomeAndExpensesRequest.MonthlyExternalIncome();              
        await IncomeAndExpensesRequest.MonthlyExternalExpenses();  
        await IncomeAndExpensesRequest.MonthlySalesIncome();
        await IncomeAndExpensesRequest.MonthlyWholeSalerExpenses();        
        await IncomeAndExpensesRequest.YearlyExternalIncome();
        await IncomeAndExpensesRequest.YearlyExternalExpenses();
        await IncomeAndExpensesRequest.YearlySalesIncome();
        await IncomeAndExpensesRequest.YearlyWholeSalerExpenses();
        await IncomeAndExpensesRequest.YearlyIncomeExpensesChart();
        await IncomeAndExpensesRequest.MonthlyExternalIncomeChart();
        await IncomeAndExpensesRequest.MonthlyExternalExpensesChart();
        await IncomeAndExpensesRequest.MonthlySalesIncomeChart();
        await IncomeAndExpensesRequest.MonthlyEstimatedIncomeChart();
        await IncomeAndExpensesRequest.MonthlyEstimatedExpensesChart();
        await IncomeAndExpensesRequest.MonthlyEstimatedEarningChart();
                     
        var estimatedIncome = parseFloat(IncomeAndExpensesRequest.MonthlyExternalIncomeValue) + parseFloat(IncomeAndExpensesRequest.MonthlySalesIncomeValue);
        var estimatedExpenses = parseFloat(IncomeAndExpensesRequest.MonthlyExternalExpensesValue) + parseFloat(IncomeAndExpensesRequest.MonthlyWholeSalerExpensesValue);
        var estimatedEarning = (estimatedIncome - estimatedExpenses) === 0 ? 1 : (estimatedIncome - estimatedExpenses);

        var estimatedIncomePrevious = parseFloat(IncomeAndExpensesRequest.MonthlyExternalIncomeValuePrevious) + parseFloat(IncomeAndExpensesRequest.MonthlySalesIncomeValuePrevious);
        var estimatedExpensesPrevious = parseFloat(IncomeAndExpensesRequest.MonthlyExternalExpensesValuePrevious) + parseFloat(IncomeAndExpensesRequest.MonthlyWholeSalerExpensesValuePrevious);       
        var estimatedEarningPrevious = (estimatedIncomePrevious - estimatedExpensesPrevious) === 0 ? 1 : (estimatedIncomePrevious - estimatedExpensesPrevious);

        $("#monthlyEstimatedIncomeInput").html(estimatedIncome.toFixed(2) + " &#8378;");
        $("#monthlyEstimatedExpensesInput").html(estimatedExpenses.toFixed(2) + " &#8378;");
        $("#monthlyEstimatedEarningInput").html(estimatedEarning.toFixed(2) + " &#8378;")

        if (estimatedEarning.toString().indexOf("-") != -1) {
            $("#monthlyEstimatedEarningInput").addClass("text-danger");
        }
        else {
            $("#monthlyEstimatedEarningInput").addClass("text-success");
        }
        
        if (estimatedIncome > estimatedIncomePrevious) {
            rate = ((estimatedIncome - estimatedIncomePrevious) / estimatedIncome) * 100;
            rate = rate = Math.abs(rate);
            rate = rate.toFixed(2) + '%';
            $("#monthlyEstimatedIncomeCardBody").append(
                '<span class="badge badge-light-success fs-base">' +
                '<i class="ki-duotone ki-arrow-up fs-5 text-success ms-n1">' +
                '<span class="path1"></span>' +
                '<span class="path2"></span>' +
                '</i>' + rate +
                '</span > '
            );
        }
        else {
            rate = ((estimatedIncomePrevious - estimatedIncome) / estimatedIncomePrevious) * 100;
            rate = rate = Math.abs(rate);
            rate = rate.toFixed(2) + '%';
            $("#monthlyEstimatedIncomeCardBody").append(
                '<span class="badge badge-light-danger fs-base">' +
                '<i class="ki-duotone ki-arrow-down fs-5 text-danger ms-n1">' +
                '<span class="path1"></span>' +
                '<span class="path2"></span>' +
                '</i>' + rate +
                '</span > '
            );
        }
        
        if (estimatedExpenses > estimatedExpensesPrevious) {
            rate = ((estimatedExpenses - estimatedExpensesPrevious) / estimatedExpenses) * 100;
            rate = rate = Math.abs(rate);
            rate = rate.toFixed(2) + '%';
            $("#monthlyEstimatedExpensesCardBody").append(
                '<span class="badge badge-light-danger fs-base">' +
                '<i class="ki-duotone ki-arrow-up fs-5 text-danger ms-n1">' +
                '<span class="path1"></span>' +
                '<span class="path2"></span>' +
                '</i>' + rate +
                '</span > '
            );
        }
        else {
            rate = ((estimatedExpensesPrevious - estimatedExpenses) / estimatedExpensesPrevious) * 100;
            rate = rate = Math.abs(rate);
            rate = rate.toFixed(2) + '%';
            $("#monthlyEstimatedExpensesCardBody").append(
                '<span class="badge badge-light-success fs-base">' +
                '<i class="ki-duotone ki-arrow-down fs-5 text-success ms-n1">' +
                '<span class="path1"></span>' +
                '<span class="path2"></span>' +
                '</i>' + rate +
                '</span > '
            );
        }
        
        if (estimatedEarning > estimatedEarningPrevious) {
            rate = ((estimatedEarning - estimatedEarningPrevious) / estimatedEarning) * 100;
            rate = rate = Math.abs(rate);
            rate = rate.toFixed(2) + '%';
            $("#monthlyEstimatedEarningCardBody").append(
                '<span class="badge badge-light-success fs-base">' +
                '<i class="ki-duotone ki-arrow-up fs-5 text-success ms-n1">' +
                '<span class="path1"></span>' +
                '<span class="path2"></span>' +
                '</i>' + rate +
                '</span > '
            );
        }
        else {
            rate = ((estimatedEarningPrevious - estimatedEarning) / estimatedEarningPrevious) * 100;
            rate = rate = Math.abs(rate);
            rate = rate.toFixed(2) + '%';
            $("#monthlyEstimatedEarningCardBody").append(
                '<span class="badge badge-light-danger fs-base">' +
                '<i class="ki-duotone ki-arrow-down fs-5 text-danger ms-n1">' +
                '<span class="path1"></span>' +
                '<span class="path2"></span>' +
                '</i>' + rate +
                '</span > '
            );
        }
    },

    MonthlyExternalIncome: function () {    
        
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=MonthlyExternalIncome",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {                    

                    if (response.data.total > 0) {
                        $("#monthlyExternalIncomeInput").html(response.data.total.toFixed(2) + " &#8378;");
                        IncomeAndExpensesRequest.MonthlyExternalIncomeValue = response.data.total;
                    }
                    IncomeAndExpensesRequest.MonthlyExternalIncomeValuePrevious = response.data.totalPrevious;

                    var color = "success";
                    var arrow = "up";
                    var rate;

                    if (response.data.total > response.data.totalPrevious) {
                        rate = ((response.data.total - response.data.totalPrevious) / response.data.total) * 100;
                        rate = rate = Math.abs(rate);
                        rate = rate.toFixed(2) + '%';
                    }
                    else {
                        rate = ((response.data.totalPrevious - response.data.total) / response.data.totalPrevious) * 100;
                        rate = rate = Math.abs(rate);
                        rate = rate.toFixed(2) + '%';
                        color = "danger";
                        arrow = "down";
                    }

                    $("#monthlyExternalIncomeCardBody").append(
                        '<span class="badge badge-light-' + color + ' fs-base">' +
                        '<i class="ki-duotone ki-arrow-' + arrow + ' fs-5 text-' + color + ' ms-n1">' +
                        '<span class="path1"></span>' +
                        '<span class="path2"></span>' +
                        '</i>' + rate +
                        '</span > '
                    );

                    resolve();
                },
            })
        });
    },    
    MonthlyExternalExpenses: function () {
        
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=MonthlyExternalExpenses",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    
                    if (response.data.total > 0) {
                        $("#monthlyExternalExpensesInput").html(response.data.total.toFixed(2) + " &#8378;");
                        IncomeAndExpensesRequest.MonthlyExternalExpensesValue = response.data.total;
                    }
                    IncomeAndExpensesRequest.MonthlyExternalExpensesValuePrevious = response.data.totalPrevious;

                    var color = "danger";
                    var arrow = "up";
                    var rate;

                    if (response.data.total > response.data.totalPrevious) {
                        rate = ((response.data.total - response.data.totalPrevious) / response.data.total) * 100;
                        rate = rate = Math.abs(rate);
                        rate = rate.toFixed(2) + '%';
                    }
                    else {
                        rate = ((response.data.totalPrevious - response.data.total) / response.data.totalPrevious) * 100;                        
                        rate = rate = Math.abs(rate);
                        rate = rate.toFixed(2) + '%';
                        color = "success";
                        arrow = "down";
                    }

                    $("#monthlyExternalExpensesCardBody").append(
                        '<span class="badge badge-light-' + color + ' fs-base">' +
                        '<i class="ki-duotone ki-arrow-' + arrow + ' fs-5 text-' + color + ' ms-n1">' +
                        '<span class="path1"></span>' +
                        '<span class="path2"></span>' +
                        '</i>' + rate +
                        '</span > '
                    );

                    resolve();
                },
            })
        });
    },
    MonthlySalesIncome: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=MonthlySalesIncome",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    
                    if (response.data.total > 0) {
                        $("#monthlySalesIncomeInput").html(response.data.total.toFixed(2) + " &#8378;");
                        IncomeAndExpensesRequest.MonthlySalesIncomeValue = response.data.total;
                    }
                    IncomeAndExpensesRequest.MonthlySalesIncomeValuePrevious = response.data.totalPrevious;

                    var color = "success";
                    var arrow = "up";
                    var rate;

                    if (response.data.total > response.data.totalPrevious) {
                        rate = ((response.data.total - response.data.totalPrevious) / response.data.total) * 100;
                        rate = rate = Math.abs(rate);
                        rate = rate.toFixed(2) + '%';
                    }
                    else {
                        rate = ((response.data.totalPrevious - response.data.total) / response.data.totalPrevious) * 100;
                        rate = rate = Math.abs(rate);
                        rate = rate.toFixed(2) + '%';
                        color = "danger";
                        arrow = "down";
                    }

                    $("#monthlySalesIncomeCardBody").append(
                        '<span class="badge badge-light-' + color + ' fs-base">' +
                        '<i class="ki-duotone ki-arrow-' + arrow + ' fs-5 text-' + color + ' ms-n1">' +
                        '<span class="path1"></span>' +
                        '<span class="path2"></span>' +
                        '</i>' + rate +
                        '</span > '
                    );

                    resolve();
                },
            })
        });
    },
    MonthlyWholeSalerExpenses: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=MonthlyWholeSalerExpenses",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    
                    if (response.data.total > 0) {
                        IncomeAndExpensesRequest.MonthlyWholeSalerExpensesValue = response.data.total;
                    }
                    IncomeAndExpensesRequest.MonthlyWholeSalerExpensesValuePrevious = response.data.totalPrevious;
                    resolve();
                },
            })
        });
    },

    MonthlyExternalIncomeChart: function () {

        var newTotalArray = [];
        var newMonthArray = [];
        $.each(IncomeAndExpensesRequest.YearlyExternalIncomeArray, function (index, value) {
            newTotalArray.push(value.total);
            newMonthArray.push(IncomeAndExpensesRequest.GenerateMonthName(value.month));
        })        

        var e = { self: null, rendered: !1 },
            t = function (e) {
                var t = document.getElementById("MonthlyExternalIncomeChart");
                if (t) {
                    var a = parseInt(KTUtil.css(t, "height")),
                        l = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                        r = KTUtil.getCssVariableValue("--bs-gray-800"),
                        o = {
                            series: [{ name: "Harici Gelir", data: newTotalArray }],
                            chart: { fontFamily: "inherit", type: "area", height: a, toolbar: { show: !1 } },
                            legend: { show: !1 },
                            dataLabels: { enabled: !1 },
                            fill: { type: "solid", opacity: 0 },
                            stroke: { curve: "smooth", show: !0, width: 2, colors: [r] },
                            xaxis: {
                                categories: newMonthArray,
                                axisBorder: { show: !1 },
                                axisTicks: { show: !1 },
                                labels: { show: !1 },
                                crosshairs: { position: "front", stroke: { color: r, width: 1, dashArray: 3 } },
                                tooltip: { enabled: !0, formatter: void 0, offsetY: 0, style: { fontSize: "12px" } },
                            },
                            yaxis: { labels: { show: !1 } },
                            states: { normal: { filter: { type: "none", value: 0 } }, hover: { filter: { type: "none", value: 0 } }, active: { allowMultipleDataPointsSelection: !1, filter: { type: "none", value: 0 } } },
                            tooltip: {
                                style: { fontSize: "12px" },                                
                                y: {
                                    formatter: function (e) {
                                        return e + " ₺";
                                    },
                                },
                            },
                            colors: [KTUtil.getCssVariableValue("--bs-success")],
                            grid: { borderColor: l, strokeDashArray: 4, padding: { top: 0, right: -20, bottom: -20, left: -20 }, yaxis: { lines: { show: !0 } } },
                            markers: { strokeColor: r, strokeWidth: 2 },
                        };
                    (e.self = new ApexCharts(t, o)),
                        setTimeout(function () {
                            e.self.render(), (e.rendered = !0);
                        }, 200);
                }
            };
        t(e);
    },
    MonthlyExternalExpensesChart: function () {

        var newTotalArray = [];
        var newMonthArray = [];
        $.each(IncomeAndExpensesRequest.YearlyExternalExpensesArray, function (index, value) {
            newTotalArray.push(value.total);
            newMonthArray.push(IncomeAndExpensesRequest.GenerateMonthName(value.month));
        })
       
        var e = { self: null, rendered: !1 },
            t = function (e) {
                var t = document.getElementById("MonthlyExternalExpensesChart");
                if (t) {
                    var a = parseInt(KTUtil.css(t, "height")),
                        l = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                        r = KTUtil.getCssVariableValue("--bs-gray-800"),
                        o = {
                            series: [{ name: "Harici Gider", data: newTotalArray }],
                            chart: { fontFamily: "inherit", type: "area", height: a, toolbar: { show: !1 } },
                            legend: { show: !1 },
                            dataLabels: { enabled: !1 },
                            fill: { type: "solid", opacity: 0 },
                            stroke: { curve: "smooth", show: !0, width: 2, colors: [r] },
                            xaxis: {
                                categories: newMonthArray,
                                axisBorder: { show: !1 },
                                axisTicks: { show: !1 },
                                labels: { show: !1 },
                                crosshairs: { position: "front", stroke: { color: r, width: 1, dashArray: 3 } },
                                tooltip: { enabled: !0, formatter: void 0, offsetY: 0, style: { fontSize: "12px" } },
                            },
                            yaxis: { labels: { show: !1 } },
                            states: { normal: { filter: { type: "none", value: 0 } }, hover: { filter: { type: "none", value: 0 } }, active: { allowMultipleDataPointsSelection: !1, filter: { type: "none", value: 0 } } },
                            tooltip: {
                                style: { fontSize: "12px" },
                                y: {
                                    formatter: function (e) {
                                        return e + " ₺";
                                    },
                                },
                            },
                            colors: [KTUtil.getCssVariableValue("--bs-success")],
                            grid: { borderColor: l, strokeDashArray: 4, padding: { top: 0, right: -20, bottom: -20, left: -20 }, yaxis: { lines: { show: !0 } } },
                            markers: { strokeColor: r, strokeWidth: 2 },
                        };
                    (e.self = new ApexCharts(t, o)),
                        setTimeout(function () {
                            e.self.render(), (e.rendered = !0);
                        }, 200);
                }
            };
        t(e);
    },
    MonthlySalesIncomeChart: function () {

        var newTotalArray = [];
        var newMonthArray = [];
        $.each(IncomeAndExpensesRequest.YearlySalesIncomeArray, function (index, value) {
            newTotalArray.push(value.total);
            newMonthArray.push(IncomeAndExpensesRequest.GenerateMonthName(value.month));
        })

        var e = { self: null, rendered: !1 },
            t = function (e) {
                var t = document.getElementById("MonthlySalesIncomeChart");
                if (t) {
                    var a = parseInt(KTUtil.css(t, "height")),
                        l = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                        r = KTUtil.getCssVariableValue("--bs-gray-800"),
                        o = {
                            series: [{ name: "Satış Geliri", data: newTotalArray }],
                            chart: { fontFamily: "inherit", type: "area", height: a, toolbar: { show: !1 } },
                            legend: { show: !1 },
                            dataLabels: { enabled: !1 },
                            fill: { type: "solid", opacity: 0 },
                            stroke: { curve: "smooth", show: !0, width: 2, colors: [r] },
                            xaxis: {
                                categories: newMonthArray,
                                axisBorder: { show: !1 },
                                axisTicks: { show: !1 },
                                labels: { show: !1 },
                                crosshairs: { position: "front", stroke: { color: r, width: 1, dashArray: 3 } },
                                tooltip: { enabled: !0, formatter: void 0, offsetY: 0, style: { fontSize: "12px" } },
                            },
                            yaxis: { labels: { show: !1 } },
                            states: { normal: { filter: { type: "none", value: 0 } }, hover: { filter: { type: "none", value: 0 } }, active: { allowMultipleDataPointsSelection: !1, filter: { type: "none", value: 0 } } },
                            tooltip: {
                                style: { fontSize: "12px" },
                                y: {
                                    formatter: function (e) {
                                        return e + " ₺";
                                    },
                                },
                            },
                            colors: [KTUtil.getCssVariableValue("--bs-success")],
                            grid: { borderColor: l, strokeDashArray: 4, padding: { top: 0, right: -20, bottom: -20, left: -20 }, yaxis: { lines: { show: !0 } } },
                            markers: { strokeColor: r, strokeWidth: 2 },
                        };
                    (e.self = new ApexCharts(t, o)),
                        setTimeout(function () {
                            e.self.render(), (e.rendered = !0);
                        }, 200);
                }
            };
        t(e);
    },
    MonthlyEstimatedIncomeChart: function () {
        
        var newTotalArray = [];
        var newMonthArray = [];

        const combinedArray = IncomeAndExpensesRequest.YearlyExternalIncomeArray.map((item, index) => {
            newTotalArray.push(item.total + IncomeAndExpensesRequest.YearlySalesIncomeArray[index].total);
            newMonthArray.push(IncomeAndExpensesRequest.GenerateMonthName(item.month));
        });
        
        var e = { self: null, rendered: !1 },
            t = function (e) {
                var t = document.getElementById("MonthlyEstimatedIncomeChart");
                if (t) {
                    var a = parseInt(KTUtil.css(t, "height")),
                        l = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                        r = KTUtil.getCssVariableValue("--bs-gray-800"),
                        o = {
                            series: [{ name: "Tahmini Gelir", data: newTotalArray }],
                            chart: { fontFamily: "inherit", type: "area", height: a, toolbar: { show: !1 } },
                            legend: { show: !1 },
                            dataLabels: { enabled: !1 },
                            fill: { type: "solid", opacity: 0 },
                            stroke: { curve: "smooth", show: !0, width: 2, colors: [r] },
                            xaxis: {
                                categories: newMonthArray,
                                axisBorder: { show: !1 },
                                axisTicks: { show: !1 },
                                labels: { show: !1 },
                                crosshairs: { position: "front", stroke: { color: r, width: 1, dashArray: 3 } },
                                tooltip: { enabled: !0, formatter: void 0, offsetY: 0, style: { fontSize: "12px" } },
                            },
                            yaxis: { labels: { show: !1 } },
                            states: { normal: { filter: { type: "none", value: 0 } }, hover: { filter: { type: "none", value: 0 } }, active: { allowMultipleDataPointsSelection: !1, filter: { type: "none", value: 0 } } },
                            tooltip: {
                                style: { fontSize: "12px" },
                                y: {
                                    formatter: function (e) {
                                        return e + " ₺";
                                    },
                                },
                            },
                            colors: [KTUtil.getCssVariableValue("--bs-success")],
                            grid: { borderColor: l, strokeDashArray: 4, padding: { top: 0, right: -20, bottom: -20, left: -20 }, yaxis: { lines: { show: !0 } } },
                            markers: { strokeColor: r, strokeWidth: 2 },
                        };
                    (e.self = new ApexCharts(t, o)),
                        setTimeout(function () {
                            e.self.render(), (e.rendered = !0);
                        }, 200);
                }
            };
        t(e);
    },
    MonthlyEstimatedExpensesChart: function () {

        var newTotalArray = [];
        var newMonthArray = [];

        const combinedArray = IncomeAndExpensesRequest.YearlyExternalExpensesArray.map((item, index) => {
            newTotalArray.push(item.total + IncomeAndExpensesRequest.YearlyWholeSalerExpensesArray[index].total);
            newMonthArray.push(IncomeAndExpensesRequest.GenerateMonthName(item.month));
        });

        var e = { self: null, rendered: !1 },
            t = function (e) {
                var t = document.getElementById("MonthlyEstimatedExpensesChart");
                if (t) {
                    var a = parseInt(KTUtil.css(t, "height")),
                        l = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                        r = KTUtil.getCssVariableValue("--bs-gray-800"),
                        o = {
                            series: [{ name: "Tahmini Gider", data: newTotalArray }],
                            chart: { fontFamily: "inherit", type: "area", height: a, toolbar: { show: !1 } },
                            legend: { show: !1 },
                            dataLabels: { enabled: !1 },
                            fill: { type: "solid", opacity: 0 },
                            stroke: { curve: "smooth", show: !0, width: 2, colors: [r] },
                            xaxis: {
                                categories: newMonthArray,
                                axisBorder: { show: !1 },
                                axisTicks: { show: !1 },
                                labels: { show: !1 },
                                crosshairs: { position: "front", stroke: { color: r, width: 1, dashArray: 3 } },
                                tooltip: { enabled: !0, formatter: void 0, offsetY: 0, style: { fontSize: "12px" } },
                            },
                            yaxis: { labels: { show: !1 } },
                            states: { normal: { filter: { type: "none", value: 0 } }, hover: { filter: { type: "none", value: 0 } }, active: { allowMultipleDataPointsSelection: !1, filter: { type: "none", value: 0 } } },
                            tooltip: {
                                style: { fontSize: "12px" },
                                y: {
                                    formatter: function (e) {
                                        return e + " ₺";
                                    },
                                },
                            },
                            colors: [KTUtil.getCssVariableValue("--bs-success")],
                            grid: { borderColor: l, strokeDashArray: 4, padding: { top: 0, right: -20, bottom: -20, left: -20 }, yaxis: { lines: { show: !0 } } },
                            markers: { strokeColor: r, strokeWidth: 2 },
                        };
                    (e.self = new ApexCharts(t, o)),
                        setTimeout(function () {
                            e.self.render(), (e.rendered = !0);
                        }, 200);
                }
            };
        t(e);
    },
    MonthlyEstimatedEarningChart: function () {

        var newTotalArray = [];
        var newMonthArray = [];

        const combinedArray = IncomeAndExpensesRequest.YearlyExternalIncomeArray.map((item, index) => {
            newTotalArray.push((IncomeAndExpensesRequest.YearlyExternalIncomeArray[index].total + IncomeAndExpensesRequest.YearlySalesIncomeArray[index].total) - (IncomeAndExpensesRequest.YearlyExternalExpensesArray[index].total + IncomeAndExpensesRequest.YearlyWholeSalerExpensesArray[index].total));
            newMonthArray.push(IncomeAndExpensesRequest.GenerateMonthName(item.month));
        });

        var e = { self: null, rendered: !1 },
            t = function (e) {
                var t = document.getElementById("MonthlyEstimatedEarningChart");
                if (t) {
                    var a = parseInt(KTUtil.css(t, "height")),
                        l = KTUtil.getCssVariableValue("--bs-border-dashed-color"),
                        r = KTUtil.getCssVariableValue("--bs-gray-800"),
                        o = {
                            series: [{ name: "Tahmini Kazanç", data: newTotalArray }],
                            chart: { fontFamily: "inherit", type: "area", height: a, toolbar: { show: !1 } },
                            legend: { show: !1 },
                            dataLabels: { enabled: !1 },
                            fill: { type: "solid", opacity: 0 },
                            stroke: { curve: "smooth", show: !0, width: 2, colors: [r] },
                            xaxis: {
                                categories: newMonthArray,
                                axisBorder: { show: !1 },
                                axisTicks: { show: !1 },
                                labels: { show: !1 },
                                crosshairs: { position: "front", stroke: { color: r, width: 1, dashArray: 3 } },
                                tooltip: { enabled: !0, formatter: void 0, offsetY: 0, style: { fontSize: "12px" } },
                            },
                            yaxis: { labels: { show: !1 } },
                            states: { normal: { filter: { type: "none", value: 0 } }, hover: { filter: { type: "none", value: 0 } }, active: { allowMultipleDataPointsSelection: !1, filter: { type: "none", value: 0 } } },
                            tooltip: {
                                style: { fontSize: "12px" },
                                y: {
                                    formatter: function (e) {
                                        return e + " ₺";
                                    },
                                },
                            },
                            colors: [KTUtil.getCssVariableValue("--bs-success")],
                            grid: { borderColor: l, strokeDashArray: 4, padding: { top: 0, right: -20, bottom: -20, left: -20 }, yaxis: { lines: { show: !0 } } },
                            markers: { strokeColor: r, strokeWidth: 2 },
                        };
                    (e.self = new ApexCharts(t, o)),
                        setTimeout(function () {
                            e.self.render(), (e.rendered = !0);
                        }, 200);
                }
            };
        t(e);
    },

    YearlyExternalIncome: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=YearlyExternalIncome",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {

                    IncomeAndExpensesRequest.YearlyExternalIncomeArray = response.data;
                    resolve();
                },
            })
        });
    },
    YearlyExternalExpenses: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=YearlyExternalExpenses",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {

                    IncomeAndExpensesRequest.YearlyExternalExpensesArray = response.data;
                    resolve();
                },
            })
        });
    },
    YearlySalesIncome: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=YearlySalesIncome",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {

                    IncomeAndExpensesRequest.YearlySalesIncomeArray = response.data;
                    resolve();
                },
            })
        });
    },
    YearlyWholeSalerExpenses: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "IncomeAndExpensesReport?handler=YearlyWholeSalerExpenses",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(null),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {

                    IncomeAndExpensesRequest.YearlyWholeSalerExpensesArray = response.data;
                    resolve();
                },
            })
        });
    },
    YearlyIncomeExpensesChart: function () {

        const combinedArray = IncomeAndExpensesRequest.YearlyExternalIncomeArray.map((item, index) => {
            return {
                year: IncomeAndExpensesRequest.GenerateMonthName(item.month),                
                EstimatedExpenses: IncomeAndExpensesRequest.YearlyExternalExpensesArray[index].total + IncomeAndExpensesRequest.YearlyWholeSalerExpensesArray[index].total,
                EstimatedIncome: IncomeAndExpensesRequest.YearlyExternalIncomeArray[index].total + IncomeAndExpensesRequest.YearlySalesIncomeArray[index].total,
                EstimatedEarning: (IncomeAndExpensesRequest.YearlyExternalIncomeArray[index].total + IncomeAndExpensesRequest.YearlySalesIncomeArray[index].total) - (IncomeAndExpensesRequest.YearlyExternalExpensesArray[index].total + IncomeAndExpensesRequest.YearlyWholeSalerExpensesArray[index].total)
            };
        });
        
        !(function () {
            if ("undefined" != typeof am5) {
                var e = document.getElementById("kt_charts_widget_13_chart");
                if (e) {
                    var t,
                        a = function () {
                            (t = am5.Root.new(e)).setThemes([am5themes_Animated.new(t)]);
                            var a = t.container.children.push(am5xy.XYChart.new(t, { panX: !0, panY: !0, wheelX: "panX", wheelY: "zoomX" }));
                            a.set("cursor", am5xy.XYCursor.new(t, { behavior: "none" })).lineY.set("visible", !1);
                            var l = combinedArray,
                                r = a.xAxes.push(am5xy.CategoryAxis.new(t, { categoryField: "year", startLocation: 0.5, endLocation: 0.5, renderer: am5xy.AxisRendererX.new(t, {}), tooltip: am5.Tooltip.new(t, {}) }));
                            r.get("renderer").grid.template.setAll({ disabled: !0, strokeOpacity: 0 }),
                                r.get("renderer").labels.template.setAll({ fontWeight: "400", fontSize: 13, fill: am5.color(KTUtil.getCssVariableValue("--bs-gray-500")) }),
                                r.data.setAll(l);
                            var o = a.yAxes.push(am5xy.ValueAxis.new(t, { renderer: am5xy.AxisRendererY.new(t, {}) }));
                            function i(e, i, s) {
                                var n = a.series.push(
                                    am5xy.LineSeries.new(t, {
                                        name: e,
                                        xAxis: r,
                                        yAxis: o,
                                        stacked: !0,
                                        valueYField: i,
                                        categoryXField: "year",
                                        fill: am5.color(s),
                                        tooltip: am5.Tooltip.new(t, { pointerOrientation: "horizontal", labelText: "[bold]{name}[/]\n{categoryX}: {valueY}" }),
                                    })
                                );
                                n.fills.template.setAll({ fillOpacity: 0.5, visible: !0 }), n.data.setAll(l), n.appear(1e3);
                            }
                            o.get("renderer").grid.template.setAll({ stroke: am5.color(KTUtil.getCssVariableValue("--bs-gray-300")), strokeWidth: 1, strokeOpacity: 1, strokeDasharray: [3] }),
                                o.get("renderer").labels.template.setAll({ fontWeight: "400", fontSize: 13, fill: am5.color(KTUtil.getCssVariableValue("--bs-gray-500")) }),                               
                                i("Tahmini Gider", "EstimatedExpenses", KTUtil.getCssVariableValue("--bs-danger")),                                
                                i("Tahmini Gelir", "EstimatedIncome", KTUtil.getCssVariableValue("--bs-primary")),                                
                                i("Tahmini Kazanç", "EstimatedEarning", KTUtil.getCssVariableValue("--bs-success")),                                
                                a.set("scrollbarX", am5.Scrollbar.new(t, { orientation: "horizontal", marginBottom: 25, height: 8 }));                        
                        };
                    am5.ready(function () {
                        a();
                    }),
                    KTThemeMode.on("kt.thememode.change", function () {
                        t.dispose(), a();
                    });
                }
            }
        })();
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
    IncomeAndExpensesRequest.Init();
});