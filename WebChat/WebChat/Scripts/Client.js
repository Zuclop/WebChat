$(function () {

    $('#chatBody').hide();
    $('#loginBlock').show();

    // Ссылка на автоматически-сгенерированный прокси хаба
    var chat = $.connection.chatHub;

    // Хаб вызывает после подключения агента
    chat.client.onAgentConnected = function (agentName, agentConnectionId) {
        // устанавливаем имя собеседника в шапке, сохраняем его id
        
        $('#loginBlock').hide();
        $('#chatBody').show();

        $('#agentid').val(agentConnectionId);
        $('#agentname').val(agentName);
        //var name = $('#clientname').val();
        $("#header").html('<h3>' + agentName + ': Добро пожаловать, ' + $("#clientname").val() + '</h3>');
    };

    // Хаб вызывает при получении сообщений
    chat.client.addMessage = function (message) {
        // Добавление сообщений на веб-страницу 
        $('#chatroom').append('<p><b>' + $('#agentname').val() + '</b>: ' + htmlEncode(message) + '</p>');
    };

    $.connection.hub.url = "http://localhost:54347/signalr"

    // Открываем соединение
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            // Вызываем у хаба метод Send
            chat.server.send($('#agentid').val(), $('#message').val());            
            $('#chatroom').append('<p><b>' + $('#clientname').val() + '</b>: ' + $('#message').val() + '</p>');
            $('#message').val('');
        });

        // обработка логина
        $("#btnLogin").click(function () {

            var name = $("#txtUserName").val();
            if (name.length > 0) {
                $('#clientname').val(name);
                chat.server.connect(name);
            }
            else {
                alert("Введите имя");
            }
        });
    });
});
// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
//Добавление нового пользователя
function AddUser(id, name) {

    var userId = $('#hdId').val();

    if (userId != id) {

        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
    }
}