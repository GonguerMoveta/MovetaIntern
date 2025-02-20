CREATE VIEW V_ABSCHLVOR ( [AVARZTNR] , [AVSALDO] , [AVZAHLDM] , [AVBANK] ) 
AS 
SELECT dbo.ABSCHLVOR.[AVARZTNR] , dbo.ABSCHLVOR.[AVSALDO] , dbo.ABSCHLVOR.[AVZAHLDM] , dbo.ABSCHLVOR.[AVBANK] FROM [ABSCHLVOR] 
GO

CREATE VIEW V_AERZTE ( [AARZTNR] , [ANAME1] , [ANAME2] , [ASTR] , [AORT] , [ABANK] , [ABLZ] , [AKONTO] , [AEINTR] , [AABSCHGR] , [AKZPRAXART] , [AFAELLTG] , [AKZMAHNDM] , [ATITEL] , [ANR2] , [ASTAFFEL] , [AKZMB] , [AAUSTRITT] , [AGESTORBEN] , [AGEBDM] , [AMAHNINT] , [AEINLAGE] , [ABEITRAG] , [AMGAUSBUCHEN] , [AKLEINBETRAG] , [AKZGEBSTAF] , [AVORRECH] , [APROZ1] , [APROZ2] , [APROZ3] , [AVORSOLL] , [AVORHABEN] , [AVORMWST] , [AVORVST] , [ADRUCKE] , [AMULTI] , [AMATCH] , [ADISKABR] , [ACOMPEIG] , [APRAXART] , [AAKTIV] , [ATAANZ] , [AGKTYP] , [AMEMO] , [ALOGO] , [ASKONTO] , [ARECHINFO] , [AMYCO] , [AEMAIL] , [AVORSCHPROZ] , [AALLGINFO] , [ATELPRAXIS] , [ATELHANDY] , [ATELPRIVAT] , [ATELFAX] , [AMINDGUTH] , [AITMEMO] , [AITEI] , [AITTV] , [AITPALM] , [AITBRIEF] , [AITMULTI] , [AITGESTSEIT] , [AITLTZTBELAST] , [AITBELASTDM] , [ASEHRGEEHRTE] , [AUSTID] , [ARAABGMS] , [ARAABGAUS] , [ARUNDSCHREIBEN] , [AEMAILTVS] , [AEMAILNEWS] , [ASVORT] , [ASVSTR] , [ASVNAME2] , [ASVNAME1] , [ASVTITEL] , [ABILD] , [AKATITEL] , [AKANAME1] , [AKANAME2] , [AKASTR] , [AKAORT] , [ARAZA] , [ATVNKONTO] , [AREGNR] ) 
AS 
SELECT dbo.AERZTE.[AARZTNR] , dbo.AERZTE.[ANAME1] , dbo.AERZTE.[ANAME2] , dbo.AERZTE.[ASTR] , dbo.AERZTE.[AORT] , dbo.AERZTE.[ABANK] , dbo.AERZTE.[ABLZ] , dbo.AERZTE.[AKONTO] , dbo.AERZTE.[AEINTR] , dbo.AERZTE.[AABSCHGR] , dbo.AERZTE.[AKZPRAXART] , dbo.AERZTE.[AFAELLTG] , dbo.AERZTE.[AKZMAHNDM] , dbo.AERZTE.[ATITEL] , dbo.AERZTE.[ANR2] , dbo.AERZTE.[ASTAFFEL] , dbo.AERZTE.[AKZMB] , dbo.AERZTE.[AAUSTRITT] , dbo.AERZTE.[AGESTORBEN] , dbo.AERZTE.[AGEBDM] , dbo.AERZTE.[AMAHNINT] , dbo.AERZTE.[AEINLAGE] , dbo.AERZTE.[ABEITRAG] , dbo.AERZTE.[AMGAUSBUCHEN] , dbo.AERZTE.[AKLEINBETRAG] , dbo.AERZTE.[AKZGEBSTAF] , dbo.AERZTE.[AVORRECH] , dbo.AERZTE.[APROZ1] , dbo.AERZTE.[APROZ2] , dbo.AERZTE.[APROZ3] , dbo.AERZTE.[AVORSOLL] , dbo.AERZTE.[AVORHABEN] , dbo.AERZTE.[AVORMWST] , dbo.AERZTE.[AVORVST] , dbo.AERZTE.[ADRUCKE] , dbo.AERZTE.[AMULTI] , dbo.AERZTE.[AMATCH] , dbo.AERZTE.[ADISKABR] , dbo.AERZTE.[ACOMPEIG] , dbo.AERZTE.[APRAXART] , dbo.AERZTE.[AAKTIV] , dbo.AERZTE.[ATAANZ] , dbo.AERZTE.[AGKTYP] , dbo.AERZTE.[AMEMO] , dbo.AERZTE.[ALOGO] , dbo.AERZTE.[ASKONTO] , dbo.AERZTE.[ARECHINFO] , dbo.AERZTE.[AMYCO] , dbo.AERZTE.[AEMAIL] , dbo.AERZTE.[AVORSCHPROZ] , dbo.AERZTE.[AALLGINFO] , dbo.AERZTE.[ATELPRAXIS] , dbo.AERZTE.[ATELHANDY] , dbo.AERZTE.[ATELPRIVAT] , dbo.AERZTE.[ATELFAX] , dbo.AERZTE.[AMINDGUTH] , dbo.AERZTE.[AITMEMO] , dbo.AERZTE.[AITEI] , dbo.AERZTE.[AITTV] , dbo.AERZTE.[AITPALM] , dbo.AERZTE.[AITBRIEF] , dbo.AERZTE.[AITMULTI] , dbo.AERZTE.[AITGESTSEIT] , dbo.AERZTE.[AITLTZTBELAST] , dbo.AERZTE.[AITBELASTDM] , dbo.AERZTE.[ASEHRGEEHRTE] , dbo.AERZTE.[AUSTID] , dbo.AERZTE.[ARAABGMS] , dbo.AERZTE.[ARAABGAUS] , dbo.AERZTE.[ARUNDSCHREIBEN] , dbo.AERZTE.[AEMAILTVS] , dbo.AERZTE.[AEMAILNEWS] , dbo.AERZTE.[ASVORT] , dbo.AERZTE.[ASVSTR] , dbo.AERZTE.[ASVNAME2] , dbo.AERZTE.[ASVNAME1] , dbo.AERZTE.[ASVTITEL] , dbo.AERZTE.[ABILD] , dbo.AERZTE.[AKATITEL] , dbo.AERZTE.[AKANAME1] , dbo.AERZTE.[AKANAME2] , dbo.AERZTE.[AKASTR] , dbo.AERZTE.[AKAORT] , dbo.AERZTE.[ARAZA] , dbo.AERZTE.[ATVNKONTO] , dbo.AERZTE.[AREGNR] FROM [AERZTE] 
GO

CREATE VIEW V_AKTIONEN ( [AKARZTNR] , [AKHALTERNR] , [AKRECHNR] , [AKDATUM] , [AKTYP] , [AKTYPDETAIL] , [AKANZAHL] , [AKPROZGEB] , [AKDM] , [AKRECHSUMM] , [AKPROZNR] , [AKZUST] , [AKTERMIN] , [AKDAUER] , [AKZEICHEN] ) 
AS 
SELECT dbo.AKTIONEN.[AKARZTNR] , dbo.AKTIONEN.[AKHALTERNR] , dbo.AKTIONEN.[AKRECHNR] , dbo.AKTIONEN.[AKDATUM] , dbo.AKTIONEN.[AKTYP] , dbo.AKTIONEN.[AKTYPDETAIL] , dbo.AKTIONEN.[AKANZAHL] , dbo.AKTIONEN.[AKPROZGEB] , dbo.AKTIONEN.[AKDM] , dbo.AKTIONEN.[AKRECHSUMM] , dbo.AKTIONEN.[AKPROZNR] , dbo.AKTIONEN.[AKZUST] , dbo.AKTIONEN.[AKTERMIN] , dbo.AKTIONEN.[AKDAUER] , dbo.AKTIONEN.[AKZEICHEN] FROM [AKTIONEN] 
GO

