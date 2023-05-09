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
function guardarChecada() {
    var IDChecada = getParameterByName("IDChecada")
    $.ajax({
        type: "POST",
        url: UrlGuardarChecada,
        async: true,
        data: {
            id_checada: IDChecada,
            id_personal: document.querySelector("#id_personal option:checked").value,
            fecha: document.getElementById("fecha").value,
            hora_entrada: document.getElementById("hora_entrada").value,
            hora_salida: document.getElementById("hora_salida").value
        },
        success: function (data) {

            location.href = "../home/listaChecadas";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}