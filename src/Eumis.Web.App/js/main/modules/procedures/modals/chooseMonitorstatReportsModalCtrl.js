import _ from 'lodash';

function ChooseMonitorstatReportsModalCtrl(
  $scope,
  $uibModalInstance,
  scMessage,
  $q,
  scModalParams,
  ProcedureMonitorstatDocument,
  monitorstatReports
) {
  $scope.procedureId = scModalParams.procedureId;
  $scope.chosenReportIds = [];
  $scope.hasChoosenAll = true;
  $scope.monitorstatReports = monitorstatReports;
  $scope.tableControl = {};

  $scope.filters = {
    year: null,
    survey: null
  };

  $scope.onYearChange = function() {
    $scope.filters.survey = null;
  };

  $scope.search = function() {
    return ProcedureMonitorstatDocument.getMonitorstatReports({
      id: scModalParams.procedureId,
      year: $scope.filters.year,
      surveyId: $scope.filters.survey
    }).$promise.then(function(filteredReports) {
      $scope.chosenReportIds = _.intersection(
        $scope.chosenReportIds,
        _.map(filteredReports, 'reportId')
      );

      _.map(filteredReports, function(report) {
        if (_.contains($scope.chosenReportIds, report.reportId)) {
          report.isChosen = true;
        }

        return report;
      });

      $scope.monitorstatReports = _.map(filteredReports, r => r);
    });
  };

  $scope.chooseReport = function(report) {
    report.isChosen = true;
    $scope.chosenReportIds.push(report.reportId);
  };

  $scope.removeReport = function(report) {
    report.isChosen = false;
    $scope.chosenReportIds = _.without($scope.chosenReportIds, report.reportId);
  };

  $scope.ok = function() {
    return ProcedureMonitorstatDocument.save(
      {
        id: scModalParams.procedureId,
        version: scModalParams.version
      },
      $scope.chosenReportIds
    ).$promise.then(function(res) {
      if (res && res.error === 'reportAlreadyIncludedInProcedure') {
        var deferred = $q.defer(),
          promise;
        promise = deferred.promise;

        promise.then(function() {
          return scMessage('procedures_modals_chooseMonitorstatReport_cannotAddReport');
        });

        deferred.resolve($uibModalInstance.close());
      } else {
        return $uibModalInstance.close();
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };

  $scope.chooseAll = function() {
    var filteredReports = $scope.tableControl.getFilteredItems();
    _.forEach(filteredReports, function(proj) {
      if (_.contains($scope.chosenReportIds, proj.reportId)) {
        proj.isChosen = true;
      } else {
        $scope.chooseReport(proj);
      }
    });
    $scope.hasChoosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.monitorstatReports, function(report) {
      $scope.removeReport(report);
    });
    $scope.hasChoosenAll = true;
  };
}

ChooseMonitorstatReportsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scMessage',
  '$q',
  'scModalParams',
  'ProcedureMonitorstatDocument',
  'monitorstatReports'
];

ChooseMonitorstatReportsModalCtrl.$resolve = {
  monitorstatReports: [
    'ProcedureMonitorstatDocument',
    'scModalParams',
    function(ProcedureMonitorstatDocument, scModalParams) {
      return ProcedureMonitorstatDocument.getMonitorstatReports({
        id: scModalParams.procedureId
      }).$promise;
    }
  ]
};

export { ChooseMonitorstatReportsModalCtrl };
