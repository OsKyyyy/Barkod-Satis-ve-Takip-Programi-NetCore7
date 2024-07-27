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

        var allItems = Role.GetSelect();
        var allItems2 = Role.GetSelect();

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
}
$(document).ready(function () {
    Request.Init();
})