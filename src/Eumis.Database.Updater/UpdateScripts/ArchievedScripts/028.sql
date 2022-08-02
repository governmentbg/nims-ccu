GO

ALTER TABLE [dbo].[RegProjectXmls] ALTER COLUMN [Xml] XMLALTER TABLE [dbo].[ProjectXmls] ALTER COLUMN [Xml] XMLALTER TABLE [dbo].[ProcedureEvalTableXmls] ALTER COLUMN [Xml] XMLDROP XML SCHEMA COLLECTION [dbo].[RioSchemaCollection]DBCC CLEANTABLE ('Eumis', 'RegProjectXmls')DBCC CLEANTABLE ('Eumis', 'ProjectXmls')DBCC CLEANTABLE ('Eumis', 'ProcedureEvalTableXmls')--------------------------------------------CREATE XML SCHEMA COLLECTION [dbo].[RioSchemaCollection] AS N'
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <xsd:import namespace="http://ereg.egov.bg/segment/R-10019" />
    <xsd:import namespace="http://ereg.egov.bg/segment/R-10023" />
    <xsd:element name="Project" xmlns:p="http://ereg.egov.bg/segment/R-10019" type="p:Project" />
    <xsd:element name="EvalTable" xmlns:p="http://ereg.egov.bg/segment/R-10023" type="p:EvalTable" />
</xsd:schema>


