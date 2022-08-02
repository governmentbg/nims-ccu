/*
Usage <sc-treetable items="data"
        row-class="{'class' : expression}">
 </sc-treetable>
*/
import angular from 'angular';
import _ from 'lodash';
import treetableDirectiveTemplateUrl from './treetableDirective.html';
import 'jquery-treetable/javascripts/src/jquery.treetable';
function TreetableDirective(l10n, $timeout, $parse) {
  return {
    restrict: 'E',
    replace: true,
    transclude: true,
    templateUrl: treetableDirectiveTemplateUrl,
    scope: {},
    link: function($scope, $element, $attrs) {
      var tableHeader,
        tableRows = [],
        treeTable = $element.find('table'),
        rowClassExpr = $parse($attrs.rowClass),
        initializing;

      function renderHeader() {
        if (tableHeader) {
          tableHeader.remove();
        }

        tableHeader = $('<thead></thead>');
        var headerRow = $('<tr></tr>');

        _($scope.columnDefs).forEach(function(columnDef) {
          var headerCell = $('<th></th>')
            .html(columnDef.title + ' <span></span>')
            .width(columnDef.width)
            .addClass(columnDef.columnClass);

          headerRow.append(headerCell);
        });

        tableHeader.append(headerRow);
        treeTable.append(tableHeader);
      }

      function destroyRows() {
        var i, row;

        if (tableRows.length > 0) {
          for (i = 0; i < tableRows.length; i++) {
            row = tableRows[i];

            row.rowElement.remove();
            if (row.scope) {
              row.scope.$destroy();
              row.scope = null;
            }
          }

          tableRows = [];
        }
      }

      function render() {
        var i, j, l1, l2, row, childScope, rowElement, columnDef, cellData, cell, clone;

        destroyRows();

        for (i = 0, l1 = $scope.pageItems.length; i < l1; i++) {
          childScope = null;
          row = {};
          rowElement = $('<tr></tr>');

          rowElement.attr('data-row-id', $scope.pageItems[i].item.rowId);
          rowElement.attr('data-parent-id', $scope.pageItems[i].item.parentId);

          // disable W083: Don't make functions within a loop.
          // because this function is not used as a callback in the future
          _.forOwn(rowClassExpr($scope.$parent, { item: $scope.pageItems[i].item }), function(
            value,
            key
          ) {
            if (value) {
              rowElement.addClass(key);
            }
          });

          for (j = 0, l2 = $scope.columnDefs.length; j < l2; j++) {
            columnDef = $scope.columnDefs[j];

            cellData = $scope.pageItems[i][j];
            cell = $('<td></td>').addClass(columnDef.columnClass);

            if (columnDef.hasContent) {
              if (!childScope) {
                childScope = $scope.$parent.$new();
                childScope.item = $scope.pageItems[i].item;
                row.scope = childScope;
                rowElement.on('$destroy', angular.bind(childScope, childScope.$destroy));
              }

              clone = columnDef.transcludeFn(childScope, angular.noop);

              rowElement.append(cell.append(clone));
            } else if (cellData !== undefined && cellData !== null) {
              rowElement.append(cell.html(cellData));
            } else {
              rowElement.append(cell.html(columnDef.defaultContent));
            }
          }

          treeTable.append(rowElement);
          row.rowElement = rowElement;
          tableRows.push(row);
        }

        if ($scope.pageItems.length === 0) {
          row = {};
          rowElement = $('<tr></tr>');

          rowElement.append(
            $('<td></td>')
              .attr('colspan', _.filter($scope.columnDefs, 'visible').length)
              .append($('<div></div>').html($scope.treeTableTexts.noDataAvailable))
          );

          treeTable.append(rowElement);
          row.rowElement = rowElement;
          tableRows.push(row);
        }
      }

      initializing = true;
      $scope.$parent.$watchCollection($attrs.items, function(items) {
        if (initializing) {
          renderHeader();
          initializing = false;
        }
        if (!items) {
          return;
        }
        $scope.setItems(items);
        render();
        treeTable.treetable();
      });

      $element.bind('$destroy', function onDestroyTreetable() {
        destroyRows();
        treeTable.remove();
      });
    },
    controller: 'TreetableCtrl'
  };
}

TreetableDirective.$inject = ['l10n', '$timeout', '$parse'];

export { TreetableDirective as scTreetableDirective };
