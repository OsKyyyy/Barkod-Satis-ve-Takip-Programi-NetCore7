var Report = {

    Init: function () {
        Report.ToastrError();
        Report.ToastrSuccess();
        Report.LoadDatePicker();
        Report.FilterByDate();
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
    DeleteConfirm: function (e) {

        event.preventDefault(); 
        var url = $(e).attr("href"); 
        
        Swal.fire({
            title: 'Emin misin?',
            text: 'Satışı silmek üzeresiniz. Bu işlem yapıldıktan sonra geri alınamaz',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: "Onaylıyorum, Sil",
            cancelButtonText: 'Vazgeç',
            customClass: {
                confirmButton: 'btn btn-primary',
                cancelButton: 'btn btn-danger'
            }
        }).then((willDelete) => {

            if (willDelete.isConfirmed) {
                window.location.href = url;
            }
        });
    },
    LoadDatePicker: function () {
        new tempusDominus.TempusDominus(document.getElementById("kt_td_picker_date_only"), {
            display: {
                viewMode: "calendar",
                components: {
                    decades: true,
                    year: true,
                    month: true,
                    date: true,
                    hours: false,
                    minutes: false,
                    seconds: false
                },
                buttons: {
                    clear: true,
                },
            }
        });
    },
    FilterByDate: function () {
        const datepicker = document.getElementById("kt_td_picker_date_only");

        datepicker.addEventListener(tempusDominus.Namespace.events.change, (e) => {

            var startDate = e.detail.date;
            var formattedStartDate = undefined;
            var date = new Date();

            if (startDate !== undefined) {
                date = new Date(startDate);
            }

            date.setTime(date.getTime() - (date.getTimezoneOffset() * 60000));
            formattedStartDate = date.toISOString().substr(0, 19);

            var saleDetailModel = { "Date": formattedStartDate };

            $.ajax({
                type: "POST",
                url: "SalesDetailReport?handler=List",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(saleDetailModel),
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {

                    if (response.message == "Authentication Error") {
                        window.location.reload();
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

                    $('#kt_datatable_example').DataTable().clear().rows.add(response.data).draw();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        })
    }
}
$(document).ready(function () {
    Report.Init();
});

$(window).on("load", function () {
    $("#pageLoader").attr("style", "display:none");    
})