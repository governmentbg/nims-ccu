import _ from 'lodash';

function PPriorityDocumentsSearchCtrl($scope, $stateParams, ProgrammePriorityFile, documents) {
  $scope.programmePriorityId = $stateParams.id;
  $scope.programmePriorityStatus = $scope.info.status;

  $scope.documents = _.map(documents, function(item) {
    if (item.file) {
      item.file.url = ProgrammePriorityFile.getUrl({
        id: item.mapNodeId,
        fileKey: item.file.key
      });
    }
    return item;
  });
}

PPriorityDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'ProgrammePriorityFile',
  'documents'
];

PPriorityDocumentsSearchCtrl.$resolve = {
  documents: [
    '$stateParams',
    'ProgrammePriorityDocument',
    function($stateParams, ProgrammePriorityDocument) {
      return ProgrammePriorityDocument.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { PPriorityDocumentsSearchCtrl };
