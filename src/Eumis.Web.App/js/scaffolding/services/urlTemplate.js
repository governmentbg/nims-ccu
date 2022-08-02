import angular from 'angular';
import _ from 'lodash';

export const urlTemplateService = function() {
  function encodeUriQuery(val, pctEncodeSpaces) {
    return encodeURIComponent(val)
      .replace(/%40/gi, '@')
      .replace(/%3A/gi, ':')
      .replace(/%24/g, '$')
      .replace(/%2C/gi, ',')
      .replace(/%20/g, pctEncodeSpaces ? '%20' : '+');
  }

  function encodeUriSegment(val) {
    return encodeUriQuery(val, true)
      .replace(/%26/gi, '&')
      .replace(/%3D/gi, '=')
      .replace(/%2B/gi, '+');
  }

  function buildUrl(url, params) {
    if (!params) {
      return url;
    }

    var parts = [];
    _.forOwn(params, function(value, key) {
      if (value === null || _.isUndefined(value)) {
        return;
      }

      if (!_.isArray(value)) {
        value = [value];
      }

      _.forEach(value, function(v) {
        if (_.isObject(v)) {
          v = angular.toJson(v);
        }
        parts.push(encodeUriQuery(key) + '=' + encodeUriQuery(v));
      });
    });
    if (parts.length > 0) {
      url += (url.indexOf('?') === -1 ? '?' : '&') + parts.join('&');
    }
    return url;
  }

  function createUrl(template, params, defaults) {
    var url = template,
      val,
      encodedVal,
      urlParams = {},
      queryStringParams = {};

    _.forEach(url.split(/\W/), function(param) {
      if (param === 'hasOwnProperty') {
        throw new Error('hasOwnProperty is not a valid parameter name.');
      }
      if (
        !new RegExp('^\\d+$').test(param) &&
        param &&
        new RegExp('(^|[^\\\\]):' + param + '(\\W|$)').test(url)
      ) {
        urlParams[param] = true;
      }
    });
    url = url.replace(/\\:/g, ':');

    params = params || {};
    _.forEach(urlParams, function(pv, urlParam) {
      val = {}.hasOwnProperty.call(params, urlParam) ? params[urlParam] : defaults[urlParam];
      if (angular.isDefined(val) && val !== null) {
        encodedVal = encodeUriSegment(val);
        url = url.replace(new RegExp(':' + urlParam + '(\\W|$)', 'g'), function(match, p1) {
          return encodedVal + p1;
        });
      } else {
        url = url.replace(new RegExp('(/?):' + urlParam + '(\\W|$)', 'g'), function(
          match,
          leadingSlashes,
          tail
        ) {
          if (tail.charAt(0) === '/') {
            return tail;
          } else {
            return leadingSlashes + tail;
          }
        });
      }
    });

    // strip trailing slashes and set the url
    url = url.replace(/\/+$/, '') || '/';
    // then replace collapse `/.` if found in the last URL path segment before the query
    // E.g. `http://url.com/id./format?q=x` becomes `http://url.com/id.format?q=x`
    url = url.replace(/\/\.(?=\w+($|\?))/, '.');
    // replace escaped `/\.` with `/.`
    url = url.replace(/\/\\\./, '/.');

    // add unused params to queryStringParams
    _.forEach(params, function(value, key) {
      if (!urlParams[key]) {
        queryStringParams[key] = value;
      }
    });

    url = buildUrl(url, queryStringParams);

    return url;
  }

  function UrlTemplate(template) {
    this.template = template;
  }

  UrlTemplate.prototype.getUrl = function(params) {
    return createUrl(this.template, params);
  };

  return function(template) {
    return new UrlTemplate(template);
  };
};
