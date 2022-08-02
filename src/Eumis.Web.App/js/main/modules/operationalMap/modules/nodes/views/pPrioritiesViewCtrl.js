import _ from 'lodash';

function PPrioritiesViewCtrl($scope, $state, $stateParams, l10n, info, showIndicatorTab) {
  $scope.info = info;
  $scope.showIndicatorTab = showIndicatorTab;
  $scope.belongTo = l10n.get('operationalMap_belongTo');

  $scope.tabList = {
    programmePriorities_tabs_programmePriorityData: 'root.map.ppriorities.view.edit'
  };

  if (showIndicatorTab) {
    _.assign($scope.tabList, {
      programmePriorities_tabs_directions: 'root.map.ppriorities.view.directions'
      /*programmePriorities_tabs_indicators: 'root.map.ppriorities.view.indicators'*/
    });
  }

  _.assign($scope.tabList, {
    programmePriorities_tabs_documents: 'root.map.ppriorities.view.documents'
  });
}

PPrioritiesViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'info',
  'showIndicatorTab'
];

PPrioritiesViewCtrl.$resolve = {
  info: [
    'ProgrammePriority',
    '$stateParams',
    function(ProgrammePriority, $stateParams) {
      return ProgrammePriority.getInfo({ id: $stateParams.id }).$promise;
    }
  ],
  showIndicatorTab: [
    'authorizerService',
    '$stateParams',
    function(authorizerService, $stateParams) {
      return authorizerService.canDoRequest('MapNodeIndicatorActions.View', $stateParams.id);
    }
  ]
};

export { PPrioritiesViewCtrl };
