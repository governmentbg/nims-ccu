ALTER TABLE [dbo].[Indicators] ADD [NameAlt] NVARCHAR(MAX) NULL
GO

ALTER TABLE [dbo].[InvestmentPriorities] ADD [NameAlt] NVARCHAR(MAX) NULL
GO

--Update InvestmentPriorities 

update InvestmentPriorities set NameAlt=N'Enhancing research and innovation (R&I) infrastructure and capacities to develop R&I excellence, and promoting centres of competence, in particular those of European interest' where Code='1а'
update InvestmentPriorities set NameAlt=N'Promoting business investment in R&I, developing links and synergies between enterprises, research and development centres and the higher education sector, in particular promoting investment in product and service development, technology transfer, social innovation, eco-innovation, public service applications, demand stimulation, networking, clusters and open innovation through smart specialisation, and sup' where Code='1b'
update InvestmentPriorities set NameAlt=N'Extending broadband deployment and the roll-out of high-speed networks and supporting the adoption of emerging technologies and networks for the digital economy' where Code='2a'
update InvestmentPriorities set NameAlt=N'Developing ICT products and services, e-commerce, and enhancing demand for ICT' where Code='2b'
update InvestmentPriorities set NameAlt=N'Strengthening ICT applications for e-government, e- learning, e-inclusion, e-culture and e-health' where Code='2c'
update InvestmentPriorities set NameAlt=N'Promoting entrepreneurship, in particular by facilitating the economic exploitation of new ideas and fostering the creation of new firms, including through business incubators' where Code='3a'
update InvestmentPriorities set NameAlt=N'Developing and implementing new business models for SMEs, in particular with regard to internationalisation' where Code='3b'
update InvestmentPriorities set NameAlt=N'Supporting the creation and the extension of advanced capacities for product and service development' where Code='3c'
update InvestmentPriorities set NameAlt=N'Supporting the capacity of SMEs to grow in regional, national and international markets, and to engage in innovation processes' where Code='3d'
update InvestmentPriorities set NameAlt=N'Promoting the production and distribution of energy derived from renewable sources' where Code='4a'
update InvestmentPriorities set NameAlt=N'Promoting the production and distribution of energy derived from renewable sources' where Code='4i'
update InvestmentPriorities set NameAlt=N'Promoting energy efficiency and renewable energy use in enterprises' where Code='4b'
update InvestmentPriorities set NameAlt=N'Promoting energy efficiency and renewable energy use in enterprises' where Code='4ii'
update InvestmentPriorities set NameAlt=N'Supporting energy efficiency, smart energy management and renewable energy use in public infrastructure, including in public buildings, and in the housing sector' where Code='4c'
update InvestmentPriorities set NameAlt=N'Supporting energy efficiency, smart energy management and renewable energy use in public infrastructure, including in public buildings, and in the housing sector' where Code='4iii'
update InvestmentPriorities set NameAlt=N'Developing and implementing smart distribution systems that operate at low and medium voltage levels' where Code='4d'
update InvestmentPriorities set NameAlt=N'Developing and implementing smart distribution systems that operate at low and medium voltage levels' where Code='4iv'
update InvestmentPriorities set NameAlt=N'Promoting low-carbon strategies for all types of territories, in particular for urban areas, including the promotion of sustainable multimodal urban mobility and mitigation-relevant adaptation measures' where Code='4e'
update InvestmentPriorities set NameAlt=N'Promoting low-carbon strategies for all types of territories, in particular for urban areas, including the promotion of sustainable multimodal urban mobility and mitigation-relevant adaptation measures' where Code='4v'
update InvestmentPriorities set NameAlt=N'Promoting research and innovation in, and adoption of, low-carbon technologies' where Code='4f'
update InvestmentPriorities set NameAlt=N'Promoting the use of high-efficiency co-generation of heat and power based on useful heat demand' where Code='4g'
update InvestmentPriorities set NameAlt=N'Promoting the use of high-efficiency co-generation of heat and power based on useful heat demand' where Code='4vi'
update InvestmentPriorities set NameAlt=N'Supporting investment for adaptation to climate change, including ecosystem-based approaches' where Code='5a'
update InvestmentPriorities set NameAlt=N'Supporting investment for adaptation to climate change, including ecosystem-based approaches' where Code='5i'
update InvestmentPriorities set NameAlt=N'Promoting investment to address specific risks, ensuring disaster resilience and developing disaster management systems' where Code='5b'
update InvestmentPriorities set NameAlt=N'Promoting investment to address specific risks, ensuring disaster resilience and developing disaster management systems' where Code='5ii'
update InvestmentPriorities set NameAlt=N'Investing in the waste sector to meet the requirements of the Union''s environmental acquis and to address needs, identified by the Member States, for investment that goes beyond those requirements' where Code='6a'
update InvestmentPriorities set NameAlt=N'Investing in the waste sector to meet the requirements of the Union''s environmental acquis and to address needs, identified by the Member States, for investment that goes beyond those requirements' where Code='6i'
update InvestmentPriorities set NameAlt=N'Investing in the water sector to meet the requirements of the Union''s environmental acquis and to address needs, identified by the Member States, for investment that goes beyond those requirements' where Code='6b'
update InvestmentPriorities set NameAlt=N'Investing in the water sector to meet the requirements of the Union''s environmental acquis and to address needs, identified by the Member States, for investment that goes beyond those requirements' where Code='6ii'
update InvestmentPriorities set NameAlt=N'Conserving, protecting, promoting and developing natural and cultural heritage' where Code='6c'
update InvestmentPriorities set NameAlt=N'Protecting and restoring biodiversity and soil and promoting ecosystem services, including through Natura 2000, and green infrastructure' where Code='6d'
update InvestmentPriorities set NameAlt=N'Protecting and restoring biodiversity and soil and promoting ecosystem services, including through Natura 2000, and green infrastructure' where Code='6iii'
update InvestmentPriorities set NameAlt=N'Taking action to improve the urban environment, to revitalise cities, regenerate and decontaminate brownfield sites (including conversion areas), reduce air pollution and promote noise-reduction measures' where Code='6e'
update InvestmentPriorities set NameAlt=N'Taking action to improve the urban environment, to revitalise cities, regenerate and decontaminate brownfield sites (including conversion areas), reduce air pollution and promote noise-reduction measures' where Code='6iv'
update InvestmentPriorities set NameAlt=N'Promoting innovative technologies to improve environmental protection and resource efficiency in the waste sector, water sector and with regard to soil, or to reduce air pollution' where Code='6f'
update InvestmentPriorities set NameAlt=N'Supporting industrial transition towards a resource- efficient economy, promoting green growth, eco-innovation and environmental performance management in the public and private sectors' where Code='6g'
update InvestmentPriorities set NameAlt=N'Supporting a multimodal Single European Transport Area by investing in the TEN-T' where Code='7a'
update InvestmentPriorities set NameAlt=N'Supporting a multimodal Single European Transport Area by investing in the TEN-T' where Code='7i'
update InvestmentPriorities set NameAlt=N'Enhancing regional mobility by connecting secondary and tertiary nodes to TEN-T infrastructure, including multimodal nodes' where Code='7b'
update InvestmentPriorities set NameAlt=N'Developing and improving environmentally-friendly (including low-noise) and low-carbon transport systems, including inland waterways and maritime transport, ports, multimodal links and airport infrastructure, in order to promote sustainable regional and local mobility' where Code='7c'
update InvestmentPriorities set NameAlt=N'Developing and improving environmentally-friendly (including low-noise) and low-carbon transport systems, including inland waterways and maritime transport, ports, multimodal links and airport infrastructure, in order to promote sustainable regional and local mobility' where Code='7ii'
update InvestmentPriorities set NameAlt=N'Developing and rehabilitating comprehensive, high quality and interoperable railway systems, and promoting noise-reduction measures' where Code='7d'
update InvestmentPriorities set NameAlt=N'Developing and rehabilitating comprehensive, high quality and interoperable railway systems, and promoting noise-reduction measures' where Code='7iii'
update InvestmentPriorities set NameAlt=N'Improving energy efficiency and security of supply through the development of smart energy distribution, storage and transmission systems and through the integration of distributed generation from renewable sources' where Code='7e'
update InvestmentPriorities set NameAlt=N'Supporting the development of business incubators and investment support for self-employment, micro- enterprises and business creation' where Code='8a'
update InvestmentPriorities set NameAlt=N'Supporting employment-friendly growth through the development of endogenous potential as part of a territorial strategy for specific areas, including the conversion of declining industrial regions and enhancement of accessibility to, and development of, specific natural and cultural resources' where Code='8b'
update InvestmentPriorities set NameAlt=N'Supporting local development initiatives and aid for structures providing neighbourhood services to create jobs, where such actions are outside the scope of Regulation (EU) No 1304/2013 of the European Parliament and of the Council' where Code='8c'
update InvestmentPriorities set NameAlt=N'Investing in infrastructure for employment services' where Code='8d'
update InvestmentPriorities set NameAlt=N'Access to employment for job-seekers and inactive people, including the long-term unemployed and people far from the labour market, also through local employment initiatives and support for labour mobility' where Code='8i'
update InvestmentPriorities set NameAlt=N'Sustainable integration into the labour market of young people, in particular those not in employment, education or training, including young people at risk of social exclusion and young people from marginalised communities, including through the implementation of the Youth Guarantee' where Code='8ii'
update InvestmentPriorities set NameAlt=N'Self-employment, entrepreneurship and business creation including innovative micro, small and medium sized enterprises' where Code='8iii'
update InvestmentPriorities set NameAlt=N'Equality between men and women in all areas, including in access to employment, career progression, reconciliation of work and private life and promotion of equal pay for equal work' where Code='8iv'
update InvestmentPriorities set NameAlt=N'Adaptation of workers, enterprises and entrepreneurs to change' where Code='8v'
update InvestmentPriorities set NameAlt=N'Active and healthy ageing' where Code='8vi'
update InvestmentPriorities set NameAlt=N'Modernisation of labour market institutions, such as public and private employment services, and improving the matching of labour market needs, including through actions that enhance transnational labour mobility as well as through mobility schemes and better cooperation between institutions and relevant stakeholders' where Code='8vii'
update InvestmentPriorities set NameAlt=N'Investing in health and social infrastructure which contributes to national, regional and local development, reducing inequalities in terms of health status, promoting social inclusion through improved access to social, cultural and recreational services and the transition from institutional to community-based services' where Code='9a'
update InvestmentPriorities set NameAlt=N'Providing support for physical, economic and social regeneration of deprived communities in urban and rural areas' where Code='9b'
update InvestmentPriorities set NameAlt=N'Providing support for social enterprises' where Code='9c'
update InvestmentPriorities set NameAlt=N'Undertaking investment in the context of community- led local development strategies' where Code='9d'
update InvestmentPriorities set NameAlt=N'Active inclusion, including with a view to promoting equal opportunities and active participation, and improving employability' where Code='9i'
update InvestmentPriorities set NameAlt=N'Socio-economic integration of marginalised communities such as the Roma' where Code='9ii'
update InvestmentPriorities set NameAlt=N'Combating all forms of discrimination and promoting equal opportunities' where Code='9iii'
update InvestmentPriorities set NameAlt=N'Enhancing access to affordable, sustainable and high- quality services, including health care and social services of general interest' where Code='9iv'
update InvestmentPriorities set NameAlt=N'Promoting social entrepreneurship and vocational integration in social enterprises and the social and solidarity economy in order to facilitate access to employment' where Code='9v'
update InvestmentPriorities set NameAlt=N'Community-led local development strategies' where Code='9vi'
update InvestmentPriorities set NameAlt=N'Reducing and preventing early school-leaving and promoting equal access to good quality early-childhood, primary and secondary education including formal, non-formal and informal learning pathways for reintegrating into education and training' where Code='10i'
update InvestmentPriorities set NameAlt=N'Improving the quality and efficiency of, and access to, tertiary and equivalent education with a view to increasing participation and attainment levels, especially for disadvantaged groups' where Code='10ii'
update InvestmentPriorities set NameAlt=N'Enhancing equal access to lifelong learning for all age groups in formal, non-formal and informal settings, upgrading the knowledge, skills and competences of the workforce, and promoting flexible learning pathways including through career guidance and validation of acquired competences' where Code='10iii'
update InvestmentPriorities set NameAlt=N'Improving the labour market relevance of education and training systems, facilitating the transition from education to work, and strengthening vocational education and training systems and their quality, including through mechanisms for skills anticipation, adaptation of curricula and the establishment and development of work-based learning systems, including dual learning systems and apprenticeship schemes' where Code='10iv'
update InvestmentPriorities set NameAlt=N'Investing in education, training and vocational training for skills and lifelong learning by developing education and training infrastructure' where Code='10a'
update InvestmentPriorities set NameAlt=N'Investment in institutional capacity and in the efficiency of public administrations and public services at the national, regional and local levels with a view to reforms, better regulation and good governance' where Code='11i'
update InvestmentPriorities set NameAlt=N'Capacity building for all stakeholders delivering education, lifelong learning, training and employment and social policies, including through sectoral and territorial pacts to mobilise for reform at the national, regional and local levels' where Code='11ii'
update InvestmentPriorities set NameAlt=N'Actions to strengthen the institutional capacity and the efficiency of public administrations and public services related to the implementation of the ERDF, and in support of actions under the ESF to strengthen the institutional capacity and the efficiency of public administration' where Code='unknown2'
update InvestmentPriorities set NameAlt=N'Actions to strengthen the institutional capacity and the efficiency of public administrations and public services related to the implementation of the Cohesion Fund' where Code='unknown3'
update InvestmentPriorities set NameAlt=N'No investment priority' where Code='00'

--Update MapNodes
update MapNodes set NameAlt=N'Good governance' where MapNodeId=01
update MapNodes set NameAlt=N'Transport and transport infrastructure' where MapNodeId=02
update MapNodes set NameAlt=N'Regions in growth' where MapNodeId=03
update MapNodes set NameAlt=N'Human Resources Development' where MapNodeId=04
update MapNodes set NameAlt=N'Innovations and competitiveness' where MapNodeId=05
update MapNodes set NameAlt=N'Environment' where MapNodeId=06
update MapNodes set NameAlt=N'Science and education for smart growth' where MapNodeId=07

