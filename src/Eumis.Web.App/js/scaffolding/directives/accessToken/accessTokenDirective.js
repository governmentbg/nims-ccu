// Usage: <.. sc-access-token ..>

function AccessTokenDirective(accessToken) {
  return {
    restrict: 'A',
    priority: 110,
    link: function(scope, element, attrs) {
      element.on('click', function() {
        if (/access_token/.test(attrs.href)) {
          return;
        } else {
          if (/\?/.test(attrs.href)) {
            attrs.$set('href', attrs.href + '&access_token=' + accessToken.get());
          } else {
            attrs.$set('href', attrs.href + '?access_token=' + accessToken.get());
          }
        }
      });
    }
  };
}

AccessTokenDirective.$inject = ['accessToken'];

export { AccessTokenDirective as scAccessTokenDirective };