CREATE VIEW V_AUFSCHLAG ( [AUARZTNR] , [AUGRENZE] , [AUDM] , [AUKZAB] , [AURGTEXT] ) 
AS 
SELECT dbo.AUFSCHLAG.[AUARZTNR] , dbo.AUFSCHLAG.[AUGRENZE] , dbo.AUFSCHLAG.[AUDM] , dbo.AUFSCHLAG.[AUKZAB] , dbo.AUFSCHLAG.[AURGTEXT] FROM [AUFSCHLAG] 
GO

CREATE VIEW V_BANKEINZUG ( [BEZBEARBEITER] , [BEZBEARBDATUM] , [BEZARZTNR] , [BEZHALTERNR] , [BEZRECHNR] , [BEZDM] , [BEZERLDAT] ) 
AS 
SELECT dbo.BANKEINZUG.[BEZBEARBEITER] , dbo.BANKEINZUG.[BEZBEARBDATUM] , dbo.BANKEINZUG.[BEZARZTNR] , dbo.BANKEINZUG.[BEZHALTERNR] , dbo.BANKEINZUG.[BEZRECHNR] , dbo.BANKEINZUG.[BEZDM] , dbo.BANKEINZUG.[BEZERLDAT] FROM [BANKEINZUG] 
GO

CREATE VIEW V_BANKENSTAMM ( [BSNR] , [BSNAME] , [BSBLZ] , [BSKONTO] ) 
AS 
SELECT dbo.BANKENSTAMM.[BSNR] , dbo.BANKENSTAMM.[BSNAME] , dbo.BANKENSTAMM.[BSBLZ] , dbo.BANKENSTAMM.[BSKONTO] FROM [BANKENSTAMM] 
GO

CREATE VIEW V_BASISZINSEN ( [BZVON] , [BZBIS] , [BZZINS] ) 
AS 
SELECT dbo.BASISZINSEN.[BZVON] , dbo.BASISZINSEN.[BZBIS] , dbo.BASISZINSEN.[BZZINS] FROM [BASISZINSEN] 
GO

CREATE VIEW V_BATCHBUCH ( [BBARZTNR] , [BBHALTERNR] , [BBRECHNR] , [BBZAHLDM] , [BBBUCHDAT] , [BBKONTO] , [BBBELNR] , [BBPROZNR] ) 
AS 
SELECT dbo.BATCHBUCH.[BBARZTNR] , dbo.BATCHBUCH.[BBHALTERNR] , dbo.BATCHBUCH.[BBRECHNR] , dbo.BATCHBUCH.[BBZAHLDM] , dbo.BATCHBUCH.[BBBUCHDAT] , dbo.BATCHBUCH.[BBKONTO] , dbo.BATCHBUCH.[BBBELNR] , dbo.BATCHBUCH.[BBPROZNR] FROM [BATCHBUCH] 
GO

CREATE VIEW V_BATCHPROT ( [BPARZTNR] , [BPHALTERNR] , [BPRECHNR] , [BPZAHLDM] , [BPBUCHDM] , [BPBUCHDAT] , [BPRESTL] , [BPRESTA] , [BPRESTM] , [BPRESTZ] , [BPKONTO] , [BPERGEBNIS] , [BPBELNR] ) 
AS 
SELECT dbo.BATCHPROT.[BPARZTNR] , dbo.BATCHPROT.[BPHALTERNR] , dbo.BATCHPROT.[BPRECHNR] , dbo.BATCHPROT.[BPZAHLDM] , dbo.BATCHPROT.[BPBUCHDM] , dbo.BATCHPROT.[BPBUCHDAT] , dbo.BATCHPROT.[BPRESTL] , dbo.BATCHPROT.[BPRESTA] , dbo.BATCHPROT.[BPRESTM] , dbo.BATCHPROT.[BPRESTZ] , dbo.BATCHPROT.[BPKONTO] , dbo.BATCHPROT.[BPERGEBNIS] , dbo.BATCHPROT.[BPBELNR] FROM [BATCHPROT] 
GO

CREATE VIEW V_BENUTZER ( [BNCODE] , [BNPW] , [BNNAME] , [BNABTLG] , [BNTEL] , [BNADMIN] , [BNZEI] , [BNEMAIL] , [BNVERTR] ) 
AS 
SELECT dbo.BENUTZER.[BNCODE] , dbo.BENUTZER.[BNPW] , dbo.BENUTZER.[BNNAME] , dbo.BENUTZER.[BNABTLG] , dbo.BENUTZER.[BNTEL] , dbo.BENUTZER.[BNADMIN] , dbo.BENUTZER.[BNZEI] , dbo.BENUTZER.[BNEMAIL] , dbo.BENUTZER.[BNVERTR] FROM [BENUTZER] 
GO

CREATE VIEW V_BUCHART ( [BANR] , [BABUART] , [BAKZ1] , [BADM1] , [BASTKZARZT1] , [BASTKZABRST1] , [BAKTOSOLL1] , [BAKTOHABEN1] , [BAKZ2] , [BADM2] , [BAKTOSOLL2] , [BAKTOHABEN2] , [BAKZ3] , [BADM3] , [BASTKZABRST3] , [BAKTOSOLL3] , [BAKTOHABEN3] ) 
AS 
SELECT dbo.BUCHART.[BANR] , dbo.BUCHART.[BABUART] , dbo.BUCHART.[BAKZ1] , dbo.BUCHART.[BADM1] , dbo.BUCHART.[BASTKZARZT1] , dbo.BUCHART.[BASTKZABRST1] , dbo.BUCHART.[BAKTOSOLL1] , dbo.BUCHART.[BAKTOHABEN1] , dbo.BUCHART.[BAKZ2] , dbo.BUCHART.[BADM2] , dbo.BUCHART.[BAKTOSOLL2] , dbo.BUCHART.[BAKTOHABEN2] , dbo.BUCHART.[BAKZ3] , dbo.BUCHART.[BADM3] , dbo.BUCHART.[BASTKZABRST3] , dbo.BUCHART.[BAKTOSOLL3] , dbo.BUCHART.[BAKTOHABEN3] FROM [BUCHART] 
GO

CREATE VIEW V_DRUKON ( [DKDRUCK] , [DKUSER] ) 
AS 
SELECT dbo.DRUKON.[DKDRUCK] , dbo.DRUKON.[DKUSER] FROM [DRUKON] 
GO

CREATE VIEW V_DRUTEMP ( [DRUTYP] , [DRUBEARBEITER] , [DRUBEARBDATUM] , [DRUARZTNR] , [DRUHALTERNR] , [DRUPOSNR] , [DRUNAME1] , [DRUNAME2] , [DRUSTR] , [DRUORT] , [DRUPLA] , [DRUPMWST] , [DRUPRECHTEXT] , [DRUPDATUM] , [DRUPDM] ) 
AS 
SELECT dbo.DRUTEMP.[DRUTYP] , dbo.DRUTEMP.[DRUBEARBEITER] , dbo.DRUTEMP.[DRUBEARBDATUM] , dbo.DRUTEMP.[DRUARZTNR] , dbo.DRUTEMP.[DRUHALTERNR] , dbo.DRUTEMP.[DRUPOSNR] , dbo.DRUTEMP.[DRUNAME1] , dbo.DRUTEMP.[DRUNAME2] , dbo.DRUTEMP.[DRUSTR] , dbo.DRUTEMP.[DRUORT] , dbo.DRUTEMP.[DRUPLA] , dbo.DRUTEMP.[DRUPMWST] , dbo.DRUTEMP.[DRUPRECHTEXT] , dbo.DRUTEMP.[DRUPDATUM] , dbo.DRUTEMP.[DRUPDM] FROM [DRUTEMP] 
GO

CREATE VIEW V_DUMMY ( [DUMMY] ) 
AS 
SELECT dbo.DUMMY.[DUMMY] FROM [DUMMY] 
GO

CREATE VIEW V_ETIDRU ( [EDN1] , [EDN2] , [EDS] , [EDO] , [EDA] ) 
AS 
SELECT dbo.ETIDRU.[EDN1] , dbo.ETIDRU.[EDN2] , dbo.ETIDRU.[EDS] , dbo.ETIDRU.[EDO] , dbo.ETIDRU.[EDA] FROM [ETIDRU] 
GO

CREATE VIEW V_EURO ( [EUDATE] , [EUTABELLE] , [EUFELD] , [EUBEZ] , [EUDM] , [EUEURO] , [EURUND] ) 
AS 
SELECT dbo.EURO.[EUDATE] , dbo.EURO.[EUTABELLE] , dbo.EURO.[EUFELD] , dbo.EURO.[EUBEZ] , dbo.EURO.[EUDM] , dbo.EURO.[EUEURO] , dbo.EURO.[EURUND] FROM [EURO] 
GO

