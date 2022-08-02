import angular from 'angular';

function BasicInterestRatesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scMessage,
  scConfirm,
  scModal,
  BasicInterestRate,
  InterestRate,
  basicInterestRate
) {
  $scope.editMode = null;
  $scope.basicInterestRate = basicInterestRate;

  $scope.save = function() {
    return $scope.editBasicInterestRateForm.$validate().then(function() {
      if ($scope.editBasicInterestRateForm.$valid) {
        return BasicInterestRate.update(
          {
            id: $stateParams.id
          },
          $scope.basicInterestRate.basicInterestRateData
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'BasicInterestRate',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.basicInterestRate.basicInterestRateData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.basicInterestRates.search');
      }
    });
  };

  $scope.newRate = function() {
    var modalInstance = scModal.open('newInterestRateModal', {
      basicInterestRateId: $stateParams.id
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.editRate = function(interestRateId) {
    var modalInstance = scModal.open('editInterestRateModal', {
      basicInterestRateId: $stateParams.id,
      interestRateId: interestRateId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteRate = function(interestRateId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'InterestRate',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: interestRateId,
        version: $scope.basicInterestRate.basicInterestRateData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

BasicInterestRatesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scMessage',
  'scConfirm',
  'scModal',
  'BasicInterestRate',
  'InterestRate',
  'basicInterestRate'
];

BasicInterestRatesEditCtrl.$resolve = {
  basicInterestRate: [
    'BasicInterestRate',
    '$stateParams',
    function(BasicInterestRate, $stateParams) {
      return BasicInterestRate.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { BasicInterestRatesEditCtrl };
