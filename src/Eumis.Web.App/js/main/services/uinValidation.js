export const uinValidationService = [
  'Company',
  function(Company) {
    function isValidBulstat(uin) {
      if (!/^\d{9}(?:\d{4})?$/.test(uin)) {
        return false;
      }

      var valid, checkDigit, secondCheckDigit;

      checkDigit =
        (uin[0] * 1 +
          uin[1] * 2 +
          uin[2] * 3 +
          uin[3] * 4 +
          uin[4] * 5 +
          uin[5] * 6 +
          uin[6] * 7 +
          uin[7] * 8) %
        11;

      if (checkDigit === 10) {
        checkDigit =
          (uin[0] * 3 +
            uin[1] * 4 +
            uin[2] * 5 +
            uin[3] * 6 +
            uin[4] * 7 +
            uin[5] * 8 +
            uin[6] * 9 +
            uin[7] * 10) %
          11;

        checkDigit = checkDigit === 10 ? 0 : checkDigit;
        valid = checkDigit === parseInt(uin[8], 10);
      } else {
        valid = checkDigit === parseInt(uin[8], 10);
      }

      if (valid && /^\d{13}$/.test(uin)) {
        secondCheckDigit = (uin[8] * 2 + uin[9] * 7 + uin[10] * 3 + uin[11] * 5) % 11;

        if (secondCheckDigit === 10) {
          secondCheckDigit = (uin[8] * 4 + uin[9] * 9 + uin[10] * 5 + uin[11] * 7) % 11;

          secondCheckDigit = secondCheckDigit === 10 ? 0 : secondCheckDigit;
          valid = secondCheckDigit === parseInt(uin[12], 10);
        } else {
          valid = secondCheckDigit === parseInt(uin[12], 10);
        }
      }

      return valid;
    }

    function isValidPersonalBulstat(uin) {
      if (!/^\d{10}$/.test(uin)) {
        return false;
      }

      var checkDigit =
        (uin[0] * 2 +
          uin[1] * 4 +
          uin[2] * 8 +
          uin[3] * 5 +
          uin[4] * 10 +
          uin[5] * 9 +
          uin[6] * 7 +
          uin[7] * 3 +
          uin[8] * 6) %
        11;

      checkDigit = checkDigit === 10 ? 0 : checkDigit;

      return checkDigit === parseInt(uin[9], 10);
    }

    function isValidForeignNumber(uin) {
      if (!/^\d{10}$/.test(uin)) {
        return false;
      }

      var checkDigit =
        (uin[0] * 21 +
          uin[1] * 19 +
          uin[2] * 17 +
          uin[3] * 13 +
          uin[4] * 11 +
          uin[5] * 9 +
          uin[6] * 7 +
          uin[7] * 3 +
          uin[8] * 1) %
        10;

      return checkDigit === parseInt(uin[9], 10);
    }

    this.isUniqueUin = function(uin, uinType, companyId) {
      if (!uin || !uinType) {
        return true;
      }

      return Company.isUniqueUin({
        uin: uin,
        uinType: uinType,
        companyId: companyId
      }).$promise;
    };

    this.uinValid = function(uin, uinType) {
      if (!uin || !uinType) {
        return true;
      }

      if (uinType === 'personalBulstat') {
        return isValidPersonalBulstat(uin);
      } else if (uinType === 'bulstat' || uinType === 'eik') {
        return isValidBulstat(uin);
      } else if (uinType === 'foreignNumber') {
        return isValidForeignNumber(uin);
      } else if (uinType === 'foreign') {
        return true;
      } else {
        return false;
      }
    };
  }
];
