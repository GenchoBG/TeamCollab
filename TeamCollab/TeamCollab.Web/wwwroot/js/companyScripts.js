window.onload = function () {
    $.ajax({
        type: "GET",
        url: "/Company/GetUsers",
        success: function (data) {
            console.log(data);
            for (var user of data) {
                $("#managersTable")
                    .append($(`<tr id="${user.id}"></tr>`)
                        .append($(`<td>`).text(user.userName))
                        .append($(`<td>`).text(user.email))
                        .append($('<td>')
                            .append($(`<button class="btn btn-outline-info">`).text("Promote")
                                .on("click",
                                    function () {
                                        $.ajax({
                                            type: "GET",
                                            url: `/Company/Promote/${user.id}`,
                                            success: function () {
                                                $(`#${user.id}`).fadeOut("slow");
                                            }
                                        });
                                    }))));
            }
        }
    });
}