"use strict";

// Class definition
var KTDatatablesExample = function () {
    // Shared variables
    var table;
    var datatable;

    // Private functions
    var initDatatable = function () {
        // Set date data order
        const tableRows = table.querySelectorAll('tbody tr');

        // Init datatable --- more info on datatables: https://datatables.net/manual/
        datatable = $(table).DataTable({
            "info": false,
            'order': [],
            'pageLength': 10,
            initComplete: function () {
                this.api()
                    .columns()
                    .every(function () {
                        $("#chooseStatus").on("change", function () {                            
                            var val = $(this).val();
                            "all" === val && (val = ""), datatable.column(4).search(val).draw();
                        });
                    });
            },
            columnDefs: [
                {
                    targets: 0,
                    data: "createDate",
                    name: "CreateDate",
                    render: function (data, type, row) {    
                        
                        var search = data.search("T");

                        if (search != -1) {
                            return new Date(data).toLocaleString('tr-TR'); 
                        }
                        return data;
                    }
                },
                {
                    targets: 1,
                    data: "customerName",
                    name: "CustomerName",
                    render: function (data, type, row) {
                        
                        if (data === null || data === "" || data === undefined) {

                            return "-";
                        }                        

                        var customerId = row.customerId;
                        if (row.customerId === undefined) {
                            customerId = row[3];
                        }

                        const color = ["success", "danger", "info", "primary", "warning"];
                        const random = Math.floor(Math.random() * color.length);

                        return "<div class='d-flex align-items-center'><div class='symbol symbol-circle symbol-50px overflow-hidden me-3'><a href = '/Customer/Movements/" + customerId + "' ><div class='symbol-label fs-3 bg-light-" + color[random] + " text-" + color[random] + "'>" + data.charAt(0).toUpperCase() + "</div></a></div><div class='ms-5'><a href = '/Customer/Movements/" + customerId + "' class='text-gray-800 text-hover-primary fs-5 fw-bold'>" + data + "</a></div></div>"                        
                    }
                },
                {
                    targets: 2,
                    data: "amount",
                    name: "Amount",
                    render: function (data, type, row) {

                        if (data === null || data === "" || data === undefined) {

                            return "-";
                        }
                        
                        return "<span class='fw-bold'>" + data + ' ₺' + "</span>";
                    }
                },
                {
                    targets: 3,
                    data: "paymentType",
                    name: "PaymentType",
                    render: function (data, type, row) {
                        
                        if (data === null || data === "" || data === undefined) {

                            return "-";
                        }

                        if (data == 1) {
                            return "Nakit";
                        }
                        return "Kredi Kartı";                        
                    }
                },
                {
                    targets: 4,
                    data: "deleted",
                    name: "Deleted",
                    render: function (data, type, row) {
                        
                        if (data === null || data === "" || data === undefined) {

                            return "-";
                        }

                        if (data == false || data == "False") {
                            return "<div class='badge badge-light-success'>Tamamlandı</div>";
                        }
                        return "<div class='badge badge-light-danger'>İptal Edildi</div>";
                    }
                },
                {
                    targets: 5,   
                    className: 'text-end',
                    render: function (data, type, row) {
                        debugger;
                        if (row.deleted == "True" || row.deleted == true) {
                            return "<a class='btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1' href='/Report/SalesProductsDetailReport/" + row.id + "'><i class='ki-duotone ki-eye fs-2'><span class='path1'></span><span class='path2'></span><span class='path3'></span></i></a>";                        
                        }
                        return "<a class='btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1' href='/Report/SalesProductsDetailReport/" + row.id + "'><i class='ki-duotone ki-eye fs-2'><span class='path1'></span><span class='path2'></span><span class='path3'></span></i></a><a class='btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1' href = '/Report/SalesDelete/" + row.id +"' onclick='Report.DeleteConfirm(this)'><i class='ki-duotone ki-trash fs-2'><span class='path1'></span><span class='path2'></span><span class='path3'></span><span class='path4'></span><span class='path5'></span></i></a>";                        
                    }
                },
                {
                    targets: 6,
                    visible: false,
                    data: "id",
                    name: "Id",
                    render: function (data, type, row) {
                        
                        return data;
                    }
                },
                {
                    targets: 7,
                    visible: false,
                    data: "customerId",
                    name: "CustomerId",
                    render: function (data, type, row) {

                        return data;
                    }
                }  
            ]                     
        });       
    }
    
    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = document.querySelector('#kt_datatable_example');

            if (!table) {
                return;
            }

            initDatatable();
            handleSearchDatatable();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesExample.init();
});