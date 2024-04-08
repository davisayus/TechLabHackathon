// Suscribirse al evento input del editor HTML
//window.subscribeToKeyDownEvent = function (editor) {
//    editor.addEventListener('keydown', function () {
//        var cursorPosition = editor.selectionStart;
//        console.log("cursorPosition:", editor.Id);
//        DotNet.invokeMethodAsync('LT.TechLabHackathon.UI', 'OnKeyDown', cursorPosition);
//    });
//};

window.moveCursorToEnd = function (id) {
    //var editor = document.getElementsByClassName('rz-html-editor-content')[0];
    var spanElements = document.getElementsByClassName('span_color');

    var currentElement = spanElements[spanElements.length - 1];
    var editor = currentElement.parentElement;

    editor.focus()
    if (typeof editor.selectionStart == "number") {
        editor.selectionStart = editor.selectionEnd = editor.value.length;
    }
    else
    { 
        var range = document.createRange();
        range.selectNodeContents(editor);
        range.collapse(false); // Colapsamos al final
        var selection = window.getSelection();
        selection.removeAllRanges(); // Limpiamos las selecciones anteriores
        selection.addRange(range); // Añadimos el rango al div
    }
};

//window.subscribeInputEvent = function () {
//    var editor = document.getElementsByClassName('Xrz-html-editor-content')[0];
//    editor.addEventListener('input', function (event) {
//        var texto = editor.innerText;
//        var cursorInicio = editor.selectionStart;
//        var cursorFin = editor.selectionEnd;
//        var palabrasReservadas = ['public', 'class', 'int']; // Tu lista de palabras reservadas

//        palabrasReservadas.forEach(function (palabra) {
//            var regex = new RegExp('\\b' + palabra + '\\b', 'gi');
//            texto = texto.replace(regex, '<span style="color: blue;">$&</span>');
//        });

//        editor.innerHTML = texto;

//        // Restaurar la posición del cursor
//        moveCursorToEnd('editor');
//    });
//};

// Función para obtener la posición del cursor dentro de un elemento <div>
window.getCursorPosition = function (editor) {
    var divEditor = editor.children[1];

    var selection = window.getSelection();
    var range = selection.getRangeAt(0);
    var preCaretRange = range.cloneRange();
    preCaretRange.selectNodeContents(divEditor);
    preCaretRange.setEnd(range.endContainer, range.endOffset);
    return preCaretRange.toString().length;
};

// Función para establecer la posición del cursor dentro de un elemento <div>
window.setCursorPosition = function (editor, position) {
    var divEditor = editor.children[1];

    var range = document.createRange();
    var sel = window.getSelection();
    range.selectNodeContents(divEditor);

    //range.setStart(divEditor.children[4], 2);
    //range.setEnd(divEditor.children[4], 5)
    range.collapse(true);
    sel.removeAllRanges();
    sel.addRange(range);
};

window.setContent = function (editor, content) {
    var divEditor = editor.children[1];
    divEditor.innerHTML = content;
    divEditor.focus();
};

window.getInnerText = function (editor) {
    var divEditor = editor.children[1];
    return divEditor.innerText;
};

window.saveSelection = function () {
    var sel = window.getSelection();
    if (sel.rangeCount > 0) {
        var range = sel.getRangeAt(0);
        var selectionId = ++currentSelectionId; // Genera un nuevo ID
        selections[selectionId] = range; // Almacena el rango usando el ID
        return selectionId; // Devuelve el ID al código Blazor
    }
};

//windows.restoreSelection = function (selectionId) {
//    var range = selections[selectionId];
//    if (range) {
//        var sel = window.getSelection();
//        sel.removeAllRanges();
//        sel.addRange(range);
//    }
//};

//window.addEventListener('DOMContentLoaded', (event) => {
//    const editor = document.querySelector('.rz-html-editor');

//    editor.addEventListener('input', (event) => {
//        const contenido = editor.contentDocument.body.innerHTML;

//        // Resalta las palabras reservadas en azul
//        const palabrasReservadas = @Json.Serialize(palabrasReservadas);
//        const regex = new RegExp('\\b(' + palabrasReservadas.join('|') + ')\\b', 'gi');
//        const contenidoResaltado = contenido.replace(regex, '<span style="@estiloResaltado">$1</span>');

//        editor.contentDocument.body.innerHTML = contenidoResaltado;
//    });
//});