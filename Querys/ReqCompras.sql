USE NBG_V21

SELECT*FROM REQ_USERS_APROBADORES_UAP

CREATE VIEW V_WEB_REQCOMPRAS_Index 
AS
Select          A.rco_codepk ,a.rco_numrco,
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(F.uap_deslar),'') as Usuario_Aprueba,
            	Isnull(rtrim(A.rco_codusu),'') as User_Solicita,
				uap_deslar ,uap_coduap,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,b.ung_codepk ,
            	rtrim(C.cco_codcco) + '-' + rtrim(C.cco_descco) as Centro_Costo, C.cco_codepk,C.cco_codcco,C.cco_descco,
            	e.dis_nomlar as Disciplina,e.dis_codepk,e.dis_coddis ,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,a.rco_sitrco,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,a.rco_priori,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,a.rco_obspri,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,a.rco_rembls,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,a.rco_presup,             
				(Case a.rco_indval When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as RCO_Categorizado,a.rco_indval,
                 '' as SCC_Cotizacion,
				 a.ocm_corocm as Occ_numeroocc, A.occ_codepk,I.occ_numero,
				 '' as Occ_dessituacionocc,
				 ' 'as OCC_ProveedorOCC,
				 Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2)as periodo,
				 A.rco_estado as estado,rco_estado,
				 A.cia_codcia as cia,A.suc_codsuc as suc,A.uap_codepk ,A.rco_glorco
				  
             From REQ_REQUI_COMPRA_RCO A
             Left Join UNID_NEGOCIO_UNG    B on A.cia_codcia = B.cia_codcia and A.ung_codepk = B.ung_codepk
             Left Join CENT_COST_CCO       C on A.cia_codcia = C.cia_codcia and A.cco_codepk = C.cco_codepk
             Left Join DISCIPLINAS_DIS     E on A.cia_codcia = E.cia_codcia AND A.dis_codepk = E.dis_codepk
             LEFT JOIN REQ_USERS_APROBADORES_UAP F ON A.cia_codcia = F.cia_codcia AND A.uap_codepk = F.uap_codepk
             --LEFT JOIN  SYS_TABLA_USUARIOS_S10 G ON A.s10_codepk = G.s10_codepk
             Left Join REQ_TIPO_REQUISICION_TRE H On A.cia_codcia = H.cia_codcia And A.tre_codepk = H.tre_codepk
			 LEFT JOIN OCOMPRA_OCC I ON A.cia_codcia=I.cia_codcia AND A.occ_codepk=I.occ_codepk
GO
SELECT *FROM V_WEB_REQCOMPRAS_Index			 
SELECT *FROM REQ_REQUI_COMPRA_RCO --PRINCIPAL
SELECT *FROM REQ_REQUI_COMPRA_RCD --DETALLE
SELECT *FROM REQ_REQUI_FILES_RCF  --ADJUNTOS

SELECT CIA_NOMCIA FROM COMPANIA_CIA WHERE CIA_CODCIA =01
SELECT *FROM REQ_REQUI_COMPRA_RCO a
SELECT ccr_codepk,*FROM CUEN_CORR_CCR WHERE ccr_nomaux LIKE '%sistema%'
SELECT *FROM REQ_USERS_APROBADORES_UAP

SELECT*FROM REQ_APROB_ORDCOM_AOC
SELECT SYSTEM_USER; 
GO
--DETALLE REQ
SELECT  B.rcd_corite as item ,L.prd_codprd as codigo,B.rcd_desprd as descri,B.rcd_glorcd as glosa,K.ume_descor as unidad,
	                                    rcd_canapr as cantidad,J.ccr_codccr as codprov, J.ccr_nomaux as nomprov
	                                    FROM REQ_REQUI_COMPRA_RCO A
	                                    LEFT JOIN REQ_REQUI_COMPRA_RCD B ON A.cia_codcia=B.cia_codcia AND A.suc_codsuc=B.suc_codsuc AND A.rco_codepk=B.rco_codepk
	                                    Left Join OCOMPRA_OCC I on a.cia_codcia=i.CIA_CODCIA and a.suc_codsuc=i.suc_codsuc and A.occ_codepk =I.occ_codepk
                                        Left Join CUEN_CORR_CCR J on i.cia_codcia=j.CIA_CODCIA and i.ccr_codepk=j.ccr_codepk
										LEFT JOIN UMEDIDA_UME K ON A.cia_codcia=K.cia_codcia AND B.ume_codepk=K.ume_codepk
										LEFT JOIN PRODUCTOS_PRD L ON A.cia_codcia=L.cia_codcia AND B.prd_codepk=L.prd_codepk
	                                    WHERE A.rco_numrco =@Rco_numero
--DETALLE ADJUNTOS

