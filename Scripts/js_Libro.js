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
function guardarLibro() {
    var idLibro = getParameterByName("IDLibro")
    $.ajax({
        type: "POST",
        url: UrlGuardarLibro,
        async: true,
        data: {
            id_libro: idLibro,
            id_categoria_libro: document.querySelector('#id_categoria_libro option:checked').value,
            titulo: document.getElementById("titulo").value,
            autor: document.getElementById("autor").value,
            editorial: document.getElementById("editorial").value,
            numero_copias: document.getElementById("numero_copias").value,
            precio: document.getElementById("precio").value
        },
        success: function (data) {

            location.href = "../home/listaLibros";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}