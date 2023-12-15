var Product = {

    Init: function () {
        Product.ToastrError();
        Product.ToastrSuccess();
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
    }
}

$(document).ready(function () {
    Product.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style","display:none");
})