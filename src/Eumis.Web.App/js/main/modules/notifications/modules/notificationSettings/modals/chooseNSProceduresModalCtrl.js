import _ from 'lodash';

function ChooseNSProceduresModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  NotificationSettingAttachedProcedure,
  procedures
) {
  $scope.chosenProcedureIds = [];
  $scope.procedures = procedures;
  $scope.hasChoosenAll = true;
  $scope.tableControl = {};

  $scope.chooseAll = function() {
    var filteredProcedures = $scope.tableControl.getFilteredItems();
    _.forEach(filteredProcedures, function(cr) {
      if (_.includes($scope.chosenProcedureIds, cr.procedureId)) {
        cr.isChosen = true;
      } else {
        $scope.choose(cr);
      }
    });
    $scope.hasChoosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.procedures, function(cr) {
      $scope.remove(cr);
    });
    $scope.hasChoosenAll = true;
  };

  $scope.choose = function(procedure) {
    procedure.isChosen = true;
    $scope.chosenProcedureIds.push(procedure.procedureId);
  };

  $scope.remove = function(procedure) {
    procedure.isChosen = false;
    $scope.chosenProcedureIds = _.without($scope.chosenProcedureIds, procedure.ProcedureId);
  };

  $scope.ok = function() {
    return NotificationSettingAttachedProcedure.save(
      {
        id: scModalParams.notificationSettingId,
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

ChooseNSProceduresModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'NotificationSettingAttachedProcedure',
  'procedures'
];

ChooseNSProceduresModalCtrl.$resolve = {
  procedures: [
    'NotificationSettingAttachedProcedure',
    'scModalParams',
    function(NotificationSettingAttachedProcedure, scModalParams) {
      return NotificationSettingAttachedProcedure.getProcedures({
        id: scModalParams.notificationSettingId
      }).$promise;
    }
  ]
};

export { ChooseNSProceduresModalCtrl };