<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09989" 
	xmlns="http://ereg.egov.bg/segment/R-09989" 
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:complexType name="Location">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Местоположение</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Code" type="xsd:string" minOccurs="0" />
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="FullPath" type="xsd:string" minOccurs="0" />
      <xsd:element name="FullPathName" type="xsd:string" minOccurs="0" />
		</xsd:sequence>

		<xsd:attribute name="orderNum" type="xsd:integer"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/nomenclature/R-09990" xmlns="http://ereg.egov.bg/nomenclature/R-09990" xmlns:xsd="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified">
  <xsd:simpleType name="MessageTypeNomenclature">
    <xsd:annotation>
      <xsd:documentation xml:lang="bg">Номенклатура на видовете съобщения</xsd:documentation>
    </xsd:annotation>
    <xsd:restriction base="xsd:string">
      <xsd:enumeration value="Send" />
      <xsd:enumeration value="Reply" />
    </xsd:restriction>
  </xsd:simpleType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09991" 
	xmlns="http://ereg.egov.bg/segment/R-09991" 
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:complexType name="EnumNomenclature">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Enum номенклатура</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Value" type="xsd:string" minOccurs="0" />
			<xsd:element name="Description" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09992"
	xmlns="http://ereg.egov.bg/segment/R-09992"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema"
	elementFormDefault="qualified">

  <xsd:complexType name="AttachedDocumentContent">
    <xsd:annotation>
      <xsd:documentation xml:lang="bg">Съдържание на прикачен документ</xsd:documentation>
    </xsd:annotation>
    <xsd:sequence>
      <xsd:element name="FileName" type="xsd:string" minOccurs="0" />
      <xsd:element name="BlobContentId" type="xsd:string" minOccurs="0" />
      <xsd:element name="Hash" type="xsd:string" minOccurs="0" />
      <xsd:element name="Size" type="xsd:string" minOccurs="0" />
    </xsd:sequence>
  </xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/nomenclature/R-09993" xmlns="http://ereg.egov.bg/nomenclature/R-09993" xmlns:xsd="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified">
  <xsd:simpleType name="EvalTypeNomenclature">
    <xsd:annotation>
      <xsd:documentation xml:lang="bg">Номенклатура на видовете оценяване</xsd:documentation>
    </xsd:annotation>
    <xsd:restriction base="xsd:string">
      <xsd:enumeration value="Rejection" />
      <xsd:enumeration value="Weight" />
    </xsd:restriction>
  </xsd:simpleType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09994" 
	xmlns="http://ereg.egov.bg/segment/R-09994" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="PaperAttachedDocument">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Прикачен документ на хартиен носител</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Type" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="Description" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09995" 
	xmlns="http://ereg.egov.bg/segment/R-09995" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:ca="http://ereg.egov.bg/segment/R-10011"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10011" />
	
	<xsd:complexType name="ProgrammeContractActivities">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">План по програма за изпълнение на Дейности на договор за БФП</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="ContractActivity" type="ca:ContractActivity" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="isLocked" type="xsd:boolean"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09997" 
	xmlns="http://ereg.egov.bg/segment/R-09997" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="ProgrammeBasicData">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Основни данни за програма</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="Programme" type="pun:PublicNomenclature" minOccurs="0" />
      <xsd:element name="ProgrammePriority" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09998"
	xmlns="http://ereg.egov.bg/segment/R-09998"
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:icd="http://ereg.egov.bg/segment/R-10005"
	xmlns:b="http://ereg.egov.bg/segment/R-10010"
	xmlns:con="http://ereg.egov.bg/segment/R-10006"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema"
	elementFormDefault="qualified">

  <xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10005" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10010" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10006" />

  <xsd:complexType name="DimensionsBudgetContract">
    <xsd:annotation>
      <xsd:documentation xml:lang="bg">Кодове по измерения, бюджет и източници на финансиране по програма</xsd:documentation>
    </xsd:annotation>
    <xsd:sequence>
      <xsd:element name="InterventionCategoryDimensions" type="icd:InterventionCategoryDimensions" minOccurs="0" />
      <xsd:element name="Budget" type="b:Budget" minOccurs="0" />
      <xsd:element name="Contract" type="con:Contract" minOccurs="0" />
    </xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="isLocked" type="xsd:boolean"/>
    <xsd:attribute name="programmeCode" type="xsd:string"/>
  </xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-09999"
	xmlns="http://ereg.egov.bg/segment/R-09999"
	
	xmlns:l="http://ereg.egov.bg/segment/R-09989"
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema"
	elementFormDefault="qualified">

  <xsd:import namespace="http://ereg.egov.bg/segment/R-09989" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />

  <xsd:complexType name="NutsAddress">
    <xsd:annotation>
      <xsd:documentation xml:lang="bg">Адрес по региони</xsd:documentation>
    </xsd:annotation>
    <xsd:sequence>
      <xsd:element name="NutsLevel" type="prn:PrivateNomenclature" minOccurs="0" />

      <xsd:element name="NutsAddressContent" minOccurs="0" maxOccurs="unbounded">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="Country" type="l:Location" minOccurs="0" />
            <xsd:element name="ProtectedZone" type="l:Location" minOccurs="0" />
            <xsd:element name="Nuts1" type="l:Location" minOccurs="0" />
            <xsd:element name="Nuts2" type="l:Location" minOccurs="0" />
            <xsd:element name="District" type="l:Location" minOccurs="0" />
            <xsd:element name="Municipality" type="l:Location" minOccurs="0" />
            <xsd:element name="Settlement" type="l:Location" minOccurs="0" />
          </xsd:sequence>
        </xsd:complexType>
      </xsd:element>
    </xsd:sequence>
  </xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10000" 
	xmlns="http://ereg.egov.bg/segment/R-10000" 
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:complexType name="PrivateNomenclature">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Вътрешна номенклатура</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Id" type="xsd:string" minOccurs="0" />
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
		</xsd:sequence>

		<xsd:attribute name="orderNum" type="xsd:integer"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10001" 
	xmlns="http://ereg.egov.bg/segment/R-10001" 
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:complexType name="PublicNomenclature">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Външна номенклатура</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Code" type="xsd:string" minOccurs="0" />
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
		</xsd:sequence>

		<xsd:attribute name="orderNum" type="xsd:integer"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10002" 
	xmlns="http://ereg.egov.bg/segment/R-10002" 
	
	xmlns:pbd="http://ereg.egov.bg/segment/R-09997"
            
	xmlns:en="http://ereg.egov.bg/segment/R-09991"
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:na="http://ereg.egov.bg/segment/R-09999"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

  <xsd:import namespace="http://ereg.egov.bg/segment/R-09997" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-09991" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-09999" />
	
	<xsd:complexType name="ProjectBasicData">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Основни данни за проектно предложение</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="ProgrammeBasicData" type="pbd:ProgrammeBasicData" minOccurs="0" maxOccurs="unbounded" />
			<xsd:element name="Procedure" type="pun:PublicNomenclature" minOccurs="0" />
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="NameEN" type="xsd:string" minOccurs="0" />
      <xsd:element name="ApplicationFormType" type="en:EnumNomenclature" minOccurs="0" />
      <xsd:element name="PreliminarySelectionProjectRegNumber" type="xsd:string" minOccurs="0" />
			<xsd:element name="Duration" type="xsd:integer" minOccurs="0" />
			<xsd:element name="NutsAddress" type="na:NutsAddress" minOccurs="0" />
			<xsd:element name="IsVatEligible" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="AmountType" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="IsJointActionPlan" type="xsd:boolean" minOccurs="0" />
			<xsd:element name="IsUsesFinancialInstruments" type="xsd:boolean" minOccurs="0" />
			<xsd:element name="IsIncludesSupportFromIYE" type="xsd:boolean" minOccurs="0" />
			<xsd:element name="IsSubjectToStateAidRegime" type="xsd:boolean" minOccurs="0" />
      <xsd:element name="IsAssignedDeMinimisAid" type="xsd:boolean" minOccurs="0" />
			<xsd:element name="IsIncludesPublicPrivatePartnership" type="xsd:boolean" minOccurs="0" />
			<xsd:element name="Description" type="xsd:string" minOccurs="0" />
      <xsd:element name="DescriptionEN" type="xsd:string" minOccurs="0" />
			<xsd:element name="Purpose" type="xsd:string" minOccurs="0" />
      <xsd:element name="AdditionalDescription" type="xsd:string" minOccurs="0" />
      <xsd:element name="ProcedureIdentifier" type="xsd:string" minOccurs="0" />  
		</xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="isLocked" type="xsd:boolean"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10003" 
	xmlns="http://ereg.egov.bg/segment/R-10003" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="Address">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Адрес</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Country" type="pun:PublicNomenclature" minOccurs="0" />
			<xsd:element name="Settlement" type="pun:PublicNomenclature" minOccurs="0" />
			<xsd:element name="PostCode" type="xsd:string" minOccurs="0" />
			<xsd:element name="Street" type="xsd:string" minOccurs="0" />
			<xsd:element name="FullAddress" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10004" 
	xmlns="http://ereg.egov.bg/segment/R-10004" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:a="http://ereg.egov.bg/segment/R-10003"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10003" />
	
	<xsd:complexType name="Company">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Организация</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="NameEN" type="xsd:string" minOccurs="0" />
			<xsd:element name="Uin" type="xsd:string" minOccurs="0" />
			<xsd:element name="UinType" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="CompanyType" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="CompanyLegalType" type="prn:PrivateNomenclature" minOccurs="0" />
      <xsd:element name="IsPrivateLegal" type="xsd:boolean" minOccurs="0" />
      <xsd:element name="CompanySizeType" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="KidCodeOrganization" type="pun:PublicNomenclature" minOccurs="0" />
      <xsd:element name="KidCodeProject" type="pun:PublicNomenclature" minOccurs="0" />
      <xsd:element name="FinancialContribution" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="Seat" type="a:Address" minOccurs="0" />
			<xsd:element name="Correspondence" type="a:Address" minOccurs="0" />
			<xsd:element name="Email" type="xsd:string" minOccurs="0" />
			<xsd:element name="Phone1" type="xsd:string" minOccurs="0" />
			<xsd:element name="Phone2" type="xsd:string" minOccurs="0" />
			<xsd:element name="Fax" type="xsd:string" minOccurs="0" />
			<xsd:element name="CompanyRepresentativePerson" type="xsd:string" minOccurs="0" />
			<xsd:element name="CompanyContactPerson" type="xsd:string" minOccurs="0" />
			<xsd:element name="CompanyContactPersonPhone" type="xsd:string" minOccurs="0" />
			<xsd:element name="CompanyContactPersonEmail" type="xsd:string" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="isLocked" type="xsd:boolean"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10005" 
	xmlns="http://ereg.egov.bg/segment/R-10005" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="InterventionCategoryDimensions">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Кодове по измерения</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="InterventionField" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
			<xsd:element name="FormOfFinance" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
			<xsd:element name="TerritorialDimension" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
			<xsd:element name="TerritorialDeliveryMechanism" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
			<xsd:element name="ThematicObjective" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
      <xsd:element name="ESFSecondaryTheme" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
      <xsd:element name="EconomicDimension" type="pun:PublicNomenclature" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10006" 
	xmlns="http://ereg.egov.bg/segment/R-10006" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="Contract">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Източници на финансиране</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="RequestedFundingAmount" type="xsd:decimal" minOccurs="0" />
			<xsd:element name="RequestedFundingPartOfCrossFinancingAmount" type="xsd:decimal" minOccurs="0" />
      
			<xsd:element name="CoFinancingBudgetAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="BudgetEIBAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="BudgetEBRDAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="BudgetWBAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="BudgetOtherMFIAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="BudgetOtherAmount" type="xsd:decimal" minOccurs="0" />

      <xsd:element name="CoFinancingNonBudgetAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="NonBudgetEIBAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="NonBudgetEBRDAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="NonBudgetWBAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="NonBudgetOtherMFIAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="NonBudgetOtherAmount" type="xsd:decimal" minOccurs="0" />
      
      <xsd:element name="TotalCoFinancingAmount" type="xsd:decimal" minOccurs="0" />
			<xsd:element name="TotalEligibleCostsPublicFunding" type="xsd:decimal" minOccurs="0" />
			<xsd:element name="TotalEligibleCosts" type="xsd:decimal" minOccurs="0" />
			<xsd:element name="RatioRequestedFundingTotalEligibleCosts" type="xsd:string" minOccurs="0" />
			
			<xsd:element name="ExpectedRevenue" type="xsd:decimal" minOccurs="0" />
      
			<xsd:element name="IneligibleCosts" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="IneligibleEIBAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="IneligibleEBRDAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="IneligibleWBAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="IneligibleOtherMFIAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="IneligibleOtherAmount" type="xsd:decimal" minOccurs="0" />
			
			<xsd:element name="TotalProjectCost" type="xsd:decimal" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10007" 
	xmlns="http://ereg.egov.bg/segment/R-10007" 
	
	xmlns:l="http://ereg.egov.bg/segment/R-09989"
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

  <xsd:import namespace="http://ereg.egov.bg/segment/R-09989" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="ProgrammeDetailsExpenseBudget">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Ред от бюджета на програма на трето ниво</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="OrderNum" type="xsd:string" minOccurs="0" />
      <xsd:element name="Nuts" type="l:Location" minOccurs="0" />
      <xsd:element name="InterventionFieldCode" type="xsd:string" minOccurs="0" />
      <xsd:element name="FormOfFinanceCode" type="xsd:string" minOccurs="0" />
      <xsd:element name="TerritorialDimensionCode" type="xsd:string" minOccurs="0" />
      <xsd:element name="TerritorialDeliveryMechanismCode" type="xsd:string" minOccurs="0" />
      <xsd:element name="ThematicObjectiveCode" type="xsd:string" minOccurs="0" />
      <xsd:element name="ESFSecondaryThemeCode" type="xsd:string" minOccurs="0" />
      <xsd:element name="EconomicDimensionCode" type="xsd:string" minOccurs="0" />
			<xsd:element name="GrandAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="SelfAmount" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="TotalAmount" type="xsd:decimal" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="gid" type="xsd:string"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10008" 
	xmlns="http://ereg.egov.bg/segment/R-10008" 
	
	xmlns:en="http://ereg.egov.bg/segment/R-09991"
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:pdeb="http://ereg.egov.bg/segment/R-10007"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-09991" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10007" />
	
	<xsd:complexType name="ProgrammeExpenseBudget">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Ред от бюджета на програма на второ ниво</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="OrderNum" type="xsd:string" minOccurs="0" />
      <xsd:element name="ProgrammePriorityCode" type="xsd:string" minOccurs="0" />
      <xsd:element name="IsEligibleCost" type="xsd:boolean" minOccurs="0" />
      <xsd:element name="FinanceSource" type="en:EnumNomenclature" minOccurs="0" />
      <xsd:element name="AidMode" type="en:EnumNomenclature" minOccurs="0" />
      <xsd:element name="ProgrammeDetailsExpenseBudget" type="pdeb:ProgrammeDetailsExpenseBudget" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>

    <xsd:attribute name="gid" type="xsd:string"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10009" 
	xmlns="http://ereg.egov.bg/segment/R-10009" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:peb="http://ereg.egov.bg/segment/R-10008"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10008" />
	
	<xsd:complexType name="ProgrammeBudget">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Бюджет към програма</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="OrderNum" type="xsd:string" minOccurs="0" />
			<xsd:element name="ProgrammeExpenseBudget" type="peb:ProgrammeExpenseBudget" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>

    <xsd:attribute name="gid" type="xsd:string"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10010" 
	xmlns="http://ereg.egov.bg/segment/R-10010" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:pb="http://ereg.egov.bg/segment/R-10009"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10009" />
	
	<xsd:complexType name="Budget">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Бюджет</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="ProgrammeBudget" type="pb:ProgrammeBudget" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10011" 
	xmlns="http://ereg.egov.bg/segment/R-10011" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="ContractActivity">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">План за изпълнение на Дейности на договор за БФП</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Code" type="xsd:string" minOccurs="0" />
			<xsd:element name="Company" type="prn:PrivateNomenclature" minOccurs="0" maxOccurs="unbounded" />
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
			<xsd:element name="ExecutionMethod" type="xsd:string" minOccurs="0" />
			<xsd:element name="Result" type="xsd:string" minOccurs="0" />
			<xsd:element name="StartMonth" type="xsd:string" minOccurs="0" />
			<xsd:element name="Duration" type="xsd:string" minOccurs="0" />
			<xsd:element name="Amount" type="xsd:decimal" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10012" 
	xmlns="http://ereg.egov.bg/segment/R-10012" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="ProjectPayPlan">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">План за плащане на проектно предложение</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
			<xsd:element name="StartDate" type="xsd:date" minOccurs="0" />
			<xsd:element name="EndDate" type="xsd:date" minOccurs="0" />
			<xsd:element name="Amount" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10013" 
	xmlns="http://ereg.egov.bg/segment/R-10013" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="Indicator">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Индикатор</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="Id" type="xsd:string" minOccurs="0" />
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
			<xsd:element name="ModeName" type="xsd:string" minOccurs="0" />
			<xsd:element name="TrendName" type="xsd:string" minOccurs="0" />
			<xsd:element name="TypeName" type="xsd:string" minOccurs="0" />
			<xsd:element name="MeasureName" type="xsd:string" minOccurs="0" />
      <xsd:element name="AggregatedReport" type="xsd:string" minOccurs="0" />
      <xsd:element name="AggregatedTarget" type="xsd:string" minOccurs="0" />
      
			<xsd:element name="BaseMen" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="BaseWomen" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="BaseTotal" type="xsd:decimal" minOccurs="0" />

      <xsd:element name="TargetMen" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="TargetWomen" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="TargetTotal" type="xsd:decimal" minOccurs="0" />
      
      <xsd:element name="HasGenderDivision" type="xsd:boolean" minOccurs="0" />

      <xsd:element name="Description" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10014" 
	xmlns="http://ereg.egov.bg/segment/R-10014" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:i="http://ereg.egov.bg/segment/R-10013"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10013" />
	
	<xsd:complexType name="ProgrammeIndicators">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Индикатори към програма</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Indicator" type="i:Indicator" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>
    
    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="isLocked" type="xsd:boolean"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10015" 
	xmlns="http://ereg.egov.bg/segment/R-10015" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="ContractTeam">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Екип на договор за БФП</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
			<xsd:element name="Position" type="xsd:string" minOccurs="0" />
			<xsd:element name="Responsibilities" type="xsd:string" minOccurs="0" />
			<xsd:element name="Phone" type="xsd:string" minOccurs="0" />
			<xsd:element name="Fax" type="xsd:string" minOccurs="0" />
			<xsd:element name="Email" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10016" 
	xmlns="http://ereg.egov.bg/segment/R-10016" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="ProjectErrand">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Външно възлагане на проектно предложение</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
			<xsd:element name="ErrandArea" type="prn:PrivateNomenclature" minOccurs="0" />
      <xsd:element name="ErrandLegalAct" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="ErrandType" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="Amount" type="xsd:decimal" minOccurs="0" />
			<xsd:element name="PlanDate" type="xsd:date" minOccurs="0" />
			<xsd:element name="Description" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10017" 
	xmlns="http://ereg.egov.bg/segment/R-10017" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
	
	<xsd:complexType name="ProjectSpecField">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Допълнителна информация необходима за оценка на проектното предложение</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="Id" type="xsd:string" minOccurs="0" />
			<xsd:element name="Title" type="xsd:string" minOccurs="0" />
			<xsd:element name="Description" type="xsd:string" minOccurs="0" />
      <xsd:element name="Value" type="xsd:string" minOccurs="0" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10018" 
	xmlns="http://ereg.egov.bg/segment/R-10018" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
            
	xmlns:adc="http://ereg.egov.bg/segment/R-09992"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
	<xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />

  <xsd:import namespace="http://ereg.egov.bg/segment/R-09992" />
	
	<xsd:complexType name="AttachedDocument">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Прикачен документ</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Type" type="prn:PrivateNomenclature" minOccurs="0" />
			<xsd:element name="Description" type="xsd:string" minOccurs="0" />

      <xsd:element name="AttachedDocumentContent" type="adc:AttachedDocumentContent" minOccurs="0" />
      <xsd:element name="SignatureContent" type="adc:AttachedDocumentContent" minOccurs="0" maxOccurs="unbounded" />
		</xsd:sequence>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10019"
	xmlns="http://ereg.egov.bg/segment/R-10019"
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:pun="http://ereg.egov.bg/segment/R-10001"
	xmlns:pbd="http://ereg.egov.bg/segment/R-10002"
	xmlns:com="http://ereg.egov.bg/segment/R-10004"
	xmlns:dbc="http://ereg.egov.bg/segment/R-09998"
	xmlns:ppp="http://ereg.egov.bg/segment/R-10012"
	xmlns:pi="http://ereg.egov.bg/segment/R-10014"
	xmlns:ct="http://ereg.egov.bg/segment/R-10015"
	xmlns:pe="http://ereg.egov.bg/segment/R-10016"
	xmlns:psf="http://ereg.egov.bg/segment/R-10017"
	xmlns:ad="http://ereg.egov.bg/segment/R-10018"
	xmlns:pca="http://ereg.egov.bg/segment/R-09995"
	xmlns:pad="http://ereg.egov.bg/segment/R-09994"
            
  xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema"
	elementFormDefault="qualified">

  <xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10001" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10002" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10004" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-09998" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10012" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10014" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10015" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10016" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10017" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10018" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-09995" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-09994" />

  <xsd:import namespace="http://www.w3.org/2000/09/xmldsig#" schemaLocation="http://www.w3.org/TR/2002/REC-xmldsig-core-20020212/xmldsig-core-schema.xsd" />

  <xsd:complexType name="Project">
    <xsd:annotation>
      <xsd:documentation xml:lang="bg">Проектно предложение</xsd:documentation>
    </xsd:annotation>
    <xsd:sequence>
      
      <xsd:element name="ProjectBasicData" type="pbd:ProjectBasicData" minOccurs="0" />
      
      <xsd:element name="Candidate" type="com:Company" minOccurs="0" />
        
      <!--<xsd:element name="Candidate" minOccurs="0" >
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="Company" type="com:Company" minOccurs="0" maxOccurs="1" />
          </xsd:sequence>
          <xsd:attribute name="id" type="xsd:string"/>
          <xsd:attribute name="isLocked" type="xsd:boolean"/>
        </xsd:complexType>
      </xsd:element>-->

      <xsd:element name="Partners" minOccurs="0">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="Partner" type="com:Company" minOccurs="0" maxOccurs="unbounded" />
          </xsd:sequence>
          <xsd:attribute name="id" type="xsd:string"/>
          <xsd:attribute name="isLocked" type="xsd:boolean"/>
        </xsd:complexType>
      </xsd:element>

      <xsd:element name="DimensionsBudgetContract" type="dbc:DimensionsBudgetContract" minOccurs="0" maxOccurs="unbounded" />

      <xsd:element name="ProgrammeContractActivities" type="pca:ProgrammeContractActivities" minOccurs="0" maxOccurs="unbounded" />

      <xsd:element name="ProgrammeIndicators" type="pi:ProgrammeIndicators" minOccurs="0" maxOccurs="unbounded" />

      <xsd:element name="ContractTeams" minOccurs="0">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="ContractTeam" type="ct:ContractTeam" minOccurs="0" maxOccurs="unbounded" />
          </xsd:sequence>
          <xsd:attribute name="id" type="xsd:string"/>
          <xsd:attribute name="isLocked" type="xsd:boolean"/>
        </xsd:complexType>
      </xsd:element>

      <xsd:element name="ProjectErrands" minOccurs="0">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="ProjectErrand" type="pe:ProjectErrand" minOccurs="0" maxOccurs="unbounded" />
          </xsd:sequence>
          <xsd:attribute name="id" type="xsd:string"/>
          <xsd:attribute name="isLocked" type="xsd:boolean"/>
        </xsd:complexType>
      </xsd:element>

      <xsd:element name="ProjectSpecFields" minOccurs="0">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="ProjectSpecField" type="psf:ProjectSpecField" minOccurs="0" maxOccurs="unbounded" />
          </xsd:sequence>
          <xsd:attribute name="id" type="xsd:string"/>
          <xsd:attribute name="isLocked" type="xsd:boolean"/>
        </xsd:complexType>
      </xsd:element>

      <xsd:element name="AttachedDocuments" minOccurs="0">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="AttachedDocument" type="ad:AttachedDocument" minOccurs="0" maxOccurs="unbounded" />
          </xsd:sequence>
          <xsd:attribute name="id" type="xsd:string"/>
          <xsd:attribute name="isLocked" type="xsd:boolean"/>
        </xsd:complexType>
      </xsd:element>

      <xsd:element name="PaperAttachedDocuments" minOccurs="0">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="PaperAttachedDocument" type="pad:PaperAttachedDocument" minOccurs="0" maxOccurs="unbounded" />
          </xsd:sequence>
          <xsd:attribute name="id" type="xsd:string"/>
          <xsd:attribute name="isLocked" type="xsd:boolean"/>
        </xsd:complexType>
      </xsd:element>

      <xsd:element ref="ds:Signature" minOccurs="0" />

    </xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="isLocked" type="xsd:boolean"/>
    <xsd:attribute name="version" type="xsd:string"/>
  
    <xsd:attribute name="createDate" type="xsd:dateTime"/>
    <xsd:attribute name="modificationDate" type="xsd:dateTime"/>
  </xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10020" 
	xmlns="http://ereg.egov.bg/segment/R-10020" 
	
	xmlns:p="http://ereg.egov.bg/segment/R-10019"
	xmlns:mtn="http://ereg.egov.bg/nomenclature/R-09990"
  xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10019" />
	<xsd:import namespace="http://ereg.egov.bg/nomenclature/R-09990" />
  <xsd:import namespace="http://www.w3.org/2000/09/xmldsig#" schemaLocation="http://www.w3.org/TR/2002/REC-xmldsig-core-20020212/xmldsig-core-schema.xsd" />
	
	<xsd:complexType name="Message">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Съобщение</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Content" type="xsd:string" minOccurs="0" />
			<xsd:element name="Project" type="p:Project" minOccurs="0" />
			<xsd:element name="Reply" type="xsd:string" minOccurs="0" />
    
      <xsd:element ref="ds:Signature" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="version" type="xsd:string"/>
    <xsd:attribute name="type" type="mtn:MessageTypeNomenclature"/>
  
    <xsd:attribute name="createDate" type="xsd:dateTime"/>
    <xsd:attribute name="modificationDate" type="xsd:dateTime"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10021" 
	xmlns="http://ereg.egov.bg/segment/R-10021" 
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:complexType name="EvalTableCriteria">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Критерий за оценка в оценителна таблица</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="IsRejection" type="xsd:boolean" minOccurs="0" />
      <xsd:element name="Weight" type="xsd:decimal" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="gid" type="xsd:string"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10022" 
	xmlns="http://ereg.egov.bg/segment/R-10022" 
	
	xmlns:etc="http://ereg.egov.bg/segment/R-10021"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10021" />
	
	<xsd:complexType name="EvalTableGroup">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Група в оценителна таблица</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="EvalTableCriteria" type="etc:EvalTableCriteria" minOccurs="0" maxOccurs="unbounded" />

			<xsd:element name="Limit" type="xsd:decimal" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="gid" type="xsd:string"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10023" 
	xmlns="http://ereg.egov.bg/segment/R-10023" 
	
	xmlns:etg="http://ereg.egov.bg/segment/R-10022"
	xmlns:ad="http://ereg.egov.bg/segment/R-10018"
            
  xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
            
	xmlns:ettn="http://ereg.egov.bg/nomenclature/R-09993"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10022" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10018" />
  
  <xsd:import namespace="http://www.w3.org/2000/09/xmldsig#" schemaLocation="http://www.w3.org/TR/2002/REC-xmldsig-core-20020212/xmldsig-core-schema.xsd" />
  
  <xsd:import namespace="http://ereg.egov.bg/nomenclature/R-09993" />
	
	<xsd:complexType name="EvalTable">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Оценителна таблица</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="EvalTableGroup" type="etg:EvalTableGroup" minOccurs="0" maxOccurs="unbounded" />
      <xsd:element name="AttachedDocument" type="ad:AttachedDocument" minOccurs="0" maxOccurs="unbounded" />

			<xsd:element name="Limit" type="xsd:decimal" minOccurs="0" />
    
      <xsd:element ref="ds:Signature" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="version" type="xsd:string"/>
    <xsd:attribute name="type" type="ettn:EvalTypeNomenclature"/>
  
    <xsd:attribute name="createDate" type="xsd:dateTime"/>
    <xsd:attribute name="modificationDate" type="xsd:dateTime"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10024" 
	xmlns="http://ereg.egov.bg/segment/R-10024" 
	
	xmlns:prn="http://ereg.egov.bg/segment/R-10000"
	xmlns:etc="http://ereg.egov.bg/segment/R-10021"
            
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

  <xsd:import namespace="http://ereg.egov.bg/segment/R-10000" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10021" />

  <xsd:complexType name="EvalSheetCriteria">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Критерий за оценка в оценителен лист</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="EvalTableCriteria" type="etc:EvalTableCriteria" minOccurs="0" />
			<xsd:element name="Accept" type="prn:PrivateNomenclature" minOccurs="0" />
      <xsd:element name="Evaluation" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="Note" type="xsd:string" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="gid" type="xsd:string"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10025" 
	xmlns="http://ereg.egov.bg/segment/R-10025" 
	
	xmlns:eсc="http://ereg.egov.bg/segment/R-10024"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10024" />
	
	<xsd:complexType name="EvalSheetGroup">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Група в оценителен лист</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Name" type="xsd:string" minOccurs="0" />
      <xsd:element name="EvalSheetCriteria" type="eсc:EvalSheetCriteria" minOccurs="0" maxOccurs="unbounded" />

      <xsd:element name="Total" type="xsd:decimal" minOccurs="0" />
      <xsd:element name="Limit" type="xsd:decimal" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="gid" type="xsd:string"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10026" 
	xmlns="http://ereg.egov.bg/segment/R-10026" 
	
	xmlns:esg="http://ereg.egov.bg/segment/R-10025"
	xmlns:ad="http://ereg.egov.bg/segment/R-10018"
            
  xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
	
	xmlns:ettn="http://ereg.egov.bg/nomenclature/R-09993"
	
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

	<xsd:import namespace="http://ereg.egov.bg/segment/R-10025" />
  <xsd:import namespace="http://ereg.egov.bg/segment/R-10018" />
  
  <xsd:import namespace="http://www.w3.org/2000/09/xmldsig#" schemaLocation="http://www.w3.org/TR/2002/REC-xmldsig-core-20020212/xmldsig-core-schema.xsd" />
  
  <xsd:import namespace="http://ereg.egov.bg/nomenclature/R-09993" />
	
	<xsd:complexType name="EvalSheet">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Оценителен лист</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
      <xsd:element name="EvalSheetGroup" type="esg:EvalSheetGroup" minOccurs="0" maxOccurs="unbounded" />
      <xsd:element name="EvalTableAttachedDocument" type="ad:AttachedDocument" minOccurs="0" maxOccurs="unbounded" />
      <xsd:element name="AttachedDocument" type="ad:AttachedDocument" minOccurs="0" maxOccurs="unbounded" />

			<xsd:element name="Limit" type="xsd:decimal" minOccurs="0" />
			<xsd:element name="Total" type="xsd:decimal" minOccurs="0" />
      
			<xsd:element name="IsSuccess" type="xsd:boolean" minOccurs="0" />
			<xsd:element name="IsManual" type="xsd:boolean" minOccurs="0" />
			<xsd:element name="ReasonManual" type="xsd:string" minOccurs="0" />

			<xsd:element name="Note" type="xsd:string" minOccurs="0" />
    
      <xsd:element ref="ds:Signature" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="version" type="xsd:string"/>
    <xsd:attribute name="type" type="ettn:EvalTypeNomenclature"/>
  
    <xsd:attribute name="createDate" type="xsd:dateTime"/>
    <xsd:attribute name="modificationDate" type="xsd:dateTime"/>
	</xsd:complexType>
</xsd:schema>

<xsd:schema targetNamespace="http://ereg.egov.bg/segment/R-10027" 
	xmlns="http://ereg.egov.bg/segment/R-10027" 
	xmlns:ad="http://ereg.egov.bg/segment/R-10018"
  xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
	xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
	elementFormDefault="qualified">

  <xsd:import namespace="http://ereg.egov.bg/segment/R-10018" />
  <xsd:import namespace="http://www.w3.org/2000/09/xmldsig#" schemaLocation="http://www.w3.org/TR/2002/REC-xmldsig-core-20020212/xmldsig-core-schema.xsd" />
  
	<xsd:complexType name="Standpoint">
		<xsd:annotation>
			<xsd:documentation xml:lang="bg">Становище</xsd:documentation>
		</xsd:annotation>
		<xsd:sequence>
			<xsd:element name="Subject" type="xsd:string" minOccurs="0" />
			<xsd:element name="Content" type="xsd:string" minOccurs="0" />
      
      <xsd:element name="AttachedDocument" type="ad:AttachedDocument" minOccurs="0" maxOccurs="unbounded" />
    
      <xsd:element ref="ds:Signature" minOccurs="0" />
		</xsd:sequence>

    <xsd:attribute name="id" type="xsd:string"/>
    <xsd:attribute name="version" type="xsd:string"/>
  
    <xsd:attribute name="createDate" type="xsd:dateTime"/>
    <xsd:attribute name="modificationDate" type="xsd:dateTime"/>
	</xsd:complexType>
</xsd:schema>

<!-- Schema for XML Signatures
    http://www.w3.org/2000/09/xmldsig#
    $Revision: 1.1 $ on $Date: 2002/02/08 20:32:26 $ by $Author: reagle $

    Copyright 2001 The Internet Society and W3C (Massachusetts Institute
    of Technology, Institut National de Recherche en Informatique et en
    Automatique, Keio University). All Rights Reserved.
    http://www.w3.org/Consortium/Legal/

    This document is governed by the W3C Software License [1] as described
    in the FAQ [2].

    [1] http://www.w3.org/Consortium/Legal/copyright-software-19980720
    [2] http://www.w3.org/Consortium/Legal/IPR-FAQ-20000620.html#DTD
-->

<schema xmlns="http://www.w3.org/2001/XMLSchema"
        xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
        targetNamespace="http://www.w3.org/2000/09/xmldsig#"
        version="0.1" elementFormDefault="qualified"> 

<!-- Basic Types Defined for Signatures -->

<simpleType name="CryptoBinary">
  <restriction base="base64Binary">
  </restriction>
</simpleType>

<!-- Start Signature -->

<element name="Signature" type="ds:SignatureType"/>
<complexType name="SignatureType">
  <sequence> 
    <element ref="ds:SignedInfo"/> 
    <element ref="ds:SignatureValue"/> 
    <element ref="ds:KeyInfo" minOccurs="0"/> 
    <element ref="ds:Object" minOccurs="0" maxOccurs="unbounded"/> 
  </sequence>  
  <attribute name="Id" type="ID" use="optional"/>
