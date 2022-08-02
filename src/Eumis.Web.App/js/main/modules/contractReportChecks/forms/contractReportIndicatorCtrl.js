function ContractReportIndicatorCtrl($scope) {
  const updateTotalAmount = () => {
    $scope.model.approvedPeriodAmountTotal =
      ($scope.model.approvedPeriodAmountMen || 0) + ($scope.model.approvedPeriodAmountWomen || 0);

    $scope.totalAmountChanged();
  };

  $scope.totalAmountChanged = function() {
    if (
      $scope.model.approvedPeriodAmountTotal !== null &&
      $scope.model.approvedPeriodAmountTotal !== undefined
    ) {
      if ($scope.model.aggregatedReport === 'aggregated') {
        $scope.model.approvedCumulativeAmountTotal = $scope.model.approvedPeriodAmountTotal;
      } else {
        $scope.model.approvedCumulativeAmountTotal =
          $scope.model.approvedPeriodAmountTotal + $scope.model.lastReportCumulativeAmountTotal;
      }
      if ($scope.model.aggregatedTarget === 'aggregated') {
        if ($scope.model.trend !== 'reduction') {
          $scope.model.approvedResidueAmountTotal =
            $scope.model.contractIndicator.targetTotalValue -
            $scope.model.approvedCumulativeAmountTotal -
            $scope.model.contractIndicator.baseTotalValue;
        } else {
          $scope.model.approvedResidueAmountTotal =
            $scope.model.contractIndicator.baseTotalValue -
            $scope.model.approvedCumulativeAmountTotal -
            $scope.model.contractIndicator.targetTotalValue;
        }
      } else {
        if ($scope.model.trend !== 'reduction') {
          $scope.model.approvedResidueAmountTotal =
            $scope.model.contractIndicator.targetTotalValue -
            $scope.model.approvedCumulativeAmountTotal;
        } else {
          $scope.model.approvedResidueAmountTotal =
            $scope.model.contractIndicator.baseTotalValue -
            $scope.model.approvedCumulativeAmountTotal;
        }
      }
    } else {
      $scope.model.approvedCumulativeAmountTotal = null;
      $scope.model.approvedResidueAmountTotal = null;
    }
  };

  $scope.amountMenChanged = function() {
    if (
      $scope.model.approvedPeriodAmountMen !== null &&
      $scope.model.approvedPeriodAmountMen !== undefined
    ) {
      if ($scope.model.aggregatedReport === 'aggregated') {
        $scope.model.approvedCumulativeAmountMen = $scope.model.approvedPeriodAmountMen;
      } else {
        $scope.model.approvedCumulativeAmountMen =
          $scope.model.approvedPeriodAmountMen + $scope.model.lastReportCumulativeAmountMen;
      }
      if ($scope.model.aggregatedTarget === 'aggregated') {
        if ($scope.model.trend !== 'reduction') {
          $scope.model.approvedResidueAmountMen =
            $scope.model.contractIndicator.targetMenValue -
            $scope.model.approvedCumulativeAmountMen -
            $scope.model.contractIndicator.baseMenValue;
        } else {
          $scope.model.approvedResidueAmountMen =
            $scope.model.contractIndicator.baseMenValue -
            $scope.model.approvedCumulativeAmountMen -
            $scope.model.contractIndicator.targetMenValue;
        }
      } else {
        if ($scope.model.trend !== 'reduction') {
          $scope.model.approvedResidueAmountMen =
            $scope.model.contractIndicator.targetMenValue -
            $scope.model.approvedCumulativeAmountMen;
        } else {
          $scope.model.approvedResidueAmountMen =
            $scope.model.contractIndicator.baseMenValue - $scope.model.approvedCumulativeAmountMen;
        }
      }
    } else {
      $scope.model.approvedCumulativeAmountMen = null;
      $scope.model.approvedResidueAmountMen = null;
    }

    updateTotalAmount();
  };

  $scope.amountWomenChanged = function() {
    if (
      $scope.model.approvedPeriodAmountWomen !== null &&
      $scope.model.approvedPeriodAmountWomen !== undefined
    ) {
      if ($scope.model.aggregatedReport === 'aggregated') {
        $scope.model.approvedCumulativeAmountWomen = $scope.model.approvedPeriodAmountWomen;
      } else {
        $scope.model.approvedCumulativeAmountWomen =
          $scope.model.approvedPeriodAmountWomen + $scope.model.lastReportCumulativeAmountWomen;
      }
      if ($scope.model.aggregatedTarget === 'aggregated') {
        if ($scope.model.trend !== 'reduction') {
          $scope.model.approvedResidueAmountWomen =
            $scope.model.contractIndicator.targetWomenValue -
            $scope.model.approvedCumulativeAmountWomen -
            $scope.model.contractIndicator.baseWomenValue;
        } else {
          $scope.model.approvedResidueAmountWomen =
            $scope.model.contractIndicator.baseWomenValue -
            $scope.model.approvedCumulativeAmountWomen -
            $scope.model.contractIndicator.targetWomenValue;
        }
      } else {
        if ($scope.model.trend !== 'reduction') {
          $scope.model.approvedResidueAmountWomen =
            $scope.model.contractIndicator.targetWomenValue -
            $scope.model.approvedCumulativeAmountWomen;
        } else {
          $scope.model.approvedResidueAmountWomen =
            $scope.model.contractIndicator.baseWomenValue -
            $scope.model.approvedCumulativeAmountWomen;
        }
      }
    } else {
      $scope.model.approvedCumulativeAmountWomen = null;
      $scope.model.approvedResidueAmountWomen = null;
    }

    updateTotalAmount();
  };
}

ContractReportIndicatorCtrl.$inject = ['$scope'];

export { ContractReportIndicatorCtrl };
