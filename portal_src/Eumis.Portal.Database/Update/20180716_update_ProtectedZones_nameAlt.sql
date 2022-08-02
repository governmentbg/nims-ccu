GO
ALTER TABLE [dbo].[ProtectedZones] ADD [NameAlt] NVARCHAR(200) NULL;
ALTER TABLE [dbo].[ProtectedZones] ADD [FullPathNameAlt] NVARCHAR(1000) NULL;
GO

UPDATE [ProtectedZones] SET  
    NameAlt = ListData.NameAlt
FROM (VALUES
	('BG0000000', 'All protected zones'),
	('BG0000607', 'Mikre cave'),
	('BG0000118', 'Zlatni pyasatsi'),
	('BG0000119', 'Trite bratya'),
	('BG0000130', 'Kraymorska Dobrudzha'),
	('BG0000132', 'Pobitite kamani'),
	('BG0000133', 'Kamchiyska i Emenska planina'),
	('BG0000134', 'Choklyovo blato'),
	('BG0000136', 'Reka Gorna Luda Kamchia'),
	('BG0000137', 'Reka Dolna Luda Kamchia'),
	('BG0000138', 'Kamenitsa'),
	('BG0000139', 'Luda Kamchia'),
	('BG0000141', 'Reka Kamchia'),
	('BG0000143', 'Karaagach'),
	('BG0000146', 'Plazh Gradina - Zlatna ribka'),
	('BG0000149', 'Rishki prohod'),
	('BG0000151', 'Aytoska planina'),
	('BG0002005', 'Ponor'),
	('BG0002006', 'Ribarnitsi Orsoya'),
	('BG0002007', 'Ostrov Ibisha'),
	('BG0002008', 'Ostrov do Gorni Tsibar'),
	('BG0002009', 'Zlatiyata'),
	('BG0002010', 'Yazovir Pyasachnik'),
	('BG0002012', 'Krumovitsa'),
	('BG0002013', 'Studen kladenets'),
	('BG0002014', 'Madzharovo'),
	('BG0002015', 'Yazovir Konush'),
	('BG0002016', 'Ribarnitsi Plovdiv'),
	('BG0002017', 'Kompleks Belenski ostrovi'),
	('BG0002018', 'Ostrov Vardim'),
	('BG0002019', 'Byala reka'),
	('BG0002020', 'Radinchevo'),
	('BG0000100', 'Plazh Shkorpilovtsi'),
	('BG0000102', 'Dolinata na reka Batova'),
	('BG0000103', 'Galata'),
	('BG0000104', 'Provadiysko - Royaksko plato'),
	('BG0000106', 'Harsovska reka'),
	('BG0000107', 'Suha reka'),
	('BG0000113', 'Vitosha'),
	('BG0000116', 'Kamchia'),
	('BG0000117', 'Kotlenska planina'),
	('BG0000152', 'Pomoriysko ezero'),
	('BG0000154', 'Ezero Durankulak'),
	('BG0000156', 'Shablenski ezeren kompleks'),
	('BG0000164', 'Sinite kamani'),
	('BG0000165', 'Lozenska planina'),
	('BG0000166', 'Vrachanski Balkan'),
	('BG0000167', 'Belasitsa'),
	('BG0000168', 'Ludogorie'),
	('BG0000169', 'Ludogorie - Srebarna'),
	('BG0000171', 'Ludogorie - Boblata'),
	('BG0000173', 'Ostrovche'),
	('BG0000178', 'Ticha'),
	('BG0000180', 'Boblata'),
	('BG0000181', 'Reka Vit'),
	('BG0000182', 'Orsoya'),
	('BG0000190', 'Vitata stena'),
	('BG0000191', 'Varnensko-Beloslavsko ezero'),
	('BG0000192', 'Reka Tundzha 1'),
	('BG0000194', 'Reka Chaya'),
	('BG0000195', 'Reka Tundzha 2'),
	('BG0000196', 'Reka Mochuritsa'),
	('BG0000198', 'Sredetska reka'),
	('BG0000199', 'Tsibar'),
	('BG0000203', 'Tulovo'),
	('BG0000205', 'Straldzha'),
	('BG0000206', 'Sadievo'),
	('BG0000208', 'Bosna'),
	('BG0000209', 'Pirin'),
	('BG0000211', 'Tvardishka planina'),
	('BG0000212', 'Sakar'),
	('BG0000213', 'Tarnovski visochini'),
	('BG0000214', 'Dryanovski manastir'),
	('BG0000216', 'Emen'),
	('BG0000217', 'Zhdreloto na reka Tundzha'),
	('BG0000218', 'Derventski vazvishenia 1'),
	('BG0000219', 'Derventski vazvishenia 2'),
	('BG0000220', 'Dolna Mesta'),
	('BG0000224', 'Ograzhden - Maleshevo'),
	('BG0000230', 'Fakiyska reka'),
	('BG0000231', 'Belenska gora'),
	('BG0000232', 'Batin'),
	('BG0000233', 'Studena reka'),
	('BG0000237', 'Ostrov Pozharevo'),
	('BG0000239', 'Obnova - Karaman dol'),
	('BG0000240', 'Studenets'),
	('BG0000241', 'Srebarna'),
	('BG0000242', 'Zaliv Chengene skele'),
	('BG0000247', 'Nikopolsko plato'),
	('BG0000254', 'Besaparski vazvishenia'),
	('BG0000255', 'Gradinska gora'),
	('BG0000261', 'Yazovir Koprinka'),
	('BG0000263', 'Skalsko'),
	('BG0000266', 'Peshtera Mandrata'),
	('BG0000269', 'Peshtera Lyastovitsata'),
	('BG0000270', 'Atanasovsko ezero'),
	('BG0000271', 'Mandra - Poda'),
	('BG0000273', 'Burgasko ezero'),
	('BG0000275', 'Yazovir Stamboliyski'),
	('BG0000279', 'Stara reka'),
	('BG0000280', 'Zlatarishka reka'),
	('BG0000281', 'Reka Belitsa'),
	('BG0000282', 'Dryanovska reka'),
	('BG0000287', 'Merichlerska reka'),
	('BG0000289', 'Trilistnik'),
	('BG0000291', 'Gora Shishmantsi'),
	('BG0000294', 'Karshalevo'),
	('BG0000295', 'Dolni Koriten'),
	('BG0000298', 'Konyavska planina'),
	('BG0000301', 'Cherni rid'),
	('BG0000304', 'Golak'),
	('BG0000308', 'Verila'),
	('BG0000313', 'Ruy'),
	('BG0000314', 'Rebro'),
	('BG0000322', 'Dragoman'),
	('BG0000332', 'Karlukovski karst'),
	('BG0000334', 'Ostrov'),
	('BG0000335', 'Karaboaz'),
	('BG0000336', 'Zlatia'),
	('BG0000339', 'Rabrovo'),
	('BG0000340', 'Tsar Petrovo'),
	('BG0000365', 'Ovchi halmove'),
	('BG0000366', 'Kresna - Ilindentsi'),
	('BG0000372', 'Tsigansko gradishte'),
	('BG0000374', 'Bebresh'),
	('BG0000377', 'Kalimok - Brashlen'),
	('BG0000382', 'Shumensko plato'),
	('BG0000393', 'Ekokoridor Kamchia - Emine'),
	('BG0000396', 'Persina'),
	('BG0000399', 'Bulgarka'),
	('BG0000401', 'Sveti Iliyski vazvishenia'),
	('BG0000402', 'Bakadzhitsite'),
	('BG0000418', 'Kermenski vazvishenia'),
	('BG0000420', 'Grebenets'),
	('BG0000421', 'Preslavska planina'),
	('BG0000424', 'Reka Vacha - Trakia'),
	('BG0000425', 'Reka Sazliyka'),
	('BG0000426', 'Reka Luda Yana'),
	('BG0000427', 'Reka Ovcharitsa'),
	('BG0000429', 'Reka Stryama'),
	('BG0000432', 'Golyama reka'),
	('BG0000434', 'Banska reka'),
	('BG0000435', 'Reka Kayaliyka'),
	('BG0000436', 'Reka Mechka'),
	('BG0000437', 'Reka Cherkezitsa'),
	('BG0000438', 'Reka Chinardere'),
	('BG0000440', 'Reka Sokolitsa'),
	('BG0000441', 'Reka Blatnitsa'),
	('BG0000442', 'Reka Martinka'),
	('BG0000443', 'Reka Omurovska'),
	('BG0000444', 'Reka Pyasachnik'),
	('BG0000487', 'Bozhite mostove'),
	('BG0000494', 'Tsentralen Balkan'),
	('BG0000495', 'Rila'),
	('BG0000496', 'Rilski manastir'),
	('BG0000497', 'Archar'),
	('BG0000498', 'Vidbol'),
	('BG0000500', 'Voynitsa'),
	('BG0000501', 'Golyama Kamchia'),
	('BG0000503', 'Reka Lom'),
	('BG0000507', 'Deleyna'),
	('BG0000508', 'Reka Skat'),
	('BG0000509', 'Tsibritsa'),
	('BG0000513', 'Voynishki Bakadzhik'),
	('BG0000516', 'Chernata mogila'),
	('BG0000517', 'Portitovtsi - Vladimirovo'),
	('BG0000518', 'Vartopski dol'),
	('BG0000519', 'Mominbrodsko blato'),
	('BG0000521', 'Makresh'),
	('BG0000522', 'Vidinski park'),
	('BG0000523', 'Shishentsi'),
	('BG0000524', 'Orizishteto'),
	('BG0000525', 'Timok'),
	('BG0000526', 'Dolno Linevo'),
	('BG0000527', 'Kozloduy'),
	('BG0000528', 'Ostrovska step - Vadin'),
	('BG0000529', 'Marten - Ryahovo'),
	('BG0000530', 'Pozharevo - Garvan'),
	('BG0000532', 'Ostrov Bliznatsi'),
	('BG0000533', 'Ostrovi Kozloduy'),
	('BG0000534', 'Ostrov Chayka'),
	('BG0000539', 'Gora Topolyane'),
	('BG0000552', 'Ostrov Kutovo'),
	('BG0000553', 'Gora Topolchane'),
	('BG0000554', 'Gora Zhelyu Voyvoda'),
	('BG0000567', 'Gora Blatets'),
	('BG0000569', 'Kardam'),
	('BG0000570', 'Izvorovo - Kraishte'),
	('BG0000572', 'Rositsa - Loznitsa'),
	('BG0000573', 'Kompleks Kaliakra'),
	('BG0000574', 'Aheloy - Ravda - Nesebar'),
	('BG0000576', 'Svishtovska gora'),
	('BG0000578', 'Reka Maritsa'),
	('BG0000587', 'Varkan'),
	('BG0000589', 'Marina dupka'),
	('BG0000591', 'Sedlarkata'),
	('BG0000593', 'Bilernitsite'),
	('BG0000594', 'Bozhia most - Ponora'),
	('BG0000601', 'Kalenska peshtera'),
	('BG0000602', 'Kabiyuk'),
	('BG0000605', 'Bozhkova dupka'),
	('BG0000608', 'Lomovete'),
	('BG0000609', 'Reka Rositsa'),
	('BG0000610', 'Reka Yantra'),
	('BG0000611', 'Yazovir Gorni Dabnik'),
	('BG0000612', 'Reka Blyagornitsa'),
	('BG0000613', 'Reka Iskar'),
	('BG0000614', 'Reka Ogosta'),
	('BG0000615', 'Devetashko plato'),
	('BG0000616', 'Mikre'),
	('BG0000617', 'Reka Palakaria'),
	('BG0000618', 'Vidima'),
	('BG0000620', 'Pomorie'),
	('BG0000621', 'Ezero Shabla - Ezerets'),
	('BG0000622', 'Varnensko - Beloslavski kompleks'),
	('BG0000623', 'Taushan tepe'),
	('BG0000624', 'Lyubash'),
	('BG0000625', 'Izvoro'),
	('BG0000626', 'Krushe'),
	('BG0000627', 'Konunski dol'),
	('BG0000628', 'Chirpanski vazvishenia'),
	('BG0000631', 'Novo selo'),
	('BG0000635', 'Devnenski halmove'),
	('BG0001001', 'Ropotamo'),
	('BG0001004', 'Emine - Irakli'),
	('BG0001007', 'Strandzha'),
	('BG0001011', 'Osogovska planina'),
	('BG0001012', 'Zemen'),
	('BG0001013', 'Skrino'),
	('BG0001014', 'Karlukovo'),
	('BG0001017', 'Karvav kamak'),
	('BG0001021', 'Reka Mesta'),
	('BG0001022', 'Oranovski prolom - Leshko'),
	('BG0001023', 'Rupite - Strumeshnitsa'),
	('BG0001028', 'Sreden Pirin - Alibotush'),
	('BG0001030', 'Rodopi - Zapadni'),
	('BG0001031', 'Rodopi - Sredni'),
	('BG0001032', 'Rodopi - Iztochni'),
	('BG0001033', 'Brestovitsa'),
	('BG0001034', 'Ostar kamak'),
	('BG0001036', 'Balgarski izvor'),
	('BG0001037', 'Pastrina'),
	('BG0001039', 'Popintsi'),
	('BG0001040', 'Zapadna Stara planina i Predbalkan'),
	('BG0001042', 'Iskarski prolom - Rzhana'),
	('BG0001043', 'Etropole - Baylovo'),
	('BG0001307', 'Plana'),
	('BG0001375', 'Ostritsa'),
	('BG0001386', 'Yadenitsa'),
	('BG0001389', 'Sredna gora'),
	('BG0001493', 'Tsentralen Balkan - bufer'),
	('BG0001500', 'Aladzha banka'),
	('BG0001501', 'Emona'),
	('BG0001502', 'Otmanli'),
	('BG0002001', 'Rayanovtsi'),
	('BG0002002', 'Zapaden Balkan'),
	('BG0002003', 'Kresna'),
	('BG0002004', 'Dolni Bogrov - Kazichene'),
	('BG0002021', 'Sakar'),
	('BG0002022', 'Yazovir Rozov kladenets'),
	('BG0002023', 'Yazovir Ovcharitsa'),
	('BG0002024', 'Ribarnitsi Mechka'),
	('BG0002025', 'Lomovete'),
	('BG0002026', 'Derventski vazvishenia'),
	('BG0002027', 'Yazovir Malko Sharkovo'),
	('BG0002028', 'Kompleks Straldzha'),
	('BG0002029', 'Kotlenska planina'),
	('BG0002030', 'Kompleks Kalimok'),
	('BG0002031', 'Stenata'),
	('BG0002038', 'Provadiysko-Royaksko plato'),
	('BG0002039', 'Harsovska reka'),
	('BG0002040', 'Strandzha'),
	('BG0002041', 'Kompleks Ropotamo'),
	('BG0002043', 'Emine'),
	('BG0002044', 'Kamchiyska planina'),
	('BG0002045', 'Kompleks Kamchia'),
	('BG0002046', 'Yatata'),
	('BG0002048', 'Suha reka'),
	('BG0002050', 'Durankulashko ezero'),
	('BG0002051', 'Kaliakra'),
	('BG0002052', 'Yazovir Zhrebchevo'),
	('BG0002053', 'Vrachanski Balkan'),
	('BG0002054', 'Sredna gora'),
	('BG0002057', 'Besaparski ridove'),
	('BG0002058', 'Sinite kamani - Grebenets'),
	('BG0002059', 'Kamenski bair'),
	('BG0002060', 'Galata'),
	('BG0002061', 'Balchik'),
	('BG0002062', 'Ludogorie'),
	('BG0002063', 'Zapadni Rodopi'),
	('BG0002064', 'Garvansko blato'),
	('BG0002065', 'Blato Malak Preslavets'),
	('BG0002066', 'Zapadna Strandzha'),
	('BG0002067', 'Ostrov Golya'),
	('BG0002069', 'Ribarnitsi Zvanichevo'),
	('BG0002070', 'Ribarnitsi Hadzhi Dimitrovo'),
	('BG0002071', 'Most Arda'),
	('BG0002072', 'Melnishki piramidi'),
	('BG0002073', 'Dobrostan'),
	('BG0002074', 'Nikopolsko plato'),
	('BG0002076', 'Mesta'),
	('BG0002077', 'Bakarlaka'),
	('BG0002078', 'Slavyanka'),
	('BG0002079', 'Osogovo'),
	('BG0002081', 'Maritsa - Parvomay'),
	('BG0002082', 'Batova'),
	('BG0002083', 'Svishtovsko-Belenska nizina'),
	('BG0002084', 'Palakaria'),
	('BG0002085', 'Chairya'),
	('BG0002086', 'Orizishta Tsalapitsa'),
	('BG0002087', 'Maritsa - Plovdiv'),
	('BG0002088', 'Mikre'),
	('BG0002089', 'Noevtsi'),
	('BG0002090', 'Berkovitsa'),
	('BG0002091', 'Ostrov Lakat'),
	('BG0002092', 'Harmanliyska reka'),
	('BG0002093', 'Ovcharovo'),
	('BG0002094', 'Adata - Tundzha'),
	('BG0002095', 'Gorni Dabnik - Telish'),
	('BG0002096', 'Obnova'),
	('BG0002097', 'Belite skali'),
	('BG0002098', 'Rupite'),
	('BG0002099', 'Kocherinovo'),
	('BG0002100', 'Dolna Koznitsa'),
	('BG0002101', 'Meshtitsa'),
	('BG0002102', 'Devetashko plato'),
	('BG0002103', 'Zlato pole'),
	('BG0002104', 'Tsibarsko blato'),
	('BG0002105', 'Persenk'),
	('BG0002106', 'Yazovir Ivaylovgrad'),
	('BG0002107', 'Boboshevo'),
	('BG0002108', 'Skrino'),
	('BG0002109', 'Vasilyovska planina'),
	('BG0002110', 'Apriltsi'),
	('BG0002111', 'Velchevo'),
	('BG0002112', 'Ruy'),
	('BG0002113', 'Trigrad - Mursalitsa'),
	('BG0002114', 'Ribarnitsi Chelopechene'),
	('BG0002115', 'Bilo'),
	('BG0002126', 'Pirin bufer'),
	('BG0002128', 'Tsentralen Balkan bufer')) AS ListData(NutsCode, NameAlt) 
WHERE 
    ListData.NutsCode = ProtectedZones.NutsCode
GO
