var CapNhatThongTin = function (id, flag) {

    if (flag == 1) {
        var url = "/Home/CapNhatThongTin?ID=" + id;
        $("#xemThongTin").load(url, function () {
            $("#titleModal").html("Thay đổi thông tin cá nhân")
            $("#myModalXemThongTin").modal("show");
        })
    } else {
        var url = "/Home/DoiMatKhau?ID=" + id;
        $("#xemThongTin").load(url, function () {
            $("#titleModal").html("Đổi mật khẩu")
            $("#myModalXemThongTin").modal("show");
        })
    }
   
}