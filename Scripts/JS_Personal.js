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
function guardarPersonal() {
    var idPersonal = getParameterByName("IDPersonal")
    $.ajax({
        type: "POST",
        url: UrlGuardarPersonal,
        async: true,
        data: {
            id_personal: idPersonal,
            nombre: document.getElementById('nombre').value,
            apellido: document.getElementById("apellido").value,
            puesto: document.getElementById("puesto").value,
            sueldo: document.getElementById("sueldo").value,
        },
        success: function (data) {

            location.href = "../home/listaPersonal";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}