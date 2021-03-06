function TreetableCtrl($scope, $element, $attrs, $parse, l10n, $filter, scTreetableConfig) {
  $scope.columnDefs = [];
  $scope.pageItems = [];
  $scope.treeTableTexts = {
    noDataAvailable: l10n.get('scaffolding.scTreetable.noDataAvailable')
  };

  var columnIndex = 0;

  this.addColumn = function(column) {
    var dataFunction = null,
      parsedExpression;
    if (column.data) {
      parsedExpression = $parse(column.data);
      dataFunction = function(item) {
        if (column.type === 'date') {
          return $filter('date')(parsedExpression(item), scTreetableConfig.format);
        } else if (column.type === 'boolean') {
          return parsedExpression(item) ? 'Да' : 'Не';
        } else {
          return parsedExpression(item);
        }
      };
    }

    $scope.columnDefs.push({
      transcludeFn: column.transcludeFn,
      hasContent: column.hasContent,
      data: column.data,
      title: l10n.get(column.title) || '',
      dataFunction: dataFunction,
      type: column.type || 'string',
      index: [columnIndex++],
      defaultContent: '',
      columnClass:
        (column['class'] || '') +
        ' scdt-' +
        (column.data ? column.data.replace(/[[\].]/g, '_') : 'empty'),
      width: column.width,
      rowId: column.rowId,
      parentId: column.parentId
    });
  };

  $scope.setItems = function(items) {
    var mappedItem, i, j, l1, l2;
    $scope.pageSize = items.length || 1;

    for (i = 0, l1 = items.length; i < l1; i++) {
      mappedItem = [];
      mappedItem.item = items[i];
      for (j = 0, l2 = $scope.columnDefs.length; j < l2; j++) {
        if ($scope.columnDefs[j].dataFunction) {
          mappedItem.push($scope.columnDefs[j].dataFunction(items[i]));
        } else {
          mappedItem.push(null);
        }
      }

      $scope.pageItems.push(mappedItem);
    }
  };

  $scope.$on('$destroy', function() {
    $scope.columnDefs = null;
    $scope.pageItems = null;
  });
}

TreetableCtrl.$inject = [
  '$scope',
  '$element',
  '$attrs',
  '$parse',
  'l10n',
  '$filter',
  'scTreetableConfig'
];

export { TreetableCtrl };

export const scTreetableConfigConstant = {
  format: 'mediumDate'
};
