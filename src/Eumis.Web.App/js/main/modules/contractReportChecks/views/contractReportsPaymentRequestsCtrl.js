import _ from 'lodash';
import s from 'underscore.string';

function ContractReportsPaymentRequestsCtrl(
  moment,
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  romanizer,
  contractBudgetTree
) {
  $scope.contractReportRegDate = $scope.contractReportInfo.regDate;
  $scope.contractBudgetTree = contractBudgetTree;

  $scope.title = l10n.get('contractReportChecks_contractReportChecks_paymentRequests_title');

  var lev2code = 1;
  $scope.contractBudgetTree.programme.euAmount = s.numberFormat(
    $scope.contractBudgetTree.programme.euAmount,
    2,
    ',',
    ' '
  );
  $scope.contractBudgetTree.programme.bgAmount = s.numberFormat(
    $scope.contractBudgetTree.programme.bgAmount,
    2,
    ',',
    ' '
  );
  $scope.contractBudgetTree.programme.selfAmount = s.numberFormat(
    $scope.contractBudgetTree.programme.selfAmount,
    2,
    ',',
    ' '
  );
  $scope.contractBudgetTree.programme.totalAmount = s.numberFormat(
    $scope.contractBudgetTree.programme.totalAmount,
    2,
    ',',
    ' '
  );

  $scope.contractBudgetTree.programme.level1Items = _.map(
    contractBudgetTree.programme.level1Items,
    function(lev1) {
      lev1.euAmount = s.numberFormat(lev1.euAmount, 2, ',', ' ');
      lev1.bgAmount = s.numberFormat(lev1.bgAmount, 2, ',', ' ');
      lev1.selfAmount = s.numberFormat(lev1.selfAmount, 2, ',', ' ');
      lev1.totalAmount = s.numberFormat(lev1.totalAmount, 2, ',', ' ');

      lev1.level2Items = _.map(lev1.level2Items, function(lev2) {
        lev2.euAmount = s.numberFormat(lev2.euAmount, 2, ',', ' ');
        lev2.bgAmount = s.numberFormat(lev2.bgAmount, 2, ',', ' ');
        lev2.selfAmount = s.numberFormat(lev2.selfAmount, 2, ',', ' ');
        lev2.totalAmount = s.numberFormat(lev2.totalAmount, 2, ',', ' ');

        lev2.level3Items = _.map(lev2.level3Items, function(lev3) {
          lev3.euAmount = s.numberFormat(lev3.euAmount, 2, ',', ' ');
          lev3.bgAmount = s.numberFormat(lev3.bgAmount, 2, ',', ' ');
          lev3.selfAmount = s.numberFormat(lev3.selfAmount, 2, ',', ' ');
          lev3.totalAmount = s.numberFormat(lev3.totalAmount, 2, ',', ' ');

          return lev3;
        });

        lev2.code = lev2code++;
        return lev2;
      });

      return lev1;
    }
  );

  $scope.romanize = function(val) {
    return romanizer(val);
  };
}

ContractReportsPaymentRequestsCtrl.$inject = [
  'moment',
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'romanizer',
  'contractBudgetTree'
];

ContractReportsPaymentRequestsCtrl.$resolve = {
  contractBudgetTree: [
    '$stateParams',
    'ContractReportPaymentRequest',
    function($stateParams, ContractReportPaymentRequest) {
      return ContractReportPaymentRequest.getContractBudgetTree({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportsPaymentRequestsCtrl };
