﻿PRINT 'Insert SapSchemas'
GO

SET IDENTITY_INSERT [SapSchemas] ON

INSERT INTO [SapSchemas]
    ([SapSchemaId], [Content], [IsActive], [CreateDate], [ModifyDate], [Type])
VALUES
    (1             , N'<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema"><xs:element name="SAPImport"><xs:complexType><xs:sequence><xs:element name="SapKey" type="xs:string" minOccurs="1" maxOccurs="1"/><xs:element name="Date" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Time" type="xs:time" minOccurs="1" maxOccurs="1" /><xs:element name="SapUser" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="Contract" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ContractSapNum" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="EuFund" type="_EuFund" minOccurs="1" maxOccurs="1"/><xs:element name="ReqPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ReqPaymentNum" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="ReqPaymentDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="ContractPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="FinanceSource" type="_FinanceSource" minOccurs="1" maxOccurs="1" /><xs:element name="PayedAmount" type="xs:double" minOccurs="1" maxOccurs="1" /><xs:element name="Currency" type="_Currency" minOccurs="1" maxOccurs="1" /><xs:element name="PaymentType" type="_PaymentType" minOccurs="0" maxOccurs="1" /><xs:element name="AccDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="BankDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="SAPDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Comment" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="StornoCode" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="StornoDescr" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field3" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field4" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field5" type="xs:string" minOccurs="0" maxOccurs="1" /></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element><xs:simpleType name="_EuFund"><xs:restriction base="xs:string"><xs:pattern value="([A-Z0-9])+" /></xs:restriction></xs:simpleType><xs:simpleType name="_PaymentType"><xs:restriction base="xs:string"><xs:enumeration value="" /><xs:enumeration value="авансово" /><xs:enumeration value="междинно" /><xs:enumeration value="окончателно" /><xs:enumeration value="глоба" /><xs:enumeration value="лихва" /><xs:enumeration value="възстановяване при доброволно прекратяване" /><xs:enumeration value="възстановяване при грешка" /><xs:enumeration value="възстановяване при нередност" /><xs:enumeration value="банкова гаранция" /><xs:enumeration value="касов трансфер" /></xs:restriction></xs:simpleType><xs:simpleType name="_Currency"><xs:restriction base="xs:string"><xs:enumeration value="BGN" /><xs:enumeration value="EUR" /></xs:restriction></xs:simpleType><xs:simpleType name="_FinanceSource"><xs:restriction base="xs:string"><xs:enumeration value="BG" /><xs:enumeration value="EU" /><xs:enumeration value="MZ" /><xs:enumeration value="SF" /><xs:enumeration value="IF" /><xs:enumeration value="PF" /></xs:restriction></xs:simpleType></xs:schema>' , 1, GETDATE(), GETDATE(), 1),
    (2             , N'<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" attributeFormDefault="unqualified" elementFormDefault="qualified"><xs:element name="SAPImport"><xs:complexType><xs:sequence><xs:element name="SapKey" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="Date" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Time" type="xs:time" minOccurs="1" maxOccurs="1" /><xs:element name="SapUser" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="Contract" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ContractSapNum" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="EuFund" type="_EuFund" minOccurs="1" maxOccurs="1" /><xs:element name="ReqPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="ReqPaymentNum" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="ReqPaymentDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="ContractPayment" minOccurs="1" maxOccurs="unbounded"><xs:complexType><xs:sequence><xs:element name="FinanceSource" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="PayedAmount" type="xs:double" minOccurs="1" maxOccurs="1" /><xs:element name="Currency" type="_Currency" minOccurs="1" maxOccurs="1" /><xs:element name="PaymentType" type="_PaymentType" minOccurs="0" maxOccurs="1" /><xs:element name="AccDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="BankDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="SAPDate" type="xs:date" minOccurs="1" maxOccurs="1" /><xs:element name="Comment" type="xs:string" minOccurs="1" maxOccurs="1" /><xs:element name="StornoCode" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="StornoDescr" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field3" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field4" type="xs:string" minOccurs="0" maxOccurs="1" /><xs:element name="Field5" type="xs:string" minOccurs="0" maxOccurs="1" /></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element><xs:simpleType name="_EuFund"><xs:restriction base="xs:string"><xs:pattern value="([A-Z0-9])+" /></xs:restriction></xs:simpleType><xs:simpleType name="_PaymentType"><xs:restriction base="xs:string"><xs:enumeration value="" /><xs:enumeration value="авансово" /><xs:enumeration value="междинно" /><xs:enumeration value="окончателно" /><xs:enumeration value="глоба" /><xs:enumeration value="лихва" /><xs:enumeration value="възстановяване при доброволно прекратяване" /><xs:enumeration value="възстановяване при грешка" /><xs:enumeration value="възстановяване при нередност" /><xs:enumeration value="банкова гаранция" /><xs:enumeration value="касов трансфер" /></xs:restriction></xs:simpleType><xs:simpleType name="_Currency"><xs:restriction base="xs:string"><xs:enumeration value="BGN" /><xs:enumeration value="EUR" /></xs:restriction></xs:simpleType></xs:schema>' , 1, GETDATE(), GETDATE(), 2);
SET IDENTITY_INSERT [SapSchemas] OFF
GO
