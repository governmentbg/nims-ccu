import _ from 'lodash';

function EvalSessionDistributionsNewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scMessage,
  EvalSessionDistribution,
  newEvalSessionDistribution
) {
  $scope.newEvalSessionDistribution = newEvalSessionDistribution;
  $scope.yesText = l10n.get('common_texts_yes');
  $scope.noText = l10n.get('common_texts_no');
  $scope.requiredText = l10n.get('defaultErrorTexts_required');

  $scope.newEvalSessionDistribution.projects.forEach(function(p) {
    p.isSystemDeleted = p.isDeleted;
  });

  $scope.calculateAllAdded = function() {
    $scope.allAdded = _.every($scope.newEvalSessionDistribution.projects, function(p) {
      return p.isSystemDeleted || !p.isDeleted;
    });
  };
  $scope.calculateAllAdded();

  $scope.save = function() {
    return $scope.newEvalSessionDistributionForm.$validate().then(function() {
      var includedProjects = _.filter($scope.newEvalSessionDistribution.projects, function(p) {
          return p.isDeleted === false;
        }),
        excludedProjectsWithoutNote = _.filter($scope.newEvalSessionDistribution.projects, function(
          p
        ) {
          return p.isDeleted === true && !p.isDeletedNote;
        });

      if ($scope.newEvalSessionDistributionForm.$invalid) {
        return;
      } else if (includedProjects.length === 0) {
        return scMessage('evalSessions_newEvalSessionDistribution_noProjects', [
          {
            name: 'OK',
            l10nLabel: 'scaffolding.scMessage.okButton',
            type: 'btn-primary',
            icon: 'glyphicon-ok'
          }
        ]);
      } else if (excludedProjectsWithoutNote.length > 0) {
        return scMessage('evalSessions_newEvalSessionDistribution_noIsDeletedNote', [
          {
            name: 'OK',
            l10nLabel: 'scaffolding.scMessage.okButton',
            type: 'btn-primary',
            icon: 'glyphicon-ok'
          }
        ]);
      }

      return EvalSessionDistribution.save(
        { id: $stateParams.id },
        $scope.newEvalSessionDistribution
      ).$promise.then(function() {
        return $state.go('root.evalSessions.view.distributions.search');
      });
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.distributions.search');
  };

  $scope.deleteProject = function(p) {
    p.isDeleted = true;
    $scope.calculateAllAdded();
  };

  $scope.addProject = function(p) {
    p.isDeleted = false;
    p.isDeletedNote = null;
    $scope.calculateAllAdded();
  };

  $scope.deleteAll = function() {
    $scope.newEvalSessionDistribution.projects.forEach(function(p) {
      p.isDeleted = true;
    });
    $scope.allAdded = false;
  };

  $scope.addAll = function() {
    $scope.newEvalSessionDistribution.projects.forEach(function(p) {
      if (!p.isSystemDeleted) {
        p.isDeleted = false;
        p.isDeletedNote = null;
      }
    });
    $scope.allAdded = true;
  };
}

EvalSessionDistributionsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scMessage',
  'EvalSessionDistribution',
  'newEvalSessionDistribution'
];

EvalSessionDistributionsNewCtrl.$resolve = {
  newEvalSessionDistribution: [
    '$stateParams',
    'EvalSessionDistribution',
    function($stateParams, EvalSessionDistribution) {
      return EvalSessionDistribution.newEvalSessionDistribution({
        id: $stateParams.id,
        evalTableType: $stateParams.t
      }).$promise;
    }
  ]
};

export { EvalSessionDistributionsNewCtrl };
