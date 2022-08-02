function RegistrationsSearchCtrl($scope, $stateParams, regs) {
  $scope.regs = regs;
  $scope.registrationsExportUrl = 'api/registrations/excelExport';
}

RegistrationsSearchCtrl.$inject = ['$scope', '$stateParams', 'regs'];

RegistrationsSearchCtrl.$resolve = {
  regs: [
    'Registration',
    function(Registration) {
      return Registration.query().$promise;
    }
  ]
};

export { RegistrationsSearchCtrl };
