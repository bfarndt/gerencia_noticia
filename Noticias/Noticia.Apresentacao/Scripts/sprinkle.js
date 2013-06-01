$(document).ready(function() {
	$('#tabvanilla > ul').tabs({ fx: { height: 'toggle', opacity: 'toggle' } });
	$('#featuredvid > ul').tabs();
});

var tempoTodosSistemas;
var tempoAberturaTodosSistemas;

function fecharTodosDeptos() {
    $(".todosSistemas").slideUp(500);
    tempoTodosSistemas = undefined;
}

$("#linkAbrirSistema").mouseover(function () {
    
    if (tempoTodosSistemas != undefined)
        clearTimeout(tempoTodosSistemas);
    else {
        tempoAberturaTodosSistemas = setTimeout('mostrarSistemas();', 200);
    }
});

$("#linkAbrirSistema").click(function () {
    mostrarSistemas();
});

function mostrarSistemas() {
    alert('teste');
    $("#todoSistema").slideDown(500);
}

$("#linkAbrirSistema").mouseleave(function () { tempoTodosSistemas = setTimeout("fecharTodosDeptos();", 200); try { clearTimeout(tempoAberturaTodosSistemas); } catch (e) { }; });
$(".todosSistemas").mouseover(function () { clearTimeout(tempoTodosSistemas); tempoTodosSistemas = undefined; });
$(".todosSistemas").mouseleave(function () { tempoTodosSistemas = setTimeout("fecharTodosDeptos();", 200); });