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
function agregarFila() { 
    var nrows = $("#tblProductos tr").length;
    //var nColumnas = $("#mi-tabla tr:last td").length;
    $("#nitems_prd").val(nrows);
    console.log(nrows);
    var item = '00' + (nrows).toString();
    var citem = '<input id="input_DPrd_item"   value="'+item+'" type="text" style="display:none" class="form-control"/>';
    var cod = ' <input  id="input_DPrd_cod" value="2" type="text" style="display:none" class="form-control"/> <input value="999900000018" type="text"/>';
    var des = '<input   id="input_DPrd_descri" type="text" class="form-control"/>';
    var det = '<button  id="btn_detalle" type="button" class="btn btn-outline-success"  data-bs-toggle="modal" data-bs-target="#ModalDetPrd'+nrows+'">+</button>' +
        ' <div class="modal" id="ModalDetPrd'+nrows+'"> ' +
        ' <div class="modal-dialog"> ' +
        ' <div class="modal-content"> ' +
        ' <div class="modal-header"> ' +
        '<h4 class="modal-title" > Especificaciones de producto </h4> ' +
        ' </div> ' +
        ' <div class="modal-body d-flex flex-column justify-content-center" > ' +
        ' <label>Producto: 999900000018 </label> ' +
        ' <textarea id="input_DPrd_glosa" class="col-5 mt-2 form-control" style="width:350px;height:200px" > </textarea> ' +
        ' </div> ' +
        ' <div class="modal-footer" > ' +
        ' <button onclick="" type = "button" class="btn btn-danger" data-bs-dismiss="modal" > Retornar </button> ' +
        ' </div> ' +
        ' </div> ' +
        ' </div> ' +
        ' </div> ';
    var uni  = "UND";
    var cuni = '<input id="input_DPrd_unidad" class="form-control"value="3" type="text" style="display:none"/>';
    var cant = '<input id="input_DPrd_cantidad" class="form-control" type="text"  value="0.0" />';
    var codprv = '<input id="input_DPrd_codprov" class="form-control" value="000001" type="text"/>';
    var prov = '<input type="text" placeholder="Proveedor" />';
    var sust = '<input type="text" placeholder="Sustento.." />';
    var fila = "<tr><td></td><td>" + item+citem + "</td><td>" + cod + "</td><td>" + des + "</td><td>" + det + "</td><td>" + uni +cuni+ "</td><td>" + cant + "</td><td>" + codprv + "</td><td>" + prov + "</td><td>" + sust + "</td> </tr>";
    $('#tblProductos tbody').append(fila);  
}
function colocaObjPrd() {
    document.getElementById("DPrd_item").value = document.getElementById("input_DPrd_item").value; 
    document.getElementById("DPrd_descri").value = document.getElementById("input_DPrd_descri").value; 
    document.getElementById("DPrd_cod").value = document.getElementById("input_DPrd_cod").value; 
    document.getElementById("DPrd_glosa").value = document.getElementById("input_DPrd_glosa").value; 
    document.getElementById("DPrd_unidad").value = document.getElementById("input_DPrd_unidad").value; 
    document.getElementById("DPrd_cantidad").value = document.getElementById("input_DPrd_cantidad").value; 
    document.getElementById("DPrd_codprov").value = document.getElementById("input_DPrd_codprov").value; 
    console.log("Event Onfocus");
}

function RegistraDetalle() {
    var url = '@Url.Action("DetReq", "RQCompra")';
    $.ajax({
        cache: false,
        async: true,
        type: "POST",
        url: url,
        data: {},
        success: function (response) {
            $('#resultado').html('');
            $('#resultado').html(response);
        }
    });
}

