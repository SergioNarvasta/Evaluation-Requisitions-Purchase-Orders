USE DRA_V22

ALTER VIEW V_WEB_REQCOMPRAS_Index 
AS
Select          A.rco_codepk ,a.rco_numrco,
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(F.uap_deslar),'') as Usuario_Aprueba,
            	Isnull(rtrim(G.S10_NOMUSU),'') as User_Solicita,s10_codusu ,S10_NOMUSU,
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
				 A.cia_codcia as cia,A.suc_codsuc as suc,A.s10_codepk ,A.rco_glorco
				  
             From REQ_REQUI_COMPRA_RCO A
             Left Join UNID_NEGOCIO_UNG    B on A.cia_codcia = B.cia_codcia and A.ung_codepk = B.ung_codepk
             Left Join CENT_COST_CCO       C on A.cia_codcia = C.cia_codcia and A.cco_codepk = C.cco_codepk
             Left Join DISCIPLINAS_DIS     E on A.cia_codcia = E.cia_codcia AND A.dis_codepk = E.dis_codepk
             LEFT JOIN REQ_USERS_APROBADORES_UAP F ON A.cia_codcia = F.cia_codcia AND A.uap_codepk = F.uap_codepk
             LEFT JOIN  SYS_TABLA_USUARIOS_S10 G ON A.s10_codepk = G.s10_codepk
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

SELECT rcf_corite as item ,rcf_nomarc as nombre,'' as archivo, rcf_codarc as codarchivo 
FROM REQ_REQUI_FILES_RCF B LEFT JOIN REQ_REQUI_COMPRA_RCO A ON A.cia_codcia=B.cia_codcia AND A.rco_codepk=B.rco_codepk
WHERE A.rco_numrco = ''
--AYUDA CENTRO COSTO
SELECT cco_codepk,CCO_CODCCO,cco_descco FROM CENT_COST_CCO 
                                                             WHERE CIA_CODCIA =1 AND cco_estado=1
--AYUDA DISCIPLINA
SELECT cia_codcia,dis_codepk,dis_nomlar FROM DISCIPLINAS_DIS 
                                                             WHERE cia_codcia=1 AND dis_estado =1
--AYUDA USUARIOS
SELECT DISTINCT AUX_CODAUX as Codigo,S10_NOMUSU as Descri 
                                                   FROM SYS_TABLA_USUARIOS_S10 WHERE S10_INDEST=1 ORDER BY S10_NOMUSU

ALTER PROCEDURE PA_WEB_ReqCompra_Inserta  @cia_codcia char(2),@suc_codsuc char(2),@rco_codepk int,@rco_numrco char(10),@tin_codtin smallint, @rco_motivo varchar(200),@rco_glorco varchar(200),
@cco_codepk smallint, @rco_sitrco char(1), @rco_codusu varchar(30),@ung_codepk smallint,  @rco_indval smallint, @rco_indest smallint, @rco_rembls char(1),  @rco_presup char(1), @rco_priori char(1),
@tre_codepk smallint ,@rco_estado varchar(1),@dis_codepk smallint,@s10_codepk int,@occ_codepk int
AS
INSERT INTO REQ_REQUI_COMPRA_RCO(cia_codcia,suc_codsuc,rco_codepk,rco_numrco,tin_codtin,rco_fecreg,rco_motivo,rco_glorco,cco_codepk,rco_sitrco,ano_codano,mes_codmes,rco_codusu,ung_codepk,rco_indval,rco_rembls,rco_presup,rco_priori,tre_codepk,rco_estado,dis_codepk,s10_codepk,occ_codepk) 
VALUES(@cia_codcia,@suc_codsuc,@rco_codepk,@rco_numrco,@tin_codtin,GETDATE(),@rco_motivo,@rco_glorco,@cco_codepk, @rco_sitrco,CAST(YEAR(GETDATE()) AS CHAR(4)),CAST(MONTH(GETDATE()) AS CHAR(2)), @rco_codusu,@ung_codepk,@rco_indval, @rco_rembls, @rco_presup,@rco_priori,@tre_codepk,@rco_estado,@dis_codepk,@s10_codepk,@occ_codepk)
GO

