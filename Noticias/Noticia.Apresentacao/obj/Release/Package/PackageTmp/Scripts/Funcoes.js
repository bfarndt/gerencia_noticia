/*
Nome: Bento Rafael Siqueira
Criado em: Domingo, 17 de abril de 2011 01:40
Alterado em:
Descrição: Funções que serão estudadas e implementadas de maneira genérica.
*/

//Permitir que um campo possa utilizar apenas números
function SomenteNumero(e) {
    var tecla = (window.event) ? event.keyCode : e.which;
    if ((tecla > 47 && tecla < 58))
        return true;
    else {
        if (tecla == 8 || tecla == 0)
            return true;
        else
            return false;
    }
}

//Atribuir qualquer mascara
function formatar(src, mask) {
    var i = src.value.length;
    var saida = mask.substring(0, 1);
    var texto = mask.substring(i)
    if (texto.substring(0, 1) != saida) {
        src.value += texto.substring(0, 1);
    }
}

//Validacao de dados inicial e final com horas
function verificaDatas(dtInicial, dtFinal, hrInicial, hrFinal) {

    var dtini = dtInicial;
    var dtfim = dtFinal;
    var hrini = hrInicial;
    var hrfim = hrFinal;

    if ((dtini == '') && (dtfim == '') && (hrini == '') && (hrfim == '')) {
        return false;
    }

    datInicio = new Date(dtini.substring(6, 10), dtini.substring(3, 5), dtini.substring(0, 2));
    datInicio.setMonth(datInicio.getMonth() - 1);

    datInicio.setHours(hrini.substring(0, 2));
    datInicio.setMinutes(hrini.substring(5, 2).replace(':', ''));

    datFim = new Date(dtfim.substring(6, 10), dtfim.substring(3, 5), dtfim.substring(0, 2));

    datFim.setMonth(datFim.getMonth() - 1);

    datFim.setHours(hrfim.substring(0, 2));
    datFim.setMinutes(hrfim.substring(5, 2).replace(':', ''));

    if (datInicio <= datFim) {
        return true;
    } else {
        return false;
    }
}


/*INICIO IDENTIFICAÇÃO DE BROWSER--------------------------------------------------------------
Identificação de browser
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/

function setValorPesquisaBox(campos, post_pagina_princ) {
    var array_id = campos.split('*');

    for (i = 0; i < array_id.length; i++) {
        window.parent.document.getElementById(array_id[i]).value = array_id[i + 1];
        i = i + 1;
    }

    if (post_pagina_princ == 'true') { window.parent.post(); }

    window.parent.FechaModal();
}

//*** Modal shadow box
function ModalBox(url, width, height, tituloJanela) {

    Shadowbox.open({
        player: 'iframe',
        content: url,
        width: width,
        height: height,
        title: tituloJanela
    });

}

//abrir relatorios.
function AbreRelatorio(url, nome_relatorio) {
    Shadowbox.open({
        player: 'iframe',
        content: url,
        width: 790,
        height: 590,
        title: nome_relatorio
    });
}

//Mensagem de erro customizada, abre a shadowBox
function Mensagem(descricaoErro, alturaJanela) {
    var altura = '';
    if (alturaJanela != '') { altura = alturaJanela } else { altura = '100' }
    Shadowbox.open({
        player: 'html',
        content: '<div class="Aviso" style="text-align:center; text-align:justify; color:#000; padding:10px; margin:10px">' + descricaoErro + '</div>',
        height: altura,
        width: 400
    });
}

function PostShadow(controle) {
    document.getElementById(controle).click();
    Shadowbox.close();
}

function FechaModal() { Shadowbox.close(); }

var isNav4, isNav, isIE;
if (parseInt(navigator.appVersion.charAt(0)) >= 4) {
    isNav = (navigator.appName == "Netscape") ? true : false;
    isIE = (navigator.appName.indexOf("Microsoft") != -1) ? true : false;
}

if (navigator.appName == "Netscape") {
    isNav4 = (parseInt(navigator.appVersion.charAt(0)) == 4);
}
/*FIM IDENTIFICAÇÃO DE BROWSER------------------------------------------------------------*/

/*----------------------------------------------------------------------------------------
aplicacarFormatacaoCampos - Encarregado do comportamento dos campos num formulário
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008


Descrição:
Formata os campos de um formulário HTML de acordo com o texto das primeiras três letras
do nome do campo.
		
O valores possíveis são:
* num - Numérico
* vlr - Valor
* aln - alphanumérico
* alp - alpha	
* cpf - Campo CPF
* cpj - Campo CNPJ
* pfj - campo CPFCNPJ
* eml - campo de email
----------------------------------------------------------------------------------------*/
function aplicacarFormatacaoCampos(objForm) {
    var i, numEl, j;
    numEl = objForm.elements.length;
    for (i = 0; i < numEl; i++) {
        var tipo, prefixo;

        if (objForm.elements[i].name == undefined) {
            prefixo = '';
        } else {
            j = objForm.elements[i].name.length;
            if ((j - 4) >= 0) { j = j - 4; }
            prefixo = objForm.elements[i].name.substr(j, 4);
        }

        switch (prefixo) {
            case "_num":  //campo numérico

                objForm.elements[i].onkeypress = soNumero;
                break;

            case "_aln": //campo alfanumerico - nao permite a insercao de letras acentuadas
                objForm.elements[i].onkeypress = soAlfaNumerico;
                break;

            case "_alp": //campo alfanumerico - nao permite a insercao de letras acentuadas
                objForm.elements[i].onkeypress = soAlfa;
                break;

            case "_vlr":  //campo valor
                objForm.elements[i].onkeypress = exibirValorFormatado;
                objForm.elements[i].onkeydown = capturaCodTecla;

                if ((objForm.elements[i].maxLength == -1) || (objForm.elements[i].maxLength == 2147483647) || (objForm.elements[i].maxLength == "undefined")) {
                    objForm.elements[i].maxLength = 17
                }
                if (navigator.appName != "Netscape") {
                    objForm.elements[i].style.textAlign = "right";
                }
                break;

            case "_dtt":  //campo data
                objForm.elements[i].onkeypress = exibirDataFormatada;
                objForm.elements[i].onkeydown = capturaCodTeclaCPData;
                objForm.elements[i].onfocus = limparValorLabel;
                objForm.elements[i].onblur = exibirValorLabel;
                objForm.elements[i].maxLength = 10
                break;

            case "_dtm":  //campo data
                objForm.elements[i].onkeypress = exibirDataFormatadaMenor;
                objForm.elements[i].onkeydown = capturaCodTeclaCPData;
                objForm.elements[i].onfocus = limparValorLabel;
                objForm.elements[i].onblur = exibirValorLabel;
                objForm.elements[i].maxLength = 5
                break;

            case "_alf":  //campo alfa - só permite a ditação de letras sem acento
                objForm.elements[i].onkeypress = soAlfa;
                break;

            case "_eml":  //campo email - só permite a ditação de letras sem acento, números e os caracteres (@ _ - . /)
                objForm.elements[i].onkeypress = soEmail;
                break;

            case "_cpf": //campo cpf - só permite valores numéricos formatando no padrão de cpf.
                objForm.elements[i].onkeypress = soCPF;
                objForm.elements[i].onkeychange = soCPF;
                break;

            case "_hra": //campo cpf - só permite valores numéricos formatando no padrão de cpf.
                objForm.elements[i].onkeypress = valida_horas;
                break;

        }
    }
}

/*INICIO - FORMATAÇÃO DE CAMPOS------------------------------------------------------------------*/
/* -----------------------------------------------------------------------------------------------
variável que armazena a tecla que foi digita pelo usuário
Essa variável é usa na função: "exibirValorFormatado"
---------------------------------------------------------------------------------------*/
var codTeclaKeyDown;

/* -----------------------------------------------------------------------------------------------
exibirValorFormatado - Encarregada de exibir uma string com formatação de valor num campo text
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/
function exibirValorFormatado(e) {
    var obj, tecla;
    //verificando se o que foi digitado é um número
    if (!soNumero(e)) {
        return false;
    }
    obj = (isNav) ? e.target : event.srcElement;
    codTecla = (isNav) ? e.which : event.keyCode;
    switch (codTeclaKeyDown) {
        case 8:
            obj.value = formatarValor(obj.value.substring(0, obj.value.length - 1));
            break;
        case 9:
            return true;
            break;
        case 46:
            obj.value = formatarValor(obj.value.substring(0, obj.value.length - 1));
            break;
        default:
            if ((codTecla > 47) && (codTecla < 58)) {
                if (obj.maxLength > obj.value.length) {
                    obj.value = formatarValor(obj.value + String.fromCharCode(codTecla));
                }
            }
    }
    return false;
}

function valida_horas(e) {
    var obj, tecla;

    if (!soNumero(e)) {
        return false;
    }

    obj = (isNav) ? e.target : event.srcElement;

    if (obj.value.length == 2) {
        obj.value += ":";
    }
}



/* -----------------------------------------------------------------------------------------------
exibirDataFormatadaMenor - Encarregada de exibir uma string com formatação de data num campo text
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/

function exibirDataFormatadaMenor(e) {
    var obj, tecla;
    //verificando se o que foi digitado é um número
    if (!soNumero(e)) {
        return false;
    }
    obj = (isNav) ? e.target : event.srcElement;
    codTecla = (isNav) ? e.which : event.keyCode;
    switch (codTeclaKeyDown) {
        case 8:
            obj.value = formatarDataMenor(obj.value.substring(0, obj.value.length - 1));
            break;
        case 9:
            return true;
            break;
        case 46:
            obj.value = formatarDataMenor(obj.value.substring(0, obj.value.length - 1));
            break;
        default:
            if ((codTecla > 47) && (codTecla < 58)) {
                if (obj.maxLength > obj.value.length) {
                    obj.value = formatarDataMenor(obj.value + String.fromCharCode(codTecla));
                }
            }
    }
    return false;
}

/* -----------------------------------------------------------------------------------------------
exibirDataFormatada - Encarregada de exibir uma string com formatação de data num campo text
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/

function exibirDataFormatada(e) {

    var obj, tecla;
    //verificando se o que foi digitado é um número
    if (!soNumero(e)) {
        return false;
    }
    obj = (isNav) ? e.target : event.srcElement;
    codTecla = (isNav) ? e.which : event.keyCode;
    switch (codTeclaKeyDown) {
        case 8:
            obj.value = formatarData(obj.value.substring(0, obj.value.length - 1));
            break;
        case 9:
            return true;
            break;
        case 46:
            obj.value = formatarData(obj.value.substring(0, obj.value.length - 1));
            break;
        default:
            if ((codTecla > 47) && (codTecla < 58)) {
                if (obj.maxLength > obj.value.length) {
                    obj.value = formatarData(obj.value + String.fromCharCode(codTecla));
                }
            }
    }
    return false;
}

function limparValorLabel(e) {
    var obj = (isNav) ? e.target : event.srcElement;

    if (obj.value == "dd/mm/aaaa") {
        obj.value = "";
    }
    else if (obj.value == "mm/aa") {
        obj.value = "";
    }
}

function exibirValorLabel(e) {
    var obj = (isNav) ? e.target : event.srcElement;

    if (obj.value == "") {
        if (obj.maxLength == 10)
            obj.value = "dd/mm/aaaa";
        else
            obj.value = "mm/aa";
    }
}

/* -----------------------------------------------------------------------------------------------
capturaCodTecla - Função encarregada obter a tecla digitada pelo usuário
apresentando comportamento distinto para o netScape e IE.
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/

function capturaCodTecla(e) {
    codTeclaKeyDown = (isNav) ? e.which : event.keyCode;
    if (isIE) {
        switch (codTeclaKeyDown) {
            case 8:
                event.srcElement.value = formatarValor(event.srcElement.value.substring(0, event.srcElement.value.length - 1));
                return false;
                break;
            case 46:
                event.srcElement.value = formatarValor(event.srcElement.value.substring(0, event.srcElement.value.length - 1));
                return false;
                break;
        }
    }
}


/* -----------------------------------------------------------------------------------------------
capturaCodTeclaCPData - Função encarregada obter a tecla digitada pelo usuário
apresentando comportamento distinto para o netScape e IE.
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
----------------------------------------------------*/

function capturaCodTeclaCPData(e) {
    codTeclaKeyDown = (isNav) ? e.which : event.keyCode;
    if (isIE) {
        switch (codTeclaKeyDown) {
            case 8:
                event.srcElement.value = formatarData(event.srcElement.value.substring(0, event.srcElement.value.length - 1));
                return false;
                break;
            case 46:
                event.srcElement.value = formatarData(event.srcElement.value.substring(0, event.srcElement.value.length - 1));
                return false;
                break;
        }
    }
}


/* -----------------------------------------------------------------------------------------------
formatarValor - Formata uma string no padrão de valor: xx.xxx.xxx,xx
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/

function formatarValor(str) {
    var decimal, inteiro;
    var i, count;
    STR = new String(str);
    STR = tirarZerosEsquerda(STR);
    inteiro = '';
    if (STR.length == 1) {
        inteiro = '0';
        decimal = '0' + STR;
    }
    else {
        if (STR.length == 2) {
            inteiro = '0';
            decimal = STR;
        }
        else {
            decimal = STR.substring(STR.length - 2, STR.length);
            i = 3;
            count = 0;
            while (i <= STR.length) {
                if (count == 3) {
                    inteiro = '.' + inteiro;
                    count = 0;
                }
                inteiro = STR.charAt(STR.length - i) + inteiro;
                count++;
                i++;
            }
        }
    }
    if (inteiro == '') {
        inteiro = '0';
    }
    if (decimal == '') {
        decimal = '00';
    }
    return inteiro + ',' + decimal;
}

/* -----------------------------------------------------------------------------------------------
formatarData - Formata uma string no padrão de data: DD/MM/AAAA
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/
function formatarData(str) {
    var data, re, T;
    re = /\//g;
    data = new String(str);
    data = data.replace(re, "");
    data = data.substr(0, 8);
    T = data.length;
    if (T > 2 && T < 5) {
        data = data.substr(0, 2) + "/" + data.substr(2, 2);
    }
    if (T > 4) {
        data = data.substr(0, 2) + "/" + data.substr(2, 2) + "/" + data.substr(4, 4);
    }
    return data;
}

/* -----------------------------------------------------------------------------------------------
formatarDataMenor - Formata uma string no padrão de data: MM/AA
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/
function formatarDataMenor(str) {
    var data, re, T;
    re = /\//g;
    data = new String(str);
    data = data.replace(re, "");
    data = data.substr(0, 4);
    T = data.length;
    if (T > 2 && T < 5) {
        data = data.substr(0, 2) + "/" + data.substr(2, 2);
    }
    if (T < 3) {
        data = data;
    }
    return data;
}
/* -----------------------------------------------------------------------------------------------
tirarZerosEsquerda - função que tira todos os "0", "," e "." da string
passada como argumento
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------*/

function tirarZerosEsquerda(STR) {
    var sAux = '';
    STR = new String(STR);
    var i = 0;
    while (i < STR.length) {
        if ((STR.charAt(i) != '.') && (STR.charAt(i) != ',')) {
            sAux += STR.charAt(i);
        }
        i++
    }
    STR = new String(sAux);
    sAux = '';
    i = 0;
    while (i < STR.length) {
        if (STR.charAt(i) != '0') {
            sAux = STR.substring(i, STR.length)
            i = STR.length;
        }
        i++;
    }
    return sAux;
}

/* -----------------------------------------------------------------------------------------------
tirarZerosEsquerda - função que tira todos os "0", "," e "." da string
passada como argumento
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function soNumero(e) {
    var keyNumber = (isIE) ? event.keyCode : e.which;
    if (((keyNumber < 48) || (keyNumber > 57)) && (keyNumber != 13) && (keyNumber != "0") && (keyNumber != 8)) {
        if (isIE) {
            event.keyCode = 0
        }
        return false;
    }
    return true;
}
/*FIM - FORMATAÇÃO DE VALOR---------------------------------------------------------------------*/


/* -----------------------------------------------------------------------------------------------
validarData - Verifica se a data passada é valida
o retorno da função é booleano, se data válida retorna true senão retorna false
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function validarData(DIA, MES, ANO) {
    MES = MES - 1;
    data = new Date(ANO, MES, DIA);
    if ((data.getDate() != DIA) || (data.getMonth() != MES) || (data.getFullYear() != ANO)) {
        return false;
    }
    return true;
}
/*-----------------------------------------------------------------------------------------------*/

/* -----------------------------------------------------------------------------------------------
verifDataSelMenorDataAtual - Verifica se a data selecionada é menor que a data atual
o retorno da função é booleano, se data selecionada for menor que data atual retorna true senão 
retorna false
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function verifDataSelMenorDataAtual(diaAtual, mesAtual, anoAtual, diaSelecionado, mesSelecionado, anoSelecionado) {
    var dataAtual = anoAtual + mesAtual + diaAtual;
    var dataSelec = anoSelecionado + mesSelecionado + diaSelecionado;
    dataAtual = parseInt(dataAtual);
    dataSelec = parseInt(dataSelec);
    if (dataSelec < dataAtual) {
        return true;
    }
    return false;
}

/* -----------------------------------------------------------------------------------------------
dataDifDias - Retorna a diferenca entre duas datas, todos os parametros são strings
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function dataDifDias(diaSelecionado, mesSelecionado, anoSelecionado, diaAtual, mesAtual, anoAtual) {
    data1 = new Date(anoAtual, mesAtual - 1, diaAtual);
    data2 = new Date(anoSelecionado, mesSelecionado - 1, diaSelecionado);
    var difDias = data2 - data1;
    difDias /= 86400000;
    return difDias;
}

/* -----------------------------------------------------------------------------------------------
validarAgCta - Verifica se a agencia conta digita é válida
o retorno da função é booleano, se a AG/CTA for válida retorna true senão retorna false
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function validarAgCta(ag, cta) {
    ag = new String(ag);
    cta = new String(cta);

    if ((ag.length4) || (cta.length7)) {
        return false;
    }

    var AG_CTA_AUX = new String(ag + cta);
    var i = 0;
    strRes = '';
    peso = 1;
    for (i = 0; i < 10; i++) {
        strRes += (AG_CTA_AUX.charAt(i) * peso);
        peso = (peso == 1) ? 2 : 1;
    }
    soma = 0;
    for (i = 0; i < strRes.length; i++) {
        soma += parseInt(strRes.charAt(i));
    }
    dv = 10 - (soma % 10);
    if (dv == 10) {
        dv = 0;
    }
    if (cta.charAt(6) == dv) {
        return true;
    }
    return false;

}

/* -----------------------------------------------------------------------------------------------
soAlfaNumerico - só permite a digitação de letras não acentuadas e números
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008	
------------------------------------------------------*/
function soAlfaNumerico(e) {
    var keyNumber = (isIE) ? event.keyCode : e.which;
    if (
			!(
				(
					((keyNumber > 47) && (keyNumber < 58)) ||
					((keyNumber > 64) && (keyNumber < 91)) ||
					((keyNumber > 96) && (keyNumber < 123)) ||
					(keyNumber == 32) ||
					(keyNumber == 8)
				) &&
				(keyNumber != 168)
			)
		) {
        if (isIE) {
            event.keyCode = 0
        }
        return false;
    }
    return true;
}

/* -----------------------------------------------------------------------------------------------
soAlfa - só permite a digitação de letras não acentuadas 
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function soAlfa(e) {
    var keyNumber = (isIE) ? event.keyCode : e.which;
    if (
			!(
				(
					((keyNumber > 64) && (keyNumber < 91)) ||
					((keyNumber > 96) && (keyNumber < 123)) ||
					(keyNumber == 32) ||
					(keyNumber == 8)
				) &&
				(keyNumber != 168)
			)
		) {
        if (isIE) {
            event.keyCode = 0
        }
        return false;
    }
    return true;
}


/* -----------------------------------------------------------------------------------------------
abrirCalendario - Abre calendário pop up para escolha de data de agendamento.
Parametro HTodos:
se parametro passado for = 0 habilita todos os links.  
se parametro passado for = 1 desabilita os links para os feriados e fins de semana.
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function abrirCalendario(comboDia, comboMes, comboAno, HTodos) {
    var cbDiaName = comboDia.name;
    var cbMesName = comboMes.name;
    var cbAnoName = comboAno.name;
    var diaSelecionado = comboDia.options[comboDia.selectedIndex].value;
    var mesSelecionado = comboMes.options[comboMes.selectedIndex].value;
    var anoSelecionado = comboAno.options[comboAno.selectedIndex].value;
    var url = '/calendar.asp?cpDia=' + cbDiaName + '&cpMes=' + cbMesName + '&cpAno=' + cbAnoName + '&valorDia=' + diaSelecionado + '&valorMes=' + mesSelecionado + '&valorAno=' + anoSelecionado + '&HTodos=' + HTodos;
    window.open(url, 'calendar', 'top=70,left=115,width=132,height=140', 0);
}




/* -----------------------------------------------------------------------------------------------
abrirCalendario1 - Abre calendário pop up para escolha de data de agendamento.
Parametro HTodos:
se parametro passado for = 0 habilita todos os links.  
se parametro passado for = 1 desabilita os links para os feriados e fins de semana.
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------*/
function abrirCalendario1(campoTextoData, dataAtualDDMMAAAA, HTodos, segmento, qtdeMesesAnt, qtdeMesesProx) {
    var data, re, cpDataName, diaSel, mesSel, anoSel, dataAtual;
    cpDataName = campoTextoData.name;
    re = /\//g;
    data = new String(campoTextoData.value);
    data = data.replace(re, "");
    dataAtual = new String(dataAtualDDMMAAAA);
    dataAtual = dataAtual.replace(re, "");
    diaSel = "";
    mesSel = "";
    anoSel = "";
    if (campoTextoData.length = 8) {
        diaSel = data.substr(0, 2);
        mesSel = data.substr(2, 2);
        anoSel = data.substr(4, 4);

        if ((!validarData(diaSel, mesSel, anoSel)) || (anoSel < 1970)) {
            diaSel = dataAtual.substr(0, 2);
            mesSel = dataAtual.substr(2, 2);
            anoSel = dataAtual.substr(4, 4);
        }
    }
    var url = '/PF/JavaScript/calendario/calendar1.asp?cpData=' + cpDataName + '&vDia=' + diaSel + '&vMes=' + mesSel + '&vAno=' + anoSel + '&HTodos=' + HTodos + '&Segmento=' + segmento + '&qtdeMesesAnt=' + qtdeMesesAnt + '&qtdeMesesProx=' + qtdeMesesProx;
    if (validarData(diaSel, mesSel, anoSel)) {
        window.open(url, 'calendar', 'top=10,left=10,width=162,height=151', 0);
    }
}

/* -----------------------------------------------------------------------------------------------------------------
soEmail - Validacao de campo e-mail.
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------*/

function soEmail(e) {
    var keyNumber = (isIE) ? event.keyCode : e.which;
    if (
			!(
				(
					((keyNumber > 43) && (keyNumber < 58)) ||
					((keyNumber > 63) && (keyNumber < 91)) ||
					((keyNumber > 96) && (keyNumber < 123)) ||
					((keyNumber > 191) && (keyNumber < 221)) ||
					((keyNumber > 223) && (keyNumber < 253)) ||
					(keyNumber == 32) ||
					(keyNumber == 8) ||
					(keyNumber == 95)
				) &&
				(keyNumber != 168)
			)
		) {
        if (isIE) {
            event.keyCode = 0
        }
        return false;
    }
    return true;
}

/* ----------------------------------------------------------------
soCPF - Formata o campo no padrão de CPF ("999.999.999-99")
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
-----------------------------------------------------------------*/
function soCPF(e) {
    var keyNumber = (isIE) ? event.keyCode : e.which;
    if (((keyNumber < 48) || (keyNumber > 57)) && (keyNumber != "0") && (keyNumber != 8)) {
        if (isIE) {
            event.keyCode = 0
        }
        return false;
    }

    obj = (isNav) ? e.target : event.srcElement;

    if ((keyNumber != "0") && (keyNumber != 8)) {
        var i, nCount, sValue, fldLen, mskLen, bolMask, sCod, nTecla;

        objForm = document.forms[0];

        sMask = '999.999.999-99';

        sValue = obj.value;

        // Limpa todos os caracteres de formatação que já estiverem no campo.
        sValue = sValue.toString().replace("-", "");
        sValue = sValue.toString().replace("-", "");
        sValue = sValue.toString().replace(".", "");
        sValue = sValue.toString().replace(".", "");
        sValue = sValue.toString().replace("/", "");
        sValue = sValue.toString().replace("/", "");
        sValue = sValue.toString().replace("(", "");
        sValue = sValue.toString().replace("(", "");
        sValue = sValue.toString().replace(")", "");
        sValue = sValue.toString().replace(")", "");
        sValue = sValue.toString().replace(" ", "");
        sValue = sValue.toString().replace(" ", "");
        fldLen = sValue.length;
        mskLen = sMask.length;

        i = 0;
        nCount = 0;
        sCod = "";
        mskLen = fldLen;

        while (i <= mskLen) {
            bolMask = ((sMask.charAt(i) == "-") || (sMask.charAt(i) == ".") || (sMask.charAt(i) == "/"))
            bolMask = bolMask || ((sMask.charAt(i) == "(") || (sMask.charAt(i) == ")") || (sMask.charAt(i) == " "))

            if (bolMask) {
                sCod += sMask.charAt(i);
                mskLen++;
            }
            else {
                sCod += sValue.charAt(nCount);
                nCount++;
            }

            i++;
        }

        obj.value = sCod;
    }

    return true;
}

