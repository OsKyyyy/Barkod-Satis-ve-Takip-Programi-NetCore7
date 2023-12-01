var User = {

    Init: function () {
        User.AlertError();
        User.ToastrError();
        User.ToastrSuccess();
        User.PasswordHideShow();
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
                $("#passwordInput").attr("type","text");
                return;
            }

            $(this).find("i").attr("class", "ki-solid ki-eye fs-4");
            $("#passwordInput").attr("type", "password");

        });
    }
}
$(document).ready(function () {
    User.Init();
});