update MapNodes set NameAlt=N'Administrative service delivery and e-governance' where MapNodeId=0101
update MapNodes set NameAlt=N'Effective and professional governance in partnership with the civil society and the business' where MapNodeId=0102
update MapNodes set NameAlt=N'Transparent and efficient judiciary' where MapNodeId=0103
update MapNodes set NameAlt=N'Technical assistance for the management of ESIF' where MapNodeId=0104
update MapNodes set NameAlt=N'Technical assistance' where MapNodeId=0105
update MapNodes set NameAlt=N'Development of railway infrastructure along the “core” TEN-T' where MapNodeId=0201
update MapNodes set NameAlt=N'Development of road infrastructure along the „core” and “comprehensive” TEN-T' where MapNodeId=0202
update MapNodes set NameAlt=N'Improvement of intermodal transport services for passengers and freights and development of sustainable urban transport' where MapNodeId=0203
update MapNodes set NameAlt=N'Innovations in management and services - establishment of modern infrastructure for traffic management and transport safety improvement' where MapNodeId=0204
update MapNodes set NameAlt=N'Technical assistance' where MapNodeId=0205
update MapNodes set NameAlt=N'Sustainable and Integrated Urban Development' where MapNodeId=0301
update MapNodes set NameAlt=N'Support for Energy Efficiency in  support centers in peripheral areas ' where MapNodeId=0302
update MapNodes set NameAlt=N'Regional Educational Infrastructure' where MapNodeId=0303
update MapNodes set NameAlt=N'Regional Health Infrastructure' where MapNodeId=0304
update MapNodes set NameAlt=N'Regional Social Infrastructure' where MapNodeId=0305
update MapNodes set NameAlt=N'Regional Tourism' where MapNodeId=0306
update MapNodes set NameAlt=N'Regional Road Infrastructure' where MapNodeId=0307
update MapNodes set NameAlt=N'Technical assistance' where MapNodeId=0308
update MapNodes set NameAlt=N'Improving the access to employment and the quality of jobs' where MapNodeId=0401
update MapNodes set NameAlt=N'Reducing poverty and promoting social inclusion' where MapNodeId=0402
update MapNodes set NameAlt=N'Modernising the institutions in the area of social inclusion, healthcare, equal opportunities and non-discrimination and working conditions' where MapNodeId=0403
update MapNodes set NameAlt=N'Transnational cooperation' where MapNodeId=0404
update MapNodes set NameAlt=N'Technical assistance' where MapNodeId=0405
update MapNodes set NameAlt=N'Technological development and Innovation' where MapNodeId=0501
update MapNodes set NameAlt=N'Entrepreneurship and Capacity for growth of SMEs' where MapNodeId=0502
update MapNodes set NameAlt=N'Energy and Resource Efficiency' where MapNodeId=0503
update MapNodes set NameAlt=N'Removing bottlenecks in security of gas supplies' where MapNodeId=0504
update MapNodes set NameAlt=N'Technical assistance' where MapNodeId=0505
update MapNodes set NameAlt=N'Water' where MapNodeId=0601
update MapNodes set NameAlt=N'Waste' where MapNodeId=0602
update MapNodes set NameAlt=N'NATURA2000 and biodiversity' where MapNodeId=0603
update MapNodes set NameAlt=N'Flood and landslides risk prevention and management' where MapNodeId=0604
update MapNodes set NameAlt=N'Improvement of the ambient air quality' where MapNodeId=0605
update MapNodes set NameAlt=N'Technical assistance' where MapNodeId=0606
update MapNodes set NameAlt=N'Research and technological development' where MapNodeId=0701
update MapNodes set NameAlt=N'Education and lifelong learning' where MapNodeId=0702
update MapNodes set NameAlt=N'Educational environment for active social inclusion' where MapNodeId=0703
update MapNodes set NameAlt=N'Technical assistance' where MapNodeId=0704

update MapNodes set NameAlt=N'Investment in institutional capacity and in the efficiency of public administrations and public services at the national, regional and local levels with a view to reforms, better regulation and good governance' where MapNodeId=010101
update MapNodes set NameAlt=N'Investment in institutional capacity and in the efficiency of public administrations and public services at the national, regional and local levels with a view to reforms, better regulation and good governance' where MapNodeId=010201
update MapNodes set NameAlt=N'Investment in institutional capacity and in the efficiency of public administrations and public services at the national, regional and local levels with a view to reforms, better regulation and good governance' where MapNodeId=010301
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=010401
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=010501
update MapNodes set NameAlt=N'Supporting a multimodal Single European Transport Area by investing in the TEN-T' where MapNodeId=020101
update MapNodes set NameAlt=N'Supporting a multimodal Single European Transport Area by investing in the TEN-T' where MapNodeId=020201
update MapNodes set NameAlt=N'Promoting low-carbon strategies for all types of territories, in particular for urban areas, including the promotion of sustainable multimodal urban mobility and mitigation-relevant adaptation measures' where MapNodeId=020301
update MapNodes set NameAlt=N'Supporting a multimodal Single European Transport Area by investing in the TEN-T' where MapNodeId=020302
update MapNodes set NameAlt=N'Developing and improving environmentally-friendly (including low-noise) and low-carbon transport systems, including inland waterways and maritime transport, ports, multimodal links and airport infrastructure, in order to promote sustainable regional and local mobility' where MapNodeId=020401
update MapNodes set NameAlt=N'Developing and rehabilitating comprehensive, high quality and interoperable railway systems, and promoting noise-reduction measures' where MapNodeId=020402
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=020501
update MapNodes set NameAlt=N'Supporting energy efficiency, smart energy management and renewable energy use in public infrastructure, including in public buildings, and in the housing sector' where MapNodeId=030101
update MapNodes set NameAlt=N'Promoting low-carbon strategies for all types of territories, in particular for urban areas, including the promotion of sustainable multimodal urban mobility and mitigation-relevant adaptation measures' where MapNodeId=030102
update MapNodes set NameAlt=N'Taking action to improve the urban environment, to revitalise cities, regenerate and decontaminate brownfield sites (including conversion areas), reduce air pollution and promote noise-reduction measures' where MapNodeId=030103
update MapNodes set NameAlt=N'Investing in health and social infrastructure which contributes to national, regional and local development, reducing inequalities in terms of health status, promoting social inclusion through improved access to social, cultural and recreational services and the transition from institutional to community-based services' where MapNodeId=030104
update MapNodes set NameAlt=N'Investing in education, training and vocational training for skills and lifelong learning by developing education and training infrastructure' where MapNodeId=030105
update MapNodes set NameAlt=N'Supporting energy efficiency, smart energy management and renewable energy use in public infrastructure, including in public buildings, and in the housing sector' where MapNodeId=030201
update MapNodes set NameAlt=N'Investing in education, training and vocational training for skills and lifelong learning by developing education and training infrastructure' where MapNodeId=030301
update MapNodes set NameAlt=N'Investing in health and social infrastructure which contributes to national, regional and local development, reducing inequalities in terms of health status, promoting social inclusion through improved access to social, cultural and recreational services and the transition from institutional to community-based services' where MapNodeId=030401
update MapNodes set NameAlt=N'Investing in health and social infrastructure which contributes to national, regional and local development, reducing inequalities in terms of health status, promoting social inclusion through improved access to social, cultural and recreational services and the transition from institutional to community-based services' where MapNodeId=030501
update MapNodes set NameAlt=N'Conserving, protecting, promoting and developing natural and cultural heritage' where MapNodeId=030601
update MapNodes set NameAlt=N'Enhancing regional mobility by connecting secondary and tertiary nodes to TEN-T infrastructure, including multimodal nodes' where MapNodeId=030701
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=030801
update MapNodes set NameAlt=N'Access to employment for job-seekers and inactive people, including the long-term unemployed and people far from the labour market, also through local employment initiatives and support for labour mobility' where MapNodeId=040101
update MapNodes set NameAlt=N'Sustainable integration into the labour market of young people, in particular those not in employment, education or training, including young people at risk of social exclusion and young people from marginalised communities, including through the implementation of the Youth Guarantee' where MapNodeId=040102
update MapNodes set NameAlt=N'Self-employment, entrepreneurship and business creation including innovative micro, small and medium sized enterprises' where MapNodeId=040103
update MapNodes set NameAlt=N'Adaptation of workers, enterprises and entrepreneurs to change' where MapNodeId=040104
update MapNodes set NameAlt=N'Modernisation of labour market institutions, such as public and private employment services, and improving the matching of labour market needs, including through actions that enhance transnational labour mobility as well as through mobility schemes and better cooperation between institutions and relevant stakeholders' where MapNodeId=040105
update MapNodes set NameAlt=N'Enhancing equal access to lifelong learning for all age groups in formal, non-formal and informal settings, upgrading the knowledge, skills and competences of the workforce, and promoting flexible learning pathways including through career guidance and validation of acquired competences' where MapNodeId=040106
update MapNodes set NameAlt=N'Sustainable integration into the labour market of young people, in particular those not in employment, education or training, including young people at risk of social exclusion and young people from marginalised communities, including through the implementation of the Youth Guarantee' where MapNodeId=040107
update MapNodes set NameAlt=N'Active inclusion, including with a view to promoting equal opportunities and active participation, and improving employability' where MapNodeId=040201
update MapNodes set NameAlt=N'Socio-economic integration of marginalised communities such as the Roma' where MapNodeId=040202
update MapNodes set NameAlt=N'Enhancing access to affordable, sustainable and high- quality services, including health care and social services of general interest' where MapNodeId=040203
update MapNodes set NameAlt=N'Promoting social entrepreneurship and vocational integration in social enterprises and the social and solidarity economy in order to facilitate access to employment' where MapNodeId=040204
update MapNodes set NameAlt=N'Investment in institutional capacity and in the efficiency of public administrations and public services at the national, regional and local levels with a view to reforms, better regulation and good governance' where MapNodeId=040301
update MapNodes set NameAlt=N'Access to employment for job-seekers and inactive people, including the long-term unemployed and people far from the labour market, also through local employment initiatives and support for labour mobility' where MapNodeId=040401
update MapNodes set NameAlt=N'Sustainable integration into the labour market of young people, in particular those not in employment, education or training, including young people at risk of social exclusion and young people from marginalised communities, including through the implementation of the Youth Guarantee' where MapNodeId=040402
update MapNodes set NameAlt=N'Self-employment, entrepreneurship and business creation including innovative micro, small and medium sized enterprises' where MapNodeId=040403
update MapNodes set NameAlt=N'Adaptation of workers, enterprises and entrepreneurs to change' where MapNodeId=040404
update MapNodes set NameAlt=N'Modernisation of labour market institutions, such as public and private employment services, and improving the matching of labour market needs, including through actions that enhance transnational labour mobility as well as through mobility schemes and better cooperation between institutions and relevant stakeholders' where MapNodeId=040405
update MapNodes set NameAlt=N'Active inclusion, including with a view to promoting equal opportunities and active participation, and improving employability' where MapNodeId=040406
update MapNodes set NameAlt=N'Socio-economic integration of marginalised communities such as the Roma' where MapNodeId=040407
update MapNodes set NameAlt=N'Enhancing access to affordable, sustainable and high- quality services, including health care and social services of general interest' where MapNodeId=040408
update MapNodes set NameAlt=N'Promoting social entrepreneurship and vocational integration in social enterprises and the social and solidarity economy in order to facilitate access to employment' where MapNodeId=040409
update MapNodes set NameAlt=N'Enhancing equal access to lifelong learning for all age groups in formal, non-formal and informal settings, upgrading the knowledge, skills and competences of the workforce, and promoting flexible learning pathways including through career guidance and validation of acquired competences' where MapNodeId=040410
update MapNodes set NameAlt=N'Investment in institutional capacity and in the efficiency of public administrations and public services at the national, regional and local levels with a view to reforms, better regulation and good governance' where MapNodeId=040411
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=040501
update MapNodes set NameAlt=N'Promoting business investment in R&I, developing links and synergies between enterprises, research and development centres and the higher education sector, in particular promoting investment in product and service development, technology transfer, social innovation, eco-innovation, public service applications, demand stimulation, networking, clusters and open innovation through smart specialisation, and supporting technological and applied research, pilot lines, early product validation actions, advanced manufacturing capabilities and first production, in particular in key enabling technologies and diffusion of general purpose technologies' where MapNodeId=050101
update MapNodes set NameAlt=N'Promoting entrepreneurship, in particular by facilitating the economic exploitation of new ideas and fostering the creation of new firms, including through business incubators' where MapNodeId=050201
update MapNodes set NameAlt=N'Supporting the capacity of SMEs to grow in regional, national and international markets, and to engage in innovation processes' where MapNodeId=050202
update MapNodes set NameAlt=N'Promoting energy efficiency and renewable energy use in enterprises' where MapNodeId=050301
update MapNodes set NameAlt=N'Supporting industrial transition towards a resource- efficient economy, promoting green growth, eco-innovation and environmental performance management in the public and private sectors' where MapNodeId=050302
update MapNodes set NameAlt=N'Improving energy efficiency and security of supply through the development of smart energy distribution, storage and transmission systems and through the integration of distributed generation from renewable sources' where MapNodeId=050401
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=050501
update MapNodes set NameAlt=N'Investing in the water sector to meet the requirements of the Union''s environmental acquis and to address needs, identified by the Member States, for investment that goes beyond those requirements' where MapNodeId=060101
update MapNodes set NameAlt=N'Investing in the waste sector to meet the requirements of the Union''s environmental acquis and to address needs, identified by the Member States, for investment that goes beyond those requirements' where MapNodeId=060201
update MapNodes set NameAlt=N'Protecting and restoring biodiversity and soil and promoting ecosystem services, including through Natura 2000, and green infrastructure' where MapNodeId=060301
update MapNodes set NameAlt=N'Promoting investment to address specific risks, ensuring disaster resilience and developing disaster management systems' where MapNodeId=060401
update MapNodes set NameAlt=N'Taking action to improve the urban environment, to revitalise cities, regenerate and decontaminate brownfield sites (including conversion areas), reduce air pollution and promote noise-reduction measures' where MapNodeId=060501
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=060601
update MapNodes set NameAlt=N'Enhancing research and innovation (R&I) infrastructure and capacities to develop R&I excellence, and promoting centres of competence, in particular those of European interest' where MapNodeId=070101
update MapNodes set NameAlt=N'Reducing and preventing early school-leaving and promoting equal access to good quality early-childhood, primary and secondary education including formal, non-formal and informal learning pathways for reintegrating into education and training' where MapNodeId=070201
update MapNodes set NameAlt=N'Improving the quality and efficiency of, and access to, tertiary and equivalent education with a view to increasing participation and attainment levels, especially for disadvantaged groups' where MapNodeId=070202
update MapNodes set NameAlt=N'Enhancing equal access to lifelong learning for all age groups in formal, non-formal and informal settings, upgrading the knowledge, skills and competences of the workforce, and promoting flexible learning pathways including through career guidance and validation of acquired competences' where MapNodeId=070203
update MapNodes set NameAlt=N'Improving the labour market relevance of education and training systems, facilitating the transition from education to work, and strengthening vocational education and training systems and their quality, including through mechanisms for skills anticipation, adaptation of curricula and the establishment and development of work-based learning systems, including dual learning systems and apprenticeship schemes' where MapNodeId=070204
update MapNodes set NameAlt=N'Active inclusion, including with a view to promoting equal opportunities and active participation, and improving employability' where MapNodeId=070301
update MapNodes set NameAlt=N'Socio-economic integration of marginalised communities such as the Roma' where MapNodeId=070302
update MapNodes set NameAlt=N'No investment priority' where MapNodeId=070401

