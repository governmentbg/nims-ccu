import _ from 'lodash';

function ContractDocumentsSearchCtrl(
  $scope,
  $stateParams,
  structuredDocument,
  ContractFile,
  contractGrantDocuments,
  contractProcurementDocuments
) {
  $scope.contractId = $stateParams.id;
  $scope.contractStatus = $scope.contractInfo.status;

  $scope.contractGrantDocuments = _.map(contractGrantDocuments, function(item) {
    if (item.file) {
      item.file.url = ContractFile.getUrl({
        id: item.contractId,
        fileKey: item.file.key
      });
    }
    return item;
  });
  $scope.contractProcurementDocuments = _.map(contractProcurementDocuments, function(item) {
    if (item.file) {
      item.file.url = ContractFile.getUrl({
        id: item.contractId,
        fileKey: item.file.key
      });
    }
    return item;
  });
}

ContractDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'structuredDocument',
  'ContractFile',
  'contractGrantDocuments',
  'contractProcurementDocuments'
];

ContractDocumentsSearchCtrl.$resolve = {
  contractGrantDocuments: [
    '$stateParams',
    'ContractGrantDocument',
    function($stateParams, ContractGrantDocument) {
      return ContractGrantDocument.query($stateParams).$promise;
    }
  ],
  contractProcurementDocuments: [
    '$stateParams',
    'ContractProcurementDocument',
    function($stateParams, ContractProcurementDocument) {
      return ContractProcurementDocument.query($stateParams).$promise;
    }
  ]
};

export { ContractDocumentsSearchCtrl };
