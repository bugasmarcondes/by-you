var cadastro = function () {

    var _cpf = '',
        $resultado = $('#resultado'),
        $cpf = $('#Cpf');

    //VERIFICA CPF NO ARQUIVO CSV: /Content/csv/lista.csv
    var verifica_csv = function () {
            $(document).on('submit', '#verificaCpf', function (e) {
                e.preventDefault();

                if ($cpf.val() == '') {
                    return false;
                }

                var $form = $('#verificaCpf');

                try {
                    $.ajax({
                        type: 'POST',
                        url: $form.attr('action'),
                        data: $form.serialize(),
                        success: function (data) {
                            if (data) {
                                $resultado.html(data);
                                $('#Cpf').inputmask('999.999.999-99').attr('readonly', true);
                            } else {
                                verifica_servico($cpf.val());
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            if (errorThrown == 'Not Found') {
                                alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: Não é possível encontrar o recurso.');
                            } else {
                                alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: ' + errorThrown);
                            }
                        }
                    });
                } catch (e) {
                    alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: ' + e.message);
                }

                return false;
            });
        },
        //SE NAO ENCONTRAR O CPF NO CSV
        //BUSCA CPF NO SERVICO DO BYYOU
        verifica_servico = function (cpf) {
            _cpf = cpf.replace(/[\.|-]/g, '');

            //CHAMADA AO SERVICO
            try {
                $.ajax({
                    url: 'https://www.qabyyou.com/api/rest/social/viralizacao/socialTenant/remoteDocumentVerifier?jsoncallback=cpfvalido',
                    jsonp: false,
                    data: {
                        'code': 'cpf',
                        'documentValue': _cpf
                    },
                    dataType: 'jsonp'
                });
            } catch (e) {
                alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: ' + e.message);
            }

            //RETORNO DA CHAMADA AO SERVICO
            window.cpfvalido = function (data) {
                if (data == 'true') {
                    $.get('/Home/FormEmail?cpf=' + _cpf, function (data) {
                        $resultado.html(data);
                    });
                } else {
                    contato();
                }
            }
        },
        //SE NAO ENCONTRAR CPF NO CSV
        //SE NAO ENCONTRAR CPF NO SERVICO DO BYYOU
        //INFORMA O USUARIO PARA ENTRAR EM CONTATO COM A ESTACIO
        contato = function () {
            $resultado.html('<p><strong>E-mail não encontrado.</strong></p><p>Por favor, entre em contato pelo: e-mail@estacio.com.br</p>');
        },
        //VALIDA E CHAMA SERVICO DE CONVITE DO BYYOU
        ajax_envia_convite = function () {
            $(document).on('submit', '#enviaConvite', function (e) {
                e.preventDefault();

                var $form = $('#enviaConvite');

                try {
                    $.ajax({
                        type: 'POST',
                        url: $form.attr('action'),
                        data: $form.serialize(),
                        success: function (data) {
                            if (data) {
                                $resultado.html(data);
                            } else {
                                servico_envia_convite();
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            if (errorThrown == 'Not Found') {
                                alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: Não é possível encontrar o recurso.');
                            } else {
                                alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: ' + errorThrown);
                            }
                        }
                    });
                } catch (e) {
                    alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: ' + e.message);
                }

                return false;
            });
        },
        //INTEGRACAO COM O SERVICO DO BYYOU
        servico_envia_convite = function () {
            //CHAMADA AO SERVICO
            try {
                $.ajax({
                    url: 'https://www.qabyyou.com/api/rest/social/viralizacao/socialTenant/remoteSendInvite?jsoncallback=callback_convite',
                    jsonp: false,
                    data: {
                        'email': $('#Email').val(),
                        'authorizationCode': '0124b972-ddbc-4f97-b1b4-02fa528c72fe'
                    },
                    dataType: 'jsonp'
                });
            } catch (e) {
                alert('Erro, por favor entre em contato com o administrador do sistema\n\nMensagem: ' + e.message);
            }

            //RETORNO DA CHAMADA AO SERVICO
            window.callback_convite = function (data) {
                if (data.sent) {
                    $resultado.html('<h3>Convite enviado com sucesso!</h3>');
                } else {
                    $resultado.html('<h3>Erro</h3><p>Por favor, entre em contato com o administrador do sistema.</p>');
                }
            }
        },
        //INICIA LOGICA PARA VERIFICAR CPF E ENVIAR CONVITE
        init = function () {
            $cpf.inputmask('999.999.999-99');

            verifica_csv();
        };

    return {
        init: init,
        convite: ajax_envia_convite
    };
}();

cadastro.init();