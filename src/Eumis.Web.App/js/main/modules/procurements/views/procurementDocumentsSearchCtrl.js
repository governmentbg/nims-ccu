import _ from 'lodash';

function ProcurementDocumentsSearchCtrl(
  $scope,
  $stateParams,
  ProcurementFile,
  procurementDocuments
) {
  $scope.contractId = $stateParams.id;
  $scope.status = $scope.info.status;

  $scope.procurementDocuments = _.map(procurementDocuments, function(item) {
    if (item.file) {
      item.file.url = ProcurementFile.getUrl({
        id: item.procurementId,
        fileKey: item.file.key
      });
    }
    return item;
  });
}

ProcurementDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'ProcurementFile',
  'procurementDocuments'
];

ProcurementDocumentsSearchCtrl.$resolve = {
  procurementDocuments: [
    '$stateParams',
    'ProcurementDocument',
    function($stateParams, ProcurementDocument) {
      return ProcurementDocument.query($stateParams).$promise;
    }
  ]
};

export { ProcurementDocumentsSearchCtrl };
