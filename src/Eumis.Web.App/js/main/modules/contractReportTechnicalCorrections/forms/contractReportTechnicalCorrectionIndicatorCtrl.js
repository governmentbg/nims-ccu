function ContractReportTechnicalCorrectionIndicatorCtrl($scope) {
  const updateTotalAmount = () => {
    $scope.model.correctedApprovedPeriodAmountTotal =
      ($scope.model.correctedApprovedPeriodAmountMen || 0) +
      ($scope.model.correctedApprovedPeriodAmountWomen || 0);

    $scope.totalAmountChanged();
  };

  $scope.totalAmountChanged = function() {
    if (
      $scope.model.correctedApprovedPeriodAmountTotal !== null &&
      $scope.model.correctedApprovedPeriodAmountTotal !== undefined
    ) {
      if ($scope.model.contractReportIndicator.aggregatedReport === 'aggregated') {
        $scope.model.correctedApprovedCumulativeAmountTotal =
          $scope.model.correctedApprovedPeriodAmountTotal;
      } else {
        $scope.model.correctedApprovedCumulativeAmountTotal =
          $scope.model.correctedApprovedPeriodAmountTotal +
          $scope.model.lastReportCorrectedCumulativeAmountTotal;
      }
      if ($scope.model.contractReportIndicator.aggregatedTarget === 'aggregated') {
        if ($scope.model.contractReportIndicator.trend !== 'reduction') {
          $scope.model.correctedApprovedResidueAmountTotal =
            $scope.model.contractReportIndicator.contractIndicator.targetTotalValue -
            $scope.model.correctedApprovedCumulativeAmountTotal -
            $scope.model.contractReportIndicator.contractIndicator.baseTotalValue;
        } else {
          $scope.model.correctedApprovedResidueAmountTotal =
            $scope.model.contractReportIndicator.contractIndicator.baseTotalValue -
            $scope.model.correctedApprovedCumulativeAmountTotal -
            $scope.model.contractReportIndicator.contractIndicator.targetTotalValue;
        }
      } else {
        if ($scope.model.contractReportIndicator.trend !== 'reduction') {
          $scope.model.correctedApprovedResidueAmountTotal =
            $scope.model.contractReportIndicator.contractIndicator.targetTotalValue -
            $scope.model.correctedApprovedCumulativeAmountTotal;
        } else {
          $scope.model.correctedApprovedResidueAmountTotal =
            $scope.model.contractReportIndicator.contractIndicator.baseTotalValue -
            $scope.model.correctedApprovedCumulativeAmountTotal;
        }
      }
    } else {
      $scope.model.correctedApprovedCumulativeAmountTotal = null;
      $scope.model.correctedApprovedResidueAmountTotal = null;
    }
  };

  $scope.amountMenChanged = function() {
    if (
      $scope.model.correctedApprovedPeriodAmountMen !== null &&
      $scope.model.correctedApprovedPeriodAmountMen !== undefined
    ) {
      if ($scope.model.contractReportIndicator.aggregatedReport === 'aggregated') {
        $scope.model.correctedApprovedCumulativeAmountMen =
          $scope.model.correctedApprovedPeriodAmountMen;
      } else {
        $scope.model.correctedApprovedCumulativeAmountMen =
          $scope.model.correctedApprovedPeriodAmountMen +
          $scope.model.lastReportCorrectedCumulativeAmountMen;
      }
      if ($scope.model.contractReportIndicator.aggregatedTarget === 'aggregated') {
        if ($scope.model.contractReportIndicator.trend !== 'reduction') {
          $scope.model.correctedApprovedResidueAmountMen =
            $scope.model.contractReportIndicator.contractIndicator.targetMenValue -
            $scope.model.correctedApprovedCumulativeAmountMen -
            $scope.model.contractReportIndicator.contractIndicator.baseMenValue;
        } else {
          $scope.model.correctedApprovedResidueAmountMen =
            $scope.model.contractReportIndicator.contractIndicator.baseMenValue -
            $scope.model.correctedApprovedCumulativeAmountMen -
            $scope.model.contractReportIndicator.contractIndicator.targetMenValue;
        }
      } else {
        if ($scope.model.contractReportIndicator.trend !== 'reduction') {
          $scope.model.correctedApprovedResidueAmountMen =
            $scope.model.contractReportIndicator.contractIndicator.targetMenValue -
            $scope.model.correctedApprovedCumulativeAmountMen;
        } else {
          $scope.model.correctedApprovedResidueAmountMen =
            $scope.model.contractReportIndicator.contractIndicator.baseMenValue -
            $scope.model.correctedApprovedCumulativeAmountMen;
        }
      }
    } else {
      $scope.model.correctedApprovedCumulativeAmountMen = null;
      $scope.model.correctedApprovedResidueAmountMen = null;
    }

    updateTotalAmount();
  };

  $scope.amountWomenChanged = function() {
    if (
      $scope.model.correctedApprovedPeriodAmountWomen !== null &&
      $scope.model.correctedApprovedPeriodAmountWomen !== undefined
    ) {
      if ($scope.model.contractReportIndicator.aggregatedReport === 'aggregated') {
        $scope.model.correctedApprovedCumulativeAmountWomen =
          $scope.model.correctedApprovedPeriodAmountWomen;
      } else {
        $scope.model.correctedApprovedCumulativeAmountWomen =
          $scope.model.correctedApprovedPeriodAmountWomen +
          $scope.model.lastReportCorrectedCumulativeAmountWomen;
      }
      if ($scope.model.contractReportIndicator.aggregatedTarget === 'aggregated') {
        if ($scope.model.contractReportIndicator.trend !== 'reduction') {
          $scope.model.correctedApprovedResidueAmountWomen =
            $scope.model.contractReportIndicator.contractIndicator.targetWomenValue -
            $scope.model.correctedApprovedCumulativeAmountWomen -
            $scope.model.contractReportIndicator.contractIndicator.baseWomenValue;
        } else {
          $scope.model.correctedApprovedResidueAmountWomen =
            $scope.model.contractReportIndicator.contractIndicator.baseWomenValue -
            $scope.model.correctedApprovedCumulativeAmountWomen -
            $scope.model.contractReportIndicator.contractIndicator.targetWomenValue;
        }
      } else {
        if ($scope.model.contractReportIndicator.trend !== 'reduction') {
          $scope.model.correctedApprovedResidueAmountWomen =
            $scope.model.contractReportIndicator.contractIndicator.targetWomenValue -
            $scope.model.correctedApprovedCumulativeAmountWomen;
        } else {
          $scope.model.correctedApprovedResidueAmountWomen =
            $scope.model.contractReportIndicator.contractIndicator.baseWomenValue -
            $scope.model.correctedApprovedCumulativeAmountWomen;
        }
      }
    } else {
      $scope.model.correctedApprovedCumulativeAmountWomen = null;
      $scope.model.correctedApprovedResidueAmountWomen = null;
    }

    updateTotalAmount();
  };
}

ContractReportTechnicalCorrectionIndicatorCtrl.$inject = ['$scope'];

export { ContractReportTechnicalCorrectionIndicatorCtrl };
