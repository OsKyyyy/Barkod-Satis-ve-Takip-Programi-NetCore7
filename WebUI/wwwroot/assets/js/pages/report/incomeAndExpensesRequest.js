var IncomeAndExpensesRequest = {

    MonthlyExternalIncomeValue: 0,
    MonthlyExternalExpensesValue: 0,
    MonthlySalesIncomeValue: 0,
    MonthlyWholeSalerExpensesValue: 0,

    MonthlyExternalIncomeValuePrevious: 0,
    MonthlyExternalExpensesValuePrevious: 0,
    MonthlySalesIncomeValuePrevious: 0,
    MonthlyWholeSalerExpensesValuePrevious: 0,

    Init: async function () {
        
        await IncomeAndExpensesRequest.MonthlyExternalIncome();              
        await IncomeAndExpensesRequest.MonthlyExternalExpenses();  
        await IncomeAndExpensesRequest.MonthlySalesIncome();
        await IncomeAndExpensesRequest.MonthlyWholeSalerExpenses();

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

}
$(document).ready(function () {
    IncomeAndExpensesRequest.Init();
});