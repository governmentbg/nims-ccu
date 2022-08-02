function EditFlatFinancialCorrectionItemModalCtrl(
  $scope,
  $injector,
  $uibModalInstance,
  scModalParams,
  moneyOperation,
  flatFinancialCorrectionItem
) {
  $scope.editMode = null;
  $scope.flatFinancialCorrectionItem = flatFinancialCorrectionItem;
  $scope.flatFinancialCorrectionId = scModalParams.flatFinancialCorrectionId;
  $scope.flatFinancialCorrectionStatus = scModalParams.status;

  $scope.$watch(
    '[flatFinancialCorrectionItem.euAmount, flatFinancialCorrectionItem.bgAmount]',
    function() {
      $scope.flatFinancialCorrectionItem.totalAmount = moneyOperation.addAmounts(
        $scope.flatFinancialCorrectionItem.euAmount,
        $scope.flatFinancialCorrectionItem.bgAmount
      );
    },
    true
  );

  $scope.save = function() {
    return $scope.editFlatFinancialCorrectionItemForm.$validate().then(function() {
      if ($scope.editFlatFinancialCorrectionItemForm.$valid) {
        var resourceName = null;

        if (scModalParams.level === 'programmePriority') {
          resourceName = 'FlatFinancialCorrectionProgrammePriorityItem';
        } else if (scModalParams.level === 'procedure') {
          resourceName = 'FlatFinancialCorrectionProcedureItem';
        } else if (scModalParams.level === 'contract') {
          resourceName = 'FlatFinancialCorrectionContractItem';
        } else if (scModalParams.level === 'contractContract') {
          resourceName = 'FlatFinancialCorrectionContractContractItem';
        }

        return $injector
          .get(resourceName)
          .update(
            {
              id: scModalParams.flatFinancialCorrectionId,
              ind: scModalParams.flatFinancialCorrectionLevelItemId,
              version: scModalParams.version
            },
            $scope.flatFinancialCorrectionItem
          )
          .$promise.then(function() {
            return $uibModalInstance.close();
          });
      }
    });
  };

  $scope.calculate = function() {
    if (!$scope.flatFinancialCorrectionItem.percent) {
      return;
    }
    if ($scope.flatFinancialCorrectionItem.percent > 10000) {
      return $scope.editFlatFinancialCorrectionItemForm.$validate();
    }

    var resourceName = null;

    if (scModalParams.level === 'programmePriority') {
      resourceName = 'FlatFinancialCorrectionProgrammePriorityItem';
    } else if (scModalParams.level === 'procedure') {
      resourceName = 'FlatFinancialCorrectionProcedureItem';
    } else if (scModalParams.level === 'contract') {
      resourceName = 'FlatFinancialCorrectionContractItem';
    } else if (scModalParams.level === 'contractContract') {
      resourceName = 'FlatFinancialCorrectionContractContractItem';
    }
    return $injector
      .get(resourceName)
      .calculate(
        {
          id: scModalParams.flatFinancialCorrectionId,
          ind: scModalParams.flatFinancialCorrectionLevelItemId
        },
        $scope.flatFinancialCorrectionItem
      )
      .$promise.then(function(result) {
        $scope.flatFinancialCorrectionItem.euAmount = result.euAmount;
        $scope.flatFinancialCorrectionItem.bgAmount = result.bgAmount;
        $scope.flatFinancialCorrectionItem.totalAmount = result.totalAmount;
      });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

EditFlatFinancialCorrectionItemModalCtrl.$inject = [
  '$scope',
  '$injector',
  '$uibModalInstance',
  'scModalParams',
  'moneyOperation',
  'flatFinancialCorrectionItem'
];

EditFlatFinancialCorrectionItemModalCtrl.$resolve = {
  flatFinancialCorrectionItem: [
    'scModalParams',
    'FlatFinancialCorrectionProgrammePriorityItem',
    'FlatFinancialCorrectionProcedureItem',
    'FlatFinancialCorrectionContractItem',
    'FlatFinancialCorrectionContractContractItem',
    function(
      scModalParams,
      FlatFinancialCorrectionProgrammePriorityItem,
      FlatFinancialCorrectionProcedureItem,
      FlatFinancialCorrectionContractItem,
      FlatFinancialCorrectionContractContractItem
    ) {
      var params = {
        id: scModalParams.flatFinancialCorrectionId,
        ind: scModalParams.flatFinancialCorrectionLevelItemId
      };

      if (scModalParams.level === 'programmePriority') {
        return FlatFinancialCorrectionProgrammePriorityItem.get(params).$promise;
      } else if (scModalParams.level === 'procedure') {
        return FlatFinancialCorrectionProcedureItem.get(params).$promise;
      } else if (scModalParams.level === 'contract') {
        return FlatFinancialCorrectionContractItem.get(params).$promise;
      } else if (scModalParams.level === 'contractContract') {
        return FlatFinancialCorrectionContractContractItem.get(params).$promise;
      }
    }
  ]
};

export { EditFlatFinancialCorrectionItemModalCtrl };
