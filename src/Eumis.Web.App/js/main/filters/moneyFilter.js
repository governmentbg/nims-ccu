import s from 'underscore.string';

function MoneyFilter() {
  return function(value) {
    if (!value) {
      return value;
    }

    return s.numberFormat(value, 2, ',', ' ');
  };
}

export { MoneyFilter as moneyFilter };
