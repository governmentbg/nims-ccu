import _ from 'lodash';

function EvalSessionDistributionCtrl($scope, l10n) {
  $scope.yesText = l10n.get('common_texts_yes');
  $scope.noText = l10n.get('common_texts_no');

  $scope.deleteAssessor = function(assessor) {
    assessor.isDeleted = true;
  };

  $scope.addAssessor = function(assessor) {
    assessor.isDeleted = false;
    assessor.isDeletedNote = null;
  };

  $scope.isValidAssessorCount = function(assessorsPerProject) {
    if (!assessorsPerProject) {
      return true;
    }

    var usedAsessors = _.filter($scope.model.assessors, function(as) {
      return !as.isDeleted;
    });

    if (assessorsPerProject > usedAsessors.length) {
      return false;
    } else {
      return true;
    }
  };
}

EvalSessionDistributionCtrl.$inject = ['$scope', 'l10n'];

export { EvalSessionDistributionCtrl };
