var dataTable;

$(document).ready(function () {
    LoadTableToFrequency();
});

function LoadTableToFrequency() {
    dataTable = $("#tblDataFrequency").DataTable({
        "ajax": {
            "url": "/Admin/frequency/GetAll",
            "type": "GET",
            "datatype": "json"
        }, "columns": [
            { "data": "name", "width": "50%" },
            { "data": "frequencyCount", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="text-center">
                        <a href="/Admin/frequency/Upsert/${data}" class="btn btn-success text-white" style:"cursor:pointer; width:100%"><i class="fas fa-edit"></i>Edit
                        </a>
                        <a onclick=Delete("/Admin/Frequency/Delete/${data}") class="btn btn-danger text-white" style:"cursor:pointer; width:100%"><i class="fas fa-delete"></i>Delete
                        </a>
                    </div>
                    `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No records found!",
        },
        "width": "100%",
    });
}

function Delete(data) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: "DELETE",
            url: data,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                } else {
                    toastr.error(data.message);
                }
            }
        })
    })
}