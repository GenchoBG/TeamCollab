let connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

let sender = $("#sender").text();
let room = $("#room").text();

//Disable send button until connection is established
$("#sendButton").attr("disabled", "disabled");

connection.on("ReceiveMessage", function (user, message) {
    let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    let encodedMsg = user + " says " + msg;
    let div = $("<div>");
    div.text(encodedMsg);
    $("#messages").append(div);
});

connection.start().then(function () {
    connection.invoke("JoinRoom", room).catch(function (err) {
        return console.error(err.toString());
    });
    $("#sendButton").removeAttr("disabled");
}).catch(function (err) {
    return console.error(err.toString());
});

$("#sendButton").on("click", function (event) {
    let message = $("#messageInput").val();
    connection.invoke("SendMessage", room, sender, message).catch(function (err) {
        return console.error(err.toString());
    });
    $("#messageInput").val("");
    event.preventDefault();
});
