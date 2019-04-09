$("#description").on("blur",
    function () {
        var content = $("#description").val();
        var id = $("#projectId").text();

        $.ajax({
            url: '/Project/UpdateDescription',
            type: 'post',
            dataType: 'json',
            data: {
                description: content,
                id: id
            },
            success: function(data) {
                console.log(data);
            },
            error: function (err) {
                console.log(err);
            }
        });
    });