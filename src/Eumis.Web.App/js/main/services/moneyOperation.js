export const moneyOperationService = function() {
  function isNumber(num) {
    return num !== null && num !== undefined;
  }

  this.addAmounts = function(a1, a2) {
    return isNumber(a1) || isNumber(a2) ? (a1 || 0) + (a2 || 0) : null;
  };

  this.subtractAmounts = function(a1, a2) {
    return isNumber(a1) || isNumber(a2) ? (a1 || 0) - (a2 || 0) : null;
  };
};
