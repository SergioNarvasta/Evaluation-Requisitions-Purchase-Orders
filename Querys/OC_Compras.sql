USE LAVALIN

/*
        rsOC                    = objOC.ObtenerOrdenDeCompra                  (sCodigoCIA, sCodigoSUC, sNumeroOC);
        rsOC_Detalle            = objOC.ObtenerDetalleOrdenCompra             (sCodigoCIA, sCodigoSUC, sNumeroOC);
        rsOC_Detalle_Entrega    = objOC.ObtenerDetalleOrdenCompra_Entrega     (sCodigoCIA, sCodigoSUC, sNumeroOC);
        rsOC_Detalle_Aprobacion = objOC.ObtenerDetalleOrdenCompra_Aprobacion  (sCodigoCIA, sCodigoSUC, sNumeroOC);
        rsOC_Detalle_CCosto     = objOC.ObtenerDetalleOrdenCompra_CCosto      (sCodigoCIA, sCodigoSUC, sNumeroOC);
        rsOC_Detalle_Pagos      = objOC.ObtenerDetalleOrdenCompra_Pagos       (sCodigoCIA, sCodigoSUC, sNumeroOC);
        rsOC_Detalle_Adelantos  = objOC.ObtenerDetalleOrdenCompra_Adelantos   (sCodigoCIA, sCodigoSUC, sNumeroOC);
        rsOC_Detalle_Kardex     = objOC.ObtenerDetalleOrdenCompra_Kardex      (sCodigoCIA, sCodigoSUC, sNumeroOC);
*/
USE DRA_V22
SELECT*FROM OCOMPRA_OCC Where occ_numero = 'I000000058'
UPDATE OCOMPRA_OCC SET occ_estado=0 WHERE occ_numero = 'I000000058'
SELECT A.occ_numero,A.occ_feccre,A.occ_tcaocc,B.ccr_codccr,B.ccr_nomaux,A.occ_observ 
FROM OCOMPRA_OCC A
LEFT JOIN CUEN_CORR_CCR B ON A.cia_codcia=B.cia_codcia AND A.ccr_codepk=B.ccr_codepk
WHERE Cast(YEAR(A.occ_feccre) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.occ_feccre)as char(2))),len(ltrim(Cast(MONTH(A.occ_feccre)as char(2)))),2)=202211
AND A.cia_codcia = 1 AND A.suc_codsuc = 1 
ORDER BY A.occ_feccre DESC

--OCC DETALLADA PARA APROBACION 
SELECT  Convert(varbinary,A.occ_numero) as occ_encryp, A.occ_codepk ,A.occ_numero,A.occ_feccre,A.occ_tcaocc,B.ccr_codccr,B.ccr_nomaux,A.occ_observ,A.occ_impigv,
       A.tco_codtco,C.tco_nombre,A.occ_estado,iif(A.occ_estado=1,'APROBADO','PENDIENTE')as occ_destado,A.mon_codepk,D.mon_desmon,A.cpg_codepk,E.cpg_deslar,
	   A.occ_fecemi,A.occ_pordet,A.occ_impdet,A.imp_codepk,F.imp_desimp
FROM OCOMPRA_OCC A
LEFT JOIN CUEN_CORR_CCR   B ON A.cia_codcia=B.cia_codcia AND A.ccr_codepk=B.ccr_codepk
LEFT JOIN TIPO_COMPRA_TCO C ON A.cia_codcia=C.cia_codcia AND A.tco_codtco=C.tco_codtco
LEFT JOIN MONEDA_MON      D ON A.mon_codepk=D.mon_codepk
LEFT JOIN COND_PAGO_CPG   E ON A.cia_codcia=E.cia_codcia AND A.cpg_codepk=E.cpg_codepk
LEFT JOIN IMPUESTOS_IMP   F ON A.imp_codepk=F.imp_codepk
WHERE Convert(varbinary,A.occ_numero) = Convert(varbinary,83484848484848485548) AND
Cast(YEAR(A.occ_feccre) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.occ_feccre)as char(2))),len(ltrim(Cast(MONTH(A.occ_feccre)as char(2)))),2)=202211
AND A.cia_codcia = 1 AND A.suc_codsuc = 1 
ORDER BY A.occ_feccre DESC


