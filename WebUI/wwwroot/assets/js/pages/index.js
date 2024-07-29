var Index = {

    UserId: undefined,
    UserRoleName: undefined,
    UserDetail: undefined,
    AllRoles: [
        "user_add", "user_list", "user_rolelist", "customer_add", "customer_list", "wholesaler_add", "wholesaler_list", "category_add", "category_list", "product_add", "product_list",
        "product_stockentry", "pos_sale", "incomeandexpenses_add", "incomeandexpenses_list", "incomeandexpensestype_add", "incomeandexpensestype_list", "webscraping_pricecomparison",
        "webscraping_pricetracking", "webscraping_priceproducemarket", "report_incomeandexpensesreport", "report_incomeandexpenseslist", "report_salesreport", "report_salesdetailreport",
        "report_customerreport", "report_customershoppinglist", "report_customerrefundlist", "report_customertotaldebtlist", "report_customernonpayerslist", "report_wholesalerreport",
        "report_wholesalershoppinglist", "report_wholesalerrefundlist", "report_wholesalertotaldebtlist", "report_wholesalernonpayerslist"
    ],
    Init: async function () {
        await Index.GetUserId();
        await Index.GetUserById();
        await Index.GetRoles();
        await Index.SetUserDetail();

        var sidebarMinimizeLs = localStorage.getItem("data-kt-app-sidebar-minimize");

        if (sidebarMinimizeLs == "on") {
            $("#kt_app_body").attr("data-kt-app-sidebar-minimize", "on");
            $("#kt_app_sidebar_toggle").addClass("active");
        }
        else {
            $("#kt_app_body").removeAttr("data-kt-app-sidebar-minimize");
            $("#kt_app_sidebar_toggle").removeClass("active");
        }

        $("#kt_app_sidebar_toggle").on("click", function () {
            
            var sidebarMinimize = $("#kt_app_body").attr("data-kt-app-sidebar-minimize");

            if (sidebarMinimize == undefined) {
                localStorage.removeItem("data-kt-app-sidebar-minimize");
                localStorage.setItem("data-kt-app-sidebar-minimize", "off");
            }
            else {
                localStorage.removeItem("data-kt-app-sidebar-minimize");
                localStorage.setItem("data-kt-app-sidebar-minimize", "on");
            }
        });
    },
    GetUserId: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "GET",
                url: "/Index?handler=UserId",
                contentType: "application/json; charset=utf-8",
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    Index.UserId = response;
                    resolve();
                },
            })
        });
    },
    GetUserById: function () {

        return new Promise((resolve, reject) => {
            $.ajax({                
                type: "GET",
                url: "/Index?handler=UserById",
                data: { id: Index.UserId },
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    Index.UserRoleName = response.data.roleName;
                    Index.UserDetail = response.data;
                    resolve();
                },
            })
        });
        
    },
    GetRoles: function () {

        return new Promise((resolve, reject) => {
            $.ajax({
                type: "GET",
                url: "/Index?handler=Role",
                data: { roleName: Index.UserRoleName },
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    const unauthorizedRoles = Index.AllRoles.filter(role => !response.data.pageNames.includes(role));                    
                    $.each(unauthorizedRoles, function (key, value) {
                        
                        $('[page-name="' + value + '"]').remove();                        
                    })
                    $.each(unauthorizedRoles, function (key, value) {
                        debugger;
                        var split = value.split("_")[0];

                        if (split != "report") {
                            var length = $('[page-base="' + split + '"]').find('div').length;
                            if (length == 0) {
                                $('[page-base="' + split + '"]').remove()
                            }                            
                        }
                        else {
                            if (value.indexOf("incomeandexpenses") != -1) {
                                var length = $('[page-base="report_incomeandexpenses"]').find('div').length;
                                if (length == 0) {
                                    $('[page-base="report_incomeandexpenses"]').remove()
                                }
                            }
                            else if (value.indexOf("sales") != -1) {
                                var length = $('[page-base="report_sales"]').find('div').length;
                                if (length == 0) {
                                    $('[page-base="report_sales"]').remove()
                                }
                            }
                            else if (value.indexOf("customer") != -1) {
                                var length = $('[page-base="report_customer"]').find('div').length;
                                if (length == 0) {
                                    $('[page-base="report_customer"]').remove()
                                }
                            }
                            else if (value.indexOf("wholesaler") != -1) {
                                var length = $('[page-base="report_wholesaler"]').find('div').length;
                                if (length == 0) {
                                    $('[page-base="report_wholesaler"]').remove()
                                }
                            }
                        }                        
                    })
                    resolve();
                },
            })
        });
    },
    SetUserDetail: function () {

        return new Promise((resolve, reject) => {
            $("#headerMyProfile").attr("href", "/User/Edit/" + Index.UserDetail.id);
            $("#headerEmail").html(Index.UserDetail.email);
            $("#headerName").html(Index.UserDetail.firstName + " " + Index.UserDetail.lastName);
            $("#headerRole").html(Index.UserDetail.roleName);            
            resolve();
        });
    }
}
$(document).ready(function () {
    Index.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");
})