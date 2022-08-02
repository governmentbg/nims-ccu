import _ from 'lodash';

function ActuallyPaidAmountsDocumentsSearchCtrl($scope, $stateParams, PaidAmountFile, documents) {
  $scope.paidAmountId = $stateParams.id;
  $scope.status = $scope.paidAmountInfo.status;
  $scope.documents = _.map(documents, function(item) {
    if (item.file) {
      item.file.url = PaidAmountFile.getUrl({
        id: item.actuallyPaidAmountId,
        fileKey: item.file.key
      });
    }
    return item;
  });
}

ActuallyPaidAmountsDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'PaidAmountFile',
  'documents'
];

ActuallyPaidAmountsDocumentsSearchCtrl.$resolve = {
  documents: [
    '$stateParams',
    'ActuallyPaidAmountDocument',
    function($stateParams, ActuallyPaidAmountDocument) {
      return ActuallyPaidAmountDocument.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ActuallyPaidAmountsDocumentsSearchCtrl };
