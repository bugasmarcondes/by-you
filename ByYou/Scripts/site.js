var cadastro = function (el) {
    var resultDiv = $(el),
        verificaCpf = function () {
            var form = $('#verificaCpf'),
                action = form.attr('action');

            $('#verificaCpf').on('submit', function (e) {
                e.preventDefault();

                $.post(action, { cpf: $('#cpf').val() }, function (data) {
                    if (data) {
                        $('#resultado').html(data);
                        $('#cpf').inputmask('999.999.999-99');
                    } else {
                        $('#resultado').html('<p>Entre em contato pelo: e-mail@estacio.com.br</p>');
                    }
                });

                return false;
            });
        },
        init = function () {
            $('#cpf').inputmask('999.999.999-99');

            verificaCpf();
        };

    return {
        init: init
    };
}('resultado');

cadastro.init();