update MapNodes set NameAlt=N'Reduction of administrative and regulatory burden on citizens and businesss and introduction of services based on "life events" and "business events"' where MapNodeId=01010101
update MapNodes set NameAlt=N'Increase of e -services available to citizens and businesses' where MapNodeId=01010102
update MapNodes set NameAlt=N'Increase the number of the administrations, implementing organizational development mechanisms and result-based management ' where MapNodeId=01020101
update MapNodes set NameAlt=N'Improvement of the specialized knowledge and skills of the administration staff and development of career development mechanisms ' where MapNodeId=01020102
update MapNodes set NameAlt=N'Increase of citizen participation in policy-making and control ' where MapNodeId=01020103
update MapNodes set NameAlt=N'Increase of transparency and acceleration of judicial proceedings through structural, procedural and organisational reforms in the judiciary ' where MapNodeId=01030101
update MapNodes set NameAlt=N'Improve the accessibility and the accountability of the judiciary through the introduction of e-justice ' where MapNodeId=01030102
update MapNodes set NameAlt=N'Extension of the scope and improvement of the quality of training in the judicial system ' where MapNodeId=01030103
update MapNodes set NameAlt=N'Support for horizontal structures responsible for the management and implementation of ESIF' where MapNodeId=01040101
update MapNodes set NameAlt=N'Ensuring effective functioning of UMIS 2020' where MapNodeId=01040102
update MapNodes set NameAlt=N'Increase of public awareness on the ESIF opportunities and results in Bulgaria, and improvement of beneficiary capacity' where MapNodeId=01040103
update MapNodes set NameAlt=N'Effective and efficient management of OPGG' where MapNodeId=01050101
update MapNodes set NameAlt=N'Increase capacity and awareness of OP beneficiaries' where MapNodeId=01050102
update MapNodes set NameAlt=N'Increasing railway traffic of passenger and freight through improving the quality of the TEN-T railway infrastructure' where MapNodeId=02010101
update MapNodes set NameAlt=N'Removal of bottlenecks in the TEN-T road network' where MapNodeId=02020101
update MapNodes set NameAlt=N'Increased use of metro' where MapNodeId=02030101
update MapNodes set NameAlt=N'Increased intermodal transport' where MapNodeId=02030201
update MapNodes set NameAlt=N'Improved transport management through introduction of innovative systems' where MapNodeId=02040101
update MapNodes set NameAlt=N'Improved management of the railway network' where MapNodeId=02040201
update MapNodes set NameAlt=N'Establishment of necessary conditions for successful completion of OPT 2007-2013 and implementation of OPTTI 2014-2020, strengthening the administrative capacity and public awareness of OPTTI' where MapNodeId=02050101
update MapNodes set NameAlt=N'Raising energy efficiency in housing sector' where MapNodeId=03010101
update MapNodes set NameAlt=N'Raising energy efficiency in public buildings' where MapNodeId=03010102
update MapNodes set NameAlt=N'Development of ecological and sustainable urban transport' where MapNodeId=03010201
update MapNodes set NameAlt=N'Improving the quality of the urban environment' where MapNodeId=03010301
update MapNodes set NameAlt=N'Improving investment economic activity in the cities through regeneration of areas with potential for economic development' where MapNodeId=03010302
update MapNodes set NameAlt=N'Improving the housing conditions for marginalised groups of the population including the roma' where MapNodeId=03010401
update MapNodes set NameAlt=N'Improving conditions for modern social services ' where MapNodeId=03010402
update MapNodes set NameAlt=N'Improving the access for sports for all and cultural services in cities ' where MapNodeId=03010403
update MapNodes set NameAlt=N'Improving the  conditions for modern education services' where MapNodeId=03010501
update MapNodes set NameAlt=N'Raising energy efficiency in the housing sector in the support centres of 4th level of the national polycentric system' where MapNodeId=03020101
update MapNodes set NameAlt=N'Raising energy efficiency in public buildingsin the support centres of 4th level of the national polycentric system' where MapNodeId=03020102
update MapNodes set NameAlt=N'Improving conditions for modern educational services' where MapNodeId=03030101
update MapNodes set NameAlt=N'Increased access to emergency medical care with the possibility of emergency diagnosis, treatment and monitoring within 24 hours.' where MapNodeId=03040101
update MapNodes set NameAlt=N'Reducing the hospitalisations in the health system' where MapNodeId=03040102
update MapNodes set NameAlt=N'Improving regional social infrastructure for Deinstitutionalization of social services for children and elderly' where MapNodeId=03050101
update MapNodes set NameAlt=N'Increasing the tourist frequentation of cultural sites of national and world importance' where MapNodeId=03060101
update MapNodes set NameAlt=N'Improving connectivity and accessibility with the TEN-T network for freights and passengres' where MapNodeId=03070101
update MapNodes set NameAlt=N'Strengthening the effectiveness of the Managing Authority' where MapNodeId=03080101
update MapNodes set NameAlt=N'Improvement of the administrative capacity of OPRG beneficiaries 2014-2020' where MapNodeId=03080102
update MapNodes set NameAlt=N'Raising OPRG public awareness' where MapNodeId=03080103
update MapNodes set NameAlt=N'Increasing the number of unemployed or inactive persons aged 30-54 in employment' where MapNodeId=04010101
update MapNodes set NameAlt=N'Increasing the number of unemployed or inactive persons  aged 30-54 years with low education in employment ' where MapNodeId=04010102
update MapNodes set NameAlt=N'Increasing the number of unemployed or inactive persons above 54 years in employment' where MapNodeId=04010103
update MapNodes set NameAlt=N'Increasing the number of inactive young people not in education and training up to 29 years of age, who receive an offer for training, employment, apprenticeship or traineeship or for continuing their education' where MapNodeId=04010201
update MapNodes set NameAlt=N'Increasing the number of unemployed young people not in education or training up to 29 years of age with primary or lower secondary education, who are engaged in training or are in employment' where MapNodeId=04010202
update MapNodes set NameAlt=N'Increasing the number of unemployed young people not in education or training up to 29 years of age with upper secondary or higher education who are in employment, self-employment or in training' where MapNodeId=04010203
update MapNodes set NameAlt=N'Increasing the number of unemployed, inactive and employed persons who are involved in self-employment' where MapNodeId=04010301
update MapNodes set NameAlt=N'Increasing the number of the employees affected by the introduced new human resources development systems, practices and tools and the improved organisation and working conditions in the enterprises' where MapNodeId=04010401
update MapNodes set NameAlt=N'Increasing the number of the introduced  new or updated processes and models for plannig, implementation, monitoring, control and evaluation of the labour market policies and services' where MapNodeId=04010501
update MapNodes set NameAlt=N'Increasing the number and quality of the services, provided by the institutions and organizations on the labour market to the job-seekers and employers' where MapNodeId=04010502
update MapNodes set NameAlt=N'Increasing the number of the job seekers and employers to whom intermediary services have been provided in order to promote the transnational mobility within the EURES network' where MapNodeId=04010503
update MapNodes set NameAlt=N'Reforming the EURES network at national level by including new partners and creation of new data exchange systems and mechanisms for coordinated planning, implementation, monitoring, evaluation and control of EURES activities' where MapNodeId=04010504
update MapNodes set NameAlt=N'Increasing the number of employed persons above 54 years of age with acquired and /or improved professional qualification and/or key competences ' where MapNodeId=04010601
update MapNodes set NameAlt=N'Increasing the number of employed persons with upper secondary or lower level of education, who have acquired new knowledge and skills ' where MapNodeId=04010602
update MapNodes set NameAlt=N'Increasing the number of persons employed in knowledge-based sectors, high technology and ICT, green economy, „white" sector and personal services sector, processing industry with higher added value from labour, creative and cultural sectors, who have improved their knowledge and skills with the support of HRD OP' where MapNodeId=04010603
update MapNodes set NameAlt=N'Increasing the number of inactive young people not in education and training up to  29 years of age, who receive an offer for training, employment, apprenticeship or traineeship or for continuing their education' where MapNodeId=04010701
update MapNodes set NameAlt=N'Increasing the number of unemployed young people not in education or training up to 29 years of age with primary or lower secondary education, who are engaged in training or are in employment' where MapNodeId=04010702
update MapNodes set NameAlt=N'Increasing the number of unemployed young people not in education or training up to 29 years of age with upper secondary or higher education who are in employment, self-employment or in training' where MapNodeId=04010703
update MapNodes set NameAlt=N'Increasing the number of family members with children (including children with disabilities) who have started searching for a job or are  in employment as a result of integrated measures for social inclusion' where MapNodeId=04020101
update MapNodes set NameAlt=N'Increasing the number of people with disabilities who have started to search for a job or who are in employment by providing social and health services, including through integrated measures for people with disabilities and their families' where MapNodeId=04020102
update MapNodes set NameAlt=N'Increasing the number of people from vulnerable ethnic communities in employment, education, training, healthcare and social services,  with a focus on the Roma, migrants and participants with a foreign background' where MapNodeId=04020201
update MapNodes set NameAlt=N'Improving the access of people with disabilities and people over 65 years of age, unable to take care of themselves, to social inclusion and healthcare services' where MapNodeId=04020301
update MapNodes set NameAlt=N'Reducing the number of elderly people and people with disabilities placed in institutions by providing community-based social and health services, including long term care services' where MapNodeId=04020302
update MapNodes set NameAlt=N'Reducing the number of children and youth, placed in institutions by providing community-based social and health services' where MapNodeId=04020303
update MapNodes set NameAlt=N'Increasing the number of persons employed in social enterprises following support provided' where MapNodeId=04020401
update MapNodes set NameAlt=N'Increasing the knowledge, skills and competencies of the employees in the administrations in the field of social inclusion, healthcare, equal opportunities and  non-discrimination and working  conditions' where MapNodeId=04030101
update MapNodes set NameAlt=N'Introduction of new processes with the aim to improve the processes of planning, implementation, monitoring, evaluation and control at the institutions operating in the field of social inclusion, healthcare, equal opportunities and non-discrimination and working conditions ' where MapNodeId=04030102
update MapNodes set NameAlt=N'Promoting transnational cooperation in respect of exchange of experience, good practices and models of promoting employment for job-seekers and inactive people over 29 years of age' where MapNodeId=04040101
update MapNodes set NameAlt=N'Promoting transnational cooperation in respect of exchange of experience, good practices and models of sustainable integration of young people under 29 years of age, incl. on the labour market' where MapNodeId=04040201
update MapNodes set NameAlt=N'Promoting transnational cooperation in respect of exchange of experience, good practices and models for promotion of self-employment, enterpreneurship and creation of enterprises, including innovative micro, small and medium enterprises' where MapNodeId=04040301
update MapNodes set NameAlt=N'Promoting transnational cooperation in respect of the exchange of experience, good practices and models for increasing the number of the covered employees in the enterprises, which have introduced new human resources development systems, practices and tools, and improving the organisation and working conditions' where MapNodeId=04040401
update MapNodes set NameAlt=N'Promoting transnational cooperation aimed at the improvement of the processes for policy formulation and implementation and for active labour market measures' where MapNodeId=04040501
update MapNodes set NameAlt=N'Promoting transnational cooperation in exchange of experience, good practices and models for ensuring active social inclusion and promoting equal opportunities for the vulnerable groups' where MapNodeId=04040601
update MapNodes set NameAlt=N'Promoting transnational cooperation in exchange of experience, good practices and models for ensuring socio-economic integration of the marginalised communities' where MapNodeId=04040701
update MapNodes set NameAlt=N'Promoting transnational cooperation in respect of exchange of experience, good practices and models of long-term care for people incapable of self-care and the disabled, reduction of the number of children, youth and elderly people placed in institutions' where MapNodeId=04040801
update MapNodes set NameAlt=N'Promoting transnational cooperation in respect of exchange of experience, good practices and models for increasing the number of persons employed in social enterprises following the support provided' where MapNodeId=04040901
update MapNodes set NameAlt=N'Promoting transnational cooperation in exchange of experience, good practices and models for ensuring equal access of employed persons to lifelong learning' where MapNodeId=04041001
update MapNodes set NameAlt=N'Promoting transnational cooperation in respect of exchange of experience, good practices and models of increasing knowledge, skills and competences of the employees in the institutions in the area of social inclusion, healthcare, equal opportunities and non-discrimination and working conditions' where MapNodeId=04041101
update MapNodes set NameAlt=N'Improving the HRD OP planning, implementation, monitoring,  evaluation and control  processes' where MapNodeId=04050101
update MapNodes set NameAlt=N'Strengthening the project management capacity of the beneficiaries under HRD OP' where MapNodeId=04050102
update MapNodes set NameAlt=N'Increased innovation activity of enterprises' where MapNodeId=05010101
update MapNodes set NameAlt=N'Improving the survival rate of SMEs including through stimulating entrepreneurship' where MapNodeId=05020101
update MapNodes set NameAlt=N'Strengthening productivity and export potential of Bulgarian SMEs' where MapNodeId=05020201
update MapNodes set NameAlt=N'Reducing energy intensity of the economy' where MapNodeId=05030101
update MapNodes set NameAlt=N'Increased share of SMEs with measures of resource efficiency' where MapNodeId=05030201
update MapNodes set NameAlt=N'Better interconnectivity with neighbouring gas transmission systems' where MapNodeId=05040101
update MapNodes set NameAlt=N'Support for effective and efficient implementation of actions related to the programming, management, monitoring, evaluation and control of OPIC in accordance with the current legislation and existing best practices' where MapNodeId=05050101
update MapNodes set NameAlt=N'Supporting the Managing Authority for provision of adequate and timely information and raising the public awareness of funding opportunities under OPIC and the criteria, rules and procedures for participation in its implementation' where MapNodeId=05050102
update MapNodes set NameAlt=N'Strengthening the capacity of the beneficiaries of OPIC for participation in the programme implementation and better (quantitative and qualitative) use of its financial resources' where MapNodeId=05050103
update MapNodes set NameAlt=N'Protection and improvement of the water resources status' where MapNodeId=06010101
update MapNodes set NameAlt=N'Improvement of the water bodies status assessment' where MapNodeId=06010102
update MapNodes set NameAlt=N'Reducing the amount of waste going to landfills' where MapNodeId=06020101
update MapNodes set NameAlt=N'Improving the conservation status of species and habitats within Natura 2000 network' where MapNodeId=06030101
update MapNodes set NameAlt=N'Increasing the flood protection and the preparedness of the population for an adequate response to floods' where MapNodeId=06040101
update MapNodes set NameAlt=N'Increasing the protection of the population from landslides' where MapNodeId=06040102
update MapNodes set NameAlt=N'Reducing ambient air pollution by lowering the quantities of PM10 and NOx' where MapNodeId=06050101
update MapNodes set NameAlt=N'Strengthening the administrative capacity of the responsible structures for the effective and efficient implementation of activities related to programming, management, monitoring, evaluation and control of OPE 2014-2020' where MapNodeId=06060101
update MapNodes set NameAlt=N'Raising the public awareness about the programme and the ESIF contribution, and ensuring publicity and relevant information for all identified target groups' where MapNodeId=06060102
update MapNodes set NameAlt=N'Strengthening the capacity of OPE beneficiaries for the successful implementation of projects under the programme' where MapNodeId=06060103
update MapNodes set NameAlt=N'Enhancing excellent and market-oriented research' where MapNodeId=07010101
update MapNodes set NameAlt=N'Improving territorial and thematic distribution of research infrastructure, with a view to regional smart specialisation' where MapNodeId=07010102
update MapNodes set NameAlt=N'Increasing the participation of Bulgarian researchers in international cooperation' where MapNodeId=07010103
update MapNodes set NameAlt=N'Improving children’s and students’ performance in acquiring key competences' where MapNodeId=07020101
update MapNodes set NameAlt=N'Reducing the number of early school leavers and sustainable keeping of students in the education system' where MapNodeId=07020102
update MapNodes set NameAlt=N'Implementation of a system to monitor the number of graduates who are not included in subsequent training programs and have started work in the first year after graduation' where MapNodeId=07020201
update MapNodes set NameAlt=N'Introduction of management systems and financing of higher education institutions, according to the results achieved' where MapNodeId=07020202
update MapNodes set NameAlt=N'Increasing the number of higher education graduates among 30-34 year-olds' where MapNodeId=07020203
update MapNodes set NameAlt=N'Increasing the qualification of research staff and participation of young doctorates in research' where MapNodeId=07020204
update MapNodes set NameAlt=N'Developing the capacity and raising the qualification of the employed in the field of education' where MapNodeId=07020301
update MapNodes set NameAlt=N'Increasing the participation in continued learning and upgrading knowledge, skills and competences of the persons covered under the OP actions' where MapNodeId=07020302
update MapNodes set NameAlt=N'Increasing the number of students in VET schools and adapting of vocational education and training to labour market needs' where MapNodeId=07020401
update MapNodes set NameAlt=N'Increasing the share of graduates in vocational or higher education who have started work in the first year after graduation, in their area of study' where MapNodeId=07020402
update MapNodes set NameAlt=N'Increasing the number of educational institutions having ensured supportive environment for inclusive education' where MapNodeId=07030101
update MapNodes set NameAlt=N'Increasing the number of successfully integrated through the educational system children and students from marginalized communities, including Roma' where MapNodeId=07030201
update MapNodes set NameAlt=N'Strengthening and reinforcing the administrative capacity of the Managing Authority and the beneficiaries of the Operational Programme' where MapNodeId=07040101


