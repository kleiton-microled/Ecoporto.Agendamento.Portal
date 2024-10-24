// Importar o jQuery
(function () {
    var script = document.createElement('script');
    script.src = 'https://code.jquery.com/jquery-3.5.1.min.js';
    document.head.appendChild(script);
})();

// Importar o Bootstrap Datepicker
(function () {
    var script = document.createElement('script');
    script.src = 'https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js';
    script.onload = function () {
        // Somente depois que o Datepicker for carregado, carregue o idioma pt-BR
        var scriptLocale = document.createElement('script');
        scriptLocale.src = 'https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/locales/bootstrap-datepicker.pt-BR.min.js';
        document.head.appendChild(scriptLocale);

        // Inicialize o Datepicker depois que o idioma for carregado
        scriptLocale.onload = function () {
            $(document).ready(function () {
                $('#DT_Nascimento').datepicker({
                    format: 'dd/mm/yyyy',
                    endDate: new Date(),
                    autoclose: true,
                    todayHighlight: true,
                    language: 'pt-BR'  // Agora o idioma pt-BR deve funcionar
                });
            });
        };
    };
    document.head.appendChild(script);
})();
