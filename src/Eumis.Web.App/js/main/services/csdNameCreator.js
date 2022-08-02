export const csdNameCreatorService = [
  'l10n',
  'moment',
  function(l10n, moment) {
    var fromText = l10n.get('certReports_certReportFinancialRevalidationsEdit_from');

    return function(item) {
      item.date = moment(item.date).format('DD.MM.YYYY');

      item.csd = [
        item.type,
        item.number,
        fromText,
        item.date,
        '(' + item.companyType + ')',
        item.companyUinType,
        item.companyUin,
        item.companyName
      ].join(' ');
    };
  }
];
