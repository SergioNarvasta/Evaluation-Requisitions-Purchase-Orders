CREATE TABLE [dbo].[REQ_REQUI_COMPRA_RCO](
	[cia_codcia] [smallint] NOT NULL,
	[suc_codsuc] [smallint] NOT NULL,
	[rco_codepk] [int] NOT NULL,
	[rco_numrco] [char](12) NOT NULL,
	[tin_codtin] [smallint] NOT NULL,
	[rco_motivo] [varchar](50) NULL,
	[rco_glorco] [varchar](50) NULL,
	[cco_codepk] [int] NULL,
	[rco_sitrco] [char](1) NULL,
	[rco_codusu] [char](10) NULL,
	[ung_codepk] [int] NULL,
	[rco_indval] [int] NULL,
	[rco_indest] [char](1) NULL,
	[rco_rembls] [char](1) NULL,
	[rco_presup] [char](1) NULL,
	[rco_priori] [char](1) NULL,
	[tre_codepk] [int] NULL,
	[rco_estado] [char](1) NULL,
	[dis_codepk] [int] NULL,
	[uap_codepk] [int] NULL,
	[occ_codepk] [int] NULL,
	[rco_fecreg] [datetime] NULL,
	[ocm_corocm] [char](15) NULL,
	[rco_obspri] [varchar](50) NULL,
	[ano_codano] [char](4) NULL,
	[mes_codmes] [char](2) NULL,
	[rco_fecapr] [datetime] NULL,
	[usu_codapr] [varchar](30) NULL,
 CONSTRAINT [PK_REQ_REQUI_COMPRA_RCO_1] PRIMARY KEY CLUSTERED 
);
GO