CREATE VIEW V_FORDAUFS ( [FAPROZNR] , [FADATUM] , [FATEXT] , [FAZINSHF] , [FADMHF] , [FAZINSKO] , [FADMKO] , [FASEITKO] , [FAUNVZDM] , [FAZINSDM] , [FAUSER] ) 
AS 
SELECT dbo.FORDAUFS.[FAPROZNR] , dbo.FORDAUFS.[FADATUM] , dbo.FORDAUFS.[FATEXT] , dbo.FORDAUFS.[FAZINSHF] , dbo.FORDAUFS.[FADMHF] , dbo.FORDAUFS.[FAZINSKO] , dbo.FORDAUFS.[FADMKO] , dbo.FORDAUFS.[FASEITKO] , dbo.FORDAUFS.[FAUNVZDM] , dbo.FORDAUFS.[FAZINSDM] , dbo.FORDAUFS.[FAUSER] FROM [FORDAUFS] 
GO

CREATE VIEW V_GELESEN ( [GLZEICHEN] , [GLTEXT] , [GLDATUM] ) 
AS 
SELECT dbo.GELESEN.[GLZEICHEN] , dbo.GELESEN.[GLTEXT] , dbo.GELESEN.[GLDATUM] FROM [GELESEN] 
GO

CREATE VIEW V_GERICHTE ( [GELG] , [GEPLZ] , [GEBEZ] ) 
AS 
SELECT dbo.GERICHTE.[GELG] , dbo.GERICHTE.[GEPLZ] , dbo.GERICHTE.[GEBEZ] FROM [GERICHTE] 
GO

CREATE VIEW V_HABU ( [HBARZTNR] , [HBHALTERNR] , [HBRECHNR] , [HBPROZNR] , [HBBUCHDAT] , [HBBELEGNR] , [HZAKZ] , [HBCODE] , [HBERFDAT] , [HBDM] , [HBSOLL] , [HBHABEN] , [HBBUCHTEXT] ) 
AS 
SELECT dbo.HABU.[HBARZTNR] , dbo.HABU.[HBHALTERNR] , dbo.HABU.[HBRECHNR] , dbo.HABU.[HBPROZNR] , dbo.HABU.[HBBUCHDAT] , dbo.HABU.[HBBELEGNR] , dbo.HABU.[HZAKZ] , dbo.HABU.[HBCODE] , dbo.HABU.[HBERFDAT] , dbo.HABU.[HBDM] , dbo.HABU.[HBSOLL] , dbo.HABU.[HBHABEN] , dbo.HABU.[HBBUCHTEXT] FROM [HABU] 
GO

CREATE VIEW V_INVAD ( [IACODE] , [IAKDNR] , [IAN1] , [IAN2] , [IAS] , [IAO] , [IALAND] , [IATELBEST] , [IATELSUPP] , [IAEMAIL] , [IAWERTUNG] , [IAANSPRECH] , [IAP] ) 
AS 
SELECT dbo.INVAD.[IACODE] , dbo.INVAD.[IAKDNR] , dbo.INVAD.[IAN1] , dbo.INVAD.[IAN2] , dbo.INVAD.[IAS] , dbo.INVAD.[IAO] , dbo.INVAD.[IALAND] , dbo.INVAD.[IATELBEST] , dbo.INVAD.[IATELSUPP] , dbo.INVAD.[IAEMAIL] , dbo.INVAD.[IAWERTUNG] , dbo.INVAD.[IAANSPRECH] , dbo.INVAD.[IAP] FROM [INVAD] 
GO

CREATE VIEW V_INVKO ( [IKNR] , [IKDATUM] , [IKRECHDAT] , [IKBEARBEITER] , [IKARZTNR] , [IKHAENDLER] , [IKRECHNR] , [IKKDNR] , [IKDM] , [IKTYP] , [IKERLDAT] ) 
AS 
SELECT dbo.INVKO.[IKNR] , dbo.INVKO.[IKDATUM] , dbo.INVKO.[IKRECHDAT] , dbo.INVKO.[IKBEARBEITER] , dbo.INVKO.[IKARZTNR] , dbo.INVKO.[IKHAENDLER] , dbo.INVKO.[IKRECHNR] , dbo.INVKO.[IKKDNR] , dbo.INVKO.[IKDM] , dbo.INVKO.[IKTYP] , dbo.INVKO.[IKERLDAT] FROM [INVKO] 
GO

CREATE VIEW V_INVPO ( [IPNR] , [IPCODE] , [IPMENGE] , [IPPCK] , [IPDM] , [IPINVNR] , [IPARZTNR] , [IPSERIENNR] , [IPBEZ] , [IPPOS] ) 
AS 
SELECT dbo.INVPO.[IPNR] , dbo.INVPO.[IPCODE] , dbo.INVPO.[IPMENGE] , dbo.INVPO.[IPPCK] , dbo.INVPO.[IPDM] , dbo.INVPO.[IPINVNR] , dbo.INVPO.[IPARZTNR] , dbo.INVPO.[IPSERIENNR] , dbo.INVPO.[IPBEZ] , dbo.INVPO.[IPPOS] FROM [INVPO] 
GO

CREATE VIEW V_LAENDERCODES ( [LCNAME] , [LCCODE] , [LCKFZ] ) 
AS 
SELECT dbo.LAENDERCODES.[LCNAME] , dbo.LAENDERCODES.[LCCODE] , dbo.LAENDERCODES.[LCKFZ] FROM [LAENDERCODES] 
GO

CREATE VIEW V_LEISTUNGEN ( [LKENN] , [LBEZ] , [LPREIS] , [LLEISARZKZ] ) 
AS 
SELECT dbo.LEISTUNGEN.[LKENN] , dbo.LEISTUNGEN.[LBEZ] , dbo.LEISTUNGEN.[LPREIS] , dbo.LEISTUNGEN.[LLEISARZKZ] FROM [LEISTUNGEN] 
GO

CREATE VIEW V_LOG ( [LOGNAME] , [LOGDATE] , [LOGTEXT] ) 
AS 
SELECT dbo.LOG.[LOGNAME] , dbo.LOG.[LOGDATE] , dbo.LOG.[LOGTEXT] FROM [LOG] 
GO

CREATE VIEW V_LOGBUG ( [LBPROGNAME] , [LBVERSION] , [LBREL] , [LBARZTNR] , [LBKOMM] , [LBVIA] , [LBBEZ] , [LBTEXT] , [LBDATUM] , [LBTYP] , [LBERLDAT] , [LBTICKET] ) 
AS 
SELECT dbo.LOGBUG.[LBPROGNAME] , dbo.LOGBUG.[LBVERSION] , dbo.LOGBUG.[LBREL] , dbo.LOGBUG.[LBARZTNR] , dbo.LOGBUG.[LBKOMM] , dbo.LOGBUG.[LBVIA] , dbo.LOGBUG.[LBBEZ] , dbo.LOGBUG.[LBTEXT] , dbo.LOGBUG.[LBDATUM] , dbo.LOGBUG.[LBTYP] , dbo.LOGBUG.[LBERLDAT] , dbo.LOGBUG.[LBTICKET] FROM [LOGBUG] 
GO

CREATE VIEW V_MAHNBESCH ( [MBARZTNR] , [MBHALTERNR] , [MBRG1] , [MBRG2] , [MBRG3] , [MBRG4] , [MBRG5] , [MBRG6] , [MBDATUM] , [MBEHELEUTE] , [MBEHEPARTNER] , [MBGERKOSTEN] , [MBGERNAME] , [MBA1] , [MBA2] , [MBA3] , [MBA4] , [MBA5] , [MBA6] , [MBNR] , [MBPROZHF] ) 
AS 
SELECT dbo.MAHNBESCH.[MBARZTNR] , dbo.MAHNBESCH.[MBHALTERNR] , dbo.MAHNBESCH.[MBRG1] , dbo.MAHNBESCH.[MBRG2] , dbo.MAHNBESCH.[MBRG3] , dbo.MAHNBESCH.[MBRG4] , dbo.MAHNBESCH.[MBRG5] , dbo.MAHNBESCH.[MBRG6] , dbo.MAHNBESCH.[MBDATUM] , dbo.MAHNBESCH.[MBEHELEUTE] , dbo.MAHNBESCH.[MBEHEPARTNER] , dbo.MAHNBESCH.[MBGERKOSTEN] , dbo.MAHNBESCH.[MBGERNAME] , dbo.MAHNBESCH.[MBA1] , dbo.MAHNBESCH.[MBA2] , dbo.MAHNBESCH.[MBA3] , dbo.MAHNBESCH.[MBA4] , dbo.MAHNBESCH.[MBA5] , dbo.MAHNBESCH.[MBA6] , dbo.MAHNBESCH.[MBNR] , dbo.MAHNBESCH.[MBPROZHF] FROM [MAHNBESCH] 
GO

