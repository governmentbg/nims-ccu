import _ from 'lodash';

function ChooseProcedureItemsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  FlatFinancialCorrectionProcedureItem,
  procedures
) {
  $scope.form = {};
  $scope.chosenProcedureIds = [];
  $scope.procedures = procedures;

  $scope.choose = function(procedure) {
    procedure.isChosen = true;
    $scope.chosenProcedureIds.push(procedure.itemId);
  };

  $scope.remove = function(procedure) {
    procedure.isChosen = false;
    $scope.chosenProcedureIds = _.without($scope.chosenProcedureIds, procedure.itemId);
  };

  $scope.ok = function() {
    return FlatFinancialCorrectionProcedureItem.save(
      {
        id: scModalParams.flatFinancialCorrectionId,
        version: scModalParams.version
      },
      $scope.chosenProcedureIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseProcedureItemsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'FlatFinancialCorrectionProcedureItem',
  'procedures'
];

ChooseProcedureItemsModalCtrl.$resolve = {
  procedures: [
    'FlatFinancialCorrectionProcedureItem',
    'scModalParams',
    function(FlatFinancialCorrectionProcedureItem, scModalParams) {
      return FlatFinancialCorrectionProcedureItem.getProcedures({
        id: scModalParams.flatFinancialCorrectionId
      }).$promise;
    }
  ]
};

export { ChooseProcedureItemsModalCtrl };
