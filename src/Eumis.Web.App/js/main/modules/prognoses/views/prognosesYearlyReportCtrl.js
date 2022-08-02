import _ from 'lodash';

function PrognosesYearlyReportCtrl($scope, $q, l10n, Programme, PrognosisReport, quarters) {
  var years;

  $scope.filters = {
    programmeId: null,
    years: null
  };

  $scope.isDataVisible = false;

  $scope.chartData = {
    labels: [],
    series: [
      l10n.get('prognoses_yearlyReport_advancePayments'),
      l10n.get('prognoses_yearlyReport_advanceVerPayments'),
      l10n.get('prognoses_yearlyReport_intermediatePayments'),
      l10n.get('prognoses_yearlyReport_finalPayments')
    ]
  };

  $scope.search = function() {
    return $scope.reportForm.$validate().then(function() {
      if ($scope.reportForm.$valid) {
        var promises = {
          prognosisReport: PrognosisReport.getYearlyPrognoses({
            programmeId: $scope.filters.programmeId,
            years: $scope.filters.years
          }).$promise
        };

        $scope.isDataVisible = false;

        return $q.all(promises).then(function(res) {
          years = _.sortBy($scope.years, 'nomValueId');

          $scope.excelUrl =
            'api/prognosisReports/yearlyPrognoses/excel?' +
            'programmeId=' +
            $scope.filters.programmeId;

          _.forEach($scope.filters.years, function(year) {
            $scope.excelUrl += '&years=' + year;
          });

          $scope.chartData.labels = [];
          _.forEach(years, function(year) {
            _.forEach(quarters, function(quarter) {
              $scope.chartData.labels.push(year.name + ' ' + quarter.name);
            });
          });

          $scope.data = res.prognosisReport;
          $scope.openProgrammePriority($scope.data[0]);
          $scope.isDataVisible = true;
        });
      }
    });
  };

  $scope.openProgrammePriority = function(pp) {
    pp.loading = true;

    var prevActive = _.find($scope.data, function(item) {
      return item.isActive;
    });

    if (prevActive) {
      prevActive.isActive = false;
    }

    pp.isActive = true;

    pp.loading = false;
  };
}

PrognosesYearlyReportCtrl.$inject = [
  '$scope',
  '$q',
  'l10n',
  'Programme',
  'PrognosisReport',
  'quarters'
];

PrognosesYearlyReportCtrl.$resolve = {
  quarters: [
    'Nomenclatures',
    function(Nomenclatures) {
      return Nomenclatures.query({
        alias: 'quarters'
      }).$promise;
    }
  ]
};

export { PrognosesYearlyReportCtrl };
