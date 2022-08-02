function ProcedureMassCommunicationsViewCtrl($scope, $stateParams, info) {
  $scope.procedureMassCommunicationInfo = info;

  $scope.tabList = {
    procedureMassCommunications_tabs_data: 'root.procedureMassCommunications.view.edit',
    procedureMassCommunications_tabs_documents: 'root.procedureMassCommunications.view.documents',
    procedureMassCommunications_tabs_recipients: 'root.procedureMassCommunications.view.recipients'
  };
}

ProcedureMassCommunicationsViewCtrl.$inject = ['$scope', '$stateParams', 'info'];

ProcedureMassCommunicationsViewCtrl.$resolve = {
  info: [
    'ProcedureMassCommunication',
    '$stateParams',
    function(ProcedureMassCommunication, $stateParams) {
      return ProcedureMassCommunication.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureMassCommunicationsViewCtrl };
