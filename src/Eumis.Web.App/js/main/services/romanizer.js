import _ from 'lodash';

export const romanizerService = function() {
  var romanNumerals = {
    M: 1000,
    CM: 900,
    D: 500,
    CD: 400,
    C: 100,
    XC: 90,
    L: 50,
    XL: 40,
    X: 10,
    IX: 9,
    V: 5,
    IV: 4,
    I: 1
  };

  return function(n) {
    var r = '';
    _.forEach(romanNumerals, function(value, key) {
      for (var i = value; n >= i; n -= i) {
        r += key;
      }
    });
    return r;
  };
};
