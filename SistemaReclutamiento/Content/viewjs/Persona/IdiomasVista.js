﻿var IdiomasVista = function () {
    var _inicio = function () {
        $("[name='per_id']").val(persona.per_id);
        $("[name='pos_id']").val(postulante.pos_id);
        $("[name='fk_postulante']").val(postulante.pos_id);
        $("#persona_nombre").text(persona.per_nombre + " " + persona.per_apellido_pat + " " + persona.per_apellido_mat);
        $('#myDatepicker1').datetimepicker({
            format: 'DD/MM/YYYY',
            ignoreReadonly: true,
            allowInputToggle: true
        });
        $('#myDatepicker2').datetimepicker({
            format: 'DD/MM/YYYY',
            ignoreReadonly: true,
            allowInputToggle: true
        });
        $("#perfil_principal").attr("src", "data:image/gif;base64," + rutaImage);
        $("#img_layout_post").attr("src", "data:image/gif;base64," + rutaImage);

    };
    var _ListarIdiomas = function () {

        responseSimple({
            url: "Idioma/IdiomaListarJson",
            data: JSON.stringify({ fkPosID: $("[name='fk_postulante']").val() }),
            refresh: false,
            callBackSuccess: function (response) {
                var respuesta = response.respuesta;
                var datos = response.data;
                if (respuesta) {
                    $("#tbody_idioma").html("");
                    $.each(datos, function (index, value) {
                        $("#tbody_idioma").append('<tr><td>' + value.idi_tipo + '</td><td>' + value.idi_idioma + '</td><td>' + value.idi_centro_estudio + '</td><td>' + moment(value.idi_periodo_ini).format("DD/MM/YYYY") + '</td><td>' + moment(value.idi_periodo_fin).format("DD/MM/YYYY") + '</td><td>' + value.idi_nivel + '</td><td><button type="button" data-id="' + value.idi_id + '" class="btn btn-danger btn-xs btn_delete"><i class="fa fa-times"></i></button></td></tr>');
                    });

                }
            }
        });

    };

    var _componentes = function () {

        $(document).on("click", ".btn_guardar", function (e) {
            $("#frmIdiomas-form").submit();
            if (_objetoForm_frmIdiomas.valid()) {
                var dataForm = $('#frmIdiomas-form').serializeFormJSON();
                responseSimple({
                    url: "Idioma/IdiomaInsertarJson",
                    data: JSON.stringify(dataForm),
                    refresh: false,
                    callBackSuccess: function (response) {
                        var respuesta = response.respuesta;
                        if (respuesta) {
                            limpiar_form({ contenedor: "#frmIdiomas-form" });
                            _objetoForm_frmIdiomas.resetForm();
                            IdiomasVista.init__ListarIdiomas();
                        }
                    }
                });
            } else {
                messageResponse({
                    text: "Complete los campos Obligatorios",
                    type: "error"
                })
            }
        });

        $(document).on("click", ".btn_cancelar", function (e) {
            _objetoForm_frmIdiomas.resetForm();
        });

        $(document).on("click", ".btn_delete", function (e) {
            var id = $(this).data("id");
            if (id != "" || id > 0) {
                messageConfirmation({
                    callBackSAceptarComplete: function () {
                        responseSimple({
                            url: "Idioma/IdiomaEliminarJson",
                            data: JSON.stringify({ id: id }),
                            refresh: false,
                            callBackSuccess: function (response) {
                                IdiomasVista.init__ListarIdiomas();
                            }
                        });
                    }
                });
            }
            else {
                messageResponse({
                    text: "Error no se encontro ID",
                    type: "error"
                })
            }
        });

        $('#subir-img-perfil').change(function () {
            var dataForm = new FormData();
            var _image = $('#subir-img-perfil')[0].files[0];
            dataForm.append('file', _image);
            dataForm.append('postulanteID', $("#pos_id").val());
            responseFileSimple({
                url: "Postulante/PostulanteSubirFotoJson",
                data: dataForm,
                refresh: false,
                callBackSuccess: function (response) {
                    var respuesta = response.respuesta;
                    if (respuesta) {
                        readImage(_image, "#perfil_principal");
                        readImage(_image, "#img_layout_post");
                    }
                }
            });
        });
    };

    var _metodos = function () {
        validar_Form({
            nameVariable: 'frmIdiomas',
            contenedor: '#frmIdiomas-form',
            rules: {
                idi_tipo:
                {
                    required: true,

                },
                idi_idioma:
                {
                    required: true,

                },
                idi_centro_estudio:
                {
                    required: true,

                },
                idi_periodo_ini:
                {
                    required: true,

                },
                idi_periodo_fin:
                {
                    required: true,

                },
                idi_nivel:
                {
                    required: true,

                },

            },
            messages: {
                idi_tipo:
                {
                    required: 'Tipo Obligatorio',
                },
                idi_idioma:
                {
                    required: 'Idioma Obligatorio',
                },
                idi_centro_estudio:
                {
                    required: 'Centro de Estudios Obligatorio',
                },
                idi_periodo_ini:
                {
                    required: 'Fecha Inicio Obligatorio',
                },
                idi_periodo_fin:
                {
                    required: 'Fecha Fin Obligatorio',
                },
                idi_nivel:
                {
                    required: 'Nivel Obligatorio',
                },
            }
        });

    };

    //
    // Return objects assigned to module
    //
    return {
        init: function () {
            _inicio();
            _ListarIdiomas();
            _componentes();
            _metodos();

        },
        init__ListarIdiomas: function () {
            _ListarIdiomas();
        }
    }
}();

// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function () {
    IdiomasVista.init();
});