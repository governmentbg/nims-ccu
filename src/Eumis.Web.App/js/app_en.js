import angular from 'angular';
import MainModule from 'js/main/main';
import MainEnModule from 'js/main/main_en';
import ScaffoldingEnModule from 'js/scaffolding/scaffolding_en';
import 'angular-i18n/angular-locale_en-gb';
import 'moment/locale/en-gb';

const AppBgModule = angular.module('app.en', [MainModule, MainEnModule, ScaffoldingEnModule]);

export default AppBgModule.name;
