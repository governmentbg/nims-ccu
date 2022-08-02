import Select2 from 'select2';

function PrognosesSummaryReportCtrl($scope, l10n, summaryReport) {
  var getChartData = function() {
    return [
      [
        $scope.procedure.procedureBudget,
        $scope.procedure.approvedProjectsBudget,
        $scope.procedure.contractsBfpBudget,
        $scope.procedure.prognosedContractedBfpAmount,
        $scope.procedure.paymentsBfpAmount,
        $scope.procedure.approvedBfpAmount,
        $scope.procedure.actuallyPaidAmount,
        $scope.procedure.prognosedApprovedBfpAmount,
        $scope.procedure.certifiedBfpAmount,
        $scope.procedure.prognosedCertifiedBfpAmount
      ]
    ];
  };

  $scope.select2Options = {
    data: summaryReport,
    formatResult: function(result, container, query, escapeMarkup) {
      var markup = [];
      Select2.util.markMatch(result.procedureName, query.term, markup, escapeMarkup);
      return markup.join('');
    },
    formatSelection: function(data) {
      return data ? Select2.util.escapeMarkup(data.procedureName) : undefined;
    },
    id: function(obj) {
      return obj.procedureId;
    }
  };

  $scope.procedure = summaryReport[0];

  $scope.procedureChanged = function() {
    $scope.chartData.data = getChartData();
  };

  $scope.chartData = {
    labels: [
      l10n.get('prognoses_summaryReport_budgetTotal'),
      l10n.get('prognoses_summaryReport_approvedProjectsBudget'),
      l10n.get('prognoses_summaryReport_contractsBudget'),
      l10n.get('prognoses_summaryReport_prognosedContractedAmounts'),
      l10n.get('prognoses_summaryReport_paymentAmounts'),
      l10n.get('prognoses_summaryReport_approvedAmounts'),
      l10n.get('prognoses_summaryReport_paidAmounts'),
      l10n.get('prognoses_summaryReport_prognosedApprovedAmounts'),
      l10n.get('prognoses_summaryReport_certifiedAmounts'),
      l10n.get('prognoses_summaryReport_prognosedCertifiedAmounts')
    ],
    data: getChartData()
  };
}

PrognosesSummaryReportCtrl.$inject = ['$scope', 'l10n', 'summaryReport'];

PrognosesSummaryReportCtrl.$resolve = {
  summaryReport: [
    'PrognosisReport',
    function(PrognosisReport) {
      return PrognosisReport.getSummary().$promise;
    }
  ]
};

export { PrognosesSummaryReportCtrl };
