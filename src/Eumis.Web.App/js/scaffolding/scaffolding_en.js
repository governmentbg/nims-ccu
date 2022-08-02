import angular from 'angular';
import AngularL10NModule from 'l10n-angular/build/l10n-with-tools';

const ScaffoldingBgModule = angular.module('scaffolding.en', [AngularL10NModule]).config([
  'l10nProvider',
  function(l10n) {
    l10n.add('en-gb', {
      scaffolding: {
        scFile: {
          noFile: 'No file.'
        },
        scDatatable: {
          nextPage: 'Next',
          previousPage: 'Previous',
          info: '{{total}} results found (from {{start}} to {{end}})',
          noDataAvailable: 'None',
          noFilteredDataAvailable: 'No results',
          search: 'Search',
          filtered: ' (filtered out of {{max}} records)',
          deleteColumns: 'Columns'
        },
        scTreetable: {
          noDataAvailable: 'No results'
        },
        scMessage: {
          okButton: 'OK',
          cancelButton: 'Cancel'
        }
      }
    });
  }
]);

export default ScaffoldingBgModule.name;
