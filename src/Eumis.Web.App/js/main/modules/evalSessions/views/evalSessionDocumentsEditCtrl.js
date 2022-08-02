function EvalSessionDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  EvalSessionDocument,
  evalSessionDocument,
  scConfirm
) {
  $scope.evalSessionDocument = evalSessionDocument;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.isSessionEndedByLAG = $scope.evalSessionInfo.evalSessionStatusName === 'endedByLAG';
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editEvalSessionDocumentForm.$validate().then(function() {
      if ($scope.editEvalSessionDocumentForm.$valid) {
        return EvalSessionDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.evalSessionDocument
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteDocument = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionReport_deleteConfirm',
      noteLabel: 'evalSessions_editEvalSessionReport_deleteMessage',
      resource: 'EvalSessionDocument',
      action: 'cancelDocument',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'EvalSessionDocument',
  'evalSessionDocument',
  'scConfirm'
];

EvalSessionDocumentsEditCtrl.$resolve = {
  evalSessionDocument: [
    '$stateParams',
    'EvalSessionDocument',
    function($stateParams, EvalSessionDocument) {
      return EvalSessionDocument.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { EvalSessionDocumentsEditCtrl };
