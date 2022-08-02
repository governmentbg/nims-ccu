function ProcurementsViewCtrl($scope, $stateParams, info) {
  $scope.info = info;

  $scope.tabList = {
    procurements_tabs_procedureData: 'root.procurements.view.edit',
    procurements_tabs_differentiatedPositions: 'root.procurements.view.differentiatedPosition',
    procurements_tabs_documents: 'root.procurements.view.documents'
  };
}

ProcurementsViewCtrl.$inject = ['$scope', '$stateParams', 'info'];

ProcurementsViewCtrl.$resolve = {
  info: [
    'Procurement',
    '$stateParams',
    function(Procurement, $stateParams) {
      return Procurement.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcurementsViewCtrl };
