$(document).ready(function () {
    $("#Input_ProvinciaId").change(function () {
        $("#Input_CiudadId").empty();
        $("#Input_CiudadId").append('<option value="0">[Seleccionar Ciudad]</option>');

        $.ajax({
            type: 'POST',
            url: '/Admin/Generico/GetCities',
            dataType: 'json',
            data: {
                provinciaId: $("#Input_ProvinciaId").val()
            },
            success: function (ciudades) {
                $.each(ciudades,
                    function (i, ciudad) {
                        $("#Input_CiudadId").append('<option value="' + ciudad.ciudadId + '">' + ciudad.nombre + '</option>');
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
