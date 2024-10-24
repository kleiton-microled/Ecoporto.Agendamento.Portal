$(document).ready(function () {

    $('main')
        .addClass('container')
        .removeClass('container-fluid');

    if ($('#Id').val() != null) {

        $('#chkConcordouRegrasLTL, #btnConfirmarRegrasLTL').addClass('invisivel');    
    }

    $('#CidadeId').select2();
});

function AbrirRegrasLTL() {

    $('#modal-regrasLTL')
        .modal('show');
}

function HabilitarConfirmacaoRegrasLTL() {

    if ($('#chkConcordouRegrasLTL').is(':checked')) {

        $('#frmLTL :input').prop('disabled', false);

        $('#btnConfirmarRegrasLTL').removeClass('disabled');

    } else {

        $('#frmLTL :input').prop('disabled', true);

        $('#btnConfirmarRegrasLTL').addClass('disabled');
    }
}

$('#EmailFaturamento, #EmailFollowUp, #EmailContato').on('keydown', function (e) {
    if (e.keyCode == 32) return false;
});

function PesquisarEnderecoPorCnpj() {

    event.preventDefault();

    var cnpj = $('#AverbacaoDadosLTLViewModel_DadosLTLCNPJ').val();

    if (cnpj === '') {
        toastr.error('Informe o CNPJ corretamente', 'Portal');
        return;
    }

    $('#btnPesquisarEndereco')
        .html('<i class="fa fa-spinner fa-spin"></i>')
        .addClass('disabled');

    $.get(urlBase + 'Averbacao/ObterLocaisEntregaPorCnpj' + '?cnpj=' + cnpj, function (resultado) {

        $('#AverbacaoDadosLTLViewModel_LocaisEntrega').empty();
        $('#AverbacaoDadosLTLViewModel_DadosLTLRazaoSocial').val('');
        $('#AverbacaoDadosLTLViewModel_DadosLTLInscricaoEstadual').val('');

        if (resultado) {
            $('#AverbacaoDadosLTLViewModel_DadosLTLRazaoSocial').val(resultado.RazaoSocial);
            $('#AverbacaoDadosLTLViewModel_DadosLTLInscricaoEstadual').val(resultado.InscricaoEstadual);

            resultado.forEach(function (item) {

                var texto = item.Endereco + ', ' + item.Numero + ' - ' + item.Bairro + ' - ' + item.CidadeDescricao + '/' + item.UF

                $('#AverbacaoDadosLTLViewModel_LocaisEntrega')
                    .append($('<option>', {
                        value: item.Id, text: texto
                    }));

                $('#AverbacaoDadosLTLViewModel_DadosLTLRazaoSocial').val(item.RazaoSocial);
                $('#AverbacaoDadosLTLViewModel_DadosLTLInscricaoEstadual').val(item.InscricaoEstadual);
            });
        }
    }).fail(function (data) {
        toastr.error(data.statusText, 'LTL');
    }).always(function () {
        $('#btnPesquisarEndereco')
            .html('<i class="fas fa-search"></i>')
            .removeClass('disabled');
    });
}



function LimparEndereco() {

    $('#AverbacaoDadosLTLViewModel_DadosLTLEndereco').val('').prop('readonly', false);

    $('#AverbacaoDadosLTLViewModel_DadosLTLBairro').val('').prop('readonly', false);

    $('#AverbacaoDadosLTLViewModel_DadosLTLUF option').each(function () {
        $(this).removeAttr('selected');
    });

}

function ConsultarCEP() {

    //event.preventDefault();

    var cep = $('#AverbacaoDadosLTLViewModel_DadosLTLCEP').val().replace(/\D/g, '');
    

    if (cep != "") {

        var validacep = /^[0-9]{8}$/;

        if (validacep.test(cep)) {

            $('#btnConsultarCEP')
                .html('<i class="fa fa-spinner fa-spin"></i>')
                .addClass('disabled');

            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                if (!("erro" in dados)) {

                    LimparEndereco();

                    $('#AverbacaoDadosLTLViewModel_DadosLTLEndereco').val(dados.logradouro);

                    if (dados.logradouro !== '') {
                        $('#AverbacaoDadosLTLViewModel_DadosLTLEndereco').prop('readonly', true);
                    }

                    $('#AverbacaoDadosLTLViewModel_DadosLTLBairro').val(dados.bairro);

                    if (dados.bairro !== '') {
                        $('#AverbacaoDadosLTLViewModel_DadosLTLBairro').prop('readonly', true);
                    }

                    $('#AverbacaoDadosLTLViewModel_DadosLTLCidadeId').val(dados.ibge).addClass('select-readonly');

                    $('#AverbacaoDadosLTLViewModel_DadosLTLUF option').each(function () {
                        alert(dados.uf);
                        if ($(this).html() == dados.uf) {
                            $(this).attr('selected', 'selected');
                            return;
                        }
                    });

                    $("#AverbacaoDadosLTLViewModel_DadosLTLUF").addClass('select-readonly');
                }
                else {

                    LimparEndereco();
                    toastr.error('CEP não encontrado', 'Portal');
                }

                $('#btnConsultarCEP')
                    .html('<i class="fas fa-search"></i>')
                    .removeClass('disabled');
            });
        }
        else {

            LimparEndereco();
            toastr.error('Formato de CEP inválido', 'Portal');
        }
    }
    else {

        LimparEndereco();
    }
}

$("input[name='AverbacaoDadosLTLViewModel.DadosLTLAjudante']").click(function () {

    if ($("#rbAjudanteSim").is(':checked')) {
        $('#AverbacaoDadosLTLViewModel_DadosLTLQuantidadeAjudantes').prop('readonly', false);
    } else {
        
        $('#AverbacaoDadosLTLViewModel_DadosLTLQuantidadeAjudantes')
            .val('')
            .prop('readonly', true);        
    }
});

function obterMascaraDocumento() {

    var documentoId = $('#TipoDocumentoId').val();
    
    if (isNumero(documentoId)) {
        var url = urlBase + 'Averbacao/ObterMascaraDocumento/' + documentoId;
        
        $.get(url, function (resultado) {

            if (resultado) {

                var mascara = resultado.replace(/#/g, 'A'); 
                
                $('#NumeroDocumentoAverbacao').val('');
                $('#NumeroDocumentoAverbacao').mask(mascara, { clearIfNotMatch: true });
                $('#TipoDocumentoId').removeClass('pulse');
            }
        }).fail(function (data) {
            toastr.error(data.statusText, 'LTL');
        });
    }
}


var mensagemSucesso = function (data) {

    toastr.success('Para salvar a solicitação favor efetuar cadastro da nota fiscal!', 'Portal');

    setTimeout(function () {

        if (data.redirectUrl) {
            if (data.hash) {
                document.location.href = decodeURIComponent(data.redirectUrl) + "#" + data.hash;
            }
        }

    }, 2000);
}

function mensagemErro(data) {

    if (data.responseJSON != null) {

        var retorno = data.responseJSON;

        if (retorno.erros != null) {

            toastr.error('Não foi possível cadastrar/atualizar. Verifique mensagens.', 'Portal');

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

            $(window).scrollTop(0);
        }
    } else {
        toastr.error(data.statusText, 'Portal');
    }
}