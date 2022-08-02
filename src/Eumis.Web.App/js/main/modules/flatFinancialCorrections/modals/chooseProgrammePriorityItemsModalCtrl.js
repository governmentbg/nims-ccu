import _ from 'lodash';

function ChooseProgrammePriorityItemsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  FlatFinancialCorrectionProgrammePriorityItem,
  programmePriorities
) {
  $scope.form = {};
  $scope.chosenProgrammePriorityIds = [];
  $scope.programmePriorities = programmePriorities;

  $scope.choose = function(programmePriority) {
    programmePriority.isChosen = true;
    $scope.chosenProgrammePriorityIds.push(programmePriority.itemId);
  };

  $scope.remove = function(programmePriority) {
    programmePriority.isChosen = false;
    $scope.chosenProgrammePriorityIds = _.without(
      $scope.chosenProgrammePriorityIds,
      programmePriority.itemId
    );
  };

  $scope.ok = function() {
    return FlatFinancialCorrectionProgrammePriorityItem.save(
      {
        id: scModalParams.flatFinancialCorrectionId,
        version: scModalParams.version
      },
      $scope.chosenProgrammePriorityIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseProgrammePriorityItemsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'FlatFinancialCorrectionProgrammePriorityItem',
  'programmePriorities'
];

ChooseProgrammePriorityItemsModalCtrl.$resolve = {
  programmePriorities: [
    'FlatFinancialCorrectionProgrammePriorityItem',
    'scModalParams',
    function(FlatFinancialCorrectionProgrammePriorityItem, scModalParams) {
      return FlatFinancialCorrectionProgrammePriorityItem.getProgrammePriorities({
        id: scModalParams.flatFinancialCorrectionId
      }).$promise;
    }
  ]
};

export { ChooseProgrammePriorityItemsModalCtrl };
