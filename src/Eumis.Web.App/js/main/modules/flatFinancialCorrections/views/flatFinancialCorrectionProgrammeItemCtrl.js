function FlatFinancialCorrectionProgrammeItemCtrl(
  $scope,
  $state,
  scModal,
  scConfirm,
  $stateParams,
  moneyOperation,
  FlatFinancialCorrectionProgrammeItem,
  flatFinancialCorrectionProgrammeItem
) {
  $scope.editMode = null;
  $scope.flatFinancialCorrectionId = $stateParams.id;
  $scope.flatFinancialCorrectionProgrammeItem = flatFinancialCorrectionProgrammeItem;
  $scope.flatFinancialCorrectionStatus = $scope.flatFinancialCorrectionInfo.status;

  $scope.$watch(
    '[flatFinancialCorrectionProgrammeItem.euAmount, ' +
      'flatFinancialCorrectionProgrammeItem.bgAmount]',
    function() {
      $scope.flatFinancialCorrectionProgrammeItem.totalAmount = moneyOperation.addAmounts(
        $scope.flatFinancialCorrectionProgrammeItem.euAmount,
        $scope.flatFinancialCorrectionProgrammeItem.bgAmount
      );
    },
    true
  );

  $scope.save = function() {
    return $scope.editFlatFinancialCorrectionProgrammeItem.$validate().then(function() {
      if ($scope.editFlatFinancialCorrectionProgrammeItem.$valid) {
        return FlatFinancialCorrectionProgrammeItem.update(
          {
            id: $stateParams.id,
            ind: $scope.flatFinancialCorrectionProgrammeItem.flatFinancialCorrectionLevelItemId,
            version: flatFinancialCorrectionProgrammeItem.version
          },
          $scope.flatFinancialCorrectionProgrammeItem
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.calculate = function() {
    if (!$scope.flatFinancialCorrectionProgrammeItem.percent) {
      return;
    }
    if ($scope.flatFinancialCorrectionProgrammeItem.percent > 10000) {
      return $scope.editFlatFinancialCorrectionProgrammeItem.$validate();
    }

    return FlatFinancialCorrectionProgrammeItem.calculate(
      {
        id: $stateParams.id,
        ind: $scope.flatFinancialCorrectionProgrammeItem.flatFinancialCorrectionLevelItemId
      },
      $scope.flatFinancialCorrectionProgrammeItem
    ).$promise.then(function(result) {
      $scope.flatFinancialCorrectionProgrammeItem.euAmount = result.euAmount;
      $scope.flatFinancialCorrectionProgrammeItem.bgAmount = result.bgAmount;
      $scope.flatFinancialCorrectionProgrammeItem.totalAmount = result.totalAmount;
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

FlatFinancialCorrectionProgrammeItemCtrl.$inject = [
  '$scope',
  '$state',
  'scModal',
  'scConfirm',
  '$stateParams',
  'moneyOperation',
  'FlatFinancialCorrectionProgrammeItem',
  'flatFinancialCorrectionProgrammeItem'
];

FlatFinancialCorrectionProgrammeItemCtrl.$resolve = {
  flatFinancialCorrectionProgrammeItem: [
    '$stateParams',
    'FlatFinancialCorrectionProgrammeItem',
    function($stateParams, FlatFinancialCorrectionProgrammeItem) {
      return FlatFinancialCorrectionProgrammeItem.get($stateParams).$promise;
    }
  ]
};

export { FlatFinancialCorrectionProgrammeItemCtrl };
