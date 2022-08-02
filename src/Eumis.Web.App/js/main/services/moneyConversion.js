export const moneyConversionService = [
  'exchangeRates',
  function(exchangeRates) {
    this.convertFromLv = function(lv, toCurrency) {
      if (!lv) {
        return null;
      }

      return Math.round(lv / exchangeRates[toCurrency]);
    };

    this.convertToLv = function(money, fromCurrency) {
      if (!money) {
        return null;
      }

      return Math.round(money * exchangeRates[fromCurrency]);
    };
  }
];

export const exchangeRatesConstant = {
  euroEC: 1.9558,
  euro: 1.95583
};
