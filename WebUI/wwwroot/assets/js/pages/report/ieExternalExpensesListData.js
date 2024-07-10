"use strict";

// Class definition
var KTDatatablesExternalExpenses = function () {
    // Shared variables
    var table;
    var datatable;

    // Private functions
    var initDatatableExternalExpenses = function () {
        // Set date data order
        const tableRows = table.querySelectorAll('tbody tr');

        // Init datatable --- more info on datatables: https://datatables.net/manual/
        datatable = $(table).DataTable({
            "info": false,
            'order': [],
            'pageLength': 10
        });
    }

    // Hook export buttons
    var exportButtonsExternalExpenses = () => {
        
        const documentTitle = 'Harici Gider';
        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle
                }
            ]
        }).container().appendTo($('#kt_datatable_external_expenses_buttons'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#kt_datatable_external_expenses_export_menu [data-kt-export-expenses]');
        exportButtons.forEach(exportButton => {
              
            exportButton.addEventListener('click', e => {
                
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export-expenses');                
                const target = $("#kt_datatable_external_expenses_buttons .dt-buttons .buttons-" + exportValue)

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatableExternalExpenses = () => {
        const filterSearch = document.querySelector('[data-kt-filter="searchExternalExpenses"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = document.querySelector('#kt_datatable_external_expenses');

            if (!table) {
                return;
            }

            initDatatableExternalExpenses();
            exportButtonsExternalExpenses();
            handleSearchDatatableExternalExpenses();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesExternalExpenses.init();
});