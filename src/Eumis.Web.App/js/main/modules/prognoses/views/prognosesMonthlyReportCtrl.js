import _ from 'lodash';

function PrognosesMonthlyReportCtrl($scope, $q, l10n, Programme, PrognosisReport) {
  var months;

  $scope.filters = {
    programmeId: null,
    year: null,
    months: null
  };

  $scope.isDataVisible = false;

  $scope.chartData = {
    labels: [],
    series: [
      l10n.get('prognoses_monthlyReport_advancePayments'),
      l10n.get('prognoses_monthlyReport_advanceVerPayments'),
      l10n.get('prognoses_monthlyReport_intermediatePayments'),
      l10n.get('prognoses_monthlyReport_finalPayments')
    ]
  };

  $scope.search = function() {
    return $scope.reportForm.$validate().then(function() {
      if ($scope.reportForm.$valid) {
        var promises = {
          prognosisReport: PrognosisReport.getMonthlyPrognoses({
            programmeId: $scope.filters.programmeId,
            year: $scope.filters.year,
            months: $scope.filters.months
          }).$promise
        };

        $scope.isDataVisible = false;

        return $q.all(promises).then(function(res) {
          months = _.sortBy($scope.months, 'orderNum');

          $scope.excelUrl =
            'api/prognosisReports/monthlyPrognoses/excel?' +
            'programmeId=' +
            $scope.filters.programmeId +
            '&year=' +
            $scope.filters.year;

          _.forEach($scope.filters.months, function(month) {
            $scope.excelUrl += '&months=' + month;
          });

          $scope.chartData.labels = _.map(months, 'name');

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

PrognosesMonthlyReportCtrl.$inject = ['$scope', '$q', 'l10n', 'Programme', 'PrognosisReport'];

export { PrognosesMonthlyReportCtrl };
