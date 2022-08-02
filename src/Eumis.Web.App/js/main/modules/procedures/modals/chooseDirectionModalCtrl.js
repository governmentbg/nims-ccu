import _ from 'lodash';

function ChooseDirectionModalCtrl(
  $scope,
  $uibModalInstance,
  scMessage,
  $q,
  scModalParams,
  ProcedureDirections,
  directions
) {
  $scope.chosenDirectionIds = [];
  $scope.hasChoosenAll = true;
  $scope.directions = directions;
  $scope.tableControl = {};

  $scope.chooseDirection = function(direction) {
    direction.isChosen = true;
    $scope.chosenDirectionIds.push(direction.mapNodeDirectionId);
  };

  $scope.removeDirection = function(direction) {
    direction.isChosen = false;
    $scope.chosenDirectionIds = _.without($scope.chosenDirectionIds, direction.mapNodeDirectionId);
  };

  $scope.ok = function() {
    return ProcedureDirections.save(
      {
        id: scModalParams.procedureId,
        version: scModalParams.version
      },
      $scope.chosenDirectionIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };

  $scope.chooseAll = function() {
    var filteredDirections = $scope.tableControl.getFilteredItems();
    _.forEach(filteredDirections, function(dir) {
      if (_.includes($scope.chosenDirectionIds, dir.mapNodeDirectionId)) {
        dir.isChosen = true;
      } else {
        $scope.chooseDirection(dir);
      }
    });
    $scope.hasChoosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.directions, function(dir) {
      $scope.removeDirection(dir);
    });
    $scope.hasChoosenAll = true;
  };
}

ChooseDirectionModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scMessage',
  '$q',
  'scModalParams',
  'ProcedureDirection',
  'directions'
];

ChooseDirectionModalCtrl.$resolve = {
  directions: [
    'ProcedureDirection',
    'scModalParams',
    function(ProcedureDirection, scModalParams) {
      return ProcedureDirection.getDirections({
        id: scModalParams.procedureId
      }).$promise;
    }
  ]
};

export { ChooseDirectionModalCtrl };
