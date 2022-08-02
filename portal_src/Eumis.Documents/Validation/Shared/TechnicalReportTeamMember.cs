using System.Collections.Generic;

using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using Eumis.Documents.Enums;

namespace Eumis.Documents.Validation.Shared
{
    public class TechnicalReportTeamMember : CSValidatorBase<R_10057.TechnicalReportTeamMember>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10057.TechnicalReportTeamMember complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsNameValid = true;
            complexType.IsPositionValid = true;
            complexType.IsCommitmentTypeValid = true;
            complexType.IsDateValid = true;
            complexType.IsHoursValid = true;
            complexType.IsActivityValid = true;
            complexType.IsResultValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Name))
            {
                errors.Add(ValidationOption.Create(
                                modelPath + ".Name",
                                Global.ShortTemplateRequired,
                                string.Format(Global.ViewTemplateRequired, Global.TeamMemberName, Global.SectionTeam), true, true));

                complexType.IsNameValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Position))
            {
                errors.Add(ValidationOption.Create(
                                modelPath + ".Position",
                                Global.ShortTemplateRequired,
                                string.Format(Global.ViewTemplateRequired, Global.TeamMemberPosition, Global.SectionTeam), true, true));

                complexType.IsPositionValid = false;
            }

            #region Uin

            bool isUinTypeEmpty = complexType.UinType == null || string.IsNullOrWhiteSpace(complexType.UinType.Id) || string.IsNullOrWhiteSpace(complexType.UinType.Name);

            complexType.IsUinValid = true;

            if (isUinTypeEmpty)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".UinType.id",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Identifier, Global.SectionTeam), true, true));

                complexType.IsUinTypeValid = false;
            }
            else
                complexType.IsUinTypeValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Uin))
            {
                if (isUinTypeEmpty || complexType.UinType.Id != UinTypeNomenclature.ForeignPerson.Code)
                {
                    errors.Add(ValidationOption.Create(
                                        modelPath + ".Uin",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.TechnicalReportTeamMemberUin, Global.SectionTeam), true, true));

                    complexType.IsUinValid = false;
                }
            }
            else if (!isUinTypeEmpty)
            {
                string uinTypeValue = complexType.UinType.Id;

                if (uinTypeValue == UinTypeNomenclature.Egn.Code && !BulstatValidator.IsValidEGN(complexType.Uin))
                {
                    errors.Add(ValidationOption.Create(
                            modelPath + ".Uin",
                            Global.ShortTemplateInvalid,
                            string.Format(Global.ViewTemplateInvalid, Global.TechnicalReportTeamMemberUin, Global.SectionTeam), true, true));

                    complexType.IsUinValid = false;
                }
                else if (complexType.Uin.Length > Constants.BulstatFieldMaxLength)
                {
                    errors.Add(ValidationOption.Create(
                                    modelPath + ".Uin",
                                    string.Format(Global.ShortTemplateSymbolsMax, Constants.BulstatFieldMaxLength),
                                    string.Format(Global.ViewTemplateSymbolsMax, Global.TechnicalReportTeamMemberUin, Global.SectionTeam, Constants.BulstatFieldMaxLength), true, true));

                    complexType.IsUinValid = false;
                }
            }

            #endregion

            if (complexType.CommitmentType == null || string.IsNullOrWhiteSpace(complexType.CommitmentType.Value) || string.IsNullOrWhiteSpace(complexType.CommitmentType.Description))
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".CommitmentType.Value",
                                    Global.ShortTemplateRequired,
                                    string.Format(Global.ViewTemplateRequired, Global.TeamMemberCommitmentType, Global.SectionTeam), true, true));

                complexType.IsCommitmentTypeValid = false;
            }

            if (!complexType.Date.HasValue)
            {
                errors.Add(ValidationOption.Create(
                                modelPath + ".Date",
                                Global.ShortTemplateRequired,
                                string.Format(Global.ViewTemplateRequired, Global.TeamMemberDate, Global.SectionTeam), true, true));

                complexType.IsDateValid = false;
            }

            if (complexType.Hours < 0)
            {
                errors.Add(ValidationOption.Create(
                                    modelPath + ".Hours",
                                    Global.ShortTemplateNonNegativeNumber,
                                    string.Format(Global.ViewTemplateNonNegativeNumber, Global.TeamMemberHours, Global.SectionTeam), true, true));

                complexType.IsHoursValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Activity))
            {
                errors.Add(ValidationOption.Create(
                                modelPath + ".Activity",
                                Global.ShortTemplateRequired,
                                string.Format(Global.ViewTemplateRequired, Global.TeamMemberActivity, Global.SectionTeam), true, true));

                complexType.IsActivityValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Result))
            {
                errors.Add(ValidationOption.Create(
                                modelPath + ".Result",
                                Global.ShortTemplateRequired,
                                string.Format(Global.ViewTemplateRequired, Global.TeamMemberResult, Global.SectionTeam), true, true));

                complexType.IsResultValid = false;
            }
        }
    }
}
