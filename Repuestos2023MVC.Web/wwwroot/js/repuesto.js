
var repuestoTable;
$(document).ready(function () {
    repuestoTable = $('#tblRepuestos').DataTable({
        "ajax": {
            "url": "/Admin/Repuesto/GetAll"
        },
        "columns": [

            { "data": "descripcion" },
            { "data": "stock" },
            { "data": "categoria" },
            { "data": "precioLista" },
            {
                "data": "repuestoId",
                "render": function (data) {
                    return `
                                <a class="btn btn-warning" href="/Admin/Repuesto/UpSert?id=${data}" >
                                    <i class="bi bi-pencil-square"></i>&nbsp;
                                    Editar
                                </a>
                                <a class="btn btn-danger" onclick="Delete('/Admin/Repuesto/Delete/${data}')" >
                                    <i class="bi bi-trash3"></i> &nbsp;
                                    Eliminar
                                </a>

                            `
                }
            }
        ],
        
        "drawCallback": function () {
            var api = this.api();
            var currentPage = api.page();
            var totalPages = api.pages();

            // Deshabilitar el botón "Previous" en la primera página
            if (currentPage === 0) {
                $('#tblRepuestos_paginate .previous').addClass('disabled');
            } else {
                $('#tblRepuestos_paginate .previous').removeClass('disabled');
            }

            // Deshabilitar el botón "Next" en la última página
            if (currentPage === totalPages - 1) {
                $('#tblRepuestos_paginate .next').addClass('disabled');
            } else {
                $('#tblRepuestos_paginate .next').removeClass('disabled');
            }
        }
    });

});


function Delete(url) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "¡No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminarlo'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                "url": url,
                "type": 'DELETE',
                "success": function (data) {
                    console.log(data);
                    if (data.success) {
                        repuestoTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}