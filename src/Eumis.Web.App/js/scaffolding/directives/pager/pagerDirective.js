// Usage: <sc-pager count="count" page="page" change="fn"></sc-pager>

import _ from 'lodash';
import pagerDirectiveTemplateUrl from './pagerDirective.html';

function PagerDirective($parse, scPagerConfig) {
  function PagerLink(scope, element, attrs) {
    var count = $parse(attrs.count)(scope.$parent),
      median = Math.floor(scPagerConfig.numberOfButtons / 2),
      pageNum = Math.ceil(count / scPagerConfig.itemsPerPage),
      buttonNum = Math.min(pageNum, scPagerConfig.numberOfButtons),
      pageArr;

    scope.pageNum = pageNum;
    scope.count = count;
    scope.currentPage = parseInt($parse(attrs.page)(scope.$parent), 10) || 1;

    if (scope.currentPage <= median) {
      pageArr = _.range(1, buttonNum + 1);
    } else if (scope.currentPage > pageNum - buttonNum + median) {
      pageArr = _.range(pageNum - buttonNum + 1, pageNum + 1);
    } else {
      pageArr = _.range(
        scope.currentPage - median,
        scope.currentPage - median + scPagerConfig.numberOfButtons
      );
    }

    scope.pagingContents = _.map(pageArr, function(page) {
      return {
        number: page
      };
    });

    if (pageNum > buttonNum && scope.currentPage - median > 1) {
      scope.pagingContents.unshift({ text: '...' });
    }

    if (pageNum > buttonNum && pageNum - buttonNum + median >= scope.currentPage) {
      scope.pagingContents.push({ text: '...' });
    }
  }

  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    link: PagerLink,
    scope: {
      change: '&'
    },
    templateUrl: pagerDirectiveTemplateUrl
  };
}

PagerDirective.$inject = ['$parse', 'scPagerConfig'];

export { PagerDirective as scPagerDirective };

export const scPagerConfigConstant = {
  itemsPerPage: 10,
  numberOfButtons: 6
};

export const pagerFactory = [
  'scPagerConfig',
  function PagerFactory(scPagerConfig) {
    return {
      getOffsetAndLimit: function(page) {
        page = page || 1;

        return {
          offset: (page - 1) * scPagerConfig.itemsPerPage,
          limit: scPagerConfig.itemsPerPage
        };
      }
    };
  }
];
