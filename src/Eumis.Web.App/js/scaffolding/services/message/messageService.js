// Usage: scMessage(<message text l10n string>, [<buttons object>])

import messageModalTemplateUrl from './messageModal.html';

export const scMessageService = [
  '$uibModal',
  function($uibModal) {
    return function(l10nText, buttons) {
      buttons = buttons || [
        {
          name: 'OK',
          l10nLabel: 'scaffolding.scMessage.okButton',
          type: 'btn-primary',
          icon: 'glyphicon-ok'
        },
        {
          name: 'Cancel',
          l10nLabel: 'scaffolding.scMessage.cancelButton',
          type: 'btn-default',
          icon: 'glyphicon-ban-circle'
        }
      ];
      var modal = $uibModal.open({
        templateUrl: messageModalTemplateUrl,
        controller: 'MessageModalCtrl',
        backdrop: 'static',
        keyboard: false,
        resolve: {
          l10nText: function() {
            return l10nText;
          },
          buttons: function() {
            return buttons;
          }
        }
      });
      return modal.result;
    };
  }
];
