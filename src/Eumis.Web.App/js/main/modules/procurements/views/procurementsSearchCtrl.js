function ProcurementsSearchCtrl($scope, $state, $stateParams, procurements) {
  $scope.procurements = procurements;
}

ProcurementsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'procurements'];

ProcurementsSearchCtrl.$resolve = {
  procurements: [
    '$stateParams',
    'Procurement',
    function($stateParams, Procurement) {
      return Procurement.query($stateParams).$promise;
    }
  ]
};

export { ProcurementsSearchCtrl };
