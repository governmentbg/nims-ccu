import _ from 'lodash';

function PrognosesProgrammePriorityReportCtrl($scope, l10n, PrognosisReport, years) {
  $scope.filters = {
    programmePriorityId: null
  };

  $scope.isDataVisible = false;

  $scope.chartData = {
    labels: _.map(years, 'name'),
    series: [
      l10n.get('prognoses_programmePriorityReport_prognosedContracted'),
      l10n.get('prognoses_programmePriorityReport_contracted'),
      l10n.get('prognoses_programmePriorityReport_prognosedApproved'),
      l10n.get('prognoses_programmePriorityReport_approved'),
      l10n.get('prognoses_programmePriorityReport_prognosedCertified'),
      l10n.get('prognoses_programmePriorityReport_certified')
    ]
  };

  $scope.search = function() {
    return $scope.reportForm.$validate().then(function() {
      if ($scope.reportForm.$valid) {
        $scope.isDataVisible = false;

        return PrognosisReport.getProgrammePriorityPrognoses({
          programmePriorityId: $scope.filters.programmePriorityId
        }).$promise.then(function(res) {
          var resByYears = _.groupBy(res, 'year'),
            prognosedContracted = [],
            contracted = [],
            prognosedApproved = [],
            approved = [],
            prognosedCertified = [],
            certified = [];

          _.forEach(years, function(year) {
            var yearReport = resByYears[year.nomValueId] || [],
              prognosedContractedForYear = 0,
              contractedForYear = 0,
              prognosedApprovedForYear = 0,
              approvedForYear = 0,
              prognosedCertifiedForYear = 0,
              certifiedForYear = 0;

            _.forEach(yearReport, function(item) {
              prognosedContractedForYear += item.prognosedContractedBfpAmount;
              contractedForYear += item.contractsBfpAmount;
              prognosedApprovedForYear += item.prognosedApprovedBfpAmount;
              approvedForYear += item.approvedBfpAmount;
              prognosedCertifiedForYear += item.prognosedCertifiedBfpAmount;
              certifiedForYear += item.certifiedBfpAmount;
            });

            prognosedContracted.push(prognosedContractedForYear);
            contracted.push(contractedForYear);
            prognosedApproved.push(prognosedApprovedForYear);
            approved.push(approvedForYear);
            prognosedCertified.push(prognosedCertifiedForYear);
            certified.push(certifiedForYear);
          });

          $scope.chartData.data = [
            prognosedContracted,
            contracted,
            prognosedApproved,
            approved,
            prognosedCertified,
            certified
          ];
          $scope.isDataVisible = true;
        });
      }
    });
  };
}

PrognosesProgrammePriorityReportCtrl.$inject = ['$scope', 'l10n', 'PrognosisReport', 'years'];

PrognosesProgrammePriorityReportCtrl.$resolve = {
  years: [
    'Nomenclatures',
    function(Nomenclatures) {
      return Nomenclatures.query({
        alias: 'years'
      }).$promise;
    }
  ]
};

export { PrognosesProgrammePriorityReportCtrl };