</complexType>

  <element name="SignatureValue" type="ds:SignatureValueType"/> 
  <complexType name="SignatureValueType">
    <simpleContent>
      <extension base="base64Binary">
        <attribute name="Id" type="ID" use="optional"/>
      </extension>
    </simpleContent>
  </complexType>

<!-- Start SignedInfo -->

<element name="SignedInfo" type="ds:SignedInfoType"/>
<complexType name="SignedInfoType">
  <sequence> 
    <element ref="ds:CanonicalizationMethod"/> 
    <element ref="ds:SignatureMethod"/> 
    <element ref="ds:Reference" maxOccurs="unbounded"/> 
  </sequence>  
  <attribute name="Id" type="ID" use="optional"/> 
</complexType>

  <element name="CanonicalizationMethod" type="ds:CanonicalizationMethodType"/> 
  <complexType name="CanonicalizationMethodType" mixed="true">
    <sequence>
      <any namespace="##any" minOccurs="0" maxOccurs="unbounded"/>
      <!-- (0,unbounded) elements from (1,1) namespace -->
    </sequence>
    <attribute name="Algorithm" type="anyURI" use="required"/> 
  </complexType>

  <element name="SignatureMethod" type="ds:SignatureMethodType"/>
  <complexType name="SignatureMethodType" mixed="true">
    <sequence>
      <element name="HMACOutputLength" minOccurs="0" type="ds:HMACOutputLengthType"/>
      <any namespace="##other" minOccurs="0" maxOccurs="unbounded"/>
      <!-- (0,unbounded) elements from (1,1) external namespace -->
    </sequence>
    <attribute name="Algorithm" type="anyURI" use="required"/> 
  </complexType>

