var IncomeAndExpensesList = {

    YearlyExternalIncomeArray: [],
    YearlyExternalExpensesArray: [],
    YearlySalesIncomeArray: [],
    YearlyWholeSalerExpensesArray: [],

    YearlyEstimatedExpensesArray: [],
    YearlyEstimatedIncomeArray: [],
    YearlyEstimatedEarningArray: [],

    Init: async function () {
       
        await IncomeAndExpensesList.YearlyExternalIncome();
        await IncomeAndExpensesList.YearlyExternalExpenses();
        await IncomeAndExpensesList.YearlySalesIncome();
        await IncomeAndExpensesList.YearlySalesIncomeCreateTable();
        await IncomeAndExpensesList.YearlyWholeSalerExpenses();   
        await IncomeAndExpensesList.YearlyIncomeExpensesEarningChart();        
        
        IncomeAndExpensesList.YearlyExternalExpensesArray.map((item, index) => {
            IncomeAndExpensesList.YearlyEstimatedExpensesArray.push({
                total: item.total + IncomeAndExpensesList.YearlyWholeSalerExpensesArray[index].total,
                year: item.year,
                month: item.month
            });
        });
        IncomeAndExpensesList.YearlyExternalIncomeArray.map((item, index) => {
            IncomeAndExpensesList.YearlyEstimatedIncomeArray.push({
                total: item.total + IncomeAndExpensesList.YearlySalesIncomeArray[index].total,
                year: item.year,
                month: item.month
            });
        });
        IncomeAndExpensesList.YearlyExternalIncomeArray.map((item, index) => {
            IncomeAndExpensesList.YearlyEstimatedEarningArray.push({
                total: (IncomeAndExpensesList.YearlyExternalIncomeArray[index].total + IncomeAndExpensesList.YearlySalesIncomeArray[index].total) - (IncomeAndExpensesList.YearlyExternalExpensesArray[index].total + IncomeAndExpensesList.YearlyWholeSalerExpensesArray[index].total),
                year: item.year,
                month: item.month
            });
        });

        await IncomeAndExpensesList.YearlyExternalIncomeCreateTable();
        await IncomeAndExpensesList.YearlyExternalExpensesCreateTable();
        await IncomeAndExpensesList.YearlyEstimatedExpensesCreateTable();
        await IncomeAndExpensesList.YearlyEstimatedIncomeCreateTable();
        await IncomeAndExpensesList.YearlyEstimatedEarningCreateTable();
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

                    IncomeAndExpensesList.YearlyExternalIncomeArray = response.data;
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

                    IncomeAndExpensesList.YearlyExternalExpensesArray = response.data;
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

                    IncomeAndExpensesList.YearlySalesIncomeArray = response.data;
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

                    IncomeAndExpensesList.YearlyWholeSalerExpensesArray = response.data;
                    resolve();
                },
            })
        });
    },
    YearlyExternalIncomeCreateTable: function () {

        var tableExternalIncome = $('#kt_datatable_external_income').DataTable();

        $.each(IncomeAndExpensesList.YearlyExternalIncomeArray.slice().reverse(), function (index, value) {

            tableExternalIncome.row.add([
                '' + IncomeAndExpensesList.GenerateMonthName(value.month) + ' ' + value.year + '',
                '<span class="text-success">' + value.total.toFixed(2) + ' &#8378;</span>'
            ]).draw(false);
        });
    },
    YearlyExternalExpensesCreateTable: function () {

        var tableExternalExpenses = $('#kt_datatable_external_expenses').DataTable();
        
        $.each(IncomeAndExpensesList.YearlyExternalExpensesArray.slice().reverse(), function (index, value) {

            tableExternalExpenses.row.add([
                '' + IncomeAndExpensesList.GenerateMonthName(value.month) + ' ' + value.year + '',
                '<span class="text-danger">' + value.total.toFixed(2) + ' &#8378;</span>'
            ]).draw(false);
        });
    },
    YearlySalesIncomeCreateTable: function () {

        var tableSalesIncome = $('#kt_datatable_sales_income').DataTable();

        $.each(IncomeAndExpensesList.YearlySalesIncomeArray.slice().reverse(), function (index, value) {

            tableSalesIncome.row.add([
                '' + IncomeAndExpensesList.GenerateMonthName(value.month) + ' ' + value.year + '',
                '<span class="text-success">' + value.total.toFixed(2) + ' &#8378;</span>'
            ]).draw(false);
        });
    },
    YearlyEstimatedExpensesCreateTable: function () {
        
        var tableEstimatedExpenses = $('#kt_datatable_estimated_expenses').DataTable();

        $.each(IncomeAndExpensesList.YearlyEstimatedExpensesArray.slice().reverse(), function (index, value) {
            
            tableEstimatedExpenses.row.add([
                '' + IncomeAndExpensesList.GenerateMonthName(value.month) + ' ' + value.year + '',
                '<span class="text-danger">' + value.total.toFixed(2) + ' &#8378;</span>'
            ]).draw(false);
        });
    },
    YearlyEstimatedIncomeCreateTable: function () {

        var tableEstimatedIncome = $('#kt_datatable_estimated_income').DataTable();

        $.each(IncomeAndExpensesList.YearlyEstimatedIncomeArray.slice().reverse(), function (index, value) {

            tableEstimatedIncome.row.add([
                '' + IncomeAndExpensesList.GenerateMonthName(value.month) + ' ' + value.year + '',
                '<span class="text-success">' + value.total.toFixed(2) + ' &#8378;</span>'
            ]).draw(false);
        });
    },
    YearlyEstimatedEarningCreateTable: function () {

        var tableEstimatedEarning = $('#kt_datatable_estimated_earning').DataTable();

        $.each(IncomeAndExpensesList.YearlyEstimatedEarningArray.slice().reverse(), function (index, value) {

            var color = "success";
            if (value.total < 0) {
                color = "danger";
            }

            tableEstimatedEarning.row.add([
                '' + IncomeAndExpensesList.GenerateMonthName(value.month) + ' ' + value.year + '',
                '<span class="text-' + color + '">' + value.total.toFixed(2) + ' &#8378;</span>'
            ]).draw(false);
        });
    },
    YearlyIncomeExpensesEarningChart: function () {

        const combinedArray = IncomeAndExpensesList.YearlyExternalIncomeArray.map((item, index) => {
            return {
                year: IncomeAndExpensesList.GenerateMonthName(item.month) + " " + item.year,                
                expenses: IncomeAndExpensesList.YearlyExternalExpensesArray[index].total + IncomeAndExpensesList.YearlyWholeSalerExpensesArray[index].total,
                income: IncomeAndExpensesList.YearlyExternalIncomeArray[index].total + IncomeAndExpensesList.YearlySalesIncomeArray[index].total,
                columnSettings: { fill: am5.color(KTUtil.getCssVariableValue("--bs-primary")) }
            };
        });
        
        !(function () {
            if ("undefined" != typeof am5) {
                var e = document.getElementById("kt_charts_widget_23");
                if (e) {
                    var t,
                        a = function () {
                            (t = am5.Root.new(e)).setThemes([am5themes_Animated.new(t)]);
                            var a = t.container.children.push(am5xy.XYChart.new(t, { panX: !1, panY: !1, layout: t.verticalLayout })),
                                l = combinedArray,
                                r = a.xAxes.push(am5xy.CategoryAxis.new(t, { categoryField: "year", renderer: am5xy.AxisRendererX.new(t, {}) }));
                            r.data.setAll(l),
                                r.get("renderer").labels.template.setAll({ paddingTop: 20, fontWeight: "400", fontSize: 11, fill: am5.color(KTUtil.getCssVariableValue("--bs-gray-500")) }),
                                r.get("renderer").grid.template.setAll({ disabled: !0, strokeOpacity: 0 });
                            var o = a.yAxes.push(am5xy.ValueAxis.new(t, { min: 0, extraMax: 0.1, renderer: am5xy.AxisRendererY.new(t, {}) }));
                            o.get("renderer").labels.template.setAll({ paddingTop: 0, fontWeight: "400", fontSize: 11, fill: am5.color(KTUtil.getCssVariableValue("--bs-gray-500")) }),
                                o.get("renderer").grid.template.setAll({ stroke: am5.color(KTUtil.getCssVariableValue("--bs-gray-300")), strokeWidth: 1, strokeOpacity: 1, strokeDasharray: [3] });
                            var i = a.series.push(
                                am5xy.ColumnSeries.new(t, {
                                    name: "Gelir",
                                    xAxis: r,
                                    yAxis: o,
                                    valueYField: "income",
                                    categoryXField: "year",
                                    tooltip: am5.Tooltip.new(t, { pointerOrientation: "horizontal", labelText: "{name} - {categoryX}: {valueY} {info} ₺" }),
                                })
                            );
                            i.columns.template.setAll({ tooltipY: am5.percent(10), templateField: "columnSettings" }), i.data.setAll(l);
                            var s = a.series.push(
                                am5xy.LineSeries.new(t, {
                                    name: "Gider",
                                    xAxis: r,
                                    yAxis: o,
                                    valueYField: "expenses",
                                    categoryXField: "year",
                                    fill: am5.color(KTUtil.getCssVariableValue("--bs-success")),
                                    stroke: am5.color(KTUtil.getCssVariableValue("--bs-success")),
                                    tooltip: am5.Tooltip.new(t, { pointerOrientation: "horizontal", labelText: "{name} - {categoryX}: {valueY} {info} ₺" }),
                                })
                            );
                            s.strokes.template.setAll({ stroke: am5.color(KTUtil.getCssVariableValue("--bs-success")) }),
                                s.strokes.template.setAll({ strokeWidth: 3, templateField: "strokeSettings" }),
                                s.data.setAll(l),
                                s.bullets.push(function () {
                                    return am5.Bullet.new(t, {
                                        sprite: am5.Circle.new(t, { strokeWidth: 3, stroke: am5.color(KTUtil.getCssVariableValue("--bs-success")), radius: 5, fill: am5.color(KTUtil.getCssVariableValue("--bs-success-light")) }),
                                    });
                                }),
                                i.columns.template.setAll({ strokeOpacity: 0, cornerRadiusBR: 0, cornerRadiusTR: 6, cornerRadiusBL: 0, cornerRadiusTL: 6 }),
                                a.set("cursor", am5xy.XYCursor.new(t, {})),
                                a.get("cursor").lineX.setAll({ visible: !1 }),
                                a.get("cursor").lineY.setAll({ visible: !1 });
                            var n = a.children.push(am5.Legend.new(t, { centerX: am5.p50, x: am5.p50 }));
                            n.data.setAll(a.series.values), n.labels.template.setAll({ fontWeight: "600", fontSize: 14, fill: am5.color(KTUtil.getCssVariableValue("--bs-gray-700")) }), a.appear(1e3, 100), i.appear();
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
    }, 
}
$(document).ready(function () {
    IncomeAndExpensesList.Init();
});