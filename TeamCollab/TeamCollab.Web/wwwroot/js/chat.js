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
    var hours = date.getHours();
    var ampm = "AM";
    if (hours > 12) {
        hours -= 12;
        ampm = "PM";
    }
    var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();

    return `${hours}:${minutes} ${ampm}`;
}

$("#messageInput").on("keypress", function (event) {
    if (event.which === 13) {
        if (!event.shiftKey) {
            $("#sendButton").click();
        }
        event.preventDefault();
    }
});

function appendMessage(user, message, id) {
    let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    if (user === sender) {
        $("#messages").append($(`<div id="${id}" class="message person d-block">
        <span class="custom-tooltip">
            <span class="messageContent messageSmallContent">${msg}</span>
            <span id="tooltip-${id}" class="custom-tooltip-text">
                <ion-icon name="trash" onclick="deleteMessage(${id})"></ion-icon>
                <ion-icon name="create" onclick="editMessage(${id})"></ion-icon>
            </span>
        </span>
        <div class="timestamp">${DisplayCurrentTime(new Date(Date.now()))}</div></div>`));
    } else {
        $("#messages").append(
            $(`<div id="${id}" class="message d-block">
            <div><small><strong>${user}</strong></small></div>
            <span class="messageContent messageSmallContent">${msg}</span>
            <div class="timestamp">${DisplayCurrentTime(new Date(Date.now()))}</div></div>`));
    }
    tooltipAlign(id);
}

connection.on("ReceiveMessage", appendMessage);
connection.on("UpdateMessage", function (messageId, message) {
    $("#" + messageId).find(".messageContent").text(message);
});
connection.on("DeleteMessage", function (messageId) {
    $("#" + messageId).remove();
});

connection.start().then(function () {
    connection.invoke("JoinRoom", room).then(scrollToBottom).catch(function (err) {
        return console.error(err.toString());
    });

    $("#sendButton").removeAttr("disabled");
}).catch(function (err) {
    return console.error(err.toString());
});

$("#sendButton").bind("click", sendMessage);

function sendMessage(event) {
    event.preventDefault();

    let message = $("#messageInput").val();
    if (message === "") {
        return;
    }
    connection.invoke("SendMessage", room, sender, message).catch(function (err) {
        return console.error(err.toString());
    });
    $("#messageInput").val("");
    scrollToBottom();
}

// "infinite" scroll stuff
$('#messages').scroll(function () {
    if ($('#messages').scrollTop() === 0) {

        $('#loader').show();

        let lastId = $("#messages div.message")[0].id;

        $.ajax({
            method: "GET",
            url: `/Chat/GetLast?id=${room}&lastLoadedMessageId=${lastId}`,
            success: function (messages) {
                console.log(messages);
                for (let message of messages) {
                    let div = $("<div>");
                    div.attr("id", message.id);
                    div.addClass("message");
                    div.addClass("d-block");
                    if (message.sender === sender) {
                        div.addClass("person");
                    } else {
                        div.append($(`<div><small><strong>${message.sender}</strong></small></div>`));
                    }
                    div.append($(`<p class="messageContent messageSmallContent">${message.content}</p>`));
                    div.append($(`<div class="timestamp">${DisplayCurrentTime(new Date(Date.parse(message.created)))}</div>`));

                    $("#messages").prepend(div);
                }
            }
        }).then(function () {
            $('#loader').hide();
            console.log($(`#${lastId}`).offset().top);
            $('#messages').scrollTop($(`#${lastId}`).offset().top - 200);
        });
    }
});

function tooltipAlign(message) {
    var mess = "#" + message;
    var chatBubble = $(mess).find(".messageContent");
    var bubbleWidth = chatBubble.css("width");
    var margin = "margin-left";
    if (chatBubble.parent().parent().hasClass("person")) {
        margin = "margin-right";
    }
    $(mess).find(".custom-tooltip-text").css(margin, bubbleWidth);
}

$(document).ready(function () {
    $(".message").each(function () {
        tooltipAlign(this.id);
    });
});

function deleteMessage(id) {
    connection.invoke("DeleteMessage", room, id)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

var editId;

function editMessage(id) {
    editId = id;
    var current = $("#" + id).find(".messageContent").text();
    $("#messageInput").val(current);
    $("#sendButton").unbind("click", sendMessage);

    $("#sendButton").bind("click", edit);
}

function edit(event) {
    event.preventDefault();

    connection.invoke("UpdateMessage", room, editId, $("#messageInput").val())
        .then(function () {
            $("#messageInput").val("");
        })
        .catch(function (err) {
            return console.error(err.toString());
        });
}