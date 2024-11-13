var WholeSaler = {

    Init: function () {
        WholeSaler.ToastrError();
        WholeSaler.ToastrSuccess();
        WholeSaler.ChangeStatus();
        $("#amountPriceInput").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
        $("#amountEdit").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
        
        var totalMovements = $("#totalMovementsInput").val();
        var totalReturned = $("#totalReturnedInput").val();

        $("#totalMovements").html(totalMovements);
        $("#totalReturned").html(totalReturned);
    },

    ToastrError: function () {

        let msg = $('#ToastrError').val();
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
    DeleteConfirm: function (e) {

        event.preventDefault(); 
        var url = $(e).attr("href"); 
        
        Swal.fire({
            title: 'Emin misin?',
            text: 'Toptancıyı silmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: "Onaylıyorum, Sil",
            cancelButtonText: 'Vazgeç',
            customClass: {
                confirmButton: 'btn btn-primary',
                cancelButton: 'btn btn-danger'
            }
        }).then((willDelete) => {

            if (willDelete.isConfirmed) {
                window.location.href = url;
            }
        });
    },
    DeleteMovementConfirm: function (e) {

        event.preventDefault();
        var url = $(e).attr("href");

        Swal.fire({
            title: 'Emin misin?',
            text: 'Toptancı Haraketini silmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: "Onaylıyorum, Sil",
            cancelButtonText: 'Vazgeç',
            customClass: {
                confirmButton: 'btn btn-primary',
                cancelButton: 'btn btn-danger'
            }
        }).then((willDelete) => {

            if (willDelete.isConfirmed) {
                window.location.href = url;
            }
        });
    },
    ChangeStatus: function () {
        $("#statusSelect").on("change", function (e) {

            $("#statusCircle").removeClass("bg-success");
            $("#statusCircle").addClass("bg-danger");

            if (this.value == "true") {
                $("#statusCircle").removeClass("bg-danger");
                $("#statusCircle").addClass("bg-success");
            }
        });
    },
    CalculatorAmount: function () {

        var totalAmountDiv = $("#totalAmountDiv").html();
        var jsonString = '[' + totalAmountDiv.replace(/}{/g, '},{') + ']';
        var cleanedData = JSON.parse(jsonString.replace(/\s+/g, ''));
        var total = 0.00;
        var amuntTotal = 0.00;
        $.each(cleanedData, function (key, value) {

            if (value.ProcessType == 1) {
                total -= value.Amount;
                amuntTotal -= value.Amount;
            }
            else {
                total += value.Amount;
                amuntTotal += value.Amount;
            }
        })

        $("#amountTotal").html((Math.round(total * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
        $("#amountTotalHidden_1").html((Math.round(total * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");

        var valuemovementsTotal = parseInt($("#movementsTotal").val());
        var j = 2;

        for (var i = 1; i <= valuemovementsTotal; i++) {

            if ($("#amountHidden_" + i).val() == undefined) {
                break;
            }
            var value = parseFloat($("#amountHidden_" + i).val().replace(",", ".") + " &#8378;");
            var plus = $("#amountHidden_" + i).attr("plus");

            if (plus == "true") {
                total -= value;
                $("#amountTotalHidden_" + j).html((Math.round(total * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;")
            }
            else {
                total += value
                $("#amountTotalHidden_" + j).html((Math.round(total * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;")
            }

            j++;
        }

        if (amuntTotal.toString().indexOf(".") == -1) {
            amuntTotal = amuntTotal + ".00";
        }
        if (amuntTotal.toString().substr(-3, 1) != ".") {
            amuntTotal = amuntTotal + "0";
        }
        if (amuntTotal.toString().indexOf("-") != -1) {
            $("#amountTotal").addClass("text-danger");
        }
        else {
            $("#amountTotal").addClass("text-success");
        }
    },
    EditModal: function (id) {

        if (id == "") {
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

            toastr.error("Lütfen bir hareket seçin", "Hata!");

            return;
        }

        $.ajax({
            type: "POST",
            url: "?handler=ListById",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(id),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.status) {
                    $("#idEdit").val(response.data.id);
                    $("#wholeSalerIdEdit").val(response.data.wholeSalerId);
                    $("#imageEdit").val(response.data.image);
                    $("#invoiceDateEdit").val(response.data.invoiceDate);
                    $("#processTypeEdit").val(response.data.processType).trigger("change");
                    $("#noteEdit").val(response.data.note);

                    if (response.data.amount.toString().indexOf(".") == -1) {
                        response.data.amount = response.data.amount + ".00";
                    }
                    if (response.data.amount.toString().substr(-3, 1) != ".") {
                        response.data.amount = response.data.amount + "0";
                    }
                    $("#amountEdit").val(response.data.amount);

                    var baseUrl = $("#image2Edit").attr("style").split("url(")[1].split("Uploads")[0];  
                    $("#image2Edit").attr("style", "background-image: url(" + baseUrl + response.data.image + ");background-size: contain");
                    $("#image3Edit").attr("style", "background-image: url(" + baseUrl + response.data.image + ");background-size: contain");
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
        $("#editMovement").modal("show");
    }
}
$(document).ready(function () {
    WholeSaler.Init();
});

$(window).on("load", function () {
    WholeSaler.CalculatorAmount();
    $("#pageLoader").attr("style", "display:none");
    $("#statusSelect").trigger("change");
})