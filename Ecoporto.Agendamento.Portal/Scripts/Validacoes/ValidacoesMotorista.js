// Arquivo: validacoesMotorista.js
$(document).ready(function () {
    // Valida��o do nome quando o usu�rio sai do campo
    $('#Nome').blur(function () {
        var nome = $(this).val();
        var nomeErrorElement = $('#nomeError');

        // Verifica se o campo est� vazio ou tem menos de 3 caracteres
        if (nome === "") {
            // Exibe a mensagem de erro se o campo estiver vazio
            if (nomeErrorElement.length === 0) {
                // Se o elemento de erro ainda n�o existir, cria um
                $(this).after('<span id="nomeError" style="color:red;">O nome n�o pode estar vazio.</span>');
            } else {
                // Atualiza a mensagem de erro
                nomeErrorElement.text('O nome nao pode estar vazio.');
            }
        } else if (nome.length < 3) {
            // Exibe a mensagem de erro se o nome tiver menos de 3 caracteres
            if (nomeErrorElement.length === 0) {
                $(this).after('<span id="nomeError" style="color:red;">O primeiro nome do motorista n�o pode ter menos que tr�s caracteres.</span>');
            } else {
                nomeErrorElement.text('O primeiro nome do motorista n�o pode ter menos que tr�s caracteres.');
            }
        } else {
            // Remove a mensagem de erro se o nome for v�lido
            nomeErrorElement.remove();
        }
    });

    $('#DT_Nascimento').blur(function () {
        validarDtNascimento($(this));
    });

});

// Fun��o de valida��o para o campo DT_Nascimento
function validarDtNascimento(campo) {
    var dtNascimento = campo.val();
    var dtNascimentoErrorElement = $('#dtNascimentoError');
    var hoje = new Date();

    // Converter a data de dd/MM/yyyy para yyyy-MM-dd
    var dataPartes = dtNascimento.split('/');
    var nascimentoFormatado = dataPartes[2] + '-' + dataPartes[1] + '-' + dataPartes[0]; // yyyy-MM-dd

    var nascimento = new Date(nascimentoFormatado);

    // Verificar se a data � v�lida (se o JavaScript conseguiu criar uma data v�lida)
    if (nascimento == "Invalid Date" || isNaN(nascimento.getTime())) {
        dtNascimentoErrorElement.html('Data de nascimento inv�lida.');
        return;
    }

    // Verificar se a data � futura
    if (nascimento > hoje) {
        dtNascimentoErrorElement.html('Data de nascimento n�o pode ser no futuro.');
        return;
    }

    // Calcular a idade com base na data de nascimento
    var idade = hoje.getFullYear() - nascimento.getFullYear();
    var m = hoje.getMonth() - nascimento.getMonth();
    if (m < 0 || (m === 0 && hoje.getDate() < nascimento.getDate())) {
        idade--;
    }

    // Verificar se a idade � menor que 18 anos
    if (idade < 18) {
        dtNascimentoErrorElement.html('Motorista com menos de 18 anos.');
    }
    // Verificar se a idade � maior que 100 anos
    else if (idade > 100) {
        dtNascimentoErrorElement.html('Data de nascimento inv�lida.');
    }
    // Se a idade e a data forem v�lidas, remover o erro
    else {
        dtNascimentoErrorElement.html('');  // Limpa a mensagem de erro
    }
}

// Fun��o para validar o campo Data_Emissao
function validarDataEmissao(campo) {
    var dataEmissao = campo.val();
    var dataEmissaoErrorElement = $('#dataEmissaoError'); // Elemento onde o erro ser� exibido
    var hoje = new Date();

    // Verificar se a data de emiss�o est� vazia
    if (dataEmissao === "" || dataEmissao === null) {
        dataEmissaoErrorElement.html('Data Emiss�o deve ser preenchida!');
        return false;  // A data est� inv�lida
    }

    // Converter a data de dd/MM/yyyy para um formato de data v�lido no JavaScript (yyyy-MM-dd)
    var dataPartes = dataEmissao.split('/');
    var dataEmissaoFormatada = new Date(dataPartes[2], dataPartes[1] - 1, dataPartes[0]); // yyyy-MM-dd

    // Verificar se a data de emiss�o � maior que a data de hoje
    if (dataEmissaoFormatada > hoje) {
        dataEmissaoErrorElement.html('Data inv�lida, emiss�o n�o pode ser maior que hoje!');
        return false;  // A data est� inv�lida
    }

    // Se tudo estiver certo, limpar a mensagem de erro
    dataEmissaoErrorElement.html('');
    return true;  // A data est� v�lida
}

// Fun��o para validar o campo ValidadeCNH
function validarValidadeCNH(campo) {
    var validadeCNH = campo.val();
    var validadeCNHErrorElement = $('#validadeCNHError'); // Elemento onde o erro ser� exibido
    var hoje = new Date();

    // Verificar se a data de ValidadeCNH est� vazia
    if (validadeCNH === "" || validadeCNH === null) {
        validadeCNHErrorElement.html('A Validade da CNH deve ser preenchida!');
        return false;  // A data est� inv�lida
    }

    // Converter a data de dd/MM/yyyy para um formato de data v�lido no JavaScript (yyyy-MM-dd)
    var dataPartes = validadeCNH.split('/');
    var validadeCNHFormatada = new Date(dataPartes[2], dataPartes[1] - 1, dataPartes[0]); // yyyy-MM-dd

    // Calcular a data de 10 anos a partir de hoje
    var maxValidade = new Date(hoje.getFullYear() + 10, hoje.getMonth(), hoje.getDate());

    // Verificar se a validade da CNH � maior que 10 anos a partir da data atual
    if (validadeCNHFormatada > maxValidade) {
        validadeCNHErrorElement.html('A Validade da CNH devera ser no maximo 10 anos!');
        return false;  // A data est� inv�lida
    }

    // Se tudo estiver certo, limpar a mensagem de erro
    validadeCNHErrorElement.html('');
    return true;  // A data est� v�lida
}

