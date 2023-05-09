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
function guardarTipoPrestamo() {
    var IDTipoPrestamo = getParameterByName("IDLibro")
    $.ajax({
        type: "POST",
        url: UrlGuardarTipoPrestamo,
        async: true,
        data: {
            id_tipo_prestamo: IDTipoPrestamo,
            tipo_prestamo: document.getElementById("tipo_prestamo").value,
            descripcion: document.getElementById("descripcion").value
        },
        success: function (data) {

            location.href = "../home/listaTipoPrestamo";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}