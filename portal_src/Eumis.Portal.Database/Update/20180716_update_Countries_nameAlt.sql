GO
ALTER TABLE [dbo].[Countries] ADD [NameAlt] NVARCHAR(200) NULL;
GO

UPDATE [Countries] SET  
    NameAlt = ListData.NameAlt
FROM (VALUES
	('AN', 'Netherlands Antilles'),
	('DE', 'Germany'),
	('GP', 'Guadeloupe'),
	('GF', 'French Guiana'),
	('MC', 'Monaco'),
	('MQ', 'Martinique'),
	('PR', 'Puerto Rico'),
	('RE', 'Reunion'),
	('SJ', 'Svalbard and Jan Mayen Islands'),
	('TP', 'East Timor'),
	('RS', 'Serbia'),
	('ZX', 'Not shown'),
	('BL', 'Saint Barthelemy'),
	('CE', 'Czechoslovakia'),
	('MF', 'Saint Martin'),
	('XY', 'Former Yugoslavia'),
	('XU', 'Former Union of Soviet Socialist Republics (USSR)'),
	('EU', 'EU Theritory'),
	('PR', 'Puerto Rico'), 
	('MQ', 'Martinique'),
	('GF', 'French Guiana'),
	('AE', 'Abu Dhabi'),
	('AF', 'Afghanistan'),
	('AE', 'Ajman'),
	('AL', 'Albania'),
	('DZ', 'Algeria'),
	('AS', 'American Samoa'),
	('SC', 'Amirante Isles'),
	('AD', 'Andorra'),
	('AO', 'Angola'),
	('AI', 'Anguilla'),
	('AQ', 'Antarctica'),
	('AG', 'Antigua and Barbuda'),
	('AE', 'Arab Emirates, United'),
	('SY', 'Arab Republic, Syrian'),
	('AR', 'Argentina'),
	('AM', 'Armenia'),
	('AW', 'Aruba'),
	('SH', 'Ascension'),
	('AU', 'Australia'),
	('AT', 'Austria '),
	('AZ', 'Azerbaijan'),
	('PT', 'Azores'),
	('BS', 'Bahamas  (the)'),
	('BH', 'Bahrain'),
	('BD', 'Bangladesh'),
	('BB', 'Barbados'),
	('AG', 'Barbuda, Antigua and'),
	('BY', 'Belarus'),
	('BE', 'Belgium'),
	('BZ', 'Belize'),
	('BJ', 'Benin'),
	('BM', 'Bermuda'),
	('BT', 'Bhutan'),
	('BO', 'Bolivia (Plurinational State of)'),
	('BQ', 'Bonaire, Sint Eustatius and Saba '),
	('MY', 'Borneo, (Sabah)'),
	('ID', 'Borneo, Southern'),
	('BA', 'Bosnia and Herzegovina'),
	('BW', 'Botswana'),
	('BV', 'Bouvet Island'),
	('BR', 'Brazil'),
	('IO', 'British Indian Ocean Territory  (the)'),
	('VG', 'British Virgin Islands'),
	('BN', 'Brunei Darussalam'),
	('BG', 'Bulgaria'),
	('BF', 'Burkina Faso'),
	('BI', 'Burundi'),
	('CH', 'Busingen'),
	('AO', 'Cabinda'),
	('CV', 'Cabo Verde'),
	('TC', 'Caicos Islands, Turks and'),
	('KH', 'Cambodia'),
	('CM', 'Cameroon'),
	('CA', 'Canada'),
	('ES', 'Canary Islands'),
	('KY', 'Cayman Islands  (the)'),
	('CF', 'Central African Republic  (the)'),
	('XC', 'Ceuta'),
	('TD', 'Chad'),
	('CL', 'Chile'),
	('CN', 'China'),
	('CX', 'Christmas Island (Indian Ocean)'),
	('KI', 'Christmas Island (Pacific Ocean)'),
	('CC', 'Cocos (Keeling)  Islands  (the)'),
	('CO', 'Colombia'),
	('KM', 'Comoros  (the)'),
	('CG', 'Congo  (the) '),
	('CD', 'Congo (the Democratic Republic of the)'),
	('CK', 'Cook-Islands  (the)'),
	('CR', 'Costa Rica'),
	('CI', 'Cote dIvoire'),
	('HR', 'Croatia'),
	('CU', 'Cuba'),
	('CW', 'Curacao Island '),
	('CY', 'Cyprus'),
	('CZ', 'Czech Republic  (the)'),
	('BN', 'Darussalam, Brunei '),
	('KP', 'Democratic Peoples Republic of Korea, former North Korea'),
	('CD', 'Democratic Republic  of Congo'),
	('LA', 'Democratic Republic of Laos People'),
	('DK', 'Denmark'),
	('FR', 'Desirade Island'),
	('DM', 'Dominica'),
	('DO', 'Dominican Republic  (the)'),
	('DJ', 'Dschibouti'),
	('AE', 'Dubai'),
	('MY', 'Eastern Malaysia'),
	('EC', 'Ecuador'),
	('EG', 'Egypt'),
	('SV', 'El Salvador'),
	('GQ', 'Equatorial Guinea'),
	('ER', 'Eritrea'),
	('EE', 'Estonia'),
	('ET', 'Ethiopia'),
	('FK', 'Falkland Islands (the) (Malvinas)'),
	('FO', 'Faroe Islands  (the)'),
	('FM', 'Federated States of Micronesia'),
	('FJ', 'Fiji'),
	('FI', 'Finland'),
	('FR', 'France'),
	('FR', 'French Guiana'),
	('PF', 'French Polynesia'),
	('TF', 'French Southern Territories  (the)'),
	('AE', 'Fujairah'),
	('WF', 'Futuna (Islands), Wallis and'),
	('GA', 'Gabon'),
	('EC', 'Galapagos Islands'),
	('GM', 'Gambia  (the)'),
	('GE', 'Georgia'),
	('GH', 'Ghana'),
	('GI', 'Gibraltar'),
	('GR', 'Greece'),
	('GL', 'Greenland'),
	('GD', 'Grenada'),
	('VC', 'Grenadines, Saint Vincent and the '),
	('FR', 'Guadeloupe'),
	('FR', 'Guaiana, French'),
	('GU', 'Guam'),
	('GT', 'Guatemala'),
	('GG', 'Guernsey'),
	('GN', 'Guinea'),
	('GQ', 'Guinea, Equatorial'),
	('GW', 'Guinea-Bissau'),
	('GY', 'Guyana'),
	('HT', 'Haiti'),
	('HM', 'Heard and the McDonald Islands'),
	('BA', 'Herzegovina, Bosnia and'),
	('VA', 'Holy See  (the)'),
	('HN', 'Honduras'),
	('HK', 'Hongkong'),
	('HU', 'Hungary'),
	('IS', 'Iceland'),
	('IN', 'India'),
	('ID', 'Indonesia'),
	('IM', 'Isle of Man'),
	('IR', 'Iran (Islamic Republic of)'),
	('IQ', 'Iraq'),
	('IE', 'Ireland'),
	('IL', 'Israel'),
	('IT', 'Italy'),
	('JM', 'Jamaica'),
	('JP', 'Japan'),
	('JE', 'Jersey'),
	('JO', 'Jordan'),
	('KZ', 'Kazakhstan'),
	('KE', 'Kenya'),
	('KI', 'Kiribati'),
	('KP', 'Korea, the Dem. Peoples Republic of (former North Korea)'),
	('KR', 'Korea, the Republic of (former South Korea)'),
	('XK', 'Kosovo  '),
	('KW', 'Kuwait'),
	('KG', 'Kyrgyzstan'),
	('MY', 'Labuan'),
	('LA', 'Lao Peoples Demokratic Republic (the)'),
	('LV', 'Latvia'),
	('LB', 'Lebanon'),
	('FR', 'Les Saintes Isles'),
	('LS', 'Lesotho'),
	('LR', 'Liberia'),
	('LI', 'Liechtenstein'),
	('LT', 'Lithuania'),
	('LU', 'Luxembourg'),
	('LY', 'Libya'),
	('MO', 'Macau'),
	('MK', 'Macedonia (The former Yugoslav Republic of)'),
	('MG', 'Madagascar'),
	('PT', 'Madeira'),
	('MW', 'Malawi'),
	('MY', 'Malaysia'),
	('MV', 'Maledives'),
	('ML', 'Mali'),
	('MT', 'Malta'),
	('IM', 'Man, Isle of'),
	('FR', 'Marie-Galante Islands'),
	('MH', 'Marshall Islands  (the)'),
	('FR', 'Martinique'),
	('MR', 'Mauretania'),
	('MU', 'Mauritius'),
	('YT', 'Mayotte'),
	('HM', 'McDonald-Islands, Head and'),
	('XL', 'Melilla'),
	('MX', 'Mexico'),
	('FM', 'Micronesia, Federated States of'),
	('PM', 'Miquelon'),
	('MD', 'Moldova  (the Republic of)'),
	('FR', 'Monaco'),
	('MN', 'Mongolia'),
	('ME', 'Montenegro '),
	('MS', 'Montserrat'),
	('MA', 'Morocco'),
	('MZ', 'Mozambique'),
	('MM', 'Myanmar'),
	('NA', 'Namibia'),
	('NR', 'Nauru'),
	('NP', 'Nepal'),
	('NL', 'Netherlands (the)'),
	('KN', 'Nevis, Saint Kitts and'),
	('NC', 'New Caledonia'),
	('NZ', 'New Zealand'),
	('NI', 'Nicaragua'),
	('NE', 'Niger  (the)'),
	('NG', 'Nigeria'),
	('NU', 'Niue'),
	('NF', 'Norfolk Island'),
	('GB', 'Northern Ireland'),
	('MP', 'Northern Mariana Islands  (the)'),
	('NO', 'Norway'),
	('OM', 'Oman'),
	('PK', 'Pakistan'),
	('PW', 'Palau'),
	('PS', 'Palestinian territories'),
	('PA', 'Panama (including Canal Zone)'),
	('PG', 'Papua New Guinea'),
	('PY', 'Paraguay'),
	('PE', 'Peru'),
	('PH', 'Philippines  (the)'),
	('PN', 'Pitcairn Islands Group'),
	('BO', 'Plurinational State of Bolivia'),
	('PL', 'Poland'),
	('PF', 'Polynesia, French'),
	('PT', 'Portugal'),
	('ST', 'Principe,  Sao Tome and  '),
	('US', 'Puerto Rico'),
	('QA', 'Qatar'),
	('AE', 'Ras al Khaimah'),
	('KR', 'Republic of Korea former South Korea'),
	('MD', 'Republic of Moldova   (Moldova) '),
	('CG', 'Republic of the Congo'),
	('FR', 'Reunion'),
	('RO', 'Romania'),
	('RU', 'Russian Federation  (the)'),
	('RW', 'Rwanda'),
	('BQ', 'Saba, Bonaire and Sint Eustatius '),
	('MY', 'Sabah'),
	('FR', 'Saint Barthelemy 5'),
	('SH', 'Saint Helena, Ascension and Tristan da Cunha'),
	('KN', 'Saint Kitts and Nevis'),
	('LC', 'Saint Lucia'),
	('FR', 'Saint Martin (French)'),
	('PM', 'Saint Pierre and Miquelon'),
	('VC', 'Saint Vincent and the Grenadines'),
	('WS', 'Samoa'),
	('AS', 'Samoa, American'),
	('SM', 'San Marino'),
	('ST', 'Sao Tome and Principe'),
	('SA', 'Saudi-Arabia'),
	('SN', 'Senegal'),
	('XS', 'Serbia '),
	('SC', 'Seychelles'),
	('AE', 'Sharjah'),
	('SL', 'Sierra Leone'),
	('SG', 'Singapore'),
	('SX', 'Sint Maarten (Dutch) '),
	('BQ', 'Sint Eustatius, Bonaire and Saba '),
	('SK', 'Slovakia'),
	('SI', 'Slovenia'),
	('PF', 'Society Islands'),
	('SB', 'Solomon Islands'),
	('SO', 'Somalia'),
	('ZA', 'South Africa'),
	('GS', 'South Georgia and South Sandwich Islands'),
	('SS', 'South Sudan'),
	('TF', 'Southern Territory, French'),
	('ES', 'Spain'),
	('NO', 'Spitsbergen'),
	('LK', 'Sri Lanka'),
	('SD', 'Sudan  (the)'),
	('SR', 'Suriname'),
	('NO', 'Svalbard'),
	('HN', 'Swan-Islands'),
	('SZ', 'Swaziland'),
	('SE', 'Sweden'),
	('CH', 'Switzerland'),
	('SY', 'Syrien, Arab Republic '),
	('PF', 'Tahiti'),
	('TW', 'Taiwan (Province of China)'),
	('TJ', 'Tajikistan'),
	('TZ', 'Tanzania, United Republic of'),
	('AU', 'Tasmania'),
	('ES', 'Tenerife'),
	('TH', 'Thailand'),
	('CN', 'Tibet'),
	('TL', 'Timor-Leste'),
	('TT', 'Tobago and Trinidad'),
	('TG', 'Togo'),
	('TK', 'Tokelau'),
	('TO', 'Tonga'),
	('TT', 'Trinidad and Tobago'),
	('SH', 'Tristan da Cunha'),
	('PF', 'Tuamotu (Paumotu) Islands'),
	('TN', 'Tunisia'),
	('TR', 'Turkey'),
	('TM', 'Turkmenistan'),
	('TC', 'Turks and Caicos Islands  (the)'),
	('TV', 'Tuvalu'),
	('UG', 'Uganda'),
	('UA', 'Ukraine'),
	('AE', 'Umm al-Quaiwain'),
	('AE', 'United Arab Emirates  (the)'),
	('GB', 'United Kingdom (the)'),
	('TZ', 'United Republic of Tanzania'),
	('UM', 'United States Minor Outlying Islands  (the)'),
	('US', 'United States of Amerika (the)'),
	('UY', 'Uruguay'),
	('US', 'USA  (the)'),
	('UZ', 'Uzbekistan'),
	('VU', 'Vanuatu'),
	('VA', 'Vatican City State'),
	('VE', 'Venezuela (Bolivarian Republic of) '),
	('VN', 'Viet Nam'),
	('VI', 'Virgin Islands of the United States'),
	('VG', 'Virgin Islands, British'),
	('WF', 'Wallis and Futuna (Islands)'),
	('EH', 'Western Sahara'),
	('YE', 'Yemen'),
	('ZM', 'Zambia'),
	('TZ', 'Zanzibar'),
	('ZW', 'Zimbabwe')	
	) AS ListData(NutsCode, NameAlt) 
WHERE 
    ListData.NutsCode = Countries.NutsCode
GO
