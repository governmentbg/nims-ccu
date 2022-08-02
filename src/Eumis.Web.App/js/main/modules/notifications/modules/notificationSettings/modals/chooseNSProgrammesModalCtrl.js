import _ from 'lodash';

function ChooseNSProgrammesModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  NotificationSettingAttachedProgramme,
  programmes
) {
  $scope.chosenProgrammeIds = [];
  $scope.programmes = programmes;
  $scope.hasChoosenAll = true;
  $scope.tableControl = {};

  $scope.chooseAll = function() {
    var filteredProgrammes = $scope.tableControl.getFilteredItems();
    _.forEach(filteredProgrammes, function(cr) {
      if (_.includes($scope.chosenProgrammeIds, cr.nomValueId)) {
        cr.isChosen = true;
      } else {
        $scope.choose(cr);
      }
    });
    $scope.hasChoosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.programmes, function(cr) {
      $scope.remove(cr);
    });
    $scope.hasChoosenAll = true;
  };

  $scope.choose = function(programme) {
    programme.isChosen = true;
    $scope.chosenProgrammeIds.push(programme.nomValueId);
  };

  $scope.remove = function(programme) {
    programme.isChosen = false;
    $scope.chosenProgrammeIds = _.without($scope.chosenProgrammeIds, programme.nomValueId);
  };

  $scope.ok = function() {
    return NotificationSettingAttachedProgramme.save(
      {
        id: scModalParams.notificationSettingId,
        version: scModalParams.version
      },
      $scope.chosenProgrammeIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseNSProgrammesModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'NotificationSettingAttachedProgramme',
  'programmes'
];

ChooseNSProgrammesModalCtrl.$resolve = {
  programmes: [
    'NotificationSettingAttachedProgramme',
    'scModalParams',
    function(NotificationSettingAttachedProgramme, scModalParams) {
      return NotificationSettingAttachedProgramme.getProgrammes({
        id: scModalParams.notificationSettingId
      }).$promise;
    }
  ]
};

export { ChooseNSProgrammesModalCtrl };
