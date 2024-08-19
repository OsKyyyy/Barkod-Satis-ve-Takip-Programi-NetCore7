var Product = {

    Init: function () {
        Product.ToastrError();
        Product.ToastrSuccess();
        Product.ChangeStatus();
        Product.ChangeFavorite();
        $("#purchasePriceInput").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
        $("#salePriceInput").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
        $("#totalCostInput").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
        $("#paymentAmountInput").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
        $("#vat").on("change", Product.VarCalculator);
        $("#purchasePriceInput").on("keyup", Product.VarCalculator);
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
            text: 'Ürünü silmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
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
    ChangeFavorite: function () {
        $("#favoriteSelect").on("change", function (e) {

            $("#favoriteCircle").removeClass("bg-success");
            $("#favoriteCircle").addClass("bg-danger");

            if (this.value == "true") {
                $("#favoriteCircle").removeClass("bg-danger");
                $("#favoriteCircle").addClass("bg-success");
            }
        });
    },
    VarCalculator: function () {
        
        var value = parseFloat($("#vat").val());
        if (value == 0) {
            return;
        }
        var purchasePrice = parseFloat($("#purchasePriceInput").val());
        var vat = (value * purchasePrice) / 100;

        var calculator = vat + purchasePrice;

        var round = (Math.round(calculator * Math.pow(10, 2)) / Math.pow(10, 2)).toFixed(2);
        var roundSplitTL = round.split(".")[0];
        var roundSplitKR = round.split(".")[1];
        var salePrice = "";

        if (roundSplitKR < 15) {
            salePrice = "" + roundSplitTL + ".00";
        }
        if (roundSplitKR < 35 && roundSplitKR >= 15) {
            salePrice = "" + roundSplitTL + ".25";
        }
        if (roundSplitKR < 60 && roundSplitKR >= 35) {
            salePrice = "" + roundSplitTL + ".50";
        }
        if (roundSplitKR < 85 && roundSplitKR >= 60) {
            salePrice = "" + roundSplitTL + ".75";
        }
        if (roundSplitKR <= 99 && roundSplitKR >= 85) {
            roundSplitTL = parseFloat(roundSplitTL) + 1;
            salePrice = "" + roundSplitTL + ".00";
        }

        $("#salePriceInput").val(salePrice);
    },
    PrintTag: function (productId) {

        $.ajax({
            type: "GET",
            url: "List?handler=Product",
            data: { productId: productId },
            contentType: "application/json; charset=utf-8",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                const now = new Date(response.data.updateDate);
                const day = String(now.getDate()).padStart(2, '0');
                const month = String(now.getMonth() + 1).padStart(2, '0'); // Ay 0'dan başladığı için +1
                const year = now.getFullYear();
                const formattedDate = `${day}/${month}/${year}`;

                var productName = response.data.name
                    .split(' ')
                    .map(word => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
                    .join(' ')

                var tagModel = { "Type": 2, "Code": response.data.barcode, "ProductName": productName, "ProductPrice": response.data.salePrice.toFixed(2).replace(".",","), "PriceChangeDate": formattedDate };

                $.ajax({
                    type: "POST",
                    url: "http://localhost:5000/",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(tagModel),
                })
            },
        })
    }
}

$(document).ready(function () {
    Product.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
    $("#statusSelect").trigger("change");
    $("#favoriteSelect").trigger("change");
})