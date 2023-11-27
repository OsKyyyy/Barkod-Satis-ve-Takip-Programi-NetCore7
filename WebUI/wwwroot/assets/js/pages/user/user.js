var User = {

    Init: function () {
        User.LoginError();
        User.PasswordHideShow();
    },
    LoginError: function () {

        let msg = $('#Message').val();
        if (msg.length > 0) {
            $("#LoginErrorContent").html(msg);
            $("#LoginError").show();
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