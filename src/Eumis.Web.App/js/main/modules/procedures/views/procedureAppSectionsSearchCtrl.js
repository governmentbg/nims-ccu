function ProcedureAppSectionSearchCtrl(
  $scope,
  $stateParams,
  $state,
  scConfirm,
  procedureInfo,
  applicationSections,
  ProcedureApplicationSection
) {
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureId = $stateParams.id;
  $scope.procedureInfo = procedureInfo;
  $scope.canEditApplicationSection = procedureInfo.procedureKind === 'schema';
  $scope.applicationSections = applicationSections;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.save = function() {
    return $scope.procedureAppSectionSearchForm.$validate().then(function() {
      if ($scope.procedureAppSectionSearchForm.$valid) {
        return ProcedureApplicationSection.save(
          { id: $stateParams.id },
          $scope.applicationSections
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };
}

ProcedureAppSectionSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$state',
  'scConfirm',
  'procedureInfo',
  'applicationSections',
  'ProcedureApplicationSection'
];

ProcedureAppSectionSearchCtrl.$resolve = {
  procedureInfo: [
    '$stateParams',
    'Procedure',
    function($stateParams, Procedure) {
      return Procedure.getInfo({ id: $stateParams.id }).$promise;
    }
  ],
  applicationSections: [
    '$stateParams',
    'ProcedureApplicationSection',
    function($stateParams, ProcedureApplicationSection) {
      return ProcedureApplicationSection.getSections({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureAppSectionSearchCtrl };
