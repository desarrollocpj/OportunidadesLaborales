﻿$(document).ready(function () {
 

    $("#persona_nombre").text(personaIndex.per_nombre);
    $("#persona_direccion").text(personaIndex.per_direccion);
    $("#persona_apellidos").text(personaIndex.per_apellido_pat + " " + personaIndex.per_apellido_mat);
    $("#perfil_principal").attr("src", document.location.origin + "/archivos/perfil/" + postulante.pos_foto);   

    var opcion = "";
    var f = new Date();
    $('.date').datetimepicker({
        format: 'DD/MM/YYYY',
        ignoreReadonly: true,
        allowInputToggle: true
    });

    //$('#myDatepicker1').datetimepicker({
    //    format: 'DD/MM/YYYY',
    //    ignoreReadonly: true,
    //    allowInputToggle: true
    //});
    //$('#myDatepicker2').datetimepicker({
    //    format: 'DD/MM/YYYY',
    //    ignoreReadonly: true,
    //    allowInputToggle: true
    //});
    //$('#myDatepicker3').datetimepicker({
    //    format: 'DD/MM/YYYY',
    //    ignoreReadonly: true,
    //    allowInputToggle: true
    //});
    //$("#esu_periodo_ini").val(moment(f.getDate()).format('DD/MM/YYYY'));
    //$("#esu_periodo_fin").val(moment(f.getDate()).format('DD/MM/YYYY'));
    $("[name='per_id']").val(personaIndex.per_id);
    $("[name='pos_id']").val(postulante.pos_id);
    $("[name='fk_postulante']").val(postulante.pos_id);
    //Datos de persona
 
    $("#per_correoelectronico").val(personaIndex.per_correoelectronico);
    $("#per_nombre").val(personaIndex.per_nombre);
    $("#per_apellido_pat").val(personaIndex.per_apellido_pat);
    $("#per_apellido_mat").val(personaIndex.per_apellido_mat); 
    $("#per_numdoc").val(personaIndex.per_numdoc);
    $("#per_fechanacimiento").val(moment(personaIndex.per_fechanacimiento).format('DD/MM/YYYY'));
    $("#per_telefono").val(personaIndex.per_telefono);  
    $("#cboSexo").val(personaIndex.per_sexo);
    $("#cbotipoDocumento").val(personaIndex.per_tipodoc);
    //$("#cbotipoDocumento").attr('readonly','readonly');
    //$("#per_numdoc").attr('readonly', 'readonly');

    //Datos de postulante
    $("#pos_celular").val(postulante.pos_celular);
    $("#cbotipoDireccion").val(postulante.pos_tipo_direccion);
    $("#pos_direccion").val(postulante.pos_direccion);
    $("#cbotipoCalle").val(postulante.pos_tipo_calle);
    $("#pos_numero_casa").val(postulante.pos_numero_casa);
    $("#cbotipoCasa").val(postulante.pos_tipo_casa);
    $("#cboestadoCivil").val(postulante.pos_estado_civil);
    $("#cboBrevete").val(String(postulante.pos_brevete));
    $("#pos_num_brevete").val(postulante.pos_num_brevete);
    $("#pos_estado").val(postulante.pos_estado);   
   


    //llenando combos
    $("#cboSexo").select2();
    $.when(llenarSelect(
        basePath + "OfimaticaHerramienta/OfimaticaHerramientaListarJson", {}, "cboofimaticaHerramienta", "her_id", "her_descripcion", "")).then(function (response, textStatus) {
            $("#cboofimaticaHerramienta").select2();
        });
    $.when(llenarSelect(
        basePath + "Ubigeo/UbigeoListarPaisesJson", {}, "cboPais", "ubi_pais_id", "ubi_nombre", ubigeo.ubi_pais_id)).then(function (response, textStatus) {
            $("#cboPais").select2();
        });
    $.when(llenarSelect(basePath + "Ubigeo/UbigeoListarDepartamentosporPaisJson", { ubi_pais_id: ubigeo.ubi_pais_id }, "cboDepartamento", "ubi_departamento_id", "ubi_nombre", ubigeo.ubi_departamento_id)).then(function (response, textStatus) {
        $("#cboDepartamento").select2();
    });
    $.when(llenarSelect(basePath + "Ubigeo/UbigeoListarProvinciasporDepartamentoJson", { ubi_pais_id: ubigeo.ubi_pais_id, ubi_departamento_id: ubigeo.ubi_departamento_id }, "cboProvincia", "ubi_provincia_id", "ubi_nombre", ubigeo.ubi_provincia_id)).then(function (response, textStatus) {
        $("#cboProvincia").select2();
    });
    $.when(llenarSelect(basePath + "Ubigeo/UbigeoListarDistritosporProvinciaJson", { ubi_pais_id: ubigeo.ubi_pais_id, ubi_departamento_id: ubigeo.ubi_departamento_id, ubi_provincia_id: ubigeo.ubi_provincia_id }, "cboDistrito", "ubi_distrito_id", "ubi_nombre", ubigeo.ubi_distrito_id)).then(function (response, textStatus) {
        $("#cboDistrito").select2();
    });

    $("#cboPais").change(function () {
        var ubi_id_pais = $("#cboPais option:selected").val();

        $.when(llenarSelect(basePath + "Ubigeo/UbigeoListarDepartamentosporPaisJson", { ubi_pais_id: ubi_id_pais }, "cboDepartamento", "ubi_departamento_id", "ubi_nombre", "")).then(function (response, textStatus) {
            $("#cboDepartamento").select2();
        });
    });
    $("#cboDepartamento").change(function () {
        var ubi_pais_id = $("#cboPais option:selected").val();
        var ubi_departamento_id = $("#cboDepartamento option:selected").val();

        $.when(llenarSelect(basePath + "Ubigeo/UbigeoListarProvinciasporDepartamentoJson", { ubi_pais_id: ubi_pais_id, ubi_departamento_id: ubi_departamento_id }, "cboProvincia", "ubi_provincia_id", "ubi_nombre", "")).then(function (response, textStatus) {
            $("#cboProvincia").select2();


        });
    });
    $("#cboProvincia").change(function () {
        var ubi_pais_id = $("#cboPais option:selected").val();
        var ubi_departamento_id = $("#cboDepartamento option:selected").val();
        var ubi_provincia_id = $("#cboProvincia option:selected").val();
        $.when(llenarSelect(basePath + "Ubigeo/UbigeoListarDistritosporProvinciaJson", { ubi_pais_id: ubi_pais_id, ubi_departamento_id: ubi_departamento_id, ubi_provincia_id: ubi_provincia_id }, "cboDistrito", "ubi_distrito_id", "ubi_nombre", "")).then(function (response, textStatus) {
            $("#cboDistrito").select2();
        });
    });

  


    $(document).on('click', '#btnGuardarDatosPersonales', function () {
        var validar = $("#frmDatosPersonales");
        if (validar.valid()) {
            var dataForm = $("#frmDatosPersonales").serializeFormJSON();
            var url = basePath + "Persona/PersonaEditarJson";
            fncRegistrar(dataForm, url, false, "");
        }

    });
    $(document).on('click', '#btnGuardarEducacionBasica', function () {
        var validar = $("#frmEducacionBasica");
        if (validar.valid()) {
            var dataForm = $("#frmEducacionBasica").serializeFormJSON();
            var url = basePath + "EducacionBasica/EducacionBasicaInsertarJson";
            fncRegistrar(dataForm, url, false,"");
        }
    });  
    $(document).on('click', '#btnGuardarEducacionSuperior', function () {
        var validar = $("#frmEducacionSuperior");
        if (validar.valid()) {
            var dataForm = $("#frmEducacionSuperior").serializeFormJSON();
            var url = basePath + "EducacionSuperior/EducacionSuperiorInsertarJson";
            fncRegistrar(dataForm, url, false, "");
        }
    }); 
    $(document).on('click', '#btnGuardarPostgrado', function () {
        var validar = $("#frmPostgrado");
        if (validar.valid()) {
            var dataForm = $("#frmPostgrado").serializeFormJSON();
            var url = basePath + "Postgrado/PostgradoInsertarJson";
            fncRegistrar(dataForm, url, false, "");
        }
    }); 
    $(document).on('click', '#btnGuardarOfimatica', function () {
        var validar = $("#frmOfimatica");
        if (validar.valid()) {
            var dataForm = $("#frmOfimatica").serializeFormJSON();
            var url = basePath + "Ofimatica/OfimaticaInsertarJson";
            fncRegistrar(dataForm, url, false, "");
        }
    });   
    $(document).on('click', '#btnGuardarIdioma', function () {
        var validar = $("#frmIdioma");
        if (validar.valid()) {
            var dataForm = $("#frmIdioma").serializeFormJSON();
            var url = basePath + "Idioma/IdiomaInsertarJson";
            fncRegistrar(dataForm, url, false, "");
        }
    });
    $(document).on('click', '#btnGuardarExperiencia', function () {
        var validar = $("#frmExperiencia");
        if (validar.valid()) {
            var dataForm = $("#frmExperiencia").serializeFormJSON();
            var url = basePath + "Experiencia/ExperienciaInsertarJson";
            fncRegistrar(dataForm, url, false, "");
        }
    }); 
    $(document).on('click', '#btnGuardarInformacionAdicional', function () {
        var formData = new FormData(document.getElementById("frmInformacionAdicional"));
        $.ajax({
            url: document.location.origin +"/Postulante/PostulanteInsertarInformacionAdicionalJson",
            type: "post",
            dataType: "html",
            data: formData,
            cache: false,
            contentType: false,
            processData: false
        })
            .done(function (res) {
                console.log(res);
            });
    });    
    $('#boton-archivo').change(function () {
        var data = new FormData();
        $.each(jQuery('#boton-archivo')[0].files, function (i, file) {
            data.append('file-' + i, file);
        });      
        
        $.ajax({
            url: document.location.origin + "/Postulante/PostulanteSubirFotoJson",
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            dataType: "html",
            method: 'POST',
            type: 'POST', // For jQuery < 1.9
            success: function (data) {               
                window.location.reload(true);
               
            }
        });
    });

});
$("#frmDatosPersonales")
    .validate({
        rules: {
            per_nombre:
            {
                required: true,

            },
            per_apellido_pat:
            {
                required: true,

            },
            per_apellido_mat:
            {
                required: true,

            },       
            per_numdoc:
            {
                required: true,

            },
            per_fechanacimiento:
            {
                required: true,

            },
            per_celular:
            {
                required: true,

            },
            per_telefono:
            {
                required: true,

            },
            per_sexo:
            {
                required: true,
            }
        },
        messages: {
            per_nombre:
            {
                required: 'REQUERIDO',
            },
            per_apellido_pat:
            {
                required: 'REQUERIDO',
            },
            per_apellido_mat:
            {
                required: 'REQUERIDO',
            },       
            per_numdoc:
            {
                required: 'REQUERIDO',
            },
            per_fechanacimiento:
            {
                required: 'REQUERIDO',
            },
            per_celular:
            {
                required: 'REQUERIDO',
            },
            per_telefono:
            {
                required: 'REQUERIDO',
            },
            per_sexo:
            {
                required: 'REQUERIDO',
            }

        },
        errorPlacement: function (error, element) {
            if (element.is(":radio") || element.is(":checkbox")) {
                element.closest('.option-group').after(error);
            }
            else {
                error.insertAfter(element);
            }
        }
    });
$("#frmEducacionBasica")
    .validate({
        rules: {
            eba_tipo:
            {
                required: true,

            },
            eba_nombre:
            {
                required: true,

            },
            eba_condicion:
            {
                required: true,

            }
          
        },
        messages: {
            eba_tipo:
            {
                required: 'REQUERIDO',
            },
            eba_nombre:
            {
                required: 'REQUERIDO',
            },
            eba_condicion:
            {
                required: 'REQUERIDO',
            }
        },
        errorPlacement: function (error, element) {
            if (element.is(":radio") || element.is(":checkbox")) {
                element.closest('.option-group').after(error);
            }
            else {
                error.insertAfter(element);
            }
        }
    });