$(document).ready(function () {

    var hash = window.location.hash;
    hash && $('[href="' + hash + '"]').tab('show');

});

var links = $('.navbar ul li a');

$.each(links, function (key, va) {
    if (va.href == document.URL) {
        $(this).addClass('active');
    }
});

$(".moeda").mask('#.##0,00', {
    reverse: true,
    allowNegative: false,
    thousands: '.',
    decimal: ',',
    affixesStay: false,
    clearIfNotMatch: false
});

$(".data").mask('00/00/0000');
$('.siglaCntr').mask('SSSS000000-0');
$('.celular').mask('(00) 00000-0000');
$('.cep').mask('00000-000');
$('.cpf').mask('000.000.000-00');
$('.cnpj').mask('00.000.000/0000-00');

var SPMaskBehavior = function (val) {
    return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
},
    spOptions = {
        onKeyPress: function (val, e, field, options) {
            field.mask(SPMaskBehavior.apply({}, arguments), options);
        }
    };

$('.telefone').mask(SPMaskBehavior, spOptions);

$(".inteiro").on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^\d].+/, ""));
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

var isNumero = function (numero) {
    return !isNaN(numero - parseFloat(numero));
}

var isInteiro = function (numero) {
    return /^\d+$/.test(numero);
}

var isMoeda = function (numero) {

    var valor = numero
        .replace(/\./g, '')
        .replace(',', '.');

    return !isNaN(valor - parseFloat(valor));
}

var formataMoedaCalculo = function (numero) {

    return numero
        .replace(/\./g, '')
        .replace(',', '.');
}

var formataMoedaPtBr = function (numero) {

    var numero = numero.toFixed(2).split('.');
    numero[0] = numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-center",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "800",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

var pesquisar = function () {
    var valor = $("#btnPesquisaSite").getSelectedItemData();
    window.location.href = urlBase + 'Busca/?chave=' + valor.Chave + '&termo=' + '&menu=' + valor.Menu;
    console.log(valor);
};

var options = {
    url: function (phrase) {
        return urlBase + "Home/Buscar/?criterio=" + phrase;
    },
    getValue: "Termo",
    listLocation: "resultados",
    requestDelay: 500,
    template: {
        type: "description",
        fields: {
            description: "Menu"
        }
    },
    list: {
        onClickEvent: function () {
            pesquisar();
        },
        onKeyEnterEvent: function () {
            pesquisar();
        },
        onShowListEvent: function () {

            var termo = $('#btnPesquisaSite').val();

            $('#li-comeca-com').remove();

            var liComecaCom = document.createElement('li');
            liComecaCom.classList.add('eac-item');
            liComecaCom.setAttribute('id', 'li-comeca-com');

            var divComecaCom = document.createElement('div');
            divComecaCom.classList.add('eac-item');

            var lnkComecaCom = document.createElement('a');
            lnkComecaCom.innerHTML = '<br/><b>Começa com: </b>' + termo;
            lnkComecaCom.href = urlBase + 'Busca/?chave=&termo=' + termo;

            divComecaCom.appendChild(lnkComecaCom);
            liComecaCom.appendChild(divComecaCom);

            $('#eac-container-btnPesquisaSite > ul')
                .append(liComecaCom);
        }
    }
};

$("#btnPesquisaSite").easyAutocomplete(options);

//$("input[type=text]").keyup(function () {

//    var start = $(this)[0].selectionStart;
//    var end = $(this)[0].selectionEnd;

//    $(this).val($(this).val().toUpperCase())$(this).selectRange(start, end);
//});

$.fn.selectRange = function (start, end) {
    $(this).each(function () {
        var el = $(this)[0];

        if (el) {
            el.focus();

            if (el.setSelectionRange) {
                el.setSelectionRange(start, end);

            } else if (el.createTextRange) {
                var range = el.createTextRange();
                range.collapse(true);
                range.moveEnd('character', end);
                range.moveStart('character', start);
                range.select();

            } else if (el.selectionStart) {
                el.selectionStart = start;
                el.selectionEnd = end;
            }
        }
    });
};

$('.btn').on('click', function () {

    var $this = $(this);
    var dataAnimation = $(this).data('animation-name');

    if (dataAnimation) {
        var loadingText = '<i class="fa fa-spinner fa-spin"></i> ' + $(this).html();
        if ($(this).html() !== loadingText) {
            $this.data('original-text', $(this).html());
            $this.html(loadingText);
        }
        setTimeout(function () {
            $this.html($this.data('original-text'));
        }, 2000);
    }
});

$('select').keyup(function (e) {

    if (e.keyCode === 46) {
        $(this).empty();
    }
});

$(document).on('keyup keypress', 'form input', function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        return false;
    }
});

var Strings = {};
Strings.orEmpty = function (entity) {
    return entity || "";
};

var mensagemCadastroSucesso = function (data) {

    $('#msgErro')
        .addClass('invisivel');

    if (data) {
        if (data.RedirectUrl) {
            location.href = data.RedirectUrl;
        }
    }

    toastr.success('Registro salvo com sucesso!', 'Portal');
};

function exibirErrosResponse(data, status) {

    var retorno = data.responseJSON;

    if (retorno.erros != null) {

        $('#msgErro')
            .html('');

        if (retorno.erros.length === 1) {

            var erro = retorno.erros[0];

            $('#msgErro')
                .removeClass('invisivel')
                .html(erro.ErrorMessage);
        }

        if (retorno.erros.length > 1) {

            retorno.erros.map(function (erro) {
                $('#msgErro').append('<li>' + erro.ErrorMessage + '</li>');
            });
        }

        $('#msgErro')
            .removeClass('invisivel');

        var btnSubmit = $("button[type=submit]");

        if (btnSubmit) {

            btnSubmit
                .html('<i class="fas fa-save"></i>  Salvar')
                .pro('disabled');
        }

        $(window).scrollTop(0);
    }    
}

function Spinner(btn) {

    if (btn !== null) {
        
        $('#' + btn.id)
            .html('<i class="fa fa-spinner fa-spin"></i> aguarde...')
            .addClass('disabled');
    }
}

$('#btnPesquisaSite').click(function () {
    $(this).animate({
        width: '380px'
    });
})
