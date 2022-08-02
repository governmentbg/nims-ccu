import _ from 'lodash';

function ChooseRecipientsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ProcedureMassCommunicationRecipient,
  recipients
) {
  $scope.chosenContractIds = [];
  $scope.recipients = recipients;
  $scope.hasChosenAll = true;
  $scope.tableControl = {};

  $scope.chooseAll = function() {
    var filteredRecipients = $scope.tableControl.getFilteredItems();
    _.forEach(filteredRecipients, function(cr) {
      if (_.includes($scope.chosenContractIds, cr.contractId)) {
        cr.isChosen = true;
      } else {
        $scope.choose(cr);
      }
    });
    $scope.hasChosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.recipients, function(cr) {
      $scope.remove(cr);
    });
    $scope.hasChosenAll = true;
  };

  $scope.choose = function(recipient) {
    recipient.isChosen = true;
    $scope.chosenContractIds.push(recipient.contractId);
  };

  $scope.remove = function(recipient) {
    recipient.isChosen = false;
    $scope.chosenContractIds = _.without($scope.chosenContractIds, recipient.contractId);
  };

  $scope.ok = function() {
    return ProcedureMassCommunicationRecipient.save(
      {
        id: scModalParams.communicationId,
        version: scModalParams.version
      },
      $scope.chosenContractIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseRecipientsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ProcedureMassCommunicationRecipient',
  'recipients'
];

ChooseRecipientsModalCtrl.$resolve = {
  recipients: [
    'ProcedureMassCommunicationRecipient',
    'scModalParams',
    function(ProcedureMassCommunicationRecipient, scModalParams) {
      return ProcedureMassCommunicationRecipient.getUnattachedRecipients({
        id: scModalParams.communicationId
      }).$promise;
    }
  ]
};

export { ChooseRecipientsModalCtrl };