SELECT rcf_corite as item ,rcf_nomarc as nombre,rcf_file as archivo, rcf_codarc as codarchivo 
                     FROM REQ_REQUI_FILES_RCF B LEFT JOIN REQ_REQUI_COMPRA_RCO A ON A.cia_codcia=B.cia_codcia AND A.rco_codepk=B.rco_codepk
                     WHERE A.rco_numrco =@Rco_numero
--AYUDA CENTRO COSTO
SELECT cco_codepk,CCO_CODCCO,cco_descco FROM CENT_COST_CCO 
                                                             WHERE CIA_CODCIA =1 AND cco_estado=1
--AYUDA DISCIPLINA
SELECT cia_codcia,dis_codepk,dis_nomlar FROM DISCIPLINAS_DIS 
                                                             WHERE cia_codcia=1 AND dis_estado =1
--AYUDA USUARIOS
Select uap_coduap as codigo, uap_deslar as descri 
                          From REQ_USERS_APROBADORES_UAP
                          Where cia_codcia=1 ORDER BY UAP_DESLAR
GO

/*---------------------------------------------------------------------*/

ALTER PROCEDURE PA_WEB_ReqCompra_Inserta  @cia_codcia char(2),@suc_codsuc char(2),@rco_codepk int,@rco_numrco char(10),@tin_codtin smallint, @rco_motivo varchar(200),@rco_glorco varchar(200),
@cco_codepk smallint, @rco_sitrco char(1), @rco_codusu varchar(30),@ung_codepk smallint,  @rco_indval smallint, @rco_indest smallint, @rco_rembls char(1),  @rco_presup char(1), @rco_priori char(1),
@tre_codepk smallint ,@rco_estado varchar(1),@dis_codepk smallint,@uap_codepk int,@occ_codepk int,

@rcd_corite char(10),  @prd_codepk int, @rcd_desprd varchar(50),@rcd_glorcd varchar(50) ,@rcd_canate numeric(10,2),@ccr_codepk int ,@ume_codepk int ,

 @rcf_corite1 char(10) ,@rcf_codarc1 char(30) ,@rcf_nomarc1 varchar(30),@rcf_file1 varchar(MAX),
 @rcf_corite2 char(10) ,@rcf_codarc2 char(30) ,@rcf_nomarc2 varchar(30),@rcf_file2 varchar(MAX),
 @s_mensaje varchar(50)
AS
INSERT INTO REQ_REQUI_COMPRA_RCO(cia_codcia,suc_codsuc,rco_codepk,rco_numrco,tin_codtin,rco_fecreg,rco_motivo,rco_glorco,cco_codepk,rco_sitrco,ano_codano,mes_codmes,rco_codusu,ung_codepk,rco_indval,rco_rembls,rco_presup,rco_priori,tre_codepk,rco_estado,dis_codepk,uap_codepk,occ_codepk) 
VALUES(@cia_codcia,@suc_codsuc,@rco_codepk,@rco_numrco,@tin_codtin,GETDATE(),@rco_motivo,@rco_glorco,@cco_codepk, @rco_sitrco,CAST(YEAR(GETDATE()) AS CHAR(4)),CAST(MONTH(GETDATE()) AS CHAR(2)), @rco_codusu,@ung_codepk,@rco_indval, @rco_rembls, @rco_presup,@rco_priori,@tre_codepk,@rco_estado,@dis_codepk,@uap_codepk,@occ_codepk)
If @@ERROR <> 0 
Begin
	Set @s_mensaje = 'Error al Insertar datos en REQ_REQUI_COMPRA_RCO ' 
	Raiserror(@s_mensaje,16,1)
	Select -1 as Cod_Resultado, @s_Mensaje as Des_Resultado
	Return -1
End
INSERT INTO REQ_REQUI_COMPRA_RCD (rco_codepk,cia_codcia,suc_codsuc,rcd_corite,prd_codepk,rcd_desprd,rcd_glorcd,rcd_canate) VALUES(@rco_codepk,@cia_codcia,@suc_codsuc,@rcd_corite,@prd_codepk,@rcd_desprd,@rcd_glorcd,@rcd_canate)
If @@ERROR <> 0 
Begin
	Set @s_mensaje = 'Error al Insertar datos en REQ_REQUI_COMPRA_RCD '
	Raiserror(@s_mensaje,16,1)
	Select -2 as Cod_Resultado, @s_Mensaje as Des_Resultado
	Return -2
