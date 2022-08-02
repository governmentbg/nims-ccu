import _ from 'lodash';

function ChooseNSPPrioritiesModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  NotificationSettingAttachedProgrammePriority,
  pPriorities
) {
  $scope.chosenPPriorityIds = [];
  $scope.pPriorities = pPriorities;
  $scope.hasChoosenAll = true;
  $scope.tableControl = {};

  $scope.chooseAll = function() {
    var filteredPPriorities = $scope.tableControl.getFilteredItems();
    _.forEach(filteredPPriorities, function(cr) {
      if (_.includes($scope.chosenPPriorityIds, cr.pPriorityId)) {
        cr.isChosen = true;
      } else {
        $scope.choose(cr);
      }
    });
    $scope.hasChoosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.pPriorities, function(cr) {
      $scope.remove(cr);
    });
    $scope.hasChoosenAll = true;
  };

  $scope.choose = function(pPriority) {
    pPriority.isChosen = true;
    $scope.chosenPPriorityIds.push(pPriority.itemId);
  };

  $scope.remove = function(pPriority) {
    pPriority.isChosen = false;
    $scope.chosenPPriorityIds = _.without($scope.chosenPPriorityIds, pPriority.itemId);
  };

  $scope.ok = function() {
    return NotificationSettingAttachedProgrammePriority.save(
      {
        id: scModalParams.notificationSettingId,
        version: scModalParams.version
      },
      $scope.chosenPPriorityIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseNSPPrioritiesModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'NotificationSettingAttachedProgrammePriority',
  'pPriorities'
];

ChooseNSPPrioritiesModalCtrl.$resolve = {
  pPriorities: [
    'NotificationSettingAttachedProgrammePriority',
    'scModalParams',
    function(NotificationSettingAttachedProgrammePriority, scModalParams) {
      return NotificationSettingAttachedProgrammePriority.getPPriorities({
        id: scModalParams.notificationSettingId
      }).$promise;
    }
  ]
};

export { ChooseNSPPrioritiesModalCtrl };
