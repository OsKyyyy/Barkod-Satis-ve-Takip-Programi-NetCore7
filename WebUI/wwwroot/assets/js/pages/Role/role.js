var Role = {

    Init: function () {
        Role.AlertError();
        Role.ToastrError();
        Role.ToastrSuccess();
        Role.PasswordHideShow();
        Role.ChangeStatus();   
        Role.GetSelectText();
    },
    AlertError: function () {

        let msg = $('#AlertError').val();
        if (msg != undefined && msg.length > 0) {
            $("#ErrorContent").html(msg);
            $("#ErrorDiv").show();
        }
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
    PasswordHideShow: function () {
        $("#passwordIcon").on("click", function () {

            let currentIcon = $(this).find("i").attr("class");

            if (currentIcon == "ki-solid ki-eye fs-4") {

                $(this).find("i").attr("class", "ki-solid ki-eye-slash fs-4");
                $("#passwordInput").attr("type", "text");
                return;
            }

            $(this).find("i").attr("class", "ki-solid ki-eye fs-4");
            $("#passwordInput").attr("type", "password");

        });
    },
    DeleteConfirm: function (e) {

        event.preventDefault(); 
        var url = $(e).attr("href"); 
        
        Swal.fire({
            title: 'Emin misin?',
            text: 'Kullanıcıyı silmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
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
    GetSelect: function () {
        const options = [
            { text: "Kullanıcı İşlemleri -> Kullanıcı Ekle", value: "user_add" },
            { text: "Kullanıcı İşlemleri -> Kullanıcı Listesi", value: "user_list" },
            { text: "Kullanıcı İşlemleri -> Rol İşlemleri", value: "user_rolelist" },
            { text: "Müşteri İşlemleri -> Müşteri Ekle", value: "customer_add" },
            { text: "Müşteri İşlemleri -> Müşteri Listesi", value: "customer_list" },
            { text: "Toptancı İşlemleri -> Toptancı Ekle", value: "wholesaler_add" },
            { text: "Toptancı İşlemleri -> Toptancı Listesi", value: "wholesaler_list" },
            { text: "Kategori İşlemleri -> Kategori Ekle", value: "category_add" },
            { text: "Kategori İşlemleri -> Kategori Listesi", value: "category_list" },
            { text: "Ürün İşlemleri -> Ürün Ekle", value: "product_add" },
            { text: "Ürün İşlemleri -> Ürün Listesi", value: "product_list" },
            { text: "Ürün İşlemleri -> Stok Girişi", value: "product_stockentry" },
            { text: "Perakende Satış", value: "pos_sale" },
            { text: "Gelir-Gider İşlemleri -> Gelir-Gider Ekle", value: "incomeandexpenses_add" },
            { text: "Gelir-Gider İşlemleri -> Gelir-Gider Listesi", value: "incomeandexpenses_list" },
            { text: "Gelir-Gider İşlemleri -> Tür İşlemleri -> Tür Ekle", value: "incomeandexpensestype_add" },
            { text: "Gelir-Gider İşlemleri -> Tür İşlemleri -> Tür Listesi", value: "incomeandexpensestype_list" },
            { text: "Fiyat Karşılaştır", value: "webscraping_pricecomparison" },
            { text: "Fiyat İzleme", value: "webscraping_pricetracking" },
            { text: "Hal Fiyatları", value: "webscraping_priceproducemarket" },
            { text: "Gelir Gider Raporları -> Gelir Gider Raporları", value: "report_incomeandexpensesreport" },
            { text: "Gelir Gider Raporları -> Gelir Gider Listesi", value: "report_incomeandexpenseslist" },
            { text: "Satış Raporları -> Satış Listesi", value: "report_salesreport" },
            { text: "Satış Raporları -> Satış Detay Listesi", value: "report_salesdetailreport" },
            { text: "Müşteri Raporları -> Müşteri Raporları", value: "report_customerreport" },
            { text: "Müşteir Raporları -> Alışveriş Listesi", value: "report_customershoppinglist" },
            { text: "Müşteri Raporları -> Geri Ödeme Listesi", value: "report_customerrefundlist" },
            { text: "Müşteri Raporları -> Toplam Borç Listesi", value: "report_customertotaldebtlist" },
            { text: "Müşteri Raporları -> Ödeme Yapılmayan Gün Listesi", value: "report_customernonpayerslist" },
            { text: "Toptancı Raporları -> Toptancı Raporları", value: "report_wholesalerreport" },
            { text: "Toptancı Raporları -> Alışveriş Listesi", value: "report_wholesalershoppinglist" },
            { text: "Toptancı Raporları -> Geri Ödeme Listesi", value: "report_wholesalerrefundlist" },
            { text: "Toptancı Raporları -> Toplam Borç Listesi", value: "report_wholesalertotaldebtlist" },
            { text: "Toptancı Raporları -> Ödeme Yapılmayan Gün Listesi", value: "report_wholesalernonpayerslist" },
        ];

        return options;
    },
    GetSelectText: function () {

        const options = Role.GetSelect();
        const values = $("#pageNamesListInput").val().split(',');

        const matchedTexts = [];

        values.forEach(value => {
            const match = options.find(option => option.value === value.trim());

            if (match) {
                matchedTexts.push(match.text);
            }
        });
        
        $.each(matchedTexts, function (key, value) {
            
            $("#pageNamesList").append(
                '<div class="d-flex align-items-center py-2">' +
                '<span class="bullet bg-primary me-3"></span>' +
                '' + value + '' +
                '</div>'
            );
        })

    }
}
$(document).ready(function () {
    Role.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
    $("#statusSelect").trigger("change");
})