CREATE VIEW V_MAHNGEB ( [MGDM1] , [MGDM2] , [MGDM3] , [MGDM4] , [MGDM5] , [MGDM6] , [MGDM7] , [MGDM8] , [MGDM9] , [MGDM10] , [MGDMABR] , [MGARZTNR] , [MGDMTVS] ) 
AS 
SELECT dbo.MAHNGEB.[MGDM1] , dbo.MAHNGEB.[MGDM2] , dbo.MAHNGEB.[MGDM3] , dbo.MAHNGEB.[MGDM4] , dbo.MAHNGEB.[MGDM5] , dbo.MAHNGEB.[MGDM6] , dbo.MAHNGEB.[MGDM7] , dbo.MAHNGEB.[MGDM8] , dbo.MAHNGEB.[MGDM9] , dbo.MAHNGEB.[MGDM10] , dbo.MAHNGEB.[MGDMABR] , dbo.MAHNGEB.[MGARZTNR] , dbo.MAHNGEB.[MGDMTVS] FROM [MAHNGEB] 
GO

CREATE VIEW V_MAHNINT ( [MINR] , [MITG1] , [MITG2] , [MITG3] , [MITG4] , [MITG5] , [MITG6] , [MITG7] , [MITG8] , [MITG9] , [MITG10] ) 
AS 
SELECT dbo.MAHNINT.[MINR] , dbo.MAHNINT.[MITG1] , dbo.MAHNINT.[MITG2] , dbo.MAHNINT.[MITG3] , dbo.MAHNINT.[MITG4] , dbo.MAHNINT.[MITG5] , dbo.MAHNINT.[MITG6] , dbo.MAHNINT.[MITG7] , dbo.MAHNINT.[MITG8] , dbo.MAHNINT.[MITG9] , dbo.MAHNINT.[MITG10] FROM [MAHNINT] 
GO

CREATE VIEW V_MAHNTEXTE ( [MTARZTNR] , [MTSTUFE] , [MTO1] , [MTO2] , [MTO3] , [MTO4] , [MTO5] , [MTO6] , [MTU1] , [MTU2] , [MTU3] , [MTU4] , [MTU5] , [MTU6] , [MTO] , [MTU] ) 
AS 
SELECT dbo.MAHNTEXTE.[MTARZTNR] , dbo.MAHNTEXTE.[MTSTUFE] , dbo.MAHNTEXTE.[MTO1] , dbo.MAHNTEXTE.[MTO2] , dbo.MAHNTEXTE.[MTO3] , dbo.MAHNTEXTE.[MTO4] , dbo.MAHNTEXTE.[MTO5] , dbo.MAHNTEXTE.[MTO6] , dbo.MAHNTEXTE.[MTU1] , dbo.MAHNTEXTE.[MTU2] , dbo.MAHNTEXTE.[MTU3] , dbo.MAHNTEXTE.[MTU4] , dbo.MAHNTEXTE.[MTU5] , dbo.MAHNTEXTE.[MTU6] , dbo.MAHNTEXTE.[MTO] , dbo.MAHNTEXTE.[MTU] FROM [MAHNTEXTE] 
GO

CREATE VIEW V_MAHNUNGEN ( [MAARZTNR] , [MAHALTERNR] , [MARECHNR] , [MADATUM] , [MADMGEB] , [MAMS] , [MADMZINS] , [MADMREST] ) 
AS 
SELECT dbo.MAHNUNGEN.[MAARZTNR] , dbo.MAHNUNGEN.[MAHALTERNR] , dbo.MAHNUNGEN.[MARECHNR] , dbo.MAHNUNGEN.[MADATUM] , dbo.MAHNUNGEN.[MADMGEB] , dbo.MAHNUNGEN.[MAMS] , dbo.MAHNUNGEN.[MADMZINS] , dbo.MAHNUNGEN.[MADMREST] FROM [MAHNUNGEN] 
GO

CREATE VIEW V_MAHNVOR ( [MVARZTNR] , [MVHALTERNR] , [MVRECHNR] , [MVRECHDAT] , [MVMS] , [MVDMREST] , [MVNAME] , [MVMAHNDAT] , [MVDMMAHNGEB] , [MVDMZINS] , [MVMZF] ) 
AS 
SELECT dbo.MAHNVOR.[MVARZTNR] , dbo.MAHNVOR.[MVHALTERNR] , dbo.MAHNVOR.[MVRECHNR] , dbo.MAHNVOR.[MVRECHDAT] , dbo.MAHNVOR.[MVMS] , dbo.MAHNVOR.[MVDMREST] , dbo.MAHNVOR.[MVNAME] , dbo.MAHNVOR.[MVMAHNDAT] , dbo.MAHNVOR.[MVDMMAHNGEB] , dbo.MAHNVOR.[MVDMZINS] , dbo.MAHNVOR.[MVMZF] FROM [MAHNVOR] 
GO

CREATE VIEW V_MBINFOTEMP ( [TARZTNR] , [THALTERNR] , [TRECHNR] , [TDATUM] , [TTYP] , [TTYPTEXT] , [TDMHF] , [TDMMA] , [TZAHF] , [TZAMA] , [TDMGK] , [TDMZA] , [TMS] , [TPROZNR] ) 
AS 
SELECT dbo.MBINFOTEMP.[TARZTNR] , dbo.MBINFOTEMP.[THALTERNR] , dbo.MBINFOTEMP.[TRECHNR] , dbo.MBINFOTEMP.[TDATUM] , dbo.MBINFOTEMP.[TTYP] , dbo.MBINFOTEMP.[TTYPTEXT] , dbo.MBINFOTEMP.[TDMHF] , dbo.MBINFOTEMP.[TDMMA] , dbo.MBINFOTEMP.[TZAHF] , dbo.MBINFOTEMP.[TZAMA] , dbo.MBINFOTEMP.[TDMGK] , dbo.MBINFOTEMP.[TDMZA] , dbo.MBINFOTEMP.[TMS] , dbo.MBINFOTEMP.[TPROZNR] FROM [MBINFOTEMP] 
GO

CREATE VIEW V_MBKOSTEN ( [MKARZTNR] , [MKHALTERNR] , [MKRECHNR] , [MKBUCHDAT] , [MKDMA] , [MKDMB] , [MKDMC] , [MKDRUCK] , [MKTYP] ) 
AS 
SELECT dbo.MBKOSTEN.[MKARZTNR] , dbo.MBKOSTEN.[MKHALTERNR] , dbo.MBKOSTEN.[MKRECHNR] , dbo.MBKOSTEN.[MKBUCHDAT] , dbo.MBKOSTEN.[MKDMA] , dbo.MBKOSTEN.[MKDMB] , dbo.MBKOSTEN.[MKDMC] , dbo.MBKOSTEN.[MKDRUCK] , dbo.MBKOSTEN.[MKTYP] FROM [MBKOSTEN] 
GO

CREATE VIEW V_MCGEM ( [ID] , [GEMEINDE] ) 
AS 
SELECT dbo.MCGEM.[ID] , dbo.MCGEM.[GEMEINDE] FROM [MCGEM] 
GO

CREATE VIEW V_MCKREIS ( [ID] , [KREIS] ) 
AS 
SELECT dbo.MCKREIS.[ID] , dbo.MCKREIS.[KREIS] FROM [MCKREIS] 
GO

CREATE VIEW V_MCLAND ( [ID] , [LAND] ) 
AS 
SELECT dbo.MCLAND.[ID] , dbo.MCLAND.[LAND] FROM [MCLAND] 
GO

CREATE VIEW V_MCORT ( [IDORT] , [STATUS] , [ORT] , [ZUSATZ] , [ORTPHON] ) 
AS 
SELECT dbo.MCORT.[IDORT] , dbo.MCORT.[STATUS] , dbo.MCORT.[ORT] , dbo.MCORT.[ZUSATZ] , dbo.MCORT.[ORTPHON] FROM [MCORT] 
GO