EXEC PA_WEB_ReqCompra_Inserta @cia_codcia = @cia_codcia ,@suc_codsuc = @suc_codsuc,@rco_codepk = @rco_codepk,@rco_numrco = @rco_numrco ,@tin_codtin = @tin_codtin,@rco_motivo = @rco_motivo,@rco_glorco = @rco_glorco,
@cco_codepk = @cco_codepk, @rco_sitrco = @rco_sitrco, @rco_codusu = @rco_codusu,@ung_codepk = @ung_codepk, @rco_indval = @rco_indval, @rco_indest =  @rco_indest, @rco_rembls = @rco_rembls, @rco_presup = @rco_presup,
@rco_priori = @rco_priori, @tre_codepk = @tre_codepk, @rco_estado = @rco_estado, @dis_codepk = @dis_codepk,@s10_codepk = @s10_codepk, @occ_codepk = @occ_codepk,
GO

SELECT* FROM SYS_TABLA_USUARIOS_S10
SELECT* FROM AspNetUsers
SELECT * FROM REQ_REQUI_COMPRA_RCO
SELECT S10_CODEPK,* FROM SYS_TABLA_USUARIOS_S10 A LEFT JOIN AspNetUsers B ON A.S10_USUARIO = B.UserName @NomUser
--INSERTA USUARIOS EN S10 
INSERT INTO SYS_TABLA_USUARIOS_S10(S10_USUARIO,S10_NOMUSU,S10_NOMCOR,S10_NIVUSU,S10_PASSWO) VALUES(@usu,@nom,@nomcor,@nivusu,@psd);
SELECT SCOPE_IDENTITY()

SELECT*FROM V_WEB_REQCOMPRAS_Index

--ENVIAR INT EN  [dbo].[PA_WEB_OC_Aprueba] @p_CodCia as Smallint, @p_CodSuc as Smallint, @p_NumOC as int, @p_CodUsr as int
GO
USE DRA_V22
--CREAR TABLA REQ_COMP
ALTER TABLE [dbo].[REQ_APROB_REQCOM_ARC] (
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

SELECT uap_codepk,*FROM REQ_USERS_APROBADORES_UAP
SELECT*FROM OCOMPRA_OCC

SELECT A.occ_codepk, A.occ_numero,A.occ_feccre,A.occ_tcaocc,B.ccr_codccr,B.ccr_nomaux,A.occ_observ,A.occ_impigv,A.tco_codtco,C.tco_nombre,
                    A.occ_estado,iif(A.occ_estado=1,'APROBADO','PENDIENTE')as occ_destado,A.mon_codepk,D.mon_desmon,A.cpg_codepk,E.cpg_deslar,
	                A.occ_fecemi,A.occ_pordet,A.occ_impdet,A.imp_codepk,F.imp_desimp
               FROM OCOMPRA_OCC A
               LEFT JOIN CUEN_CORR_CCR   B ON A.cia_codcia=B.cia_codcia AND A.ccr_codepk=B.ccr_codepk
               LEFT JOIN TIPO_COMPRA_TCO C ON A.cia_codcia=C.cia_codcia AND A.tco_codtco=C.tco_codtco
               LEFT JOIN MONEDA_MON      D ON A.mon_codepk=D.mon_codepk
               LEFT JOIN COND_PAGO_CPG   E ON A.cia_codcia=E.cia_codcia AND A.cpg_codepk=E.cpg_codepk
               LEFT JOIN IMPUESTOS_IMP   F ON A.imp_codepk=F.imp_codepk
               WHERE A.occ_numero = @Occ_numero 

GO
USE DRA_V22
CREATE TABLE [REQ_MOTIVO_DEVREQ_MDR] (
    mdr_codepk int Identity(1,1) Primary Key,
    cia_codcia  smallint      NOT NULL,
    suc_codsuc  smallint      NOT NULL,
    Rco_codepk  int           NOT NULL,
    mdr_corite  int           NOT NULL,
    mdr_fecmdr  DATETIME      NOT NULL,
    uap_codepk  int            NOT NULL,
    [MDR_TIPMDR]  CHAR (1)      NOT NULL,
    [MDR_MOTMDR]  VARCHAR (200) NOT NULL, 
);
GO
SELECT SYSTEM_USER