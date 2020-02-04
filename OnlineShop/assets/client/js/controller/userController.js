

function loadProvince() {
    $.ajax({
        url: '/User/LoadProvince',
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                var html = '<option value="">--Chọn tỉnh thành--</option>';
                var data = response.data;
                $.each(data, function (i, item) {
                    html += '<option value="' + item.ID + '">'+item.Name+'</option>'
                });
                $('#ddlProvinceID').html(html);
            }
        }

    })
}

function loadDistrict(id) {
    $.ajax({
        url: '/User/LoadDistrict',
        type: 'POST',
        data: { provinceID:id },
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                var html = '<option value="">--Chọn Quận Huyện--</option>';
                var data = response.data;
                $.each(data, function (i, item) {
                    html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                });
                $('#ddlDistrictID').html(html);
            }
        }

    })
}


$(document).ready(function () {
    debugger;   
    loadProvince();
    $('#ddlProvinceID').off('change').on('change', function() {
        var id = $(this).val();
        if (id != '') {
            loadDistrict(parseInt(id));
        }
        else {
            $('#ddlDistrict').html('');
        }
    });

});
