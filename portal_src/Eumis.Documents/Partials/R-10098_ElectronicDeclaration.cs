using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace R_10098
{
    public partial class ElectronicDeclaration
    {
        public static ElectronicDeclaration Init(ContractProcedureDeclaration declaration)
        {
            ElectronicDeclaration electronicDeclaration = new ElectronicDeclaration();

            electronicDeclaration.Gid = declaration.gid.ToString();
            electronicDeclaration.Name = declaration.displayName;
            electronicDeclaration.Content = declaration.displayContent;
            electronicDeclaration.IsRequired = declaration.isRequired;
            electronicDeclaration.FieldType = declaration.fieldType;
            electronicDeclaration.OrderNum = declaration.orderNum.ToString();

            electronicDeclaration.IsActive = declaration.isActive;

            if (electronicDeclaration.FieldType == R_10098.FieldType.Nomenclature)
            {
                electronicDeclaration.Items = declaration.items.Where(i => i.isActive).OrderBy(i => i.orderNum).ToList();
            }

            #region Metadata

            electronicDeclaration.createDate = DateTime.Now;

            #endregion Metadata

            return electronicDeclaration;

        }

        public void SetFieldValuesForEdit(ElectronicDeclaration electronicDeclaration)
        {
            if (electronicDeclaration != null)
            {
                switch (electronicDeclaration.FieldType)
                {
                    case FieldType.CheckBox:
                        if (bool.TryParse(electronicDeclaration.FieldValue, out bool fieldValueCheckBox))
                        {
                            this.FieldValueCheckBox = fieldValueCheckBox;
                        }
                        break;
                    case FieldType.Numeric:
                        this.FieldValueNumeric = electronicDeclaration.FieldValue;
                        break;
                    case FieldType.Text:
                        this.FieldValueText = electronicDeclaration.FieldValue;
                        break;
                    case FieldType.Currency:
                        this.FieldValueCurrency = electronicDeclaration.FieldValue;
                        break;
                    case FieldType.Nomenclature:
                        this.FieldValueNomenclature = null;
                        if (!string.IsNullOrWhiteSpace(electronicDeclaration.FieldValueId) && Guid.TryParse(electronicDeclaration.FieldValueId, out Guid gid))
                        {
                            this.FieldValueNomenclature = new ContractProcedureDeclarationItem
                            {
                                gid = gid,
                                content = electronicDeclaration.FieldValue,
                            };
                        }
                        break;
                    case FieldType.Date:
                        if (!string.IsNullOrWhiteSpace(electronicDeclaration.FieldValue) && DateTime.TryParse(electronicDeclaration.FieldValue, out DateTime date))
                        {
                            this.FieldValueDate = date;
                        }
                        break;
                    case FieldType.Period:
                        this.FieldValuePeriod = new ContractDeclarationPeriod(electronicDeclaration.FieldValue);
                        break;
                    default:
                        throw new Exception("Unknown FieldType.");
                }
            }
        }

        public void SetFieldValues(ElectronicDeclaration vmDeclaration)
        {
            if (vmDeclaration != null)
            {
                switch (this.FieldType)
                {
                    case FieldType.CheckBox:
                        this.FieldValue = vmDeclaration.FieldValueCheckBox?.ToString();
                        this.FieldValueCheckBox = vmDeclaration.FieldValueCheckBox;
                        break;
                    case FieldType.Numeric:
                        this.FieldValue = !string.IsNullOrWhiteSpace(vmDeclaration.FieldValueNumeric) ? Regex.Replace(vmDeclaration.FieldValueNumeric, @"\s+", "") : string.Empty;
                        this.FieldValueNumeric = vmDeclaration.FieldValueNumeric;
                        break;
                    case FieldType.Text:
                        this.FieldValue = vmDeclaration.FieldValueText;
                        this.FieldValueText = vmDeclaration.FieldValueText;
                        break;
                    case FieldType.Currency:
                        this.FieldValue = !string.IsNullOrWhiteSpace(vmDeclaration.FieldValueCurrency) ? Regex.Replace(vmDeclaration.FieldValueCurrency, @"\s+", "") : string.Empty;
                        this.FieldValueCurrency = vmDeclaration.FieldValueCurrency;
                        break;
                    case FieldType.Nomenclature:
                        this.FieldValueId = vmDeclaration.FieldValueNomenclature?.gid.ToString();
                        this.FieldValue = vmDeclaration.FieldValueNomenclature?.content;
                        this.FieldValueNomenclature = vmDeclaration.FieldValueNomenclature;
                        break;
                    case FieldType.Date:
                        this.FieldValue = vmDeclaration.FieldValueDate.HasValue ? vmDeclaration.FieldValueDate.Value.ToString("dd.MM.yyyy") : string.Empty;
                        this.FieldValueDate = vmDeclaration.FieldValueDate;
                        break;
                    case FieldType.Period:
                        this.FieldValue = null;
                        if (vmDeclaration.FieldValuePeriod.DateFrom.HasValue || vmDeclaration.FieldValuePeriod.DateTo.HasValue)
                        {
                            this.FieldValue = string.Format("{0:dd.MM.yyyy}${1:dd.MM.yyyy}", vmDeclaration.FieldValuePeriod.DateFrom.HasValue ? vmDeclaration.FieldValuePeriod.DateFrom.Value : (DateTime?)null, vmDeclaration.FieldValuePeriod.DateTo.HasValue ? vmDeclaration.FieldValuePeriod.DateTo.Value.Date : (DateTime?)null);
                        }
                        this.FieldValuePeriod = vmDeclaration.FieldValuePeriod;
                        break;
                    default:
                        throw new Exception("Unknown FieldType.");
                }
            }
        }

        [XmlIgnore]
        public bool? FieldValueCheckBox { get; set; }

        [XmlIgnore]
        public string FieldValueNumeric { get; set; }

        [XmlIgnore]
        public string FieldValueText { get; set; }

        [XmlIgnore]
        public string FieldValueCurrency { get; set; }

        [XmlIgnore]
        public ContractProcedureDeclarationItem FieldValueNomenclature { get; set; }

        [XmlIgnore]
        public DateTime? FieldValueDate { get; set; }

        [XmlIgnore]
        public ContractDeclarationPeriod FieldValuePeriod { get; set; }

        [XmlIgnore]
        public bool IsActive { get; set; }

        [XmlIgnore]
        public IList<ContractProcedureDeclarationItem> Items { get; set; }
    }
}
