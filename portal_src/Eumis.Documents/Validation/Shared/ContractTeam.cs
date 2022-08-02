using System;
using System.Collections.Generic;
using Eumis.Common.Validation;
using Eumis.Common.Validation.Resources;
using System.Text.RegularExpressions;

namespace Eumis.Documents.Validation.Shared
{
    public class ContractTeam : CSValidatorBase<R_10015.ContractTeam>
    {
        protected override void Validate(ICSValidationEngine csValidationEngine, R_10015.ContractTeam complexType, string modelPath, IList<ValidationOption> errors)
        {
            complexType.IsNameValid = true;
            complexType.IsPositionValid = true;
            complexType.IsResponsibilitiesValid = true;
            complexType.IsPhoneValid = true;
            complexType.IsEmailValid = true;
            complexType.IsFaxValid = true;

            //if (string.IsNullOrWhiteSpace(complexType.Name))
            //{
            //    errors.Add(ValidationOption.Create(
            //                            modelPath + ".Name",
            //                            Global.ShortTemplateRequired,
            //                            string.Format(Global.ViewTemplateRequired, Global.NameByDocument, Global.SectionTeam), true, true));

            //    complexType.IsNameValid = false;
            //}
            if (!string.IsNullOrWhiteSpace(complexType.Name) && complexType.Name.Length > Constants.ContractTeamNameLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Name",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractTeamNameLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.NameByDocument, Global.SectionTeam, Constants.ContractTeamNameLength), true, true));

                complexType.IsNameValid = false;
            }
            else
                complexType.IsNameValid = true;

            if (string.IsNullOrWhiteSpace(complexType.Position))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Position",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.ProjectPosition, Global.SectionTeam), true, true));

                complexType.IsPositionValid = false;
            }
            else if (complexType.Position.Length > Constants.ContractTeamPositionLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Position",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractTeamPositionLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.ProjectPosition, Global.SectionTeam, Constants.ContractTeamPositionLength), true, true));

                complexType.IsPositionValid = false;
            }

            if (string.IsNullOrWhiteSpace(complexType.Responsibilities))
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Responsibilities",
                                        Global.ShortTemplateRequired,
                                        string.Format(Global.ViewTemplateRequired, Global.Responsibilities, Global.SectionTeam), true, true));

                complexType.IsResponsibilitiesValid = false;
            }
            else if (complexType.Responsibilities.Length > Constants.ContractTeamResponsibilitiesLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Responsibilities",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractTeamResponsibilitiesLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Responsibilities, Global.SectionTeam, Constants.ContractTeamResponsibilitiesLength), true, true));

                complexType.IsResponsibilitiesValid = false;
            }

            //if (string.IsNullOrWhiteSpace(complexType.Phone))
            //{
            //    errors.Add(ValidationOption.Create(
            //                            modelPath + ".Phone",
            //                            Global.ShortTemplateRequired,
            //                            string.Format(Global.ViewTemplateRequired, Global.Phone, Global.SectionTeam), true, true));
            //
            //    complexType.IsPhoneValid = false;
            //}

            if (!string.IsNullOrWhiteSpace(complexType.Phone) && complexType.Phone.Length > Constants.ContractTeamPhoneLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Phone",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractTeamPhoneLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Phone, Global.SectionTeam, Constants.ContractTeamPhoneLength), true, true));

                complexType.IsPhoneValid = false;
            }

            if (!string.IsNullOrWhiteSpace(complexType.Email))
            {
                if (complexType.Email.Length > Constants.ContractTeamEmailLength)
                {
                    errors.Add(ValidationOption.Create(
                                            modelPath + ".Email",
                                            string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractTeamEmailLength),
                                            string.Format(Global.ViewTemplateSymbolsMax, Global.Email, Global.SectionTeam, Constants.ContractTeamEmailLength), true, true));

                    complexType.IsEmailValid = false;
                }
                else if (!_emailRegex.IsMatch(complexType.Email))
                {
                    errors.Add(ValidationOption.Create(
                        modelPath + ".Email",
                        Global.ShortTemplateValidMail,
                        string.Format(Global.ViewTemplateValidMail, Global.Email, Global.SectionTeam), true, true));

                    complexType.IsEmailValid = false;
                }
            }
            //else
            //{
            //    errors.Add(ValidationOption.Create(
            //            modelPath + ".Email",
            //            Global.ShortTemplateRequired,
            //            string.Format(Global.ViewTemplateRequired, Global.Email, Global.SectionTeam), true, true));
            //
            //    complexType.IsEmailValid = false;
            //}

            if (!string.IsNullOrWhiteSpace(complexType.Fax) && complexType.Fax.Length > Constants.ContractTeamPhoneLength)
            {
                errors.Add(ValidationOption.Create(
                                        modelPath + ".Fax",
                                        string.Format(Global.ShortTemplateSymbolsMax, Constants.ContractTeamPhoneLength),
                                        string.Format(Global.ViewTemplateSymbolsMax, Global.Fax, Global.SectionTeam, Constants.ContractTeamPhoneLength), true, true));

                complexType.IsFaxValid = false;
            }
        }

        private Regex _emailRegex = new Regex(@"(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$");
    }
}