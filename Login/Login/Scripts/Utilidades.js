function OcultarURL(saludo) {
    //alert(saludo);
    var blobMe = URL['createObjectURL'](new Blob([''], { type: 'text/html' }));
    var elIframe = document['createElement']('iframe');
    //elIframe.classList("w-100");
    //elIframe.classList("h-100");

    elIframe['setAttribute']('frameborder', '0');
    //elIframe['setAttribute']('width', '100%');
    //elIframe['setAttribute']('height', '100%');
    //elIframe['setAttribute']('height', '100vh');
    elIframe['setAttribute']('allowfullscreen', 'true');
    elIframe['setAttribute']('webkitallowfullscreen', 'true');
    elIframe['setAttribute']('mozallowfullscreen', 'true');
    elIframe['setAttribute']('src', blobMe);
    //elIframe['setAttribute']('class', "embed-responsive-item");
    elIframe['setAttribute']('style', "overflow: hidden; height: 100%; width: 100%; position: absolute;");
    var idOne = 'gepa_' + Date.now();
    elIframe['setAttribute']('id', idOne);
    document.getElementById('myframe').appendChild(elIframe);
    //const iframeHere = 'https://app.powerbi.com/view?r=eyJrIjoiNWE2NWJmNjktOWZiYS00ODRmLWJkZDctYTI2NmRjNDIwYjJkIiwidCI6IjhmYmFhNWJmLTJlY2MtNGRjOC1iNTZiLThmOTJlMzA3ZjA3NiIsImMiOjR9';
    const iframeHere = saludo;
    document['getElementById'](idOne)['contentWindow']['document'].write('<script type="text/javascript">location.href = "' + iframeHere + '";</script>')
}

function saludo() {
    alert("Hola");
}