//Agregar Items a Array
$("#btn_registrar").on("click",function()
{
    var DetalleReq = [];
    var total = 0;
    $("#tblProductos > tbody > tr").each(function(index,tr){
       DetalleReq.push(
         {
           Codigo:$(this).find("td:nth-child(3)").html().substring(14,26),
           //$(tr).find('input').eq(2).val() ,
           Descri:$(tr).find('td:eq(3)').html(),
           Glosa :$(tr).find('td:eq(4)').text(),
           Unidad:$(tr).find('td:eq(5)').text(),
           Cantidad:parseInt($(tr).find('input').eq(6).val()),
           CodProv:  $(tr).find('input').eq(7).val(),
           Nombprov: $(tr).find('input').eq(8).val()
         });
       //console.log(DetalleReq);
    })
})

function agregarFilaAdj() {
    var nrows = $("#tblAdjuntos tr").length;
    //var nColumnas = $("#mi-tabla tr:last td").length;
    $("#nitems_adj").val(nrows);
    console.log(nrows);
    var item = '00' + (nrows).toString();
    var name = '<input type="text" />';
    var file = ' <div class="form-group" style="width:300px"> '+
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
}
$('#btn_adicionar_adj').on('click', function () {
    let cant_file_act = document.getElementById('cant_activefile').value;
    let cant_file_sig = (parseInt(cant_file_act)+1).toString();
    var itemactivar = "#tr_file"+cant_file_sig;
    $(itemactivar).show();
    $(cant_activefile).val(cant_file_sig);
    console.log("Objeto Activado :"+itemactivar);
});

const dropArea = document.getElementById("drop-area1");
const dragText = dropArea.querySelector("p");
const button = dropArea.querySelector('button');
const input = dropArea.querySelector("#input-file");
let files;
//Eventos
button.addEventListener("click", (e) => {
    input.click();
});
input.addEventListener("change", (e) => {
    files = this.files;
    dropArea.classList.add("active");
    showFiles(files);
    dropArea.classList.remove("active");
});
//Iteracciones con la pagina y subida de archivos
//cuadro
dropArea.addEventListener("dragover", (e) => {
    e.preventDefault();
    dropArea.classList.add("active");
    dragText.textContent = "Suelta aqui para subir archivo";
});
//arrastrar archivo
dropArea.addEventListener("dragleave", (e) => {
    e.preventDefault();
    dropArea.classList.remove("active");
    dragText.textContent = "Archivo Cargado";
});
//soltar archivo
dropArea.addEventListener("drop", (e) => {
    e.preventDefault();
    files = e.dataTransfer.files;
    showFiles(files);
    dropArea.classList.remove("active");
    //dragText.textContent = "Arrastra y suelta archivo";
});

function showFiles(files) {
    if (files.length === undefined) {
        processFile(files);
    } else {
        for (const file of files) {
            processFile(file);
        }
    }
}
const blobToBase64 = (blob) => {
    return new Promise( (resolve, reject) =>{
        const reader = new FileReader();
        reader.readAsDataURL(blob);
        reader.onloadend = () => {
            resolve(reader.result.split(',')[1]);
        };
    });
};
const b64ToBlob = async(b64, type)=>{
    const blob = await fetch(`data:${type};base64,${b64}`);
    return blob;
};
//Cargar el archivo de forma iteractiva con innerHTML
function processFile(file) {
        
    const fileReader = new FileReader();
    fileReader.addEventListener('load', async(e) => {
        //Convierte Archivo en B64
        const myB64 = await blobToBase64(file);
        
        document.getElementById('b64string1').value = myB64;
        ArchivoCargadoExito();
        console.log(myB64);
        document.querySelector('#preview').innerHTML = image ;
    });

    fileReader.readAsDataURL(file);
}
function ArchivoCargadoExito(){
    let nb64string1 = document.getElementById('b64string1').value;
    if(nb64string1.length >0){
        let estado = document.getElementById('est_file1');
         estado.value="CARGADO";
         estado.style.backgroundColor="#0FB607";
        let Rco_numero = document.getElementById('sRco_numero').value; 
         $("#cod_file1").val("REQCOM_"+Rco_numero+"001.pdf");
         dragText.textContent = "Archivo Cargado con exito !!";
    }

}

/*********************** */
const fileInput = document.querySelector('#fileInput');
const btnTob64 = document.querySelector('#btnTob64');
const btnToBlob = document.querySelector('#btnToBlob');

