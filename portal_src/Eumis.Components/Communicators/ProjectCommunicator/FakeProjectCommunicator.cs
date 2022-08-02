using System.Collections.Generic;
using System.Linq;
using Eumis.Documents.Contracts;
using System;

namespace Eumis.Components.Communicators
{
    public class FakeProjectCommunicator : IProjectCommunicator
    {
        public ContractProject GetProject(Guid gid, string token)
        {
            return new ContractProject
            {
                xml = FakeProject.Xml.Trim(),
                isDraft = false,
                regData = new ContractProjectHeader { regNumber = "BG-1234567890-000-9999", regDate = DateTime.Now },
                version = new byte[1]
            };
        }

        public ContractProject PutProject(Guid gid, string token, string xml, byte[] version)
        {
            throw new NotImplementedException();
        }

        public void SubmitProject(Guid gid, string token, string xml, byte[] version)
        {
            throw new NotImplementedException();
        }

        public List<ContractValidationError> ValidateDraft(string xml, string accessToken)
        {
            return new List<ContractValidationError>()
            {
                new ContractValidationError()
                {
                    error = "error 1",
                    isRequired = true
                },

                new ContractValidationError()
                {
                    error = "error 2",
                    isRequired = false
                }
            };
        }

        public void ResurrectFiles(string xml)
        {
            throw new NotImplementedException();
        }

        public byte[] GetProjectFilesZip(string projectGid, string accessToken)
        {
            return new byte[10];
        }
    }

