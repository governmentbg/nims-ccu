import angular from 'angular';

function PortalIntegrationModalCtrl(
  $scope,
  $sce,
  $http,
  $timeout,
  $interval,
  $uibModalInstance,
  structuredDocument,
  accessToken,
  scModalParams,
  $exceptionHandler
) {
  var documentUrl;

  if (scModalParams.parentGid && scModalParams.childGid) {
    documentUrl = structuredDocument.getParentChildUrl(
      scModalParams.doc,
      scModalParams.action,
      scModalParams.parentGid,
      scModalParams.childGid
    );
  } else {
    documentUrl = structuredDocument.getUrl(
      scModalParams.doc,
      scModalParams.action,
      scModalParams.xmlGid
    );
  }

  var href = documentUrl,
    currentAccessToken = accessToken.get(),
    ping,
    lastPinged,
    pinging,
    onMessage;

  $scope.title = structuredDocument.getPortalTitle(scModalParams.doc, scModalParams.action);
  $scope.iframeSrc = $sce.trustAsResourceUrl(href);

  $scope.cancel = function() {
    $interval.cancel(ping);
    window.removeEventListener('message', onMessage, false);

    $uibModalInstance.close();
    return $uibModalInstance.result;
  };

  ping = $interval(function() {
    if (
      !window.document.hidden &&
      !pinging &&
      (!lastPinged || (new Date().getTime() - lastPinged.getTime()) / (1000 * 60) > 5) // more than 5 minutes
    ) {
      // block other pings in case the request takes some time
      pinging = true;
      $http.get('api/system/ping').then(
        function() {
          pinging = false;
          lastPinged = new Date();
          if (currentAccessToken !== accessToken.get()) {
            currentAccessToken = accessToken.get();
            $('#portalFrame')[0].contentWindow.postMessage(
              'eumis.updateToken:' + currentAccessToken,
              '*'
            );
          }
        },
        function(error) {
          if (error.status === 401) {
            $scope.cancel()['finally'](function() {
              // no mather the result of the cancel operation
              // pass the original error to the $exceptionHandler
              $exceptionHandler(error);
            });
          } else {
            pinging = false;
          }
        }
      );
    }
  }, 10000); // every 10 second

  onMessage = function(e) {
    if (e.data && e.data.indexOf('eumis.close') === 0) {
      $scope.cancel();
    }
  };

  window.addEventListener('message', onMessage, false);

  angular.element(window.document).ready(function() {
    function setFocus() {
      var iframe = $('#portalFrame')[0];
      iframe.contentWindow.focus();
    }

    $timeout(setFocus, 500);
  });
}

PortalIntegrationModalCtrl.$inject = [
  '$scope',
  '$sce',
  '$http',
  '$timeout',
  '$interval',
  '$uibModalInstance',
  'structuredDocument',
  'accessToken',
  'scModalParams',
  '$exceptionHandler'
];

export { PortalIntegrationModalCtrl };
