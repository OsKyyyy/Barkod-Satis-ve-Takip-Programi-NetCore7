var Request = {
    Init: function () {
        Request.CreateDualListBox();
    },
    GetRoleByName: function (name) {

        var getByRoleNameModel = { "Name": name };

        $.ajax({
            type: "POST",
            url: "RoleList?handler=GetRoleByName",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(getByRoleNameModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                $("#updateOperationClaimId").val(response.data.operationClaimId);
                $("#updateRoleName").val(response.data.roleName);

                $('#kt_dual_listbox_2 option').prop('selected', false);
                response.data.pageNames.forEach(function (option) {
                    $('#kt_dual_listbox_2 option[value="' + option + '"]').prop('selected', true);
                });
                $('#kt_dual_listbox_2').bootstrapDualListbox('refresh', true);
            },
        })
    },   
    CreateDualListBox: function () {

        var $select = $('#kt_dual_listbox_1');
        var $select2 = $('#kt_dual_listbox_2');

        var allItems = Request.GetSelect();
        var allItems2 = Request.GetSelect();

        allItems.forEach(function (item) {
            $select.append($('<option>', {
                value: item.value,
                text: item.text
            }));
        });
        allItems2.forEach(function (item) {
            $select2.append($('<option>', {
                value: item.value,
                text: item.text
            }));
        });

        $select.bootstrapDualListbox({
            nonSelectedListLabel: 'Mevcut Yetkiler',
            selectedListLabel: 'Seçilen Yetkiler',
            moveOnSelect: false,
            filterPlaceHolder: 'Filtrele',
            moveSelectedLabel: 'Seçilenleri Taşı',
            moveAllLabel: 'Tümünü Taşı',
            removeSelectedLabel: 'Seçilenleri Kaldır',
            removeAllLabel: 'Tümünü Kaldır',
            infoText: 'Toplam {0} öğe',
            infoTextFiltered: '<span class="label label-warning">Filtrelenmiş</span> {0} / {1}',
            infoTextEmpty: 'Boş liste',
            filterTextClear: 'Hepsini Göster'
        });      
        $select2.bootstrapDualListbox({
            nonSelectedListLabel: 'Mevcut Yetkiler',
            selectedListLabel: 'Seçilen Yetkiler',
            moveOnSelect: false,
            filterPlaceHolder: 'Filtrele',
            moveSelectedLabel: 'Seçilenleri Taşı',
            moveAllLabel: 'Tümünü Taşı',
            removeSelectedLabel: 'Seçilenleri Kaldır',
            removeAllLabel: 'Tümünü Kaldır',
            infoText: 'Toplam {0} öğe',
            infoTextFiltered: '<span class="label label-warning">Filtrelenmiş</span> {0} / {1}',
            infoTextEmpty: 'Boş liste',
            filterTextClear: 'Hepsini Göster'
        });
    },
    GetSelect: function () {
        const options = [
            { text: "Kullanıcı İşlemleri -> Kullanıcı Ekle", value: "user_add" },
            { text: "Kullanıcı İşlemleri -> Kullanıcı Listesi", value: "user_list" },
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
}
$(document).ready(function () {
    Request.Init();
})