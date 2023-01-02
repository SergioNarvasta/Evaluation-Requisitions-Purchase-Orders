
ALTER VIEW V_WEB_REQCOMPRAS_Index 
AS
Select 
                a.rco_numrco as Rco_Numero,             
            	a.rco_fecreg as Rco_Fec_Registro, 
            	Isnull(rtrim(F.uap_deslar),'') as Usuario_Aprueba,
            	Isnull(rtrim(G.S10_NOMUSU),'') as User_Solicita,
            	a.rco_motivo as Rco_Motivo,
            	rtrim(b.ung_deslar) as U_Negocio,
            	rtrim(C.cco_codcco) + '-' + rtrim(C.cco_descco) as Centro_Costo,
            	e.dis_nomlar as Disciplina,
            	(Case a.rco_sitrco When '1' Then 'PENDIENTE'
                               When '2' Then 'APROBADO'
                               When '3' Then 'RECHAZADO'
                               Else 'NO DEFINIDO' End) as Rco_Situacion_Aprobado,
                (Case a.rco_priori When '1' Then 'MUY ALTA'
                               When '2' Then 'ALTA'
                               When '3' Then 'MEDIA'
                               When '4' Then 'BAJA'
                               Else 'NO DEFINIDO' End) as Rco_Prioridad,
            	Isnull(a.rco_obspri,'') as Rco_Justificacion,
            	(Case a.rco_rembls When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Reembolso,
            	(Case a.rco_presup When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as Rco_Presupuesto,             
				(Case a.rco_indval When '0' Then 'NO' 
                               When '1' Then 'SI' 
                               Else ' ' End) as RCO_Categorizado,
                 '' as SCC_Cotizacion,
				 a.ocm_corocm as Occ_numeroocc, 
				 '' as Occ_dessituacionocc,
				 ' 'as OCC_ProveedorOCC,
				 Cast(YEAR(A.rco_fecreg) as char(4))+ Substring('0'+ltrim(Cast(MONTH(A.rco_fecreg)as char(2))),len(ltrim(Cast(MONTH(A.rco_fecreg)as char(2)))),2)as periodo,
				 A.rco_estado as estado,
				 A.cia_codcia as cia,A.suc_codsuc as suc,A.s10_codepk 
				  
             From REQ_REQUI_COMPRA_RCO A
             Left Join UNID_NEGOCIO_UNG    B on A.cia_codcia = B.cia_codcia and A.ung_codepk = B.ung_codepk
             Left Join CENT_COST_CCO       C on A.cia_codcia = C.cia_codcia and A.cco_codepk = C.cco_codepk
             Left Join DISCIPLINAS_DIS     E on A.cia_codcia = E.cia_codcia AND A.dis_codepk = E.dis_codepk
             LEFT JOIN REQ_USUARIO_APROBADORES_UAP F ON A.cia_codcia = F.cia_codcia AND A.uap_codepk = F.uap_codepk
             LEFT JOIN  SYS_TABLA_USUARIOS_S10 G ON A.s10_codepk = G.s10_codepk
             Left Join REQ_TIPO_REQUISICION_TRE H On A.cia_codcia = H.cia_codcia And A.tre_codepk = H.tre_codepk
GO
			 
SELECT *FROM REQ_REQUI_COMPRA_RCO --PRINCIPAL
SELECT *FROM REQ_REQUI_COMPRA_RCD --DETALLE
SELECT *FROM REQ_REQUI_FILES_RCF  --ADJUNTOS

SELECT CIA_NOMCIA FROM COMPANIA_CIA WHERE CIA_CODCIA =01
SELECT *FROM REQ_REQUI_COMPRA_RCO a
SELECT ccr_codepk,*FROM CUEN_CORR_CCR WHERE ccr_nomaux LIKE '%sistema%'
SELECT occ_codepk,*FROM CUEN_CORR_CCR

SELECT*FROM V_WEB_REQCOMPRAS_Index 
WHERE Rco_Numero ='RQ20221228'
ORDER BY Rco_Fec_Registro DESC 

GO
--CREACION DE TABLAS
CREATE TABLE [dbo].[SYS_TABLA_IDIOMAS_S19] (
    [S19_CODIDI] CHAR (2)        NOT NULL,
    [S19_NOMIDI] CHAR (40)       NULL,
    [S19_NOMCOR] CHAR (20)       NULL,
    [S19_INDEST] CHAR(1)  NULL,
    [S19_FECACT] DATETIME  NULL,
    [S19_CODUSU] [dbo].[USUARIO] NULL,
    CONSTRAINT [PK_SYS_TABLA_IDIOMAS_S19] PRIMARY KEY CLUSTERED ([S19_CODIDI] ASC)
);
GO
CREATE TABLE [dbo].[SYS_TABLA_USUARIOS_S10] (
    [S10_USUARIO] CHAR (20)     NOT NULL,
    [S10_NOMUSU]  CHAR (40)     NOT NULL,
    [S10_NOMCOR]  CHAR (10)     NOT NULL,
    [S10_NIVUSU]  INT           NOT NULL,
    [S10_TIPUSU]  CHAR (1)      NULL,
    [S10_PASSWO]  CHAR (10)     NOT NULL,
    [S19_CODIDI]  CHAR (2)      NULL,
    [S10_CODDBF]  CHAR (10)     NULL,
    [AUX_CODAUX]  CHAR (12)     NULL,
    [S10_INDEST]  CHAR (1)      NULL,
    [S10_FECACT]  DATETIME      NULL,
    [S10_CODUSU]  VARCHAR (30)  NULL,
    [S10_FIRUSU]  IMAGE         NULL,
    [S10_FOTUSU]  IMAGE         NULL,
    [S10_PERFIL]  CHAR (20)     NULL,
    [ACO_CODACO]  CHAR (2)      NULL,
    [s10_firapro] VARCHAR (100) NULL,
    [S10_NOMFIR]  VARCHAR (30)  NULL,
    [s10_ind_rlg] TINYINT       NULL,
    CONSTRAINT [PK__SYS_TABLA_USUARI__4EC8A2F6] PRIMARY KEY CLUSTERED ([S10_USUARIO] ASC),
    CONSTRAINT [FK_SYS_TABLA_USUARIOS_S10_SYS_TABLA_IDIOMAS_S19] FOREIGN KEY ([S19_CODIDI]) REFERENCES [dbo].[SYS_TABLA_IDIOMAS_S19] ([S19_CODIDI])
);
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

-- REGISTRA EN RCO

Select cia_codcia,
       suc_codsuc,
	   rco_numrco,
	   tin_codtin,
	   rco_sitrco,
	   rco_sitlog,
	   ano_codano,
	   mes_codmes,
	   rco_estado,
       rco_fecreg,
	   rco_codusu,
	   ung_codepk,
	   rco_indcie,
	   rco_indval,
	   dis_codepk,
	   rco_rembls,
	   rco_presup,
	   rco_priori,
	   rco_motivo
from  REQ_REQUI_COMPRA_RCO
Where cia_codcia=1 AND suc_codsuc=1 AND ano_codano=2022 AND mes_codmes=12
SELECT *FROM REQ_REQUI_COMPRA_RCO
GO
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
SELECT*FROM REQ_TIPO_REQUISICION_TRE
SELECT S10_CODEPK,* FROM SYS_TABLA_USUARIOS_S10 A LEFT JOIN AspNetUsers B ON A.S10_USUARIO = B.UserName @NomUser
--INSERTA USUARIOS EN S10 
INSERT INTO SYS_TABLA_USUARIOS_S10(S10_USUARIO,S10_NOMUSU,S10_NOMCOR,S10_NIVUSU,S10_PASSWO) VALUES(@usu,@nom,@nomcor,@nivusu,@psd);
SELECT SCOPE_IDENTITY()

SELECT COUNT(*)  FROM SYS_TABLA_USUARIOS_S10 A 
                                     LEFT JOIN AspNetUsers B ON A.S10_USUARIO = B.UserName 
                                     WHERE A.S10_USUARIO = 'Sistemas'

