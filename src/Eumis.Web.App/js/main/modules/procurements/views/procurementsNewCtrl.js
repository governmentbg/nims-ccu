function ProcurementsNewCtrl($scope, $state, Procurement, procurement) {
  $scope.procurement = procurement;

  $scope.save = function() {
    return $scope.newProcurementForm.$validate().then(function() {
      if ($scope.newProcurementForm.$valid) {
        return Procurement.save($scope.procurement).$promise.then(function(result) {
          return $state.go('root.procurements.view.edit', {
            id: result.procurementId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procurements.search');
  };
}

ProcurementsNewCtrl.$inject = ['$scope', '$state', 'Procurement', 'procurement'];

ProcurementsNewCtrl.$resolve = {
  procurement: [
    'Procurement',
    function(Procurement) {
      return Procurement.newProcurement().$promise;
    }
  ]
};

export { ProcurementsNewCtrl };
