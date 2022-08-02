function ProgrammesViewCtrl($scope, $state, $stateParams, info) {
  $scope.info = info;

  $scope.tabList = {
    programmes_tabs_programmeData: 'root.map.programmes.view.edit',
    programmes_tabs_directions: 'root.map.programmes.view.directions.search',
    programmes_tabs_programmePriorities: 'root.map.programmes.view.programmePriorities',
    programmes_tabs_documents: 'root.map.programmes.view.documents'
  };
}

ProgrammesViewCtrl.$inject = ['$scope', '$state', '$stateParams', 'info'];

ProgrammesViewCtrl.$resolve = {
  info: [
    'Programme',
    '$stateParams',
    function(Programme, $stateParams) {
      return Programme.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammesViewCtrl };
