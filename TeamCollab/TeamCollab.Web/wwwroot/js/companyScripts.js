window.onload = function() {
    $.ajax({
        type: "GET",
        url: "/Company/GetUsers",
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                var user = data[i];
                $("#managersTable")
                    .append($(`<tr id="${user.id}"></tr>`)
                        .append($(`<td>`).text(user.userName))
                        .append($(`<td>`).text(user.email))
                        .append($('<td>')
                            .append($(`<button onclick="Promote('${user.id}')" class="btn btn-outline-info" id="btn-${user.id}">`).text("Promote"))));
            }
        },
        error: function(err) {
            console.log(err);
        }
    });


};

function Promote(userId) {
    $.ajax({
        type: "GET",
        url: `/Company/Promote/${userId}`,
        success: function () {
            $(`#btn-${userId}`).addClass("disabled");
            $(`#${userId}`).fadeOut("slow");
        },
        error: function (err) {
            console.log(err);
        }
    });
}