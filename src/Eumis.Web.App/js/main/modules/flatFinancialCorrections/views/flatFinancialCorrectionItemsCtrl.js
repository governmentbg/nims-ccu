import angular from 'angular';

function FlatFinancialCorrectionItemsCtrl(
  $scope,
  $state,
  scModal,
  scConfirm,
  $stateParams,
  flatFinancialCorrectionItems
) {
  $scope.flatFinancialCorrectionId = $stateParams.id;
  $scope.flatFinancialCorrectionItems = flatFinancialCorrectionItems;
  $scope.flatFinancialCorrectionContractId = $scope.flatFinancialCorrectionInfo.contractId;
  $scope.flatFinancialCorrectionLevel = $scope.flatFinancialCorrectionInfo.level;
  $scope.flatFinancialCorrectionStatus = $scope.flatFinancialCorrectionInfo.status;

  $scope.chooseItems = function() {
    var modalName, modalInstance;
    if ($scope.flatFinancialCorrectionInfo.level === 'programmePriority') {
      modalName = 'chooseProgrammePriorityItemsModal';
    } else if ($scope.flatFinancialCorrectionInfo.level === 'procedure') {
      modalName = 'chooseProcedureItemsModal';
    } else if ($scope.flatFinancialCorrectionInfo.level === 'contract') {
      modalName = 'chooseContractItemsModal';
    } else if ($scope.flatFinancialCorrectionInfo.level === 'contractContract') {
      modalName = 'chooseContractContractItemsModal';
    }

    modalInstance = scModal.open(modalName, {
      flatFinancialCorrectionId: $scope.flatFinancialCorrectionInfo.flatFinancialCorrectionId,
      flatFinancialCorrectionLevel: $scope.flatFinancialCorrectionLevel,
      version: $scope.flatFinancialCorrectionInfo.version,
      flatFinancialCorrectionContractId: $scope.flatFinancialCorrectionContractId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.editItem = function(flatFinancialCorrectionLevelItemId) {
    var modalInstance = scModal.open('editFlatFinancialCorrectionItemModal', {
      flatFinancialCorrectionId: $scope.flatFinancialCorrectionInfo.flatFinancialCorrectionId,
      flatFinancialCorrectionLevelItemId: flatFinancialCorrectionLevelItemId,
      version: $scope.flatFinancialCorrectionInfo.version,
      level: $scope.flatFinancialCorrectionLevel,
      status: $scope.flatFinancialCorrectionStatus
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(flatFinancialCorrectionLevelItemId) {
    var resource = null;
    if ($scope.flatFinancialCorrectionInfo.level === 'programmePriority') {
      resource = 'FlatFinancialCorrectionProgrammePriorityItem';
    } else if ($scope.flatFinancialCorrectionInfo.level === 'procedure') {
      resource = 'FlatFinancialCorrectionProcedureItem';
    } else if ($scope.flatFinancialCorrectionInfo.level === 'contract') {
      resource = 'FlatFinancialCorrectionContractItem';
    } else if ($scope.flatFinancialCorrectionInfo.level === 'contractContract') {
      resource = 'FlatFinancialCorrectionContractContractItem';
    }
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: resource,
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: flatFinancialCorrectionLevelItemId,
        version: $scope.flatFinancialCorrectionInfo.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

FlatFinancialCorrectionItemsCtrl.$inject = [
  '$scope',
  '$state',
  'scModal',
  'scConfirm',
  '$stateParams',
  'flatFinancialCorrectionItems'
];

FlatFinancialCorrectionItemsCtrl.$resolve = {
  flatFinancialCorrectionItems: [
    '$stateParams',
    'FlatFinancialCorrectionProgrammePriorityItem',
    'FlatFinancialCorrectionProcedureItem',
    'FlatFinancialCorrectionContractItem',
    'FlatFinancialCorrectionContractContractItem',
    'flatFinancialCorrectionInfo',
    function(
      $stateParams,
      FlatFinancialCorrectionProgrammePriorityItem,
      FlatFinancialCorrectionProcedureItem,
      FlatFinancialCorrectionContractItem,
      FlatFinancialCorrectionContractContractItem,
      flatFinancialCorrectionInfo
    ) {
      if (flatFinancialCorrectionInfo.level === 'programmePriority') {
        return FlatFinancialCorrectionProgrammePriorityItem.query($stateParams).$promise;
      } else if (flatFinancialCorrectionInfo.level === 'procedure') {
        return FlatFinancialCorrectionProcedureItem.query($stateParams).$promise;
      } else if (flatFinancialCorrectionInfo.level === 'contract') {
        return FlatFinancialCorrectionContractItem.query($stateParams).$promise;
      } else if (flatFinancialCorrectionInfo.level === 'contractContract') {
        return FlatFinancialCorrectionContractContractItem.query($stateParams).$promise;
      }
    }
  ]
};

export { FlatFinancialCorrectionItemsCtrl };
