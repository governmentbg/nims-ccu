import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import UiSelect2Module from 'angular-ui-select2/src/select2';
import UiJqModule from 'angular-ui-utils/modules/jq/jq';
import AngularL10NModule from 'l10n-angular/build/l10n-with-tools';
import { scAccessTokenDirective } from './directives/accessToken/accessTokenDirective';
import { scBreadcrumbDirective } from './directives/breadcrumb/breadcrumbDirective';
import { scButtonDirective } from './directives/button/buttonDirective';
import { scClickDirective } from './directives/click/clickDirective';
import { scColumnDirective } from './directives/datatable/columnDirective';
import { DatatableCtrl, scDatatableConfigConstant } from './directives/datatable/datatableCtrl';
import { scDatatableDirective } from './directives/datatable/datatableDirective';
import { scDateDirective, scDateConfigConstant } from './directives/fields/date/dateDirective';
import { scFieldDirective } from './directives/fields/field/fieldDirective';
import { scFieldGroupDirective } from './directives/fields/fieldGroup/fieldGroupDirective';
import { FileCtrl } from './directives/fields/file/fileCtrl';
import { scFileConfigConstant, scFileDirective } from './directives/fields/file/fileDirective';
import { scFormNameDirective } from './directives/formName/formNameDirective';
import { scFormReadonlyDirective } from './directives/formReadonly/formReadonlyDirective';
import { scHasErrorDirective } from './directives/hasError/hasErrorDirective';
import { scInfoDirective } from './directives/info/infoDirective';
import { scLinkButtonDirective } from './directives/linkButton/linkButtonDirective';
import {
  scNomenclatureDirective,
  scNomenclatureConfigConstant
} from './directives/fields/nomenclature/nomenclatureDirective';
import {
  scFloatParserFactoryConstant,
  scFloatFormatterFactoryConstant,
  scFloatDirective
} from './directives/fields/number/floatDirective';
import { scIntDirective } from './directives/fields/number/intDirective';
import { scMoneyDirective } from './directives/fields/number/moneyDirective';
import { numberDirectiveFactoryFactory } from './directives/fields/number/numberDirectiveFactory';
import { numberValidationsConstant } from './directives/fields/number/validations';
import {
  scPagerDirective,
  scPagerConfigConstant,
  pagerFactory
} from './directives/pager/pagerDirective';
import { scPasswordDirective } from './directives/fields/password/passwordDirective';
import { scPromiseStateDirective } from './directives/promiseState/promiseStateDirective';
import { scSelectDirective } from './directives/fields/select/selectDirective';
import { scSrefDirective } from './directives/sref/srefDirective';
import {
  scTextSuggestionDirective,
  scTextareaSuggestionDirective
} from './directives/fields/suggestion/suggestionDirectives';
import { scTabsDirective } from './directives/tabs/tabsDirective';
import { scTextDirective } from './directives/fields/text/textDirective';
import { scTextareaDirective } from './directives/fields/textarea/textareaDirective';
import { scTimeDirective } from './directives/fields/time/timeDirective';
import { scTreeColumnDirective } from './directives/treetable/treeColumnDirective';
import { TreetableCtrl, scTreetableConfigConstant } from './directives/treetable/treetableCtrl';
import { scTreetableDirective } from './directives/treetable/treetableDirective';
import { scRevalidateOnDirective } from './directives/fields/validate/revalidateOnDirective';
import {
  scValidateDirective,
  scValidateAsyncDirective
} from './directives/fields/validate/validateDirective';
import {
  scValidationErrorDirective,
  scValidationErrorConfigConstant
} from './directives/fields/validationError/validationErrorDirective';
import { scaffoldingProvider } from './providers/scaffoldingProvider';
import { scModalProvider } from './providers/scModalProvider';
import { NomenclaturesFactory } from './resources/nomenclatures';
import { MessageModalCtrl } from './services/message/messageModalCtrl';
import { scMessageService } from './services/message/messageService';
import { urlTemplateService } from './services/urlTemplate';

const ScaffoldingModule = angular.module('scaffolding', [
  UiBootstrapModule,
  UiRouterModule,
  UiSelect2Module,
  AngularL10NModule,
  UiJqModule
]);

export default ScaffoldingModule.name;
ScaffoldingModule.directive('scAccessToken', scAccessTokenDirective);
ScaffoldingModule.directive('scBreadcrumb', scBreadcrumbDirective);
ScaffoldingModule.directive('scButton', scButtonDirective);
ScaffoldingModule.directive('scClick', scClickDirective);
ScaffoldingModule.directive('scColumn', scColumnDirective);
ScaffoldingModule.directive('scDatatable', scDatatableDirective);
ScaffoldingModule.directive('scDate', scDateDirective);
ScaffoldingModule.directive('scField', scFieldDirective);
ScaffoldingModule.directive('scFieldGroup', scFieldGroupDirective);
ScaffoldingModule.directive('scFile', scFileDirective);
ScaffoldingModule.directive('scFormName', scFormNameDirective);
ScaffoldingModule.directive('scFormReadonly', scFormReadonlyDirective);
ScaffoldingModule.directive('scHasError', scHasErrorDirective);
ScaffoldingModule.directive('scInfo', scInfoDirective);
ScaffoldingModule.directive('scLinkButton', scLinkButtonDirective);
ScaffoldingModule.directive('scNomenclature', scNomenclatureDirective);
ScaffoldingModule.directive('scFloat', scFloatDirective);
ScaffoldingModule.directive('scInt', scIntDirective);
ScaffoldingModule.directive('scMoney', scMoneyDirective);
ScaffoldingModule.directive('scPager', scPagerDirective);
ScaffoldingModule.directive('scPassword', scPasswordDirective);
ScaffoldingModule.directive('scPromiseState', scPromiseStateDirective);
ScaffoldingModule.directive('scSelect', scSelectDirective);
ScaffoldingModule.directive('scSref', scSrefDirective);
ScaffoldingModule.directive('scTextSuggestion', scTextSuggestionDirective);
ScaffoldingModule.directive('scTextareaSuggestion', scTextareaSuggestionDirective);
ScaffoldingModule.directive('scTabs', scTabsDirective);
ScaffoldingModule.directive('scText', scTextDirective);
ScaffoldingModule.directive('scTextarea', scTextareaDirective);
ScaffoldingModule.directive('scTime', scTimeDirective);
ScaffoldingModule.directive('scTreeColumn', scTreeColumnDirective);
ScaffoldingModule.directive('scTreetable', scTreetableDirective);
ScaffoldingModule.directive('scRevalidateOn', scRevalidateOnDirective);
ScaffoldingModule.directive('scValidate', scValidateDirective);
ScaffoldingModule.directive('scValidateAsync', scValidateAsyncDirective);
ScaffoldingModule.directive('scValidationError', scValidationErrorDirective);
ScaffoldingModule.controller('DatatableCtrl', DatatableCtrl);
ScaffoldingModule.controller('FileCtrl', FileCtrl);
ScaffoldingModule.controller('TreetableCtrl', TreetableCtrl);
ScaffoldingModule.controller('MessageModalCtrl', MessageModalCtrl);
ScaffoldingModule.constant('scDatatableConfig', scDatatableConfigConstant);
ScaffoldingModule.constant('scDateConfig', scDateConfigConstant);
ScaffoldingModule.constant('scFileConfig', scFileConfigConstant);
ScaffoldingModule.constant('scNomenclatureConfig', scNomenclatureConfigConstant);
ScaffoldingModule.constant('scFloatParserFactory', scFloatParserFactoryConstant);
ScaffoldingModule.constant('scFloatFormatterFactory', scFloatFormatterFactoryConstant);
ScaffoldingModule.constant('numberValidations', numberValidationsConstant);
ScaffoldingModule.constant('scPagerConfig', scPagerConfigConstant);
ScaffoldingModule.constant('scTreetableConfig', scTreetableConfigConstant);
ScaffoldingModule.constant('scValidationErrorConfig', scValidationErrorConfigConstant);
ScaffoldingModule.factory('numberDirectiveFactory', numberDirectiveFactoryFactory);
ScaffoldingModule.factory('pager', pagerFactory);
ScaffoldingModule.factory('Nomenclatures', NomenclaturesFactory);
ScaffoldingModule.provider('scaffolding', scaffoldingProvider);
ScaffoldingModule.provider('scModal', scModalProvider);
ScaffoldingModule.service('scMessage', scMessageService);
ScaffoldingModule.service('urlTemplate', urlTemplateService);
