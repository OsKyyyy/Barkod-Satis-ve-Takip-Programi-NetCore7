var User = {

    Init: function () {
        User.AlertError();
        User.ToastrError();
        User.ToastrSuccess();
        User.PasswordHideShow();
        User.ChangeStatus();
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
    }
}
$(document).ready(function () {
    User.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
    $("#statusSelect").trigger("change");
})