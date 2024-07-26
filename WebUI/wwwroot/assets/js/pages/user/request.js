var Request = {
    Init: function () {

    },
    UpdateUserRole: function () {
        
        var userId = $("#updateRoleUserId").val();
        var operationClaimId = $("input[name='user_role']:checked").val();        

        var updateUserRoleRequestModel = { "Id": userId, "OperationClaimId": operationClaimId };

        $.ajax({
            type: "POST",
            url: "?handler=UpdateUserRole",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(updateUserRoleRequestModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.message == "Authentication Error") {
                    window.location.reload();
                }
                if (response.status) {
                    window.open(window.location.origin + window.location.pathname + "?r=" + User.EncodeParam(response.message), "_self");
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
    }, 
    UpdatePassword: function () {

        var userId = $("#updateRoleUserId").val();
        var currentPassword = $("#updatePasswordCurrentPassword").val();
        var newPassword = $("#updatePasswordNewPassword").val();
        var confirmPassword = $("#updatePasswordConfirmPassword").val(); 

        var validate = Request.PasswordValidate(currentPassword, newPassword, confirmPassword);

        if (!validate) {
            return;
        }

        var updateUserPasswordRequestModel = { "Id": userId, "CurrentPassword": currentPassword, "NewPassword": newPassword, "ConfirmPassword": confirmPassword };

        $.ajax({
            type: "POST",
            url: "?handler=UpdateUserPassword",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(updateUserPasswordRequestModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                
                if (response.message == "Authentication Error") {
                    window.location.reload();
                }
                if (response.status) {                   
                    window.open(window.location.origin + window.location.pathname + "?r=" + User.EncodeParam(response.message), "_self");
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
    },
    UpdateEmail: function () {

        var userId = $("#updateRoleUserId").val();
        var email = $("#updateEmail").val();      

        var updateUserPasswordRequestModel = { "Id": userId, "Email": email };

        $.ajax({
            type: "POST",
            url: "?handler=UpdateUserEmail",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(updateUserPasswordRequestModel),
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (response.message == "Authentication Error") {
                    window.location.reload();
                }
                if (response.status) {
                    window.open(window.location.origin + window.location.pathname + "?r=" + User.EncodeParam(response.message), "_self");
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
    },
    PasswordValidate: function (currentPassword, newPassword, confirmPassword) {

        if (currentPassword == "" || newPassword == "" || confirmPassword == "") {
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

            toastr.error("Lütfen tüm alanları doldurun", "Hata!");
            return false;
        }

        if (newPassword.length < 8) {
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

            toastr.error("Yeni şifre en az 8 karakterden oluşmalıdır", "Hata!");
            return false;
        }

        if (newPassword != confirmPassword) {
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

            toastr.error("Yeni şifre ile yeni şifre onayla uyuşmuyor", "Hata!");
            return false;
        }  

        return true;
    }, 
}
$(document).ready(function () {
    Request.Init();
})