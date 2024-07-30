var NumPad = {
    Init: function () {
        $("#addMoneyNumpad").on('shown.bs.modal', function () {
            $('#addMoneyNumpadInput').focus();
        });
        $("#addMoneyNumpad").on('hidden.bs.modal', function () {
            $("#addMoneyNumpadInput").val("");
            $("#barcodeNo").focus();
        });
        $("#receivedMoneyNumpad").on('shown.bs.modal', function () {
            $('#receivedMoneyNumpadInput').focus();
        });
        $("#receivedMoneyNumpad").on('hidden.bs.modal', function () {
            $("#receivedMoneyNumpadInput").val("");
            $("#barcodeNo").focus();
        });
        $("#addMoneyNumpadInput").on("keyup", function () {
            $(this).val($(this).val().replace(",","."));
        })
        $("#receivedMoneyNumpadInput").on("keyup", function () {
            $(this).val($(this).val().replace(",", "."));
        })
    },
    SetSymbol: function (value,input) {
        if (value == "CE") {
            var newValue = $("#" + input).val().slice(0, -1);
            $("#" + input).val(newValue);
        }
        if (value == "C") {
            $("#" + input).val("");
        }
        if (value == "1") {
            var val = $("#" + input).val();
            var newValue = val + 1;
            $("#" + input).val(newValue);
        }
        if (value == "2") {
            var val = $("#" + input).val();
            var newValue = val + 2;
            $("#" + input).val(newValue);
        }
        if (value == "3") {
            var val = $("#" + input).val();
            var newValue = val + 3;
            $("#" + input).val(newValue);
        }
        if (value == "4") {
            var val = $("#" + input).val();
            var newValue = val + 4;
            $("#" + input).val(newValue);
        }
        if (value == "5") {
            var val = $("#" + input).val();
            var newValue = val + 5;
            $("#" + input).val(newValue);
        }
        if (value == "6") {
            var val = $("#" + input).val();
            var newValue = val + 6;
            $("#" + input).val(newValue);
        }
        if (value == "7") {
            var val = $("#" + input).val();
            var newValue = val + 7;
            $("#" + input).val(newValue);
        }
        if (value == "8") {
            var val = $("#" + input).val();
            var newValue = val + 8;
            $("#" + input).val(newValue);
        }
        if (value == "9") {
            var val = $("#" + input).val();
            var newValue = val + 9;
            $("#" + input).val(newValue);
        }
        if (value == "0") {
            var val = $("#" + input).val();
            var newValue = val + 0;
            $("#" + input).val(newValue);
        }
        if (value == "00") {
            var val = $("#" + input).val();
            var newValue = val + 0;
            var newValue = newValue + 0;
            $("#" + input).val(newValue);
        }
        if (value == ".") {
            var val = $("#" + input).val();
            var newValue = val + ".";
            $("#" + input).val(newValue);
        }
        $("#" + input).focus();
    },
    ReceivedMoney: function () {
        debugger;
        basket = localStorage.getItem("basket");
        
        var val = $("#receivedMoneyNumpadInput").val();

        if (val == "") {
            return;
        }

        if (basket == 1) {
            $("#receivedMoneyNumpadSpan").html((Math.round(val * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
            var remainder = $("#caseTotal").html().split(" ")[0].replace(",", ".");
            $("#remainderMoneyNumpadSpan").html((Math.round((val - remainder) * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
        }
        else {
            $("#receivedMoneyNumpadSpan2").html((Math.round(val * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
            var remainder = $("#caseTotal2").html().split(" ")[0].replace(",", ".");
            $("#remainderMoneyNumpadSpan2").html((Math.round((val - remainder) * 100) / 100).toFixed(2).replace(".", ",") + " &#8378;");
        }
        $("#receivedMoneyNumpad").modal("hide");            
    }
}
$(document).ready(function () {
    NumPad.Init();
});