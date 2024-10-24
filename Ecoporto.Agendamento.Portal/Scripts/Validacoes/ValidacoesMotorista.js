// Arquivo: validacoesMotorista.js
$(document).ready(function () {
    // Validação do nome quando o usuário sai do campo
    $('#Nome').blur(function () {
        var nome = $(this).val();
        var nomeErrorElement = $('#nomeError');

        // Verifica se o campo está vazio ou tem menos de 3 caracteres
        if (nome === "") {
            // Exibe a mensagem de erro se o campo estiver vazio
            if (nomeErrorElement.length === 0) {
                // Se o elemento de erro ainda não existir, cria um
                $(this).after('<span id="nomeError" style="color:red;">O nome não pode estar vazio.</span>');
            } else {
                // Atualiza a mensagem de erro
                nomeErrorElement.text('O nome nao pode estar vazio.');
            }
        } else if (nome.length < 3) {
            // Exibe a mensagem de erro se o nome tiver menos de 3 caracteres
            if (nomeErrorElement.length === 0) {
                $(this).after('<span id="nomeError" style="color:red;">O primeiro nome do motorista não pode ter menos que três caracteres.</span>');
            } else {
                nomeErrorElement.text('O primeiro nome do motorista não pode ter menos que três caracteres.');
            }
        } else {
            // Remove a mensagem de erro se o nome for válido
            nomeErrorElement.remove();
        }
    });

    $('#DT_Nascimento').blur(function () {
        validarDtNascimento($(this));
    });
});

// Função de validação para o campo DT_Nascimento
function validarDtNascimento(campo) {
    var dtNascimento = campo.val();
    var dtNascimentoErrorElement = $('#dtNascimentoError');
    var hoje = new Date();

    // Converter a data de dd/MM/yyyy para yyyy-MM-dd
    var dataPartes = dtNascimento.split('/');
    var nascimentoFormatado = dataPartes[2] + '-' + dataPartes[1] + '-' + dataPartes[0]; // yyyy-MM-dd

    var nascimento = new Date(nascimentoFormatado);

    // Verificar se a data é válida (se o JavaScript conseguiu criar uma data válida)
    if (nascimento == "Invalid Date" || isNaN(nascimento.getTime())) {
        dtNascimentoErrorElement.html('Data de nascimento inválida.');
        return;
    }

    // Verificar se a data é futura
    if (nascimento > hoje) {
        dtNascimentoErrorElement.html('Data de nascimento não pode ser no futuro.');
        return;
    }

    // Calcular a idade com base na data de nascimento
    var idade = hoje.getFullYear() - nascimento.getFullYear();
    var m = hoje.getMonth() - nascimento.getMonth();
    if (m < 0 || (m === 0 && hoje.getDate() < nascimento.getDate())) {
        idade--;
    }

    // Verificar se a idade é menor que 18 anos
    if (idade < 18) {
        dtNascimentoErrorElement.html('Motorista com menos de 18 anos.');
    }
    // Verificar se a idade é maior que 100 anos
    else if (idade > 100) {
        dtNascimentoErrorElement.html('Data de nascimento inválida.');
    }
    // Se a idade e a data forem válidas, remover o erro
    else {
        dtNascimentoErrorElement.html('');  // Limpa a mensagem de erro
    }
}

// Função para validar o campo Data_Emissao
function validarDataEmissao(campo) {
    var dataEmissao = campo.val();
    var dataEmissaoErrorElement = $('#dataEmissaoError'); // Elemento onde o erro será exibido
    var hoje = new Date();

    // Verificar se a data de emissão está vazia
    if (dataEmissao === "" || dataEmissao === null) {
        dataEmissaoErrorElement.html('Data Emissão deve ser preenchida!');
        return false;  // A data está inválida
    }

    // Converter a data de dd/MM/yyyy para um formato de data válido no JavaScript (yyyy-MM-dd)
    var dataPartes = dataEmissao.split('/');
    var dataEmissaoFormatada = new Date(dataPartes[2], dataPartes[1] - 1, dataPartes[0]); // yyyy-MM-dd

    // Verificar se a data de emissão é maior que a data de hoje
    if (dataEmissaoFormatada > hoje) {
        dataEmissaoErrorElement.html('Data inválida, emissão não pode ser maior que hoje!');
        return false;  // A data está inválida
    }

    // Se tudo estiver certo, limpar a mensagem de erro
    dataEmissaoErrorElement.html('');
    return true;  // A data está válida
}

// Função para validar o campo ValidadeCNH
function validarValidadeCNH(campo) {
    var validadeCNH = campo.val();
    var validadeCNHErrorElement = $('#validadeCNHError'); // Elemento onde o erro será exibido
    var hoje = new Date();

    // Verificar se a data de ValidadeCNH está vazia
    if (validadeCNH === "" || validadeCNH === null) {
        validadeCNHErrorElement.html('A Validade da CNH deve ser preenchida!');
        return false;  // A data está inválida
    }

    // Converter a data de dd/MM/yyyy para um formato de data válido no JavaScript (yyyy-MM-dd)
    var dataPartes = validadeCNH.split('/');
    var validadeCNHFormatada = new Date(dataPartes[2], dataPartes[1] - 1, dataPartes[0]); // yyyy-MM-dd

    // Calcular a data de 10 anos a partir de hoje
    var maxValidade = new Date(hoje.getFullYear() + 10, hoje.getMonth(), hoje.getDate());

    // Verificar se a validade da CNH é maior que 10 anos a partir da data atual
    if (validadeCNHFormatada > maxValidade) {
        validadeCNHErrorElement.html('A Validade da CNH devera ser no maximo 10 anos!');
        return false;  // A data está inválida
    }

    // Se tudo estiver certo, limpar a mensagem de erro
    validadeCNHErrorElement.html('');
    return true;  // A data está válida
}