function validarCNH() {
    // Verificar se o campo CNH est� preenchido
    var cnhValue = $('#CNH').val();
    if (!cnhValue || cnhValue.trim() === "") {
        // Exibir mensagem de erro se o campo estiver vazio
        $('#cnhError').html('Por favor, insira um valor valido para a CNH.').show();
        return;
    }

    // Limpar a mensagem de erro se o campo estiver preenchido
    $('#cnhError').html('').hide();

    var campo = $('#CNH');
    var valor = campo.val();
    var cnhErrorElement = $('#cnhError');

    if (!valor || valor.trim() === '') {
        // Se o campo estiver vazio, exibe a mensagem de erro
        if (cnhErrorElement.length === 0) {
            campo.after('<span id="cnhError" style="color:red;">CNH n�o pode estar vazia.</span>');
        } else {
            cnhErrorElement.text('CNH nao pode estar vazia.');
        }
        return false;
    } else {
        // Se o campo estiver preenchido, remove a mensagem de erro
        cnhErrorElement.text('');
        return true;
    }
}

function limparFormulario() {
    $('#frmCadastroAgendamento')[0].reset(); // Limpa todos os campos do formul�rio
    $('.text-danger').text(''); // Limpa todas as mensagens de erro exibidas
    $('#pnlMotoristaChronos').addClass('invisivel'); // Caso tenha elementos de alerta
}

// Fun��o para bloquear todos os campos
// Fun��o principal de pesquisa da CNH
function PesquisarCNH(target) {
    event.preventDefault();

    var $target = $('#' + target.id);
    var cnh = $('#CNH').val().trim();  // Remover espa�os em branco

    if (!cnh) {
        toastr.error('Por favor, informe a CNH.', 'Erro');
        return;
    }

    // Exibe o estado de carregamento
    mostrarCarregando($target, true);

    // Pegar a URL do atributo data-url-pesquisar no bot�o
    var urlPesquisarCNH = $(target).data('url-pesquisar');

    $.get(urlPesquisarCNH, { cnh: cnh })
        .done(function (resultado) {
            if (resultado) {
                preencherCamposMotorista(resultado);
                $('#msgErro').html('').addClass('invisivel');
                $('#pnlMotoristaChronos').removeClass('invisivel');
            } else {
                toastr.warning('Motorista n�o encontrado.', 'Aten��o');
                limparCamposMotorista();
                $('#pnlMotoristaChronos').addClass('invisivel');
            }
            // Habilitar os campos ap�s a pesquisa
            habilitarCampos(true);
        })
        .fail(function (data) {
            toastr.error(data.statusText || 'Erro ao buscar dados do motorista.', 'Erro');
        })
        .always(function () {
            mostrarCarregando($target, false);
        });
}



// Fun��o para exibir o estado de carregamento
function mostrarCarregando($element, isLoading) {
    if (isLoading) {
        $element.html('<i class="fa fa-spinner fa-spin"></i> aguarde...').addClass('disabled');
    } else {
        $element.html('<i class="fas fa-search"></i> Pesquisar').removeClass('disabled');
    }
}

// Fun��o para preencher os campos com os dados do motorista
function preencherCamposMotorista(motorista) {
    $('#Nome').val(motorista.Nome).prop('readonly', true);
    $('#RG').val(motorista.RG).prop('readonly', false);
    $('#CPF').val(motorista.CPF).prop('readonly', true);
    $('#DT_Nascimento').val(motorista.DT_Nascimento).prop('readonly', false);
    $('#Orgao_Emissor').val(motorista.Orgao_Emissor).prop('readonly', false);
    $('#Data_Emissao').val(motorista.Data_Emissao).prop('readonly', false);
    $('#ValidadeCNH').val(motorista.ValidadeCNH);
    $('#Celular').val(motorista.Celular);
    $('#UltimaAlteracao').val(motorista.UltimaAlteracao).prop('readonly', true);
}

// Fun��o para limpar os campos
function limparCamposMotorista() {
    $('#Nome, #RG, #CPF, #DT_Nascimento, #Orgao_Emissor, #Data_Emissao, #ValidadeCNH, #Celular, #UltimaAlteracao').val('').prop('readonly', false);
    $('#UltimaAlteracao').prop('readonly', true);  // Este sempre deve ser readonly
}

// Fun��o para habilitar ou desabilitar campos
function habilitarCampos(enable) {
    var isReadonly = !enable;
    $('#ValidadeCNH, #Celular, #Nextel, #MOP').prop('readonly', isReadonly);
}

// Desabilitar os campos inicialmente ao carregar o formul�rio
$(document).ready(function () {
    habilitarCampos(false);  // Desabilitar os campos no in�cio

    // Ativar/desativar o bot�o de pesquisa baseado na presen�a da CNH
    $('#CNH').on('input', function () {
        var cnh = $(this).val().trim();
        $('#btnPesquisarCNH').prop('disabled', !cnh);  // Desabilita se o campo estiver vazio
    });
});