btnTob64.addEventListener('click', async (e) => {
    console.log(btnTob64.innerText);
    console.log('Convirtiendo mi blob');
    const myBlob = fileInput.files[0];
    const myB64 = await blobToBase64(myBlob);
    document.getElementById('input_Base64String').value = myB64;
    console.log(myB64);
});

btnToBlob.addEventListener('click', async (e) => {
    console.log(btnToBlob.innerText);
});
/*********************************** */

//Carga los valores automaticamente
$(document).ready(function () {
    colocaEstado();
    colocaTipo();
    colocaPrioridad();
    colocaUnegocio();
    colocaSituacion();
    colocaPresup();
    colocaReembls();
});
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
function colocaUnegocio() {
    var combo = document.getElementById("cbo_unegocio");
    var selected = combo.options[combo.selectedIndex].value;
    $("#input_unegocio").val(selected);
}
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
function AyudaDisciplina1() {
    $.ajax({
        ache: false,
        async: true,
        type: "GET",
        url: '/RQCompra/AyudaDisciplina',
        data: {},
        datatype: "html",
        type: 'POST',
        success: function (result) {
            $('#Modal_Dis').html();
            $('#Modal_Dis').html(result);
        }
    })
}
function AyudaDisciplina2() {
    var laURLDeLaVista = '@Url.Action("AyudaDisciplina", "RQCompra")';
    $.ajax({
        cache: false,
        async: true,
        type: "GET",
        url: laURLDeLaVista,
        data: {},
        success: function (response) {
            $('#resultado').html('');
            $('#resultado').html(response);
        }
    });
}
// AyudaDisciplina 3 con JQuery
$('btn_showModal').on('click', function () {
    $("#div_modal").dialog({
        autoOpen: true,
        position: { my: "center", at: "top+350", of: window },
        width: 1000,
        resizable: false,
        title: 'Add User Form',
        modal: true,
        open: function () {
            $(this).load('@Url.Action("AyudaDisciplina", "RQCompra")');
        }
    });
    return false;
});
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
        //Limpiar la busqueda actual
        //$("#busqueda_ayuda_cco").val('');
        $("#btn_cerrar_modal_cco").click();
    });
});
function abrir_modal_cco() {
    $("#btn_abrir_modal_cco").click();
}
//JQuery para Ayuda Usuario
$(document).ready(function () {
    $('tr#tr_usu').click(function (e) {
        var tr_data = $(this).text().trim();
        var cod = tr_data.substring(0, 11).trim();
        var des = tr_data.substring(11, tr_data.length).trim();
        console.log("-Codigo:" + cod + "-Desc:" + des + "Leng" + tr_data.length);
        $("#input_cod_usu").val(cod);
        $("#input_des_usu").val(des);
        //Limpiar la busqueda actual
        //$("#busqueda_ayuda_usu").val(null); Ya no limpia, cada busqueda en ayuda es independiente 09/01/23
        $("#btn_cerrar_modal_usu").click();
    });
});
function abrir_modal_usu() {
    $("#btn_abrir_modal_usu").click();
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

$(document).ready(function () {
    $('#tblDetalleCco_Prd"').DataTable({
        "scrollY": "20vh",
        "scrollCollapse": true,
    });
    $('.dataTables_length').addClass('bs-select');
});

//Evaluacion Req
//Funcion para validar el input y enviar el rechazo del Req
function validaMotivo() {
    var mot = document.getElementById("mot_rechazo_req").value;
    console.log(mot);
    if (mot.length == 0) {
        alert("¡Ingrese un motivo porfavor!")
    } 
    console.log("Longitud : " + mot.length);
    $("#btn_envia_rechazo_req").click();
}
function btn_abrir_modal_motivo() {  
    $("#btn_abrir_modal_motivo").click();
}
/*
 let inputValue = document.getElementById("domTextElement").value; 
  document.getElementById("valueInput").innerHTML = inputValue; 
 
 */







