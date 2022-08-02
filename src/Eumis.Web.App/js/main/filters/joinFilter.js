import _ from 'lodash';

function JoinFilter() {
  return function(collection, separator) {
    return _.toArray(collection).join(separator);
  };
}

JoinFilter.$inject = [];

export { JoinFilter as joinFilter };