    public static class FakeProject
    {
        public static readonly string Xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
	<ProjectBasicData id=""73b2fbc3-4e7b-4010-8844-9af5e558d065"" isLocked=""false"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<ProgrammeBasicData xmlns=""http://ereg.egov.bg/segment/R-10002"">
			<Programme xmlns=""http://ereg.egov.bg/segment/R-09997"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">2014BG05M9OP001</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">Развитие на човешките ресурси</Name>
			</Programme>
			<ProgrammePriority xmlns=""http://ereg.egov.bg/segment/R-09997"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">2014BG05M9OP001-1</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">Подобряване достъпа до заетост и качеството на работните места</Name>
			</ProgrammePriority>
		</ProgrammeBasicData>
		<Procedure xmlns=""http://ereg.egov.bg/segment/R-10002"">
			<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">2014BG05M9OP001-1.2014.001</Code>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">Младежка заетост</Name>
		</Procedure>
		<Name xmlns=""http://ereg.egov.bg/segment/R-10002"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor.</Name>
		<Duration xmlns=""http://ereg.egov.bg/segment/R-10002"">12</Duration>
		<NutsAddress xmlns=""http://ereg.egov.bg/segment/R-10002"">
			<NutsLevel xmlns=""http://ereg.egov.bg/segment/R-09999"">
				<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">settlement</Id>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Населено място</Name>
			</NutsLevel>
			<NutsAddressContent xmlns=""http://ereg.egov.bg/segment/R-09999"">
				<Settlement>
					<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">14475</Code>
					<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гара Лакатник</Name>
				</Settlement>
			</NutsAddressContent>
			<NutsAddressContent xmlns=""http://ereg.egov.bg/segment/R-09999"">
				<Settlement>
					<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">14461</Code>
					<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гара Бов</Name>
				</Settlement>
			</NutsAddressContent>
			<NutsAddressContent xmlns=""http://ereg.egov.bg/segment/R-09999"">
				<Settlement>
					<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">14489</Code>
					<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гара Орешец</Name>
				</Settlement>
			</NutsAddressContent>
			<NutsAddressContent xmlns=""http://ereg.egov.bg/segment/R-09999"">
				<Settlement>
					<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">00182</Code>
					<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гр.Аксаково</Name>
				</Settlement>
			</NutsAddressContent>
			<NutsAddressContent xmlns=""http://ereg.egov.bg/segment/R-09999"">
				<Settlement>
					<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">00518</Code>
					<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гр.Антоново</Name>
				</Settlement>
			</NutsAddressContent>
			<NutsAddressContent xmlns=""http://ereg.egov.bg/segment/R-09999"">
				<Settlement>
					<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">02659</Code>
					<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гр.Банкя</Name>
				</Settlement>
			</NutsAddressContent>
		</NutsAddress>
		<IsVatEligible xmlns=""http://ereg.egov.bg/segment/R-10002"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">2</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Друго</Name>
		</IsVatEligible>
		<AmountType xmlns=""http://ereg.egov.bg/segment/R-10002"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">0</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Проектът е голям проект съгласно чл. 100 от Регламент (ЕС) № 1303/ 2013 г.</Name>
		</AmountType>
		<IsJointActionPlan xmlns=""http://ereg.egov.bg/segment/R-10002"">true</IsJointActionPlan>
		<IsUsesFinancialInstruments xmlns=""http://ereg.egov.bg/segment/R-10002"">true</IsUsesFinancialInstruments>
		<IsIncludesSupportFromIYE xmlns=""http://ereg.egov.bg/segment/R-10002"">true</IsIncludesSupportFromIYE>
		<IsSubjectToStateAidRegime xmlns=""http://ereg.egov.bg/segment/R-10002"">true</IsSubjectToStateAidRegime>
		<IsAssignedDeMinimisAid xmlns=""http://ereg.egov.bg/segment/R-10002"">true</IsAssignedDeMinimisAid>
		<IsIncludesPublicPrivatePartnership xmlns=""http://ereg.egov.bg/segment/R-10002"">true</IsIncludesPublicPrivatePartnership>
		<Description xmlns=""http://ereg.egov.bg/segment/R-10002"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</Description>
		<Purpose xmlns=""http://ereg.egov.bg/segment/R-10002"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</Purpose>
		<AdditionalDescription xmlns=""http://ereg.egov.bg/segment/R-10002"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum.</AdditionalDescription>
		<ProcedureIdentifier xmlns=""http://ereg.egov.bg/segment/R-10002"">8cf3e381-428c-435d-9d44-f9daffe25751</ProcedureIdentifier>
	</ProjectBasicData>
	<Candidate id=""28757778-b16b-4f49-bb2d-4c4724e11f83"" isLocked=""false"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<Name xmlns=""http://ereg.egov.bg/segment/R-10004"">Кандидат 1</Name>
		<Uin xmlns=""http://ereg.egov.bg/segment/R-10004"">12345678</Uin>
		<UinType xmlns=""http://ereg.egov.bg/segment/R-10004"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">bulstat</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Булстат</Name>
		</UinType>
		<CompanyType xmlns=""http://ereg.egov.bg/segment/R-10004"" />
		<CompanyLegalType xmlns=""http://ereg.egov.bg/segment/R-10004"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">2da6a748-fb37-45aa-ba25-0313f3752ec6</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Акционерно дружество АД</Name>
		</CompanyLegalType>
		<CompanySizeType xmlns=""http://ereg.egov.bg/segment/R-10004"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">ffc36285-b8b3-4b94-9fa7-dbf97d92a8c4</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Средно</Name>
		</CompanySizeType>
		<KidCodeOrganization xmlns=""http://ereg.egov.bg/segment/R-10004"">
			<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">N</Code>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">N АДМИНИСТРАТИВНИ И СПОМАГАТЕЛНИ ДЕЙНОСТИ</Name>
		</KidCodeOrganization>
		<KidCodeProject xmlns=""http://ereg.egov.bg/segment/R-10004"">
			<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">82</Code>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">82 Административни офис дейности и друго спомагателно обслужване на стопанската дейност</Name>
		</KidCodeProject>
		<Seat xmlns=""http://ereg.egov.bg/segment/R-10004"">
			<Country xmlns=""http://ereg.egov.bg/segment/R-10003"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">BG</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">България</Name>
			</Country>
			<Settlement xmlns=""http://ereg.egov.bg/segment/R-10003"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">68134</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гр.София</Name>
			</Settlement>
			<PostCode xmlns=""http://ereg.egov.bg/segment/R-10003"">1000</PostCode>
			<Street xmlns=""http://ereg.egov.bg/segment/R-10003"">бул. Тодор Александров №15</Street>
		</Seat>
		<Correspondence xmlns=""http://ereg.egov.bg/segment/R-10004"">
			<Country xmlns=""http://ereg.egov.bg/segment/R-10003"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">BG</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">България</Name>
			</Country>
			<Settlement xmlns=""http://ereg.egov.bg/segment/R-10003"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">68134</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">гр.София</Name>
			</Settlement>
			<PostCode xmlns=""http://ereg.egov.bg/segment/R-10003"">1000</PostCode>
			<Street xmlns=""http://ereg.egov.bg/segment/R-10003"">ул. Хан Аспарух №41</Street>
		</Correspondence>
		<Email xmlns=""http://ereg.egov.bg/segment/R-10004"">email@example.com</Email>
		<Phone1 xmlns=""http://ereg.egov.bg/segment/R-10004"">+359888 888 888</Phone1>
		<Phone2 xmlns=""http://ereg.egov.bg/segment/R-10004"">+35928 88 88 88</Phone2>
		<Fax xmlns=""http://ereg.egov.bg/segment/R-10004"">987654321</Fax>
		<CompanyRepresentativePerson xmlns=""http://ereg.egov.bg/segment/R-10004"">asdf asgfds dfs sdf gsdf dsf g</CompanyRepresentativePerson>
		<CompanyContactPerson xmlns=""http://ereg.egov.bg/segment/R-10004"">Иван Иванов</CompanyContactPerson>
		<CompanyContactPersonPhone xmlns=""http://ereg.egov.bg/segment/R-10004"">0888 888 888</CompanyContactPersonPhone>
		<CompanyContactPersonEmail xmlns=""http://ereg.egov.bg/segment/R-10004"">i.ivanov@example.com</CompanyContactPersonEmail>
	</Candidate>
	<Partners id=""94e02fc8-4569-4bea-9f69-382d11953e10"" xmlns=""http://ereg.egov.bg/segment/R-10019"" />
	<DimensionsBudgetContract id=""ccd79cc6-4b94-4621-9d76-7ac09931a37e"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<InterventionCategoryDimensions xmlns=""http://ereg.egov.bg/segment/R-09998"">
			<InterventionField xmlns=""http://ereg.egov.bg/segment/R-10005"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">103</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">Устойчиво интегриране на пазара на труда на младите хора, в частност тези, които не са ангажирани с трудова дейност, образование или обучение, включително младите хора, изложени на риск от социално изключване, и младите хора от маргинализирани</Name>
			</InterventionField>
			<FormOfFinance xmlns=""http://ereg.egov.bg/segment/R-10005"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">01</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">Безвъзмездни средства</Name>
			</FormOfFinance>
			<TerritorialDimension xmlns=""http://ereg.egov.bg/segment/R-10005"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">07</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">Не се прилага</Name>
			</TerritorialDimension>
			<TerritorialDeliveryMechanism xmlns=""http://ereg.egov.bg/segment/R-10005"">
				<Code xmlns=""http://ereg.egov.bg/segment/R-10001"">07</Code>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10001"">Не се прилага</Name>
			</TerritorialDeliveryMechanism>
		</InterventionCategoryDimensions>
		<Budget xmlns=""http://ereg.egov.bg/segment/R-09998"">
			<ProgrammeBudget xmlns=""http://ereg.egov.bg/segment/R-10010"">
				<Name xmlns=""http://ereg.egov.bg/segment/R-10009"">РАЗХОДИ ЗА ВЪЗНАГРАЖДЕНИЯ</Name>
				<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10009"">1</OrderNum>
				<ProgrammeExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10009"">
					<Name xmlns=""http://ereg.egov.bg/segment/R-10008"">Разходи за трудови и други възнаграждения, стипендии и други доходи на физически лица, пряко ангажирани с изпълнението на финансираните дейности и необходими за тяхната подготовка и осъществяване, вкл.осигурителните вноски, начислени за сметка на осигурителя върху договореното възнаграждение съгласно националното законодателство.</Name>
					<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10008"">1</OrderNum>
					<IsDeminimis xmlns=""http://ereg.egov.bg/segment/R-10008"">false</IsDeminimis>
					<IsEligibleCost xmlns=""http://ereg.egov.bg/segment/R-10008"">true</IsEligibleCost>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">Възнаграждения и осигуровки от страна на работодателя</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">1</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">1.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">12.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">13.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
				</ProgrammeExpenseBudget>
				<ProgrammeExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10009"">
					<Name xmlns=""http://ereg.egov.bg/segment/R-10008"">Пътни разходи за целевата група</Name>
					<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10008"">2</OrderNum>
					<IsDeminimis xmlns=""http://ereg.egov.bg/segment/R-10008"">false</IsDeminimis>
					<IsEligibleCost xmlns=""http://ereg.egov.bg/segment/R-10008"">true</IsEligibleCost>
				</ProgrammeExpenseBudget>
				<ProgrammeExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10009"">
					<Name xmlns=""http://ereg.egov.bg/segment/R-10008"">Разходи за материали и консумативи</Name>
					<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10008"">3</OrderNum>
					<IsDeminimis xmlns=""http://ereg.egov.bg/segment/R-10008"">false</IsDeminimis>
					<IsEligibleCost xmlns=""http://ereg.egov.bg/segment/R-10008"">true</IsEligibleCost>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">1234</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">1</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">2.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">7.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">9.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">555</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">2</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">1.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">7.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">8.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">6</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">3</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">4.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">7.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">11.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">89</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">4</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">6.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">9.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">15.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">0</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">5</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">7.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">8.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">15.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">4</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">6</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">1.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">2.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">3.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">5</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">7</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">1.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">2.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">3.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">9</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">8</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">1.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">2.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">3.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
				</ProgrammeExpenseBudget>
				<ProgrammeExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10009"">
					<Name xmlns=""http://ereg.egov.bg/segment/R-10008"">Разходи за външни услуги</Name>
					<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10008"">4</OrderNum>
					<IsDeminimis xmlns=""http://ereg.egov.bg/segment/R-10008"">false</IsDeminimis>
					<IsEligibleCost xmlns=""http://ereg.egov.bg/segment/R-10008"">true</IsEligibleCost>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">Разходи за дейности, свързани с осигуряване на публичност - до 1% от разходите по операцията</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">1</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">123.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">1.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">124.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
					<ProgrammeDetailsExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10008"">
						<Name xmlns=""http://ereg.egov.bg/segment/R-10007"">Разходи, произтичащи от договори за изработка/ услуга или договори за поръчка по реда на ЗЗД  - АКО Е ПРИЛОЖИМО</Name>
						<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10007"">2</OrderNum>
						<InterventionFieldCode xmlns=""http://ereg.egov.bg/segment/R-10007"">103</InterventionFieldCode>
						<FormOfFinanceCode xmlns=""http://ereg.egov.bg/segment/R-10007"">01</FormOfFinanceCode>
						<TerritorialDimensionCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDimensionCode>
						<TerritorialDeliveryMechanismCode xmlns=""http://ereg.egov.bg/segment/R-10007"">07</TerritorialDeliveryMechanismCode>
						<GrandAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">123.00</GrandAmount>
						<SelfAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">13.00</SelfAmount>
						<TotalAmount xmlns=""http://ereg.egov.bg/segment/R-10007"">136.00</TotalAmount>
					</ProgrammeDetailsExpenseBudget>
				</ProgrammeExpenseBudget>
			</ProgrammeBudget>
			<ProgrammeBudget xmlns=""http://ereg.egov.bg/segment/R-10010"">
				<Name xmlns=""http://ereg.egov.bg/segment/R-10009"">РАЗХОДИ ЗА ОРГАНИЗАЦИЯ И УПРАВЛЕНИЕ</Name>
				<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10009"">2</OrderNum>
				<ProgrammeExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10009"">
					<Name xmlns=""http://ereg.egov.bg/segment/R-10008"">Разходи за възнаграждения на екипа по проекта</Name>
					<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10008"">5</OrderNum>
					<IsDeminimis xmlns=""http://ereg.egov.bg/segment/R-10008"">false</IsDeminimis>
					<IsEligibleCost xmlns=""http://ereg.egov.bg/segment/R-10008"">true</IsEligibleCost>
				</ProgrammeExpenseBudget>
				<ProgrammeExpenseBudget xmlns=""http://ereg.egov.bg/segment/R-10009"">
					<Name xmlns=""http://ereg.egov.bg/segment/R-10008"">Разходи за командировки на персонала и административни разходи за издръжка на офис на проекта</Name>
					<OrderNum xmlns=""http://ereg.egov.bg/segment/R-10008"">6</OrderNum>
					<IsDeminimis xmlns=""http://ereg.egov.bg/segment/R-10008"">false</IsDeminimis>
					<IsEligibleCost xmlns=""http://ereg.egov.bg/segment/R-10008"">true</IsEligibleCost>
				</ProgrammeExpenseBudget>
			</ProgrammeBudget>
		</Budget>
		<Contract xmlns=""http://ereg.egov.bg/segment/R-09998"">
			<RequestedFundingAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">270.00</RequestedFundingAmount>
			<RequestedFundingPartOfCrossFinancingAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">346436</RequestedFundingPartOfCrossFinancingAmount>
			<CoFinancingBudgetAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">345636</CoFinancingBudgetAmount>
			<BudgetEIBAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">436</BudgetEIBAmount>
			<BudgetEBRDAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">456346</BudgetEBRDAmount>
			<BudgetWBAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">34634</BudgetWBAmount>
			<BudgetOtherMFIAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">43634</BudgetOtherMFIAmount>
			<BudgetOtherAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">34635</BudgetOtherAmount>
			<CoFinancingNonBudgetAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">36346</CoFinancingNonBudgetAmount>
			<NonBudgetEIBAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">34634</NonBudgetEIBAmount>
			<NonBudgetEBRDAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">43634</NonBudgetEBRDAmount>
			<NonBudgetWBAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">436366</NonBudgetWBAmount>
			<NonBudgetOtherMFIAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">345634</NonBudgetOtherMFIAmount>
			<NonBudgetOtherAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">34636</NonBudgetOtherAmount>
			<TotalCoFinancingAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">36346</TotalCoFinancingAmount>
			<TotalEligibleCostsPublicFunding xmlns=""http://ereg.egov.bg/segment/R-10006"">346456</TotalEligibleCostsPublicFunding>
			<TotalEligibleCosts xmlns=""http://ereg.egov.bg/segment/R-10006"">340.00</TotalEligibleCosts>
			<RatioRequestedFundingTotalEligibleCosts xmlns=""http://ereg.egov.bg/segment/R-10006"">79.41 %</RatioRequestedFundingTotalEligibleCosts>
			<ExpectedRevenue xmlns=""http://ereg.egov.bg/segment/R-10006"">3463456</ExpectedRevenue>
			<IneligibleCosts xmlns=""http://ereg.egov.bg/segment/R-10006"">0.00</IneligibleCosts>
			<IneligibleEIBAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">4363456</IneligibleEIBAmount>
			<IneligibleEBRDAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">3463</IneligibleEBRDAmount>
			<IneligibleWBAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">3456</IneligibleWBAmount>
			<IneligibleOtherMFIAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">436</IneligibleOtherMFIAmount>
			<IneligibleOtherAmount xmlns=""http://ereg.egov.bg/segment/R-10006"">4364563</IneligibleOtherAmount>
			<TotalProjectCost xmlns=""http://ereg.egov.bg/segment/R-10006"">340.00</TotalProjectCost>
		</Contract>
	</DimensionsBudgetContract>
	<ProgrammeContractActivities id=""0f7c688b-df08-4375-9a89-15c40c8e8a42"" isLocked=""false"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<ContractActivity xmlns=""http://ereg.egov.bg/segment/R-09995"">
			<Code xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada</Code>
			<Company xmlns=""http://ereg.egov.bg/segment/R-10011"">
				<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">12345678</Id>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Кандидат 1</Name>
			</Company>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum.</Name>
			<ExecutionMethod xmlns=""http://ereg.egov.bg/segment/R-10011"">asdasdasd</ExecutionMethod>
			<Result xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis.</Result>
			<StartMonth xmlns=""http://ereg.egov.bg/segment/R-10011"">12</StartMonth>
			<Duration xmlns=""http://ereg.egov.bg/segment/R-10011"">12</Duration>
			<Amount xmlns=""http://ereg.egov.bg/segment/R-10011"">123123</Amount>
		</ContractActivity>
		<ContractActivity xmlns=""http://ereg.egov.bg/segment/R-09995"">
			<Code xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis.</Code>
			<Company xmlns=""http://ereg.egov.bg/segment/R-10011"">
				<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">12345678</Id>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Кандидат 1</Name>
			</Company>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis. Nunc pharetra bibendum felis, eget posuere ante efficitur id. Pellentesque porttitor, erat quis vehicula blandit, lacus orci varius quam, at convallis neque justo quis leo.</Name>
			<ExecutionMethod xmlns=""http://ereg.egov.bg/segment/R-10011"">12123</ExecutionMethod>
			<Result xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis.</Result>
			<StartMonth xmlns=""http://ereg.egov.bg/segment/R-10011"">12</StartMonth>
			<Duration xmlns=""http://ereg.egov.bg/segment/R-10011"">12</Duration>
			<Amount xmlns=""http://ereg.egov.bg/segment/R-10011"">asdasdasd</Amount>
		</ContractActivity>
		<ContractActivity xmlns=""http://ereg.egov.bg/segment/R-09995"">
			<Code xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis.</Code>
			<Company xmlns=""http://ereg.egov.bg/segment/R-10011"">
				<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">12345678</Id>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Кандидат 1</Name>
			</Company>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis.</Name>
			<ExecutionMethod xmlns=""http://ereg.egov.bg/segment/R-10011"">12123</ExecutionMethod>
			<Result xmlns=""http://ereg.egov.bg/segment/R-10011"">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis.</Result>
			<StartMonth xmlns=""http://ereg.egov.bg/segment/R-10011"">12</StartMonth>
			<Duration xmlns=""http://ereg.egov.bg/segment/R-10011"">12</Duration>
			<Amount xmlns=""http://ereg.egov.bg/segment/R-10011"">12</Amount>
		</ContractActivity>
	</ProgrammeContractActivities>
	<ProgrammeIndicators id=""981b38b5-dd7c-43ac-b528-021ed12125f9"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<Indicator xmlns=""http://ereg.egov.bg/segment/R-10014"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10013"">761bf6ed-4968-460d-bba4-5511ad5a883f</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10013"">Безработни лица на възраст 15-29 години включително, с постоянен адрес в населено място извън област София - град</Name>
			<ModeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Основен</ModeName>
			<TrendName xmlns=""http://ereg.egov.bg/segment/R-10013"">Увеличение</TrendName>
			<TypeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Изпълнение</TypeName>
			<MeasureName xmlns=""http://ereg.egov.bg/segment/R-10013"">Брой</MeasureName>
		</Indicator>
		<Indicator xmlns=""http://ereg.egov.bg/segment/R-10014"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10013"">7af13548-2a73-4a37-acad-7f2f32d47e4b</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10013"">Безработни лица на възраст 15-29 години с постоянен или настоящ адрес в населено място на територията на област София-град</Name>
			<ModeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Основен</ModeName>
			<TrendName xmlns=""http://ereg.egov.bg/segment/R-10013"">Увеличение</TrendName>
			<TypeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Изпълнение</TypeName>
			<MeasureName xmlns=""http://ereg.egov.bg/segment/R-10013"">Брой</MeasureName>
		</Indicator>
		<Indicator xmlns=""http://ereg.egov.bg/segment/R-10014"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10013"">7af13548-2a73-4a37-acad-7f2f32d47e4b</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10013"">Безработни лица на възраст 15-29 години с постоянен или настоящ адрес в населено място на територията на област София-град</Name>
			<ModeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Основен</ModeName>
			<TrendName xmlns=""http://ereg.egov.bg/segment/R-10013"">Увеличение</TrendName>
			<TypeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Изпълнение</TypeName>
			<MeasureName xmlns=""http://ereg.egov.bg/segment/R-10013"">Брой</MeasureName>
		</Indicator>
		<Indicator xmlns=""http://ereg.egov.bg/segment/R-10014"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10013"">7af13548-2a73-4a37-acad-7f2f32d47e4b</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10013"">Безработни лица на възраст 15-29 години с постоянен или настоящ адрес в населено място на територията на област София-град</Name>
			<ModeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Основен</ModeName>
			<TrendName xmlns=""http://ereg.egov.bg/segment/R-10013"">Увеличение</TrendName>
			<TypeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Изпълнение</TypeName>
			<MeasureName xmlns=""http://ereg.egov.bg/segment/R-10013"">Брой</MeasureName>
		</Indicator>
		<Indicator xmlns=""http://ereg.egov.bg/segment/R-10014"">
			<Id xmlns=""http://ereg.egov.bg/segment/R-10013"">761bf6ed-4968-460d-bba4-5511ad5a883f</Id>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10013"">Безработни лица на възраст 15-29 години включително, с постоянен адрес в населено място извън област София - град</Name>
			<ModeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Основен</ModeName>
			<TrendName xmlns=""http://ereg.egov.bg/segment/R-10013"">Увеличение</TrendName>
			<TypeName xmlns=""http://ereg.egov.bg/segment/R-10013"">Изпълнение</TypeName>
			<MeasureName xmlns=""http://ereg.egov.bg/segment/R-10013"">Брой</MeasureName>
		</Indicator>
	</ProgrammeIndicators>
	<ContractTeams id=""6eef8e3a-8a28-4b89-993b-510eb0d79e7a"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit1</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit2</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit3</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Name>
			<Position xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Position>
			<Responsibilities xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Responsibilities>
			<Phone xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Phone>
			<Fax xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Fax>
			<Email xmlns=""http://ereg.egov.bg/segment/R-10015"">consectetur adipiscing elit</Email>
		</ContractTeam>
		<ContractTeam />
	</ContractTeams>
	<ProjectErrands id=""61fb6999-c660-4300-a909-a6e026dfd533"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<ProjectErrand>
			<Name xmlns=""http://ereg.egov.bg/segment/R-10016"">consectetur adipiscing elit</Name>
			<ErrandArea xmlns=""http://ereg.egov.bg/segment/R-10016"">
				<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">0</Id>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Доставка</Name>
			</ErrandArea>
			<ErrandLegalAct xmlns=""http://ereg.egov.bg/segment/R-10016"">
				<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">7e9b44e8-742b-45e5-b967-7b7feec6e18a</Id>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">ПМС</Name>
			</ErrandLegalAct>
			<ErrandType xmlns=""http://ereg.egov.bg/segment/R-10016"">
				<Id xmlns=""http://ereg.egov.bg/segment/R-10000"">0</Id>
				<Name xmlns=""http://ereg.egov.bg/segment/R-10000"">Открита процедура</Name>
			</ErrandType>
			<Amount xmlns=""http://ereg.egov.bg/segment/R-10016"">123</Amount>
			<PlanDate xmlns=""http://ereg.egov.bg/segment/R-10016"">2015-01-19</PlanDate>
			<Description xmlns=""http://ereg.egov.bg/segment/R-10016"">consectetur adipiscing elit</Description>
		</ProjectErrand>
	</ProjectErrands>
	<ProjectSpecFields id=""582486fa-25f9-4c22-9f3d-ee0b43e08c2a"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<ProjectSpecField>
			<Id xmlns=""http://ereg.egov.bg/segment/R-10017"">51f0a522-e8e5-45e1-b957-6e91b1f47486</Id>
			<Title xmlns=""http://ereg.egov.bg/segment/R-10017"">Устойчивост на резултатите (максимум 1 страници)</Title>
			<Description xmlns=""http://ereg.egov.bg/segment/R-10017"">Моля опишете устойчивостта на резултатите и очаквания ефект върху целевите групи.</Description>
			<Value xmlns=""http://ereg.egov.bg/segment/R-10017"">orem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis odio vitae ligula ultrices varius ac vitae mauris. Aliquam scelerisque id nisi sit amet facilisis. In pretium eget nibh vel tempor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla accumsan sapien ultrices leo lacinia dignissim. Integer eget turpis malesuada, pretium turpis sed, faucibus augue. Praesent quis felis vitae ligula cursus elementum. Curabitur augue tortor, condimentum at sagittis a, suscipit in turpis. Nunc pharetra bibendum felis, eget posuere ante efficitur id. Pellentesque porttitor, erat quis vehicula blandit, lacus orci varius quam, at convallis neque justo quis leo. Vestibulum scelerisque ex a magna ultricies tincidunt. Nunc malesuada quis sem suscipit ultricies.</Value>
		</ProjectSpecField>
	</ProjectSpecFields>
	<PaperAttachedDocuments id=""bbcb2f23-a121-488b-90f4-83dd65de67e4"" xmlns=""http://ereg.egov.bg/segment/R-10019"">
		<PaperAttachedDocument>
			<Type xmlns=""http://ereg.egov.bg/segment/R-09994"" />
		</PaperAttachedDocument>
	</PaperAttachedDocuments>
</Project>";
    }
}