--CREAR TABLA Terminos_Compra_TCO
--DETALLE OCOMPRA_OCD
USE DRA_V22

Select 
A.occ_codepk,A.ocd_corite, A.prd_codepk , B.tin_codtin , B.prd_desprd, A.ocd_especi, A.ocd_cansol, a.ocd_poraju, a.ocd_canaju ,
A.equ_codequ as OCD_Ume_Compra, A.ume_codepk ,A.ocd_preuni ,A.ocd_impbru ,A.ocd_impdes ,A.ocd_impigv ,A.ocd_imptot,
(Case When Isnull(c.equ_canequ,0)<=0 Then 0 Else 
      Round((((Isnull(a.ocd_cansol,0)+Isnull(a.ocd_canaju,0)) * Isnull(c.equ_canori,0)) / Isnull(c.equ_canequ,0)),3) End) As OCD_Cantidad_Real ,
        A.ocd_canate as OCD_Cantidad_Atendida,
((Case When Isnull(c.equ_canequ,0)<=0 Then 0 Else 
       Round((((Isnull(a.ocd_cansol,0)+Isnull(a.ocd_canaju,0)) * Isnull(c.equ_canori,0)) / Isnull(c.equ_canequ,0)),3) End)-ISNULL(a.ocd_canate,0)) As OCD_Cantidad_Saldo,
Round(Isnull(a.ocd_impbru,0) - (Isnull(a.ocd_impdes,0)),3) As OCD_Valor_Venta
From OCOMPRA_OCD A
Left Join PRODUCTOS_PRD B     ON (a.cia_codcia=b.cia_codcia And a.prd_codepk=b.prd_codepk)
Left Join UEQUIVALENCIA_EQU C ON (A.cia_codcia=C.cia_codcia AND A.ume_codepk=C.ume_codepk and A.equ_codequ=C.equ_codequ)  
LEFT JOIN UMEDIDA_UME D       ON (A.ume_codepk=D.ume_codepk AND A.cia_codcia=D.cia_codcia)
WHERE A.occ_codepk = 113

SELECT*FROM PRODUCTOS_PRD
SELECT*FROM PROYECTOS_PRY --TOMAR COMO CENTRO COSTO
--DETALLE ENTREGA LAVALIN GUIA 
Select 
a.OCM_COROCM as OCC_Numero, 
a.OCE_NROENT as OCE_Numero_Entrega, 
a.OCE_FECENT as OCE_Fecha_Entrega,
a.LEC_CODLEC as OCE_Lugar_Entrega_Id,
b.lec_deslar as OCE_Lugar_Entrega
From dbo.ordcom_entregas_oce A
Inner join dbo.lugar_entrega_lec B
    on(b.cia_codcia = a.cia_codcia and b.lec_codlec = a.lec_codlec) 

--DETALLE APROBADORES
Select 
a.ocm_corocm as OCC_Numero,
a.S10_USUARIO as AOA_Usuario_ID,
b.s10_nomusu as AOA_Nombres,
a.AOA_INDAPR as AOA_Indicador_Aprobacion,
a.AOA_PORAPR as AOA_Indicador_Por_Aprobar,
a.ANM_CODANM as AOA_Nivel_Aprobacion,
(Case When a.aoa_indapr='1' Then 'SI' else '  ' end ) as Aoa_Indicador_Aprobacion_Texto,
(Case When a.aoa_indapr='1' Then a.aoa_fecact else null end ) as Aoa_FechaEval,
a.AOA_INDENV as Ara_Ind_Envio,
a.AOA_FECENV as Ara_FechaEnvio
--,a.*,b.s10_nomusu 
From APROBAC_ORDCOM_APROBACIONES_AOA A 
Inner join SYS_TABLA_USUARIOS_S10 b 
   On b.s10_usuario=a.s10_usuario 

