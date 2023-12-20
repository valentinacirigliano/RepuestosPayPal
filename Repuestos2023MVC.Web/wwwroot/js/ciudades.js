$(document).ready(function () {
    $("#ProvinciaId").change(function () {
        $("#CiudadId").empty();
        $("#CiudadId").append('<option value="0">[Seleccionar Ciudad]</option>');

        $.ajax({
            type: 'POST',
            url: '/Admin/Generico/GetCities',
            dataType: 'json',
            data: {
                provinciaId: $("#ProvinciaId").val()
            },
            success: function (ciudades) {
                $.each(ciudades,
                    function (i, ciudad) {
                        $("#CiudadId").append('<option value="' + ciudad.ciudadId + '">' + ciudad.nombre + '</option>');
                    });
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error al intentar cargar las ciudades.');
                console.log('Error: ' + textStatus);
                console.log('Descripción del error: ' + errorThrown);
                console.log('Respuesta del servidor: ', xhr.responseText);
            }
        });
        return false;
    });
});
