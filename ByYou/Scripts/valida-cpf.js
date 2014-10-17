var cadastro = function (el) {
    var resultDiv = $(el),
        verificaCpf = function () {
            var form = $('#verificaCpf'),
                action = form.attr('action');

            $(document).on('submit', '#verificaCpf', function (e) {
                e.preventDefault();

                if ($('#Cpf').val() == '') {
                    return false;
                }

                $.post(action, { usuario: {
                    Cpf: $('#Cpf').val()
                } }, function (data) {
                    if (data) {
                        $('#resultado').html(data);
                        $('#Cpf').inputmask('999.999.999-99');
                    } else {
                        $('#resultado').html(data);
                    }
                });

                return false;
            });
        },
        init = function () {
            $('#Cpf').inputmask('999.999.999-99');

            verificaCpf();
        };

    return {
        init: init
    };
}('resultado');

cadastro.init();