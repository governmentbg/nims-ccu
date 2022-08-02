import angular from 'angular';
import AngularL10NModule from 'l10n-angular/build/l10n-with-tools';
import 'bootstrap-datepicker/js/bootstrap-datepicker';
import 'bootstrap-datepicker/js/locales/bootstrap-datepicker.bg';
import 'select2';
import 'select2/select2_locale_bg';

const ScaffoldingBgModule = angular.module('scaffolding.bg', [AngularL10NModule]).config([
  'l10nProvider',
  function(l10n) {
    l10n.add('bg-bg', {
      scaffolding: {
        scFile: {
          noFile: 'Няма прикачен файл.'
        },
        scDatatable: {
          nextPage: 'Следваща',
          previousPage: 'Предишна',
          info: 'Намерени общo {{total}} резултата (от {{start}} до {{end}})',
          noDataAvailable: 'Няма',
          noFilteredDataAvailable: 'Няма намерени резултати',
          search: 'Търси',
          filtered: ' (филтрирани от {{max}} записа)',
          deleteColumns: 'Колони'
        },
        scTreetable: {
          noDataAvailable: 'Няма намерени резултати'
        },
        scMessage: {
          okButton: 'OK',
          cancelButton: 'Отказ'
        }
      }
    });
  }
]);

export default ScaffoldingBgModule.name;
