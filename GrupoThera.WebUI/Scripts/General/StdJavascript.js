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

function goToByScroll(id) {
    $('html, body').animate({
        scrollTop: $("#" + id).offset().top
    }, 1000);
}

function showLocalicedError(message) {
    $('#errorFocus').html(message);
    createMessageDanger("ERROR: Something goes wrong");
}