<!-- Start Reference -->

<element name="Reference" type="ds:ReferenceType"/>
<complexType name="ReferenceType">
  <sequence> 
    <element ref="ds:Transforms" minOccurs="0"/> 
    <element ref="ds:DigestMethod"/> 
    <element ref="ds:DigestValue"/> 
  </sequence>
  <attribute name="Id" type="ID" use="optional"/> 
  <attribute name="URI" type="anyURI" use="optional"/> 
  <attribute name="Type" type="anyURI" use="optional"/> 
</complexType>

  <element name="Transforms" type="ds:TransformsType"/>
  <complexType name="TransformsType">
    <sequence>
      <element ref="ds:Transform" maxOccurs="unbounded"/>  
    </sequence>
  </complexType>

  <element name="Transform" type="ds:TransformType"/>
  <complexType name="TransformType" mixed="true">
    <choice minOccurs="0" maxOccurs="unbounded"> 
      <any namespace="##other" processContents="lax"/>
      <!-- (1,1) elements from (0,unbounded) namespaces -->
      <element name="XPath" type="string"/> 
    </choice>
    <attribute name="Algorithm" type="anyURI" use="required"/> 
  </complexType>

<!-- End Reference -->

<element name="DigestMethod" type="ds:DigestMethodType"/>
<complexType name="DigestMethodType" mixed="true"> 
  <sequence>
    <any namespace="##other" processContents="lax" minOccurs="0" maxOccurs="unbounded"/>
  </sequence>    
  <attribute name="Algorithm" type="anyURI" use="required"/> 