CREATE VIEW V_MCORTT ( [PLZ] , [IDORT] , [IDGEM] , [ORT] , [ORTPHON] ) 
AS 
SELECT dbo.MCORTT.[PLZ] , dbo.MCORTT.[IDORT] , dbo.MCORTT.[IDGEM] , dbo.MCORTT.[ORT] , dbo.MCORTT.[ORTPHON] FROM [MCORTT] 
GO

CREATE VIEW V_MCPLZ ( [PLZ] , [IDORT] , [IDGEM] , [ORT] , [ORTPHON] ) 
AS 
SELECT dbo.MCPLZ.[PLZ] , dbo.MCPLZ.[IDORT] , dbo.MCPLZ.[IDGEM] , dbo.MCPLZ.[ORT] , dbo.MCPLZ.[ORTPHON] FROM [MCPLZ] 
GO

CREATE VIEW V_MCSTR ( [IDORT] , [IDGEM] , [STR] , [STRPHON] , [PLZ] ) 
AS 
SELECT dbo.MCSTR.[IDORT] , dbo.MCSTR.[IDGEM] , dbo.MCSTR.[STR] , dbo.MCSTR.[STRPHON] , dbo.MCSTR.[PLZ] FROM [MCSTR] 
GO

CREATE VIEW V_MD1 ( [MDARZTNR] , [MDHALTERNR] , [MDDATUM] , [MDBEREICH] , [MDTEXT] , [MDMS] , [MDZAHLEING] , [MDLFD] , [MDDM] , [MDMZF] , [MDKONTROLL] ) 
AS 
SELECT dbo.MD1.[MDARZTNR] , dbo.MD1.[MDHALTERNR] , dbo.MD1.[MDDATUM] , dbo.MD1.[MDBEREICH] , dbo.MD1.[MDTEXT] , dbo.MD1.[MDMS] , dbo.MD1.[MDZAHLEING] , dbo.MD1.[MDLFD] , dbo.MD1.[MDDM] , dbo.MD1.[MDMZF] , dbo.MD1.[MDKONTROLL] FROM [MD1] 
GO

CREATE VIEW V_PERSONEN ( [PEARZTNR] , [PEANREDE] , [PETITEL] , [PENACHNAME] , [PEVORNAME] , [PENAME2] , [PESTR] , [PEORT] , [PEGEBTAG] ) 
AS 
SELECT dbo.PERSONEN.[PEARZTNR] , dbo.PERSONEN.[PEANREDE] , dbo.PERSONEN.[PETITEL] , dbo.PERSONEN.[PENACHNAME] , dbo.PERSONEN.[PEVORNAME] , dbo.PERSONEN.[PENAME2] , dbo.PERSONEN.[PESTR] , dbo.PERSONEN.[PEORT] , dbo.PERSONEN.[PEGEBTAG] FROM [PERSONEN] 
GO

CREATE VIEW V_PLAN_TABLE ( [QUERYNO] , [OUTER_TBL] , [IND_USED_O] , [INNER_TBL] , [IND_USED_I] , [RESULT_TBL] , [JOIN_METHOD] , [SORT] , [PLANNO] , [JOIN_TYPE] , [SEQUENCE_NO] , [COMMENT] ) 
AS 
SELECT dbo.PLAN_TABLE.[QUERYNO] , dbo.PLAN_TABLE.[OUTER_TBL] , dbo.PLAN_TABLE.[IND_USED_O] , dbo.PLAN_TABLE.[INNER_TBL] , dbo.PLAN_TABLE.[IND_USED_I] , dbo.PLAN_TABLE.[RESULT_TBL] , dbo.PLAN_TABLE.[JOIN_METHOD] , dbo.PLAN_TABLE.[SORT] , dbo.PLAN_TABLE.[PLANNO] , dbo.PLAN_TABLE.[JOIN_TYPE] , dbo.PLAN_TABLE.[SEQUENCE_NO] , dbo.PLAN_TABLE.[COMMENT] FROM [PLAN_TABLE] 
GO

CREATE VIEW V_PORTO ( [ARZT] , [HALTER] , [DATUM] , [TYP] ) 
AS 
SELECT dbo.PORTO.[ARZT] , dbo.PORTO.[HALTER] , dbo.PORTO.[DATUM] , dbo.PORTO.[TYP] FROM [PORTO] 
GO

