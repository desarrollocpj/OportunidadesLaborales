﻿var ExperienciaVista = function () {
    var _llenarPorcentaje = function () {
        responseSimple({
            url: "Postulante/PostulanteObtenerPorcentajeAvanceJson",
            refresh: false,
            callBackSuccess: function (response) {
                CloseMessages();
                $('#porcentajeProgreso').css({ 'width': response.data + '%' });
                $('.progress_wide>span>i').html("")
                $('.progress_wide>span>i').append(response.data + "%")
            }
        });
    }
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
            allowInputToggle: true,
            useCurrent: false
        });
        $("#perfil_principal").attr("src", "data:image/gif;base64," + rutaImage);
        $("#img_layout_post").attr("src", "data:image/gif;base64," + rutaImage);

    };
    var _ListarExperiencia = function () {

        responseSimple({
            url: "Experiencia/ExperienciaListarJson",
            data: JSON.stringify({ fkPosID: $("[name='fk_postulante']").val() }),
            refresh: false,
            callBackSuccess: function (response) {
                var respuesta = response.respuesta;
                var datos = response.data;
                if (respuesta) {
                    $("#tbody_experiencia").html("");
                    $.each(datos, function (index, value) {
                        $("#tbody_experiencia").append('<tr><td>' + value.exp_empresa + '</td><td>' + value.exp_cargo + '</td><td>' + value.exp_motivo_cese + '</td><td>' + moment(value.exp_fecha_ini).format("DD/MM/YYYY") + '</td><td>' + moment(value.exp_fecha_fin).format("DD/MM/YYYY") + '</td><td><button type="button" data-id="' + value.exp_id + '" class="btn btn-danger btn-xs btn_delete"><i class="fa fa-times"></i></button></td></tr>');
                    });
             
                    if (datos.length == 0) {
                        CloseMessages();
                        messageResponse({
                            text: "No se Encontraron Registros",
                            type: "warning"
                        });
                    }
                    CloseMessages();
                }
            }
        });

    };

    var _componentes = function () {

        $("#myDatepicker1").on("dp.change", function (e) {
            $('#myDatepicker2').data("DateTimePicker").minDate(e.date);
        });
        $("#myDatepicker2").on("dp.change", function (e) {
            $('#myDatepicker1').data("DateTimePicker").maxDate(e.date);
        });
  
        $(document).on("click", ".btn_guardar", function (e) {
            $("#frmExperiencia-form").submit();
            if (_objetoForm_frmExperiencia.valid()) {
                var dataForm = $('#frmExperiencia-form').serializeFormJSON();
                responseSimple({
                    url: "Experiencia/ExperienciaInsertarJson",
                    data: JSON.stringify(dataForm),
                    refresh: false,
                    callBackSuccess: function (response) {
                        var respuesta = response.respuesta;
                        if (respuesta) {
                            _llenarPorcentaje();
                            limpiar_form({ contenedor: "#frmExperiencia-form" });
                            _objetoForm_frmExperiencia.resetForm();
                            ExperienciaVista.init__ListarExperiencia();
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
            _objetoForm_frmExperiencia.resetForm();
        });

        $(document).on("click", ".btn_delete", function (e) {
            var id = $(this).data("id");
            if (id != "" || id > 0) {
                messageConfirmation({
                    callBackSAceptarComplete: function () {
                        responseSimple({
                            url: "Experiencia/ExperienciaEliminarJson",
                            data: JSON.stringify({ id: id }),
                            refresh: false,
                            callBackSuccess: function (response) {
                                _llenarPorcentaje();
                                ExperienciaVista.init__ListarExperiencia();
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
                        _llenarPorcentaje();
                        readImage(_image, "#perfil_principal");
                        readImage(_image, "#img_layout_post");
                    }
                }
            });
        });
    };

    var _metodos = function () {
        validar_Form({
            nameVariable: 'frmExperiencia',
            contenedor: '#frmExperiencia-form',
            rules: {
                exp_empresa:
                {
                    required: true,

                },
                exp_cargo:
                {
                    required: true,

                },
                exp_motivo_cese:
                {
                    required: true,

                },
                exp_fecha_ini:
                {
                    required: true,

                },
                exp_fecha_fin:
                {
                    required: true,

                },
                

            },
            messages: {
                exp_empresa:
                {
                    required: 'Empresa Obligatorio',
                },
                exp_cargo:
                {
                    required: 'Cargo Obligatorio',
                },
                exp_motivo_cese:
                {
                    required: 'Motivo Cese Obligatorio',
                },
                exp_fecha_ini:
                {
                    required: 'Fecha Inicio Obligatorio',
                },
                exp_fecha_fin:
                {
                    required: 'Fecha Fin Obligatorio',
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
            _ListarExperiencia();
            _componentes();
            _metodos();
            _llenarPorcentaje();
        },
        init__ListarExperiencia: function () {
            _ListarExperiencia();
        }
    }
}();

// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function () {
    ExperienciaVista.init();
});