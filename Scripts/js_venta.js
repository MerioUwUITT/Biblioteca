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

//crea una funcion que se llame llenararray()
function llenararray() {
   // tenemos el id del libro que se esta vendiendo y la cantidad de libros que se estan vendiendo, definiermos un array para cada uno
    //DOS ARRAYS
    //el primero es para el id del libro
    //el segundo es para la cantidad de libros que se estan vendiendo
    var arrayids = [];
    var arraycantidades = [];
    //capturamos el id llamado tamanio, que define cuantas iteraciones se haran
    var i = document.getElementById("tamanio").getAttribute("data-i");
    console.log(i);
    //iteramos desde 0 hasta i
    for (var j = 0; j < i; j++) {
        // en una tabla tengo el id asi <td id="id_@i" value="@item.id_libro">@item.titulo</td>, entonces para capturar el id, hago lo siguiente
        //capturamos el id del libro
        var id_libro = document.getElementById("id_" + j).getAttribute("value");
        //capturamos la cantidad de librosque capturo su vlores con                                         <label id="cantidad_@i" name="cantidad_@i" max="@maxCantidad" class="center-align white-text" data-cantidad="0">0</label>
        var cantidad = document.getElementById("cantidad_" + j).getAttribute("data-cantidad");
        //solo los agregamos si la cantidad es mayor a 0
        if (cantidad > 0) {
            //agregamos el id del libro al array de ids
            arrayids.push(id_libro);
            //agregamos la cantidad de libros al array de cantidades
            arraycantidades.push(cantidad);
        }
    }
    console.log(arrayids);
    console.log(arraycantidades);
    //imprimimos los arrays en consola para ver que esten bien
    //una vez que tenemos los dos arrays, los mandamos como return de la funcion
    return [arrayids, arraycantidades];



}

function hacerVenta() {
    var id_venta = getParameterByName("id_venta")
    //recibimos los arrays que se mandaron como return de la funcion
    var array = llenararray();
    //el primer array es el de los ids
    var arrayids = array[0];
    //el segundo array es el de las cantidades
    var arraycantidades = array[1];
    $.ajax({
        type: "POST",
        url: UrlHacerVenta,
        async: true,
        data: {
            id_venta: id_venta,//esta venta es la que se esta haciendo, por lo que puede ser null, pero si no es null, es porque se esta editando
            //debo mandar el id del index elegido para el cliente y el id del personal que realizo la venta
            id_cliente: document.querySelector('#id_cliente option:checked').value,
            id_personal: document.querySelector('#id_personal option:checked').value,
            //mandamos los dos arrays
            arrayids: arrayids,
            arraycantidades: arraycantidades
        },
        success: function (data) {

            location.href = "../home/formularioVenta";
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}