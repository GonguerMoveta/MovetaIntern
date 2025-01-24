
Create Table [dbo].[ABSCHLVOR] ( 
	[AVARZTNR]	bigint   NULL ,
	[AVSALDO]	decimal(10, 2)   NULL ,
	[AVZAHLDM]	decimal(10, 2)   NULL ,
	[AVBANK]	varchar(5)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[AERZTE] ( 
	[AARZTNR]	bigint NOT NULL ,
	[ANAME1]	varchar(50)   NULL ,
	[ANAME2]	varchar(50)   NULL ,
	[ASTR]	varchar(50)   NULL ,
	[AORT]	varchar(50)   NULL ,
	[ABANK]	varchar(25)   NULL ,
	[ABLZ]	varchar(11)   NULL ,
	[AKONTO]	varchar(11)   NULL ,
	[AEINTR]	date   NULL ,
	[AABSCHGR]	decimal(8, 2)   NULL ,
	[AKZPRAXART]	varchar(1) NOT NULL ,
	[AFAELLTG]	smallint   NULL ,
	[AKZMAHNDM]	varchar(1) NOT NULL ,
	[ATITEL]	varchar(30)   NULL ,
	[ANR2]	varchar(4)   NULL ,
	[ASTAFFEL]	smallint NOT NULL ,
	[AKZMB]	varchar(1)   NULL ,
	[AAUSTRITT]	date   NULL ,
	[AGESTORBEN]	date   NULL ,
	[AGEBDM]	decimal(8, 2)   NULL ,
	[AMAHNINT]	smallint   NULL ,
	[AEINLAGE]	decimal(8, 2)   NULL ,
	[ABEITRAG]	decimal(8, 2)   NULL ,
	[AMGAUSBUCHEN]	smallint   NULL ,
	[AKLEINBETRAG]	decimal(5, 2)   NULL ,
	[AKZGEBSTAF]	smallint   NULL ,
	[AVORRECH]	decimal(10, 2)   NULL ,
	[APROZ1]	decimal(4, 2)   NULL ,
	[APROZ2]	decimal(4, 2)   NULL ,
	[APROZ3]	decimal(4, 2)   NULL ,
	[AVORSOLL]	decimal(10, 2)   NULL ,
	[AVORHABEN]	decimal(10, 2)   NULL ,
	[AVORMWST]	decimal(10, 2)   NULL ,
	[AVORVST]	decimal(10, 2)   NULL ,
	[ADRUCKE]	varchar(40)   NULL ,
	[AMULTI]	smallint   NULL ,
	[AMATCH]	varchar(8)   NULL ,
	[ADISKABR]	smallint   NULL ,
	[ACOMPEIG]	smallint   NULL ,
	[APRAXART]	varchar(1)   NULL ,
	[AAKTIV]	smallint   NULL ,
	[ATAANZ]	smallint   NULL ,
	[AGKTYP]	varchar(1)   NULL ,
	[AMEMO]	varchar(max)   NULL ,
	[ALOGO]	varchar(8)   NULL ,
	[ASKONTO]	decimal(4, 2)   NULL ,
	[ARECHINFO]	varchar(max)   NULL ,
	[AMYCO]	integer   NULL ,
	[AEMAIL]	varchar(128)   NULL ,
	[AVORSCHPROZ]	decimal(2, 0)   NULL ,
	[AALLGINFO]	varchar(max)   NULL ,
	[ATELPRAXIS]	varchar(25)   NULL ,
	[ATELHANDY]	varchar(25)   NULL ,
	[ATELPRIVAT]	varchar(25)   NULL ,
	[ATELFAX]	varchar(25)   NULL ,
	[AMINDGUTH]	decimal(9, 2)   NULL ,
	[AITMEMO]	varchar(max)   NULL ,
	[AITEI]	smallint   NULL ,
	[AITTV]	smallint   NULL ,
	[AITPALM]	varchar(12)   NULL ,
	[AITBRIEF]	smallint   NULL ,
	[AITMULTI]	smallint   NULL ,
	[AITGESTSEIT]	date   NULL ,
	[AITLTZTBELAST]	date   NULL ,
	[AITBELASTDM]	decimal(8, 2)   NULL ,
	[ASEHRGEEHRTE]	varchar(50)   NULL ,
	[AUSTID]	varchar(30)   NULL ,
	[ARAABGMS]	smallint   NULL ,
	[ARAABGAUS]	smallint   NULL ,
	[ARUNDSCHREIBEN]	smallint   NULL ,
	[AEMAILTVS]	varchar(128)   NULL ,
	[AEMAILNEWS]	varchar(128)   NULL ,
	[ASVORT]	varchar(50)   NULL ,
	[ASVSTR]	varchar(50)   NULL ,
	[ASVNAME2]	varchar(50)   NULL ,
	[ASVNAME1]	varchar(50)   NULL ,
	[ASVTITEL]	varchar(30)   NULL ,
	[ABILD]	varchar(max)   NULL ,
	[AKATITEL]	varchar(30)   NULL ,
	[AKANAME1]	varchar(50)   NULL ,
	[AKANAME2]	varchar(50)   NULL ,
	[AKASTR]	varchar(50)   NULL ,
	[AKAORT]	varchar(50)   NULL ,
	[ARAZA]	smallint   NULL ,
	[ATVNKONTO]	integer   NULL ,
	[AREGNR]	varchar(12)   NULL ,
	[ARZZINS]	decimal(5, 1)   NULL ,
	[ALAND]	varchar(2)   NULL ,
	[ASVLAND]	varchar(2)   NULL ,
	[AKALAND]	varchar(2)   NULL ,
	[ARAZANEU]	smallint   NULL ,
	[AKEINGELD]	smallint   NULL ,
	[APMEMAIL]	varchar(250)   NULL ,
	[AIBAN]	varchar(34)   NULL ,
	[ABIC]	varchar(11)   NULL ,
	[ASPERRINFO]	varchar(max)   NULL ,
	[AITSOFTWARE]	varchar(50)   NULL ,
	[AGFKRIT]	varchar(max)   NULL ,
	[ABEZTAGE]	smallint   NULL ,
	[AKAPPUNG]	decimal(8, 2)   NULL ,
	[AUSTID2]	varchar(20)   NULL ,
	[AEWAABEUR]	decimal(10, 2)   NULL ,
	[AEWAVARIANTE]	smallint   NULL ,
	[ANEUENR]	smallint   NULL ,
	[AZAHLERINN]	smallint   NULL ,
	[AZETEXT]	varchar(max)   NULL ,
	[AZEFRIST]	smallint   NULL ,
	[ARECHTEXT]	varchar(max)   NULL ,
	[AABRKEINGUTHABEN]	smallint   NULL ,
	[AWOMO]	smallint   NULL ,
	[AWODI]	smallint   NULL ,
	[AWOMI]	smallint   NULL ,
	[AWODO]	smallint   NULL ,
	[AWOFR]	smallint   NULL ,
	[ATM]	smallint   NULL ,
	[AWOTAGLTZT]	date   NULL ,
	[AABRKEINDARLEHENBEIABRG]	smallint   NULL ,
	[ALTZTABSCHTAG]	date   NULL ,
	[ARAZAKLEINBETRAG]	decimal(5, 2)   NULL ,
	[AABTRETUNG]	smallint   NULL ,
	[AMAILAKTEERL]	smallint   NULL ,
	[AVORSCHUSS1]	smallint   NULL ,
	[AVORSCHUSS2]	smallint   NULL ,
	[AVORSCHUSS3]	smallint   NULL ,
	[AVORSCHUSS6]	smallint   NULL ,
	[AVORSCHUSS7]	smallint   NULL ,
	[AVORSCHUSS9]	smallint   NULL ,
	[AVORSCHUSS1WOCHEN]	smallint   NULL ,
	[AVORSCHUSS9CR]	smallint   NULL ,
	[ASTEUERNR]	varchar(50)   NULL ,
	[AAPIKEY]	varchar(50)   NULL ,
	[AAPISECRET]	varchar(50)   NULL ,
	[ADATEVBERATERNR]	varchar(8)   NULL ,
	[ADATEVMANDANTENNR]	varchar(8)   NULL ,
	[ADATEVCODE]	varchar(5)   NULL ,
	[ADATEVMAIL]	varchar(100)   NULL ,
	[ATMEIN]	smallint   NULL ,
	[AVORMAHN]	decimal(10, 2)   NULL ,
	[AMGAUSBUCHENMTL]	smallint   NULL ,
	[AGFMEMO]	varchar(max)   NULL ,
	[AANDERENUMMERN]	varchar(100)   NULL ,
	[ABILANZIERER]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[AERZTEANSPRECH] ( 
	[AAARZTNR]	bigint   NULL ,
	[AANAME]	varchar(250)   NULL ,
	[AARUFNR]	varchar(100)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[AERZTEPERSONEN] ( 
	[APARZTNR]	bigint   NULL ,
	[APNAME]	varchar(100)   NULL ,
	[APGESCHLECHT]	varchar(1)   NULL ,
	[APGEBDAT]	date   NULL ,
	[APEINTRDAT]	date   NULL ,
	[APERFDAT]	datetime   NULL ,
	[APART]	varchar(20)   NULL ,
	[APUMSATZ]	decimal(10, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[AKTIONEN] ( 
	[AKARZTNR]	bigint   NULL ,
	[AKHALTERNR]	bigint   NULL ,
	[AKRECHNR]	integer   NULL ,
	[AKDATUM]	datetime   NULL ,
	[AKTYP]	varchar(2)   NULL ,
	[AKTYPDETAIL]	varchar(6)   NULL ,
	[AKANZAHL]	smallint   NULL ,
	[AKPROZGEB]	decimal(5, 2)   NULL ,
	[AKDM]	decimal(10, 2)   NULL ,
	[AKRECHSUMM]	decimal(10, 2)   NULL ,
	[AKPROZNR]	integer   NULL ,
	[AKZUST]	date   NULL ,
	[AKTERMIN]	date   NULL ,
	[AKDAUER]	smallint   NULL ,
	[AKZEICHEN]	varchar(2)   NULL ,
	[AKRZNR]	integer   NULL ,
	[AKDEAKTIV]	smallint   NULL ,
	[AKABRID]	varchar(36)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[AKTIONENA] ( 
	[AKARZTNR]	bigint   NULL ,
	[AKHALTERNR]	bigint   NULL ,
	[AKRECHNR]	integer   NULL ,
	[AKDATUM]	datetime   NULL ,
	[AKTYP]	varchar(2)   NULL ,
	[AKTYPDETAIL]	varchar(6)   NULL ,
	[AKANZAHL]	smallint   NULL ,
	[AKPROZGEB]	decimal(5, 2)   NULL ,
	[AKDM]	decimal(10, 2)   NULL ,
	[AKRECHSUMM]	decimal(10, 2)   NULL ,
	[AKPROZNR]	integer   NULL ,
	[AKZUST]	date   NULL ,
	[AKTERMIN]	date   NULL ,
	[AKDAUER]	smallint   NULL ,
	[AKZEICHEN]	varchar(2)   NULL ,
	[AKRZNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[ANWEISUNGSBELEGE] ( 
	[ABARZTNR]	bigint   NULL ,
	[ABSOLL]	integer   NULL ,
	[ABHABEN]	integer   NULL ,
	[ABBUCHTEXT]	varchar(50)   NULL ,
	[ABDM]	decimal(8, 2)   NULL ,
	[ABERFDAT]	datetime   NULL ,
	[ABDRUDAT]	datetime   NULL ,
	[ABZEI]	varchar(2)   NULL ,
	[ABMWST]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[AUFSCHLAG] ( 
	[AUARZTNR]	bigint NOT NULL ,
	[AUGRENZE]	decimal(9, 2)   NULL ,
	[AUDM]	decimal(9, 2)   NULL ,
	[AUKZAB]	smallint   NULL ,
	[AURGTEXT]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BANKCLEARING] ( 
	[BCARZTNR]	bigint   NULL ,
	[BCHALTERNR]	bigint   NULL ,
	[BCRECHNR]	integer   NULL ,
	[BCZAHLDM]	decimal(10, 2)   NULL ,
	[BCBUCHDM]	decimal(10, 2)   NULL ,
	[BCBUCHDAT]	date   NULL ,
	[BCRESTL]	decimal(10, 2)   NULL ,
	[BCRESTA]	decimal(10, 2)   NULL ,
	[BCRESTM]	decimal(10, 2)   NULL ,
	[BCRESTZ]	decimal(10, 2)   NULL ,
	[BCKONTO]	varchar(5)   NULL ,
	[BCERGEBNIS]	varchar(40)   NULL ,
	[BCAUSZUG]	smallint   NULL ,
	[BCREF]	varchar(16)   NULL ,
	[BCTEXTSCHL]	smallint NOT NULL ,
	[BCVZ1]	varchar(100)   NULL ,
	[BCVZ2]	varchar(100)   NULL ,
	[BCAUFTR1]	varchar(27)   NULL ,
	[BCAUFTR2]	varchar(27)   NULL ,
	[BCVZ3]	varchar(100)   NULL ,
	[BCVZ4]	varchar(100)   NULL ,
	[BCVZ5]	varchar(100)   NULL ,
	[BCVZ6]	varchar(100)   NULL ,
	[BCRZNR]	integer   NULL ,
	[BCVWZ]	varchar(max)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]


ALTER TABLE [dbo].[BANKCLEARING] ADD
	DEFAULT 0 FOR [BCTEXTSCHL] 
 
GO


Create Table [dbo].[BANKEINZUG] ( 
	[BEZBEARBEITER]	varchar(10)   NULL ,
	[BEZBEARBDATUM]	datetime   NULL ,
	[BEZARZTNR]	bigint   NULL ,
	[BEZHALTERNR]	bigint   NULL ,
	[BEZRECHNR]	integer   NULL ,
	[BEZDM]	decimal(10, 2)   NULL ,
	[BEZERLDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BANKENSTAMM] ( 
	[BSNR]	smallint NOT NULL ,
	[BSNAME]	varchar(20)   NULL ,
	[BSBLZ]	varchar(11)   NULL ,
	[BSKONTO]	varchar(11)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BASISZINSEN] ( 
	[BZVON]	date   NULL ,
	[BZBIS]	date   NULL ,
	[BZZINS]	decimal(5, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BATCHBUCH] ( 
	[BBARZTNR]	bigint   NULL ,
	[BBHALTERNR]	bigint   NULL ,
	[BBRECHNR]	integer   NULL ,
	[BBZAHLDM]	decimal(10, 2)   NULL ,
	[BBBUCHDAT]	date   NULL ,
	[BBKONTO]	varchar(5)   NULL ,
	[BBBELNR]	integer   NULL ,
	[BBPROZNR]	integer   NULL ,
	[BBEXPORTNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BATCHPROT] ( 
	[BPARZTNR]	bigint   NULL ,
	[BPHALTERNR]	bigint   NULL ,
	[BPRECHNR]	integer   NULL ,
	[BPZAHLDM]	decimal(10, 2)   NULL ,
	[BPBUCHDM]	decimal(10, 2)   NULL ,
	[BPBUCHDAT]	date   NULL ,
	[BPRESTL]	decimal(10, 2)   NULL ,
	[BPRESTA]	decimal(10, 2)   NULL ,
	[BPRESTM]	decimal(10, 2)   NULL ,
	[BPRESTZ]	decimal(10, 2)   NULL ,
	[BPKONTO]	varchar(5)   NULL ,
	[BPERGEBNIS]	varchar(40)   NULL ,
	[BPBELNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BEHANDLUNGSIDS] ( 
	[BIARZTNR]	bigint   NULL ,
	[BIRECHNR]	integer   NULL ,
	[BIID]	varchar(19)   NULL ,
	[BIERFDAT]	datetime   NULL ,
	[BIUEBDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BEIHILFEN] ( 
	[BEREGARZT]	varchar(20)   NULL ,
	[BEREGHALTER]	varchar(20)   NULL ,
	[BENAME]	varchar(50)   NULL ,
	[BEDATE]	date   NULL ,
	[BEANZAHL]	integer   NULL ,
	[BEBETRAG]	decimal(8, 2)   NULL ,
	[BEERFDAT]	datetime   NULL ,
	[BEERLDAT]	datetime   NULL ,
	[BEGUID]	varchar(36)   NULL ,
	[BESTATUS]	varchar(50)   NULL ,
	[BEARZTNR]	bigint   NULL ,
	[BERECHNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BENUTZER] ( 
	[BNCODE]	varchar(8)   NULL ,
	[BNPW]	varchar(8)   NULL ,
	[BNNAME]	varchar(50)   NULL ,
	[BNABTLG]	varchar(2)   NULL ,
	[BNTEL]	varchar(2)   NULL ,
	[BNADMIN]	smallint   NULL ,
	[BNZEI]	varchar(2)   NULL ,
	[BNEMAIL]	varchar(50)   NULL ,
	[BNVERTR]	varchar(8)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BEZDATEN] ( 
	[BEZDARZTNR]	bigint   NULL ,
	[BEZDHALTERNR]	bigint   NULL ,
	[BEZDRECHNR]	integer   NULL ,
	[BEZDERFDAT]	datetime   NULL ,
	[BEZDBLZ]	varchar(8)   NULL ,
	[BEZDKONTO]	varchar(10)   NULL ,
	[BEZDDM]	decimal(8, 2)   NULL ,
	[BEZDERLDAT]	datetime   NULL ,
	[BEZDIBAN]	varchar(34)   NULL ,
	[BEZDID]	varchar(50)   NULL ,
	[BEZDSIGNDAT]	date   NULL ,
	[BEZDBIC]	varchar(11)   NULL ,
	[BEZDDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BEZSTAMM] ( 
	[BEZSARZTNR]	bigint   NULL ,
	[BEZSHALTERNR]	bigint   NULL ,
	[BEZSRECHNR]	integer   NULL ,
	[BEZSERFDAT]	datetime   NULL ,
	[BEZSBLZ]	varchar(8)   NULL ,
	[BEZSKONTO]	varchar(10)   NULL ,
	[BEZSMAXDM]	decimal(8, 2)   NULL ,
	[BEZSERLDAT]	datetime   NULL ,
	[BEZSLTZTEINZUG]	date   NULL ,
	[BEZSTAG]	smallint   NULL ,
	[BEZSABDATUM]	date   NULL ,
	[BEZSIBAN]	varchar(34)   NULL ,
	[BEZSSIGNDAT]	date   NULL ,
	[BEZSBIC]	varchar(11)   NULL ,
	[BEZSANZRETOURE]	smallint   NULL ,
	[BEZSANZSEITRETOURE]	smallint   NULL ,
	[BEZSDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BLZ] ( 
	[BLZBLZ]	varchar(8)   NULL ,
	[BLZNAME]	varchar(50)   NULL ,
	[BLZBIC]	varchar(11)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BUCHART] ( 
	[BANR]	smallint NOT NULL ,
	[BABUART]	varchar(5)   NULL ,
	[BAKZ1]	smallint   NULL ,
	[BADM1]	decimal(8, 2)   NULL ,
	[BASTKZARZT1]	smallint   NULL ,
	[BASTKZABRST1]	smallint   NULL ,
	[BAKTOSOLL1]	smallint   NULL ,
	[BAKTOHABEN1]	smallint   NULL ,
	[BAKZ2]	smallint   NULL ,
	[BADM2]	decimal(8, 2)   NULL ,
	[BAKTOSOLL2]	smallint   NULL ,
	[BAKTOHABEN2]	smallint   NULL ,
	[BAKZ3]	smallint   NULL ,
	[BADM3]	decimal(8, 2)   NULL ,
	[BASTKZABRST3]	smallint   NULL ,
	[BAKTOSOLL3]	smallint   NULL ,
	[BAKTOHABEN3]	smallint   NULL ,
	[BABUCHTEXT]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[BUCHBEL] ( 
	[BUBBEARBEITER]	varchar(10)   NULL ,
	[BUBBEARBDATUM]	datetime   NULL ,
	[BUBARZTNR]	bigint   NULL ,
	[BUBSOLL]	integer   NULL ,
	[BUBHABEN]	integer   NULL ,
	[BUBDM]	decimal(10, 2)   NULL ,
	[BUBERLDAT]	datetime   NULL ,
	[BUBBUCHTEXT]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[DATEVSTAMM] ( 
	[DSCODE]	varchar(5)   NULL ,
	[DS5STELLIG]	smallint   NULL ,
	[DSBERATERNR]	varchar(8)   NULL ,
	[DSMANDANTENNR]	varchar(5)   NULL ,
	[DSFORDERUNG]	integer   NULL ,
	[DSGEG0]	integer   NULL ,
	[DSGEG5]	integer   NULL ,
	[DSGEG7]	integer   NULL ,
	[DSGEG16]	integer   NULL ,
	[DSGEG19]	integer   NULL ,
	[DSSKR]	varchar(2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[DBGUIDS] ( 
	[DBARZTNR]	bigint   NULL ,
	[DBGUID]	varchar(36)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[DOPPDRUCK] ( 
	[DDARZTNR]	bigint   NULL ,
	[DDCODE]	varchar(2)   NULL ,
	[DDEMAIL]	varchar(50)   NULL ,
	[DDZ1]	varchar(50)   NULL ,
	[DDERFDAT]	datetime   NULL ,
	[DDZ2]	varchar(50)   NULL ,
	[DDSTR]	varchar(50)   NULL ,
	[DDORT]	varchar(50)   NULL ,
	[DDLAND]	varchar(2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[DRUKON] ( 
	[DKDRUCK]	varchar(20)   NULL ,
	[DKUSER]	varchar(8)   NULL ,
	[DKARZTNR]	bigint   NULL ,
	[DKHALTERNR]	bigint   NULL ,
	[DKRECHNR]	integer   NULL ,
	[DKNAME]	varchar(50)   NULL ,
	[DKORT]	varchar(50)   NULL ,
	[DKLAND]	varchar(50)   NULL ,
	[DKDATUM]	datetime   NULL ,
	[DKERLDAT]	datetime   NULL ,
	[DKINKL]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[DRULISTE] ( 
	[DLCODE]	varchar(4)   NULL ,
	[DLERFDAT]	datetime   NULL ,
	[DLID]	varchar(10)   NULL ,
	[DLNAME]	varchar(100)   NULL ,
	[DLDM]	decimal(8, 2)   NULL ,
	[DLERLDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[DRUTEMP] ( 
	[DRUTYP]	varchar(2) NOT NULL ,
	[DRUBEARBEITER]	varchar(2) NOT NULL ,
	[DRUBEARBDATUM]	datetime   NULL ,
	[DRUARZTNR]	bigint NOT NULL ,
	[DRUHALTERNR]	bigint   NULL ,
	[DRUPOSNR]	integer   NULL ,
	[DRUNAME1]	varchar(30)   NULL ,
	[DRUNAME2]	varchar(30)   NULL ,
	[DRUSTR]	varchar(30)   NULL ,
	[DRUORT]	varchar(30)   NULL ,
	[DRUPLA]	varchar(1)   NULL ,
	[DRUPMWST]	decimal(5, 2)   NULL ,
	[DRUPRECHTEXT]	varchar(50)   NULL ,
	[DRUPDATUM]	date   NULL ,
	[DRUPDM]	decimal(8, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[DUMMY] ( 
	[DUMMY]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[ERINNERUNGSLISTE] ( 
	[ELARZTNR]	bigint   NULL ,
	[ELHALTERNR]	bigint   NULL ,
	[ELRECHNR]	integer   NULL ,
	[ELZEICHEN]	varchar(10)   NULL ,
	[ELTEXT]	varchar(150)   NULL ,
	[ELDATUM]	datetime   NULL ,
	[ELABTLG]	varchar(2)   NULL ,
	[ELERLEDIGT]	varchar(1)   NULL ,
	[ELERLDATUM]	date   NULL ,
	[ELDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[ETIDRU] ( 
	[EDN1]	varchar(50)   NULL ,
	[EDN2]	varchar(50)   NULL ,
	[EDS]	varchar(50)   NULL ,
	[EDO]	varchar(50)   NULL ,
	[EDA]	varchar(30)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[EURO] ( 
	[EUDATE]	datetime   NULL ,
	[EUTABELLE]	varchar(30)   NULL ,
	[EUFELD]	varchar(30)   NULL ,
	[EUBEZ]	varchar(50)   NULL ,
	[EUDM]	decimal(9, 2)   NULL ,
	[EUEURO]	decimal(9, 2)   NULL ,
	[EURUND]	decimal(9, 5)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[EWA] ( 
	[EWAARZTNR]	bigint   NULL ,
	[EWAHALTERNR]	bigint   NULL ,
	[EWAZEI]	varchar(2)   NULL ,
	[EWAERLDAT]	datetime   NULL ,
	[EWAABDATUM]	date   NULL ,
	[EWADEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[EWAERINNERUNG] ( 
	[EEARZTNR]	bigint   NULL ,
	[EEHALTERNR]	bigint   NULL ,
	[EEDATUM]	date   NULL ,
	[EEZEI]	varchar(2)   NULL ,
	[EEERLDATUM]	datetime   NULL ,
	[EEDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[FEIERTAGE] ( 
	[FEDATUM]	date   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[FIBU] ( 
	[FIFERTIG]	smallint   NULL ,
	[FISOLL]	integer   NULL ,
	[FIHABEN]	integer   NULL ,
	[FIBELNR]	varchar(10)   NULL ,
	[FIBELDAT]	date   NULL ,
	[FIBUCHTEXT]	varchar(50)   NULL ,
	[FIDM]	decimal(11, 2)   NULL ,
	[FITYP]	smallint   NULL ,
	[FIARZTNR]	bigint   NULL ,
	[FIHALTERNR]	bigint   NULL ,
	[FIRECHNR]	integer   NULL ,
	[FIUEBDAT]	date   NULL ,
	[FIEXPORTNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[FORDAUFS] ( 
	[FAPROZNR]	integer   NULL ,
	[FADATUM]	datetime   NULL ,
	[FATEXT]	varchar(90)   NULL ,
	[FAZINSHF]	decimal(4, 1)   NULL ,
	[FADMHF]	decimal(9, 2)   NULL ,
	[FAZINSKO]	decimal(4, 1)   NULL ,
	[FADMKO]	decimal(9, 2)   NULL ,
	[FASEITKO]	date   NULL ,
	[FAUNVZDM]	decimal(9, 2)   NULL ,
	[FAZINSDM]	decimal(9, 2)   NULL ,
	[FAUSER]	varchar(8)   NULL ,
	[FAUNVZDMTIT]	decimal(9, 2)   NULL ,
	[FAZINSVON] date NULL,
	[FAZINSBIS date NULL,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[GELESEN] ( 
	[GLZEICHEN]	varchar(100)   NULL ,
	[GLTEXT]	varchar(max)   NULL ,
	[GLDATUM]	date   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[GERICHTE] ( 
	[GELG]	smallint   NULL ,
	[GEPLZ]	varchar(5)   NULL ,
	[GEBEZ]	varchar(50)   NULL ,
	[GESTR]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[HABU] ( 
	[HBARZTNR]	bigint   NULL ,
	[HBHALTERNR]	bigint   NULL ,
	[HBRECHNR]	integer   NULL ,
	[HBPROZNR]	integer   NULL ,
	[HBBUCHDAT]	date   NULL ,
	[HBBELEGNR]	integer   NULL ,
	[HZAKZ]	smallint   NULL ,
	[HBCODE]	varchar(5)   NULL ,
	[HBERFDAT]	datetime   NULL ,
	[HBDM]	decimal(8, 2)   NULL ,
	[HBSOLL]	integer   NULL ,
	[HBHABEN]	integer   NULL ,
	[HBBUCHTEXT]	varchar(50)   NULL ,
	[HBRZNR]	integer   NULL ,
	[HBDEAKTIV]	smallint   NULL ,
	[HBRETEINZDAT]	date   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[HALTER] ( 
	[HARZTNR]	bigint NOT NULL ,
	[HHALTERNR]	bigint NOT NULL ,
	[HNAME1]	varchar(30)   NULL ,
	[HNAME2]	varchar(50)   NULL ,
	[HSTR]	varchar(50)   NULL ,
	[HORT]	varchar(56)   NULL ,
	[HFAELLTG]	smallint   NULL ,
	[HMAHNINT]	smallint   NULL ,
	[HBANKEINZUG]	smallint   NULL ,
	[HZUCHTBETRIEB]	smallint   NULL ,
	[HETIKETT]	smallint   NULL ,
	[HTITEL]	varchar(27)   NULL ,
	[HMATCH]	varchar(8)   NULL ,
	[HPROZ1]	decimal(4, 2)   NULL ,
	[HPROZ2]	decimal(4, 2)   NULL ,
	[HPROZ3]	decimal(4, 2)   NULL ,
	[HSKONTO]	decimal(4, 2)   NULL ,
	[HCREDONEG]	smallint   NULL ,
	[HBLZ]	varchar(10)   NULL ,
	[HKONTO]	varchar(10)   NULL ,
	[HGEBDAT]	date   NULL ,
	[HAG]	varchar(50)   NULL ,
	[HMOLKEREI]	varchar(50)   NULL ,
	[HANDEREHALTER]	varchar(20)   NULL ,
	[HTELEFON]	varchar(20)   NULL ,
	[HMEMO]	varchar(max)   NULL ,
	[HERFDAT]	datetime   NULL ,
	[HTI]	varchar(10)   NULL ,
	[HVN]	varchar(50)   NULL ,
	[HNN]	varchar(50)   NULL ,
	[HHALTERNEIN]	smallint   NULL ,
	[HREGNR]	varchar(15)   NULL ,
	[HEMAIL]	varchar(128)   NULL ,
	[HTEL2]	varchar(30)   NULL ,
	[HFAX]	varchar(30)   NULL ,
	[HHANDY]	varchar(30)   NULL ,
	[HLAND]	varchar(2)   NULL ,
	[HSPERRDATUM]	date   NULL ,
	[HVIRTKONTO]	varchar(10)   NULL ,
	[HREVERSE]	smallint   NULL ,
	[HUSTID]	varchar(20)   NULL ,
	[HBEZBLZ]	varchar(8)   NULL ,
	[HBEZKONTO]	varchar(10)   NULL ,
	[HPMEMAIL]	varchar(250)   NULL ,
	[HHTEL]	varchar(50)   NULL ,
	[HHTEL2]	varchar(50)   NULL ,
	[HHTELHANDY]	varchar(50)   NULL ,
	[HHTELFAX]	varchar(50)   NULL ,
	[HHEMAIL]	varchar(50)   NULL ,
	[HIBAN]	varchar(34)   NULL ,
	[HBEZIBAN]	varchar(34)   NULL ,
	[HBIC]	varchar(11)   NULL ,
	[HBEZBIC]	varchar(11)   NULL ,
	[HBEZSIGNDAT]	date   NULL ,
	[HGEBDAT2]	date   NULL ,
	[HGEBDAT2TEXT]	varchar(25)   NULL ,
	[HGEBDATTEXT]	varchar(25)   NULL ,
	[HSVNAME]	varchar(50)   NULL ,
	[HSVNAME2]	varchar(50)   NULL ,
	[HSVSTR]	varchar(50)   NULL ,
	[HSVORT]	varchar(50)   NULL ,
	[HSVLAND]	varchar(50)   NULL ,
	[HSVTITEL]	varchar(50)   NULL ,
	[HRECHTEXT]	varchar(max)   NULL ,
	[HNACHMBMS7]	smallint   NULL ,
	[HBEZABZUG]	smallint   NULL ,
	[HBEZABZUGTEXT]	varchar(40)   NULL ,
	[HDEAKTIV]	smallint   NULL ,
	[HDIGITALRG]	varchar(3)   NULL ,
	[HLEITWEGID]	varchar(50)   NULL ,
	[HDIGITALMAIL]	varchar(100)   NULL ,
	[HEPOSTMAIL]	varchar(50)   NULL ,
	[HEPOSTAENDDAT]	datetime   NULL ,
	[HAENDHERKUNFT]	varchar(100)   NULL ,
	[HAENDHERKUNFTDAT]	datetime   NULL ,
	[HVECODE]	varchar(10)   NULL ,
	[HVEVSNR]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[INITIALISIERUNG] ( 
	[INISECTION]	varchar(8)   NULL ,
	[INIENTRY]	varchar(50)   NULL ,
	[INIVALUE]	varchar(250)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[INVAD] ( 
	[IACODE]	varchar(8)   NULL ,
	[IAKDNR]	varchar(50)   NULL ,
	[IAN1]	varchar(50)   NULL ,
	[IAN2]	varchar(50)   NULL ,
	[IAS]	varchar(50)   NULL ,
	[IAO]	varchar(50)   NULL ,
	[IALAND]	varchar(25)   NULL ,
	[IATELBEST]	varchar(25)   NULL ,
	[IATELSUPP]	varchar(25)   NULL ,
	[IAEMAIL]	varchar(50)   NULL ,
	[IAWERTUNG]	smallint   NULL ,
	[IAANSPRECH]	varchar(30)   NULL ,
	[IAP]	varchar(5)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[INVKO] ( 
	[IKNR]	integer   NULL ,
	[IKDATUM]	date   NULL ,
	[IKRECHDAT]	date   NULL ,
	[IKBEARBEITER]	varchar(8)   NULL ,
	[IKARZTNR]	bigint   NULL ,
	[IKHAENDLER]	varchar(8)   NULL ,
	[IKRECHNR]	varchar(50)   NULL ,
	[IKKDNR]	varchar(50)   NULL ,
	[IKDM]	decimal(8, 2)   NULL ,
	[IKTYP]	varchar(20)   NULL ,
	[IKERLDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[INVPO] ( 
	[IPNR]	integer   NULL ,
	[IPCODE]	varchar(8)   NULL ,
	[IPMENGE]	integer   NULL ,
	[IPPCK]	varchar(5)   NULL ,
	[IPDM]	decimal(8, 2)   NULL ,
	[IPINVNR]	integer   NULL ,
	[IPARZTNR]	bigint   NULL ,
	[IPSERIENNR]	varchar(50)   NULL ,
	[IPBEZ]	varchar(50)   NULL ,
	[IPPOS]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[KONTIERUNGEN] ( 
	[KOCODE]	varchar(50)   NULL ,
	[KOBILANZIERER]	smallint   NULL ,
	[KOKONTENRAHMEN]	varchar(5)   NULL ,
	[KOSOLL]	varchar(5)   NULL ,
	[KOHABEN]	varchar(5)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[LAENDERCODES] ( 
	[LCNAME]	varchar(80)   NULL ,
	[LCCODE]	varchar(2)   NULL ,
	[LCKFZ]	varchar(3)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[LEISERF] ( 
	[LEBEARBEITER]	varchar(10) NOT NULL ,
	[LEARZTNR]	bigint NOT NULL ,
	[LEHALTERNR]	bigint   NULL ,
	[LEPOSNR]	integer   NULL ,
	[LENAME1]	varchar(30)   NULL ,
	[LENAME2]	varchar(50)   NULL ,
	[LESTR]	varchar(50)   NULL ,
	[LEORT]	varchar(56)   NULL ,
	[LEKENNUNGTA]	varchar(8)   NULL ,
	[LEBEARBDATUM]	datetime   NULL ,
	[LEMANDISK]	varchar(1)   NULL ,
	[LERECHNR]	integer   NULL ,
	[LERECHDAT]	date   NULL ,
	[LEMATCH]	varchar(8)   NULL ,
	[LEFAELLDAT]	date   NULL ,
	[LEMZF]	varchar(13)   NULL ,
	[LEZEILE1]	varchar(70)   NULL ,
	[LEZEILE2]	varchar(70)   NULL ,
	[LEBRUTTO]	decimal(10, 2)   NULL ,
	[LEHERFDAT]	datetime   NULL ,
	[LESTATUS]	varchar(5)   NULL ,
	[LETI]	varchar(10)   NULL ,
	[LEVN]	varchar(50)   NULL ,
	[LENN]	varchar(50)   NULL ,
	[LEBARCODE]	varchar(13)   NULL ,
	[LEEINZUG]	smallint   NULL ,
	[LEBLZ]	varchar(8)   NULL ,
	[LEKONTO]	varchar(10)   NULL ,
	[LEREVERSE]	smallint   NULL ,
	[LEUSTID]	varchar(20)   NULL ,
	[LEGUID]	varchar(36)   NULL ,
	[LEHTEL]	varchar(50)   NULL ,
	[LEHTEL2]	varchar(50)   NULL ,
	[LEHTELHANDY]	varchar(50)   NULL ,
	[LEHTELFAX]	varchar(50)   NULL ,
	[LEHEMAIL]	varchar(50)   NULL ,
	[LEPAPIERRG]	smallint   NULL ,
	[LEGEBDAT]	date   NULL ,
	[LEDATEINAME]	varchar(250)   NULL ,
	[LEBRIEFART]	smallint   NULL ,
	[LEDIGITALRG]	varchar(3)   NULL ,
	[LELEITWEGID]	varchar(50)   NULL ,
	[LEDIGITALMAIL]	varchar(100)   NULL ,
	[LEEPOST]	smallint   NULL ,
	[LEEPOSTMAIL]	varchar(50)   NULL ,
	[LEEPOSTAENDDAT]	datetime   NULL ,
	[LEVECODE]	varchar(10)   NULL ,
	[LEVEVSNR]	varchar(50)   NULL ,
	[LEBEGUID]	varchar(36)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[LEISERFPOS] ( 
	[LEPBEARBEITER]	varchar(10) NOT NULL ,
	[LEPARZTNR]	bigint NOT NULL ,
	[LEPHALTERNR]	bigint   NULL ,
	[LEPPOSNR]	integer   NULL ,
	[LEPLA]	varchar(1)   NULL ,
	[LEPMWST]	decimal(5, 2)   NULL ,
	[LEPRECHTEXT]	varchar(90)   NULL ,
	[LEPDATUM]	date   NULL ,
	[LEPDM]	decimal(8, 2)   NULL ,
	[LEPTEXT]	smallint   NULL ,
	[LEBEARBDATUM]	datetime   NULL ,
	[LEPRECHNR]	integer   NULL ,
	[LEPZEIDAT]	datetime   NULL ,
	[LEPBEIHILFE]	decimal(8, 2)   NULL ,
	[LEPREVERSEVERSION]	smallint   NULL ,
	[LEPRABATT]	decimal(5, 2)   NULL ,
	[LEPFORMAT]	smallint   NULL ,
	[LEPMOID]	varchar(19)   NULL ,
	[LEPFREMDRECHNUNGSNR]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[LEISTUNGEN] ( 
	[LKENN]	varchar(8) NOT NULL ,
	[LBEZ]	varchar(70)   NULL ,
	[LPREIS]	decimal(8, 2)   NULL ,
	[LLEISARZKZ]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[LOESCHTEMP] ( 
	[LTARZTNR]	bigint   NULL ,
	[LTHALTERNR]	bigint   NULL ,
	[LTRECHNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[LOG] ( 
	[LOGNAME]	varchar(10)   NULL ,
	[LOGDATE]	datetime   NULL ,
	[LOGTEXT]	varchar(254)   NULL ,
	[LOGVERSION]	varchar(20)   NULL ,
	[LOGTYP]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[LOGBUG] ( 
	[LBPROGNAME]	varchar(20)   NULL ,
	[LBVERSION]	varchar(5)   NULL ,
	[LBREL]	varchar(3)   NULL ,
	[LBARZTNR]	varchar(8)   NULL ,
	[LBKOMM]	varchar(8)   NULL ,
	[LBVIA]	varchar(8)   NULL ,
	[LBBEZ]	varchar(50)   NULL ,
	[LBTEXT]	varchar(max)   NULL ,
	[LBDATUM]	datetime   NULL ,
	[LBTYP]	varchar(2)   NULL ,
	[LBERLDAT]	datetime   NULL ,
	[LBTICKET]	decimal(8, 4)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MAHNBESCH] ( 
	[MBARZTNR]	bigint   NULL ,
	[MBHALTERNR]	bigint   NULL ,
	[MBRG1]	integer   NULL ,
	[MBRG2]	integer   NULL ,
	[MBRG3]	integer   NULL ,
	[MBRG4]	integer   NULL ,
	[MBRG5]	integer   NULL ,
	[MBRG6]	integer   NULL ,
	[MBDATUM]	date   NULL ,
	[MBEHELEUTE]	smallint   NULL ,
	[MBEHEPARTNER]	varchar(20)   NULL ,
	[MBGERKOSTEN]	decimal(5, 2)   NULL ,
	[MBGERNAME]	varchar(25)   NULL ,
	[MBA1]	smallint   NULL ,
	[MBA2]	smallint   NULL ,
	[MBA3]	smallint   NULL ,
	[MBA4]	smallint   NULL ,
	[MBA5]	smallint   NULL ,
	[MBA6]	smallint   NULL ,
	[MBNR]	integer   NULL ,
	[MBPROZHF]	decimal(5, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MAHNDATEN] ( 
	[MDARZTNR]	bigint   NULL ,
	[MDHALTERNR]	bigint   NULL ,
	[MDDATUM]	datetime   NULL ,
	[MDBEREICH]	smallint   NULL ,
	[MDTEXT]	varchar(100)   NULL ,
	[MDMS]	smallint   NULL ,
	[MDZAHLEING]	date   NULL ,
	[MDLFD]	integer   NULL ,
	[MDDM]	decimal(8, 2)   NULL ,
	[MDMZF]	varchar(15)   NULL ,
	[MDKONTROLL]	varchar(15)   NULL ,
	[MDNR]	integer   NULL ,
	[MDGIROCODEDM]	decimal(10, 2)   NULL ,
	[MDDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MAHNGEB] ( 
	[MGDM1]	decimal(5, 2)   NULL ,
	[MGDM2]	decimal(5, 2)   NULL ,
	[MGDM3]	decimal(5, 2)   NULL ,
	[MGDM4]	decimal(5, 2)   NULL ,
	[MGDM5]	decimal(5, 2)   NULL ,
	[MGDM6]	decimal(5, 2)   NULL ,
	[MGDM7]	decimal(5, 2)   NULL ,
	[MGDM8]	decimal(5, 2)   NULL ,
	[MGDM9]	decimal(5, 2)   NULL ,
	[MGDM10]	decimal(5, 2)   NULL ,
	[MGDMABR]	decimal(5, 2)   NULL ,
	[MGARZTNR]	bigint NOT NULL ,
	[MGDMTVS]	decimal(5, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MAHNINT] ( 
	[MINR]	smallint NOT NULL ,
	[MITG1]	smallint   NULL ,
	[MITG2]	smallint   NULL ,
	[MITG3]	smallint   NULL ,
	[MITG4]	smallint   NULL ,
	[MITG5]	smallint   NULL ,
	[MITG6]	smallint   NULL ,
	[MITG7]	smallint   NULL ,
	[MITG8]	smallint   NULL ,
	[MITG9]	smallint   NULL ,
	[MITG10]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MAHNTEXTE] ( 
	[MTARZTNR]	bigint   NULL ,
	[MTSTUFE]	smallint   NULL ,
	[MTO1]	varchar(100)   NULL ,
	[MTO2]	varchar(100)   NULL ,
	[MTO3]	varchar(100)   NULL ,
	[MTO4]	varchar(100)   NULL ,
	[MTO5]	varchar(100)   NULL ,
	[MTO6]	varchar(100)   NULL ,
	[MTU1]	varchar(100)   NULL ,
	[MTU2]	varchar(100)   NULL ,
	[MTU3]	varchar(100)   NULL ,
	[MTU4]	varchar(100)   NULL ,
	[MTU5]	varchar(100)   NULL ,
	[MTU6]	varchar(100)   NULL ,
	[MTO]	varchar(max)   NULL ,
	[MTU]	varchar(max)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MAHNUNGEN] ( 
	[MAARZTNR]	bigint   NULL ,
	[MAHALTERNR]	bigint   NULL ,
	[MARECHNR]	integer   NULL ,
	[MADATUM]	datetime   NULL ,
	[MADMGEB]	decimal(7, 2)   NULL ,
	[MAMS]	smallint   NULL ,
	[MADMZINS]	decimal(7, 2)   NULL ,
	[MADMREST]	decimal(9, 2)   NULL ,
	[MANR]	integer   NULL ,
	[MADEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MAHNVOR] ( 
	[MVARZTNR]	bigint   NULL ,
	[MVHALTERNR]	bigint   NULL ,
	[MVRECHNR]	integer   NULL ,
	[MVRECHDAT]	date   NULL ,
	[MVMS]	smallint   NULL ,
	[MVDMREST]	decimal(10, 2)   NULL ,
	[MVNAME]	varchar(20)   NULL ,
	[MVMAHNDAT]	datetime   NULL ,
	[MVDMMAHNGEB]	decimal(8, 2)   NULL ,
	[MVDMZINS]	decimal(8, 2)   NULL ,
	[MVMZF]	varchar(13)   NULL ,
	[MVZAHLEING]	date   NULL ,
	[MVNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MBINFOTEMP] ( 
	[TARZTNR]	bigint   NULL ,
	[THALTERNR]	bigint   NULL ,
	[TRECHNR]	integer   NULL ,
	[TDATUM]	datetime   NULL ,
	[TTYP]	varchar(5)   NULL ,
	[TTYPTEXT]	varchar(100)   NULL ,
	[TDMHF]	decimal(10, 2)   NULL ,
	[TDMMA]	decimal(10, 2)   NULL ,
	[TZAHF]	decimal(10, 2)   NULL ,
	[TZAMA]	decimal(10, 2)   NULL ,
	[TDMGK]	decimal(10, 2)   NULL ,
	[TDMZA]	decimal(10, 2)   NULL ,
	[TMS]	smallint   NULL ,
	[TPROZNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MBKALENDER] ( 
	[MBKPROZNR]	integer   NULL ,
	[MBKGERNAME]	varchar(50)   NULL ,
	[MBKZUSTMB]	date   NULL ,
	[MBKZUSTVB]	date   NULL ,
	[MBKAZ]	varchar(50)   NULL ,
	[MBKDMHF]	decimal(10, 2)   NULL ,
	[MBKPROZHF]	decimal(5, 2)   NULL ,
	[MBKDMMAHN]	decimal(10, 2)   NULL ,
	[MBKDMMB]	decimal(10, 2)   NULL ,
	[MBKDMVB]	decimal(10, 2)   NULL ,
	[MBKPROZMBVB]	decimal(5, 2)   NULL ,
	[MBKDMVA]	decimal(10, 2)   NULL ,
	[MBKTYP]	varchar(5)   NULL ,
	[MBKDMFRUEH]	decimal(10, 2)   NULL ,
	[MBKZINSHF]	decimal(10, 2)   NULL ,
	[MBKZINSMB]	decimal(10, 2)   NULL ,
	[MBKHNAME1]	varchar(90)   NULL ,
	[MBKHNAME2]	varchar(50)   NULL ,
	[MBKHSTR]	varchar(50)   NULL ,
	[MBKHORT]	varchar(50)   NULL ,
	[MBKUSER]	varchar(8)   NULL ,
	[MBKGVNFU]	varchar(35)   NULL ,
	[MBKGVNN]	varchar(35)   NULL ,
	[MBKGVNSH]	varchar(35)   NULL ,
	[MBKGVNPLZ]	varchar(5)   NULL ,
	[MBKGVNO]	varchar(27)   NULL ,
	[MBKGVNAL]	varchar(3)   NULL ,
	[MBKGVZFU]	varchar(35)   NULL ,
	[MBKGVZN]	varchar(35)   NULL ,
	[MBKGVZSH]	varchar(35)   NULL ,
	[MBKGVZPLZ]	varchar(5)   NULL ,
	[MBKGVZO]	varchar(27)   NULL ,
	[MBKGVZAL]	varchar(3)   NULL ,
	[MBKGVN2FU]	varchar(35)   NULL ,
	[MBKGVN2N]	varchar(35)   NULL ,
	[MBKGVN2SH]	varchar(35)   NULL ,
	[MBKGVN2PLZ]	varchar(5)   NULL ,
	[MBKGVN2O]	varchar(27)   NULL ,
	[MBKGVN2AL]	varchar(3)   NULL ,
	[MBKGVZ2FU]	varchar(35)   NULL ,
	[MBKGVZ2N]	varchar(35)   NULL ,
	[MBKGVZ2SH]	varchar(35)   NULL ,
	[MBKGVZ2PLZ]	varchar(5)   NULL ,
	[MBKGVZ2O]	varchar(27)   NULL ,
	[MBKGVZ2AL]	varchar(3)   NULL ,
	[MBKGVN3FU]	varchar(35)   NULL ,
	[MBKGVN3N]	varchar(35)   NULL ,
	[MBKGVN3SH]	varchar(35)   NULL ,
	[MBKGVN3PLZ]	varchar(5)   NULL ,
	[MBKGVN3O]	varchar(27)   NULL ,
	[MBKGVN3AL]	varchar(3)   NULL ,
	[MBKGVZ3FU]	varchar(35)   NULL ,
	[MBKGVZ3N]	varchar(35)   NULL ,
	[MBKGVZ3SH]	varchar(35)   NULL ,
	[MBKGVZ3PLZ]	varchar(5)   NULL ,
	[MBKGVZ3O]	varchar(27)   NULL ,
	[MBKGVZ3AL]	varchar(3)   NULL ,
	[MBKGVN4FU]	varchar(35)   NULL ,
	[MBKGVN4N]	varchar(35)   NULL ,
	[MBKGVN4SH]	varchar(35)   NULL ,
	[MBKGVN4PLZ]	varchar(5)   NULL ,
	[MBKGVN4O]	varchar(27)   NULL ,
	[MBKGVN4AL]	varchar(3)   NULL ,
	[MBKGVZ4FU]	varchar(35)   NULL ,
	[MBKGVZ4N]	varchar(35)   NULL ,
	[MBKGVZ4SH]	varchar(35)   NULL ,
	[MBKGVZ4PLZ]	varchar(5)   NULL ,
	[MBKGVZ4O]	varchar(27)   NULL ,
	[MBKGVZ4AL]	varchar(3)   NULL ,
	[MBKSKOBET]	decimal(11, 2)   NULL ,
	[MBKSKOBEG]	varchar(35)   NULL ,
	[MBKAUSK]	decimal(11, 2)   NULL ,
	[MBKPGM]	smallint   NULL ,
	[MBKPGPLZ]	varchar(5)   NULL ,
	[MBKPGO]	varchar(30)   NULL ,
	[MBKAGAL]	varchar(3)   NULL ,
	[MBKHRF]	varchar(50)   NULL ,
	[MBKAGPLZ]	varchar(5)   NULL ,
	[MBKDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MBKOSTEN] ( 
	[MKARZTNR]	bigint   NULL ,
	[MKHALTERNR]	bigint   NULL ,
	[MKRECHNR]	integer   NULL ,
	[MKBUCHDAT]	date   NULL ,
	[MKDMA]	decimal(10, 2)   NULL ,
	[MKDMB]	decimal(10, 2)   NULL ,
	[MKDMC]	decimal(10, 2)   NULL ,
	[MKDRUCK]	datetime   NULL ,
	[MKTYP]	varchar(1)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MCGEM] ( 
	[ID]	varchar(8)   NULL ,
	[GEMEINDE]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MCKREIS] ( 
	[ID]	varchar(8)   NULL ,
	[KREIS]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MCLAND] ( 
	[ID]	varchar(2)   NULL ,
	[LAND]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MCORT] ( 
	[IDORT]	varchar(8)   NULL ,
	[STATUS]	varchar(1)   NULL ,
	[ORT]	varchar(40)   NULL ,
	[ZUSATZ]	varchar(30)   NULL ,
	[ORTPHON]	varchar(40)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MCORTT] ( 
	[PLZ]	varchar(5)   NULL ,
	[IDORT]	varchar(8)   NULL ,
	[IDGEM]	varchar(8)   NULL ,
	[ORT]	varchar(40)   NULL ,
	[ORTPHON]	varchar(40)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MCPLZ] ( 
	[PLZ]	varchar(5)   NULL ,
	[IDORT]	varchar(8)   NULL ,
	[IDGEM]	varchar(8)   NULL ,
	[ORT]	varchar(40)   NULL ,
	[ORTPHON]	varchar(40)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MCSTR] ( 
	[IDORT]	varchar(8)   NULL ,
	[IDGEM]	varchar(8)   NULL ,
	[STR]	varchar(46)   NULL ,
	[STRPHON]	varchar(46)   NULL ,
	[PLZ]	varchar(5)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[MD1] ( 
	[MDARZTNR]	bigint   NULL ,
	[MDHALTERNR]	bigint   NULL ,
	[MDDATUM]	datetime   NULL ,
	[MDBEREICH]	smallint   NULL ,
	[MDTEXT]	varchar(100)   NULL ,
	[MDMS]	smallint   NULL ,
	[MDZAHLEING]	date   NULL ,
	[MDLFD]	integer   NULL ,
	[MDDM]	decimal(8, 2)   NULL ,
	[MDMZF]	varchar(13)   NULL ,
	[MDKONTROLL]	varchar(15)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[NUMMERN] ( 
	[NRCODE]	varchar(4)   NULL ,
	[NRMAX]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PACKINFO] ( 
	[PIHAUPTNR]	smallint   NULL ,
	[PINEBENNR]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PEGASUSNUTZUNG] ( 
	[PNGUID]	varchar(36)   NULL ,
	[PNCOMPUTERNAME]	varchar(50)   NULL ,
	[PNPRAXIS]	varchar(100)   NULL ,
	[PNARZTNR]	bigint   NULL ,
	[PNPROGVER]	varchar(20)   NULL ,
	[PNLETZTDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PEGASUSNUTZUNGDATEN] ( 
	[PNDDATUM]	date   NULL ,
	[PNDGUID]	varchar(36)   NULL ,
	[PNDCOMPUTERNAME]	varchar(50)   NULL ,
	[PNDPRAXIS]	varchar(100)   NULL ,
	[PNDARZTNR]	bigint   NULL ,
	[PNDPROGVER]	varchar(20)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PERSONEN] ( 
	[PEARZTNR]	bigint   NULL ,
	[PEANREDE]	varchar(50)   NULL ,
	[PETITEL]	varchar(50)   NULL ,
	[PENACHNAME]	varchar(50)   NULL ,
	[PEVORNAME]	varchar(50)   NULL ,
	[PENAME2]	varchar(50)   NULL ,
	[PESTR]	varchar(50)   NULL ,
	[PEORT]	varchar(50)   NULL ,
	[PEGEBTAG]	date   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PLAN_TABLE] ( 
	[QUERYNO]	integer NOT NULL ,
	[OUTER_TBL]	varchar(18)   NULL ,
	[IND_USED_O]	varchar(18)   NULL ,
	[INNER_TBL]	varchar(18)   NULL ,
	[IND_USED_I]	varchar(18)   NULL ,
	[RESULT_TBL]	varchar(18)   NULL ,
	[JOIN_METHOD]	varchar(24)   NULL ,
	[SORT]	varchar(4) NOT NULL ,
	[PLANNO]	integer NOT NULL ,
	[JOIN_TYPE]	varchar(24)   NULL ,
	[SEQUENCE_NO]	integer NOT NULL ,
	[COMMENT]	varchar(100)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PORTO] ( 
	[ARZT]	smallint   NULL ,
	[HALTER]	integer   NULL ,
	[DATUM]	date   NULL ,
	[TYP]	varchar(2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PREDA] ( 
	[PREPROZNR]	integer   NULL ,
	[PRESA]	varchar(2)   NULL ,
	[PREAGGM]	smallint   NULL ,
	[PREAGANRA]	smallint   NULL ,
	[PREAGN1A]	varchar(35)   NULL ,
	[PREAGN2A]	varchar(35)   NULL ,
	[PREAGN3A]	varchar(35)   NULL ,
	[PREAGRFA]	varchar(35)   NULL ,
	[PREAGSHA]	varchar(35)   NULL ,
	[PREAGPLZA]	varchar(5)   NULL ,
	[PREAGOA]	varchar(27)   NULL ,
	[PREPGMA]	smallint   NULL ,
	[PREPGPLZA]	varchar(5)   NULL ,
	[PREPGOA]	varchar(30)   NULL ,
	[PREAGANRB]	smallint   NULL ,
	[PREAGN1B]	varchar(35)   NULL ,
	[PREAGN2B]	varchar(35)   NULL ,
	[PREAGN3B]	varchar(35)   NULL ,
	[PREAGRFB]	varchar(35)   NULL ,
	[PREAGSHB]	varchar(35)   NULL ,
	[PREAGPLZB]	varchar(5)   NULL ,
	[PREAGOB]	varchar(27)   NULL ,
	[PREPGMB]	smallint   NULL ,
	[PREPGPLZB]	varchar(5)   NULL ,
	[PREPGOB]	varchar(30)   NULL ,
	[PREAGANRC]	smallint   NULL ,
	[PREAGN1C]	varchar(35)   NULL ,
	[PREAGN2C]	varchar(35)   NULL ,
	[PREAGN3C]	varchar(35)   NULL ,
	[PREAGRFC]	varchar(35)   NULL ,
	[PREAGSHC]	varchar(35)   NULL ,
	[PREAGPLZC]	varchar(5)   NULL ,
	[PREAGOC]	varchar(27)   NULL ,
	[PREPGMC]	smallint   NULL ,
	[PREPGPLZC]	varchar(5)   NULL ,
	[PREPGOC]	varchar(30)   NULL ,
	[PREAGANRD]	smallint   NULL ,
	[PREAGN1D]	varchar(35)   NULL ,
	[PREAGN2D]	varchar(35)   NULL ,
	[PREAGN3D]	varchar(35)   NULL ,
	[PREAGRFD]	varchar(35)   NULL ,
	[PREAGSHD]	varchar(35)   NULL ,
	[PREAGPLZD]	varchar(5)   NULL ,
	[PREAGOD]	varchar(27)   NULL ,
	[PREPGMD]	smallint   NULL ,
	[PREPGPLZD]	varchar(5)   NULL ,
	[PREPGOD]	varchar(30)   NULL ,
	[PREDISKNR]	smallint   NULL ,
	[PREAGALA]	varchar(3)   NULL ,
	[PREAGALB]	varchar(3)   NULL ,
	[PREAGALC]	varchar(3)   NULL ,
	[PREAGALD]	varchar(3)   NULL ,
	[PREAGGVFUA]	varchar(35)   NULL ,
	[PREAGGVVNA]	varchar(35)   NULL ,
	[PREAGGVSHA]	varchar(35)   NULL ,
	[PREAGGVPLZA]	varchar(5)   NULL ,
	[PREAGGVOA]	varchar(27)   NULL ,
	[PREAGGVALA]	varchar(3)   NULL ,
	[PREAGGVFUB]	varchar(35)   NULL ,
	[PREAGGVVNB]	varchar(35)   NULL ,
	[PREAGGVSHB]	varchar(35)   NULL ,
	[PREAGGVPLZB]	varchar(5)   NULL ,
	[PREAGGVOB]	varchar(27)   NULL ,
	[PREAGGVALB]	varchar(3)   NULL ,
	[PREAGGVFUC]	varchar(35)   NULL ,
	[PREAGGVVNC]	varchar(35)   NULL ,
	[PREAGGVSHC]	varchar(35)   NULL ,
	[PREAGGVPLZC]	varchar(5)   NULL ,
	[PREAGGVOC]	varchar(27)   NULL ,
	[PREAGGVALC]	varchar(3)   NULL ,
	[PREAGGVFUD]	varchar(35)   NULL ,
	[PREAGGVVND]	varchar(35)   NULL ,
	[PREAGGVSHD]	varchar(35)   NULL ,
	[PREAGGVPLZD]	varchar(5)   NULL ,
	[PREAGGVOD]	varchar(27)   NULL ,
	[PREAGGVALD]	varchar(3)   NULL ,
	[PREAGGVFUA2]	varchar(35)   NULL ,
	[PREAGGVVNA2]	varchar(35)   NULL ,
	[PREAGGVSHA2]	varchar(35)   NULL ,
	[PREAGGVPLZA2]	varchar(5)   NULL ,
	[PREAGGVOA2]	varchar(27)   NULL ,
	[PREAGGVALA2]	varchar(3)   NULL ,
	[PREAGGVFUB2]	varchar(35)   NULL ,
	[PREAGGVVNB2]	varchar(35)   NULL ,
	[PREAGGVSHB2]	varchar(35)   NULL ,
	[PREAGGVPLZB2]	varchar(5)   NULL ,
	[PREAGGVOB2]	varchar(27)   NULL ,
	[PREAGGVALB2]	varchar(3)   NULL ,
	[PREAGGVFUC2]	varchar(35)   NULL ,
	[PREAGGVVNC2]	varchar(35)   NULL ,
	[PREAGGVSHC2]	varchar(35)   NULL ,
	[PREAGGVPLZC2]	varchar(5)   NULL ,
	[PREAGGVOC2]	varchar(27)   NULL ,
	[PREAGGVALC2]	varchar(3)   NULL ,
	[PREAGGVFUD2]	varchar(35)   NULL ,
	[PREAGGVVND2]	varchar(35)   NULL ,
	[PREAGGVSHD2]	varchar(35)   NULL ,
	[PREAGGVPLZD2]	varchar(5)   NULL ,
	[PREAGGVOD2]	varchar(27)   NULL ,
	[PREAGGVALD2]	varchar(3)   NULL ,
	[PREAGGVFUA3]	varchar(35)   NULL ,
	[PREAGGVVNA3]	varchar(35)   NULL ,
	[PREAGGVSHA3]	varchar(35)   NULL ,
	[PREAGGVPLZA3]	varchar(5)   NULL ,
	[PREAGGVOA3]	varchar(27)   NULL ,
	[PREAGGVALA3]	varchar(3)   NULL ,
	[PREAGGVFUB3]	varchar(35)   NULL ,
	[PREAGGVVNB3]	varchar(35)   NULL ,
	[PREAGGVSHB3]	varchar(35)   NULL ,
	[PREAGGVPLZB3]	varchar(5)   NULL ,
	[PREAGGVOB3]	varchar(27)   NULL ,
	[PREAGGVALB3]	varchar(3)   NULL ,
	[PREAGGVFUC3]	varchar(35)   NULL ,
	[PREAGGVVNC3]	varchar(35)   NULL ,
	[PREAGGVSHC3]	varchar(35)   NULL ,
	[PREAGGVPLZC3]	varchar(5)   NULL ,
	[PREAGGVOC3]	varchar(27)   NULL ,
	[PREAGGVALC3]	varchar(3)   NULL ,
	[PREAGGVFUD3]	varchar(35)   NULL ,
	[PREAGGVVND3]	varchar(35)   NULL ,
	[PREAGGVSHD3]	varchar(35)   NULL ,
	[PREAGGVPLZD3]	varchar(5)   NULL ,
	[PREAGGVOD3]	varchar(27)   NULL ,
	[PREAGGVALD3]	varchar(3)   NULL ,
	[PREAGGVFUA4]	varchar(35)   NULL ,
	[PREAGGVVNA4]	varchar(35)   NULL ,
	[PREAGGVSHA4]	varchar(35)   NULL ,
	[PREAGGVPLZA4]	varchar(5)   NULL ,
	[PREAGGVOA4]	varchar(27)   NULL ,
	[PREAGGVALA4]	varchar(3)   NULL ,
	[PREAGGVFUB4]	varchar(35)   NULL ,
	[PREAGGVVNB4]	varchar(35)   NULL ,
	[PREAGGVSHB4]	varchar(35)   NULL ,
	[PREAGGVPLZB4]	varchar(5)   NULL ,
	[PREAGGVOB4]	varchar(27)   NULL ,
	[PREAGGVALB4]	varchar(3)   NULL ,
	[PREAGGVFUC4]	varchar(35)   NULL ,
	[PREAGGVVNC4]	varchar(35)   NULL ,
	[PREAGGVSHC4]	varchar(35)   NULL ,
	[PREAGGVPLZC4]	varchar(5)   NULL ,
	[PREAGGVOC4]	varchar(27)   NULL ,
	[PREAGGVALC4]	varchar(3)   NULL ,
	[PREAGGVFUD4]	varchar(35)   NULL ,
	[PREAGGVVND4]	varchar(35)   NULL ,
	[PREAGGVSHD4]	varchar(35)   NULL ,
	[PREAGGVPLZD4]	varchar(5)   NULL ,
	[PREAGGVOD4]	varchar(27)   NULL ,
	[PREAGGVALD4]	varchar(3)   NULL ,
	[PREAZB]	varchar(70)   NULL ,
	[PREAZC]	varchar(70)   NULL ,
	[PREAZD]	varchar(70)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PRINTIDS] ( 
	[PRIDARZTNR]	bigint   NULL ,
	[PRIDHALTERNR]	bigint   NULL ,
	[PRIDDRUCK]	varchar(50)   NULL ,
	[PRIDID]	varchar(36)   NULL ,
	[PRIDERFDAT]	datetime   NULL ,
	[PRIDUSER]	varchar(8)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PROZREG] ( 
	[PRARZTNR]	bigint   NULL ,
	[PRHALTERNR]	bigint   NULL ,
	[PRR1]	integer   NULL ,
	[PRR2]	integer   NULL ,
	[PRR3]	integer   NULL ,
	[PRR4]	integer   NULL ,
	[PRR5]	integer   NULL ,
	[PRR6]	integer   NULL ,
	[PRDATUM]	datetime   NULL ,
	[PRBEMERK]	varchar(30)   NULL ,
	[PRNR]	integer   NULL ,
	[PRA1]	smallint   NULL ,
	[PRA2]	smallint   NULL ,
	[PRA3]	smallint   NULL ,
	[PRA4]	smallint   NULL ,
	[PRA5]	smallint   NULL ,
	[PRA6]	smallint   NULL ,
	[PRRECHNR]	integer   NULL ,
	[PRAZ]	varchar(70)   NULL ,
	[PRTERMINVA]	date   NULL ,
	[PRGERNAME]	varchar(50)   NULL ,
	[PRTERMINVB]	date   NULL ,
	[PRDMA]	decimal(10, 2)   NULL ,
	[PRDMB]	decimal(10, 2)   NULL ,
	[PRDMC]	decimal(10, 2)   NULL ,
	[PRDMHF]	decimal(10, 2)   NULL ,
	[PRBEZAHLT]	date   NULL ,
	[PRDRUCKDATUM]	date   NULL ,
	[PRPROZHF]	decimal(5, 2)   NULL ,
	[PRPROZKO]	decimal(5, 2)   NULL ,
	[PRERLEDIGT]	smallint   NULL ,
	[PRDMMAHN]	decimal(8, 2)   NULL ,
	[PRDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[PROZREGDET] ( 
	[PRDNR]	integer   NULL ,
	[PRDARZTNR]	bigint   NULL ,
	[PRDHALTERNR]	bigint   NULL ,
	[PRDRECHNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RATEN] ( 
	[RARZTNR]	bigint   NULL ,
	[RHALTERNR]	bigint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RAZA] ( 
	[RZNR]	integer   NULL ,
	[RZARZTNR]	bigint   NULL ,
	[RZHALTERNR]	bigint   NULL ,
	[RZERFDAT]	datetime   NULL ,
	[RZKUENDDAT]	datetime   NULL ,
	[RZZINS]	decimal(5, 1)   NULL ,
	[RZTVNGEB]	decimal(8, 2)   NULL ,
	[RZAKTIVDAT]	date   NULL ,
	[RZRATE]	decimal(8, 2)   NULL ,
	[RZTAG]	smallint   NULL ,
	[RZLTZTZINSDAT]	date   NULL ,
	[RZMEMO]	varchar(max)   NULL ,
	[RZBEZDAT]	date   NULL ,
	[RZPAUSEBIS]	date   NULL ,
	[RZANZPAUSEN]	smallint   NULL ,
	[RZSCHLUSS]	date   NULL ,
	[RZMS5TAINFO]	date   NULL ,
	[RZDEAKTIV]	smallint   NULL ,
	[RZOPDM]	decimal(10, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RAZAPOS] ( 
	[RZPNR]	integer   NULL ,
	[RZPARZTNR]	bigint   NULL ,
	[RZPHALTERNR]	bigint   NULL ,
	[RZPRECHNR]	integer   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RECHAB] ( 
	[RAARZTNR]	bigint   NULL ,
	[RAHALTERNR]	bigint   NULL ,
	[RARECHNR]	integer   NULL ,
	[RADATUM]	date   NULL ,
	[RAZEICHEN]	varchar(2)   NULL ,
	[RAAN1]	varchar(50)   NULL ,
	[RAAN2]	varchar(50)   NULL ,
	[RAAN3]	varchar(50)   NULL ,
	[RAAN4]	varchar(50)   NULL ,
	[RAAN5]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RECHBUCH] ( 
	[RBARZTNR]	bigint   NULL ,
	[RBHALTERNR]	bigint   NULL ,
	[RBRECHNR]	integer   NULL ,
	[RBGRUPPE]	varchar(2)   NULL ,
	[RBUNTERGRP]	varchar(2)   NULL ,
	[RBLA]	varchar(1)   NULL ,
	[RBMWST]	decimal(5, 2)   NULL ,
	[RBDATUM]	datetime   NULL ,
	[RBBEZEICHNUNG]	varchar(50)   NULL ,
	[RBDM]	decimal(10, 2)   NULL ,
	[RBREVERSEVERSION]	smallint   NULL ,
	[RBERFDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RECHKO] ( 
	[RKARZTNR]	bigint NOT NULL ,
	[RKHALTERNR]	bigint NOT NULL ,
	[RKRECHNR]	integer NOT NULL ,
	[RKKZ]	smallint   NULL ,
	[RKJOURNSEIT]	integer   NULL ,
	[RKBUCHTEXT]	varchar(50)   NULL ,
	[RKRECHDAT]	date   NULL ,
	[RKFAELLDAT]	date   NULL ,
	[RKDMLEIS]	decimal(8, 2)   NULL ,
	[RKDMARZN]	decimal(8, 2)   NULL ,
	[RKDMMAHN]	decimal(8, 2)   NULL ,
	[RKDMZINS]	decimal(8, 2)   NULL ,
	[RKKZMWST]	smallint   NULL ,
	[RKMAHNSTUFE]	smallint   NULL ,
	[RKMAHNSPERR]	smallint   NULL ,
	[RKMAHNERST]	date   NULL ,
	[RKZALEIS]	decimal(8, 2)   NULL ,
	[RKZAARZN]	decimal(8, 2)   NULL ,
	[RKZAMAHN]	decimal(8, 2)   NULL ,
	[RKZAZINS]	decimal(8, 2)   NULL ,
	[RKKZZAHL]	smallint   NULL ,
	[RKVERJAEHR]	date   NULL ,
	[RKMAHNLTZT]	date   NULL ,
	[RKMAHNDMABR]	decimal(5, 2)   NULL ,
	[RKMBDMABR]	decimal(6, 2)   NULL ,
	[RKMBZAABR]	decimal(6, 2)   NULL ,
	[RKMBDMOPL]	decimal(6, 2)   NULL ,
	[RKMBZAOPL]	decimal(6, 2)   NULL ,
	[RKTEILZDAT]	date   NULL ,
	[RKOPNR]	integer   NULL ,
	[RKKZRECHDRU]	smallint   NULL ,
	[RKKONTROLL]	varchar(15)   NULL ,
	[RKBARCODE]	varchar(13)   NULL ,
	[RKREVERSE]	smallint   NULL ,
	[RKMAHNSPERRBIS]	date   NULL ,
	[RKPAPIERRG]	smallint   NULL ,
	[RKDEAKTIV]	smallint   NULL ,
	[RKMS5DAT]	date   NULL ,
	[RKVORSCHUSSDM]	decimal(10, 2)   NULL ,
	[RKVORSCHUSSDAT]	datetime   NULL ,
	[RKVORSCHUSSRUECKDM]	decimal(10, 2)   NULL ,
	[RKVORSCHUSSRUECKDAT]	datetime   NULL ,
	[RKVORSCHUSSRUECKGRUND]	varchar(50)   NULL ,
	[RKDATEVDATE]	datetime   NULL ,
	[RKVECODE]	varchar(10)   NULL ,
	[RKVEVSNR]	varchar(50)   NULL ,
	[RKABRID]	varchar(36)   NULL ,
	[RKBEGUID]	varchar(36)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RECHKO2] ( 
	[RKARZTNR]	bigint NOT NULL ,
	[RKHALTERNR]	bigint NOT NULL ,
	[RKRECHNR]	integer NOT NULL ,
	[RKKZ]	smallint   NULL ,
	[RKJOURNSEIT]	integer   NULL ,
	[RKBUCHTEXT]	varchar(50)   NULL ,
	[RKRECHDAT]	date   NULL ,
	[RKFAELLDAT]	date   NULL ,
	[RKDMLEIS]	decimal(8, 2)   NULL ,
	[RKDMARZN]	decimal(8, 2)   NULL ,
	[RKDMMAHN]	decimal(8, 2)   NULL ,
	[RKDMZINS]	decimal(8, 2)   NULL ,
	[RKKZMWST]	smallint   NULL ,
	[RKMAHNSTUFE]	smallint   NULL ,
	[RKMAHNSPERR]	smallint   NULL ,
	[RKMAHNERST]	date   NULL ,
	[RKZALEIS]	decimal(8, 2)   NULL ,
	[RKZAARZN]	decimal(8, 2)   NULL ,
	[RKZAMAHN]	decimal(8, 2)   NULL ,
	[RKZAZINS]	decimal(8, 2)   NULL ,
	[RKKZZAHL]	smallint   NULL ,
	[RKVERJAEHR]	date   NULL ,
	[RKMAHNLTZT]	date   NULL ,
	[RKMAHNDMABR]	decimal(5, 2)   NULL ,
	[RKMBDMABR]	decimal(6, 2)   NULL ,
	[RKMBZAABR]	decimal(6, 2)   NULL ,
	[RKMBDMOPL]	decimal(6, 2)   NULL ,
	[RKMBZAOPL]	decimal(6, 2)   NULL ,
	[RKTEILZDAT]	date   NULL ,
	[RKOPNR]	integer   NULL ,
	[RKKZRECHDRU]	smallint   NULL ,
	[RKKONTROLL]	varchar(15)   NULL ,
	[RKBARCODE]	varchar(13)   NULL ,
	[RKREVERSE]	smallint   NULL ,
	[RKMAHNSPERRBIS]	date   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RECHPO] ( 
	[RPARZTNR]	bigint NOT NULL ,
	[RPHALTERNR]	bigint NOT NULL ,
	[RPRECHNR]	integer NOT NULL ,
	[RPLA]	varchar(1)   NULL ,
	[RPMWST]	decimal(5, 2)   NULL ,
	[RPRECHTEXT]	varchar(90)   NULL ,
	[RPDATUM]	date   NULL ,
	[RPDM]	decimal(10, 2)   NULL ,
	[RPTEXT]	smallint   NULL ,
	[RPDMZAHL]	decimal(10, 2)   NULL ,
	[RPZEIDAT]	datetime   NULL ,
	[RPKONTROLL]	varchar(15)   NULL ,
	[RPREVERSEVERSION]	smallint   NULL ,
	[RPRABATT]	decimal(5, 2)   NULL ,
	[RPFORMAT]	smallint   NULL ,
	[RPMOID]	varchar(19)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RECHTEXTE] ( 
	[RTZEILE1]	varchar(70)   NULL ,
	[RTZEILE2]	varchar(70)   NULL ,
	[RTARZTNR]	bigint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RISER] ( 
	[RIARZTNR]	bigint   NULL ,
	[RIHALTERNR]	bigint   NULL ,
	[RIZEI]	varchar(2)   NULL ,
	[RIERFDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RUECKERST] ( 
	[REARZTNR]	bigint   NULL ,
	[REGEBDM]	decimal(8, 2)   NULL ,
	[REUEBERSCH]	decimal(10, 2)   NULL ,
	[REGESGEBDM]	decimal(10, 2)   NULL ,
	[REZAHLDAT]	date   NULL ,
	[REJAHR]	smallint   NULL ,
	[REDM]	decimal(8, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[RUECKRUF] ( 
	[RRPROGVER]	varchar(20)   NULL ,
	[RRSQLBASE]	varchar(10)   NULL ,
	[RRLIZENZ]	varchar(4)   NULL ,
	[RRNAME]	varchar(250)   NULL ,
	[RRPRAXIS]	varchar(250)   NULL ,
	[RRBEMERK]	varchar(max)   NULL ,
	[RRDATUM]	datetime   NULL ,
	[RRUSER]	varchar(20)   NULL ,
	[RRARZTNR]	bigint   NULL ,
	[RRERLDAT]	datetime   NULL ,
	[RRRUFNR]	varchar(100)   NULL ,
	[RRERLUSER]	varchar(20)   NULL ,
	[RRWICHTIG]	smallint   NULL ,
	[RRSCAN]	varchar(250)   NULL ,
	[RRLEVEL]	smallint   NULL ,
	[RRPRODUKT]	varchar(3)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[SPERREN] ( 
	[SPFORM]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[STAFFELN] ( 
	[STNR]	smallint NOT NULL ,
	[ST1]	decimal(5, 2)   NULL ,
	[ST2]	decimal(5, 2)   NULL ,
	[ST3]	decimal(5, 2)   NULL ,
	[ST4]	decimal(5, 2)   NULL ,
	[ST5]	decimal(5, 2)   NULL ,
	[ST6]	decimal(5, 2)   NULL ,
	[STGEB]	decimal(8, 2)   NULL ,
	[STGR6]	decimal(6, 2)   NULL ,
	[STGR5]	decimal(6, 2)   NULL ,
	[STGR4]	decimal(6, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[STEUERBERATER] ( 
	[SBARZTNR]	bigint   NULL ,
	[SBBELDAT]	date   NULL ,
	[SBBELNR]	varchar(10)   NULL ,
	[SBBUCHTEXT]	varchar(50)   NULL ,
	[SBKONTO]	integer   NULL ,
	[SBGEGKTO]	integer   NULL ,
	[SBDM]	decimal(8, 2)   NULL ,
	[SBMWST]	decimal(5, 2)   NULL ,
	[SBSH]	varchar(1)   NULL ,
	[SBUEBDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[SUPPORTLOG] ( 
	[SLARZTNR]	bigint   NULL ,
	[SLZEI]	varchar(3)   NULL ,
	[SLTEXT]	varchar(max)   NULL ,
	[SLPRODUKT]	varchar(20)   NULL ,
	[SLSTART]	datetime   NULL ,
	[SLDAUER]	integer   NULL ,
	[SLERFDAT]	datetime   NULL ,
	[SLPERSON]	varchar(50)   NULL ,
	[SLABSCHLUSS]	datetime   NULL ,
	[SLSCAN]	varchar(250)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TAKONTO] ( 
	[TKARZTNR]	bigint NOT NULL ,
	[TKSA]	smallint   NULL ,
	[TKJOURNALNR]	integer   NULL ,
	[TKBUCHTEXT]	varchar(50)   NULL ,
	[TKHALTERNR]	bigint   NULL ,
	[TKRECHNR]	integer   NULL ,
	[TKBELNR]	integer   NULL ,
	[TKBELDAT]	date   NULL ,
	[TKKONTONR]	integer   NULL ,
	[TKRECHDM]	decimal(11, 2)   NULL ,
	[TKKZHABEN]	smallint   NULL ,
	[TKTAVST]	decimal(10, 2)   NULL ,
	[TKDMZALEIS]	decimal(11, 2)   NULL ,
	[TKDMZAARZN]	decimal(11, 2)   NULL ,
	[TKDMZAMAHN]	decimal(11, 2)   NULL ,
	[TKDMZAMWST]	decimal(10, 2)   NULL ,
	[TKPROZMWST]	decimal(5, 2)   NULL ,
	[TKKZZAHL]	smallint   NULL ,
	[TKDMPORTO]	decimal(10, 2)   NULL ,
	[TKDMABR]	decimal(11, 2)   NULL ,
	[TKDMZAZINS]	decimal(8, 2)   NULL ,
	[TKSOLL]	decimal(12, 2)   NULL ,
	[TKHABEN]	decimal(12, 2)   NULL ,
	[TKDRUCKDATUM2]	datetime   NULL ,
	[TKREVERSE]	smallint   NULL ,
	[TKREVERSEVERSION]	smallint   NULL ,
	[TKDMMAHN]	decimal(10, 2)   NULL ,
	[TKFALL]	smallint   NULL ,
	[TKKONTOCODE]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TAUSER] ( 
	[TAARZTNR]	bigint   NULL ,
	[TAPW]	varchar(50)   NULL ,
	[TAZULASSUNG]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TELEFON] ( 
	[TEARZTNR]	bigint   NULL ,
	[TEHALTERNR]	bigint   NULL ,
	[TERUFNUMMER]	varchar(25)   NULL ,
	[TETYP]	varchar(1)   NULL ,
	[TEBEMERKUNG]	varchar(20)   NULL ,
	[TELETZT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TEMPDATEURO] ( 
	[TDEDAT]	date   NULL ,
	[TDEEURO]	decimal(10, 2)   NULL ,
	[TDETYP]	varchar(2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TKALT] ( 
	[TKAARZTNR]	bigint   NULL ,
	[TKASA]	smallint   NULL ,
	[TKAJOURNALNR]	integer   NULL ,
	[TKABUCHTEXT]	varchar(50)   NULL ,
	[TKAHALTERNR]	bigint   NULL ,
	[TKARECHNR]	integer   NULL ,
	[TKABELNR]	integer   NULL ,
	[TKABELDAT]	date   NULL ,
	[TKAKONTONR]	integer   NULL ,
	[TKARECHDM]	decimal(11, 2)   NULL ,
	[TKAKZHABEN]	smallint   NULL ,
	[TKATAVST]	decimal(10, 2)   NULL ,
	[TKADMZALEIS]	decimal(11, 2)   NULL ,
	[TKADMZAARZN]	decimal(11, 2)   NULL ,
	[TKADMZAMAHN]	decimal(11, 2)   NULL ,
	[TKADMZAMWST]	decimal(10, 2)   NULL ,
	[TKAPROZMWST]	decimal(5, 2)   NULL ,
	[TKAKZZAHL]	smallint   NULL ,
	[TKADMPORTO]	decimal(10, 2)   NULL ,
	[TKADMABR]	decimal(11, 2)   NULL ,
	[TKADMZAZINS]	decimal(8, 2)   NULL ,
	[TKSOLL]	decimal(10, 2)   NULL ,
	[TKHABEN]	decimal(10, 2)   NULL ,
	[TKADRUCKDATUM]	date   NULL ,
	[TKAKONTROLL]	varchar(15)   NULL ,
	[TKAREVERSE]	smallint   NULL ,
	[TKAREVERSEVERSION]	smallint   NULL ,
	[TKADEAKTIV]	smallint   NULL ,
	[TKADMMAHN]	decimal(10, 2)   NULL ,
	[TKAFALL]	smallint   NULL ,
	[TKAKONTOCODE]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TRABGBEL] ( 
	[ABKEN]	varchar(8) NOT NULL ,
	[ABDAT]	date   NULL ,
	[ABTNA]	varchar(25)   NULL ,
	[ABTRS]	varchar(20)   NULL ,
	[ABANZ]	decimal(8, 2)   NULL ,
	[ABBEZ]	varchar(45)   NULL ,
	[ABCHARGE]	varchar(20)   NULL ,
	[ABWARTEZEIT]	smallint   NULL ,
	[ABWARTEMILCH]	smallint   NULL ,
	[ABVERFALL]	date   NULL ,
	[ABTVARZT]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TRBEHKO] ( 
	[BKEN]	varchar(8) NOT NULL ,
	[BDAT]	date   NULL ,
	[BERFDAT]	datetime   NULL ,
	[BABRDAT]	date   NULL ,
	[BBEHART]	varchar(5)   NULL ,
	[BARZT]	varchar(1)   NULL ,
	[BREC]	varchar(1)   NULL ,
	[BTGS]	varchar(2)   NULL ,
	[BTNA]	varchar(25)   NULL ,
	[BTGB]	date   NULL ,
	[BTRS]	varchar(20)   NULL ,
	[BABRSP]	smallint   NULL ,
	[BTVARZT]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TRBEHPO] ( 
	[BPKEN]	varchar(8) NOT NULL ,
	[BPERFDAT]	datetime   NULL ,
	[BPAKEN]	varchar(8)   NULL ,
	[BPTYP]	varchar(1)   NULL ,
	[BPLFD]	smallint   NULL ,
	[BPANZ]	decimal(8, 2)   NULL ,
	[BPBEZ]	varchar(45)   NULL ,
	[BPPRE]	decimal(9, 2)   NULL ,
	[BPMWST]	decimal(5, 2)   NULL ,
	[BPZAH]	decimal(9, 2)   NULL ,
	[BPTVARZT]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TRIMPFKO] ( 
	[IKEN]	varchar(8) NOT NULL ,
	[IDAT]	date   NULL ,
	[IREC]	varchar(1)   NULL ,
	[ITGS]	varchar(1)   NULL ,
	[ITNA]	varchar(25)   NULL ,
	[ITGB]	date   NULL ,
	[ITRS]	varchar(20)   NULL ,
	[ITVARZT]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TRIMPFPO] ( 
	[IPKEN]	varchar(8) NOT NULL ,
	[IPLFD]	smallint   NULL ,
	[IPANZ]	decimal(8, 2)   NULL ,
	[IPBEZ]	varchar(45)   NULL ,
	[IPTVARZT]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TRKUNDEN] ( 
	[KKEN1]	varchar(8) NOT NULL ,
	[KNAM1]	varchar(30)   NULL ,
	[KNAM2]	varchar(30)   NULL ,
	[KSTR]	varchar(30)   NULL ,
	[KPLZ]	varchar(5)   NULL ,
	[KORT]	varchar(25)   NULL ,
	[KTEL]	varchar(15)   NULL ,
	[KTVS]	integer   NULL ,
	[KTVARZT]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TSEBUCH] ( 
	[TBARZTNR]	bigint   NULL ,
	[TBDATUM]	datetime   NULL ,
	[TBERLDAT]	datetime   NULL ,
	[TBANZAHL]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TSKKOPF] ( 
	[TSKARZTNR]	bigint   NULL ,
	[TSKHALTERNR]	bigint   NULL ,
	[TSKREGNR]	varchar(15)   NULL ,
	[TSKERFDAT]	datetime   NULL ,
	[TSKZM]	varchar(1)   NULL ,
	[TSKFAKTOR]	decimal(3, 1)   NULL ,
	[TSKDATUM]	date   NULL ,
	[TSKTYP]	varchar(6)   NULL ,
	[TSKBEIHILFE]	decimal(8, 2)   NULL ,
	[TSKBEIHDAT]	date   NULL ,
	[TSKERLDAT]	datetime   NULL ,
	[TSKBEIHTEXT]	varchar(50)   NULL ,
	[TSKANTRDAT]	date   NULL ,
	[TSKTADRUCKDAT]	datetime   NULL ,
	[TSKDEAKTIV]	smallint   NULL ,
	[TSKBEIHILFEGUID]	varchar(36)   NULL ,
	[TSKAPERRE]	smallint   NULL ,
	[TSKSPERRE]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TSKMERKLISTE] ( 
	[TSKMLARZTNR]	bigint   NULL ,
	[TSKMLHALTERNR]	bigint   NULL ,
	[TSKMLREGNR]	varchar(15)   NULL ,
	[TSKMLERFDAT]	datetime   NULL ,
	[TSKMLMERKTYP]	varchar(5)   NULL ,
	[TSKMLDAT]	datetime   NULL ,
	[TSKMLDM]	decimal(8, 2)   NULL ,
	[TSKMLTYP]	varchar(6)   NULL ,
	[TSKMLERLDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[TSKPOS] ( 
	[TSKPARZTNR]	bigint   NULL ,
	[TSKPHALTERNR]	bigint   NULL ,
	[TSKPREGNR]	varchar(15)   NULL ,
	[TSKPERFDAT]	datetime   NULL ,
	[TSKPPOS]	smallint   NULL ,
	[TSKPGOT]	varchar(10)   NULL ,
	[TSKPTEXT]	varchar(70)   NULL ,
	[TSKPWERT]	decimal(8, 2)   NULL ,
	[TSKPEINHEIT]	varchar(10)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[UEBERW] ( 
	[UETVNR]	smallint   NULL ,
	[UETVBANK]	varchar(27)   NULL ,
	[UETVBLZ]	varchar(8)   NULL ,
	[UETVKONTO]	varchar(10)   NULL ,
	[UETABANK]	varchar(27)   NULL ,
	[UETABLZ]	varchar(8)   NULL ,
	[UETAKONTO]	varchar(10)   NULL ,
	[UETANAME]	varchar(30)   NULL ,
	[UEVERW1]	varchar(27)   NULL ,
	[UEVERW2]	varchar(27)   NULL ,
	[UEDM]	decimal(10, 2)   NULL ,
	[UEMANUELL]	smallint   NULL ,
	[UEBEARBEITER]	varchar(2)   NULL ,
	[UETYP]	smallint   NULL ,
	[UEBELNR]	integer   NULL ,
	[UEVERW3]	varchar(27)   NULL ,
	[UEVERW4]	varchar(27)   NULL ,
	[UETVIBAN]	varchar(34)   NULL ,
	[UETAIBAN]	varchar(34)   NULL ,
	[UETABIC]	varchar(11)   NULL ,
	[UETVBIC]	varchar(11)   NULL ,
	[UEERLDAT]	datetime   NULL ,
	[UEDEAKTIV]	smallint   NULL ,
	[UEEXPORTNR]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[UEBFIBU] ( 
	[UFBEARBEITER]	varchar(10) NOT NULL ,
	[UFBEARBDATUM]	datetime NOT NULL ,
	[UFARZTNR]	bigint NOT NULL ,
	[UFRECHSUMM]	decimal(10, 2)   NULL ,
	[UFDMGEBUEHR]	decimal(10, 2)   NULL ,
	[UFPROZGEBUEHR]	decimal(5, 2)   NULL ,
	[UFDMPORTO]	decimal(10, 2)   NULL ,
	[UFDMVST]	decimal(10, 2)   NULL ,
	[UFPROZVST]	decimal(5, 2)   NULL ,
	[UFANZRG]	smallint   NULL ,
	[UFSPERRSUMME]	decimal(10, 2)   NULL ,
	[UFVORSCHSUM]	decimal(10, 2)   NULL ,
	[UFVORSCHGEB]	decimal(5, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[UEEMPF] ( 
	[UEEEMPFNAME]	varchar(27)   NULL ,
	[UEEBLZ]	varchar(11)   NULL ,
	[UEEKONTO]	varchar(11)   NULL ,
	[UEEDATUM]	datetime   NULL ,
	[UEEIBAN]	varchar(34)   NULL ,
	[UEEBIC]	varchar(11)   NULL ,
	[UEEDEAKTIV]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[UMFRAGE] ( 
	[NAME]	varchar(25)   NULL ,
	[STIMME]	smallint   NULL ,
	[ZEIT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[VERMERKE] ( 
	[VCODE]	varchar(5)   NULL ,
	[VBEZ]	varchar(100)   NULL ,
	[VART]	smallint   NULL ,
	[VERINNERUNG]	varchar(100)   NULL ,
	[VAKTUELL]	smallint   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[VERSICHERUNGEN] ( 
	[VECODE]	varchar(16)   NULL ,
	[VENAME1]	varchar(50)   NULL ,
	[VENAME2]	varchar(50)   NULL ,
	[VESTRASSE]	varchar(50)   NULL ,
	[VEORT]	varchar(50)   NULL ,
	[VELAND]	varchar(2)   NULL ,
	[VEERFDAT]	datetime   NULL ,
	[VEEMAIL]	varchar(100)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[VETAEMTER] ( 
	[VACODE]	varchar(5)   NULL ,
	[VANAME1]	varchar(50)   NULL ,
	[VANAME2]	varchar(50)   NULL ,
	[VASTR]	varchar(50)   NULL ,
	[VAORT]	varchar(50)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[VORNAMEN] ( 
	[VNANREDE]	varchar(27)   NULL ,
	[VVORNAME]	varchar(30)   NULL ,
	[VVORNAME2]	varchar(30)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[VORTRAEGE] ( 
	[VODATUM]	date   NULL ,
	[VOARZTNR]	bigint   NULL ,
	[VOVORSOLL]	decimal(10, 2)   NULL ,
	[VOVORHABEN]	decimal(10, 2)   NULL ,
	[VOVORMWST]	decimal(10, 2)   NULL ,
	[VOVORVST]	decimal(10, 2)   NULL ,
	[VOOP]	decimal(10, 2)   NULL ,
	[VOVORRECH]	decimal(10, 2)   NULL ,
	[VODEAKTIV]	smallint   NULL ,
	[VOVORMAHN]	decimal(12, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[ZAHLUNGEN] ( 
	[ZAARZTNR]	bigint NOT NULL ,
	[ZAHALTERNR]	bigint NOT NULL ,
	[ZARECHNR]	integer NOT NULL ,
	[ZAKZ]	smallint   NULL ,
	[ZABUCHDAT]	date   NULL ,
	[ZABELEGNR]	integer   NULL ,
	[ZADM]	decimal(10, 2)   NULL ,
	[ZAPROZNR]	integer   NULL ,
	[ZABUCHTEXT]	varchar(50)   NULL ,
	[ZAKONTROLL]	varchar(15)   NULL ,
	[ZARZNR]	integer   NULL ,
	[ZADEAKTIV]	smallint   NULL ,
	[ZAERFDAT]	datetime   NULL ,
	[ZAUEBERWNR]	integer   NULL ,
	[ZADMHF]	decimal(10, 2)   NULL ,
	[ZADMREST]	decimal(10, 2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[ZETTEL] ( 
	[ZVON]	varchar(4)   NULL ,
	[ZAN]	varchar(4)   NULL ,
	[ZARZTNR]	bigint   NULL ,
	[ZHALTERNR]	bigint   NULL ,
	[ZRECHNR]	integer   NULL ,
	[ZBEMERK]	varchar(max)   NULL ,
	[ZDATUM]	date   NULL ,
	[ZWIEDERVOR]	date   NULL ,
	[ZERLEDIGT]	varchar(1)   NULL ,
	[ZART]	varchar(2)   NULL ,
	[ZZM]	varchar(20)   NULL ,
	[ZZMDM]	decimal(9, 2)   NULL ,
	[ZPROZNR]	integer   NULL ,
	[ZAN0]	varchar(50)   NULL ,
	[ZAN1]	varchar(50)   NULL ,
	[ZAN2]	varchar(50)   NULL ,
	[ZAN3]	varchar(50)   NULL ,
	[ZAN4]	varchar(50)   NULL ,
	[ZANZEIGE]	smallint   NULL ,
	[ZRTFTEXT]	varchar(max)   NULL ,
	[ZERFDAT]	datetime   NULL ,
	[ZZAHLART]	varchar(20)   NULL ,
	[ZDEAKTIV]	smallint   NULL ,
	[ZGUID]	varchar(36)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[ZETTELA] ( 
	[ZVON]	varchar(4)   NULL ,
	[ZAN]	varchar(4)   NULL ,
	[ZARZTNR]	bigint   NULL ,
	[ZHALTERNR]	bigint   NULL ,
	[ZRECHNR]	integer   NULL ,
	[ZBEMERK]	varchar(max)   NULL ,
	[ZDATUM]	date   NULL ,
	[ZWIEDERVOR]	date   NULL ,
	[ZERLEDIGT]	varchar(1)   NULL ,
	[ZART]	varchar(2)   NULL ,
	[ZZM]	varchar(20)   NULL ,
	[ZZMDM]	decimal(9, 2)   NULL ,
	[ZPROZNR]	integer   NULL ,
	[ZAN0]	varchar(50)   NULL ,
	[ZAN1]	varchar(50)   NULL ,
	[ZAN2]	varchar(50)   NULL ,
	[ZAN3]	varchar(50)   NULL ,
	[ZAN4]	varchar(50)   NULL ,
	[ZANZEIGE]	smallint   NULL ,
	[ZRTFTEXT]	varchar(max)   NULL ,
	[ZERFDAT]	datetime   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO


Create Table [dbo].[ZUSTAENDIGKEITEN] ( 
	[ZUABC]	varchar(1)   NULL ,
	[ZUZEI]	varchar(2)   NULL ,
	[ROWID]	ROWVERSION ) 
ON [PRIMARY]
GO



 --Kommentare
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Abschlagsvorschlag (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'ABSCHLVOR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mitglieder',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'AERZTE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mindestbetrag bei Monatsmitte-Überweisungen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AABSCHGR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Einzel-, 1=Kurativ-, 2=Apothekenpraxis',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKZPRAXART'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. "Herrn Tierarzt"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ATITEL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'bei kurative Praxis z.B. Nr. der Apotheke. 0 bei Einzelpraxis',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ANR2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'welche Mahnstufe soll eine Rechnung nach Erstellung eines Mahnbescheides erhalten?',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKZMB'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient nur als Vorgabe bei Neuanlage von Haltern',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AMAHNINT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'bei Vollzahlung der Hauptforderung: 0 = nein, 1 = ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AMGAUSBUCHEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Bleibt bei Zahlung / Ausbuchen ein Betrag bis zu diesem €-Betrag noch offen, so wird er ausgebucht.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKLEINBETRAG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht änderbar. ',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AVORRECH'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient nur als Vorgabe bei Neuanlage von Haltern',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'APROZ1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient nur als Vorgabe bei Neuanlage von Haltern',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'APROZ2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient nur als Vorgabe bei Neuanlage von Haltern',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'APROZ3'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht änderbar. ',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AVORSOLL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht änderbar. ',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AVORHABEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht änderbar. ',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AVORMWST'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht änderbar. ',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AVORVST'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Kurzbezeichnung, dient u.a. zur Mitarbeiterzuordnung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AMATCH'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird von H.Sasse über MS Access gepflegt.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'APRAXART'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird von H.Sasse über MS Access gepflegt.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AAKTIV'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird von H.Sasse über MS Access gepflegt.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ATAANZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird von H.Sasse über MS Access gepflegt.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AGKTYP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'auf Rechnung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AMEMO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Beispiel TA4711: Eine Datei F:\tvn32\ta4711.bmp wird erwartet.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ALOGO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient nur als Vorgabe bei Neuanlage von Haltern',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ASKONTO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Memofeld, welches bei der Abrechnung angezeigt/editiert werden kann',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ARECHINFO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht änderbar. ',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AMYCO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Adresse für normalen Schriftverkehr',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AEMAIL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. 30 oder 70',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AVORSCHPROZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Info TVN-intern',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AALLGINFO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird über MS Access ausgewertet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AITMULTI'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. "r Herr r.Mustermann"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ASEHRGEEHRTE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nach Abgabe an Anwalt oder Inkasso ausbuchen? 0=nein, 1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ARAABGAUS'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein, 1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ARUNDSCHREIBEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Schriftverkehr an Mitglied verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ASVORT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Schriftverkehr an Mitglied verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ASVSTR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Schriftverkehr an Mitglied verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ASVNAME2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Schriftverkehr an Mitglied verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ASVNAME1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Schriftverkehr an Mitglied verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ASVTITEL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird noch nicht verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ABILD'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Versand der Kontoauszüge verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKATITEL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Versand der Kontoauszüge verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKANAME1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Versand der Kontoauszüge verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKANAME2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Versand der Kontoauszüge verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKASTR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird für Versand der Kontoauszüge verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'AKAORT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'TVN darf Ratenzahlervereinbarungen treffen? 0=nein, 1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AERZTE',  @level2type=N'COLUMN', @level2name=N'ARAZA'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tagebuch',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'AKTIONEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'RG=Rechnungsabtlg., MB=gerichtl.Mahnverfahren, BR=Brief',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AKTIONEN',  @level2type=N'COLUMN', @level2name=N'AKTYP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'siehe Anwenderhandbuch',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AKTIONEN',  @level2type=N'COLUMN', @level2name=N'AKTYPDETAIL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'der Mahnungen bzw. Rechnungen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AKTIONEN',  @level2type=N'COLUMN', @level2name=N'AKANZAHL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'bei Rechnungserstellung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AKTIONEN',  @level2type=N'COLUMN', @level2name=N'AKRECHSUMM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'bei Rechnungserstellung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AKTIONEN',  @level2type=N'COLUMN', @level2name=N'AKDAUER'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Rechnungsaufschläge',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'AUFSCHLAG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Grenze gilt bis € / 1 = Grenze gilt ab €',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'AUFSCHLAG',  @level2type=N'COLUMN', @level2name=N'AUKZAB'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle enthält die beim letzten Bank-Clearing erhaltenen Daten. Arzt-, Halter und Rechnungsnummer ist 0, falls keine Rechnung gefunden wurde  (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BANKCLEARING'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'alle noch zu verarbeitenden Bankeinzüge werden hier gespeichert, bis sie als Liste ausgedruckt werden.(temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BANKEINZUG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'TVN-Bankkonten',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BANKENSTAMM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'noch nicht "ausgebatchte" Buchungen im Buchungsstapel (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BATCHBUCH'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Daten des Batchprotokolls (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BATCHPROT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Benutzer des TVN-Programmes',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BENUTZER'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein, 1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BENUTZER',  @level2type=N'COLUMN', @level2name=N'BNADMIN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'falls abwesend',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BENUTZER',  @level2type=N'COLUMN', @level2name=N'BNVERTR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Buchungsarten mit Kontierungen für a-, b- und c-Kosten der Mahnbescheide (siehe Anwenderhandbuch)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BUCHART'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'siehe Anwenderhandbuch',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BASTKZARZT1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'siehe Anwenderhandbuch',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BASTKZABRST1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird *10 genommen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BAKTOSOLL1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird *10 genommen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BAKTOHABEN1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird *10 genommen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BAKTOSOLL2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird *10 genommen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BAKTOHABEN2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'siehe Anwenderhandbuch',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BASTKZABRST3'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird *10 genommen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BAKTOSOLL3'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird *10 genommen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'BUCHART',  @level2type=N'COLUMN', @level2name=N'BAKTOHABEN3'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient dem Druck von Buchungsbelegen bei Überweisung von Vorschüssen und Guthaben im Zuge einer Abrechnung',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'BUCHBEL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle, mit der verhindert wird, daß mehrere Benutzer gleichzeitig Rechnungen mit Logos drucken können (temporäre Datensätze)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'DRUKON'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'-nicht verwendet-',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'DRUTEMP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Hilfstabelle (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'DUMMY'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Liste aller Anfragen beim Mitglied. Es wird jeden Monat eine Liste aller Datensätze mit ELERLDATUM = NULL an die Mitglieder mit Bitte um Erledigung geschickt.',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'ERINNERUNGSLISTE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'J=ja, N=nein',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ERINNERUNGSLISTE',  @level2type=N'COLUMN', @level2name=N'ELERLEDIGT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der noch zu druckenden Adreß-Etiketten (temporäre Datensätze) - wird nicht mehr verwendet -',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'ETIDRU'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Protokolltabelle der Euroumstellung',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'EURO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Übergabetabelle zur Sage Classic Line. Bei Übergabe wird FIUEBDAT mit dem aktuellen Datum und Uhrzeit gefüllt.',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'FIBU'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein, 1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'FIBU',  @level2type=N'COLUMN', @level2name=N'FIFERTIG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'siehe Anwenderhandbuch "Buchhaltung" / "Verarbeiten Rechnungen/Mahnungen"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'FIBU',  @level2type=N'COLUMN', @level2name=N'FITYP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'optional',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'FIBU',  @level2type=N'COLUMN', @level2name=N'FIARZTNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'optional',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'FIBU',  @level2type=N'COLUMN', @level2name=N'FIHALTERNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'optional',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'FIBU',  @level2type=N'COLUMN', @level2name=N'FIRECHNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'an Sage Classic Line',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'FIBU',  @level2type=N'COLUMN', @level2name=N'FIUEBDAT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle zum Datensammeln für die Forderungsaufstellung (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'FORDAUFS'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mitteilungen der Geschäftsführung und der Software-Entwicklung an alle Benutzer (bezgl. Softwareänderungen) werden von jedem Benutzer als gelesen bestätigt. Dies wird hier festgehalten. - nicht mehr verwendet -',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'GELESEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Zeichen der Benutzer, die die Info gelesen haben',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'GELESEN',  @level2type=N'COLUMN', @level2name=N'GLZEICHEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Amts- und Landgerichte',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'GERICHTE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0= Amtsgericht, 1 = Landgericht',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'GERICHTE',  @level2type=N'COLUMN', @level2name=N'GELG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'des für diese LZ gültigen Gerichtes',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'GERICHTE',  @level2type=N'COLUMN', @level2name=N'GEBEZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tierhalter (Rechnungsempfänger)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'HALTER'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein, 1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HBANKEINZUG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'falls >=10, wird der Halter nicht in Halterliste und Halteretikett gedruckt',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HETIKETT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. "Herrn"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HTITEL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Achtung: Dies wird unabhängig von der Zahlfrist gewährt!',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HSKONTO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'1=auffällig',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HCREDONEG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'BLZ (wird bei Zahlungseingang automatisch gelesen)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HBLZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Konto (wird bei Zahlungseingang automatisch gelesen)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HKONTO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'für Milchgeldpfändung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HMOLKEREI'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'im Textformat',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HANDEREHALTER'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'vom Tierarzt erfasst am',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HERFDAT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HHALTERNEIN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'für Seuchenbekämpfungsmaßnahmen-Abrechnung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HREGNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nach ISO 3166 plus -- als unbekanntes Land',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'HALTER',  @level2type=N'COLUMN', @level2name=N'HLAND'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Adressen unserer Computerhändler',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'INVAD'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Inventarbuch (Kopfsätze) über Gestellungen an Mitglieder',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'INVKO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. "AUSTAUSCH" oder "WARTUNG"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'INVKO',  @level2type=N'COLUMN', @level2name=N'IKTYP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Inventarbuch (Positionssätze)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'INVPO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'freier Text',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'INVPO',  @level2type=N'COLUMN', @level2name=N'IPCODE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. "Stck." oder "Stunden"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'INVPO',  @level2type=N'COLUMN', @level2name=N'IPPCK'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Aufkleber',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'INVPO',  @level2type=N'COLUMN', @level2name=N'IPINVNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nach ISO 3166',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'LAENDERCODES'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'für Mahnbescheid-Beantragung (nur für die Länder eingeben, die vom AG Uelzen genehmigt sind)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LAENDERCODES',  @level2type=N'COLUMN', @level2name=N'LCKFZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Rechnungen (Kopfsätze) im Bearbeitungsstatus',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'LEISERF'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'M=manuell erfasst, D=aus Datei importiet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERF',  @level2type=N'COLUMN', @level2name=N'LEMANDISK'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird beim (ersten) Druck vergeben',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERF',  @level2type=N'COLUMN', @level2name=N'LERECHNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'keine Funktion mehr',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERF',  @level2type=N'COLUMN', @level2name=N'LEMATCH'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'wird in 1.Verwendungszweckzeile im Überweisungsträger gedruckt',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERF',  @level2type=N'COLUMN', @level2name=N'LEMZF'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Rechnungsbetrag',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERF',  @level2type=N'COLUMN', @level2name=N'LEBRUTTO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'in Tierarzt-Software',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERF',  @level2type=N'COLUMN', @level2name=N'LEHERFDAT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. "HPrüf"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERF',  @level2type=N'COLUMN', @level2name=N'LESTATUS'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'manuelle Erfassung der Rechnungen (Positionen) (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'LEISERFPOS'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'L=Leistung (kurativ), A=Abgabearznei',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERFPOS',  @level2type=N'COLUMN', @level2name=N'LEPLA'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Text ohne Preis, 1=Preis auf Rechnung drucken',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERFPOS',  @level2type=N'COLUMN', @level2name=N'LEPTEXT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'zum Zweck der chronologischen Sortierung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISERFPOS',  @level2type=N'COLUMN', @level2name=N'LEPZEIDAT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Stammdaten der Rechnungspositionen. Dient der Arbeitsvereinfachung, indem häufige Texte per Code aufgerufen werden können.',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'LEISTUNGEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Leistung (kurativ), 1=Abgabearznei',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'LEISTUNGEN',  @level2type=N'COLUMN', @level2name=N'LLEISARZKZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Logbuch',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'LOG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'LOGBUG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Erfassung der Mahnbescheidsanträge',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MAHNBESCH'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein, 1=ja (mehr als ein Schuldner) -keine Funktion mehr-',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNBESCH',  @level2type=N'COLUMN', @level2name=N'MBEHELEUTE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Archivierte Mahndaten',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MAHNDATEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'1=oberhalb der offenen Rechnungen, 2=unterhalb',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNDATEN',  @level2type=N'COLUMN', @level2name=N'MDBEREICH'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mahngebührensätze',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MAHNGEB'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Gebühr inkl. Porto und MwSt.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNGEB',  @level2type=N'COLUMN', @level2name=N'MGDMABR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0 = Standard (Vorgabe, falls nicht explizit aufgeführt)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNGEB',  @level2type=N'COLUMN', @level2name=N'MGARZTNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'€-betrag, der bei Zahlung an TVN fällt',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNGEB',  @level2type=N'COLUMN', @level2name=N'MGDMTVS'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mahnintervalle mit Nummer',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MAHNINT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0 = wird übersprungen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNINT',  @level2type=N'COLUMN', @level2name=N'MITG1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0 = wird übersprungen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNINT',  @level2type=N'COLUMN', @level2name=N'MITG2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0 = wird übersprungen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNINT',  @level2type=N'COLUMN', @level2name=N'MITG3'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Texte auf Mahnungen. Falls zu einer Arztnummer kein Datensatz vorhanden ist, wird der Datensatz mit Arztnummer 0 verwendet.',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MAHNTEXTE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Vorgabe, falls nicht exlizit erfasst',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNTEXTE',  @level2type=N'COLUMN', @level2name=N'MTARZTNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mahnungen',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MAHNUNGEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'am Mahndatum',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MAHNUNGEN',  @level2type=N'COLUMN', @level2name=N'MADMREST'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mahnvorschlag (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MAHNVOR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Hilfstabelle (temporär) für Zinsaufstellungen und MB-Info-Liste',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MBINFOTEMP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Hilfstabelle zum Datensammeln beim Druck von Anträgen zu Vollstreckungsbescheiden und Pfändungsbeschlüssen (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MBKALENDER'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mahnbescheid',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MBKALENDER',  @level2type=N'COLUMN', @level2name=N'MBKZUSTMB'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Vollstreckungsbescheid',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MBKALENDER',  @level2type=N'COLUMN', @level2name=N'MBKZUSTVB'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Hilfstabelle zum MB-Kostenprotokoll (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MBKOSTEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'A=automatisch gebucht, M=manuell gebucht',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'MBKOSTEN',  @level2type=N'COLUMN', @level2name=N'MKTYP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Gemeinden in Deutschland (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MCGEM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Kreise in Deutschland (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MCKREIS'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Bundesländer (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MCLAND'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Orte in Deutschland (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MCORT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Ortsteile in Deutschland (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MCORTT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Postleitzahlen in Deutschland (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MCPLZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Straßen in Deutschland (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MCSTR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'MD1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'PERSONEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Systemtabelle (nicht verwendet)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'PLAN_TABLE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der Mahnbescheidsanträge zur Datenträgererstellung
Die Datenbankfelder entsprechen genau den Namen der Felder der Schnittstellenbeschreibung (liegt bei)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'PREDA'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Prozessregister (Köpfe)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'PROZREG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRR1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRR2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRR3'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRR4'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRR5'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRR6'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRA1'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRA2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRA3'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRA4'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRA5'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'(verwendet nur bis 11.März 2002)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRA6'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'des gebuchten MB-Sammlers',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRRECHNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'der Liste "bezahlte MBs", falls MB bezahlt wurde',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRDRUCKDATUM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein (offen), 1=ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'PROZREG',  @level2type=N'COLUMN', @level2name=N'PRERLEDIGT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Prozessregister (Positionen)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'PROZREGDET'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'an Inkasso bzw. Anwalt abgegebene Rechnungen',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'RECHAB'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Rechnungsköpfe',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'RECHKO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=normale Rechnung, 2=Mahnbescheids-Sammler',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHKO',  @level2type=N'COLUMN', @level2name=N'RKKZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'siehe Anwenderhandbuch',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHKO',  @level2type=N'COLUMN', @level2name=N'RKKZMWST'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein, 1=ja, 2=ja-Halteradresse unklar',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHKO',  @level2type=N'COLUMN', @level2name=N'RKMAHNSPERR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=unbezahlt, 1=teilbezahlt, 2=vollbezahlt, 3=vollbezahlt, aber Mahngeb,. ausgebucht, 4=storniert, 5=storniert',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHKO',  @level2type=N'COLUMN', @level2name=N'RKKZZAHL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Dies kann manuell geändert werden, um einen Mahnaufschub zu bewirken',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHKO',  @level2type=N'COLUMN', @level2name=N'RKTEILZDAT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Rechnungspositionen',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'RECHPO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'L=kurativ, A=Abgabearznei',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHPO',  @level2type=N'COLUMN', @level2name=N'RPLA'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=nein (mit Preis), 1=ja (ohne Preis)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHPO',  @level2type=N'COLUMN', @level2name=N'RPTEXT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient der Sortierung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RECHPO',  @level2type=N'COLUMN', @level2name=N'RPZEIDAT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'RECHTEXTE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Rückerstattungen an Mitglieder',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'RUECKERST'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'des Mitgliedes im angegebenen Jahr',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RUECKERST',  @level2type=N'COLUMN', @level2name=N'REGEBDM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Der Betrag errechnet sich nach folgender Formel: Gebühren je Mitglied / Gesamtgebühren * Jahresüberschuß',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'RUECKERST',  @level2type=N'COLUMN', @level2name=N'REDM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Buchungssperren (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'SPERREN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'bisher nur "Übergaben" verwendet',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'SPERREN',  @level2type=N'COLUMN', @level2name=N'SPFORM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Gebührensätze der TVN',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'STAFFELN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'zusätzlich zu den Prozentsätzen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'STAFFELN',  @level2type=N'COLUMN', @level2name=N'STGEB'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tierarztbuchhaltung (lfd.Monat) (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TAKONTO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'2=Überweisungensabschläge, 1=Eingangszahlungen, 0=alle anderen',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TAKONTO',  @level2type=N'COLUMN', @level2name=N'TKSA'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'im TVN-Programm ohne Funktion',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TAKONTO',  @level2type=N'COLUMN', @level2name=N'TKKONTONR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'ohne USt. und Porto',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TAKONTO',  @level2type=N'COLUMN', @level2name=N'TKRECHDM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Soll, 1=Haben',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TAKONTO',  @level2type=N'COLUMN', @level2name=N'TKKZHABEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'= MwSt. der TVN',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TAKONTO',  @level2type=N'COLUMN', @level2name=N'TKTAVST'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=keine Zahlung, 1=Teilzahlung, 2=Vollzahlung, 3=Vollzahlung, aber Mahngebühr/Zinsen ausgebucht, 4=Storno',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TAKONTO',  @level2type=N'COLUMN', @level2name=N'TKKZZAHL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'TVN-Online Benutzer',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TAUSER'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Inhaber, 1=Mitarbeiter (keine Statistikdaten)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TAUSER',  @level2type=N'COLUMN', @level2name=N'TAZULASSUNG'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'gespeicherte Telefonnummern von Mitgliedern und Haltern',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TELEFON'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Bleibt frei oder ist 0 bei Telefonnummern von Mitgliedern',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TELEFON',  @level2type=N'COLUMN', @level2name=N'TEHALTERNR'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'B=Büro, P=Privat, <leer>=unbekannt',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TELEFON',  @level2type=N'COLUMN', @level2name=N'TETYP'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tierarztbuchhaltung (Vormonate)
Feldnamen sind identisch mit TAKONTO bis auf die letzten beiden Felder.',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TKALT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'dient der Filterung beim nachträglichen Druck der Kontoauszuüge',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'TKALT',  @level2type=N'COLUMN', @level2name=N'TKADRUCKDATUM'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle wird nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TRABGBEL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle wird nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TRBEHKO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle wird nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TRBEHPO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle wird nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TRIMPFKO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle wird nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TRIMPFPO'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle wird nicht verwendet',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'TRKUNDEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'noch an SFirm32 zu übergebende Banküberweisungen (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'UEBERW'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'0=Automatische Buchung, 1=manuelle Buchung',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'UEBERW',  @level2type=N'COLUMN', @level2name=N'UEMANUELL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Hilfstabelle zur Gebührenspeicherung während der Abrechnung (temporär)',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'UEBFIBU'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Tabelle der TVN-internen Codes (z.B. EWA=Einwohnermeldeamt) mit Info, ob diese Code einen Eintrag in der Erinnerungsliste auslöst',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'VERMERKE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'1=MB-Aktionen, 3=Standardbriefe, 0 und 2=andere',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'VERMERKE',  @level2type=N'COLUMN', @level2name=N'VART'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Vornamentabelle zur automatischen Erkennung der Anredeform',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'VORNAMEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'z.B. "Herrn" oder "Frau"',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'VORNAMEN',  @level2type=N'COLUMN', @level2name=N'VNANREDE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'in der deutschen Schreibweise (z.B. Jose statt José)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'VORNAMEN',  @level2type=N'COLUMN', @level2name=N'VVORNAME2'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Kontoauszugs-Vorträge Vormonate',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'VORTRAEGE'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Eingangszahlungen und Stornierungen der Halter',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'ZAHLUNGEN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'1=Teilzahlung, 2=Vollzahlung, 3=Vollzahlung aber Mahngebühren+Zinsen ausgebucht, 4=Storno',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ZAHLUNGEN',  @level2type=N'COLUMN', @level2name=N'ZAKZ'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Zettel Hierin werden verschiedene Aktionen protokolliert, die ein Multifeld (32767 Bytes) benötigen:
Telefonnotizen
Zahlungsmeldungen
Halterstammänderungen
Rechnungsabteilungsinformationen
',  @level0type=N'SCHEMA',  @level0name=N'dbo',  @level1type=N'TABLE', @level1name=N'ZETTEL'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mitarbeiterzeichen oder Mitgliedsnr.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ZETTEL',  @level2type=N'COLUMN', @level2name=N'ZVON'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'Mitarbeiterzeichen, A (Arzt=Mitglied), H (Halter), R (Rechtsanwalt/Inkasso), <leer> andere (z.B. Sparkassen, Gemeinden, usw.)',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ZETTEL',  @level2type=N'COLUMN', @level2name=N'ZAN'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'= Zettelinhalt',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ZETTEL',  @level2type=N'COLUMN', @level2name=N'ZBEMERK'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'N=Nein, J=Ja',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ZETTEL',  @level2type=N'COLUMN', @level2name=N'ZERLEDIGT'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'TN=telefonnotiz, ZM=Zahlungsmeldung, HS=Halterstammänderung, BR=Brief, MB=MB-Status, RA=Rechnungsabtlg.-Info',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ZETTEL',  @level2type=N'COLUMN', @level2name=N'ZART'
GO

EXEC sys.sp_addextendedproperty  @name=N'MS_Description', @value=N'1=es erfolgt keine Anzeige im Bankclearingprotokoll, da es sich um unwchtige Zettel handelt.',  @level0type=N'SCHEMA', @level0name=N'dbo',  @level1type=N'TABLE',  @level1name=N'ZETTEL',  @level2type=N'COLUMN', @level2name=N'ZANZEIGE'
GO

