let connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

let sender = $("#sender").text();
let room = $("#room").text();

//Disable send button until connection is established
$("#sendButton").attr("disabled", "disabled");

function scrollToBottom() {
    var heightMessages = $('#messages').prop('scrollHeight') * 2;
    $("#messages").animate({ scrollTop: heightMessages }, 1000);
}

function DisplayCurrentTime(date) {
    console.log(date);

    var hours = date.getHours();
    var ampm = "AM";
    if (hours > 12) {
        hours -= 12;
        ampm = "PM";
    }
    var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();

    return `${hours}:${minutes} ${ampm}`;
};

connection.on("ReceiveMessage", function (user, message) {
    let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    if (user === sender) {
        $("#messages").append($(`<div class="message person d-block">
        <p class="messageContent messageSmallContent">${msg}</p>
        <div class="timestamp">${DisplayCurrentTime(new Date(Date.now()))}</div></div>`));
    } else {
        $("#messages").append(
            $(`<div class="message d-block">
            <div><small><strong>${user}</strong></small></div>
            <p class="messageContent messageSmallContent">${msg}</p>
            <div class="timestamp">${DisplayCurrentTime(new Date(Date.now()))}</div></div>`));
    }
});

connection.start().then(function () {
    connection.invoke("JoinRoom", room).then(scrollToBottom).catch(function (err) {
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
    scrollToBottom();
    event.preventDefault();
});