CREATE TABLE [dbo].[DISCIPLINAS_DIS](
	[cia_codcia] [smallint] NOT NULL,
	[dis_codepk] [int] IDENTITY(1,1) NOT NULL,
	[dis_coddis] [char](10) NULL,
	[dis_nomlar] [varchar](50) NULL,
	[dis_estado] [char](1) NULL,
 CONSTRAINT [PK_DISCIPLINAS_DIS] PRIMARY KEY CLUSTERED 
(
	[dis_codepk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[UNID_NEGOCIO_UNG](
	[cia_codcia] [smallint] NOT NULL,
	[ung_codepk] [int] IDENTITY(1,1) NOT NULL,
	[ung_deslar] [varchar](50) NOT NULL,
	[ung_codung] [char](10) NOT NULL,
	[ung_estado] [char](1) NULL,
 CONSTRAINT [PK_UNID_NEGOCIO_UNG] PRIMARY KEY CLUSTERED 
(
	[ung_codepk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[REQ_TIPO_REQUISICION_TRE](
	[cia_codcia] [smallint] NOT NULL,
	[tre_codepk] [int] IDENTITY(1,1) NOT NULL,
	[tre_codtre] [char](10) NOT NULL,
	[tre_deslar] [varchar](50) NOT NULL,
	[tre_estado] [char](1) NULL,
 CONSTRAINT [PK_REQ_TIPO_REQUISICION_TRE] PRIMARY KEY CLUSTERED 
(
	[tre_codepk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[REQ_USERS_APROBADORES_UAP](
	[cia_codcia] [smallint] NULL,
	[uap_codepk] [int] IDENTITY(1,1) NOT NULL,
	[uap_descor] [varchar](50) NULL,
	[uap_deslar] [varchar](50) NULL,
	[uap_estado] [char](1) NULL,
	[uap_nivusu] [char](10) NULL,
	[uap_coduap] [char](10) NULL,
 CONSTRAINT [PK_REQ_USERS_APROBADORES_UAP] PRIMARY KEY CLUSTERED 
(
	[uap_codepk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[REQ_REQUI_FILES_RCF](
	[cia_codcia] [smallint] NULL,
	[rco_codepk] [int] NULL,
	[rcf_corite] [char](6) NOT NULL,
	[rcf_codarc] [varchar](30) NULL,
	[rcf_nomarc] [varchar](30) NULL,
	[rcf_file] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[REQ_REQUI_COMPRA_RCD](
	[rco_codepk] [int] NOT NULL,
	[cia_codcia] [smallint] NULL,
	[suc_codsuc] [smallint] NULL,
	[rcd_corite] [char](6) NOT NULL,
	[prd_codepk] [int] NULL,
	[rcd_desprd] [varchar](50) NULL,
	[rcd_glorcd] [varchar](50) NULL,
	[ume_codepk] [int] NULL,
	[rcd_canapr] [numeric](10, 2) NULL,
	[rcd_canate] [numeric](18, 0) NULL,
	[ccr_codepk] [int] NULL,
	[rcd_canreq] [numeric](18, 0) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[REQ_APROB_REQCOM_ARC](
	[arc_codepk] [int] IDENTITY(1,1) NOT NULL,
	[cia_codcia] [smallint] NOT NULL,
	[suc_codsuc] [smallint] NOT NULL,
	[rco_codepk] [int] NOT NULL,
	[arc_coraoa] [smallint] NOT NULL,
	[uap_codepk] [int] NOT NULL,
	[anm_codanm] [smallint] NOT NULL,
	[arc_indapr] [smallint] NOT NULL,
	[arc_porapr] [smallint] NOT NULL,
	[arc_fecact] [datetime] NOT NULL,
	[arc_codusu] [varchar](30) NOT NULL,
	[tac_codtac] [char](1) NOT NULL,
	[arc_indenv] [tinyint] NULL,
	[arc_fecenv] [datetime] NULL,
	[mao_codeve] [char](4) NULL,
PRIMARY KEY CLUSTERED 
(
	[arc_codepk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[REQ_APROB_REQCOM_ARC] ADD  CONSTRAINT [DF_APROBAC_ORDCOM_APROBACIONES_ARC_INDENV]  DEFAULT ((0)) FOR [arc_indenv]
GO

ALTER TABLE [dbo].[REQ_APROB_REQCOM_ARC]  WITH CHECK ADD  CONSTRAINT [FK_ARC_SUCURSAL_CIA] FOREIGN KEY([cia_codcia], [suc_codsuc])
REFERENCES [dbo].[SUCURSAL_SUC] ([cia_codcia], [suc_codsuc])
GO

ALTER TABLE [dbo].[REQ_APROB_REQCOM_ARC] CHECK CONSTRAINT [FK_ARC_SUCURSAL_CIA]
GO


CREATE TABLE [dbo].[OCOMPRA_OCC](
	[cia_codcia] [smallint] NOT NULL,
	[occ_codepk] [int] NOT NULL,
	[suc_codsuc] [smallint] NOT NULL,
	[toc_codtoc] [char](1) NOT NULL,
	[occ_numero] [char](10) NOT NULL,
	[eoc_codepk] [int] NULL,
	[occ_fecemi] [datetime] NOT NULL,
	[ano_codano] [smallint] NOT NULL,
	[mes_codmes] [smallint] NOT NULL,
	[occ_feccad]  AS (CONVERT([char],[occ_fecemi],(112))),
	[occ_percad]  AS (CONVERT([char](6),[occ_fecemi],(112))),
	[occ_igvocc] [numeric](18, 2) NOT NULL,
	[occ_tcaocc] [numeric](18, 4) NOT NULL,
	[ccr_codepk] [int] NOT NULL,
	[occ_contac] [int] NULL,
	[tco_codtco] [smallint] NOT NULL,
	[mon_codepk] [tinyint] NOT NULL,
	[cpg_codepk] [int] NOT NULL,
	[are_codepk] [smallint] NOT NULL,
	[occ_autori] [int] NOT NULL,
	[occ_respon] [int] NOT NULL,
	[occ_compra] [int] NULL,
	[occ_caldst] [char](1) NOT NULL,
	[oci_codoci] [char](1) NOT NULL,
	[occ_calimp] [char](1) NOT NULL,
	[occ_sitapr] [char](1) NOT NULL,
	[occ_sitalm] [char](1) NOT NULL,
	[occ_sitcon] [char](1) NOT NULL,
	[occ_ctrkdx] [smallint] NOT NULL,
	[occ_observ] [text] NULL,
	[occ_sinpre] [smallint] NOT NULL,
	[occ_impbru] [numeric](18, 2) NOT NULL,
	[occ_impdes] [numeric](18, 2) NOT NULL,
	[occ_impnet] [numeric](18, 2) NOT NULL,
	[occ_impigv] [numeric](18, 2) NOT NULL,
	[occ_imptot] [numeric](18, 2) NOT NULL,
	[evc_codepk] [int] NULL,
	[trt_codepk] [int] NULL,
	[occ_regman] [smallint] NOT NULL,
	[imp_codepk] [int] NULL,
	[occ_pordet] [numeric](12, 2) NOT NULL,
	[occ_impdet] [numeric](18, 2) NOT NULL,
	[occ_indmaq] [smallint] NOT NULL,
	[occ_netfac] [numeric](12, 2) NULL,
	[occ_estado] [dbo].[Estado] NOT NULL,
	[occ_usucre] [dbo].[usuario] NOT NULL,
	[occ_feccre] [dbo].[fecha] NOT NULL,
	[occ_usuact] [dbo].[usuario] NOT NULL,
	[occ_fecact] [dbo].[fecha] NOT NULL,
	[inc_codepk] [int] NULL,
	[lin_codepk] [int] NULL,
	[occ_tcaotm] [numeric](16, 8) NULL,
	[cot_numepk] [int] NULL,
	[occ_indlot] [smallint] NOT NULL,
	[lot_codlot] [varchar](25) NULL,
	[con_codepk] [int] NULL,
 CONSTRAINT [PK_OCOMPRA_OCC] PRIMARY KEY CLUSTERED 
(
	[cia_codcia] ASC,
	[occ_codepk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CENT_COST_CCO](
	[cia_codcia] [smallint] NOT NULL,
	[cco_codepk] [smallint] NOT NULL,
	[cco_codcco] [char](6) NOT NULL,
	[cco_descco] [varchar](50) NOT NULL,
	[cco_descor] [varchar](20) NOT NULL,
	[gcc_codgcc] [smallint] NULL,
	[cco_estado] [char](1) NOT NULL,
	[cco_usuact] [varchar](50) NOT NULL,
	[cco_fecact] [datetime] NOT NULL,
 CONSTRAINT [PK_CENT_COST_CCO] PRIMARY KEY CLUSTERED 
(
	[cia_codcia] ASC,
	[cco_codepk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[COMPANIA_CIA](
	[cia_codcia] [smallint] NOT NULL,
	[cia_nomcia] [varchar](100) NOT NULL,
	[cia_nomcor] [varchar](30) NOT NULL,
	[cia_prueba] [char](1) NOT NULL,
	[mon_codepk] [tinyint] NULL,
	[ccr_codepk] [int] NULL,
	[cia_estado] [dbo].[Estado] NOT NULL,
	[cia_usucre] [dbo].[usuario] NOT NULL,
	[cia_feccre] [dbo].[fecha] NOT NULL,
	[cia_usuact] [dbo].[usuario] NOT NULL,
	[cia_fecact] [dbo].[fecha] NOT NULL,
 CONSTRAINT [PK_COMPANIA_CIA] PRIMARY KEY CLUSTERED 
(
	[cia_codcia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[SUCURSAL_SUC](
	[cia_codcia] [smallint] NOT NULL,
	[suc_codsuc] [smallint] NOT NULL,
	[suc_nomsuc] [varchar](100) NOT NULL,
	[suc_nomcor] [varchar](30) NOT NULL,
	[pvt_codepk] [smallint] NULL,
	[suc_estado] [dbo].[Estado] NOT NULL,
	[suc_usucre] [dbo].[usuario] NOT NULL,
	[suc_feccre] [dbo].[fecha] NOT NULL,
	[suc_usuact] [dbo].[usuario] NOT NULL,
	[suc_fecact] [dbo].[fecha] NOT NULL,
 CONSTRAINT [PK_SUCURSAL_SUC] PRIMARY KEY CLUSTERED 
(
	[cia_codcia] ASC,
	[suc_codsuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO