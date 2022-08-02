/*
Usage: <eumis-structured-document text="title"
                                  document="type-of-doc-as-defined-in-structuredDocumentConfig"
                                  gid="gid"
                                  readonly="when-to-disable-edit"
                                  [view-title="title-displayed-in-view-modal"]
                                  [edit-title="title-displayed-in-edit-modal"]
                                  [view-mode]
                                  doc-updated="func-called-when-doc-is-updated">
       </eumis-structured-document>
*/

import angular from 'angular';
import structuredDocumentDirectiveTemplateUrl from './structuredDocumentDirective.html';

export const eumisStructuredDocumentDirective = [
  '$parse',
  'l10n',
  'scModal',
  'structuredDocument',
  function($parse, l10n, scModal, structuredDocument) {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        text: '@',
        readonly: '&'
      },
      templateUrl: structuredDocumentDirectiveTemplateUrl,
      link: function link(scope, iElement, iAttrs) {
        var gid = $parse(iAttrs.gid)(scope.$parent),
          getModal = function(action) {
            return scModal.open('portalIntegrationModal', {
              doc: iAttrs.document,
              action: action,
              xmlGid: gid
            });
          };

        scope.viewMode = 'viewMode' in iAttrs;
        scope.viewUrl = function() {
          return structuredDocument.getUrl(iAttrs.document, 'view', gid);
        };

        scope.view = function() {
          var modalInstance = getModal('view');
          modalInstance.result.catch(angular.noop);
          return modalInstance.opened;
        };

        scope.edit = function() {
          var modalInstance = getModal(iAttrs.editAction || 'edit');

          modalInstance.result.then(function() {
            return $parse(iAttrs.docUpdated)(scope.$parent)();
          }, angular.noop);

          return modalInstance.opened;
        };
      }
    };
  }
];
