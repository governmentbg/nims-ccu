import _ from 'lodash';

function FlatFinancialCorrectionsViewCtrl($scope, flatFinancialCorrectionInfo) {
  $scope.flatFinancialCorrectionInfo = flatFinancialCorrectionInfo;

  $scope.tabList = {
    flatFinancialCorrections_tabs_edit: 'root.flatFinancialCorrections.view.edit'
  };

  if ($scope.flatFinancialCorrectionInfo.level !== 'programme') {
    _.assign($scope.tabList, {
      flatFinancialCorrections_tabs_items: 'root.flatFinancialCorrections.view.items'
    });
  } else {
    _.assign($scope.tabList, {
      flatFinancialCorrections_tabs_programmeItem:
        'root.flatFinancialCorrections.view.programmeItem'
    });
  }
}

FlatFinancialCorrectionsViewCtrl.$inject = ['$scope', 'flatFinancialCorrectionInfo'];

FlatFinancialCorrectionsViewCtrl.$resolve = {
  flatFinancialCorrectionInfo: [
    'FlatFinancialCorrection',
    '$stateParams',
    function(FlatFinancialCorrection, $stateParams) {
      return FlatFinancialCorrection.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { FlatFinancialCorrectionsViewCtrl };
