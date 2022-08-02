import _ from 'lodash';

function ContractsViewCtrl($scope, contractInfo) {
  $scope.contractInfo = contractInfo;

  $scope.tabList = {
    contracts_tabs_edit: 'root.contracts.view.data',
    contracts_tabs_amendments: 'root.contracts.view.amendments'
  };

  if ($scope.contractInfo.procedureKind === 'schema') {
    _.assign($scope.tabList, {
      contracts_tabs_registrations: 'root.contracts.view.registrations',
      contracts_tabs_accesscodes: 'root.contracts.view.accesscodes',
      contracts_tabs_communications: 'root.contracts.view.communications',
      contracts_tabs_users: 'root.contracts.view.users'
    });
  }

  _.assign($scope.tabList, {
    contracts_tabs_documents: 'root.contracts.view.documents'
  });
}

ContractsViewCtrl.$inject = ['$scope', 'contractInfo'];

ContractsViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractsViewCtrl };
