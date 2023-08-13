// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

/*********  View Index **************/
$(function () {
    $('ul.selec_ano li').click(function (e) {
        $("#muestra_ano").text("Año: " + this.id);

    });
});
$(function () {
    $('ul.selec_mes li').click(function (e) {
        $("#muestra_mes").text("Mes: " + this.id);

    });
});
function colocaPeriodo() {
    var objano = document.getElementById("muestra_ano");
    var valueano = objano.innerHTML.substring(5, 9);
    var objmes = document.getElementById("muestra_mes");
    var valuemes = objmes.innerHTML.substring(5, 7);
    if ((valueano + valuemes).length > 5) {
        $("#input_periodo").val(valueano + '-' + valuemes);
        var Objlabel = document.getElementById("label_periodo");
        Objlabel.style.display = 'none';
        var Objinput = document.getElementById("input_periodo");
        Objinput.style.display = '';
        $("#form_periodo").val(valueano + valuemes);
        $("#filtro_periodo").val(valueano + valuemes);
        $("#btn_filtrar").click();
    }
    else {
        alert("Porfavor seleccione Año y Mes !!");
        $("#btn_modal").click();
    }
}
function cambiaEstado() {
    var combo = document.getElementById("cbo_cambiaEst");
    var selected = combo.options[combo.selectedIndex].value;
    console.log(selected);
    $("#filtro_estado").val(selected);
}
function resetColorThead() {
    var celda = document.getElementById("th_Rco_numrco");
    celda.style.backgroundColor = "#87CEFA";
    var celda = document.getElementById("th_Rco_fec_registro");
    celda.style.backgroundColor = "#87CEFA";
    var celda = document.getElementById("th_Aux_nomaux");
    celda.style.backgroundColor = "#87CEFA";
    var celda = document.getElementById("th_Ung_deslar");
    celda.style.backgroundColor = "#87CEFA";
    var celda = document.getElementById("th_Cco_codcco");
    celda.style.backgroundColor = "#87CEFA";
    var celda = document.getElementById("th_Rco_sitrco");
    celda.style.backgroundColor = "#87CEFA";
    var celda = document.getElementById("th_Rco_priori");
    celda.style.backgroundColor = "#87CEFA";
}
function ordenRco_numrco() {
    $("#filtro_orden").val("1");
    resetColorThead();
    var celda = document.getElementById("th_Rco_numrco");
    celda.style.backgroundColor = "#0000FF";
    $("#btn_filtrar").click();
}
function ordenRco_fec_registro() {
    $("#filtro_orden").val("2");
    resetColorThead();
    var celda = document.getElementById("th_Rco_fec_registro");
    celda.style.backgroundColor = "#0000FF";
    $("#btn_filtrar").click();
}
function ordenAux_nomaux() {
    $("#filtro_orden").val("3");
    resetColorThead();
    var celda = document.getElementById("th_Aux_nomaux");
    celda.style.backgroundColor = "#0000FF";
    $("#btn_filtrar").click();
}
function ordenUng_deslar() {
    $("#filtro_orden").val("4");
    resetColorThead();
    var celda = document.getElementById("th_Ung_deslar");
    celda.style.backgroundColor = "#0000FF";
    $("#btn_filtrar").click();
}
function ordenCco_codcco() {
    $("#filtro_orden").val("5");
    resetColorThead();
    var celda = document.getElementById("th_Cco_codcco");
    celda.style.backgroundColor = "#0000FF";
    $("#btn_filtrar").click();
}
function ordenRco_sitrco() {
    $("#filtro_orden").val("6");
    resetColorThead();
    var celda = document.getElementById("th_Rco_sitrco");
    celda.style.backgroundColor = "#0000FF";
    $("#btn_filtrar").click();
}
function ordenRco_priori() {
    $("#filtro_orden").val("7");
    resetColorThead();
    var celda = document.getElementById("th_Rco_priori");
    celda.style.backgroundColor = "#0000FF";
    $("#btn_filtrar").click();
}
//Ejemplo de Jquery
$(document).on('click', '#btnSaveForm', function (event) {
    var title, input_value, data_id;
    $('.newClass').each(function (i, items_list) {
        title = $(this).find('.title').val();
        input_value = $(this).find('.input-value').val();
        data_id = $(this).find('.btn-warning').attr('data-id');
        console.log('Valores recogidos:');
        console.log('titulo: ' + title + ', valor: ' + input_value + ', data_id: ' + data_id);
    });
});
/*********  View Crear **************/
var contador = 0;
function agregarFila() { 
    //var nrows = parseInt(document.getElementById("item_total").value)+1;
    //var nColumnas = $("#mi-tabla tr:last td").length;
    contador++;
    var nrows = contador;
    $("#nitems_prd").val(nrows);
    console.log("tr totales "+nrows);
    var item = '00' + (nrows).toString();
    var citem = '<input id="pite' + nrows + '"   value="' + item + '" type="text" style="display:none" class="form-control"/>';
    var cod = ' <input  id="pcod' + nrows + '" value="1" type="text" style="display:none" class="form-control"/> <input style="width:100px" value="TXT0001" type="text"/>';
    var des = '<input   id="pdes' + nrows + '" type="text" class="form-control" style="width:250px" />';
    var det = '<button  id="btn_detalle" type="button" class="btn btn-outline-success"  data-bs-toggle="modal" data-bs-target="#ModalDetPrd'+nrows+'">+</button>' +
        ' <div class="modal" id="ModalDetPrd'+nrows+'"> ' +
        ' <div class="modal-dialog"> ' +
        ' <div class="modal-content"> ' +
        ' <div class="modal-header"> ' +
        '<h4 class="modal-title" > Especificaciones de producto </h4> ' +
        ' </div> ' +
        ' <div class="modal-body d-flex flex-column justify-content-center" > ' +
        ' <label>Producto: TXT0001 </label> ' +
        ' <textarea id="pesp' + nrows + '" class="col-5 mt-2 form-control" style="width:350px;height:200px" > </textarea> ' +
        ' </div> ' +
        ' <div class="modal-footer" > ' +
        ' <button onclick="" type = "button" class="btn btn-danger" data-bs-dismiss="modal" > Retornar </button> ' +
        ' </div> ' +
        ' </div> ' +
        ' </div> ' +
        ' </div> ';
    var uni = "UND";
    var cuni = '<input id="puni' + nrows + '" class="form-control"value="3" type="text" style="display:none"/>';
    var cant = '<input id="pcan' + nrows + '" class="form-control" type="text"  value="0" />';
    var codprv = '<input value="000001" type="text" style="width:80px;"/>';
    var prov = '<input id="prov' + nrows + '" class="form-control" type="text" placeholder="Proveedor" style="width:200px;"/>';
    var sust = '<input type="text" placeholder="Sustento.." />';
    var btn = '<button id="btn_elim' + nrows + '" onclick="ELiminaItemPrd()" class="btn btn-outline-danger" >--</button>';
    var fila = '<tr id="' + nrows + '"><td>' + btn + '</td><td id = "nitem_prd'+item+'">' + item + ' </td><td>' + citem +cod+ "</td>" +"<td>" + des + "</td>" +"<td>" + det + "</td>" +"<td>" + uni + cuni + "</td>" +"<td>" + cant + "</td>" +"<td>" + codprv + "</td><td>" + prov + "</td><td>" + sust + "</td></tr>";
    $('#tblProductos tbody').append(fila);
    $('#item_total').val(contador);

    var btn_elim = document.getElementById('btn_elim' + nrows);
    btn_elim.addEventListener('click', function () {
        var id_tr = this.id;
        id_tr = id_tr.replace('btn_elim', '');
        var tr_elim = document.getElementById(id_tr);
        $(tr_elim).remove();
        //agregamos el item eliminado al textbox
        var lista = document.getElementById('item_elim').value;
        if (lista == "0") {
            $("#item_elim").val(id_tr);
            console.log("nuevos valor : " + id_tr);
        } else {
            $("#item_elim").val(lista + id_tr);
            console.log("nuevos valores : " + lista + id_tr);
        }
        console.log("debe eliminar :" + id_tr);
    });
}
function colocaObjPrd() {
    for (i = 1; i < 10; i++) {
        document.getElementById('Ep_pite'+i).value = document.getElementById("pite"+i).value;
        document.getElementById('Ep_pcod'+i).value = document.getElementById("pcod"+i).value;
        document.getElementById('Ep_pdes'+i).value = document.getElementById("pdes"+i).value;
        document.getElementById('Ep_pesp'+i).value = document.getElementById("pesp"+i).value;
        document.getElementById('Ep_puni'+i).value = document.getElementById("puni"+i).value;
        document.getElementById('Ep_pcan'+i).value = document.getElementById("pcan"+i).value;
        document.getElementById('Ep_prov'+i).value = document.getElementById("prov"+i).value;
    }
}
function colocaObjPrdCond(numero, total) {
    for (i = numero; i < total; i++) {
        document.getElementById('Ep_pite' + i).value = document.getElementById("pite" + i).value;
        document.getElementById('Ep_pcod' + i).value = document.getElementById("pcod" + i).value;
        document.getElementById('Ep_pdes' + i).value = document.getElementById("pdes" + i).value;
        document.getElementById('Ep_pesp' + i).value = document.getElementById("pesp" + i).value;
        document.getElementById('Ep_puni' + i).value = document.getElementById("puni" + i).value;
        document.getElementById('Ep_pcan' + i).value = document.getElementById("pcan" + i).value;
        document.getElementById('Ep_prov' + i).value = document.getElementById("prov" + i).value;
    }
}
function colocaObjPrdIndep(numero) {
        let i = numero; 
        document.getElementById('Ep_pite' + i).value = document.getElementById("pite" + i).value;
        document.getElementById('Ep_pcod' + i).value = document.getElementById("pcod" + i).value;
        document.getElementById('Ep_pdes' + i).value = document.getElementById("pdes" + i).value;
        document.getElementById('Ep_pesp' + i).value = document.getElementById("pesp" + i).value;
        document.getElementById('Ep_puni' + i).value = document.getElementById("puni" + i).value;
        document.getElementById('Ep_pcan' + i).value = document.getElementById("pcan" + i).value;
        document.getElementById('Ep_prov' + i).value = document.getElementById("prov" + i).value;
}
/**** XX  */
function ELiminar() {
    $('tr#tr_prd').click(function (e) {
        let tr_data = $(this).text().trim();
        let nitem = tr_data.substring(2, 6).trim();

        var ntotal = document.getElementById('item_total').value;
        console.log("nitem : " + nitem + " ntotal : " + ntotal);
    });
}
function ELiminaItemPrd() { 
    $('tr#tr_prd').click(function (e) {
        let tr_data = $(this).text().trim();
        let nitem = tr_data.substring(2, 6).trim();

        var ntotal = document.getElementById('item_total').value;
        console.log("nitem : " + nitem + " ntotal : " + ntotal);

        switch (ntotal) {
            case  "1":
                return 1;
                break;
            case  "2":
                if (nitem == "001") {
                    document.getElementById('nitem_prd002').innerHTML = "001";
                    eliminaItem(1);
                } else (nitem == "002")
                    eliminaItem(2);
                break;
            case "3":
                switch (nitem) {
                    case "001":
                        eliminaItem(1);
                        subir23();
                        break;
                    case "002":
                        eliminaItem(2);
                        break;
                    case "003":
                        eliminaItem(3);
                        break;
                }
            case "4":
                switch (nitem) {
                    case "001":
                        eliminaItem(1);
                       
                        break;
                    case "002":
                        eliminaItem(2);
                        
                        break;
                    case "003":
                        eliminaItem(3);
                        $("#nitem_prd004").val("003");
                        break;
                    case "004":
                        eliminaItem(4);
                        break;
                }
                break;
            case "5":
                switch (nitem) {
                    case "001":
                        eliminaItem(1);
                        subir2_5();
                        break;
                    case "002":
                        eliminaItem(2);
                        subir3_5();
                        break;
                    case "003":
                        eliminaItem(3);
                        $("#nitem_prd004").val("003");
                        $("#nitem_prd005").val("004");
                        break;
                    case "004":
                        eliminaItem(4);
                        $("#nitem_prd005").val("004");
                        break;
                }
                break;
            default:
                console.log("default");
                break;
        }
    });     
}
function eliminaItem(nitem) {
    var ntotal = document.getElementById('item_total').value;
    let cant_tr = $('tr#tr_prd').length;
    $("#item_total").val(cant_tr);
    console.log("item capturado :" + ntotal + "  cambiado a :" + cant_tr)
    
    let item = ".elim00" + nitem.toString()
    $(item).remove();
    console.log("se elimino item " + nitem);
}
// caputa el total
//caputa el item a eliminar
// elimiino item
//subo los demas item 


function agregarFilaAdj() {
    var nrows = $("#tblAdjuntos tr").length;
    //var nColumnas = $("#mi-tabla tr:last td").length;
    $("#nitems_adj").val(nrows);
    console.log(nrows);
    var item = '00' + (nrows).toString();
    var name = '<input type="text" />';
    var file = ' <div class="form-group" style="width:300px"> ' +
        ' <div class="input-group d-flex flex-row" > ' +
        ' <label class="input-group-btn"> ' +
        ' <span class="btn btn-file"> ' +
        ' <input accept=".docx,.doc,.pdf" class="hidden" name="banner" type="file" id="banner" style="width:90px"> ' +
        ' </span> ' +
        ' </label> ' +
        ' <input class="form-control" id="banner_captura" readonly="readonly" name="banner_captura" type="text" value="" style="width:100px"> ' +
        ' </div> ' +
        ' </div > ';
    var codfile = '<input id="nomb_file" type="text" />';
    var fila = "<tr><td></td><td>" + item + "</td><td>" + name + "</td><td>" + file + "</td><td>" + codfile + "</td> </tr>";
    $('#tblAdjuntos tbody').append(fila);

    document.getElementById("cod_file1").readOnly = true;
    document.getElementById("est_file2").readOnly = true;
    document.getElementById("est_file1").readOnly = true;
    let cant_file_act = document.getElementById('cant_activefile').value;
    let cant_file_sig = (parseInt(cant_file_act) + 1).toString();
    var itemactivar = "#tr_file" + cant_file_sig;
    $(itemactivar).show();
    $(cant_activefile).val(cant_file_sig);
    console.log("Objeto Activado :" + itemactivar);
}
/*********************** */
$('#btn_adicionar_adj').on('click', function () {
    document.getElementById("cod_file2").readOnly = true;
    document.getElementById("cod_file1").readOnly = true;
    document.getElementById("est_file2").readOnly = true;
    document.getElementById("est_file1").readOnly = true;
    let cant_file_act = document.getElementById('cant_activefile').value;
    let cant_file_sig = (parseInt(cant_file_act) + 1).toString();
    var itemactivar = "#tr_file" + cant_file_sig;
    $(itemactivar).show();
    $(cant_activefile).val(cant_file_sig);
    console.log("Objeto Activado :" + itemactivar);
});

function validaForm(){
    let motivo = document.getElementById('txa_motivo').value;
    if (motivo.length == 0) {
        alert("Ingrese un motivo !!");
    } else {
        let pry = document.getElementById('input_cod_pry').value;
        if (pry.length == 0) {
            alert("Seleccione un Proyecto !! ");
        } else {
            $('#Modal_Conf').modal('show');
            var total = document.getElementById('item_total').value;
            
            let lista = document.getElementById('item_elim').value;
            if (lista == "0") {
                colocaObjPrd();
            } else {
                let len = lista.length;
                switch (len) {
                    case 1:
                        if (lista == total) {
                            colocaObjPrdCond(1, total - 1);
                            console.log("Ingreso a esta opcion case 1 item == total");
                        } else {
                            colocaObjPrdCond(1, lista - 1);
                            colocaObjPrdCond(lista + 1, total);
                            console.log("Se actualizo la funcion colocaObjPrdCond() con 1 valores eliminados");
                        }
                        break;
                    case 2:
                        let v1 = lista.substring(0, 1);
                        let v2 = lista.substring(1, 2);
                        if (v1 == total) {
                            colocaObjPrdCond(1, v1-1);
                        } else {
                            colocaObjPrdCond(1, total - 1);
                            colocaObjPrdCond(v1 + 1, total);
                        }
                        if (v2 == total) {
                            colocaObjPrdCond(1, v2 - 1);
                        } else {
                            colocaObjPrdCond(1, total - 1);
                            colocaObjPrdCond(v2 + 1, total);
                        }
                        console.log("Se actualizo la funcion colocaObjPrdCond() con 2 valores eliminados");
                        break;
                    case 3:
                        v1 = lista.substring(0, 1);
                        v2 = lista.substring(1, 2);
                        let v3 = lista.substring(2, 3);
                        colocaObjPrdCond(v1+1,total);
                        colocaObjPrdCond(v2+1,total);
                        colocaObjPrdCond(v3+1,total);
                        console.log("Se actualizo la funcion colocaoBjPrdCond() con 3 valores eliminados");
                        break;
                    case 4:
                        v1 = lista.substring(0, 1);
                        v2 = lista.substring(1, 2);
                        v3 = lista.substring(2, 3);
                        let v4 = lista.substring(3, 4);
                        colocaObjPrdCond(v1+1,total);
                        colocaObjPrdCond(v2+1,total);
                        colocaObjPrdCond(v3+1,total);
                        colocaObjPrdCond(v4+1,total);
                        console.log("Se actualizo la funcion colocaoBjPrdCond() con 4 valores eliminados");
                        break;
                    case 5:
                        v1 = lista.substring(0, 1);
                        v2 = lista.substring(1, 2);
                        v3 = lista.substring(2, 3);
                        v4 = lista.substring(3, 4);
                        let v5 = lista.substring(4, 5);
                        colocaObjPrdCond(v1+1,total);
                        colocaObjPrdCond(v2+1,total);
                        colocaObjPrdCond(v3+1,total);
                        colocaObjPrdCond(v4+1,total);
                        colocaObjPrdCond(v5+1,total);
                        console.log("Se actualizo la funcion colocaoBjPrdCond() con 5 valores eliminados");
                        break;
                    default:
                        console.log("NO INGRESO A NINGUNA OPCION DEL SWITCH");
                }
            }
            colocaObjPrdCond(0, v1);  
        }   
    }
    //data-bs-toggle="modal" data-bs-target="#Modal_Conf"
}
function pruebaELiminar() {
    var total = document.getElementById('item_total').value;
    let lista = document.getElementById('item_elim').value;
    if (lista == total) {
        let v1 = lista.substring(0, 1);
        let v2 = lista.substring(1, 2);
        colocaObjPrdCond(1, total - 1);
        alert("total tr : " + total + " items elimi : " + lista + " -var1 :" + v1 + " -var2 :" + v2);
    }
    
}
function confirmaRegistro() {
    let motivo = document.getElementById('txa_motivo').value;
    $("#input_motivo").val(motivo);
    $("#btn_registrar").click();
}

/********************* */
// MOSTRAR ARCHIVO BASE64 A PDF EN VIEW Evaluacion
function MostrarArchivo001() {
    var data = document.getElementById('Fileb64001').value;
    //var nomfilO = document.getElementById('nomb_file001').value;
    var nomfilC = document.getElementById('Fileext001').value.toString().trim();
    let extension = 'pdf';
    let next = 0;
    console.log(nomfilC);
    if (nomfilC.lastIndexOf('.') > 0) {
        next = nomfilC.lastIndexOf('.')
        extension = nomfilC.substring(next + 1);
    }
    const blob = this.dataURItoBlob(data, extension);
    const url = URL.createObjectURL(blob);
    validaExtension(data, nomfilC, extension, url);
}
function validaExtension(data, nomfilC, extension, url) {
    switch (extension) {
        case "pdf":
            console.log("is pdf");
            window.open(url, '_blank');
            break;
        case "doc":
            extension = "msword";
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "docx":
            extension = "msword";
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "xls":
            extension = "vnd.ms - excel";
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "xlsx":
            extension = "vnd.ms - excel";
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "ppt":
            extension = "vnd.ms-powerpoint";
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "pptx":
            extension = "vnd.ms-powerpoint";
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "jpeg":
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "jpg":
            extension = "jpeg";
            createAndDownloadFile(data, nomfilC, extension);
            break;
        case "jpg":
            extension = "jpeg";
            createAndDownloadFile(data, nomfilC, extension);
            break;
    }
}
function MostrarArchivo002() {
    var data = document.getElementById('Fileb64002').value;
    var nomfilO = document.getElementById('nomb_file002').value;
    var nomfilC = document.getElementById('Fileext002').value.toString().trim();
    let extension;
    let next = 0;
    console.log(nomfilC);
    if (nomfilC.lastIndexOf('.') > 0) {
        next = nomfilC.lastIndexOf('.')
        extension = nomfilC.substring(next + 1);
    }
    const blob = this.dataURItoBlob(data,extension);
    const url = URL.createObjectURL(blob);
    validaExtension(data, nomfilC, extension, url);
}
function MostrarArchivo003() {
    var data = document.getElementById('Fileb64003').value;
    var nomfilO = document.getElementById('nomb_file003').value;
    var nomfilC = document.getElementById('Fileext003').value.toString().trim();
    let extension;
    let next = 0;
    console.log(nomfilC);
    if (nomfilC.lastIndexOf('.') > 0) {
        next = nomfilC.lastIndexOf('.')
        extension = nomfilC.substring(next + 1);
    }
    const blob = this.dataURItoBlob(data, extension);
    const url = URL.createObjectURL(blob);
    validaExtension(data, nomfilC, extension, url);
}
function MostrarArchivo004() {
    var data = document.getElementById('Fileb64004').value;
    var nomfilO = document.getElementById('nomb_file004').value;
    var nomfilC = document.getElementById('Fileext004').value.toString().trim();
    let extension;
    let next = 0;
    console.log(nomfilC);
    if (nomfilC.lastIndexOf('.') > 0) {
        next = nomfilC.lastIndexOf('.')
        extension = nomfilC.substring(next + 1);
    }
    const blob = this.dataURItoBlob(data, extension);
    const url = URL.createObjectURL(blob);
    validaExtension(data, nomfilC, extension, url);
}
function MostrarArchivo005() {
    var data = document.getElementById('Fileb64005').value;
    var nomfilO = document.getElementById('nomb_file005').value;
    var nomfilC = document.getElementById('Fileext005').value.toString().trim();
    let extension;
    let next = 0;
    console.log(nomfilC);
    if (nomfilC.lastIndexOf('.') > 0) {
        next = nomfilC.lastIndexOf('.')
        extension = nomfilC.substring(next + 1);
    }
    const blob = this.dataURItoBlob(data, extension);
    const url = URL.createObjectURL(blob);
    validaExtension(data, nomfilC, extension, url);
}
function dataURItoBlob(dataURI, extension) {
    console.log(extension);
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
        int8Array[i] = byteString.charCodeAt(i);
    }
    console.log('application/' + extension);
    var etiqueta = 'application/' + extension
    if (extension.trim() == 'xlsx') {
        etiqueta = 'application / vnd.ms - excel';
    }
    console.log(etiqueta);
    const blob = new Blob([int8Array], { type: etiqueta });
    return blob;
}

function createAndDownloadFile(content, filename, type) {
    var etiqueta = 'application/' + type
    //if (type.trim() == 'xlsx') {
    //    etiqueta = 'application / vnd.ms - excel';
    //}
    const byteString = window.atob(content);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
        int8Array[i] = byteString.charCodeAt(i);
    }
    console.log(etiqueta);
    const file = new Blob([int8Array], { type: etiqueta });

    if (window.navigator.msSaveOrOpenBlob) {
        // IE10+
        window.navigator.msSaveOrOpenBlob(file, filename);
    } else {
        // Others
        const a = document.createElement('a');
        const url = URL.createObjectURL(file);
        a.href = url;
        a.download = filename;
        document.body.appendChild(a);
        a.click();

        setTimeout(function () {
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);
        }, 0);
    }
}

//Agregar Items a Array
$("#btn_registrar").on("click", function () {
    var DetalleReq = [];
    var total = 0;
    $("#tblProductos > tbody > tr").each(function (index, tr) {
        DetalleReq.push(
            {
                Codigo: $(this).find("td:nth-child(3)").html().substring(14, 26),
                //$(tr).find('input').eq(2).val() ,
                Descri: $(tr).find('td:eq(3)').html(),
                Glosa: $(tr).find('td:eq(4)').text(),
                Unidad: $(tr).find('td:eq(5)').text(),
                Cantidad: parseInt($(tr).find('input').eq(6).val()),
                CodProv: $(tr).find('input').eq(7).val(),
                Nombprov: $(tr).find('input').eq(8).val()
            });
        //console.log(DetalleReq);
    })
})

//Carga de archivo 1
const blobToBase64 = (blob) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(blob);
        reader.onloadend = () => {
            resolve(reader.result.split(',')[1]);
        };
    });
};
const b64ToBlob = async (b64, type) => {
    const blob = await fetch(`data:${type};base64,${b64}`);
    return blob;
};

const fileInput = document.querySelector('#fileInput');
const btnTob64 = document.querySelector('#btn_carga1');

btnTob64.addEventListener('click', async (e) => {
    console.log(btnTob64.innerText);
    console.log('Convirtiendo mi blob');
    const myBlob = fileInput.files[0];
    const myB64 = await blobToBase64(myBlob);

    $('#nomb_file1').val(fileInput.files[0].name);
    document.getElementById('b64string1').value = myB64;
    ArchivoCargadoExito(1);
    console.log(myB64);
  
});

//Carga de archivo 2
const fileInput2 = document.querySelector('#fileInput2');
const btn2 = document.querySelector('#btn_carga2');

btn2.addEventListener('click', async (e) => {
    console.log('Convirtiendo archivo 2');
    const myBlob = fileInput2.files[0];
    const myB64 = await blobToBase64(myBlob);

    $('#nomb_file2').val(fileInput2.files[0].name);  
    document.getElementById('b64string2').value = myB64;
    ArchivoCargadoExito(2);
    console.log(myB64);
    
});

function ArchivoCargadoExito(numero) {
    const fecha = new Date();

    let nb64string1 = document.getElementById('b64string' + numero).value;
    if (nb64string1.length > 9) {
        let estado = document.getElementById('est_file' + numero);
        estado.value = "CARGADO";
        estado.style.backgroundColor = "#0FB607";

        let nombfile2 = document.getElementById('nomb_file' + numero).value;
        let extension = nombfile2.split('.').pop().toLowerCase();
        $("#cod_file" + numero).val("REQCOM_" + fecha.getFullYear() + "00" + numero + "." + extension);
    } else {
        alert("Archivo invalido. ");
    }
}

const fileInput3 = document.querySelector('#fileInput3');
const btn3 = document.querySelector('#btn_carga3');
btn3.addEventListener('click', async (e) => {
    console.log('Convirtiendo archivo 3');
    const myBlob = fileInput3.files[0];
    const myB64 = await blobToBase64(myBlob);

    $('#nomb_file3').val(fileInput3.files[0].name);
    document.getElementById('b64string3').value = myB64;
    ArchivoCargadoExito(3);
    console.log(myB64);

});

const fileInput4 = document.querySelector('#fileInput4');
const btn4 = document.querySelector('#btn_carga4');
btn4.addEventListener('click', async (e) => {
    console.log('Convirtiendo archivo 4');
    const myBlob = fileInput4.files[0];
    const myB64 = await blobToBase64(myBlob);

    $('#nomb_file4').val(fileInput4.files[0].name);
    document.getElementById('b64string4').value = myB64;
    ArchivoCargadoExito(4);
    console.log(myB64);
});

const fileInput5 = document.querySelector('#fileInput5');
const btn5 = document.querySelector('#btn_carga5');
btn5.addEventListener('click', async (e) => {
    console.log('Convirtiendo archivo 5');
    const myBlob = fileInput5.files[0];
    const myB64 = await blobToBase64(myBlob);

    $('#nomb_file5').val(fileInput5.files[0].name);
    document.getElementById('b64string5').value = myB64;
    ArchivoCargadoExito(5);
    console.log(myB64);
});


let act = document.getElementById('btn_activa_registrar');

act.addEventListener('click', function () {
    $("#btn_registrar").click();
});


