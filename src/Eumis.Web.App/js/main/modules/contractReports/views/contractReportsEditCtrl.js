import angular from 'angular';

function ContractReportsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  scConfirm,
  scModal,
  ContractReport,
  contractReport
) {
  $scope.editMode = null;
  $scope.contractReport = contractReport;
  $scope.contractId = contractReport.contractId;
  $scope.contractReportId = $stateParams.id;

  $scope.changeStatus = function(contractReportStatus) {
    var noteLabel = null,
      validationAction = null,
      confirmMsg = $interpolate(l10n.get('contractReports_editContractReport_confirmChangeStatus'))(
        {
          status: l10n.get('contractReports_editContractReport_' + contractReportStatus)
        }
      );
    if (contractReportStatus === 'entered') {
      validationAction = 'canChangeStatusToEntered';
    } else if (contractReportStatus === 'refused') {
      noteLabel = 'contractReports_editContractReport_statusChangeMessage';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      noteLabel: noteLabel,
      resource: 'ContractReport',
      validationAction: validationAction,
      action:
        'changeStatusTo' +
        contractReportStatus.charAt(0).toUpperCase() +
        contractReportStatus.slice(1),
      params: {
        id: $stateParams.id,
        version: $scope.contractReport.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportForm.$validate().then(function() {
      if ($scope.editContractReportForm.$valid) {
        return ContractReport.canUpdate(
          {
            id: $stateParams.id
          },
          $scope.contractReport
        ).$promise.then(function(res) {
          if (res.errors.length !== 0) {
            var modalInstance = scModal.open('validationErrorsModal', {
              errors: res.errors
            });
            modalInstance.result.catch(angular.noop);
            return modalInstance.opened;
          } else {
            return ContractReport.update(
              {
                id: $stateParams.id
              },
              $scope.contractReport
            ).$promise.then(function() {
              return $state.partialReload();
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractReport',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReport.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReports.search');
      }
    });
  };
}

ContractReportsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'scConfirm',
  'scModal',
  'ContractReport',
  'contractReport'
];

ContractReportsEditCtrl.$resolve = {
  contractReport: [
    'ContractReport',
    '$stateParams',
    function(ContractReport, $stateParams) {
      return ContractReport.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportsEditCtrl };
