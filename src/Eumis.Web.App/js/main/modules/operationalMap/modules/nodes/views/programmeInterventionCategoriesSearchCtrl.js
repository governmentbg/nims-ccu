import angular from 'angular';

function PInterventionCategoriesSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  ProgrammeInterventionCategory,
  categoriesByDimension
) {
  $scope.programmeStatus = $scope.info.status;
  $scope.programmeId = $stateParams.id;
  $scope.categoriesByDimension = categoriesByDimension;

  $scope.newCategory = function(dimension) {
    var modalInstance = scModal.open('iCategoryWithValueModal', {
      resourceProvider: ProgrammeInterventionCategory,
      resourceAlias: 'programme',
      resourceId: $stateParams.id,
      dimension: dimension
    });
    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);
    return modalInstance.opened;
  };

  $scope.editCategory = function(interventionCategoryId, dimension) {
    var modalInstance = scModal.open('iCategoryWithValueModal', {
      resourceProvider: ProgrammeInterventionCategory,
      resourceAlias: 'programme',
      resourceId: $stateParams.id,
      interventionCategoryId: interventionCategoryId,
      dimension: dimension
    });
    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);
    return modalInstance.opened;
  };

  $scope.deleteCategory = function(interventionCategoryId, dimension, version) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProgrammeInterventionCategory',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: interventionCategoryId,
        dimension: dimension,
        version: version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

PInterventionCategoriesSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'ProgrammeInterventionCategory',
  'categoriesByDimension'
];

PInterventionCategoriesSearchCtrl.$resolve = {
  categoriesByDimension: [
    '$stateParams',
    'ProgrammeInterventionCategory',
    function($stateParams, ProgrammeInterventionCategory) {
      return ProgrammeInterventionCategory.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { PInterventionCategoriesSearchCtrl };