//Carga los valores automaticamente
$(document).ready(function () {
    colocaEstado();
    colocaTipo();
    colocaPrioridad();
    //colocaUnegocio();
    colocaSituacion();
    colocaPresup();
    colocaReembls();
    colocaEmpresa();
    colocaFecha();
});
function colocaFecha() {
    let value = document.getElementById("envia_fecsug").value;
    let fecsug = value.substring(0, 10) + " " + value.substring(11, 19);
    $("#input_fecsug").val(fecsug);
    console.log(fecsug);
}
function colocaEstado() {
    var combo = document.getElementById("cbo_estado");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_estado").val(parseInt(selected));
}
function colocaTipo() {
    var combo = document.getElementById("cbo_tipo");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_tipo").val(selected);
}
function colocaPrioridad() {
    var combo = document.getElementById("cbo_prioridad");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_prioridad").val(selected);
}
/*
function colocaUnegocio() {
    var combo = document.getElementById("cbo_unegocio");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_unegocio").val(selected);
}*/
function colocaSituacion() {
    var combo = document.getElementById("cbo_situacion");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_situacion").val(selected);
}
function colocaPresup() {
    var combo = document.getElementById("cbo_presup");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_presup").val(selected);
}
function colocaReembls() {
    var combo = document.getElementById("cbo_reembls");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_reembls").val(selected);
}
function colocaEmpresa() {
    var combo = document.getElementById("cbo_empresa");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_cia").val(selected);
}

//JQuery para Ayuda Disciplina
$(document).ready(function () {
    $('tr#tr_dis').click(function (e) {
        var tr_data = $(this).text().trim();
        var cod = tr_data.substring(0, 2);
        var des = tr_data.substring(2, tr_data.length).trim();
        console.log("-Codigo:" + cod + "-Desc:" + des);
        $("#input_cod_disci").val(parseInt(cod,10));
        $("#input_des_disci").val(des);
        //Limpiar la busqueda actual
        //$("#busqueda_ayuda_dis").val('');
        $("#btn_cerrar_modal_disci").click();
    });
});
function abrir_modal_disci() {
    $("#btn_abrir_modal_disci").click();
}
//JQuery para Ayuda Centro de Costo
$(document).ready(function () {
    $('tr#tr_cco').click(function (e) {
        var tr_data = $(this).text().trim();
        var epk = tr_data.substring(0,2);
        var cod = tr_data.substring(11,22);
        var des = tr_data.substring(22,tr_data.length).trim();
        console.log("-EPK :" + epk + "-Codigo:" + cod + " -Desc:" + des + "  Leng" + tr_data.length);
        
        $("#input_epk_cco").val(parseInt(epk));
        $("#input_cod_cco").val(cod);
        $("#input_des_cco").val(des);
        $("#btn_cerrar_modal_cco").click();
    });
});
function abrir_modal_cco() {
    $("#btn_abrir_modal_cco").click();
}
//JQuery para Ayuda Usuario
$(document).ready(function () {
    $('tr#tr_usu').click(function (e) {
        let tr_data = $(this).text().trim();
        let epk = tr_data.substring(0, 3).trim();
        let cod = tr_data.substring(3, 18).trim();
        let des = tr_data.substring(18, tr_data.length).trim();
        console.log("Epk : " + epk + " -Codigo:" + cod + " -Desc: " + des + " Leng " + tr_data.length);

        $("#input_epk_usu").val(epk);
        $("#input_cod_usu").val(cod);
        $("#input_des_usu").val(des);
        $("#btn_cerrar_modal_usu").click();
    });
});
function abrir_modal_usu() {
    event.preventDefault();
    $("#btn_abrir_modal_usu").click();
}
//JQuery para Ayuda Proyecto
$(document).ready(function () {
    $('tr#tr_pry').click(function (e) {
        let tr_data = $(this).text().trim();

        let pry_codepk = tr_data.substring(0, 4).trim();
        let pry_codpry = tr_data.substring(10, 25).trim();
        let pry_deslar = tr_data.substring(25, 100).trim();

        let apr_codepk = tr_data.substring(190, 200).trim();
        let apr_codemp = tr_data.substring(200, 210).trim();
        let apr_deslar = tr_data.substring(215, 242).trim();

        let are_codepk = tr_data.substring(250, 253).trim();
        let are_codare = tr_data.substring(259, 268).trim();
        let are_nomlar = tr_data.substring(268, 303).trim();

        let dis_codepk = tr_data.substring(308, 317).trim();
        let dis_coddis = tr_data.substring(319, 325).trim();
        let dis_deslar = tr_data.substring(325, 360).trim();

        let ung_codepk = tr_data.substring(360, 367).trim();
        let ung_codung = tr_data.substring(367, 375).trim();
        let ung_deslar = tr_data.substring(375, 403).trim();

        let cco_codepk = tr_data.substring(403, 450).trim();
       
        console.log(tr_data);
        console.log(
            " *epk_pry " + pry_codepk + " *cod_pry " + pry_codpry + " *des_pry " + pry_deslar +
            " *epk_apr " + apr_codepk + " *cod_apr " + apr_codemp + " *des_apr " + apr_deslar +
            " *epk_are " + are_codepk +" *cod_are " + are_codare + " *des_are " + are_nomlar +
            " *epk_dis " + dis_codepk + " *cod_dis " + dis_coddis + " *des_dis " + dis_deslar +
            " *epk_ung " + ung_codepk + " *cod_ung " + ung_codung + " *des_ung " + ung_deslar +
            " *epk_cco " + cco_codepk +
            " ****Len " + tr_data.length
        );

        $("#input_epk_pry").val(pry_codepk);
        $("#input_cod_pry").val(pry_codpry);
        $("#input_des_pry").val(pry_deslar);
        
        $("#input_epk_apr").val(apr_codepk);
        $("#input_cod_apr").val(apr_codemp);
        $("#input_des_apr").val(apr_deslar);

        $("#input_epk_are").val(are_codepk);
        $("#input_cod_are").val(are_codare);
        $("#input_des_are").val(are_nomlar);

        $("#input_epk_dis").val(dis_codepk);
        $("#input_cod_dis").val(dis_coddis);
        $("#input_des_dis").val(dis_deslar);

        $("#input_epk_ung").val(ung_codepk);
        $("#input_cod_ung").val(ung_codung);
        $("#input_des_ung").val(ung_deslar);

        $("#input_epk_cco").val(cco_codepk);
        $("#btn_cerrar_modal_pry").click();
    });
});

function abrir_modal_pry() {
    $("#btn_abrir_modal_pry").click();
}
//JQUERY Para subir archivo
$(document).on('change', '.btn-file :file', function () {
    var input = $(this);
    var numFiles = input.get(0).files ? input.get(0).files.length : 1;
    var label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    input.trigger('fileselect', [numFiles, label]);
});
$(document).ready(function () {
    $('.btn-file :file').on('fileselect', function (event, numFiles, label) {
        var input = $(this).parents('.input-group').find(':text');
        var log = numFiles > 1 ? numFiles + ' files selected' : label;
        if (input.length) { input.val(log); } else { if (log) alert(log); }
    });
});

function CargaArchivo(e) {
    //console.log(archivo.files);
    e.preventDefault( );

    const FD = new FormData();
    const listado_archivos = e.target.id =='archivos' ? 
                                    archivo.file : 
                                    e.dataTransfer.files;

    for (let file of listado_archivos) {
        FD.append('files[]', file);
    }
    fetch('https://localhost:7218/Files1/Upload', { method: 'POST', body: FD }).
        then(rta => { rta.json }).
        then(json => {
            console.log(json);
        }).
        catch( e => { console.error(e); });

        archivo.value = '';
}


function coloca_nomb() {
    var combo = document.getElementById("banner_captura");
    var selected = combo.innerHTML;
    $("#nomb_file").val(selected);
}

/*window.onload = function() {  
    var obj = document.getElementById("input_prioridad");
    var valor = obj.innerHTML;
     document.getElementById("cbo_prioridad").value = valor;
    console.log(valor+"LOngitud: "+valor.length);
};*/

// Busqueda con JS 
(function (document) {
    'use strict';
    var LightTableFilter = (function (Arr) {

        var _input;

        function _onInputEvent(e) {
            _input = e.target;
            var tables = document.getElementsByClassName(_input.getAttribute('data-table'));
            Arr.forEach.call(tables, function (table) {
                Arr.forEach.call(table.tBodies, function (tbody) {
                    Arr.forEach.call(tbody.rows, _filter);
                });
            });
        }

        function _filter(row) {
            var text = row.textContent.toLowerCase(), val = _input.value.toLowerCase();
            row.style.display = text.indexOf(val) === -1 ? 'none' : 'table-row';
        }

        return {
            init: function () {
                var inputs = document.getElementsByClassName('light-table-filter');
                Arr.forEach.call(inputs, function (input) {
                    input.oninput = _onInputEvent;
                });
            }
        };
    })(Array.prototype);

    document.addEventListener('readystatechange', function () {
        if (document.readyState === 'complete') {
            LightTableFilter.init();
        }
    });

})(document);
//https://www.tutofox.com/javascript/buscador-datos-en-la-tabla-con-javascript/#:~:text=El%20buscador%20esta%20hecho%20en,que%20desea%20buscar%20el%20registro.&text=El%20campo%20de%20buscador%20debes,en%20el%20input%20de%20buscador.

//Aprobacion OC


//Evaluacion Req
//Funcion para validar el input y enviar el rechazo del Req
function validaRechazo() {
    var mot = document.getElementById("mot_rechazo_req").value;
    console.log(mot);
    if (mot.length == 0) {
        alert("¡Ingrese un motivo de RECHAZO!")
    } 
    console.log("Longitud : " + mot.length);
    $("#btn_envia_rechazo_req").click();
}
function validaDevolucion() {
    var mot = document.getElementById("mot_devolucion_req").value;
    console.log(mot);
    if (mot.length == 0) {
        alert("¡Ingrese un motivo de DEVOLUCION!")
    }
    console.log("Longitud : " + mot.length);
    $("#btn_envia_devolucion_req").click();
}
function btn_abrir_modal_rechazo() {  
    $("#btn_abrir_modal_rechazo").click();
}
function btn_abrir_modal_devolver() {
    $("#btn_abrir_modal_devolver").click();
}
/*
 let inputValue = document.getElementById("domTextElement").value; 
  document.getElementById("valueInput").innerHTML = inputValue; 
 
 */







