

        $('.btn-active').off('click').on('click', function (e) {
            debugger;
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                type: "POST",
                dataType: "json",
                success: function (res) {
                    if (res.status) {
                        btn.text("Kích hoạt");
                    }
                    else {
                        btn.text("Khóa");
                    }

                }
            });
        });