</complexType>

<element name="DigestValue" type="ds:DigestValueType"/>
<simpleType name="DigestValueType">
  <restriction base="base64Binary"/>
</simpleType>

<!-- End SignedInfo -->

<!-- Start KeyInfo -->

<element name="KeyInfo" type="ds:KeyInfoType"/> 
<complexType name="KeyInfoType" mixed="true">
  <choice maxOccurs="unbounded">     
    <element ref="ds:KeyName"/> 
    <element ref="ds:KeyValue"/> 
    <element ref="ds:RetrievalMethod"/> 
    <element ref="ds:X509Data"/> 
    <element ref="ds:PGPData"/> 
    <element ref="ds:SPKIData"/>
    <element ref="ds:MgmtData"/>
    <any processContents="lax" namespace="##other"/>
    <!-- (1,1) elements from (0,unbounded) namespaces -->
  </choice>
  <attribute name="Id" type="ID" use="optional"/> 
</complexType>

  <element name="KeyName" type="string"/>
  <element name="MgmtData" type="string"/>

  <element name="KeyValue" type="ds:KeyValueType"/> 
  <complexType name="KeyValueType" mixed="true">
   <choice>
     <element ref="ds:DSAKeyValue"/>
     <element ref="ds:RSAKeyValue"/>
     <any namespace="##other" processContents="lax"/>
   </choice>
  </complexType>

  <element name="RetrievalMethod" type="ds:RetrievalMethodType"/> 
  <complexType name="RetrievalMethodType">
    <sequence>
      <element ref="ds:Transforms" minOccurs="0"/> 
    </sequence>  
    <attribute name="URI" type="anyURI"/>
    <attribute name="Type" type="anyURI" use="optional"/>
  </complexType>

