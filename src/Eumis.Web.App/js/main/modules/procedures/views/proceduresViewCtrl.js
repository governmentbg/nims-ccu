import _ from 'lodash';
function ProceduresViewCtrl($scope, $stateParams, info) {
  $scope.info = info;

  $scope.tabList = {
    procedures_tabs_procedureData: 'root.procedures.view.edit',
    procedures_tabs_directions: 'root.procedures.view.directions',
    procedures_tabs_applicableSections: 'root.procedures.view.sections',
    procedures_tabs_shares: 'root.procedures.view.procedureShares',
    procedures_tabs_expenseBudgets: 'root.procedures.view.ProcedureExpenseBudgets',
    procedures_tabs_monitorstat: 'root.procedures.view.monitorstat'
  };

  if ($scope.info.isIndicatorVisible) {
    _.assign($scope.tabList, {
      procedures_tabs_indicators: 'root.procedures.view.indicators'
    });
  }

  if ($scope.info.isTimeLimitsVisible) {
    _.assign($scope.tabList, {
      procedures_tabs_timeLimits: 'root.procedures.view.procedureTimeLimits'
    });
  }

  if ($scope.info.isAdditionalInformationVisible) {
    _.assign($scope.tabList, {
      procedures_tabs_specFields: 'root.procedures.view.procedureSpecFields'
    });
  }

  _.assign($scope.tabList, {
    procedures_tabs_documents: 'root.procedures.view.allDocs',
    procedures_tabs_reportDocuments: 'root.procedures.view.reportDocs'
  });
}

ProceduresViewCtrl.$inject = ['$scope', '$stateParams', 'info'];

ProceduresViewCtrl.$resolve = {
  info: [
    'Procedure',
    '$stateParams',
    function(Procedure, $stateParams) {
      return Procedure.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProceduresViewCtrl };
