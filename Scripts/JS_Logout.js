function getParameterByName(name) {
    if (name !== "" && name !== null && name != undefined) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    } else {
        var arr = location.href.split("/");
        return arr[arr.length - 1];
    }

}
function AdminLogout() {
    var idAdmin = getParameterByName("id_admin")
    $.ajax({
        type: "POST",
        url: UrlAdminLogout,
        async: true,
        data: {
            id_admin: idAdmin,
            
        },
        success: function (data) {

            location.href = "../home/Index";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}