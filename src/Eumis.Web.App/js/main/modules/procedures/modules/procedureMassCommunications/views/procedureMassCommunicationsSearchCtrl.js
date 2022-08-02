function ProcedureMassCommunicationsSearchCtrl($scope, communications) {
  $scope.procedureMassCommunications = communications;
}

ProcedureMassCommunicationsSearchCtrl.$inject = ['$scope', 'communications'];

ProcedureMassCommunicationsSearchCtrl.$resolve = {
  communications: [
    'ProcedureMassCommunication',
    ProcedureMassCommunication => ProcedureMassCommunication.query().$promise
  ]
};

export { ProcedureMassCommunicationsSearchCtrl };
