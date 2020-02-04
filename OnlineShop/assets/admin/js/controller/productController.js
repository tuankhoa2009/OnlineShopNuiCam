

        $('.btn-active').off('click').on('click', function (e) {
            debugger;
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Product/ChangeStatusProduct",
                data: { id: id },
                type: "POST",
                dataType: "json",
                success: function (res) {
                    if (res.status == 1) {
                        btn.text("Còn hàng");
                    }
                    else {
                        btn.text("Hết hàng");
                    }

                }
            });
        });






