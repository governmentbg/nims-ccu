import _ from 'lodash';
import angular from 'angular';

function ProceduresMapCtrl($scope, $state, procedureProgrammes, scModal) {
  $scope.currentNodeLevel = null;
  $scope.currentNodeData = null;

  $scope.onSelectHandler = function(branch) {
    $scope.currentNodeLevel = branch.level;
    $scope.currentNodeData = branch.data;
  };

  $scope.addNew = function() {
    return $state.go('root.procedures.new', {
      programmeId: $scope.currentNodeData.programmeId,
      programmePriorityId: $scope.currentNodeData.programmePriorityId
    });
  };

  $scope.copy = function() {
    var modalInstance = scModal.open('chooseProcedureModal', {
      programmeId: $scope.currentNodeData.programmeId,
      programmePriorityId: $scope.currentNodeData.programmePriorityId
    });

    modalInstance.result.then(function() {
      return $state.reload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.search = function() {
    return $state.go('root.procedures.search', {
      programmeId: $scope.currentNodeData.programmeId,
      programmePriorityId: $scope.currentNodeData.programmePriorityId
    });
  };

  $scope.view = function() {
    return $state.go('root.procedures.view.edit', {
      id: $scope.currentNodeData.procedureId
    });
  };

  $scope.procedureProgrammes = _.map(procedureProgrammes, function(p) {
    return {
      label: p.name,
      data: p,
      children: _.map(p.programmePriorities, function(pp) {
        return {
          label: pp.name,
          data: pp,
          children: _.map(pp.procedures, function(pr) {
            return {
              label: pr.name,
              data: pr
            };
          })
        };
      })
    };
  });
}

ProceduresMapCtrl.$inject = ['$scope', '$state', 'procedureProgrammes', 'scModal'];

ProceduresMapCtrl.$resolve = {
  procedureProgrammes: [
    'Procedure',
    function(Procedure) {
      return Procedure.getTree().$promise;
    }
  ]
};

export { ProceduresMapCtrl };