End
INSERT INTO REQ_REQUI_FILES_RCF(rco_codepk,cia_codcia,rcf_corite,rcf_codarc,rcf_nomarc,rcf_file)VALUES(@rco_codepk,@cia_codcia,@rcf_corite1,@rcf_codarc1,@rcf_nomarc1,@rcf_file1);
INSERT INTO REQ_REQUI_FILES_RCF(rco_codepk,cia_codcia,rcf_corite,rcf_codarc,rcf_nomarc,rcf_file)VALUES(@rco_codepk,@cia_codcia,@rcf_corite1,@rcf_codarc1,@rcf_nomarc1,@rcf_file1);
If @@ERROR <> 0 
Begin
	Set @s_mensaje = 'Error al Insertar datos en REQ_REQUI_FILES_RCF '
	Raiserror(@s_mensaje,16,1)
	Select -3 as Cod_Resultado, @s_Mensaje as Des_Resultado
	Return -3
End
Select 1 as Cod_Resultado, 'Registro con exito' as Des_Resultado
GO

EXEC PA_WEB_ReqCompra_Inserta @cia_codcia = @cia_codcia ,@suc_codsuc = @suc_codsuc,@rco_codepk = @rco_codepk,@rco_numrco = @rco_numrco ,@tin_codtin = @tin_codtin,@rco_motivo = @rco_motivo,@rco_glorco = @rco_glorco,
@cco_codepk = @cco_codepk, @rco_sitrco = @rco_sitrco, @rco_codusu = @rco_codusu,@ung_codepk = @ung_codepk, @rco_indval = @rco_indval, @rco_indest =  @rco_indest, @rco_rembls = @rco_rembls, @rco_presup = @rco_presup,
@rco_priori = @rco_priori, @tre_codepk = @tre_codepk, @rco_estado = @rco_estado, @dis_codepk = @dis_codepk,@s10_codepk = @s10_codepk, @occ_codepk = @occ_codepk,
GO

--ENVIAR INT EN  [dbo].[PA_WEB_OC_Aprueba] @p_CodCia as Smallint, @p_CodSuc as Smallint, @p_NumOC as int, @p_CodUsr as int
GO
USE NBG_V21
--CREAR TABLA REQ_COMP
CREATE TABLE [dbo].[REQ_APROB_REQCOM_ARC] (
    [arc_codepk]  INT         PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    [cia_codcia]  SMALLINT     NOT NULL,
    [suc_codsuc]  SMALLINT     NOT NULL,
    [rco_codepk]  INT          NOT NULL,
    [arc_coraoa]  SMALLINT     NOT NULL,
    [uap_codepk]  INT          NOT NULL,
    [anm_codanm]   SMALLINT     NOT NULL,
    [arc_indapr]  SMALLINT     NOT NULL,
    [arc_porapr]  SMALLINT     NOT NULL,
    [arc_fecact]  DATETIME     NOT NULL,
    [arc_codusu]  VARCHAR (30) NOT NULL,
    [tac_codtac]  CHAR (1)     NOT NULL,
    [arc_indenv]  TINYINT      CONSTRAINT [DF_APROBAC_ORDCOM_APROBACIONES_ARC_INDENV] DEFAULT ((0)) NULL,
    [arc_fecenv]  DATETIME     NULL,
    [mao_codeve]  CHAR (4)     NULL,
	CONSTRAINT [FK_ARC_SUCURSAL_CIA] FOREIGN KEY ([cia_codcia], [suc_codsuc]) REFERENCES [dbo].[SUCURSAL_SUC] ([cia_codcia], [suc_codsuc]), 
);
GO
SELECT rco_sitrco,*FROM REQ_REQUI_COMPRA_RCO
SELECT*FROM V_WEB_REQCOMPRAS_Index 
SELECT*FROM REQ_APROB_REQCOM_ARC
SELECT*FROM REQ_USERS_APROBADORES_UAP
INSERT INTO REQ_USERS_APROBADORES_UAP(cia_codcia,uap_descor,uap_estado,uap_nivusu,uap_coduap)VALUES(1,'SNarvasta','1','1','EMP001')
GO
--EJECUTA APROBACION
EXEC PA_WEB_RQ_Rechaza @p_CodCia = 1, @p_CodSuc =1, @p_NumRQ =20221201, @p_CodUsr =44 ,@p_Motivo='Rechazo Prueba' 
EXEC PA_WEB_RQ_Aprueba @p_CodCia = 1, @p_CodSuc =1, @p_NumRQ =20221201, @p_CodUsr =44 

--CONSULTAR ESTADO
	Select rco_sitrco,* from REQ_REQUI_COMPRA_RCO where rco_codepk=20221201
	SELECT arc_indapr,* FROM REQ_APROB_REQCOM_ARC where rco_codepk=20221201

--DESACTIVAR APROBACION
UPDATE REQ_REQUI_COMPRA_RCO SET rco_sitrco = 1 where rco_codepk=20221201
UPDATE REQ_APROB_REQCOM_ARC SET arc_indapr = 0 where rco_codepk=20221201

--MOTIVOS DE RECHAZO 
SELECT *FROM REQ_MOTIVO_DEVREQ_MDR

