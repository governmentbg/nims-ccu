import _ from 'lodash';

function OperationalMapCtrl($scope, $state, programmes) {
  $scope.currentNodeLevel = null;
  $scope.currentNodeData = null;
  $scope.mapNodeId = null;

  $scope.onSelectHandler = function(branch) {
    $scope.currentNodeLevel = branch.level;
    $scope.currentNodeData = branch.data;
    switch ($scope.currentNodeLevel) {
      case 1:
        $scope.mapNodeId = $scope.currentNodeData.programmeId;
        break;
      case 2:
        $scope.mapNodeId = $scope.currentNodeData.programmePriorityId;
        break;
      case 3:
        $scope.mapNodeId = $scope.currentNodeData.investmentPriorityId;
        break;
      case 4:
        $scope.mapNodeId = $scope.currentNodeData.specificTargetId;
        break;
    }
  };

  $scope.view = function() {
    if ($scope.currentNodeLevel === 1) {
      return $state.go('root.map.programmes.view.edit', {
        id: $scope.currentNodeData.programmeId
      });
    } else if ($scope.currentNodeLevel === 2) {
      return $state.go('root.map.ppriorities.view.edit', {
        id: $scope.currentNodeData.programmePriorityId
      });
    } else if ($scope.currentNodeLevel === 3) {
      return $state.go('root.map.ipriorities.view.edit', {
        id: $scope.currentNodeData.investmentPriorityId
      });
    } else if ($scope.currentNodeLevel === 4) {
      return $state.go('root.map.targets.view.edit', {
        id: $scope.currentNodeData.specificTargetId
      });
    }
  };

  $scope.programmes = _.map(programmes, function(p) {
    return {
      label: p.name,
      data: p,
      children: _.map(p.programmePriorities, function(pp) {
        return {
          label: pp.name,
          data: pp,
          children: _.map(pp.investmentPriorities, function(ip) {
            return {
              label: ip.name,
              data: ip,
              children: _.map(ip.specificTargets, function(st) {
                return {
                  label: st.name,
                  data: st
                };
              })
            };
          })
        };
      })
    };
  });
}

OperationalMapCtrl.$inject = ['$scope', '$state', 'programmes'];

OperationalMapCtrl.$resolve = {
  programmes: [
    'Programme',
    function(Programme) {
      return Programme.query().$promise;
    }
  ]
};

export { OperationalMapCtrl };
