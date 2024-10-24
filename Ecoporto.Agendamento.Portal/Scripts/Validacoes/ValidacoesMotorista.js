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




