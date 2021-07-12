function tresRelacionados(id) {

    $('#tresRelacionados').html('<h2>Cargando Información...espere un momento por favor</h2>')


    $.ajax(
        {
            type: 'POST',
            data: "{'id':" + "'" + id + "'}", //datos o parametros enviados al servidor
            dataType: 'html', //el tipo de dato que nos regresara el servidor en este caso regresa html
            url: '/info/tresRelacionados?id=' + id,
            //URL del action result que cargara la vista parcial
            success: function (result) {
                // si la funcion se ejecuta lanzara un alert
                /*alert('Success');*/
                //cuando se ejecuta bien la funcion agregara al div vistaParcial el contenido
                //que recibio del servidor
                $("#tresRelacionados").html(result);
            },

            error: function (error) {
                // si hay un error lanzara el mensaje de error
                //alert('Fail');
                $("#tresRelacionados").html('<h2>Aun estamos trabajando en mostrar más información sobre este tema.</h2>');
            }
        });

}

function carruselRelacionados(id) {

    $('#carruselRelacionados').html('<h2>Cargando Información...espere un momento por favor</h2>')


    $.ajax(
        {
            type: 'POST',
            data: "{'id':" + "'" + id + "'}", //datos o parametros enviados al servidor
            dataType: 'html', //el tipo de dato que nos regresara el servidor en este caso regresa html
            url: '/info/carruselRelacionados?id=' + id,
            //URL del action result que cargara la vista parcial
            success: function (result) {
                // si la funcion se ejecuta lanzara un alert
                /*alert('Success');*/
                //cuando se ejecuta bien la funcion agregara al div vistaParcial el contenido
                //que recibio del servidor
                $("#carruselRelacionados").html(result);
            },

            error: function (error) {
                // si hay un error lanzara el mensaje de error
                //alert('Fail');
                $("#carruselRelacionados").html('<h2>Aun estamos trabajando en mostrar más información sobre este tema.</h2>');
            }
        });
   
}
