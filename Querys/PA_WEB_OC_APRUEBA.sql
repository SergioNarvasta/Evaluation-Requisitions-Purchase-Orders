
--USE DRA_V22
ALTER PROCEDURE [dbo].[PA_WEB_OC_Aprueba]
@p_CodCia as Smallint, @p_CodSuc as Smallint, @p_NumOC as int, @p_CodUsr as int
/***********************************************************************************************************
 Procedimiento	: PA_HD_WEB_OC_Aprueba
 Proposito		: Ejecuta la Aprobacion del OC en el nivel del usuario, si es el ultimo nivel se aprueba la OC
 Inputs			: p_CodCia, p_CodSuc, p_NumOC, p_CodUsr
 Se asume		: OC existe en Tablas y Ya existe Validacion de acceso al OC
 Efectos		: Retorno 1 registro con Indicacion de EXITO 1 o FALLO 0
 Retorno		: 1 Registro con 2 Columnas
 Notas			: N/A
 Modificaciones	: 
 Autor			: Narvasta Sergio
 Fecha y Hora	: 02/01/2023
***********************************************************************************************************/
AS
SET NOCOUNT ON

Declare @n_NumNiv Tinyint, @n_NivUsr Tinyint, @n_NumVis Tinyint  
Declare @s_mensaje Varchar(200), @c_numscc varchar(10)
Set @n_NumNiv = 0
Set @n_NumVis = 0
Set @n_NivUsr = 0

-- Identificar el Ultimo Nivel de Aprobacion de RQ
Select @n_NumNiv = COUNT(*) From REQ_APROB_ORDCOM_AOC
where cia_codcia=@p_CodCia and occ_codepk=@p_NumOC and aoc_indapr=0
-- Si es UNO entonces es el ultimo nivel y se aprueba OC
If @@ERROR <> 0 
Begin
	Set @s_mensaje = 'Error al Consultar datos de los Niveles de APROBACION de OC ' 
	Raiserror(@s_mensaje,16,1)
	Select -4 as Cod_Resultado, @s_Mensaje as Des_Resultado
	Return -4
End

If @n_NumNiv<=0
Begin
   Set @s_mensaje = 'NO hay niveles pendientes de APROBACION de OC '
   Select -3 as Cod_Resultado, @s_Mensaje as Des_Resultado
   Return -3
End

Select @n_NivUsr = COUNT(*) from REQ_APROB_ORDCOM_AOC
Where cia_codcia=@p_CodCia and suc_codsuc=@p_CodSuc and occ_codepk=@p_NumOC and uap_codepk=@p_CodUsr and aoc_indapr=0
If Isnull(@n_NivUsr,0)<=0
Begin
   Set @s_mensaje = 'NO hay niveles pendientes de APROBACION de OC para el USUARIO'
   Select 0 as Cod_Resultado, @s_Mensaje as Des_Resultado
   Return 0
End

-- Identificar si el aprobador es un usuario que de VISTO a las solicitudes
Select @n_NumVis = COUNT(*) From REQ_APROB_ORDCOM_AOC
where cia_codcia=@p_CodCia and occ_codepk=@p_NumOC and tac_codtac in ('2','4')
-- Si es mayor o igual UNO entonces se tiene usuario que da Visto no interesa cuantos sean
If @@ERROR <> 0 
Begin
	Set @s_mensaje = 'Error al Consultar datos de los Usuarios que dan VISTO en la Solicitud de Compra relacionada' 
	Raiserror(@s_mensaje,16,1)
	Select -5 as Cod_Resultado, @s_Mensaje as Des_Resultado
	Return -5
End
 
/*
-- Identificar la Solicitud de Compra 
Select @c_numscc = scc_numscc from ORDEN_COMPRA_OCC
Where cia_codcia=@p_CodCia and suc_codsuc=@p_CodSuc and ocm_corocm=@p_NumOC
If Len(Isnull(@c_NumScc,''))<=0
Begin
   Set @s_mensaje = 'NO hay Solicitud de Compra relacionada con esta Orden de Compra, revise tabla de Solicitud de Compra'
   Select -6 as Cod_Resultado, @s_Mensaje as Des_Resultado
   Return -6
End
   */
Begin Transaction APRUEBA

-- Aprobar en el nivel del Usuario
-- Select * From APROBAC_ORDCOM_APROBACIONES_AOA
Update REQ_APROB_ORDCOM_AOC Set aoc_indapr=1, aoc_fecact=getdate(), aoc_codusu=SYSTEM_USER 
Where cia_codcia=@p_CodCia and suc_codsuc=@p_CodSuc and occ_codepk=@p_NumOC
and uap_codepk=@p_CodUsr and anm_codanm = 
(Select min(anm_codanm) from REQ_APROB_ORDCOM_AOC 
 Where cia_codcia=@p_CodCia and suc_codsuc=@p_CodSuc and occ_codepk=@p_NumOC and uap_codepk=@p_CodUsr and aoc_indapr=0)
 
