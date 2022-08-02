import angular from 'angular';
import AngularMomentModule from 'angular-moment';
import ScaffoldingBgModule from 'js/scaffolding/scaffolding_bg';
import 'angular-i18n/angular-locale_bg-bg';
import 'moment/locale/bg';

const TestappBgModule = angular
  .module('testapp.bg', [ScaffoldingBgModule, AngularMomentModule])
  .config([
    'l10nProvider',
    function(l10n) {
      l10n.add('bg-bg', {
        states: {
          testpage: 'Test page',
          'testpage.regular': 'Test page regular',
          'testpage.filled': 'Test page filled',
          'testpage.readonly': 'Test page readonly',
          'testpage.usersDatatable': 'Users Datatable'
        }
      });
    }
  ])
  .run([
    'amMoment',
    'moment',
    function(amMoment, moment) {
      moment.updateLocale('bg', {
        calendar: {
          sameDay: '[Днес]',
          nextDay: '[Утре]',
          nextWeek: 'dddd',
          lastDay: '[Вчера]',
          lastWeek: function() {
            switch (this.day()) {
              case 0:
              case 3:
              case 6:
                return '[В изминалата] dddd';
              case 1:
              case 2:
              case 4:
              case 5:
                return '[В изминалия] dddd';
            }
          },
          sameElse: 'L'
        }
      });

      amMoment.changeLocale('bg');
    }
  ]);

export default TestappBgModule.name;
