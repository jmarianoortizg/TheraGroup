function createMessageSucess(message) {
    $.notify({ type: 'success', icon: "fa fa-check", message: message });
}

function createMessageDanger(message) {
    $.notify({ type: 'danger', icon: "fa fa-ban", message: message });
}

function createMessageWarning(message) {
    $.notify({ type: 'warning', icon: "fa fa-warning", message: message });
}

function createMessageInfo(message) {
    $.notify({ type: "info", icon: "fa fa-info-circle", message: message });
}
