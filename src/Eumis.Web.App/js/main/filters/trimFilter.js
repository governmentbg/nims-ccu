function TrimFilter($filter) {
  return function(item, maxLength) {
    if (!item) {
      return item;
    }

    return $filter('limitTo')(item, maxLength) + (item.length > maxLength ? '...' : '');
  };
}

TrimFilter.$inject = ['$filter'];

export { TrimFilter as trimFilter };
