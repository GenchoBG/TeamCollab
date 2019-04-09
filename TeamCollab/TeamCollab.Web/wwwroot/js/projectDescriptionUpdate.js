$("#description").on("blur",
    function () {
        var content = $("#description").val();
        var id = $("#projectId").text();

        if (content.length >= 100 && content.length <= 500) {
            $.ajax({
                url: '/Project/UpdateDescription',
                type: 'post',
                dataType: 'json',
                data: {
                    description: content,
                    id: id
                },
                success: function (data) {
                    console.log(data);
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    });