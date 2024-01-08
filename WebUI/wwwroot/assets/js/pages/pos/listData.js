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
                    target: 0,
                    visible: false,
                    searchable: false
                }]
        });
        
        $('#kt_datatable_example tbody').on('click', 'button', function () {
            var data_row = datatable.row($(this).parents('tr')).data();

            if (localStorage.getItem("basket") == 1) {
                $("#selectedCustomerCard").removeClass("d-none");
                $("#selectedCustomerCard").addClass("d-block");
                $("#selectedCustomerInfo").html(data_row[1] + " - " + data_row[2]);    

                localStorage.setItem("customerId", data_row[0]);
                localStorage.setItem("customerInfo", data_row[1] + " - " + data_row[2]);
                Pos.ChangeBasket(1);
            }
            else {
                $("#selectedCustomerCard2").removeClass("d-none");
                $("#selectedCustomerCard2").addClass("d-block");
                $("#selectedCustomerInfo2").html(data_row[1] + " - " + data_row[2]);

                localStorage.setItem("customerId2", data_row[0]);
                localStorage.setItem("customerInfo2", data_row[1] + " - " + data_row[2]);
                Pos.ChangeBasket(2);
            }            
            
            $("#customers").modal("hide");
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