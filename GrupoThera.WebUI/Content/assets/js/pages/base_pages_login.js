/*
 *  Document   : base_pages_login.js
 *  Description: Custom JS code used in Login Page
 */

$(document).ready(function () {


    $("#spin").spinner({
        color: "black"
        , background: "rgba(189, 227, 231, .5)"
        , html: "<i class='fa fa-repeat' style='color: gray;'> </i>"
        , spin: true
    });

    $("#empresaSource").live('click', function() { 
        alert('button clicked'); 
    });

});

function BeginFormLogin() {
    $("#frmLogin").validate({
        errorClass: 'help-block text-right animated fadeInDown',
        errorElement: 'div',
        errorPlacement: function (error, e) {
            jQuery(e).parents('.form-group > div').append(error);
        },
        highlight: function (e) {
            jQuery(e).closest('.form-group').removeClass('has-error').addClass('has-error');
            jQuery(e).closest('.help-block').remove();
        },
        success: function (e) {
            jQuery(e).closest('.form-group').removeClass('has-error');
            jQuery(e).closest('.help-block').remove();
        },
        messages: {
            'UserName': {
                required: 'Ingrese nombre de usuario',
                minlength: 'El nombre de ususario debe de tener al menos 3 caracteres'
            },
            'Password': {
                required: 'Ingrese el Password',
                minlength: 'El Password debe de tener al menos una longitud de 5 caracteres'
            }
        },
        rules: {
            UserName: {
                required: true
            },
            Password: {
                required: true,
                minlength: 5
            }
        }
    });

    var band = $('#frmLogin').validate().form();

    if (band) { $("#spin").show(); }

    return band;
}

function SuccessFormLogin(response) {
    $('#formLoginSuccess').removeClass('animated fadeOutDown element-hidden').addClass('animated fadeOutDown element-visible')
    $('#formLoginError').removeClass('animated fadeOutDown element-hidden').addClass('animated fadeOutDown element-visible')

    $("#spin").hide();
    if (response.success) {
        $('#formLoginSuccess').removeClass('animated fadeOutDown element-hidden').addClass('animated fadeInDown element-visible')
        setTimeout(function () {
            window.location.href = '/Home/Dashboard/';
        }, 3000);
    }
    else
        $('#formLoginError').removeClass('animated fadeOutDown element-hidden').addClass('animated fadeInDown element-visible')
}

function FailureFormLogin(response) {
    $("#spin").hide();
    alert("Please contact with technical support");
}