--Update Indicators

update Indicators set NameAlt=N'Permissible maximum speed' where ProgrammeId=2 and Code=N'1.1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Passenger transport performance' where ProgrammeId=2 and Code=N'1.2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Freight transport performance' where ProgrammeId=2 and Code=N'1.3' and Type=1 and Kind=3
update Indicators set NameAlt=N'Saturation ratio of road infrastructure along the Struma Motorway' where ProgrammeId=2 and Code=N'2.1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Trips by metro' where ProgrammeId=2 and Code=N'8' and Type=1 and Kind=3
update Indicators set NameAlt=N'Intermodal transport units (ITU), carried by railway, inland waterway or sea' where ProgrammeId=2 and Code=N'6' and Type=1 and Kind=3
update Indicators set NameAlt=N'Navigation period along the Bulgarian section of the Danube River' where ProgrammeId=2 and Code=N'1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Ship-generated waste and cargo residues treated in the ports' where ProgrammeId=2 and Code=N'2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Capacity of airports' where ProgrammeId=2 and Code=N'9' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of TEN-T railway network covered with GSM-R' where ProgrammeId=2 and Code=N'12' and Type=1 and Kind=3
update Indicators set NameAlt=N'Final energy consumption from households' where ProgrammeId=3 and Code=N'111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Final energy consumption from public administration, commerce and services' where ProgrammeId=3 and Code=N'112' and Type=1 and Kind=3
update Indicators set NameAlt=N'Public urban transport share' where ProgrammeId=3 and Code=N'121' and Type=1 and Kind=3
update Indicators set NameAlt=N'Quantity of fine particles in cities' where ProgrammeId=3 and Code=N'122' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of population benefitting from an improved urban environment' where ProgrammeId=3 and Code=N'131' and Type=1 and Kind=3
update Indicators set NameAlt=N'Quantity of fine particles in cities' where ProgrammeId=3 and Code=N'132' and Type=1 and Kind=3
update Indicators set NameAlt=N'Expenditures on acquisition of tangible fixed assets' where ProgrammeId=3 and Code=N'133' and Type=1 and Kind=3
update Indicators set NameAlt=N'Representatives from marginalised groups, including roma, with improved housing conditions' where ProgrammeId=3 and Code=N'141' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of modernized facilities for social services' where ProgrammeId=3 and Code=N'142' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of modernized cultural/ sport sites' where ProgrammeId=3 and Code=N'143' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of modernized cultural/ sport sites' where ProgrammeId=3 and Code=N'151' and Type=1 and Kind=3
update Indicators set NameAlt=N'Final energy consumption from households in peripheral ares' where ProgrammeId=3 and Code=N'211' and Type=1 and Kind=3
update Indicators set NameAlt=N'Final energy consumption from public administration, commerce and services in peripheral areas' where ProgrammeId=3 and Code=N'212' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of modernized educational institutions' where ProgrammeId=3 and Code=N'311' and Type=1 and Kind=3
update Indicators set NameAlt=N'Population with 30 minute access to emergency medical care and treatment and observation for 24 hours' where ProgrammeId=3 and Code=N'411' and Type=1 and Kind=3
update Indicators set NameAlt=N'Брой хоспитализации годишно' where ProgrammeId=3 and Code=N'412' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of the social infrastructure for deinstitutionalization of social services for children and elederly people' where ProgrammeId=3 and Code=N'511' and Type=1 and Kind=3
update Indicators set NameAlt=N'Internal tourism consumption' where ProgrammeId=3 and Code=N'611' and Type=1 and Kind=3
update Indicators set NameAlt=N'Passenger flow' where ProgrammeId=3 and Code=N'711' and Type=1 and Kind=3
update Indicators set NameAlt=N'Freight flow' where ProgrammeId=3 and Code=N'712' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of innovative enterprises' where ProgrammeId=5 and Code=N'SR1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Non-R&D innovation expenditure' where ProgrammeId=5 and Code=N'SR2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of enterprise survivals up to 2 years' where ProgrammeId=5 and Code=N'SR03' and Type=1 and Kind=3
update Indicators set NameAlt=N'Export volume of goods and services achieved by SMEs' where ProgrammeId=5 and Code=N'SR04' and Type=1 and Kind=3
update Indicators set NameAlt=N'Productivity of SMEs' where ProgrammeId=5 and Code=N'SR05' and Type=1 and Kind=3
update Indicators set NameAlt=N'Energy intensity of the economy' where ProgrammeId=5 and Code=N'SR06' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of SMEs with measures of resource efficiency' where ProgrammeId=5 and Code=N'SR07' and Type=1 and Kind=3
update Indicators set NameAlt=N'N – 1 standard for infrastructure' where ProgrammeId=5 and Code=N'SR08' and Type=1 and Kind=3
update Indicators set NameAlt=N'Amount of pollution load that receives full collection and treatment in compliance with the legislation' where ProgrammeId=6 and Code=N'1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Water bodies with improved monitoring of the quantitative status' where ProgrammeId=6 and Code=N'2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Water bodies with improved monitoring of the chemical status' where ProgrammeId=6 and Code=N'3' and Type=1 and Kind=3
update Indicators set NameAlt=N'Amount of waste going to landfills' where ProgrammeId=6 and Code=N'7' and Type=1 and Kind=3
update Indicators set NameAlt=N'Habitats with improved conservation status' where ProgrammeId=6 and Code=N'10' and Type=1 and Kind=3
update Indicators set NameAlt=N'Species with improved conservation status' where ProgrammeId=6 and Code=N'8' and Type=1 and Kind=3
update Indicators set NameAlt=N'Birds with improved status' where ProgrammeId=6 and Code=N'9' and Type=1 and Kind=3
update Indicators set NameAlt=N'Areas  with significant potential flood risk which population has no preparedness for an adequate response to floods' where ProgrammeId=6 and Code=N'13' and Type=1 and Kind=3
update Indicators set NameAlt=N'Population at risk of landslides' where ProgrammeId=6 and Code=N'16' and Type=1 and Kind=3
update Indicators set NameAlt=N'Quantity of PM' where ProgrammeId=6 and Code=N'17' and Type=1 and Kind=3
update Indicators set NameAlt=N'Quantity of NOx' where ProgrammeId=6 and Code=N'20' and Type=1 and Kind=3
update Indicators set NameAlt=N'Scientific publications among top 10 most cited in RIS3 priority areas' where ProgrammeId=7 and Code=N'Р111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Public expenditure on R&D (GOVERD plus HERD) financed by business enterprises as % of GDP' where ProgrammeId=7 and Code=N'Р112' and Type=1 and Kind=3
update Indicators set NameAlt=N'Public expenditure on R&D (GOVERD plus HERD) financed by business enterprises in all regions Outside BG411 Sofia (stolitsa)* as % of total public expenditure on R&D (GOVERD plus HERD)' where ProgrammeId=7 and Code=N'Р121' and Type=1 and Kind=3
update Indicators set NameAlt=N'International scientific co-publications per million population' where ProgrammeId=7 and Code=N'Р131' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of implemented "life events" and "business events" services' where ProgrammeId=1 and Code=N'R1-1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of standardised municipal services, introduced in all municipal administrations' where ProgrammeId=1 and Code=N'R1-2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of supported priority e-services, including inter-service, at transaction- and / or payment level, based on the government HPC, used over 5000 times a year' where ProgrammeId=1 and Code=N'R1-3' and Type=1 and Kind=3
update Indicators set NameAlt=N'Functioning e-procurement system' where ProgrammeId=1 and Code=N'R1-4' and Type=1 and Kind=3
update Indicators set NameAlt=N'Functioning NHIS' where ProgrammeId=1 and Code=N'R1-5' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of administrations supported for implementation of organizational development mechanisms and result-based management' where ProgrammeId=1 and Code=N'R2-1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Employees that have successfully completed the trainings upon receipt of a certificate' where ProgrammeId=1 and Code=N'R2-2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of administrations supported for the introduction of career management mechanisms' where ProgrammeId=1 and Code=N'R2-3' and Type=1 and Kind=3
update Indicators set NameAlt=N'Recommendations made by NGOs and NGO networks in the policy-making, implementation and monitoring process' where ProgrammeId=1 and Code=N'R2-4' and Type=1 and Kind=3
update Indicators set NameAlt=N'Introduced new and improvement of existing tools for modernization of the judiciary' where ProgrammeId=1 and Code=N'R3-1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of cases led electronically' where ProgrammeId=1 and Code=N'R3-2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Magistrates, court staff and employees of the investigating authorities under CPC who successfully completed training with a certificate' where ProgrammeId=1 and Code=N'R3-3' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants between 30 and 54 gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'Р1111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants between 30 and 54 in employment, including self-employment upon leaving' where ProgrammeId=4 and Code=N'Р1112' and Type=1 and Kind=3
update Indicators set NameAlt=N'Roma participants between 30 and 54 engaged in job searching, education/ training, gaining a qualification, in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1121' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants between 30 and 54 with primary or lower secondary education gaining a qualification' where ProgrammeId=4 and Code=N'Р1113' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants between 30 and 54 with primary or lower secondary education in employment, including self-employment upon leaving' where ProgrammeId=4 and Code=N'Р1122' and Type=1 and Kind=3
update Indicators set NameAlt=N'Roma participants between 30 and 54  with primary or lower secondary edication engaged in job searching, education/ training, gaining a qualification, in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1123' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants above 54 gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'Р1131' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants above 54 in employment including self-employment upon leaving' where ProgrammeId=4 and Code=N'Р1132' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants up to 29 years of age engaged in job searching upon leaving' where ProgrammeId=4 and Code=N'Р1311' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants up to 29 years of age gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'Р1312' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants up to 29 years of age in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1313' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants up to 29 years of age with primary or lower secondary education gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'Р1321' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants up to 29 years of age with primary or lower secondary education in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1322' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed Roma participants up to 29 years of age with primary or lower secondary education in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1323' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants up to 29 years of age with upper secondary or higher education gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'Р1331' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants up to 29 years of age with upper secondary or higher education in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1332' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants in self-employment upon leaving' where ProgrammeId=4 and Code=N'Р1511' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants from supported enterprises with an improved labour market situation six months after leaving the operation' where ProgrammeId=4 and Code=N'CR07' and Type=2 and Kind=3
update Indicators set NameAlt=N'Number of enterprises which have introduced new processes for health and safety at work' where ProgrammeId=4 and Code=N'Р1711' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of enterprises which have introduced new systems, practices and tools for human resources development and organisation of labour processes' where ProgrammeId=4 and Code=N'Р1712' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants of the supported enterprises in employment  six months after leaving the operation' where ProgrammeId=4 and Code=N'Р1713' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of introduced new or updated processes and models of planning and implementation,of the policies and services on the labour market' where ProgrammeId=4 and Code=N'Р1411' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of introduced new or updated processes and models of monitoring, evaluation and control of the policies and services on the labour market' where ProgrammeId=4 and Code=N'Р1412' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of participants (staff), employed in institutions on the labour market, gaining a qualification' where ProgrammeId=4 and Code=N'Р1413' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of the introduced new services on the labour market' where ProgrammeId=4 and Code=N'Р1421' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of participants (staff), employed in institutions on the labour market, gaining a qualification' where ProgrammeId=4 and Code=N'Р1422' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of CVs of job-seekers, provided to employers in EURES job placement process' where ProgrammeId=4 and Code=N'Р1431' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of EURES partners with introduced systems for data exchange regarding employment opportunities' where ProgrammeId=4 and Code=N'Р1441' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of the organisations within EURES network  with introduced mechanisms for coordinated planning, implementation, monitoring, evaluation and  control of the EURES activities' where ProgrammeId=4 and Code=N'Р1442' and Type=1 and Kind=3
update Indicators set NameAlt=N'Employed participants above 54 gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'Р1611' and Type=1 and Kind=3
update Indicators set NameAlt=N'Employed participants with secondary or lower level of education (below ISCED 4) gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'Р1621' and Type=1 and Kind=3
update Indicators set NameAlt=N'Employed participants in priority sectors, gaining a qualification upon leaving' where ProgrammeId=4 and Code=N'И1631' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive and unemployed participants who are engaged in job searching or  are in employment, including self-employment, after provision  a child care service' where ProgrammeId=4 and Code=N'Р2211' and Type=1 and Kind=3
update Indicators set NameAlt=N'Children, including children with disabilities, receiving social and health services' where ProgrammeId=4 and Code=N'Р2212' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants with disabilities above 18 years of age who are engaged in job searching or are in employment, including self-employment' where ProgrammeId=4 and Code=N'Р2221' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants with disabilities above 18 years of age receiving  services' where ProgrammeId=4 and Code=N'Р2222' and Type=1 and Kind=3
update Indicators set NameAlt=N'Disadvantaged participants who are engaged in job searching,  education/ training,  gaining a qualification or are in employmentq incl. self-employment or are  receiving  social and health services, upon leaving' where ProgrammeId=4 and Code=N'Р2111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Roma participants who are  engaged in job searching education/ training,  gaining a qualification,  or are  in employment, incl.self-employment  or are receiving social and health services, upon leaving' where ProgrammeId=4 and Code=N'Р2112' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants with disabilities and participants over 65, unable to take care of themselves, with improved access to services' where ProgrammeId=4 and Code=N'Р2311' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of providers of services for social inclusion with extended coverage of their activity.' where ProgrammeId=4 and Code=N'Р2312' and Type=1 and Kind=3
update Indicators set NameAlt=N'Participants over 18,   receiving community-based social and health services after leaving the institution' where ProgrammeId=4 and Code=N'Р2321' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of providers of services for social inclusion with extended coverage of their activity' where ProgrammeId=4 and Code=N'Р2322' and Type=1 and Kind=3
update Indicators set NameAlt=N'Children and youth receiving community-based social and health services after leaving the institution' where ProgrammeId=4 and Code=N'Р2331' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of providers of services for social inclusion with extended coverage of their activity.' where ProgrammeId=4 and Code=N'Р2332' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of supported cooperative enterprises or enterprises of the  social economy operating  6 months upon  leaving' where ProgrammeId=4 and Code=N'Р2411' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive and unemployed participants in employment upon leaving' where ProgrammeId=4 and Code=N'Р2412' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive or unemployed Roma participants in employment upon leaving' where ProgrammeId=4 and Code=N'Р2413' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of employed participants, gained qualification upon leaving' where ProgrammeId=4 and Code=N'Р3111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of newly introduced and/or updated processes and models of planning and implementation of policies and services' where ProgrammeId=4 and Code=N'Р3121' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of newly introduced and/or updated processes and models of monitoring, evaluation  and control of policies and services' where ProgrammeId=4 and Code=N'Р3122' and Type=1 and Kind=3
update Indicators set NameAlt=N'Transferred innovative practices' where ProgrammeId=4 and Code=N'Р4111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of pedagogical specialists among those involved in actions under the OP having acquired additional qualification for application of modern evaluation methods' where ProgrammeId=7 and Code=N'P2112' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of schools and kindergartens involved in actions under the OP having introduced innovative teaching methods developed by the programme using modern ICT' where ProgrammeId=7 and Code=N'Р2113' and Type=1 and Kind=3
update Indicators set NameAlt=N'Decrease in the share of early school leavers (ESL) among those involved in actions under the OP (persons aged 18-24)' where ProgrammeId=7 and Code=N'Р2121' and Type=1 and Kind=3
update Indicators set NameAlt=N'Group net enrolment ratio for the different stages of education - Primary stage' where ProgrammeId=7 and Code=N'Р2122' and Type=1 and Kind=3
update Indicators set NameAlt=N'Group net enrolment ratio for the different stages of education - Lower secondary stage' where ProgrammeId=7 and Code=N'Р2123' and Type=1 and Kind=3
update Indicators set NameAlt=N'Group net enrolment ratio for the different stages of education - Secondary stage' where ProgrammeId=7 and Code=N'Р2124' and Type=1 and Kind=3
update Indicators set NameAlt=N'Group net enrolment ratio for the different stages of education - Vocational training' where ProgrammeId=7 and Code=N'Р2125' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of schools offering activities to increase the motivation to learn through development of specific knowledge, skills and competences' where ProgrammeId=7 and Code=N'Р2126' and Type=1 and Kind=3
update Indicators set NameAlt=N'Introduced system for monitoring the number of higher education graduates not involved in subsequent training programmes and having started work in the first year after graduation' where ProgrammeId=7 and Code=N'Р2211' and Type=1 and Kind=3
update Indicators set NameAlt=N'Education support budget in the higher education institutions, which is calculated on the basis of education quality evaluation and relevance to the labour market needs, as a result of the OP interventions' where ProgrammeId=7 and Code=N'Р2221' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of 30-34 year-olds having completed higher education among those involved in actions under the OP' where ProgrammeId=7 and Code=N'Р2231' and Type=1 and Kind=3
update Indicators set NameAlt=N'Newly recruited researchers from abroad in research organisations supported under the OP' where ProgrammeId=7 and Code=N'Р2241' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of young researchers aged up to 34 years (inclusive) involved in actions under the OP among those employed in R&D (GOVERD plus HERD)' where ProgrammeId=7 and Code=N'Р2242' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of lecturers in schools of higher education among those involved in actions under the OP, received certificate for successfully completed programme for raising the qualification' where ProgrammeId=7 and Code=N'Р2243' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of pedagogical specialists aged up to 34 years (inclusive), who have successfully passed qualification courses under the OP and have remained in the education system' where ProgrammeId=7 and Code=N'Р2311' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of school students, out of those included in career guidance actions under the OP, who have received individual career guidance consultation' where ProgrammeId=7 and Code=N'Р2321' and Type=1 and Kind=3
update Indicators set NameAlt=N'Persons among those involved in actions under the OP having validated knowledge, skills and competences' where ProgrammeId=7 and Code=N'Р2322' and Type=1 and Kind=3
update Indicators set NameAlt=N'Schools of higher education participating in a common information career centre network as a result of the actions under OP' where ProgrammeId=7 and Code=N'Р2323' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of students in the secondary stage of education studying in vocational schools from the involved in actions under OP' where ProgrammeId=7 and Code=N'Р2411' and Type=1 and Kind=3
update Indicators set NameAlt=N'Students in technical specialties from the involved in actions under OP' where ProgrammeId=7 and Code=N'Р2421' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of students having passed successfully practical training in a real work environment from the involved in actions under OP' where ProgrammeId=7 and Code=N'Р2422' and Type=1 and Kind=3
update Indicators set NameAlt=N'Children aged between 3-6 who have received early prevention services which aim to prevent educational difficulties' where ProgrammeId=7 and Code=N'Р3111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Kindergartens / united institutions for childcare, who have provided a supportive environment for early prevention of learning difficulties' where ProgrammeId=7 and Code=N'Р3112' and Type=1 and Kind=3
update Indicators set NameAlt=N'Children, students and youths from ethnic minorities (including Roma) integrated in the education system' where ProgrammeId=7 and Code=N'Р3211' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of pedagogical specialists among those involved in actions under the OP qualified to work in multicultural environment' where ProgrammeId=7 and Code=N'Р3212' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of persons (including Roma), who have received certificates for successfully completed literacy courses or courses for mastering the learning content intended for the lower secondary stage of basic education under the OP' where ProgrammeId=7 and Code=N'Р3213' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'CR01' and Type=2 and Kind=3
update Indicators set NameAlt=N'Unemployed participants who receive an offer of employment, continued education, apprenticeship or traineeship upon leaving' where ProgrammeId=4 and Code=N'CR02' and Type=2 and Kind=3
update Indicators set NameAlt=N'Unemployed participants who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'CR03' and Type=2 and Kind=3
update Indicators set NameAlt=N'Long-term unemployed participants who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'CR04' and Type=2 and Kind=3
update Indicators set NameAlt=N'Long-term unemployed participants who receive an offer of employment, continued education, apprenticeship or traineeship upon leaving' where ProgrammeId=4 and Code=N'CR05' and Type=2 and Kind=3
update Indicators set NameAlt=N'Long-term unemployed participants who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'CR06' and Type=2 and Kind=3
update Indicators set NameAlt=N'Inactive participants not in education or training who receive an offer of employment, continued education, apprenticeship or traineeship upon leaving' where ProgrammeId=4 and Code=N'CR08' and Type=2 and Kind=3
update Indicators set NameAlt=N'Inactive participants not in education or training who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'CR09' and Type=2 and Kind=3
update Indicators set NameAlt=N'Participants in continued education, training programmes leading to a qualification, an apprenticeship or a traineeship six months after leaving' where ProgrammeId=4 and Code=N'CR10' and Type=2 and Kind=3
update Indicators set NameAlt=N'Participants in employment, six months after leaving the operation' where ProgrammeId=4 and Code=N'CR11' and Type=2 and Kind=3
update Indicators set NameAlt=N'Participants in self-employment six months after leaving' where ProgrammeId=4 and Code=N'CR12' and Type=2 and Kind=3
update Indicators set NameAlt=N'Inactive participants between 15 and 24 not in education or training who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1211' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants between 25 and 29 not in education or training who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1212' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants between 15 and 24 not in education or training who receive an offer of employment, continued education, apprenticeship or traineeship upon leaving' where ProgrammeId=4 and Code=N'Р1213' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants between 25 and 29 not in education or training who receive an offer of employment, continued education, apprenticeship or traineeship upon leaving' where ProgrammeId=4 and Code=N'Р1214' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants between 15 and 24 not in education or training who are in education/training, gaining a qualification, or are in employment including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1215' and Type=1 and Kind=3
update Indicators set NameAlt=N'Inactive participants between 25 and 29 not in education or training who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1216' and Type=1 and Kind=3
update Indicators set NameAlt=N'Roma inactive participants between 15 and 24 not in education or training who are in education/training, gaining a qualification, or are in employment including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1217' and Type=1 and Kind=3
update Indicators set NameAlt=N'Roma inactive participants between 25 and 29 not in education or training who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1218' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 15 and 24 with primary or lower secondary education who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1221' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 25 and 29 with primary or lower secondary education who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1222' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 15 and 24 with primary or lower secondary education who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1223' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 25 and 29 with primary or lower secondary education who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1224' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed Roma participants between 15 and 24 with primary or lower secondary education who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1225' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed Roma participants between 25 and 29 with primary or lower secondary education who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1226' and Type=1 and Kind=3
update Indicators set NameAlt=N'Long-term unemployed participants between 15 and 24 with primary or lower secondary education who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1227' and Type=1 and Kind=3
update Indicators set NameAlt=N'Long-term unemployed participants between 25 and 29 with primary or lower secondary education who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1228' and Type=1 and Kind=3
update Indicators set NameAlt=N'Long-term unemployed participants between 15 and 24 with primary and lower secondary education who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1229' and Type=1 and Kind=3
update Indicators set NameAlt=N'Long-term unemployed participants between 25 and 29 with primary and lower secondary education who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1220' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 15 and 24 with upper secondary or higher education without professional experience who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1231' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 25 and 29 with upper secondary or higher education without professional experience who complete the YEI supported intervention' where ProgrammeId=4 and Code=N'Р1232' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 15 and 24 with upper secondary or higher education without professional experience who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1233' and Type=1 and Kind=3
update Indicators set NameAlt=N'Unemployed participants between 25 and 29 with upper secondary or higher education without professional experience who are in education/training, gain a qualification, or are in employment, including self-employment, upon leaving' where ProgrammeId=4 and Code=N'Р1234' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of supported administrative regimes, reviewed for simplification' where ProgrammeId=1 and Code=N'О1-1' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of regulations with impact assessments' where ProgrammeId=1 and Code=N'О1-2' and Type=1 and Kind=2
update Indicators set NameAlt=N'Administrations, supported to introduce complex administrative service delivery' where ProgrammeId=1 and Code=N'O1-3' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of municipal services supported for standardization' where ProgrammeId=1 and Code=N'О1-4' and Type=1 and Kind=2
update Indicators set NameAlt=N'Control, revenue and regulatory authorities, supported to develop organizational and analytical capacity, including to carry out joint inspections' where ProgrammeId=1 and Code=N'О1-5' and Type=1 and Kind=2
update Indicators set NameAlt=N'State hybrid private cloud development projects' where ProgrammeId=1 and Code=N'O1-6' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of supported registers' where ProgrammeId=1 and Code=N'O1-7' and Type=1 and Kind=2
update Indicators set NameAlt=N'E-services supported, available in transactional regime' where ProgrammeId=1 and Code=N'О1-8' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of projects for development of e-governance sectoral systems (e-procurement, e-health, e-customs, e-archiving, e-insurance, etc.)' where ProgrammeId=1 and Code=N'O1-9' and Type=1 and Kind=2
update Indicators set NameAlt=N'Administrations supported for the introduction of quality management systems' where ProgrammeId=1 and Code=N'O2-1' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of sectoral/ policy functional reviews carried out' where ProgrammeId=1 and Code=N'O2-2' and Type=1 and Kind=2
update Indicators set NameAlt=N'Partnership projects for the development and / or implementation of key policies and legislation' where ProgrammeId=1 and Code=N'O2-3' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of administrations supported for the introduction of mentoring and coaching programs for employees' where ProgrammeId=1 and Code=N'O2-4' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of new / updated training modules for the administration supported' where ProgrammeId=1 and Code=N'O2-5' and Type=1 and Kind=2
update Indicators set NameAlt=N'Total number of trained state administration employees' where ProgrammeId=1 and Code=N'O2-6' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of projects fully or partially implemented by social partners or non-governmental organizations' where ProgrammeId=1 and Code=N'CO20' and Type=2 and Kind=2
update Indicators set NameAlt=N'Analyzes, research, studies, methodologies and assessments related to the operation of the judiciary supported' where ProgrammeId=1 and Code=N'O3-1' and Type=1 and Kind=2
update Indicators set NameAlt=N'Projects for the implementation of joint actions' where ProgrammeId=1 and Code=N'O3-2' and Type=1 and Kind=2
update Indicators set NameAlt=N'Judicial authorities supported for the introduction of program budgeting' where ProgrammeId=1 and Code=N'O3-3' and Type=1 and Kind=2
update Indicators set NameAlt=N'Projects for the promotion and development of alternative dispute resolution methods' where ProgrammeId=1 and Code=N'O3-4' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of institutions with IT and communication infrastructure audits' where ProgrammeId=1 and Code=N'O3-5' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of supported e-services of the judiciary' where ProgrammeId=1 and Code=N'O3-6' and Type=1 and Kind=2
update Indicators set NameAlt=N'SISC designed' where ProgrammeId=1 and Code=N'O3-7' and Type=1 and Kind=2
update Indicators set NameAlt=N'Trained magistrates, court officials, officials of the investigating authorities under the CPC' where ProgrammeId=1 and Code=N'O3-8' and Type=1 and Kind=2
update Indicators set NameAlt=N'Total length of reconstructed or upgraded railway line, of which: TEN-T' where ProgrammeId=2 and Code=N'CO12a' and Type=2 and Kind=2
update Indicators set NameAlt=N'Total length of newly built roads, of which: TEN-T' where ProgrammeId=2 and Code=N'CO13a' and Type=2 and Kind=2
update Indicators set NameAlt=N'Total length of new or improved tram and metro lines' where ProgrammeId=2 and Code=N'15' and Type=1 and Kind=2
update Indicators set NameAlt=N'New metro stations' where ProgrammeId=2 and Code=N'16' and Type=1 and Kind=2
update Indicators set NameAlt=N'Reconstructed railway stations' where ProgrammeId=2 and Code=N'7' and Type=1 and Kind=2
update Indicators set NameAlt=N'Built Intermodal freight terminal' where ProgrammeId=2 and Code=N'7.1' and Type=1 and Kind=2
update Indicators set NameAlt=N'Commissioned port reception facilities for ship-generated waste' where ProgrammeId=2 and Code=N'11' and Type=1 and Kind=2
update Indicators set NameAlt=N'Introduced/ upgraded navigation information systems' where ProgrammeId=2 and Code=N'9' and Type=1 and Kind=2
update Indicators set NameAlt=N'Delivered of multipurpose vessels' where ProgrammeId=2 and Code=N'10' and Type=1 and Kind=2
update Indicators set NameAlt=N'Built GSM-R network' where ProgrammeId=2 and Code=N'13' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of households with improved energy consumption classification' where ProgrammeId=3 and Code=N'CO31' and Type=2 and Kind=2
update Indicators set NameAlt=N'Decrease of annual primary energy consumption of public buildings' where ProgrammeId=3 and Code=N'CO32' and Type=2 and Kind=2
update Indicators set NameAlt=N'Estimated annual decrease of GHG' where ProgrammeId=3 and Code=N'CO34' and Type=2 and Kind=2
update Indicators set NameAlt=N'Total length of new or improved public transport lines' where ProgrammeId=3 and Code=N'1211' and Type=1 and Kind=2
update Indicators set NameAlt=N'Total surface area of rehabilitated land' where ProgrammeId=3 and Code=N'CO22' and Type=2 and Kind=2
update Indicators set NameAlt=N'Open space created or rehabilitated in urban areas' where ProgrammeId=3 and Code=N'CO38' and Type=2 and Kind=2
update Indicators set NameAlt=N'Public or commercial buildings built or renovated in urban areas' where ProgrammeId=3 and Code=N'CO39' and Type=2 and Kind=2
update Indicators set NameAlt=N'Population covered by improved social services' where ProgrammeId=3 and Code=N'1421' and Type=1 and Kind=2
update Indicators set NameAlt=N'Representatives from marginalised groups, including roma benefiting from modernised social infrastructure' where ProgrammeId=3 and Code=N'1422' and Type=1 and Kind=2
update Indicators set NameAlt=N'Rehabilitated housing in urban areas' where ProgrammeId=3 and Code=N'CO40' and Type=2 and Kind=2
update Indicators set NameAlt=N'Representatives from marginalised groups, including roma benefiting from modernised social infrastructure' where ProgrammeId=3 and Code=N'1511' and Type=1 and Kind=2
update Indicators set NameAlt=N'Capacity of supported childcare or educational infrastructure' where ProgrammeId=3 and Code=N'CO35' and Type=2 and Kind=2
update Indicators set NameAlt=N'Representatives from marginalised groups, including roma benefiting from modernised eduicational infrastructure' where ProgrammeId=3 and Code=N'3111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Population covered by improved emergency medical care services' where ProgrammeId=3 and Code=N'4111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Modernised facilities of EMC' where ProgrammeId=3 and Code=N'4112' and Type=1 and Kind=2
update Indicators set NameAlt=N'Purchased modern ambulances' where ProgrammeId=3 and Code=N'4121' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of supported facilities of social infrastructure in the process of deinstitutionalization' where ProgrammeId=3 and Code=N'5111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Developed tourist products for cultural heritage of national and world importance' where ProgrammeId=3 and Code=N'6111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Increase in expected number of visits to supported sites of cultural and natural heritage and attractions' where ProgrammeId=3 and Code=N'CO09' and Type=2 and Kind=2
update Indicators set NameAlt=N'Roads: Total length of reconstructed or upgraded roads' where ProgrammeId=3 and Code=N'CO14' and Type=2 and Kind=2
update Indicators set NameAlt=N'Inactive participants not in education or training between 30 and 54' where ProgrammeId=4 and Code=N'И1111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed participants between 30 and 54' where ProgrammeId=4 and Code=N'И1112' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive and unemployed Roma participants between 30 and 54' where ProgrammeId=4 and Code=N'И1113' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive participants between 30 and 54 with primary or lower secondary education (ISCED 1 and 2)' where ProgrammeId=4 and Code=N'И1121' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed participants between 30 and 54 with primary or lower secondary education (ISCED 1 and 2)' where ProgrammeId=4 and Code=N'И1122' and Type=1 and Kind=2
update Indicators set NameAlt=N'Roma participants between 30 and 54 with primary or lower secondary education (ISCED 1 and 2)' where ProgrammeId=4 and Code=N'И1123' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed participants,  including long-term unemployed, or inactive not in education or training above 54 years of age' where ProgrammeId=4 and Code=N'И1131' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive up to 29 years of age not in education or training' where ProgrammeId=4 and Code=N'И1311' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed up to 29 years of age with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1321' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed Roma up to 29 years of age with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1322' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed up to 29 years of age with upper secondary or higher education' where ProgrammeId=4 and Code=N'И1331' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive participants aged 15- 24,  not in training or education' where ProgrammeId=4 and Code=N'И1211' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive participants aged 25 - 29, not in training or education' where ProgrammeId=4 and Code=N'И1212' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive Roma participants aged 15- 24,  not in training or education' where ProgrammeId=4 and Code=N'И1213' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive Roma participants aged 25 - 29, not in training or education' where ProgrammeId=4 and Code=N'И1214' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed participants aged 15 - 24, with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1221' and Type=1 and Kind=2
update Indicators set NameAlt=N'Long-term unemployed participants aged 15 - 24, with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1222' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed participants aged 25 - 29, with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1223' and Type=1 and Kind=2
update Indicators set NameAlt=N'Long-term unemployed participants aged 25 - 29, with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1224' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed Roma participants aged 15 - 24, with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1225' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed Roma participants aged 25 - 29, with primary or lower secondary education' where ProgrammeId=4 and Code=N'И1226' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed participants aged 15-24, with upper secondary or higher education without professional experience' where ProgrammeId=4 and Code=N'И1231' and Type=1 and Kind=2
update Indicators set NameAlt=N'Unemployed participants aged 25-29, with upper secondary or higher education without professional experience' where ProgrammeId=4 and Code=N'И1232' and Type=1 and Kind=2
update Indicators set NameAlt=N'Безработни участници над 29 г. вкл. трайно безработни' where ProgrammeId=4 and Code=N'И1511' and Type=1 and Kind=2
update Indicators set NameAlt=N'Заети участници' where ProgrammeId=4 and Code=N'И1512' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of enterprises supported' where ProgrammeId=4 and Code=N'И1711' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employed, including self-employed, persons' where ProgrammeId=4 and Code=N'CO05' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of projects targeting public administrations and public services on the labour market' where ProgrammeId=4 and Code=N'И1411' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of the employed (staff) in institutions on the labour market, included in the HRD OP measures' where ProgrammeId=4 and Code=N'И1412' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of projects targeting public administrations and public services on the labour market' where ProgrammeId=4 and Code=N'И1421' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of the employed in institutions on the labour market, included in the HRD OP measures' where ProgrammeId=4 and Code=N'И1422' and Type=1 and Kind=2
update Indicators set NameAlt=N'Job-seekers who have received supporting EURES intermediary services, incl. information and consultations' where ProgrammeId=4 and Code=N'И1431' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employers who have received supporting EURES intermediary services, incl. information and consultations' where ProgrammeId=4 and Code=N'И1432' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of projects for reforming EURES network' where ProgrammeId=4 and Code=N'И1441' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employed, including self-employed above 54' where ProgrammeId=4 and Code=N'И1611' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employed participants including self-employed in priority sectors of the economy' where ProgrammeId=4 and Code=N'И1621' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employed participants including self-employed with upper secondary education or lower (ISCED 4)' where ProgrammeId=4 and Code=N'И1631' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive or unemployed participants' where ProgrammeId=4 and Code=N'И2211' and Type=1 and Kind=2
update Indicators set NameAlt=N'Children, including children with disabilities' where ProgrammeId=4 and Code=N'И2212' and Type=1 and Kind=2
update Indicators set NameAlt=N'Participants with disabilities over 18' where ProgrammeId=4 and Code=N'И2221' and Type=1 and Kind=2
update Indicators set NameAlt=N'Migrants, participants with a foreign background' where ProgrammeId=4 and Code=N'И2111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Other disadvantaged participants' where ProgrammeId=4 and Code=N'И2112' and Type=1 and Kind=2
update Indicators set NameAlt=N'Roma' where ProgrammeId=4 and Code=N'И2113' and Type=1 and Kind=2
update Indicators set NameAlt=N'Participants with disabilities  and participants over 65 incapable of self-care' where ProgrammeId=4 and Code=N'И2311' and Type=1 and Kind=2
update Indicators set NameAlt=N'Providers of services for social inclusion' where ProgrammeId=4 and Code=N'И2312' and Type=1 and Kind=2
update Indicators set NameAlt=N'Participants aged over 18' where ProgrammeId=4 and Code=N'И2321' and Type=1 and Kind=2
update Indicators set NameAlt=N'Providers of services for social inclusion' where ProgrammeId=4 and Code=N'И2322' and Type=1 and Kind=2
update Indicators set NameAlt=N'Children and youth in institutional care, covered by the deinstitutionalization measures' where ProgrammeId=4 and Code=N'И2331' and Type=1 and Kind=2
update Indicators set NameAlt=N'Providers of services for social inclusion' where ProgrammeId=4 and Code=N'И2332' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of cooperative enterprises and  enterprises from social economy supported' where ProgrammeId=4 and Code=N'И2411' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive or unemployed participants' where ProgrammeId=4 and Code=N'И2412' and Type=1 and Kind=2
update Indicators set NameAlt=N'Inactive or unemployed  Roma participants, covered by interventions in the sphere of social economy' where ProgrammeId=4 and Code=N'И2413' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employed participants' where ProgrammeId=4 and Code=N'И3111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of projects aimed at the public administrations and public services at national, regional or local level' where ProgrammeId=4 and Code=N'И3121' and Type=1 and Kind=2
update Indicators set NameAlt=N'Identified innovative practices' where ProgrammeId=4 and Code=N'И4111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of enterprises supported by the Sofia Tech Park' where ProgrammeId=5 and Code=N'SO01' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of enterprises receiving support' where ProgrammeId=5 and Code=N'CO01' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of enterprises cooperating with research institutions (Activites 1 & 3)' where ProgrammeId=5 and Code=N'CO26' and Type=2 and Kind=2
update Indicators set NameAlt=N'Private investment matching public support for innovation or R&D projects (all activities)' where ProgrammeId=5 and Code=N'CO27' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of enterprises supported to introduce new to the market products' where ProgrammeId=5 and Code=N'CO28' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of enterprises supported to introduce new to the firm products' where ProgrammeId=5 and Code=N'CO29' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of enterprises receiving grants' where ProgrammeId=5 and Code=N'CO02' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of enterprises receiving financial support other than grants (Activity 1)' where ProgrammeId=5 and Code=N'CO03' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of new enterprises supported (all activities)' where ProgrammeId=5 and Code=N'CO05' and Type=2 and Kind=2
update Indicators set NameAlt=N'Private investment matching public support to enterprises (grants)' where ProgrammeId=5 and Code=N'CO06' and Type=2 and Kind=2
update Indicators set NameAlt=N'Private investment matching public support to enterprises (non-grants)' where ProgrammeId=5 and Code=N'CO07' and Type=2 and Kind=2
update Indicators set NameAlt=N'Employment increase in supported enterprises' where ProgrammeId=5 and Code=N'CO08' and Type=2 and Kind=2
update Indicators set NameAlt=N'Number of enterprises receiving non-financial support' where ProgrammeId=5 and Code=N'CO04' and Type=2 and Kind=2
update Indicators set NameAlt=N'Energy audits performed' where ProgrammeId=5 and Code=N'SO03' and Type=1 and Kind=2
update Indicators set NameAlt=N'Expected energy savings in enterprises as a result of completed projects' where ProgrammeId=5 and Code=N'SO04' and Type=1 and Kind=2
update Indicators set NameAlt=N'GHG reduction' where ProgrammeId=5 and Code=N'CO34' and Type=2 and Kind=2
update Indicators set NameAlt=N'Implemented pilot and demonstration innitiatives' where ProgrammeId=5 and Code=N'SO05' and Type=1 and Kind=2
update Indicators set NameAlt=N'Supported projects related to industrial waste management' where ProgrammeId=5 and Code=N'SO06' and Type=1 and Kind=2
update Indicators set NameAlt=N'Gas Interconnection Bulgaria-Serbia built' where ProgrammeId=5 and Code=N'SO07' and Type=1 and Kind=2
update Indicators set NameAlt=N'Constructed/Rehabilitated/Reconstructed WWTP' where ProgrammeId=6 and Code=N'1' and Type=1 and Kind=2
update Indicators set NameAlt=N'New/Updated analytical/programming/strategic documents' where ProgrammeId=6 and Code=N'3' and Type=1 and Kind=2
update Indicators set NameAlt=N'Additional population served by improved water supply' where ProgrammeId=6 and Code=N'CO18' and Type=2 and Kind=2
update Indicators set NameAlt=N'Additional population served by improved wastewater treatment' where ProgrammeId=6 and Code=N'CO19' and Type=2 and Kind=2
update Indicators set NameAlt=N'Additional waste recycling capacity' where ProgrammeId=6 and Code=N'17' and Type=1 and Kind=2
update Indicators set NameAlt=N'Additional capacity for recovery of waste (to generate energy)' where ProgrammeId=6 and Code=N'8' and Type=1 and Kind=2
update Indicators set NameAlt=N'Surface area of habitats of species supported in order to attain a better conservation status' where ProgrammeId=6 and Code=N'10' and Type=1 and Kind=2
update Indicators set NameAlt=N'Mapped Natura 2000 marine sites' where ProgrammeId=6 and Code=N'11' and Type=1 and Kind=2
update Indicators set NameAlt=N'National information campaigns carried out' where ProgrammeId=6 and Code=N'12' and Type=1 and Kind=2
update Indicators set NameAlt=N'Natura 2000 area with established management structure' where ProgrammeId=6 and Code=N'13' and Type=1 and Kind=2
update Indicators set NameAlt=N'Surface area of habitats supported in order to attain a better conservation status' where ProgrammeId=6 and Code=N'CO23' and Type=2 and Kind=2
update Indicators set NameAlt=N'Centers for increasing the population preparedness for flood response established' where ProgrammeId=6 and Code=N'18' and Type=1 and Kind=2
update Indicators set NameAlt=N'Reinforced landslide area' where ProgrammeId=6 and Code=N'21' and Type=1 and Kind=2
update Indicators set NameAlt=N'Population benefiting from flood protection measures' where ProgrammeId=6 and Code=N'CO20' and Type=2 and Kind=2
update Indicators set NameAlt=N'Population benefitting from the measures to reduce PM10/NOx quantities' where ProgrammeId=6 and Code=N'19' and Type=1 and Kind=2
update Indicators set NameAlt=N'Projects for lowering the quantities of PM10 and NOx' where ProgrammeId=6 and Code=N'20' and Type=1 and Kind=2
update Indicators set NameAlt=N'Newly built infrastructure complexes in CoE and CoC' where ProgrammeId=7 and Code=N'113' and Type=1 and Kind=2
update Indicators set NameAlt=N'Joint research projects developed between centres (CoE or CoC) and businesses' where ProgrammeId=7 and Code=N'114' and Type=1 and Kind=2
update Indicators set NameAlt=N'Researchers working in improved RI facilities  outside Sofia' where ProgrammeId=7 and Code=N'СО25' and Type=1 and Kind=2
update Indicators set NameAlt=N'Renewed infrastructures' where ProgrammeId=7 and Code=N'116' and Type=1 and Kind=2
update Indicators set NameAlt=N'Researchers trained via international cooperation' where ProgrammeId=7 and Code=N'132' and Type=1 and Kind=2
update Indicators set NameAlt=N'Research organizations and universities, participating in international technological initiatives' where ProgrammeId=7 and Code=N'133' and Type=1 and Kind=2
update Indicators set NameAlt=N'Projects involving international cooperation' where ProgrammeId=7 and Code=N'134' and Type=1 and Kind=2
update Indicators set NameAlt=N'New researchers in supported entities' where ProgrammeId=7 and Code=N'CO24' and Type=2 and Kind=2
update Indicators set NameAlt=N'Researchers working in improved RI facilities' where ProgrammeId=7 and Code=N'CO25' and Type=2 and Kind=2
update Indicators set NameAlt=N'Enterprises cooperating with RI' where ProgrammeId=7 and Code=N'CO26' and Type=2 and Kind=2
update Indicators set NameAlt=N'Pedagogical specialists involved in training for the application of modern evaluation methods' where ProgrammeId=7 and Code=N'2111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Schools and kindergartens involved in actions for introduction of innovative teaching methods using modern ICT' where ProgrammeId=7 and Code=N'2112' and Type=1 and Kind=2
update Indicators set NameAlt=N'School students involved in activities aiming to increase motivation for learning through the development of specific knowledge, skills and competencies' where ProgrammeId=7 and Code=N'2121' and Type=1 and Kind=2
update Indicators set NameAlt=N'Schools offering activities to increase the motivation to learn through development of specific knowledge, skills and competences' where ProgrammeId=7 and Code=N'2122' and Type=1 and Kind=2
update Indicators set NameAlt=N'Career centres which have been supported to follow-up the realisation of graduates in the first year after graduation' where ProgrammeId=7 and Code=N'2211' and Type=1 and Kind=2
update Indicators set NameAlt=N'Higher education institutions, which have been included in the HEInnovate initiative' where ProgrammeId=7 and Code=N'2221' and Type=1 and Kind=2
update Indicators set NameAlt=N'Students in priority specialties having received scholarships and special scholarships' where ProgrammeId=7 and Code=N'2231' and Type=1 and Kind=2
update Indicators set NameAlt=N'Lecturers in schools of higher education involved in programmes for raising the qualification' where ProgrammeId=7 and Code=N'2241' and Type=1 and Kind=2
update Indicators set NameAlt=N'Schools of higher education involved in measures for optimisation of the professional fields and the institutional network' where ProgrammeId=7 and Code=N'2242' and Type=1 and Kind=2
update Indicators set NameAlt=N'Students involved in mobility programmes' where ProgrammeId=7 and Code=N'2243' and Type=1 and Kind=2
update Indicators set NameAlt=N'Young researchers aged up to 34 years (inclusive) supported under the OP to participate in R&D activities (GOVERD plus HERD)' where ProgrammeId=7 and Code=N'2244' and Type=1 and Kind=2
update Indicators set NameAlt=N'Pedagogical specialists involved in programmes for raising the qualification under the OP - Up to 34 years of age' where ProgrammeId=7 and Code=N'2311' and Type=1 and Kind=2
update Indicators set NameAlt=N'Pedagogical specialists involved in programmes for raising the qualification under the OP - B/w 35 and 54 years of age' where ProgrammeId=7 and Code=N'2312' and Type=1 and Kind=2
update Indicators set NameAlt=N'Students enrolled in pedagogical sciences' where ProgrammeId=7 and Code=N'2321' and Type=1 and Kind=2
update Indicators set NameAlt=N'Participants in various forms of mobility with improved qualification and skills and better job opportunities' where ProgrammeId=7 and Code=N'2322' and Type=1 and Kind=2
update Indicators set NameAlt=N'Persons willing to validate knowledge, skills and competences' where ProgrammeId=7 and Code=N'2323' and Type=1 and Kind=2
update Indicators set NameAlt=N'School students involved in career guidance activities under the OP' where ProgrammeId=7 and Code=N'2324' and Type=1 and Kind=2
update Indicators set NameAlt=N'Schools of higher education participating in the creation of a common information career centre network' where ProgrammeId=7 and Code=N'2325' and Type=1 and Kind=2
update Indicators set NameAlt=N'Participants in continued learning and upgrading knowledge, skills and competences, between 25-64 years of age' where ProgrammeId=7 and Code=N'2327' and Type=1 and Kind=2
update Indicators set NameAlt=N'School students participating in activities for promotion of vocational education in branches of primary importance to the economy' where ProgrammeId=7 and Code=N'2410' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of established educational training firms (ETFs)' where ProgrammeId=7 and Code=N'2411' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of vocational schools having introduced dual learning' where ProgrammeId=7 and Code=N'2421' and Type=1 and Kind=2
update Indicators set NameAlt=N'School students included in practical training programs in a real work environment' where ProgrammeId=7 and Code=N'2422' and Type=1 and Kind=2
update Indicators set NameAlt=N'Students in HEIs involved in practical training activities in a real work environment' where ProgrammeId=7 and Code=N'2423' and Type=1 and Kind=2
update Indicators set NameAlt=N'Children and school students with SEN , participating in activities, supported by the IP9i' where ProgrammeId=7 and Code=N'3111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Kindergartens/united childcare institutions supported to provide supportive environment for early prevention of learning difficulties' where ProgrammeId=7 and Code=N'3112' and Type=1 and Kind=2
update Indicators set NameAlt=N'Disadvantaged children, students and young people (incl. Roma), participating in activities for educational integration and re-integration' where ProgrammeId=7 and Code=N'3211' and Type=1 and Kind=2
update Indicators set NameAlt=N'Pedagogical specialists involved in training to work in multicultural environment' where ProgrammeId=7 and Code=N'3212' and Type=1 and Kind=2
update Indicators set NameAlt=N'Persons over 16 (including Roma) involved in literacy courses or courses for mastering the learning content intended for the lower secondary stage of basic education under the OP' where ProgrammeId=7 and Code=N'3213' and Type=1 and Kind=2
update Indicators set NameAlt=N'Certified expenditure' where ProgrammeId=1 and Code=N'F-1' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified expenditure' where ProgrammeId=1 and Code=N'F-2' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified expenditure' where ProgrammeId=1 and Code=N'F-3' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified expenditures' where ProgrammeId=2 and Code=N'1' and Type=1 and Kind=1
update Indicators set NameAlt=N'Number of major projects with started construction' where ProgrammeId=2 and Code=N'4' and Type=1 and Kind=4
update Indicators set NameAlt=N'Built GSM-R network' where ProgrammeId=2 and Code=N'13' and Type=1 and Kind=4
update Indicators set NameAlt=N'Number of contracts with started construction' where ProgrammeId=2 and Code=N'5' and Type=1 and Kind=4
update Indicators set NameAlt=N'Certified amount' where ProgrammeId=3 and Code=N'1' and Type=1 and Kind=1
update Indicators set NameAlt=N'Approval of major project with started construction works and supplies for some investments' where ProgrammeId=3 and Code=N'1' and Type=1 and Kind=4
update Indicators set NameAlt=N'Financial instrument for tourism development established. Mechanism for combination of support through FI and grants developed. Started construction works for some investments.' where ProgrammeId=3 and Code=N'2' and Type=1 and Kind=4
update Indicators set NameAlt=N'ESF certified expenditure' where ProgrammeId=4 and Code=N'Ф11' and Type=1 and Kind=1
update Indicators set NameAlt=N'YEI certified expenditure' where ProgrammeId=4 and Code=N'Ф12' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified funds' where ProgrammeId=4 and Code=N'Ф21' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified funds' where ProgrammeId=4 and Code=N'Ф31' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified funds' where ProgrammeId=4 and Code=N'Ф41' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified expenditure' where ProgrammeId=6 and Code=N'3' and Type=1 and Kind=1
update Indicators set NameAlt=N'Number of enterprises receiving grants' where ProgrammeId=6 and Code=N'6' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified expenditure' where ProgrammeId=6 and Code=N'9' and Type=1 and Kind=4
update Indicators set NameAlt=N'Total amount of the eligible expenditure certified by the Certifying authority' where ProgrammeId=6 and Code=N'15' and Type=1 and Kind=2
update Indicators set NameAlt=N'Major project signed grant contract' where ProgrammeId=6 and Code=N'27' and Type=1 and Kind=3
update Indicators set NameAlt=N'Surface area of habitats supported in order to attain a better conservation status' where ProgrammeId=6 and Code=N'28' and Type=1 and Kind=4
update Indicators set NameAlt=N'Total amount of the eligible expenditure certified by the Certifying authority' where ProgrammeId=5 and Code=N'FI01' and Type=1 and Kind=1
update Indicators set NameAlt=N'Population benefiting from flood protection measures' where ProgrammeId=5 and Code=N'FI02' and Type=1 and Kind=1
update Indicators set NameAlt=N'Reinforced landslide area' where ProgrammeId=5 and Code=N'SO02' and Type=1 and Kind=2
update Indicators set NameAlt=N'Total amount of the eligible expenditure certified by the Certifying authority' where ProgrammeId=5 and Code=N'FI03' and Type=1 and Kind=1
update Indicators set NameAlt=N'Certified expenditures' where ProgrammeId=7 and Code=N'Ф1' and Type=1 and Kind=1
update Indicators set NameAlt=N'Pedagogical specialists involved in training for the application of modern evaluation methods' where ProgrammeId=7 and Code=N'И2111' and Type=1 and Kind=2
update Indicators set NameAlt=N'School students involved in activities aiming to increase motivation for learning through the development of specific knowledge, skills and competencies' where ProgrammeId=7 and Code=N'И2121' and Type=1 and Kind=2
update Indicators set NameAlt=N'Students in priority subjects having received scholarships and special scholarships' where ProgrammeId=7 and Code=N'И2231' and Type=1 and Kind=2
update Indicators set NameAlt=N'Pedagogical specialists involved in programmes for raising the qualification under the OP - up to 34 years of age' where ProgrammeId=7 and Code=N'И2311' and Type=1 and Kind=2
update Indicators set NameAlt=N'Pedagogical specialists involved in programmes for raising the qualification under the OP - - b/w 35 and 54 years of age' where ProgrammeId=7 and Code=N'и2312' and Type=1 and Kind=2
update Indicators set NameAlt=N'Students enrolled in pedagogical sciences' where ProgrammeId=7 and Code=N'И2321' and Type=1 and Kind=2
update Indicators set NameAlt=N'School students involved in career guidance activities under the OP' where ProgrammeId=7 and Code=N'И2324' and Type=1 and Kind=2
update Indicators set NameAlt=N'School students included in practical training programs in a real work environment' where ProgrammeId=7 and Code=N'И2422' and Type=1 and Kind=2
update Indicators set NameAlt=N'Certified expenditures' where ProgrammeId=7 and Code=N'Ф2' and Type=1 and Kind=1
update Indicators set NameAlt=N'Children and school students with SEN, participating in activities supported by the the IP9i' where ProgrammeId=7 and Code=N'И3111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Disadvantaged children, students and young people (incl. Roma), participating in activities for educational integration and re-integration' where ProgrammeId=7 and Code=N'И3211' and Type=1 and Kind=2
update Indicators set NameAlt=N'Certified expenditures' where ProgrammeId=7 and Code=N'ФЗ' and Type=1 and Kind=1
update Indicators set NameAlt=N'Satisfaction of participants with trainings delivered' where ProgrammeId=1 and Code=N'R4-2' and Type=1 and Kind=3
update Indicators set NameAlt=N'Annual turnover of the beneficiaries’ staff' where ProgrammeId=1 and Code=N'R4-1' and Type=1 and Kind=3
update Indicators set NameAlt=N'Satisfaction of UMIS users' where ProgrammeId=1 and Code=N'R4-3' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of the population of  15+, aware of the EU Cohesion policy' where ProgrammeId=1 and Code=N'R4-4' and Type=1 and Kind=3
update Indicators set NameAlt=N'Trained people according to training programs' where ProgrammeId=2 and Code=N'14' and Type=1 and Kind=3
update Indicators set NameAlt=N'Completed activities under the Communication Plan' where ProgrammeId=2 and Code=N'15' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time required to pay beneficiary from the date of submission of the application for reimbursement' where ProgrammeId=2 and Code=N'16' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time for evaluation of project' where ProgrammeId=2 and Code=N'17' and Type=1 and Kind=3
update Indicators set NameAlt=N'Degree of public awareness of OPTTI' where ProgrammeId=2 and Code=N'18' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of expenditure covered by on the spot checks' where ProgrammeId=2 and Code=N'19' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time required to pay the beneficiary from the submission date of the reimbursement application' where ProgrammeId=3 and Code=N'811' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time for a project approval (between submission of a project’s application and signing of a contract' where ProgrammeId=3 and Code=N'812' and Type=1 and Kind=3
update Indicators set NameAlt=N'Level of satisfaction of OPRG beneficiaries with training and technical support' where ProgrammeId=3 and Code=N'813' and Type=1 and Kind=3
update Indicators set NameAlt=N'Level of general public awareness about OPRG 2014-2020' where ProgrammeId=3 and Code=N'814' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of employees, gaining qualification upon leaving' where ProgrammeId=4 and Code=N'Р5111' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of operations with an improved design  as a result of an evaluation done' where ProgrammeId=4 and Code=N'Р5112' and Type=1 and Kind=3
update Indicators set NameAlt=N'Number of beneficiaries trained in project management' where ProgrammeId=4 and Code=N'Р5121' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time for a project approval (between submission of a project‘s application and signing of a contract' where ProgrammeId=5 and Code=N'SR09' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time required to pay the beneficiary from the submission date of the reimbursement application' where ProgrammeId=5 and Code=N'SR10' and Type=1 and Kind=3
update Indicators set NameAlt=N'Degree of public awareness of OPIC, overall and with regard to the particular procedures' where ProgrammeId=5 and Code=N'SR11' and Type=1 and Kind=3
update Indicators set NameAlt=N'Terminated contracts' where ProgrammeId=5 and Code=N'SR12' and Type=1 and Kind=3
update Indicators set NameAlt=N'Level of satisfaction of OPIC beneficiaries with training and technical support' where ProgrammeId=5 and Code=N'SR13' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time for beneficiary payments request verification.' where ProgrammeId=6 and Code=N'19' and Type=1 and Kind=3
update Indicators set NameAlt=N'Average time for a project approval.' where ProgrammeId=6 and Code=N'4' and Type=1 and Kind=3
update Indicators set NameAlt=N'Ниво на обществена осведоменост за ОПОС' where ProgrammeId=6 and Code=N'21' and Type=1 and Kind=3
update Indicators set NameAlt=N'Level of satisfaction of the beneficiaries with the TA measures and training provided' where ProgrammeId=6 and Code=N'22' and Type=1 and Kind=3
update Indicators set NameAlt=N'Submitted project proposals' where ProgrammeId=7 and Code=N'Р41' and Type=1 and Kind=3
update Indicators set NameAlt=N'Share of the costs verified by the MA versus the costs reported by beneficiaries' where ProgrammeId=7 and Code=N'Р42' and Type=1 and Kind=3
update Indicators set NameAlt=N'Information materials published by type (handbooks, guidelines, books, booklets, information flyers etc.)' where ProgrammeId=1 and Code=N'O4-2' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of public information events' where ProgrammeId=1 and Code=N'O4-3' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of employees, whose remunerations are co-funded by technical assistance' where ProgrammeId=1 and Code=N'О4-4' and Type=1 and Kind=2
update Indicators set NameAlt=N'Projects contributing to the reduction of  administrative burden' where ProgrammeId=1 and Code=N'О4-5' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employees trained' where ProgrammeId=1 and Code=N'O4-1' and Type=1 and Kind=2
update Indicators set NameAlt=N'Information materials by type elaborated (printed, electronic and audiovisual)' where ProgrammeId=1 and Code=N'O5-5' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of public information events' where ProgrammeId=1 and Code=N'O5-6' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of employees, whose remunerations are co-funded by technical assistance' where ProgrammeId=1 and Code=N'O5-7' and Type=1 and Kind=2
update Indicators set NameAlt=N'MA staff and MC members trained' where ProgrammeId=1 and Code=N'O5-1' and Type=1 and Kind=2
update Indicators set NameAlt=N'MC meetings held' where ProgrammeId=1 and Code=N'O5-2' and Type=1 and Kind=2
update Indicators set NameAlt=N'Evaluations of OPGG, priorities, procedures, etc.' where ProgrammeId=1 and Code=N'O5-3' and Type=1 and Kind=2
update Indicators set NameAlt=N'Analyses, studies, reports and other items facilitating the implementation of OPGG and the preparation for the next programming period' where ProgrammeId=1 and Code=N'O5-4' and Type=1 and Kind=2
update Indicators set NameAlt=N'Adopted Evaluation plan' where ProgrammeId=2 and Code=N'20' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of trainings of employees of Managing Authority and beneficiaries' where ProgrammeId=2 and Code=N'25' and Type=1 and Kind=2
update Indicators set NameAlt=N'Meetings held of the MC' where ProgrammeId=2 and Code=N'21' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of employees (Full-time equivalents, FTEs) whose salaries are co-financed by technical assistance' where ProgrammeId=2 and Code=N'22' and Type=1 and Kind=2
update Indicators set NameAlt=N'Major  information activities' where ProgrammeId=2 and Code=N'23' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of public information events' where ProgrammeId=2 and Code=N'24' and Type=1 and Kind=2
update Indicators set NameAlt=N'Adopted communication Strategy' where ProgrammeId=2 and Code=N'26' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of on the spot checks' where ProgrammeId=2 and Code=N'27' and Type=1 and Kind=2
update Indicators set NameAlt=N'Supported salaries with a full-time equivalent' where ProgrammeId=3 and Code=N'8111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Trained people from beneficiaries' where ProgrammeId=3 and Code=N'8121' and Type=1 and Kind=2
update Indicators set NameAlt=N'Conducted information campaigns and publicity activities' where ProgrammeId=3 and Code=N'8131' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of participations of MA staff in trainings, traineeships or work visits' where ProgrammeId=4 and Code=N'И5111' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of information campaigns' where ProgrammeId=4 and Code=N'И5121' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of information events aimed at promoting HRD OP and the results achieved under it' where ProgrammeId=4 and Code=N'И5112' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of participants in trainings for beneficiaries' where ProgrammeId=4 and Code=N'И5122' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of evaluations done' where ProgrammeId=4 and Code=N'И5113' and Type=1 and Kind=2
update Indicators set NameAlt=N'MA staff, including in regional offices, having received training' where ProgrammeId=5 and Code=N'SO08' and Type=1 and Kind=2
update Indicators set NameAlt=N'Trained beneficiaries/applicants' where ProgrammeId=5 and Code=N'SO09' and Type=1 and Kind=2
update Indicators set NameAlt=N'Analyses, studies and other consultancy support' where ProgrammeId=5 and Code=N'SO10' and Type=1 and Kind=2
update Indicators set NameAlt=N'Promotional campaigns and public events held' where ProgrammeId=5 and Code=N'SO11' and Type=1 and Kind=2
update Indicators set NameAlt=N'Monitoring Committee meetings held' where ProgrammeId=5 and Code=N'SO12' and Type=1 and Kind=2
update Indicators set NameAlt=N'Internal and external evaluations performed' where ProgrammeId=5 and Code=N'SO13' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of on the spot verifications' where ProgrammeId=5 and Code=N'SO14' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of employees (Full-time equivalents, FTEs) whose salaries are co-financed by technical assistance.' where ProgrammeId=5 and Code=N'SO15' and Type=1 and Kind=2
update Indicators set NameAlt=N'Trainings for MA officials' where ProgrammeId=6 and Code=N'22' and Type=1 and Kind=2
update Indicators set NameAlt=N'Employees (FTEs) whose salaries are co-financed by TA' where ProgrammeId=6 and Code=N'23' and Type=1 and Kind=2
update Indicators set NameAlt=N'Conducted evaluations on the programme' where ProgrammeId=6 and Code=N'24' and Type=1 and Kind=2
update Indicators set NameAlt=N'Trainings  for beneficiaries’ officials' where ProgrammeId=6 and Code=N'25' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of information campaigns' where ProgrammeId=6 and Code=N'26' and Type=1 and Kind=2
update Indicators set NameAlt=N'Beneficiaries’ officials trained' where ProgrammeId=6 and Code=N'27' and Type=1 and Kind=2
update Indicators set NameAlt=N'Trainings  for beneficiaries’ officials' where ProgrammeId=6 and Code=N'28' and Type=1 and Kind=2
update Indicators set NameAlt=N'MA staff members trained' where ProgrammeId=7 and Code=N'41' and Type=1 and Kind=2
update Indicators set NameAlt=N'OP evaluations, analyses and studies performed' where ProgrammeId=7 and Code=N'42' and Type=1 and Kind=2
update Indicators set NameAlt=N'Information campaigns' where ProgrammeId=7 and Code=N'43' and Type=1 and Kind=2
update Indicators set NameAlt=N'Beneficiaries (organisations which have signed contracts for provision of grants) trained' where ProgrammeId=7 and Code=N'44' and Type=1 and Kind=2
update Indicators set NameAlt=N'Number of employees (FTEs) whose salaries are financed by Technical Assistance' where ProgrammeId=7 and Code=N'45' and Type=1 and Kind=2


