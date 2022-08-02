import angular from 'angular';

function EvalSessionSheetsNewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scModal,
  EvalSessionSheet,
  newEvalSessionSheet
) {
  $scope.isContinued = $stateParams.sheetId ? true : false;
  $scope.newEvalSessionSheet = newEvalSessionSheet;

  $scope.save = function() {
    return $scope.newEvalSessionSheetForm.$validate().then(function() {
      if ($scope.newEvalSessionSheetForm.$valid) {
        return EvalSessionSheet.canCreate(
          { id: $stateParams.id },
          $scope.newEvalSessionSheet
        ).$promise.then(function(error) {
          if (!error) {
            return EvalSessionSheet.save(
              { id: $stateParams.id },
              $scope.newEvalSessionSheet
            ).$promise.then(function() {
              return $state.go('root.evalSessions.view.sheets.search');
            });
          } else {
            var modalInstance = scModal.open('validationErrorsModal', {
              errors: [error]
            });
            modalInstance.result.catch(angular.noop);
            return modalInstance.opened;
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.sheets.search');
  };
}

EvalSessionSheetsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scModal',
  'EvalSessionSheet',
  'newEvalSessionSheet'
];

EvalSessionSheetsNewCtrl.$resolve = {
  newEvalSessionSheet: [
    '$stateParams',
    'EvalSessionSheet',
    function($stateParams, EvalSessionSheet) {
      if ($stateParams.sheetId) {
        return EvalSessionSheet.newContinuedEvalSessionSheet({
          id: $stateParams.id,
          sheetId: $stateParams.sheetId
        }).$promise;
      } else {
        return EvalSessionSheet.newEvalSessionSheet({
          id: $stateParams.id
        }).$promise;
      }
    }
  ]
};

export { EvalSessionSheetsNewCtrl };
