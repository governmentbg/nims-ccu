import angular from 'angular';

function ProcedureMonitorstatSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  ProcedureMonitorstatDocument,
  ProcedureMonitorstatRequest,
  monitorstatDocuments,
  monitorstatRequests,
  monitorstatEconomicActivities
) {
  $scope.procedureId = $stateParams.id;
  $scope.procedureVersion = $scope.info.version;
  $scope.monitorstatDocuments = monitorstatDocuments;
  $scope.monitorstatEconomicActivities = monitorstatEconomicActivities;
  $scope.monitorstatRequests = monitorstatRequests;

  $scope.removeItem = function(item) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureMonitorstatDocument',
      action: 'remove',
      params: {
        id: $scope.procedureId,
        version: $scope.procedureVersion,
        ind: item.procedureMonitorstatDocumentId
      }
    }).then(function() {
      return $state.reload();
    });
  };

  $scope.removeActivity = function(item) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureMonitorstatEconomicActivity',
      action: 'remove',
      params: {
        id: $scope.procedureId,
        version: item.version,
        ind: item.procedureMonitorstatEconomicActivityId
      }
    }).then(function() {
      return $state.reload();
    });
  };

  $scope.chooseReport = function() {
    var modalInstance = scModal.open('chooseMonitorstatReportModal', {
      procedureId: $scope.procedureId,
      version: $scope.procedureVersion
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);
  };

  $scope.sendRequest = function() {
    return ProcedureMonitorstatRequest.sendDocumentRequest({
      id: $scope.procedureId
    }).$promise.then(() => {
      return $state.partialReload();
    });
  };

  $scope.removeRequset = function(item) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureMonitorstatRequest',
      action: 'remove',
      params: {
        id: $scope.procedureId,
        version: item.version,
        ind: item.procedureMonitorstatRequestId
      }
    }).then(function() {
      return $state.reload();
    });
  };
}

ProcedureMonitorstatSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'ProcedureMonitorstatDocument',
  'ProcedureMonitorstatRequest',
  'monitorstatDocuments',
  'monitorstatRequests',
  'monitorstatEconomicActivities'
];

ProcedureMonitorstatSearchCtrl.$resolve = {
  monitorstatDocuments: [
    '$stateParams',
    'ProcedureMonitorstatDocument',
    function($stateParams, ProcedureMonitorstatDocument) {
      return ProcedureMonitorstatDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  monitorstatRequests: [
    '$stateParams',
    'ProcedureMonitorstatRequest',
    function($stateParams, ProcedureMonitorstatRequest) {
      return ProcedureMonitorstatRequest.query({ id: $stateParams.id }).$promise;
    }
  ],
  monitorstatEconomicActivities: [
    '$stateParams',
    'ProcedureMonitorstatEconomicActivity',
    function($stateParams, ProcedureMonitorstatEconomicActivity) {
      return ProcedureMonitorstatEconomicActivity.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureMonitorstatSearchCtrl };
