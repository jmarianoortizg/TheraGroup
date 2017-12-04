function createMessageSuccess(message) {    
    $.notify({ icon: "fa fa-check", message: message }, { type: 'success' });
}

function createMessageDanger(message) {
    $.notify({ icon: "fa fa-ban", message: message }, { type: 'danger' });    
}

function createMessageWarning(message) {
    $.notify({ icon: "fa fa-warning", message: message }, { type: 'warning' });    
}

function createMessageInfo(message) {
    $.notify({ icon: "fa fa-info-circle", message: message }, { type: 'info' });    
}