If @@ERROR <> 0 
Begin
	Set @s_mensaje = 'Error al APROBAR la Orden de Compra tabla APROBAC_ORDCOM_APROBACIONES_AOA ' 
	Raiserror(@s_mensaje,16,1)
	Rollback Transaction APRUEBA
	Select -2 as Cod_Resultado, @s_Mensaje as Des_Resultado
	Return -2
End


/*
-- Colocar el Visto en la Solicitud
If @n_NumVis>=1
Begin
   -- Actualizar el Visto en la Solicitud de Compra
   If Len(Isnull(@c_NumScc,''))>0
   Begin
      Update Solicitud_Compra_Scc Set scc_indfir='1'
      Where cia_codcia=@p_CodCia and suc_codsuc=@p_CodSuc and scc_numscc=@c_numscc
   End
   If @@ERROR <> 0 
   Begin
    	Set @s_mensaje = 'Error al Actualizar indicador de Firma de Solicitud de compra relacionada SOLICITUD_COMPRA_SCC (USUARIO que da el Visto) ' 
    	Raiserror(@s_mensaje,16,1)
    	Rollback Transaction APRUEBA
    	Select -1 as Cod_Resultado, @s_Mensaje as Des_Resultado
    	Return -1
   End
End
*/


-- Aprobar la OC
-- 1 => Pendiente / 2 => Aprobado / 3 => Rechazado
If @n_NumNiv=1
Begin
   Update OCOMPRA_OCC Set occ_sitapr='1', occ_estado = '1'
   Where cia_codcia=@p_CodCia and suc_codsuc=@p_CodSuc and occ_codepk=@p_NumOC
   If @@ERROR <> 0 
   Begin
    	Set @s_mensaje = 'Error al APROBAR cabecera de Orden de Compra ORDEN_COMPRA_OCC ' 
    	Raiserror(@s_mensaje,16,1)
    	Rollback Transaction APRUEBA
    	Select -1 as Cod_Resultado, @s_Mensaje as Des_Resultado
    	Return -1
   End
End
/*
   -- Cuando se Aprueba Totalmente se coloca el nivel en 2
   If Len(Isnull(@c_NumScc,''))>0
   Begin
      Update Solicitud_Compra_Scc Set scc_indfir='2'
      Where cia_codcia=@p_CodCia and suc_codsuc=@p_CodSuc and scc_numscc=@c_numscc
   End
   If @@ERROR <> 0 
   Begin
    	Set @s_mensaje = 'Error al Actualizar indicador de Firma de Solicitud de compra relacionada SOLICITUD_COMPRA_SCC ' 
    	Raiserror(@s_mensaje,16,1)
    	Rollback Transaction APRUEBA
    	Select -1 as Cod_Resultado, @s_Mensaje as Des_Resultado
    	Return -1
   End
   
End


Insert into COMPRAS_LOCALES_AUDITORIA_CLA (CIA_CODCIA, CLA_FECCLA, CLA_DESCLA, S10_CODUSU, CLA_MOTCLA, CLA_TIPCLA)
Values (@p_CodCia,GETDATE(),'APROBACION DE LA ORDEN DE COMPRA: '+@p_NumOC,current_user,'','O')
If @@ERROR <> 0 
Begin
  	Set @s_mensaje = 'Error al actualizar AUDITORIA de APROBACION COMPRAS_LOCALES_AUDITORIA_CLA ' + char(13) + ERROR_MESSAGE();
  	Raiserror(@s_mensaje,16,1)
  	Rollback Transaction APRUEBA
   	Select -5 as Cod_Resultado, @s_Mensaje as Des_Resultado
   	Return -5;
End				

-- Enviar MAIL de APROBACION
if @n_NumNiv=1
Begin
   Exec PA_HD_WEB_OC_Envio_Mail @p_CodCia=@p_CodCia, @p_CodSuc=@p_CodSuc, @p_NumOC=@p_NumOC, @p_TipAvi=3, @p_Motivo='', @p_User_Envia=@p_CodUsr
End   
Else
Begin
   Exec PA_HD_WEB_OC_Envio_Mail @p_CodCia=@p_CodCia, @p_CodSuc=@p_CodSuc, @p_NumOC=@p_NumOC, @p_TipAvi=0, @p_Motivo='', @p_User_Envia=@p_CodUsr
End   
If @@ERROR <> 0 
Begin
  	Set @s_mensaje = 'Error al enviar mail de EVALUACION ' + char(13) + ERROR_MESSAGE();
  	Raiserror(@s_mensaje,16,1)
  	Rollback Transaction APRUEBA
   	Select -7 as Cod_Resultado, @s_Mensaje as Des_Resultado
   	Return -7;
End			
*/
Commit Transaction APRUEBA

Select 1 as Cod_Resultado, 'APROBACION EXITOSA' as Des_Resultado