GO
USE DRA_V22
SELECT TOP 1 A.rco_numrco FROM REQ_REQUI_COMPRA_RCO  A ORDER BY A.rco_numrco DESC
SELECT*FROM REQ_REQUI_COMPRA_RCD

SELECT*FROM PRODUCTOS_PRD
SELECT * FROM REQ_REQUI_COMPRA_RCO

SELECT  B.rcd_corite as item  ,  L.prd_codprd as codigo,   B.rcd_desprd as descri,   B.rcd_glorcd as glosa,  
	    B.rcd_canate as cantidad,  J.ccr_codccr as codprov,  J.ccr_nomaux as nomprov,  K.ume_descor as unidad,k.ume_codepk
FROM REQ_REQUI_COMPRA_RCO A
LEFT JOIN REQ_REQUI_COMPRA_RCD B ON A.cia_codcia=B.cia_codcia AND A.suc_codsuc=B.suc_codsuc AND A.rco_codepk=B.rco_codepk
Left Join OCOMPRA_OCC I on a.cia_codcia=i.CIA_CODCIA and a.suc_codsuc=i.suc_codsuc and A.occ_codepk =I.occ_codepk
Left Join CUEN_CORR_CCR J on i.cia_codcia=j.CIA_CODCIA and i.ccr_codepk=j.ccr_codepk
LEFT JOIN UMEDIDA_UME K ON A.cia_codcia=K.cia_codcia AND B.ume_codepk=K.ume_codepk
LEFT JOIN PRODUCTOS_PRD L ON B.cia_codcia=L.cia_codcia AND B.prd_codepk=L.prd_codepk
WHERE A.rco_numrco =@Rco_numero


GO
--EJECUTA ESTORE INSERTA REQCOMPRA
EXEC PA_WEB_ReqCompra_Inserta '01','01',2023011001,'RQ555544',1,'Prueba de Insercion con PA_WEB_ReqCompra_Inserta','Prueba HD',
      0,'1','Sistemas',1,1,1,1,1,1,'1',1,44,0,1    --DetallePrd  '001','2','oficina','detalle','15','44','1'

--'01','01',2023011001,'RQ555544',1,'Prueba de Insercion con Detalle','Prueba HD',0,'1','Sistemas',1,1,1,1,0,1,1,'1',1,44,0,  '001','2','Producto de oficina','detalle','15','44','1'


		SELECT*FROM V_WEB_REQCOMPRAS_Index
                Where cia=1 AND suc=1 AND periodo =202301 AND uap_codepk = 44  AND estado in(1,1)
                ORDER BY Rco_Numero DESC 
				SELECT*FROM REQ_REQUI_COMPRA_RCD 

	SELECT  B.rcd_corite as item ,L.prd_codprd as codigo,B.rcd_desprd as descri,B.rcd_glorcd as glosa,K.ume_codume as unidad,
	    rcd_canate as cantidad,Isnull(J.ccr_codccr,'000001') as codprov, J.ccr_nomaux as nomprov
	    FROM REQ_REQUI_COMPRA_RCO A
	    LEFT JOIN REQ_REQUI_COMPRA_RCD B ON A.cia_codcia=B.cia_codcia AND A.suc_codsuc=B.suc_codsuc AND A.rco_codepk=B.rco_codepk
	    Left Join OCOMPRA_OCC I on a.cia_codcia=i.CIA_CODCIA and a.suc_codsuc=i.suc_codsuc and A.occ_codepk =I.occ_codepk
        Left Join CUEN_CORR_CCR J on i.cia_codcia=j.CIA_CODCIA and i.ccr_codepk=j.ccr_codepk
		LEFT JOIN UMEDIDA_UME K ON A.cia_codcia=K.cia_codcia AND B.ume_codepk=K.ume_codepk
		LEFT JOIN PRODUCTOS_PRD L ON A.cia_codcia=L.cia_codcia AND B.prd_codepk=L.prd_codepk
	                           WHERE A.rco_numrco ='RQ89888889'

-- Corregir la asignacion a s10_usuario
-- Obtener Detalle Productos por Epk
-- View Editar por Epk
--* View Crear Coloca Combos Automaticos (al cambiar2,3,4 y por default 1) Jquery
--* Cargar combos tre y ung con componentes async   .NET
--Validacion de Campos en Frontend
GO
SELECT tre_codepk as codigo,tre_deslar as descri 
FROM REQ_TIPO_REQUISICION_TRE WHERE cia_codcia=1 and tre_estado=1

SELECT ung_codepk as codigo,ung_deslar as descri 
FROM UNID_NEGOCIO_UNG WHERE cia_codcia=1 and ung_estado=1

--Codigo de Archivo 
--Vista Result poner boton volver a panel