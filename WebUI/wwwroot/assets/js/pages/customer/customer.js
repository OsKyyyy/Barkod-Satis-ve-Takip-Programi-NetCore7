var Customer = {

    Init: function () {
        Customer.ToastrError();
        Customer.ToastrSuccess();
        Customer.ChangeStatus();

        var tagifyAddInput = document.querySelector("#tagifyAddInput");
        var tagify = new Tagify(tagifyAddInput)

        var tagifyEditInput = document.querySelector("#tagifyEditInput");
        var tagifyEdit = new Tagify(tagifyEditInput)

        tagify.on("remove", function () {
            Customer.TagifyChange();
        })
        $("#tagifyAddInput").on("change", function () {
            Customer.TagifyChange();
        })
        tagifyEdit.on("remove", function () {
            Customer.TagifyEditChange();
        })
        $("#tagifyEditInput").on("change", function () {
            Customer.TagifyEditChange();
        })

        $("#amountPriceInput").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
        $("#amountEdit").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })

        var creditMovements = $("#creditMovementsInput").val();
        var debitMovements = $("#debitMovementsInput").val();

        $("#creditMovements").html(creditMovements);
        $("#debitMovements").html(debitMovements);
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
            text: 'Müşteriyi silmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
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
            text: 'Müşteri Haraketini silmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
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

        var total = 0.00;
        var valuemovementsTotal = parseInt($("#movementsTotal").val());
        for (var i = valuemovementsTotal; i >= 1; i--) {

            var value = parseFloat($("#amountHidden_" + i).val().replace(",", "."));
            var plus = $("#amountHidden_" + i).attr("plus");

            if (plus == "true") {
                total += value;
                $("#amountTotalHidden_" + i).html(total.toString().replace(".", ",") + " &#8378;")
            }
            else {
                total -= value
                $("#amountTotalHidden_" + i).html(total.toString().replace(".", ",") + " &#8378;")
            }
        }

        if (total.toString().indexOf(".") == -1) {
            total = total + ".00";
        }
        if (total.toString().substr(-3, 1) != ".") {
            total = total + "0";
        }
        if (total.toString().indexOf("-") != -1) {
            $("#amountTotal").addClass("text-danger");
        }
        else {
            $("#amountTotal").addClass("text-success");
        }

        $("#amountTotal").html(total.toString().replace(".", ",") + " &#8378;");
    },
    TagifyChange: function () {

        $("#productInformationInput").val("");
        var data = JSON.parse($("#tagifyAddInput").val());
        $.each(data, function (key, value) {
            
            var currentValue = $("#productInformationInput").val();
            if (currentValue == "") {
                $("#productInformationInput").val(value.value);
            }
            else {
                $("#productInformationInput").val(currentValue + "," + value.value);
            }
        })
    },
    TagifyEditChange: function () {

        $("#productInformationEdit").val("");
        var data = JSON.parse($("#tagifyEditInput").val());
        $.each(data, function (key, value) {

            var currentValue = $("#productInformationEdit").val();
            if (currentValue == "") {
                $("#productInformationEdit").val(value.value);
            }
            else {
                $("#productInformationEdit").val(currentValue + "," + value.value);
            }
        })
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
                    $("#customerIdEdit").val(response.data.customerId);
                    $("#processTypeEdit").val(response.data.processType).trigger("change");
                    $("#productInformationEdit").val(response.data.productInformation);
                    $("#tagifyEditInput").val(response.data.productInformation);
                    $("#noteEdit").val(response.data.note);

                    if (response.data.amount.toString().indexOf(".") == -1) {
                        response.data.amount = response.data.amount + ".00";
                    }
                    if (response.data.amount.toString().substr(-3, 1) != ".") {
                        response.data.amount = response.data.amount + "0";
                    }
                    $("#amountEdit").val(response.data.amount);                   
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
    Customer.Init();
});

$(window).on("load", function () {
    Customer.CalculatorAmount();
    $("#pageLoader").attr("style", "display:none");
    $("#statusSelect").trigger("change");
})