/* ----------------------------------------------------------------------------------
validarValores - Valida todos os campo de valor no formulario
Parametros:
objForm - formulario a ser varrido
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function validarValores(objForm) {
    var i, numEl;
    var bErro = false;
    var numEl = objForm.elements.length;

    for (i = 0; i < numEl; i++) {
        var prefixo = objForm.elements[i].name.substring(0, 3);

        if (prefixo == "vlr") {
            //Limpa espaços a direita e a esquerda
            objForm.elements[i].value = trim(objForm.elements[i].value);

            //verifica se tem algum caracter não numerico
            if (!verificarNumeros(objForm.elements[i].value))
                bErro = true;

            //compara o valor formatado com o valor do campo
            var sFormatado = formatarValor(objForm.elements[i].value);
            if (sFormatado != objForm.elements[i].value)
                bErro = true;
        }
    }

    if (bErro) {
        alert("Um ou mais valores informados estão incorretos.");
        return false;
    }

    return true;
}
/* ----------------------------------------------------------------------------------
trim - Limpa espaços a direita e a esquerda
Parametros:
value - valor a ser limpo
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function trim(value) {
    var temp = value;
    var obj = /^(\s*)([\W\w]*)(\b\s*$)/;
    if (obj.test(temp)) { temp = temp.replace(obj, '$2'); }
    var obj = / +/g;
    temp = temp.replace(obj, " ");
    if (temp == " ") { temp = ""; }
    return temp;
}
/* ----------------------------------------------------------------------------------
verificarNumeros - Valida se todos os caracteres são numericos
Parametros:
sValor - valor a ser verificado
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function verificarNumeros(sValor) {
    for (x = 0; x < sValor.length; x++) {
        if (isNaN(sValor.substring(x, x + 1)) || sValor.substring(x, x + 1) == " ")
            if (sValor.substring(x, x + 1) != "," && sValor.substring(x, x + 1) != ".")
                return false;
    }

    return true;
}

/* ----------------------------------------------------------------------------------
atribuirSiglaGrupo - Atribui valores aos campos hidden de sigla e grupo
Parametros:
sSigla - valor da sigla
sGrupo - valor do grupo
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function atribuirSiglaGrupo(sSigla, sGrupo) {
    document.forms[0].txt_Sigla.value = sSigla;
    document.forms[0].txt_GrupoCorrente.value = sGrupo;
}

function MostrarErroValidacao(strMensagem, objForm) {
    var obj = document.getElementById("QuadroErroValidacao").style.display = '';
    if (document.getElementById("colunaComunicacao1")) {
        document.getElementById("colunaComunicacao1").style.display = '';
    }
    if (document.getElementById("colunaComunicacao2")) {
        document.getElementById("colunaComunicacao2").style.display = '';
    }
    var obj1 = document.getElementById("mensagemErroValidacao");
    obj1.innerHTML = strMensagem;

    if (objForm != null) {
        if (objForm.type == "text" || objForm.type == "password") {
            objForm.className = 'erroForm';
        }
        else if (objForm.type == "select-one") {
            objForm.className = 'erroForm';
        }
    }
}

function ApagarErroValidacao(objForm) {
    var obj = document.getElementById("QuadroErroValidacao").style.display = 'none';
    if (document.getElementById("colunaComunicacao1")) {
        document.getElementById("colunaComunicacao1").style.display = 'none';
    }
    if (document.getElementById("colunaComunicacao2")) {
        document.getElementById("colunaComunicacao2").style.display = 'none';
    }

    if (objForm != null) {
        if (objForm.type == "text" || objForm.type == "password") {
            objForm.className = 'inputStyle';
        }
        else if (objForm.type == "select-one") {
            objForm.className = 'selectStyle';
        }
    }
}

function OcultarQuadroComunicacao(strTipoQuadro, strNomeQuadro) {
    switch (strTipoQuadro) {
        case "HorizontalSuperior":
            strNomeQuadro = "ACHS_" + strNomeQuadro;
            for (i = 0; i < arrayHorizontalSup.length; i++) {
                if (strNomeQuadro == arrayHorizontalSup[i]) {
                    if (document.getElementById(strNomeQuadro)) {
                        document.getElementById(strNomeQuadro).style.display = 'none';
                    }
                }
            }
            break;

        case "AC_HorizontalInferior":
            strNomeQuadro = "ACHI_" + strNomeQuadro;
            for (i = 0; i < arrayHorizontalInf.length; i++) {
                if (strNomeQuadro == arrayHorizontalInf[i]) {
                    if (document.getElementById(strNomeQuadro)) {
                        document.getElementById(strNomeQuadro).style.display = 'none';
                    }
                }
            }
            break;

        case "VerticalEsquerda":
            strNomeQuadro = "ACVE_" + strNomeQuadro;
            for (i = 0; i < arrayVerticalEsquerda.length; i++) {
                if (strNomeQuadro == arrayVerticalEsquerda[i]) {
                    if (document.getElementById(strNomeQuadro)) {
                        document.getElementById(strNomeQuadro).style.display = 'none';
                    }
                }
            }
            break;

        case "VerticalDireita":
            strNomeQuadro = "ACVD_" + strNomeQuadro;
            for (i = 0; i < arrayVerticalDireita.length; i++) {
                if (strNomeQuadro == arrayVerticalDireita[i]) {
                    if (document.getElementById(strNomeQuadro)) {
                        document.getElementById(strNomeQuadro).style.display = 'none';
                    }
                }
            }
            break;
    }
}

/* ----------------------------------------------------------------------------------
mostraDiv - Exibe Box de alerta
Parametros:
sBoxID - ID do DIV a ser exibido
imgHelp - Objeto imagem que está chamando a função(this)
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function mostraDiv(sBoxID, imgHelp, bAntes, bAbaixo) {
    var xTop = 10;
    var xLeft = 15;

    while (imgHelp) {
        xTop += imgHelp.offsetTop
        xLeft += imgHelp.offsetLeft
        imgHelp = imgHelp.offsetParent
    }

    if (bAntes)
        document.getElementById(sBoxID).style.left = xLeft - document.getElementById(sBoxID).offsetWidth - 18;
    else
        document.getElementById(sBoxID).style.left = xLeft;

    if (bAbaixo)
        document.getElementById(sBoxID).style.top = xTop - 8;
    else
        document.getElementById(sBoxID).style.top = xTop - document.getElementById(sBoxID).offsetHeight;

    document.getElementById(sBoxID).style.visibility = 'visible';
}

/* ----------------------------------------------------------------------------------
escondeDiv - Oculta Box de alerta
Parametros:
sBoxID - ID do DIV a ser exibido
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function escondeDiv(sBoxID) {
    document.getElementById(sBoxID).style.visibility = 'hidden';
}

function printFrame() {
    parent.frames["bankmain"].focus();
    parent.frames["bankmain"].print();
}

function escreveItemVariavel(id, valor) {
    var item = document.getElementById(id);
    if (!isIE)
        item.textContent = valor;
    else
        item.innerText = valor;
}

/* ----------------------------------------------------------------------------------
ValidarCPF - Valida o CPF informado
Parametros:
sValorCPF - CPF informado
Retornos:
true - CPF Ok
false - CPF invalido
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function ValidarCPF(sValorCPF) {
    sValorCPF = sValorCPF.replace(".", "");
    sValorCPF = sValorCPF.replace(".", "");
    sValorCPF = sValorCPF.replace("-", "");

    if (sValorCPF.length != 11)
        return false;

    if (sValorCPF == "11111111111" || sValorCPF == "22222222222" ||
			sValorCPF == "33333333333" || sValorCPF == "44444444444" ||
			sValorCPF == "55555555555" || sValorCPF == "66666666666" ||
			sValorCPF == "77777777777" || sValorCPF == "88888888888" ||
			sValorCPF == "99999999999" || sValorCPF == "00000000000" ||
			sValorCPF == "12345678909")
        return false;

    var intDigito1 = CalcularDigito(sValorCPF.substring(0, 9));
    var intDigito2 = CalcularDigito(sValorCPF.substring(0, 9) + intDigito1);
    return sValorCPF == (sValorCPF.substring(0, 9) + intDigito1 + intDigito2);
}

/* ----------------------------------------------------------------------------------
CalcularDigito - Calcula o digito verificador do CPF informado
Parametros:
strGrupo - Blocos do CPF
Retornos:
Bloco validado
Autor - Fernando Scofoni Cardoso
Data - 22/01/2008
------------------------------------------------------------------------------------*/
function CalcularDigito(strGrupo) {
    var arrPeso = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
    var intSoma = 0;
    var intDigito = 0;

    for (x = strGrupo.length - 1, intDigito; x >= 0; x--) {
        intDigito = parseInt(strGrupo.substring(x, x + 1));
        intSoma += intDigito * arrPeso[arrPeso.length - strGrupo.length + x];
    }

    intSoma = 11 - intSoma % 11;
    return intSoma > 9 ? 0 : intSoma;
}

