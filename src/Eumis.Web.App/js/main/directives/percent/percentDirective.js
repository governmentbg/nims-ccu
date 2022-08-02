import percentDirectiveTemplateUrl from './percentDirective.html';

export const eumisPercentDirective = [
  'numberDirectiveFactory',
  function(numberDirectiveFactory) {
    return numberDirectiveFactory(
      percentDirectiveTemplateUrl,
      'scFloatParserFactory',
      'scFloatFormatterFactory',
      false
    );
  }
];
