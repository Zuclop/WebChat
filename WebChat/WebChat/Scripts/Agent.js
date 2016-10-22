$(function () {

    // Ссылка на автоматически-сгенерированный прокси хаба
    var chat = $.connection.chatHub;

    chat.client.onNewClientConnect = function (name, clientConnectionId) {
        // устанавливаем имя собеседника, сохраняем его id
        $("#clients").prepend('<li><div><input type="button" id="' + htmlEncode(clientConnectionId) + '" value="' + htmlEncode(name) + '" />' +
            '<input type="hidden" id="clientid" value="' + htmlEncode(clientConnectionId) + '"/>' +
            '<input type="hidden" id="clientname" value="' + htmlEncode(name) + '"/>' +
            +'</div></li>');
    };

    // Хаб вызывает при получении сообщений
    chat.client.addMessage = function (message) {
        // Добавление сообщений на веб-страницу 
        $('#chatroom').append('<p><b>' + $('#clientname').val() + '</b>: ' + htmlEncode(message) + '</p>');
    };

    // Открываем соединение
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            // Вызываем у хаба метод Send
            chat.server.send($('#clientid').val(), $('#message').val());
            $('#chatroom').append('<p><b>' + $('#agentname').val() + '</b>: ' + $('#message').val() + '</p>');
            $('#message').val('');
        });

        chat.server.connect($('#agentname').val());
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

    if (userId !== id) {

        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
    }
}