<!-- Start X509Data -->

<element name="X509Data" type="ds:X509DataType"/> 
<complexType name="X509DataType">
  <sequence maxOccurs="unbounded">
    <choice>
      <element name="X509IssuerSerial" type="ds:X509IssuerSerialType"/>
      <element name="X509SKI" type="base64Binary"/>
      <element name="X509SubjectName" type="string"/>
      <element name="X509Certificate" type="base64Binary"/>
      <element name="X509CRL" type="base64Binary"/>
      <any namespace="##other" processContents="lax"/>
    </choice>
  </sequence>
</complexType>

<complexType name="X509IssuerSerialType"> 
  <sequence> 
    <element name="X509IssuerName" type="string"/> 
    <element name="X509SerialNumber" type="integer"/> 
  </sequence>
</complexType>

<!-- End X509Data -->

<!-- Begin PGPData -->

<element name="PGPData" type="ds:PGPDataType"/> 
<complexType name="PGPDataType"> 
  <choice>
    <sequence>
      <element name="PGPKeyID" type="base64Binary"/> 
      <element name="PGPKeyPacket" type="base64Binary" minOccurs="0"/> 
      <any namespace="##other" processContents="lax" minOccurs="0"
       maxOccurs="unbounded"/>
    </sequence>
    <!--<sequence>
      <element name="PGPKeyPacket" type="base64Binary"/> 
      <any namespace="##other" processContents="lax" minOccurs="0"
       maxOccurs="unbounded"/>
    </sequence>-->
  </choice>
</complexType>

<!-- End PGPData -->

<!-- Begin SPKIData -->

<element name="SPKIData" type="ds:SPKIDataType"/> 
<complexType name="SPKIDataType">
  <sequence maxOccurs="unbounded">
    <element name="SPKISexp" type="base64Binary"/>
    <any namespace="##other" processContents="lax" minOccurs="0"/>
  </sequence>
</complexType> 

<!-- End SPKIData -->

<!-- End KeyInfo -->

<!-- Start Object (Manifest, SignatureProperty) -->

<element name="Object" type="ds:ObjectType"/> 
<complexType name="ObjectType" mixed="true">
  <sequence minOccurs="0" maxOccurs="unbounded">
    <any namespace="##any" processContents="lax"/>
  </sequence>
  <attribute name="Id" type="ID" use="optional"/> 
  <attribute name="MimeType" type="string" use="optional"/> <!-- add a grep facet -->
  <attribute name="Encoding" type="anyURI" use="optional"/> 
</complexType>

<element name="Manifest" type="ds:ManifestType"/> 
<complexType name="ManifestType">
  <sequence>
    <element ref="ds:Reference" maxOccurs="unbounded"/> 
  </sequence>
  <attribute name="Id" type="ID" use="optional"/> 
</complexType>

<element name="SignatureProperties" type="ds:SignaturePropertiesType"/> 
<complexType name="SignaturePropertiesType">
  <sequence>
    <element ref="ds:SignatureProperty" maxOccurs="unbounded"/> 
  </sequence>
  <attribute name="Id" type="ID" use="optional"/> 
</complexType>

   <element name="SignatureProperty" type="ds:SignaturePropertyType"/> 
   <complexType name="SignaturePropertyType" mixed="true">
     <choice maxOccurs="unbounded">
       <any namespace="##other" processContents="lax"/>
       <!-- (1,1) elements from (1,unbounded) namespaces -->
     </choice>
     <attribute name="Target" type="anyURI" use="required"/> 
     <attribute name="Id" type="ID" use="optional"/> 
   </complexType>

<!-- End Object (Manifest, SignatureProperty) -->

<!-- Start Algorithm Parameters -->

<simpleType name="HMACOutputLengthType">
  <restriction base="integer"/>
</simpleType>

<!-- Start KeyValue Element-types -->

<element name="DSAKeyValue" type="ds:DSAKeyValueType"/>
<complexType name="DSAKeyValueType">
  <sequence>
    <sequence minOccurs="0">
      <element name="P" type="ds:CryptoBinary"/>
      <element name="Q" type="ds:CryptoBinary"/>
    </sequence>
    <element name="G" type="ds:CryptoBinary" minOccurs="0"/>
    <element name="Y" type="ds:CryptoBinary"/>
    <element name="J" type="ds:CryptoBinary" minOccurs="0"/>
    <sequence minOccurs="0">
      <element name="Seed" type="ds:CryptoBinary"/>
      <element name="PgenCounter" type="ds:CryptoBinary"/>
    </sequence>
  </sequence>
</complexType>

<element name="RSAKeyValue" type="ds:RSAKeyValueType"/>
<complexType name="RSAKeyValueType">
  <sequence>
    <element name="Modulus" type="ds:CryptoBinary"/> 
    <element name="Exponent" type="ds:CryptoBinary"/> 
  </sequence>
</complexType> 

<!-- End KeyValue Element-types -->

<!-- End Signature -->

</schema>

'
--------------------------------------------ALTER TABLE [dbo].[RegProjectXmls] ALTER COLUMN [Xml] XML (DOCUMENT [dbo].[RioSchemaCollection])ALTER TABLE [dbo].[ProjectXmls] ALTER COLUMN [Xml] XML (DOCUMENT [dbo].[RioSchemaCollection])ALTER TABLE [dbo].[ProcedureEvalTableXmls] ALTER COLUMN [Xml] XML (DOCUMENT [dbo].[RioSchemaCollection])