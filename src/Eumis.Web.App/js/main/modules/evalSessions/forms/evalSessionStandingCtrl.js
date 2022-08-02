function EvalSessionStandingCtrl($scope, l10n) {
  $scope.yesText = l10n.get('common_texts_yes');
  $scope.noText = l10n.get('common_texts_no');
  $scope.allProjects = $scope.model.projects;

  $scope.show = function() {
    $scope.model.projects = $scope.allProjects;
  };
}

EvalSessionStandingCtrl.$inject = ['$scope', 'l10n'];

export { EvalSessionStandingCtrl };
