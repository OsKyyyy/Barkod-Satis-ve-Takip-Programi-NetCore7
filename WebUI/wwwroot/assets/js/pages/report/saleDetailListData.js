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
                        
                        return "<a class='btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1' asp-page='/Category/Edit' asp-route-id='@item.CreateDate'><i class='ki-duotone ki-pencil fs-2'><span class='path1'></span><span class='path2'></span></i></a><a class='btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1' asp-page='/Category/Delete' asp-route-id='@item.CreateDate' onclick='Category.DeleteConfirm(this)'><i class='ki-duotone ki-trash fs-2'><span class='path1'></span><span class='path2'></span><span class='path3'></span><span class='path4'></span><span class='path5'></span></i></a>";                        
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