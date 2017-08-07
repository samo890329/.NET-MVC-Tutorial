//AJAX Call function
function ajaxRequest(message, controller, container, type, dataType, data, successCallBack, failCallback, isInitialPartial) {
    $('#LoadingModal').html(message);
    $('#LoadingModal').modal('show');
    var layoutLogOut = '<div class="panel-body"> <div class="row"> <div class="col-md-12 text-center"> <i class="fa fa-exclamation-triangle fa-5x" aria-hidden="true" style="color: #D7C12C"></i> </div> </div> <div class="row"> <div class="col-md-12 text-center"> <h2><strong>Su sesi&oacute;n ha caducado.</strong></h2> <h5>Espere un momento por favor...</h5> <br/> <img src="' + ROOT + 'images/loading.gif" class="img-responsive" style="width: 10%; height: auto; display: inline;"/> </div> </div></div>';
    if (data === null) {
        $.ajax({
            async: true,
            type: type,
            url: controller,
            dataType: dataType,
            success: function (data) {
                $('#LoadingModal').modal('hide');
                if (dataType === "json" && isInitialPartial) {
                    $(container).html(data.HtmlResponsePartialRawView);
                    if (data.Success) { // Mensaje exito
                        if (successCallBack !== null) {
                            successCallBack(data);
                        }
                    } else { // Mensaje error
                        if (failCallback !== null) {
                            failCallback(data);
                        }
                    }
                } else if (isInitialPartial) { //Crea la partial view
                    $(container).html(data);
                    if (successCallBack !== null) {
                        successCallBack(data);
                    }
                    if (failCallback !== null) {
                        failCallback(data);
                    }
                } else {
                    if (data.Success) {
                        if (successCallBack !== null) {
                            successCallBack(data);
                        }
                    } else {
                        if (failCallback !== null) {
                            failCallback(data);
                        }
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var JsonResponse = JSON.parse(jqXHR.responseText);
                $('#LoadingModal').modal('hide');
                if (jqXHR.status === 403) {
                    showModal(" ", layoutLogOut, null);
                    $(".modal-header").remove();
                    setTimeout(function () {
                        window.location.href = JsonResponse.RedirectUrl;
                    }, 3000);
                }
                else {
                    showModal("Rutas de Supervisión", "Favor de contactar al &aacute;rea de sistemas", 2);
                }
            }
        });
    } else {
        $.ajax({
            async: true,
            type: type,
            url: controller,
            dataType: dataType,
            data: data,
            success: function (data) {
                $('#LoadingModal').modal('hide');
                if (isInitialPartial) {
                    $(container).html(data.HtmlResponsePartialRawView);
                }
                if (data.response === undefined) {
                    $(container).html(data);
                    if (successCallBack !== null) {
                        successCallBack(data);
                    }
                } else if (data.response) {
                    if (successCallBack !== null) {
                        successCallBack(data);
                    }
                } else {
                    if (failCallback !== null) {
                        failCallback(data);
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var JsonResponse;
                $('#LoadingModal').modal('hide');
                if (jqXHR.status !== 403) {
                    showModal("Rutas de supervisi&oacute;n", "Favor de contactar al &aacute;rea de sistemas", 2);
                } else {
                    JsonResponse = JSON.parse(jqXHR.responseText);
                    if (jqXHR.status === 403) {
                        showModal(" ", layoutLogOut, null);
                        $(".modal-header").remove();
                        setTimeout(function () {
                            window.location.href = JsonResponse.RedirectUrl;
                        }, 3000);
                    }
                    else {
                        showModal("Rutas de supervisi&oacute;n", "Favor de contactar al &aacute;rea de sistemas", 2);
                    }
                }
            }
        });
    }
    return false
}



//Función que muestra el mensaje de en espera
var GetLoading = function (text) {
    if (text === undefined) {
        text = 'Espere un momento...';
    }

    var result = '<div class="progress progress-striped active loading progress-wait">';
    result += '<div class="progress-bar" role="progressbar" style="width: 100%;">';
    result += text;
    result += '</div></div>';

    return result;
};



function showModal(title, message, type) {
    // var contModal=$("#contModal").text('0');
    var $modal = $("#modal-");

    $modal.find(".modal-title").text(title);
    if (message !== null) {
        $modal.find(".modal-body").html(message);
    }

    switch (type) {
        case 1:
            $modal.find(".modal-footer").html('<button type="button" class="btn btn-success bt-modal-btn" data-dismiss="modal">Aceptar</button>');
            break;
        case 2:
            $modal.find(".modal-footer").html('<button type="button" class="btn btn-danger bt-modal-btn" data-dismiss="modal">Aceptar</button>');
            break;
        case 3:
            $modal.find(".modal-footer").html('<button id="btnAceptar" type="button" class="btn btn-success bt-modal-btn">Aceptar</button>' +
												'<button id="btnCancelar" type="button" class="btn btn-danger bt-modal-btn">Cancelar</button>');
            break;
        case 4:
            $modal.find(".modal-footer").html('<button id="btnSi" type="button" class="btn btn-success bt-modal-btn">Si</button>' +
												'<button id="btnNo" type="button" class="btn btn-danger bt-modal-btn">No</button>');
            break;
        case 5:
            $modal.find(".modal-footer").html('<button id="btnRegistrar" type="button" class="btn btn-success bt-modal-btn">Agregar</button>' +
												'<button id="btnCancelar" type="button" class="btn btn-danger bt-modal-btn">Cancelar</button>');
            break;
        case 6:
            $modal.find(".modal-footer").html('<button id="btnEliminar" type="button" class="btn btn-success bt-modal-btn">Eliminar</button>' +
												'<button id="btnCancelar" type="button" class="btn btn-danger bt-modal-btn">Cancelar</button>');
            break;
        case 7:
            $modal.find(".modal-footer").html('<button type="button" class="btn btn-danger bt-modal-btn" data-dismiss="modal">Cancelar</button>');
            break;
        case 8:
            $modal.find(".modal-footer").html('<button type="button" class="btn btn-danger bt-modal-btn" data-dismiss="modal">Cerrar</button>');
            break;
        default:
            $modal.find(".modal-footer").html('');
            break;
    }
    $modal.modal('show');
}

