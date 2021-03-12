var dataTable;

$(document).ready(function () {
    loadDataTable();
});

/*PARA USAR O DATABLE https://datatables.net/ TEM QUE SE FAZER ESTA FUNÇÃO, ONDE QUANDO
 A PAGINA CARREGAR, VAI CLASS A API PARA IR BUSCAR OS DADOS E DEPOIS CARREGAR NO BODY DA TABELA
 ESSES DADOS. DEPOIS DISSO NO RENDER DEFINIMOS OS BUTOES PARA CREATE E DELETE 1º VAI CHAMAR UMA METODO DO CONTROLLER O 
 2º DE DELETE VAI CHAMAR OUTRA FUNÇÃO DELETE(declarada em baixo) E PASSA COMO PARAMETRO A URL(para o controller/action)*/
function loadDataTable() {
    dataTable = $('#tblDataService').DataTable({
        "ajax": {
            "url": "/admin/service/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "price", "width": "15%" },
            { "data": "frequency.frequencyCount", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/service/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='fas fa-edit'></i> Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/service/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'><i class='fas fa-trash-alt'></i> Delete
                                </a>
                            </div>
                            `;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}

/* ESTA FUNÇÃO É CHAMADA NO BOTAO CRIADO EM CIMA, PARA A TABELA 
 ESTA FUNÇÃO USA O SWEATALERT, PARA CRIAR ALERTAS MAIS INTERETATIVOS PARA O USUARIO, https://sweetalert.js.org/guides/ */

function Delete(url) {
    swal({
        title: "Are you sure tou want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message)
                }
            }
        });
    });
}