CREATE VIEW V_PREDA ( [PREPROZNR] , [PRESA] , [PREAGGM] , [PREAGANRA] , [PREAGN1A] , [PREAGN2A] , [PREAGN3A] , [PREAGRFA] , [PREAGSHA] , [PREAGPLZA] , [PREAGOA] , [PREPGMA] , [PREPGPLZA] , [PREPGOA] , [PREAGANRB] , [PREAGN1B] , [PREAGN2B] , [PREAGN3B] , [PREAGRFB] , [PREAGSHB] , [PREAGPLZB] , [PREAGOB] , [PREPGMB] , [PREPGPLZB] , [PREPGOB] , [PREAGANRC] , [PREAGN1C] , [PREAGN2C] , [PREAGN3C] , [PREAGRFC] , [PREAGSHC] , [PREAGPLZC] , [PREAGOC] , [PREPGMC] , [PREPGPLZC] , [PREPGOC] , [PREAGANRD] , [PREAGN1D] , [PREAGN2D] , [PREAGN3D] , [PREAGRFD] , [PREAGSHD] , [PREAGPLZD] , [PREAGOD] , [PREPGMD] , [PREPGPLZD] , [PREPGOD] , [PREDISKNR] , [PREAGALA] , [PREAGALB] , [PREAGALC] , [PREAGALD] , [PREAGGVFUA] , [PREAGGVVNA] , [PREAGGVSHA] , [PREAGGVPLZA] , [PREAGGVOA] , [PREAGGVALA] , [PREAGGVFUB] , [PREAGGVVNB] , [PREAGGVSHB] , [PREAGGVPLZB] , [PREAGGVOB] , [PREAGGVALB] , [PREAGGVFUC] , [PREAGGVVNC] , [PREAGGVSHC] , [PREAGGVPLZC] , [PREAGGVOC] , [PREAGGVALC] , [PREAGGVFUD] , [PREAGGVVND] , [PREAGGVSHD] , [PREAGGVPLZD] , [PREAGGVOD] , [PREAGGVALD] , [PREAGGVFUA2] , [PREAGGVVNA2] , [PREAGGVSHA2] , [PREAGGVPLZA2] , [PREAGGVOA2] , [PREAGGVALA2] , [PREAGGVFUB2] , [PREAGGVVNB2] , [PREAGGVSHB2] , [PREAGGVPLZB2] , [PREAGGVOB2] , [PREAGGVALB2] , [PREAGGVFUC2] , [PREAGGVVNC2] , [PREAGGVSHC2] , [PREAGGVPLZC2] , [PREAGGVOC2] , [PREAGGVALC2] , [PREAGGVFUD2] , [PREAGGVVND2] , [PREAGGVSHD2] , [PREAGGVPLZD2] , [PREAGGVOD2] , [PREAGGVALD2] , [PREAGGVFUA3] , [PREAGGVVNA3] , [PREAGGVSHA3] , [PREAGGVPLZA3] , [PREAGGVOA3] , [PREAGGVALA3] , [PREAGGVFUB3] , [PREAGGVVNB3] , [PREAGGVSHB3] , [PREAGGVPLZB3] , [PREAGGVOB3] , [PREAGGVALB3] , [PREAGGVFUC3] , [PREAGGVVNC3] , [PREAGGVSHC3] , [PREAGGVPLZC3] , [PREAGGVOC3] , [PREAGGVALC3] , [PREAGGVFUD3] , [PREAGGVVND3] , [PREAGGVSHD3] , [PREAGGVPLZD3] , [PREAGGVOD3] , [PREAGGVALD3] , [PREAGGVFUA4] , [PREAGGVVNA4] , [PREAGGVSHA4] , [PREAGGVPLZA4] , [PREAGGVOA4] , [PREAGGVALA4] , [PREAGGVFUB4] , [PREAGGVVNB4] , [PREAGGVSHB4] , [PREAGGVPLZB4] , [PREAGGVOB4] , [PREAGGVALB4] , [PREAGGVFUC4] , [PREAGGVVNC4] , [PREAGGVSHC4] , [PREAGGVPLZC4] , [PREAGGVOC4] , [PREAGGVALC4] , [PREAGGVFUD4] , [PREAGGVVND4] , [PREAGGVSHD4] , [PREAGGVPLZD4] , [PREAGGVOD4] , [PREAGGVALD4] ) 
AS 
SELECT dbo.PREDA.[PREPROZNR] , dbo.PREDA.[PRESA] , dbo.PREDA.[PREAGGM] , dbo.PREDA.[PREAGANRA] , dbo.PREDA.[PREAGN1A] , dbo.PREDA.[PREAGN2A] , dbo.PREDA.[PREAGN3A] , dbo.PREDA.[PREAGRFA] , dbo.PREDA.[PREAGSHA] , dbo.PREDA.[PREAGPLZA] , dbo.PREDA.[PREAGOA] , dbo.PREDA.[PREPGMA] , dbo.PREDA.[PREPGPLZA] , dbo.PREDA.[PREPGOA] , dbo.PREDA.[PREAGANRB] , dbo.PREDA.[PREAGN1B] , dbo.PREDA.[PREAGN2B] , dbo.PREDA.[PREAGN3B] , dbo.PREDA.[PREAGRFB] , dbo.PREDA.[PREAGSHB] , dbo.PREDA.[PREAGPLZB] , dbo.PREDA.[PREAGOB] , dbo.PREDA.[PREPGMB] , dbo.PREDA.[PREPGPLZB] , dbo.PREDA.[PREPGOB] , dbo.PREDA.[PREAGANRC] , dbo.PREDA.[PREAGN1C] , dbo.PREDA.[PREAGN2C] , dbo.PREDA.[PREAGN3C] , dbo.PREDA.[PREAGRFC] , dbo.PREDA.[PREAGSHC] , dbo.PREDA.[PREAGPLZC] , dbo.PREDA.[PREAGOC] , dbo.PREDA.[PREPGMC] , dbo.PREDA.[PREPGPLZC] , dbo.PREDA.[PREPGOC] , dbo.PREDA.[PREAGANRD] , dbo.PREDA.[PREAGN1D] , dbo.PREDA.[PREAGN2D] , dbo.PREDA.[PREAGN3D] , dbo.PREDA.[PREAGRFD] , dbo.PREDA.[PREAGSHD] , dbo.PREDA.[PREAGPLZD] , dbo.PREDA.[PREAGOD] , dbo.PREDA.[PREPGMD] , dbo.PREDA.[PREPGPLZD] , dbo.PREDA.[PREPGOD] , dbo.PREDA.[PREDISKNR] , dbo.PREDA.[PREAGALA] , dbo.PREDA.[PREAGALB] , dbo.PREDA.[PREAGALC] , dbo.PREDA.[PREAGALD] , dbo.PREDA.[PREAGGVFUA] , dbo.PREDA.[PREAGGVVNA] , dbo.PREDA.[PREAGGVSHA] , dbo.PREDA.[PREAGGVPLZA] , dbo.PREDA.[PREAGGVOA] , dbo.PREDA.[PREAGGVALA] , dbo.PREDA.[PREAGGVFUB] , dbo.PREDA.[PREAGGVVNB] , dbo.PREDA.[PREAGGVSHB] , dbo.PREDA.[PREAGGVPLZB] , dbo.PREDA.[PREAGGVOB] , dbo.PREDA.[PREAGGVALB] , dbo.PREDA.[PREAGGVFUC] , dbo.PREDA.[PREAGGVVNC] , dbo.PREDA.[PREAGGVSHC] , dbo.PREDA.[PREAGGVPLZC] , dbo.PREDA.[PREAGGVOC] , dbo.PREDA.[PREAGGVALC] , dbo.PREDA.[PREAGGVFUD] , dbo.PREDA.[PREAGGVVND] , dbo.PREDA.[PREAGGVSHD] , dbo.PREDA.[PREAGGVPLZD] , dbo.PREDA.[PREAGGVOD] , dbo.PREDA.[PREAGGVALD] , dbo.PREDA.[PREAGGVFUA2] , dbo.PREDA.[PREAGGVVNA2] , dbo.PREDA.[PREAGGVSHA2] , dbo.PREDA.[PREAGGVPLZA2] , dbo.PREDA.[PREAGGVOA2] , dbo.PREDA.[PREAGGVALA2] , dbo.PREDA.[PREAGGVFUB2] , dbo.PREDA.[PREAGGVVNB2] , dbo.PREDA.[PREAGGVSHB2] , dbo.PREDA.[PREAGGVPLZB2] , dbo.PREDA.[PREAGGVOB2] , dbo.PREDA.[PREAGGVALB2] , dbo.PREDA.[PREAGGVFUC2] , dbo.PREDA.[PREAGGVVNC2] , dbo.PREDA.[PREAGGVSHC2] , dbo.PREDA.[PREAGGVPLZC2] , dbo.PREDA.[PREAGGVOC2] , dbo.PREDA.[PREAGGVALC2] , dbo.PREDA.[PREAGGVFUD2] , dbo.PREDA.[PREAGGVVND2] , dbo.PREDA.[PREAGGVSHD2] , dbo.PREDA.[PREAGGVPLZD2] , dbo.PREDA.[PREAGGVOD2] , dbo.PREDA.[PREAGGVALD2] , dbo.PREDA.[PREAGGVFUA3] , dbo.PREDA.[PREAGGVVNA3] , dbo.PREDA.[PREAGGVSHA3] , dbo.PREDA.[PREAGGVPLZA3] , dbo.PREDA.[PREAGGVOA3] , dbo.PREDA.[PREAGGVALA3] , dbo.PREDA.[PREAGGVFUB3] , dbo.PREDA.[PREAGGVVNB3] , dbo.PREDA.[PREAGGVSHB3] , dbo.PREDA.[PREAGGVPLZB3] , dbo.PREDA.[PREAGGVOB3] , dbo.PREDA.[PREAGGVALB3] , dbo.PREDA.[PREAGGVFUC3] , dbo.PREDA.[PREAGGVVNC3] , dbo.PREDA.[PREAGGVSHC3] , dbo.PREDA.[PREAGGVPLZC3] , dbo.PREDA.[PREAGGVOC3] , dbo.PREDA.[PREAGGVALC3] , dbo.PREDA.[PREAGGVFUD3] , dbo.PREDA.[PREAGGVVND3] , dbo.PREDA.[PREAGGVSHD3] , dbo.PREDA.[PREAGGVPLZD3] , dbo.PREDA.[PREAGGVOD3] , dbo.PREDA.[PREAGGVALD3] , dbo.PREDA.[PREAGGVFUA4] , dbo.PREDA.[PREAGGVVNA4] , dbo.PREDA.[PREAGGVSHA4] , dbo.PREDA.[PREAGGVPLZA4] , dbo.PREDA.[PREAGGVOA4] , dbo.PREDA.[PREAGGVALA4] , dbo.PREDA.[PREAGGVFUB4] , dbo.PREDA.[PREAGGVVNB4] , dbo.PREDA.[PREAGGVSHB4] , dbo.PREDA.[PREAGGVPLZB4] , dbo.PREDA.[PREAGGVOB4] , dbo.PREDA.[PREAGGVALB4] , dbo.PREDA.[PREAGGVFUC4] , dbo.PREDA.[PREAGGVVNC4] , dbo.PREDA.[PREAGGVSHC4] , dbo.PREDA.[PREAGGVPLZC4] , dbo.PREDA.[PREAGGVOC4] , dbo.PREDA.[PREAGGVALC4] , dbo.PREDA.[PREAGGVFUD4] , dbo.PREDA.[PREAGGVVND4] , dbo.PREDA.[PREAGGVSHD4] , dbo.PREDA.[PREAGGVPLZD4] , dbo.PREDA.[PREAGGVOD4] , dbo.PREDA.[PREAGGVALD4] FROM [PREDA] 
GO

CREATE VIEW V_PROZREG ( [PRARZTNR] , [PRHALTERNR] , [PRR1] , [PRR2] , [PRR3] , [PRR4] , [PRR5] , [PRR6] , [PRDATUM] , [PRBEMERK] , [PRNR] , [PRA1] , [PRA2] , [PRA3] , [PRA4] , [PRA5] , [PRA6] , [PRRECHNR] , [PRAZ] , [PRTERMINVA] , [PRGERNAME] , [PRTERMINVB] , [PRDMA] , [PRDMB] , [PRDMC] , [PRDMHF] , [PRBEZAHLT] , [PRDRUCKDATUM] , [PRPROZHF] , [PRPROZKO] , [PRERLEDIGT] , [PRDMMAHN] ) 
AS 
SELECT dbo.PROZREG.[PRARZTNR] , dbo.PROZREG.[PRHALTERNR] , dbo.PROZREG.[PRR1] , dbo.PROZREG.[PRR2] , dbo.PROZREG.[PRR3] , dbo.PROZREG.[PRR4] , dbo.PROZREG.[PRR5] , dbo.PROZREG.[PRR6] , dbo.PROZREG.[PRDATUM] , dbo.PROZREG.[PRBEMERK] , dbo.PROZREG.[PRNR] , dbo.PROZREG.[PRA1] , dbo.PROZREG.[PRA2] , dbo.PROZREG.[PRA3] , dbo.PROZREG.[PRA4] , dbo.PROZREG.[PRA5] , dbo.PROZREG.[PRA6] , dbo.PROZREG.[PRRECHNR] , dbo.PROZREG.[PRAZ] , dbo.PROZREG.[PRTERMINVA] , dbo.PROZREG.[PRGERNAME] , dbo.PROZREG.[PRTERMINVB] , dbo.PROZREG.[PRDMA] , dbo.PROZREG.[PRDMB] , dbo.PROZREG.[PRDMC] , dbo.PROZREG.[PRDMHF] , dbo.PROZREG.[PRBEZAHLT] , dbo.PROZREG.[PRDRUCKDATUM] , dbo.PROZREG.[PRPROZHF] , dbo.PROZREG.[PRPROZKO] , dbo.PROZREG.[PRERLEDIGT] , dbo.PROZREG.[PRDMMAHN] FROM [PROZREG] 
GO

CREATE VIEW V_PROZREGDET ( [PRDNR] , [PRDARZTNR] , [PRDHALTERNR] , [PRDRECHNR] ) 
AS 
SELECT dbo.PROZREGDET.[PRDNR] , dbo.PROZREGDET.[PRDARZTNR] , dbo.PROZREGDET.[PRDHALTERNR] , dbo.PROZREGDET.[PRDRECHNR] FROM [PROZREGDET] 
GO

CREATE VIEW V_RECHTEXTE ( [RTZEILE1] , [RTZEILE2] , [RTARZTNR] ) 
AS 
SELECT dbo.RECHTEXTE.[RTZEILE1] , dbo.RECHTEXTE.[RTZEILE2] , dbo.RECHTEXTE.[RTARZTNR] FROM [RECHTEXTE] 
GO

CREATE VIEW V_RUECKERST ( [REARZTNR] , [REGEBDM] , [REUEBERSCH] , [REGESGEBDM] , [REZAHLDAT] , [REJAHR] , [REDM] ) 
AS 
SELECT dbo.RUECKERST.[REARZTNR] , dbo.RUECKERST.[REGEBDM] , dbo.RUECKERST.[REUEBERSCH] , dbo.RUECKERST.[REGESGEBDM] , dbo.RUECKERST.[REZAHLDAT] , dbo.RUECKERST.[REJAHR] , dbo.RUECKERST.[REDM] FROM [RUECKERST] 
GO

CREATE VIEW V_SPERREN ( [SPFORM] ) 
AS 
SELECT dbo.SPERREN.[SPFORM] FROM [SPERREN] 
GO

CREATE VIEW V_STAFFELN ( [STNR] , [ST1] , [ST2] , [ST3] , [ST4] , [ST5] , [ST6] , [STGEB] ) 
AS 
SELECT dbo.STAFFELN.[STNR] , dbo.STAFFELN.[ST1] , dbo.STAFFELN.[ST2] , dbo.STAFFELN.[ST3] , dbo.STAFFELN.[ST4] , dbo.STAFFELN.[ST5] , dbo.STAFFELN.[ST6] , dbo.STAFFELN.[STGEB] FROM [STAFFELN] 
GO

CREATE VIEW V_TELEFON ( [TEARZTNR] , [TEHALTERNR] , [TERUFNUMMER] , [TETYP] , [TEBEMERKUNG] , [TELETZT] ) 
AS 
SELECT dbo.TELEFON.[TEARZTNR] , dbo.TELEFON.[TEHALTERNR] , dbo.TELEFON.[TERUFNUMMER] , dbo.TELEFON.[TETYP] , dbo.TELEFON.[TEBEMERKUNG] , dbo.TELEFON.[TELETZT] FROM [TELEFON] 
GO

CREATE VIEW V_TRABGBEL ( [ABKEN] , [ABDAT] , [ABTNA] , [ABTRS] , [ABANZ] , [ABBEZ] , [ABCHARGE] , [ABWARTEZEIT] , [ABWARTEMILCH] , [ABVERFALL] , [ABTVARZT] ) 
AS 
SELECT dbo.TRABGBEL.[ABKEN] , dbo.TRABGBEL.[ABDAT] , dbo.TRABGBEL.[ABTNA] , dbo.TRABGBEL.[ABTRS] , dbo.TRABGBEL.[ABANZ] , dbo.TRABGBEL.[ABBEZ] , dbo.TRABGBEL.[ABCHARGE] , dbo.TRABGBEL.[ABWARTEZEIT] , dbo.TRABGBEL.[ABWARTEMILCH] , dbo.TRABGBEL.[ABVERFALL] , dbo.TRABGBEL.[ABTVARZT] FROM [TRABGBEL] 
GO

CREATE VIEW V_TRBEHKO ( [BKEN] , [BDAT] , [BERFDAT] , [BABRDAT] , [BBEHART] , [BARZT] , [BREC] , [BTGS] , [BTNA] , [BTGB] , [BTRS] , [BABRSP] , [BTVARZT] ) 
AS 
SELECT dbo.TRBEHKO.[BKEN] , dbo.TRBEHKO.[BDAT] , dbo.TRBEHKO.[BERFDAT] , dbo.TRBEHKO.[BABRDAT] , dbo.TRBEHKO.[BBEHART] , dbo.TRBEHKO.[BARZT] , dbo.TRBEHKO.[BREC] , dbo.TRBEHKO.[BTGS] , dbo.TRBEHKO.[BTNA] , dbo.TRBEHKO.[BTGB] , dbo.TRBEHKO.[BTRS] , dbo.TRBEHKO.[BABRSP] , dbo.TRBEHKO.[BTVARZT] FROM [TRBEHKO] 
GO

CREATE VIEW V_TRBEHPO ( [BPKEN] , [BPERFDAT] , [BPAKEN] , [BPTYP] , [BPLFD] , [BPANZ] , [BPBEZ] , [BPPRE] , [BPMWST] , [BPZAH] , [BPTVARZT] ) 
AS 
SELECT dbo.TRBEHPO.[BPKEN] , dbo.TRBEHPO.[BPERFDAT] , dbo.TRBEHPO.[BPAKEN] , dbo.TRBEHPO.[BPTYP] , dbo.TRBEHPO.[BPLFD] , dbo.TRBEHPO.[BPANZ] , dbo.TRBEHPO.[BPBEZ] , dbo.TRBEHPO.[BPPRE] , dbo.TRBEHPO.[BPMWST] , dbo.TRBEHPO.[BPZAH] , dbo.TRBEHPO.[BPTVARZT] FROM [TRBEHPO] 
GO

CREATE VIEW V_TRIMPFKO ( [IKEN] , [IDAT] , [IREC] , [ITGS] , [ITNA] , [ITGB] , [ITRS] , [ITVARZT] ) 
AS 
SELECT dbo.TRIMPFKO.[IKEN] , dbo.TRIMPFKO.[IDAT] , dbo.TRIMPFKO.[IREC] , dbo.TRIMPFKO.[ITGS] , dbo.TRIMPFKO.[ITNA] , dbo.TRIMPFKO.[ITGB] , dbo.TRIMPFKO.[ITRS] , dbo.TRIMPFKO.[ITVARZT] FROM [TRIMPFKO] 
GO

