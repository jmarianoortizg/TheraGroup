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

function inicialiceSpinner() {
    $("#spin").spinner({
        color: "black"
                   , background: "rgba(189, 227, 231, .5)"
                   , html: "<i class='fa fa-repeat' style='color: gray;'> </i>"
                   , spin: true
    });
}

function spinShow() {
    $("#spin").show();
}

function spinHide() {
    $("#spin").hide();
}

