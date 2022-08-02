function CompanyDataCtrl(
  $scope,
  $stateParams,
  $timeout,
  $q,
  scFormParams,
  uinValidation,
  eumisConstants,
  Company,
  Nomenclatures
) {
  var inapplicableSizeTypePromise = Nomenclatures.get({
      alias: 'companySizeTypes',
      valueAlias: 'inapplicable'
    }).$promise,
    setInapplicableSizeType = function() {
      return $q.when(inapplicableSizeTypePromise).then(function(inapplicableSizeType) {
        $scope.model.companySizeTypeId = inapplicableSizeType.nomValueId;
      });
    };

  $scope.emailRegex = eumisConstants.emailRegex;
  $scope.isNew = scFormParams.isNew;

  if ($scope.isNew) {
    setInapplicableSizeType();
  }

  function uinSearchTimeout(prop) {
    return $timeout(function() {
      $scope[prop] = false;
    }, 4000);
  }

  $scope.uinSearch = function() {
    if ($scope.model.uin && $scope.model.uinType) {
      return Company.getCompanyByUin({
        uin: $scope.model.uin,
        uinType: $scope.model.uinType
      }).$promise.then(function(company) {
        if (company.uin) {
          $scope.model = company;
          $scope.resultFound = true;
          uinSearchTimeout('resultFound');
        } else {
          $scope.noResultFound = true;
          uinSearchTimeout('noResultFound');
        }
      });
    } else if (!$scope.model.uin || !$scope.model.uinType) {
      $scope.inputMissing = true;
      uinSearchTimeout('inputMissing');
    } else {
      $scope.inputNotValid = true;
      uinSearchTimeout('inputNotValid');
    }
  };

  $scope.isUniqueUin = function(uin) {
    return $q
      .when(uinValidation.isUniqueUin(uin, $scope.model.uinType, $scope.model.companyId))
      .then(function(isUnique) {
        return isUnique ? $q.resolve() : $q.reject();
      });
  };

  $scope.uinValid = function(uin) {
    return uinValidation.uinValid(uin, $scope.model.uinType);
  };

  $scope.seatCountryChange = function() {
    $scope.model.seatSettlementId = null;
    $scope.model.seatPostCode = null;
    $scope.model.seatStreet = null;
    $scope.model.seatAddress = null;
  };

  $scope.corrCountryChange = function() {
    $scope.model.corrSettlementId = null;
    $scope.model.corrPostCode = null;
    $scope.model.corrStreet = null;
    $scope.model.corrAddress = null;
  };

  $scope.companyTypeChange = function() {
    if (!$scope.companyType || $scope.companyType.alias !== 'company') {
      setInapplicableSizeType();
    } else {
      $scope.model.companySizeTypeId = null;
    }
  };

  $scope.companyLegalTypeChange = function() {
    if ($scope.companyLegalType && $scope.companyLegalType.alias === 'person') {
      $scope.model.nameAlt = null;
    }
  };

  $scope.copySeat = function() {
    $scope.model.corrCountryId = $scope.model.seatCountryId;
    $scope.model.corrSettlementId = $scope.model.seatSettlementId;
    $scope.model.corrPostCode = $scope.model.seatPostCode;
    $scope.model.corrStreet = $scope.model.seatStreet;
    $scope.model.corrAddress = $scope.model.seatAddress;
  };
}

CompanyDataCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$timeout',
  '$q',
  'scFormParams',
  'uinValidation',
  'eumisConstants',
  'Company',
  'Nomenclatures'
];

export { CompanyDataCtrl };
