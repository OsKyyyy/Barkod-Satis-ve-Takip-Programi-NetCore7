"use strict";

// Class definition
var KTDatatablesEstimatedExpenses = function () {
    // Shared variables
    var table;
    var datatable;

    // Private functions
    var initDatatableEstimatedExpenses = function () {
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
    var exportButtonsEstimatedExpenses = () => {
        const documentTitle = 'Tahmini Gider';
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
        }).container().appendTo($('#kt_datatable_estimated_expenses_buttons'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#kt_datatable_estimated_expenses_export_menu [data-kt-export-estimated-expenses]');
        exportButtons.forEach(exportButton => {
            exportButton.addEventListener('click', e => {
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export-estimated-expenses');
                const target = $("#kt_datatable_estimated_expenses_buttons .dt-buttons .buttons-" + exportValue)

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatableEstimatedExpenses = () => {
        const filterSearch = document.querySelector('[data-kt-filter="searchEstimatedExpenses"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = document.querySelector('#kt_datatable_estimated_expenses');

            if (!table) {
                return;
            }

            initDatatableEstimatedExpenses();
            exportButtonsEstimatedExpenses();
            handleSearchDatatableEstimatedExpenses();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesEstimatedExpenses.init();
});