--FALTA TABLA APROBADORES_OCC
 SELECT*FROM REQ_USUARIO_APROBADORES_UAP

 --DETALLE PAGOS FALTA TABLA DISTRIBUCION OCC
 Select 
a.ocm_corocm as OCC_Numero,
substring(a.cte_codcte,1,4) + '-' +  substring(a.cte_codcte,5,2) + '-' +  substring(a.cte_codcte,7,3) + '-' +  substring(a.cte_codcte,10,5) as DOR_CtaCte,
b.cte_fecemi as DOR_Fecha_Emision,
a.dor_impnac as DOR_Importe_Nacional,
a.dor_impdol as DOR_Importe_Dolar
--,a.cte_codcte,b.cte_tasigv,b.tmo_codtmo,b.cte_fecemi,a.dor_impnac as dor_impnac_igv, a.dor_impdol as dor_impdol_igv
From dbo.distribucion_ordcom_dor a 
Inner join dbo.cuenta_corriente_cte b 
    on(b.cia_codcia = a.cia_codcia and b.suc_codsuc = a.suc_codsuc and b.cte_codcte = a.cte_codcte) 

select * from OCOMPRA_OCC
select u01_codigo,* from SYS_USUARIO_U01
select*from SYS_TABLA_USUARIOS_S10
SELECT  * FROM REQ_EMPLEADOS_ERQ
--nueva en dra se llamara REQ_APROB_ORDCOM_AOC -- TABLA DE RUTA/NIVELES DE APROBACIONES DE LAS O/C's

CREATE TABLE [dbo].[REQ_APROB_ORDCOM_AOC] (
    [aoc_codepk]  INT         PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [cia_codcia]  SMALLINT     NOT NULL,
    [suc_codsuc]  SMALLINT     NOT NULL,
    [occ_codepk]  INT          NOT NULL,
    [aoa_coraoa]  SMALLINT     NOT NULL,
    [uap_codepk]  INT          NOT NULL,
    [anmcodanm]   SMALLINT     NOT NULL,
    [aoa_indapr]  SMALLINT     NOT NULL,
    [aoa_porapr]  SMALLINT     NOT NULL,
    [aoa_fecact]  DATETIME     NOT NULL,
    [aoa_codusu]  VARCHAR (30) NOT NULL,
    [tac_codtac]  CHAR (1)     NOT NULL,
    [aoa_indenv]  TINYINT      CONSTRAINT [DF_APROBAC_ORDCOM_APROBACIONES_AOA_AOA_INDENV] DEFAULT ((0)) NULL,
    [aoa_fecenv]  DATETIME     NULL,
    [mao_codeve]  CHAR (4)     NULL,
	CONSTRAINT [FK_AOC_SUCURSAL_CIA] FOREIGN KEY ([cia_codcia], [suc_codsuc]) REFERENCES [dbo].[SUCURSAL_SUC] ([cia_codcia], [suc_codsuc]), 

);

--EJECUTA APROBACION
EXEC PA_WEB_OC_Aprueba @p_CodCia='01' , @p_CodSuc ='01' , @p_NumOC='I000000058' , @p_CodUsr='Sistemas'

--CONSULTAR ESTADO
Select occ_estado,occ_sitapr,* from OCOMPRA_OCC where occ_codepk=273
SELECT aoc_indapr,*FROM REQ_APROB_ORDCOM_AOC where occ_codepk = 273

--DESACTIVAR APROBACION
UPDATE REQ_APROB_ORDCOM_AOC SET aoc_indapr = 0 where occ_codepk=273
UPDATE OCOMPRA_OCC SET occ_estado =0, occ_sitapr=0 where occ_codepk=273

--DELETE FROM REQ_APROB_ORDCOM_AOC
SELECT uap_codepk,*FROM REQ_USERS_APROBADORES_UAP

--CORREGIR STORE


SELECT*FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =@periodo AND  s10_codepk = @EpkUser  AND estado in(@estado1,@estado2)
                ORDER BY Rco_Numero DESC 
