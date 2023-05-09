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
function guardarCliente() {
    var idCliente = getParameterByName("IDCliente")
    $.ajax({
        type: "POST",
        url: UrlGuardarCliente,
        async: true,
        data: {
            id_cliente: idCliente,
            nombre: document.getElementById('nombre').value,
            apellido: document.getElementById("apellido").value,
            direccion: document.getElementById("direccion").value,
            telefono: document.getElementById("telefono").value,
            email: document.getElementById("email").value,
        },
        success: function (data) {

            location.href = "../home/listaClientes";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}