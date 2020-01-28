﻿
var PanelContenido = function () {

    var _sort_seccion = function (menu_id) {
        $('#accordion_' + menu_id).sortable({
            cursor: 'move',
            placeholder: 'box placeholder',
            stop: function (event, ui) {
                let lista_orden = [];
                var lista = $('#accordion_' + menu_id +' div.panel-default')
                $.each(lista, function (index, value) {
                    lista_orden.push({
                        sec_id: $(this).data("id"),
                        sec_orden: (index + 1)
                    });
                    $(this).find("span.sec_orden").text((index + 1));
                });
                console.log(lista_orden);
                responseSimple({
                    url: "IntranetSeccion/IntranetSeccionEditarOrdenJson",
                    data: JSON.stringify({ arraySecciones: lista_orden }),
                    refresh: false,
                    callBackSuccess: function (response) {
                        if (response.respuesta) {
                            //PanelContenido.init_ListarSecciones(menu_id);
                        }
                    }
                });
            }
        });
    };

    var _sort_elemento = function (seccion_id) {
        $('.tbody_elemento_' + seccion_id).sortable({
            cursor: 'move',
            placeholder: 'box placeholder',
            stop: function (event, ui) {
                let lista_orden = [];
                var lista = $('#tbody_elemento_' + seccion_id + ' tr')
                $.each(lista, function (index, value) {
                    lista_orden.push({
                        sec_id: $(this).data("id"),
                        sec_orden: (index + 1)
                    });
                    $(this).find("span.sec_orden").text((index + 1));
                });
                console.log(lista_orden);
                responseSimple({
                    url: "IntranetSeccion/IntranetSeccionEditarOrdenJson",
                    data: JSON.stringify({ arraySecciones: lista_orden }),
                    refresh: false,
                    callBackSuccess: function (response) {
                        if (response.respuesta) {
                            //PanelContenido.init_ListarSecciones(menu_id);
                        }
                    }
                });
            }
        });
    };

    var _ListarMenus = function () {
        $("#tabmenu").html("");
        $("#tabcontenido").html("");
        responseSimple({
            url: "IntranetMenu/IntranetMenuSeccionListarJson",
            refresh: false,
            callBackSuccess: function (response) {
                var data = response.data;
                var li = "";
                var div = "";
                var activo = "";
                var contenidoDiv = "";
                var menu_id_ = 0;
                $.each(data, function (index, value) {
                    activo = (index == 0) ? "in active" : "";
                    
                    li += '<li class="' + activo + '" data-id="' + value.menu_id +'">' +
                        '<a data-toggle="tab" href="#_tab_contenido_' + value.menu_id + '"> ' +
                                '<i class="red ace-icon fa fa-tachometer bigger-110"></i> '+
                                value.menu_titulo+
                            '</a >' +
                        '</li >';
                    
                    if (index == 0) {
                        var data_seccion = value.secciones;
                        var contentd = "";
                        $.each(data_seccion, function (index_, value_) {
                            var estadoText = (value_.sec_estado == "A") ? "Activo" : "Inactivo";
                            var estadoClase = (value_.sec_estado == "A") ? "success" : "danger";
                            var estadoActivo = (value_.sec_estado == "A") ? "selected" : "";
                            var estadoInActivo = (value_.sec_estado == "I") ? "selected" : "";
                            contentd += '<div class="panel panel-default" data-id="' + value_.sec_id + '">' +
                                            '<div class="panel-heading">'+
                                                '<h4 class="panel-title">'+
                                                        '<a class="accordion-toggle cabecera-seccion collapsed" data-id="' + value_.sec_id +'" data-toggle="collapse" data-parent="accordion_' + value.menu_id + '" href="#collapse_' + value_.sec_id + '">'+
                                                        '<i class="ace-icon fa fa-angle-right bigger-110" data-icon-hide="ace-icon fa fa-angle-down" data-icon-show="ace-icon fa fa-angle-right"></i>'+
                                '<span class="sec_orden label label-default label-white middle">'+(index_ +1)+ '</span> - Seccion ID_' + value_.sec_id + ' <div class="widget-toolbar" style="margin-top: -7px;line-height: 24px;"><span id="span_estado_' + value_.sec_id + '" class="label label-' + estadoClase+' label-white middle">' + estadoText+'</span></div>'+
									                '</a>'+
                                                '</h4>'+
								            '</div >'+

                                            '<div class="panel-collapse collapse " id="collapse_' + value_.sec_id + '">' +
                                               '<div class="panel-body" id="menu_panel' + value_.sec_id + '">' +
                                                    '<div class="row">'+
                                                        '<div class="col-md-12"><div class="row">' +
                                                                '<div class="col-md-2">' +
                                                                    '<div class="input-group">' +
                                                                        '<span class="input-group-addon">' +
                                                                        'Estado' +
                                                                        '</span>' +
                                '<select data-id="' + value_.sec_id + '" data-menu_id="' + value.menu_id + '" class="form-control input-sm select-estado-seccion"><option value="A" ' + estadoActivo + '>Activo</option><option value="I" ' + estadoInActivo +'>Inactivo</option></select>' +
                                                                    '</div>' +
                                                                '</div>' +
                                '<div class="col-md-10"><div class="row">' +
                                '<div class="col-md-3 col-md-offset-4 col-sm-2 col-xs-12"><button class="btn btn-white btn-warning btn-sm btn-block btn-round btn_ordenar_elemento_seccion" data-id="' + value_.sec_id + '" data-rel="tooltip" title="Ordenar Elementos"><i class="ace-icon glyphicon glyphicon-list-alt"></i> Ordenar </button></div>' +
                                '<div class="col-md-3 col-sm-5 col-xs-12"><button class="btn btn-white btn-success btn-sm btn-block btn-round btn_nuevo_elemento_seccion" data-id="' + value_.sec_id + '" data-rel="tooltip" title="Nuevo Elemento"><i class="ace-icon fa fa-file"></i> Nuevo Elemento </button></div>' +
                                '<div class="col-md-2 col-sm-5 col-xs-12"><button class="btn btn-white btn-danger btn-sm btn-block btn-round btn-eliminar-seccion" data-id="' + value_.sec_id + '" data-menu_id="' + value.menu_id + '" data-rel="tooltip" title="Eliminar Seccion"><i class="ace-icon fa fa-trash"></i> Eliminar</button></div>' +
                                                                '</div></div>' +
                                                        '</div></div>' +
                                                        '<div class="col-md-12"><div class="hr hr8 hr-double"></div><div class="" id="seccion_lista_elemento_' + value_.sec_id+'"></div></div>' +
                                                    '</div>' +
                                                '</div>' +
                                            '</div>'+
							            '</div>';
                        });
                        contenidoDiv += '<div id="accordion_' + value.menu_id + '" class="accordion-style1 panel-group">' + contentd + '</div>';
                    }
                    else {
                        contenidoDiv = "";
                    }
                    div += '<div id="_tab_contenido_' + value.menu_id + '" class="tab-pane ' + activo + '">' +
                        '<div class="row" style="margin-bottom:8px">' +
                        '<div class="col-md-2 col-sm-6 col-xs-6 pull-right"><button class="btn btn-white btn-success btn-sm btn-block btn-round btn_nueva_seccion" data-id="' + value.menu_id + '" data-rel="tooltip" title="Nueva Seccion"><i class="ace-icon fa fa-file"></i> Nueva Seccion </button></div>' +
                        '<div class="col-md-3 col-sm-6 col-xs-6 pull-right"><button class="btn btn-white btn-warning btn-sm btn-block btn-round btn_ordenar_seccion" data-id="' + value.menu_id + '" data-rel="tooltip" title="Ordenar Seccion(es)"><i class="ace-icon glyphicon glyphicon-list-alt"></i> Ordenar Seccion(es) </button></div>' +
                        '</div>' +
                                contenidoDiv +
                        '</div>';
                });
                $("#tabmenu").html(li);
                $("#tabcontenido").html(div);
                
            }
        });
    };

    var _ListarSecciones = function (menu_id) {
        $("#_tab_contenido_" + menu_id).html("");
        var dataForm = {
            menu_id: menu_id
        };
        responseSimple({
            url: "IntranetSeccion/IntranetSeccionListarTodoxMenuIDJson",
            data: JSON.stringify(dataForm),
            refresh: false,
            callBackSuccess: function (response) {
                var data = response.data;
                var contentd = "";
                $.each(data, function (index, value_) {
                    var estadoText = (value_.sec_estado == "A") ? "Activo" : "Inactivo";
                    var estadoClase = (value_.sec_estado == "A") ? "success" : "danger";
                    var estadoActivo = (value_.sec_estado == "A") ? "selected" : "";
                    var estadoInActivo = (value_.sec_estado == "I") ? "selected" : "";
                    contentd += '<div class="panel panel-default" data-id="' + value_.sec_id + '">' +
                        '<div class="panel-heading">' +
                        '<h4 class="panel-title">' +
                        '<a class="accordion-toggle cabecera-seccion collapsed" data-id="' + value_.sec_id + '" data-toggle="collapse" data-parent="accordion_' + menu_id + '" href="#collapse_' + value_.sec_id + '">' +
                        '<i class="ace-icon fa fa-angle-right bigger-110" data-icon-hide="ace-icon fa fa-angle-down" data-icon-show="ace-icon fa fa-angle-right"></i>' +
                        '<span class="sec_orden label label-default label-white middle">'+(index + 1) + '</span> - Seccion ID_' + value_.sec_id + ' <div class="widget-toolbar" style="margin-top: -7px;line-height: 24px;"><span id="span_estado_' + value_.sec_id + '" class="label label-' + estadoClase + ' label-white middle">' + estadoText + '</span></div>' +
                        '</a>' +
                        '</h4>' +
                        '</div >' +

                        '<div class="panel-collapse collapse " id="collapse_' + value_.sec_id + '">' +
                        '<div class="panel-body" id="menu_panel' + value_.sec_id + '">' +
                        '<div class="row">' +
                        '<div class="col-md-12"><div class="row">' +
                        '<div class="col-md-2">' +
                        '<div class="input-group">' +
                        '<span class="input-group-addon">' +
                        'Estado' +
                        '</span>' +
                        '<select data-id="' + value_.sec_id + '" data-menu_id="' + menu_id + '" class="form-control input-sm select-estado-seccion"><option value="A" ' + estadoActivo + '>Activo</option><option value="I" ' + estadoInActivo+'>Inactivo</option></select>' +
                        '</div>' +
                        '</div>' +
                        '<div class="col-md-10"><div class="row">' +
                        '<div class="col-md-3 col-md-offset-4 col-sm-2 col-xs-12"><button class="btn btn-white btn-warning btn-sm btn-block btn-round btn_ordenar_elemento_seccion" data-id="' + value_.sec_id + '" data-rel="tooltip" title="Ordenar Elementos"><i class="ace-icon glyphicon glyphicon-list-alt"></i> Ordenar </button></div>' +
                        '<div class="col-md-3 col-sm-5 col-xs-12"><button class="btn btn-white btn-success btn-sm btn-block btn-round btn_nuevo_elemento_seccion" data-id="' + value_.sec_id + '" data-rel="tooltip" title="Nuevo Elemento"><i class="ace-icon fa fa-file"></i> Nuevo Elemento </button></div>' +
                        '<div class="col-md-2 col-sm-5 col-xs-12"><button class="btn btn-white btn-danger btn-sm btn-block btn-round btn-eliminar-seccion" data-id="' + value_.sec_id + '" data-menu_id="' + menu_id + '" data-rel="tooltip" title="Eliminar Seccion"><i class="ace-icon fa fa-trash"></i> Eliminar</button></div>' +
                        
                        '</div></div>' +
                        '</div></div>' +
                        '<div class="col-md-12"><div class="hr hr8 hr-double"></div><div class="" id="seccion_lista_elemento_' + value_.sec_id + '"></div></div>' +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '</div>';
                });
                contentd = '<div class="row" style="margin-bottom:8px">' +
                    '<div class="col-md-2 col-sm-6 col-xs-6 pull-right"><button class="btn btn-white btn-success btn-sm btn-block btn-round btn_nueva_seccion" data-id="' + menu_id + '" data-rel="tooltip" title="Nueva Seccion"><i class="ace-icon fa fa-file"></i> Nueva Seccion </button></div>' +
                    '<div class="col-md-3 col-sm-6 col-xs-6 pull-right"><button class="btn btn-white btn-warning btn-sm btn-block btn-round btn_ordenar_seccion" data-id="' + menu_id + '" data-rel="tooltip" title="Ordenar Seccion(es)"><i class="ace-icon glyphicon glyphicon-list-alt"></i> Ordenar Seccion(es) </button></div>' +
                    '</div>' +
                    '<div id="accordion_' + menu_id + '" class="accordion-style1 panel-group">' + contentd + '</div>';

                $("#_tab_contenido_" + menu_id).html(contentd);

            }
        });
    };

    var _ListarElementos = function (sec_id) {
        var dataForm = {
            sec_id: sec_id
        };
        $("#seccion_lista_elemento_" + sec_id).html("");
        responseSimple({
            url: "IntranetElemento/IntranetElementoListarxSeccionIDJson",
            data: JSON.stringify(dataForm),
            refresh: false,
            callBackSuccess: function (response) {
                var rows = response.data;
                var tr = "";
                $.each(rows, function (index, value) {
                    var boton = '<button data-rel="tooltip" title="Editar" class="btn btn-primary  btn-xs btn-round btn-white btn_editar_elemento" data-id="' + value.elem_id + '" data-seccion_id="' + sec_id +'"><i class="ace-icon fa fa-pencil"></i> </button>' +
                        ' <button data-rel="tooltip" title="Eliminar" class="btn btn-danger  btn-xs btn-round btn-white btn_eliminar_elemento" data-id="' + value.elem_id + '" data-seccion_id="' + sec_id+'"><i class="ace-icon fa fa-trash"></i> </button>';

                    var clase_estado = 'success';
                    var estado = "Activo";
                    if (value.elem_estado == "I") {
                        clase_estado = 'danger';
                        estado = "Inactivo";
                    };

                    var detalle = '<div class="action-buttons">' +
                        '<a data-id="' + value.elem_id + '" href="javascript:void(0);" class="blue bigger-140 btn_detalle_elemento show-details-btn" title = "Detalle">' +
                        '<i class="ace-icon fa fa-angle-double-up"></i>' +
                        '<span class="sr-only">Detalle</span>' +
                        '</a>' +
                        '</div>';
                    tr += '<tr data-id="' + value.elem_id + '" data-orden="' + value.elem_orden + '"><td class="center">' + detalle + '</td><td><span class="elem_orden label label-default label-white middle">'+(index+1)+'</span> ' + value.tipo_nombre + '</td><td>' + value.elem_titulo + '</td><td><span class="label label-' + clase_estado + ' label-white middle">' + estado + '</span></td><td>' + boton + '</td></tr>';
                });
                if (rows.length == 0) {
                    tr = '<tr><td colspan="5"><div class="alert alert-warning" style="margin-bottom:0px;">No tiene Data ...</div></td></tr>';
                }


                tr = '<table class="table table-bordered table-condensed table-hover"><thead><tr><th style="width: 5%;"></th><th style="width: 18%;">Tipo</th><th>Titulo</th><th style="width: 10%;">Estado</th><th style="width: 15%;">Acciones</th></tr></thead><tbody class="tbody_elemento_' + sec_id+'">' + tr + '</tbody></table>';
                $("#seccion_lista_elemento_" + sec_id).html(tr);
            }
        })
    };

    var _ListarElementosModal = function (fk_seccion_elemento, detalle_elemento_id) {
        var dataForm = {
            fk_seccion_elemento: fk_seccion_elemento
        };

        responseSimple({
            url: "IntranetElementoModal/IntranetElementoModalListarxSeccionElementoJson",
            data: JSON.stringify(dataForm),
            refresh: false,
            callBackSuccess: function (response) {
                var rows = response.data;
                console.log(rows.length);
                var tr = "";
                $.each(rows, function (index, value) {
                    var boton = '<button data-rel="tooltip" title="Editar" class="btn btn-primary  btn-xs btn-round btn-white btn_editar_detalle_elemento" data-id=' + value.emod_id + '><i class="ace-icon fa fa-pencil"></i> </button>' +
                        ' <button data-rel="tooltip" title="Eliminar" class="btn btn-danger  btn-xs btn-round btn-white btn_eliminar_detalle_elemento" data-id=' + value.emod_id + '><i class="ace-icon fa fa-trash"></i> </button>';

                    var clase_estado = 'success';
                    var estado = "Activo";
                    if (value.emod_estado == "I") {
                        clase_estado = 'danger';
                        estado = "Inactivo";
                    };

                    var detalle = '<div class="action-buttons">' +
                        '<a data-id="' + value.emod_id + '" href="javascript:void(0);" class="blue bigger-140 btn_detalle_elemento_modal show-details-btn" title = "Detalle">' +
                        '<i class="ace-icon fa fa-angle-double-up"></i>' +
                        '<span class="sr-only">Detalle</span>' +
                        '</a>' +
                        '</div>';
                    tr += '<tr data-id="' + value.emod_id + '" data-orden="' + value.emod_orden + '"><td class="center">' + detalle + '</td><td>' + value.tipo_nombre + '</td><td>' + value.emod_titulo + '</td><td><span class="label label-' + clase_estado + ' label-white middle">' + estado + '</span></td><td>' + boton + '</td></tr>';
                });

                var boton_nuevo = ' <div class="row" style="margin-bottom:10px;">' +
                    '<div class="col-md-3 col-sm-4 col-xs-6 pull-right"><button class="btn btn-white btn-success btn-sm btn-block btn-round btn_nuevo_elemento_modal" data-seccion="' + fk_seccion_elemento + '" data-id="' + detalle_elemento_id + '" data-rel="tooltip" title="Nuevo Elemento Modal"> <i class="fa fa-file"></i> Nuevo Elemento Modal</button></div>' +
                    '</div>';

                tr = boton_nuevo + '<table class="table table-bordered table-condensed table-hover"><thead><tr><th style="width: 5%;"></th><th style="width: 18%;">Tipo</th><th>Titulo</th><th style="width: 10%;">Estado</th><th style="width: 15%;">Acciones</th></tr></thead><tbody>' + tr + '</tbody></table>';
    
                if (rows.length > 0) {
                    $('#tr_elemento_contenido_modal_' + detalle_elemento_id).html('<td colspan="6" style="padding-left: 2%;"><div class="table-detail"><div class="rows">' + tr + '</div></div></td>');
                }
                else {
                    $('#tr_elemento_contenido_modal_' + detalle_elemento_id).html('<td colspan="6" style="padding-left: 2%;"><div class="alert alert-warning" style="margin-bottom:0px;">No tiene Data ...</div></td>');
                }
            }
        });
    };

    var _componentes = function () {
        /////////////////////////////listar segmentos
        $(document).on('click', '#tabmenu li', function () {
            var menu_id = $(this).data("id");
            PanelContenido.init_ListarSecciones(menu_id);
        });

        $(document).on('click', '.cabecera-seccion', function () {
            var sec_id = $(this).data("id");
            if (!$(this).hasClass("collapsed")) {
                if (sec_id > 0 || sec_id != "") {
                    PanelContenido.init_ListarElementos(sec_id);
                }
            }
        });

        $(document).on('click', '.btn_detalle_elemento', function (e) {
            e.preventDefault();
            var elemento_id = $(this).data("id");
            var act_tr = $(this).closest("tr");
            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
            if ($('#tr_elemento_contenido_' + elemento_id).hasClass("open")) {
                $('#tr_elemento_contenido_' + elemento_id).removeClass("open");
            }
            else {
                $('#tr_elemento_contenido_' + elemento_id).remove();
                $('<tr id="tr_elemento_contenido_' + elemento_id + '" class="detail-row open"><td colspan="5"><div class="alert alert-warning" style="margin-bottom:0px;">Cargando Data ...</div></td></tr>').insertAfter(act_tr);

                var dataForm = {
                    elem_id: elemento_id
                };
                responseSimple({
                    url: "IntranetDetalleElemento/IntranetDetalleElementoListarxElementoIDJson",
                    data: JSON.stringify(dataForm),
                    refresh: false,
                    callBackSuccess: function (response) {
                        var rows = response.data;
                        var tr = "";
                        $.each(rows, function (index, value) {
                            var boton = '<button data-rel="tooltip" title="Editar" class="btn btn-primary  btn-xs btn-round btn-white btn_editar_detalle_elemento" data-id=' + value.detel_id + '><i class="ace-icon fa fa-pencil"></i> </button>' +
                                ' <button data-rel="tooltip" title="Eliminar" class="btn btn-danger  btn-xs btn-round btn-white btn_eliminar_detalle_elemento" data-id=' + value.detel_id + '><i class="ace-icon fa fa-trash"></i> </button>';

                            var clase_estado = 'success';
                            var estado = "Activo";
                            if (value.detel_estado == "I") {
                                clase_estado = 'danger';
                                estado = "Inactivo";
                            };
                            var posicion = '';
                            if (value.detel_posicion == 'L') {
                                posicion = 'Izquierda';
                            }
                            else if (value.detel_posicion == 'C') {
                                posicion = 'Centro';
                            }
                            else if (value.detel_posicion == 'R') {
                                posicion = 'Derecha';
                            }
                            else {
                                posicion = '';
                            };

                            var detalle = '<div class="action-buttons">' +
                                '<a data-id="' + value.detel_id + '" data-seccion="' + value.fk_seccion_elemento+'" href="javascript:void(0);" class="blue bigger-140 btn_elemento_modal show-details-btn" title = "Detalle">' +
                                '<i class="ace-icon fa fa-angle-double-up"></i>' +
                                '<span class="sr-only">Detalle</span>' +
                                '</a>' +
                                '</div>';

                            tr += '<tr  data-id="' + value.detel_id + '" data-orden="' + value.detel_orden + '"><td class="center">' + detalle+'</td><td>' + value.detel_descripcion + '</td><td>' + value.detel_nombre + '.' + value.detel_extension + '</td><td>' + posicion + '</td><td><span class="label label-' + clase_estado + ' label-white middle">' + estado + '</span></td><td>' + boton + '</td></tr>';
                        });

                        var boton_nuevo = ' <div class="row" style="margin-bottom:10px;">' +
                            '<div class="col-md-3 col-sm-4 col-xs-6 pull-right"><button class="btn btn-white btn-success btn-sm btn-block btn-round btn_nuevo_detalle_elemento" data-id="' + elemento_id + '" data-rel="tooltip" title="Nuevo Detalle Elemento"> <i class="fa fa-file"></i> Nuevo Detalle Elemento</button></div>' +
                            '</div>';


                        tr = boton_nuevo +'<table class="table table-bordered table-condensed table-xs table-hover"><thead><tr><th style="width: 5%;"></th><th>Texto</th><th>Imagen</th><th style="width: 12%;">Ubicacion</th><th style="width: 10%;">Estado</th><th style="width: 15%;">Acciones</th></tr></thead><tbody>' + tr + '</tbody></table>';
                        if (rows.length > 0) {
                            $('#tr_elemento_contenido_' + elemento_id).html('<td colspan="5" style="padding-left: 2%;"><div class="table-detail"><div class="rows">' + tr + '</div></div></td>');
                        }
                        else {
                            $('#tr_elemento_contenido_' + elemento_id).html('<td colspan="5" style="padding-left: 2%;"><div class="alert alert-warning" style="margin-bottom:0px;">No tiene Data ...</div></td>');
                        }
                        
                    }
                });
            }
        });

        $(document).on('click', '.btn_elemento_modal', function (e) {
            e.preventDefault();
            var detalle_elemento_id = $(this).data("id");
            var fk_seccion_elemento = $(this).data("seccion");
            var act_tr = $(this).closest("tr");
            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
            if ($('#tr_elemento_contenido_modal_' + detalle_elemento_id).hasClass("open")) {
                $('#tr_elemento_contenido_modal_' + detalle_elemento_id).removeClass("open");
            }
            else {
                $('#tr_elemento_contenido_modal_' + detalle_elemento_id).remove();
                $('<tr id="tr_elemento_contenido_modal_' + detalle_elemento_id + '" class="detail-row open"><td colspan="6"><div class="alert alert-warning" style="margin-bottom:0px;">Cargando Data ...</div></td></tr>').insertAfter(act_tr);

                PanelContenido.init_ListarElementosModal(fk_seccion_elemento, detalle_elemento_id);
            }
        });

        $(document).on('click', '.btn_detalle_elemento_modal', function (e) {
            e.preventDefault();
            var elemento_modal_id = $(this).data("id");
            var act_tr = $(this).closest("tr");
            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
            if ($('#tr_elemento_contenido_detalle_modal' + elemento_modal_id).hasClass("open")) {
                $('#tr_elemento_contenido_detalle_modal' + elemento_modal_id).removeClass("open");
            }
            else {
                $('#tr_elemento_contenido_detalle_modal' + elemento_modal_id).remove();
                $('<tr id="tr_elemento_contenido_detalle_modal' + elemento_modal_id + '" class="detail-row open"><td colspan="5"><div class="alert alert-warning" style="margin-bottom:0px;">Cargando Data ...</div></td></tr>').insertAfter(act_tr);

                var dataForm = {
                    fk_elemento_modal: elemento_modal_id
                };
                responseSimple({
                    url: "IntranetDetalleElementoModal/IntranetDetalleElementoModalListarxElementoModalJson",
                    data: JSON.stringify(dataForm),
                    refresh: false,
                    callBackSuccess: function (response) {
                        var rows = response.data;
                        var tr = "";
                        $.each(rows, function (index, value) {
                            var boton = '<button data-rel="tooltip" title="Editar" class="btn btn-primary  btn-xs btn-round btn-white btn_editar_detalle_elemento_modal" data-id=' + value.detelm_id + '><i class="ace-icon fa fa-pencil"></i> </button>' +
                                ' <button data-rel="tooltip" title="Eliminar" class="btn btn-danger  btn-xs btn-round btn-white btn_eliminar_detalle_elemento_modal" data-id=' + value.detelm_id + '><i class="ace-icon fa fa-trash"></i> </button>';

                            var clase_estado = 'success';
                            var estado = "Activo";
                            if (value.detelm_estado == "I") {
                                clase_estado = 'danger';
                                estado = "Inactivo";
                            };
                            var posicion = '';
                            if (value.detelm_posicion == 'L') {
                                posicion = 'Izquierda';
                            }
                            else if (value.detelm_posicion == 'C') {
                                posicion = 'Centro';
                            }
                            else if (value.detelm_posicion == 'R') {
                                posicion = 'Derecha';
                            }
                            else {
                                posicion = '';
                            };


                            tr += '<tr  data-id="' + value.detelm_id + '" data-orden="' + value.detelm_orden + '"><td>' + value.detelm_descripcion + '</td><td>' + value.detelm_nombre + '.' + value.detelm_extension + '</td><td>' + posicion + '</td><td><span class="label label-' + clase_estado + ' label-white middle">' + estado + '</span></td><td>' + boton + '</td></tr>';
                        });

                        var boton_nuevo = ' <div class="row" style="margin-bottom:10px;">' +
                            '<div class="col-md-5 col-sm-4 col-xs-6 pull-right"><button class="btn btn-white btn-success btn-sm btn-block btn-round btn_nuevo_detalle_elemento_modal" data-id="' + elemento_modal_id + '" data-rel="tooltip" title="Nuevo Detalle Elemento Modal"> <i class="fa fa-file"></i> Nuevo Detalle Elemento Modal</button></div>' +
                            '</div>';


                        tr = boton_nuevo+'<table class="table table-bordered table-condensed table-xs table-hover"><thead><tr><th>Texto</th><th>Imagen</th><th style="width: 12%;">Ubicacion</th><th style="width: 10%;">Estado</th><th style="width: 15%;">Acciones</th></tr></thead><tbody>' + tr + '</tbody></table>';
                        if (rows.length > 0) {
                            $('#tr_elemento_contenido_detalle_modal' + elemento_modal_id).html('<td colspan="5" style="padding-left: 2%;"><div class="table-detail"><div class="rows">' + tr + '</div></div></td>');
                        }
                        else {
                            $('#tr_elemento_contenido_detalle_modal' + elemento_modal_id).html('<td colspan="5" style="padding-left: 2%;"><div class="alert alert-warning" style="margin-bottom:0px;">No tiene Data ...</div></td>');
                        }

                    }
                });
            }
        });

        //////////////////////////////////seccion

        $(document).on('click', '.btn_ordenar_seccion', function (e) {
            var menu_id = $(this).data("id");
            var spans = $(".sec_orden");
            $.each(spans, function (index, value) {
                $(this).removeClass("label-default");
                $(this).addClass("label-warning");
            });
            PanelContenido.init_Sort_Seccion(menu_id);
        });

        $(document).on('click', '.btn_nueva_seccion', function (e) {
            var menu_id = $(this).data("id");
            $("#fk_menu").val(menu_id);
            $("#sec_id").val(0);
            $("#modalFormularioSeccion").modal("show");
        });

        $(document).on('click', '.btn_guardar_seccion', function () {
            // $("#form_seccion").submit();
            var dataForm = $('#form_seccion').serializeFormJSON();
            var url = '';
            if ($("#sec_id").val() == 0) {
                url = 'IntranetSeccion/IntranetSeccionInsertarJson';
            }
            responseSimple({
                url: url,
                refresh: false,
                data: JSON.stringify(dataForm),
                callBackSuccess: function (response) {
                    if (response.respuesta) {
                        var menu_id = $("#fk_menu").val();
                        PanelContenido.init_ListarSecciones(menu_id);
                    }
                    $("#modalFormularioSeccion").modal("hide");
                },
            })
        });

        $(document).on("click", ".btn-eliminar-seccion", function (e) {
            var seccion_id = $(this).data("id");
            var menu_id = $(this).data("menu_id");
            if (seccion_id != "" || seccion_id > 0) {
                messageConfirmation({
                    content: '¿Esta seguro que desea ELIMINAR esta Seccion?',
                    callBackSAceptarComplete: function () {
                        responseSimple({
                            url: "IntranetSeccion/IntranetSeccionEliminarJson",
                            data: JSON.stringify({ sec_id: seccion_id }),
                            refresh: false,
                            callBackSuccess: function (response) {
                                if (response.respuesta) {
                                    PanelContenido.init_ListarSecciones(menu_id);
                                }
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

        $(document).on('input', '.input-number', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        });
        
        $(document).on('change', '.select-estado-seccion', function () {
            var seccion_id = $(this).data("id");
            var sec_estado = $(this).val();
            var menu_id = $(this).data("menu_id");
            var dataForm = {
                sec_id: seccion_id,
                sec_estado: sec_estado
            }
            responseSimple({
                url: "IntranetSeccion/IntranetSeccionEditarJson",
                data: JSON.stringify(dataForm),
                refresh: false,
                callBackSuccess: function (response) {
                    if (!response.respuesta) {
                        PanelContenido.init_ListarSecciones(menu_id);
                    }
                    else {
                        var texto = (sec_estado == "A") ? "Activo" : "Inactivo";
                        var clasetexto = (sec_estado == "A") ? "label label-success label-white middle" : "label label-danger label-white middle";
                        $("#span_estado_" + seccion_id).text(texto);
                        $("#span_estado_" + seccion_id).removeClass();
                        $("#span_estado_" + seccion_id).addClass(clasetexto);
                    }
                }
            });
        });


        ///////////////////////////////////////////////////////////////////////elemento
        $(document).on('click', '.btn_nuevo_elemento_seccion', function (e) {
            var seccion_id = $(this).data("id");
            $("#fk_seccion").val(seccion_id);
            $("#elem_id").val(0);
            $("#elem_titulo").val("");
            $("#div_fk_tipo_elemento").removeClass("hidden");
            $("#div_texto_fk_tipo_elemento").addClass("hidden");
            $("#fk_tipo_elemento").val("").trigger('change');
            _objetoForm_form_elemento.resetForm();
            $("#tituloModalElemento").text("Nuevo");
            $("#modalFormularioElemento").modal("show");
        });

        $(document).on('click', '.btn_editar_elemento', function () {
            var elem_id = $(this).data('id');
            var dataForm = {
                elem_id: elem_id,
            }
            $("#div_fk_tipo_elemento").addClass("hidden");
            $("#div_texto_fk_tipo_elemento").removeClass("hidden");
            responseSimple({
                url: 'IntranetElemento/IntranetElementoIdObtenerJson',
                data: JSON.stringify(dataForm),
                refresh: false,
                callBackSuccess: function (response) {
                    if (response.respuesta) {
                        var elemento = response.data;
                        
                        $("#fk_tipo_elemento").val(elemento.fk_tipo_elemento).trigger('change');
                        $("#fk_seccion").val(elemento.fk_seccion);
                        $("#elem_id").val(elemento.elem_id);
                        $("#elem_titulo").val(elemento.elem_titulo);
                        $("#elem_estado").val(elemento.elem_estado);
                        $("#elem_orden").val(elemento.elem_orden);
                        $("#texto_fk_tipo_elemento").html($("#fk_tipo_elemento option:selected").text());
                        $("#modalFormularioElemento").modal("show");
                    }
                }
            })
        })

        $(document).on('click', '.btn_guardar_elemento', function () {
            $("#form_elemento").submit();
            if (_objetoForm_form_elemento.valid()) {
                var dataForm = $('#form_elemento').serializeFormJSON();
                var url = "";
                if ($("#elem_id").val() == 0) {
                    url = 'IntranetElemento/IntranetElementoInsertarJson';
                }
                else {
                    url = 'IntranetElemento/IntranetElementoEditarJson';
                }
                responseSimple({
                    url: url,
                    data: JSON.stringify(dataForm),
                    refresh: false,
                    callBackSuccess: function (response) {
                        //console.log(response);
                        var respuesta = response.respuesta;
                        if (respuesta) {
                            var seccion_id = $("#fk_seccion").val();
                            PanelContenido.init_ListarElementos(seccion_id);
                            $("#modalFormularioElemento").modal("hide");
                            //refresh(true);
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

        $(document).on("click", ".btn_eliminar_elemento", function (e) {
            var elemento_id = $(this).data("id");
            if (elemento_id != "" || elemento_id > 0) {
                messageConfirmation({
                    content: '¿Esta seguro que desea ELIMINAR este Elemento?',
                    callBackSAceptarComplete: function () {
                        var seccion_id = $("#fk_seccion").val();
                        responseSimple({
                            url: "IntranetElemento/IntranetElementoElementoEliminarJson",
                            data: JSON.stringify({ elem_id: elemento_id }),
                            refresh: false,
                            callBackSuccess: function (response) {
                                if (response.respuesta) {
                                    PanelContenido.init_ListarElementos(seccion_id);
                                }
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

        $(document).on('click', '.btn_ordenar_elemento_seccion', function (e) {
            var seccion_id = $(this).data("id");
            var spans = $('tbody.tbody_elemento_' + seccion_id + ' tr span.elem_orden ');
            console.log(spans)
            $.each(spans, function (index, value) {
                $(this).removeClass("label-default");
                $(this).addClass("label-warning");
            });
            PanelContenido.init_Sort_Elemento(seccion_id);
        });

    };

    var _metodos = function () {

        selectResponse({
            url: "IntranetTipoElemento/IntranetTipoElementoListarJson",
            select: "fk_tipo_elemento",
            campoID: "tipo_id",
            CampoValor: "tipo_nombre",
            select2: true,
            closeMessages: true,
        });

        validar_Form({
            nameVariable: 'form_seccion',
            contenedor: '#form_seccion',
            rules: {
                sec_estado:
                {
                    required: true,

                }
            },
            messages: {
                sec_estado:
                {
                    required: 'Campo Obligatorio',
                }
            }
        });

        validar_Form({
            nameVariable: 'form_elemento',
            contenedor: '#form_elemento',
            rules: {
                fk_tipo_elemento:
                {
                    required: true,

                },
                elem_estado:
                {
                    required: true,

                }
            },
            messages: {
                fk_tipo_elemento:
                {
                    required: 'Campo Obligatorio',
                },
                elem_estado:
                {
                    required: 'Campo Obligatorio',
                }
            }
        });

    };

    //
    // Return objects assigned to module
    //
    return {
        init: function () {
            _ListarMenus();
            _componentes();
            _metodos();

        },
        init_Sort_Seccion: function (menu_id) {
            _sort_seccion(menu_id);
        },
        init_Sort_Elemento: function (seccion_id) {
            _sort_elemento(seccion_id);
        },
        init_ListarMenus: function () {
            _ListarMenus();
        },
        init_ListarSecciones: function (menu_id) {
            _ListarSecciones(menu_id);
        },
        init_ListarElementos: function (seccion_id) {
            _ListarElementos(seccion_id);
        },
        init_ListarElementosModal: function (fk_seccion_elemento, detalle_elemento_id) {
            _ListarElementosModal(fk_seccion_elemento, detalle_elemento_id);
        }
    }
}();
// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function () {
    PanelContenido.init();
});