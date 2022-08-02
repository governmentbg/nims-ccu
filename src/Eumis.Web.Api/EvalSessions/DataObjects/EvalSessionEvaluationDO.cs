using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionEvaluationDO
    {
        public EvalSessionEvaluationDO()
        {
        }

        public EvalSessionEvaluationDO(
            int evalSessionId,
            int projectId,
            ProcedureEvalTableType evalTableType,
            ProcedureEvalType evalType,
            decimal maxEvalTableXmlPoints,
            IList<EvalSessionSheetsVO> sheets,
            byte[] version)
        {
            this.EvalSessionId = evalSessionId;
            this.ProjectId = projectId;
            this.EvalTableType = evalTableType;
            this.EvalType = evalType;
            this.CalculationType = EvalSessionEvaluationCalculationType.Automatic;
            this.IsDeleted = false;
            this.CreateDate = DateTime.Now;

            if (sheets.Where(p => p.Status == EvalSessionSheetStatus.Draft).Any())
            {
                this.CannotEvaluate = true;
                this.CannotEvaluateReason = WebApiTexts.EvalSessionEvaluationDO_CannotEvaluateReason_HasDrafts;
                this.CalculationType = EvalSessionEvaluationCalculationType.Manual;
            }
            else
            {
                var endedSheets = sheets.Where(p => p.Status == EvalSessionSheetStatus.Ended);
                if (endedSheets.Any())
                {
                    if (endedSheets.Where(p => p.EvalIsPassed.HasValue && p.EvalIsPassed.Value).Any() && endedSheets.Where(p => p.EvalIsPassed.HasValue && !p.EvalIsPassed.Value).Any())
                    {
                        this.CannotEvaluate = true;
                        this.CannotEvaluateReason = WebApiTexts.EvalSessionEvaluationDO_CannotEvaluateReason_Both_Passed_NotPassed;
                        this.CalculationType = EvalSessionEvaluationCalculationType.Manual;
                    }
                    else
                    {
                        this.EvalIsPassed = endedSheets.Where(p => p.EvalIsPassed.HasValue).Select(p => p.EvalIsPassed.Value).Aggregate(true, (a, b) => a && b);
                        this.OriginalEvalIsPassed = this.EvalIsPassed;

                        if (this.EvalType == ProcedureEvalType.Weight)
                        {
                            var endedSheetsWithPoints = endedSheets.Where(p => p.EvalPoints.HasValue);

                            if (endedSheetsWithPoints.Count() == endedSheets.Count())
                            {
                                if (endedSheetsWithPoints.Count() != 0)
                                {
                                    var maxPoints = endedSheetsWithPoints.Max(t => t.EvalPoints.Value);
                                    var minPoints = endedSheetsWithPoints.Min(t => t.EvalPoints.Value);
                                    var diff = maxPoints - minPoints;
                                    var maxDiff = Eumis.Domain.Core.Calculator.RoundBy2(0.2M * maxEvalTableXmlPoints);
                                    if (maxPoints != 0 && diff > maxDiff)
                                    {
                                        this.CannotEvaluate = true;
                                        this.CannotEvaluateReason = string.Format(
                                            WebApiTexts.EvalSessionEvaluationDO_CannotEvaluateReason_20Percent_Diff,
                                            maxDiff,
                                            maxEvalTableXmlPoints,
                                            maxPoints,
                                            minPoints);
                                        this.CalculationType = EvalSessionEvaluationCalculationType.Manual;
                                    }
                                    else
                                    {
                                        var points = endedSheetsWithPoints.Select(p => p.EvalPoints.Value).Aggregate(0M, (a, b) => a + b) / endedSheetsWithPoints.Count();
                                        this.EvalPoints = Eumis.Domain.Core.Calculator.RoundBy2(points);
                                        this.OriginalEvalPoints = this.EvalPoints;
                                    }
                                }
                                else
                                {
                                    this.CannotEvaluate = true;
                                    this.CannotEvaluateReason = WebApiTexts.EvalSessionEvaluationDO_CannotEvaluateReason_NotFinished_WithPoints;
                                    this.CalculationType = EvalSessionEvaluationCalculationType.Manual;
                                }
                            }
                            else
                            {
                                this.CannotEvaluate = true;
                                this.CannotEvaluateReason = WebApiTexts.EvalSessionEvaluationDO_CannotEvaluateReason_NoPoints;
                                this.CalculationType = EvalSessionEvaluationCalculationType.Manual;
                            }
                        }
                    }
                }
                else
                {
                    this.CannotEvaluate = true;
                    this.CannotEvaluateReason = WebApiTexts.EvalSessionEvaluationDO_CannotEvaluateReason_NoFinishedSheet;
                    this.CalculationType = EvalSessionEvaluationCalculationType.Manual;
                }
            }

            this.Sheets = sheets;
            this.Version = version;
        }

        public EvalSessionEvaluationDO(EvalSessionEvaluation evalSessionEvaluation, IList<EvalSessionSheetsVO> sheets, byte[] version)
        {
            this.EvalSessionId = evalSessionEvaluation.EvalSessionId;
            this.EvalSessionEvaluationId = evalSessionEvaluation.EvalSessionEvaluationId;
            this.ProjectId = evalSessionEvaluation.ProjectId;
            this.EvalTableType = evalSessionEvaluation.EvalTableType;
            this.CalculationType = evalSessionEvaluation.CalculationType;
            this.EvalType = evalSessionEvaluation.EvalType;
            this.EvalIsPassed = evalSessionEvaluation.EvalIsPassed;
            this.EvalPoints = evalSessionEvaluation.EvalPoints;
            this.EvalNote = evalSessionEvaluation.EvalNote;
            this.IsDeleted = evalSessionEvaluation.IsDeleted;
            this.IsDeletedNote = evalSessionEvaluation.IsDeletedNote;
            this.CreateDate = evalSessionEvaluation.CreateDate;

            this.Sheets = sheets;

            this.Version = version;
        }

        public int? EvalSessionId { get; set; }

        public int? EvalSessionEvaluationId { get; set; }

        public int? ProjectId { get; set; }

        public ProcedureEvalTableType? EvalTableType { get; set; }

        public EvalSessionEvaluationCalculationType? CalculationType { get; set; }

        public ProcedureEvalType? EvalType { get; set; }

        public bool? EvalIsPassed { get; set; }

        public bool? OriginalEvalIsPassed { get; set; }

        public decimal? EvalPoints { get; set; }

        public decimal? OriginalEvalPoints { get; set; }

        public string EvalNote { get; set; }

        public bool? IsDeleted { get; set; }

        public string IsDeletedNote { get; set; }

        public DateTime CreateDate { get; set; }

        public bool? CannotEvaluate { get; set; }

        public string CannotEvaluateReason { get; set; }

        public IList<EvalSessionSheetsVO> Sheets { get; set; }

        public byte[] Version { get; set; }
    }
}
