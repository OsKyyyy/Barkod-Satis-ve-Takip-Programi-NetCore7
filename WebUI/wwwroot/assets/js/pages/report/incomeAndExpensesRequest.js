var IncomeAndExpensesRequest = {

    Init: function () {
        IncomeAndExpensesRequest.EstimatedMonthlyIncome();              
    },

    EstimatedMonthlyIncome: function () {       

        $.ajax({
            type: "POST",
            url: "IncomeAndExpensesReport?handler=IncomeAndExpensesTable",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(null),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                                               
            },
        })
    },      
}
$(document).ready(function () {
    IncomeAndExpensesRequest.Init();
});