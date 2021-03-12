var dataTable;
var dataUrl = null;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("aproved")) {
        loadDataTable("GetAllAproved");
    }
    else {
        if (url.includes("pending")) {
            loadDataTable("GetAllPending");
        }
        else if (url.includes("all")) {
            loadDataTable("GetAll");
        }
    }
})

/*PARA USAR O DATABLE https://datatables.net/ TEM QUE SE FAZER ESTA FUNÇÃO, ONDE QUANDO
 A PAGINA CARREGAR, VAI CLASS A API PARA IR BUSCAR OS DADOS E DEPOIS CARREGAR NO BODY DA TABELA
 ESSES DADOS. DEPOIS DISSO NO RENDER DEFINIMOS OS BUTOES PARA CREATE E DELETE 1º VAI CHAMAR UMA METODO DO CONTROLLER O 
 2º DE DELETE VAI CHAMAR OUTRA FUNÇÃO DELETE(declarada em baixo) E PASSA COMO PARAMETRO A URL(para o controller/action)*/
function loadDataTable(url) {
    dataTable = $('#tblDataOrder').DataTable({
        "ajax": {
            "url": "/admin/order/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phoneNumber", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "serviceCount", "width": "15%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/order/Details/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Details
                                </a>     
                            </div>
                            `;
                }, "width": "15%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}


