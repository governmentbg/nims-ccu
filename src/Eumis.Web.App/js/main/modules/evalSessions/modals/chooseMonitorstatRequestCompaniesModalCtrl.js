import _ from 'lodash';

function ChooseMonitorstatRequestCompaniesModalCtrl(
  $scope,
  $state,
  $uibModalInstance,
  ProjectMonitorstatRequest,
  projectMonitorstatMassRequest
) {
  $scope.chosenCompanies = [];
  $scope.hasChosenAll = true;
  $scope.tableControl = {};

  $scope.massRequest = projectMonitorstatMassRequest;
  $scope.companies = projectMonitorstatMassRequest.companies;
  $scope.projectId = projectMonitorstatMassRequest.projectId;
  $scope.version = projectMonitorstatMassRequest.version;
  $scope.projectVersionXmlId = projectMonitorstatMassRequest.projectVersionXmlId;

  $scope.errors = [];

  $scope.chooseAll = function() {
    var filteredCompanies = $scope.tableControl.getFilteredItems();
    _.forEach(filteredCompanies, function(company) {
      if (_.includes($scope.chosenCompanies, company)) {
        company.isChosen = true;
      } else {
        $scope.choose(company);
      }
    });
    $scope.hasChosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.companies, function(company) {
      $scope.remove(company);
    });
    $scope.hasChosenAll = true;
  };

  $scope.choose = function(company) {
    company.isChosen = true;
    $scope.chosenCompanies.push(company);
  };

  $scope.remove = function(company) {
    company.isChosen = false;
    $scope.chosenCompanies = _.without($scope.chosenCompanies, company);
  };

  $scope.cancel = function() {
    $state.reload();
    return $uibModalInstance.dismiss('cancel');
  };

  $scope.send = function() {
    return ProjectMonitorstatRequest.canSendMonitorstatMassRequest(
      { id: $scope.projectId },
      $scope.chosenCompanies
    ).$promise.then(function(result) {
      if (result.errors.length === 0) {
        return ProjectMonitorstatRequest.sendMonitorstatMassRequest(
          {
            id: $scope.projectId,
            version: $scope.version,
            projectversionXmlId: $scope.projectVersionXmlId
          },
          $scope.chosenCompanies
        ).$promise.then(function() {
          $state.reload();
          return $uibModalInstance.close();
        });
      } else {
        $scope.errors = result.errors;
      }
    });
  };
}

ChooseMonitorstatRequestCompaniesModalCtrl.$inject = [
  '$scope',
  '$state',
  '$uibModalInstance',
  'ProjectMonitorstatRequest',
  'projectMonitorstatMassRequest'
];

ChooseMonitorstatRequestCompaniesModalCtrl.$resolve = {
  projectMonitorstatMassRequest: [
    'ProjectMonitorstatRequest',
    'scModalParams',
    function(ProjectMonitorstatRequest, scModalParams) {
      return ProjectMonitorstatRequest.newMonitorstatMassRequest({
        id: scModalParams.projectId
      }).$promise;
    }
  ]
};

export { ChooseMonitorstatRequestCompaniesModalCtrl };
