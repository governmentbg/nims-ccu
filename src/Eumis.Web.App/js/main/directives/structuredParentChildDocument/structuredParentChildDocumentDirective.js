/*
Usage: <eumis-structured-parent-child-document text="title"
                                  document="type-of-doc-as-defined-in-structuredDocumentConfig"
                                  communicationGid="communicationGid"
                                  answerGid="answerGid"
                                  readonly="when-to-disable-edit"
                                  [view-title="title-displayed-in-view-modal"]
                                  [edit-title="title-displayed-in-edit-modal"]
                                  [view-mode]
                                  doc-updated="func-called-when-doc-is-updated">
       </eumis-structured-parent-child-document>
*/

import angular from 'angular';
import structuredParentChildDocumentDirectiveTemplateUrl from './structuredParentChildDocumentDirective.html';

export const eumisStructuredParentChildDocumentDirective = [
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
      templateUrl: structuredParentChildDocumentDirectiveTemplateUrl,
      link: function link(scope, iElement, iAttrs) {
        var parentGid = $parse(iAttrs.parentGid)(scope.$parent),
          childGid = $parse(iAttrs.childGid)(scope.$parent),
          getModal = function(action) {
            return scModal.open('portalIntegrationModal', {
              doc: iAttrs.document,
              action: action,
              parentGid: parentGid,
              childGid: childGid
            });
          };

        scope.viewMode = 'viewMode' in iAttrs;
        scope.viewUrl = function() {
          return structuredDocument.getParentChildUrl(iAttrs.document, 'view', parentGid, childGid);
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