CREATE VIEW V_TRIMPFPO ( [IPKEN] , [IPLFD] , [IPANZ] , [IPBEZ] , [IPTVARZT] ) 
AS 
SELECT dbo.TRIMPFPO.[IPKEN] , dbo.TRIMPFPO.[IPLFD] , dbo.TRIMPFPO.[IPANZ] , dbo.TRIMPFPO.[IPBEZ] , dbo.TRIMPFPO.[IPTVARZT] FROM [TRIMPFPO] 
GO

CREATE VIEW V_TRKUNDEN ( [KKEN1] , [KNAM1] , [KNAM2] , [KSTR] , [KPLZ] , [KORT] , [KTEL] , [KTVS] , [KTVARZT] ) 
AS 
SELECT dbo.TRKUNDEN.[KKEN1] , dbo.TRKUNDEN.[KNAM1] , dbo.TRKUNDEN.[KNAM2] , dbo.TRKUNDEN.[KSTR] , dbo.TRKUNDEN.[KPLZ] , dbo.TRKUNDEN.[KORT] , dbo.TRKUNDEN.[KTEL] , dbo.TRKUNDEN.[KTVS] , dbo.TRKUNDEN.[KTVARZT] FROM [TRKUNDEN] 
GO

CREATE VIEW V_TSKKOPF ( [TSKARZTNR] , [TSKHALTERNR] , [TSKREGNR] , [TSKERFDAT] , [TSKZM] , [TSKFAKTOR] , [TSKDATUM] , [TSKTYP] , [TSKBEIHILFE] , [TSKBEIHDAT] , [TSKERLDAT] , [TSKBEIHTEXT] , [TSKANTRDAT] , [TSKTADRUCKDAT] ) 
AS 
SELECT dbo.TSKKOPF.[TSKARZTNR] , dbo.TSKKOPF.[TSKHALTERNR] , dbo.TSKKOPF.[TSKREGNR] , dbo.TSKKOPF.[TSKERFDAT] , dbo.TSKKOPF.[TSKZM] , dbo.TSKKOPF.[TSKFAKTOR] , dbo.TSKKOPF.[TSKDATUM] , dbo.TSKKOPF.[TSKTYP] , dbo.TSKKOPF.[TSKBEIHILFE] , dbo.TSKKOPF.[TSKBEIHDAT] , dbo.TSKKOPF.[TSKERLDAT] , dbo.TSKKOPF.[TSKBEIHTEXT] , dbo.TSKKOPF.[TSKANTRDAT] , dbo.TSKKOPF.[TSKTADRUCKDAT] FROM [TSKKOPF] 
GO

CREATE VIEW V_UEBERW ( [UETVNR] , [UETVBANK] , [UETVBLZ] , [UETVKONTO] , [UETABANK] , [UETABLZ] , [UETAKONTO] , [UETANAME] , [UEVERW1] , [UEVERW2] , [UEDM] , [UEMANUELL] , [UEBEARBEITER] ) 
AS 
SELECT dbo.UEBERW.[UETVNR] , dbo.UEBERW.[UETVBANK] , dbo.UEBERW.[UETVBLZ] , dbo.UEBERW.[UETVKONTO] , dbo.UEBERW.[UETABANK] , dbo.UEBERW.[UETABLZ] , dbo.UEBERW.[UETAKONTO] , dbo.UEBERW.[UETANAME] , dbo.UEBERW.[UEVERW1] , dbo.UEBERW.[UEVERW2] , dbo.UEBERW.[UEDM] , dbo.UEBERW.[UEMANUELL] , dbo.UEBERW.[UEBEARBEITER] FROM [UEBERW] 
GO

CREATE VIEW V_UEBFIBU ( [UFBEARBEITER] , [UFBEARBDATUM] , [UFARZTNR] , [UFRECHSUMM] , [UFDMGEBUEHR] , [UFPROZGEBUEHR] , [UFDMPORTO] , [UFDMVST] , [UFPROZVST] , [UFANZRG] ) 
AS 
SELECT dbo.UEBFIBU.[UFBEARBEITER] , dbo.UEBFIBU.[UFBEARBDATUM] , dbo.UEBFIBU.[UFARZTNR] , dbo.UEBFIBU.[UFRECHSUMM] , dbo.UEBFIBU.[UFDMGEBUEHR] , dbo.UEBFIBU.[UFPROZGEBUEHR] , dbo.UEBFIBU.[UFDMPORTO] , dbo.UEBFIBU.[UFDMVST] , dbo.UEBFIBU.[UFPROZVST] , dbo.UEBFIBU.[UFANZRG] FROM [UEBFIBU] 
GO

CREATE VIEW V_UMFRAGE ( [NAME] , [STIMME] , [ZEIT] ) 
AS 
SELECT dbo.UMFRAGE.[NAME] , dbo.UMFRAGE.[STIMME] , dbo.UMFRAGE.[ZEIT] FROM [UMFRAGE] 
GO

CREATE VIEW V_VERMERKE ( [VCODE] , [VBEZ] , [VART] , [VERINNERUNG] ) 
AS 
SELECT dbo.VERMERKE.[VCODE] , dbo.VERMERKE.[VBEZ] , dbo.VERMERKE.[VART] , dbo.VERMERKE.[VERINNERUNG] FROM [VERMERKE] 
GO

CREATE VIEW V_VETAEMTER ( [VACODE] , [VANAME1] , [VANAME2] , [VASTR] , [VAORT] ) 
AS 
SELECT dbo.VETAEMTER.[VACODE] , dbo.VETAEMTER.[VANAME1] , dbo.VETAEMTER.[VANAME2] , dbo.VETAEMTER.[VASTR] , dbo.VETAEMTER.[VAORT] FROM [VETAEMTER] 
GO

CREATE VIEW V_VORNAMEN ( [VNANREDE] , [VVORNAME] , [VVORNAME2] ) 
AS 
SELECT dbo.VORNAMEN.[VNANREDE] , dbo.VORNAMEN.[VVORNAME] , dbo.VORNAMEN.[VVORNAME2] FROM [VORNAMEN] 
GO

CREATE VIEW V_VORTRAEGE ( [VODATUM] , [VOARZTNR] , [VOVORSOLL] , [VOVORHABEN] , [VOVORMWST] , [VOVORVST] , [VOOP] , [VOVORRECH] ) 
AS 
SELECT dbo.VORTRAEGE.[VODATUM] , dbo.VORTRAEGE.[VOARZTNR] , dbo.VORTRAEGE.[VOVORSOLL] , dbo.VORTRAEGE.[VOVORHABEN] , dbo.VORTRAEGE.[VOVORMWST] , dbo.VORTRAEGE.[VOVORVST] , dbo.VORTRAEGE.[VOOP] , dbo.VORTRAEGE.[VOVORRECH] FROM [VORTRAEGE] 
GO

CREATE VIEW V_OPENITEMS ( [HHALTERNR] , [HTI] , [HVN] , [HNN] , [HORT] , [RKARZTNR] , [RKRECHNR] , [RKRECHDAT] , [RKDMLEIS] , [RKDMARZN] , [RKDMMAHN] , [RKMBDMOPL] , [RKDMZINS] , [RKZALEIS] , [RKZAARZN] , [RKZAMAHN] , [RKMBZAOPL] , [RKZAZINS] , [RKMAHNSTUFE] , [RKMAHNLTZT] , [RAAN1] , [RAZEICHEN] , [RADATUM] ) 
AS 
SELECT DISTINCT [hhalternr] , [hti] , [hvn] , [hnn] , [hort] , [rkarztnr] , [rkrechnr] , [rkrechdat] , [rkdmleis] , [rkdmarzn] , [rkdmmahn] , [rkmbdmopl] , [rkdmzins] , [rkzaleis] , [rkzaarzn] , [rkzamahn] , [rkmbzaopl] , [rkzazins] , [rkmahnstufe] , [rkmahnltzt] , [raan1] , [razeichen] , [radatum] FROM [rechko] rk INNER JOIN [halter] h ON [harztnr] = [rkarztnr] AND [hhalternr] = [rkhalternr] LEFT OUTER JOIN [rechab] ra ON [rkarztnr] = [raarztnr] AND [rkhalternr] = [rahalternr] AND [rkrechnr] = [rarechnr] WHERE [rkkzzahl] < 2 AND ( [rkdmleis] + [rkdmarzn] + [rkmbdmopl] + [rkdmmahn] + [rkdmzins] - [rkzaleis] - [rkzaarzn] - [rkmbzaopl] - [rkzamahn] - [rkzazins] ) > 0 
GO

