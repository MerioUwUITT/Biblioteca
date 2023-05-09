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
function guardarCategoria() {
    var IDCategoria = getParameterByName("IDCategoria")
    $.ajax({
        type: "POST",
        url: UrlGuardarCategoria,
        async: true,
        data: {

            id_categoria_libro: IDCategoria,
            nombre: document.getElementById("nombre").value,
            descripcion: document.getElementById("descripcion").value,

        },
        success: function (data) {

            location.href = "../home/listaCategorias";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}