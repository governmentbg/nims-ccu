/*
Usage <sc-datatable items="data"
        filterable="true|false"
        pageable="true|false"
        sortable="true|false"
        scrollable="true|false"
        has-info-text="true|false"
        dynamic-columns="true|false"
        row-class="{'class' : expression}">
 </sc-datatable>
*/

import angular from 'angular';
import _ from 'lodash';
import datatableDirectiveTemplateUrl from './datatableDirective.html';
function DatatableDirective(l10n, $timeout, $parse) {
  return {
    restrict: 'E',
    replace: true,
    transclude: true,
    templateUrl: datatableDirectiveTemplateUrl,
    scope: {
      filterable: '&',
      pageable: '&',
      sortable: '&',
      dynamicColumns: '&',
      scrollable: '&',
      hasInfoText: '&',
      defaultSort: '@',
      defaultSortAscDesc: '@',
      tableControl: '='
    },
    link: function($scope, $element, $attrs) {
      var select2,
        tableHeader,
        tableBody,
        tableRows = [],
        dataTable = $element.find('table'),
        rowClassExpr = $parse($attrs.rowClass),
        sortable = $scope.sortable() === undefined ? true : $scope.sortable();

      //fill the given tableControl object in the table-control attribute
      //with the API(defined in the controller of the directive)
      //of the datatable directive
      if ($scope.tableControl !== null) {
        if (angular.isObject($scope.tableControl)) {
          $scope.tableControl.getFilteredItems = $scope.getFilteredItems;
        }
      }

      $scope.filterable = $scope.filterable() === undefined ? true : $scope.filterable();
      $scope.scrollable = $scope.scrollable() === undefined ? true : $scope.scrollable();
      $scope.pageable = $scope.pageable() === undefined ? true : $scope.pageable();
      $scope.hasInfoText = $scope.hasInfoText() === undefined ? true : $scope.hasInfoText();
      $scope.dynamicColumns =
        $scope.dynamicColumns() === undefined ? true : $scope.dynamicColumns();
      $scope.hasGroupedColumns = _.some($scope.columnDefs, function(columnDef) {
        return columnDef.colGroup;
      });
      $scope.pageSize = '10';

      var renderHeader = function() {
        if (tableHeader) {
          tableHeader.remove();
        }

        tableHeader = $('<thead></thead>');
        var headerRow = $('<tr></tr>'),
          groupHeaderRow = $('<tr></tr>'),
          currentColGroup,
          currentColGroupColSpan = 0,
          currentColGroupHeaderCell,
          appendToGroupHeader;

        appendToGroupHeader = function() {
          if (currentColGroupColSpan > 1) {
            currentColGroupHeaderCell.attr('colspan', currentColGroupColSpan);
          }
          groupHeaderRow.append(currentColGroupHeaderCell);
          currentColGroupColSpan = 0;
          currentColGroupHeaderCell = null;
        };

        _($scope.columnDefs).forEach(function(columnDef, index) {
          if (!columnDef.visible) {
            return;
          }

          var headerCell = $('<th></th>')
            .html(columnDef.title + ' <span></span>')
            .width(columnDef.width)
            .addClass(columnDef.columnClass);

          if ($scope.hasGroupedColumns) {
            if (columnDef.colGroup) {
              if (columnDef.colGroup !== currentColGroup) {
                if (currentColGroupHeaderCell) {
                  appendToGroupHeader();
                }
                currentColGroup = columnDef.colGroup;
                currentColGroupHeaderCell = $('<th style="text-align: center;"></th>').html(
                  l10n.get(columnDef.colGroup)
                );
              }
              currentColGroupColSpan++;
            } else {
              if (currentColGroupHeaderCell) {
                appendToGroupHeader();
              }
              headerCell.attr('rowspan', 2).css({ 'vertical-align': 'middle' });
              groupHeaderRow.append(headerCell);
            }

            if (index === $scope.columnDefs.length - 1 && currentColGroupHeaderCell) {
              appendToGroupHeader();
            }
          }

          if (sortable) {
            if (columnDef.sortable) {
              var sortingSpan = $('span', headerCell);

              headerCell.addClass('sorting');

              if ($scope.sortingColumnIndex && $scope.sortingColumnIndex === index) {
                if ($scope.sortingType === 'asc') {
                  sortingSpan.addClass('glyphicon glyphicon-sort-by-attributes');
                } else {
                  sortingSpan.addClass('glyphicon glyphicon-sort-by-attributes-alt');
                }
              } else {
                sortingSpan.addClass('glyphicon glyphicon-sort');
              }

              headerCell.on('click', function() {
                $scope.$apply(function() {
                  if (sortingSpan.hasClass('glyphicon-sort-by-attributes')) {
                    $('th.sorting span', headerRow).removeClass();
                    $('th.sorting span', headerRow).addClass('glyphicon glyphicon-sort');

                    sortingSpan.removeClass();
                    sortingSpan.addClass('glyphicon glyphicon-sort-by-attributes-alt');

                    $scope.setSortingData(index, 'desc');
                  } else {
                    $('th.sorting span', headerRow).removeClass();
                    $('th.sorting span', headerRow).addClass('glyphicon glyphicon-sort');

                    sortingSpan.removeClass();
                    sortingSpan.addClass('glyphicon glyphicon-sort-by-attributes');

                    $scope.setSortingData(index, 'asc');
                  }
                });
              });
            }
          }

          if (!$scope.hasGroupedColumns || ($scope.hasGroupedColumns && columnDef.colGroup)) {
            headerRow.append(headerCell);
          }
        });

        if ($scope.hasGroupedColumns) {
          tableHeader.append(groupHeaderRow);
        }
        tableHeader.append(headerRow);
        dataTable.append(tableHeader);
      };

      var destroyRows = function() {
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
      };

      var render = function() {
        var i, j, l1, l2, row, childScope, rowElement, columnDef;

        destroyRows();

        if (tableBody) {
          tableBody.remove();
        }

        tableBody = $('<tbody></tbody>');

        for (i = 0, l1 = $scope.pageItems.length; i < l1; i++) {
          childScope = null;
          row = {};
          rowElement = $('<tr></tr>');

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

            if (!columnDef.visible) {
              continue;
            }

            var cellData = $scope.pageItems[i][j];
            var cell;
            if (columnDef.type === 'money') {
              cell = $('<td style="white-space: nowrap; text-align: right;"></td>').addClass(
                columnDef.columnClass
              );
            } else {
              cell = $('<td></td>').addClass(columnDef.columnClass);
            }

            if (columnDef.hasContent) {
              if (!childScope) {
                childScope = $scope.$parent.$new();
                childScope.item = $scope.pageItems[i].item;
                row.scope = childScope;
                rowElement.on('$destroy', angular.bind(childScope, childScope.$destroy));
              }

              var clone = columnDef.transcludeFn(childScope, angular.noop);

              rowElement.append(cell.append(clone));
            } else if (cellData !== undefined && cellData !== null) {
              rowElement.append(cell.html(cellData));
            } else {
              rowElement.append(cell.html(columnDef.defaultContent));
            }
          }

          tableBody.append(rowElement);
          dataTable.append(tableBody);
          row.rowElement = rowElement;
          tableRows.push(row);
        }

        if ($scope.pageItems.length === 0) {
          var noDatatext = $scope.filter
            ? $scope.dataTableTexts.noFilteredDataAvailable
            : $scope.dataTableTexts.noDataAvailable;
          row = {};
          rowElement = $('<tr></tr>');

          rowElement.append(
            $('<td></td>')
              .attr('colspan', _.filter($scope.columnDefs, 'visible').length)
              .append($('<div></div>').html(noDatatext))
          );

          tableBody.append(rowElement);
          dataTable.append(tableBody);
          row.rowElement = rowElement;
          tableRows.push(row);
        }
      };

      select2 = $element.find('select').select2({
        minimumResultsForSearch: -1
      });

      if ($scope.dynamicColumns) {
        //TODO find a way to do that with an inline filter
        $scope.nonEmpty = function(col) {
          return col.title !== '';
        };

        $scope.renderColumns = function() {
          renderHeader();
          render();
        };
      }

      var initializing = true;
      $scope.$parent.$watchCollection($attrs.items, function(items) {
        if ($scope.defaultSort) {
          var index = _.findIndex($scope.columnDefs, { data: $scope.defaultSort });
          if ($scope.defaultSortAscDesc) {
            $scope.setSortingData(index, $scope.defaultSortAscDesc);
          } else {
            $scope.setSortingData(index, 'desc');
          }
        }

        if (initializing) {
          renderHeader();

          initializing = false;
        }

        if (!items) {
          return;
        }
        $scope.setItems(items);
        render();
      });

      $scope.$watch('pageSize', function(pageSize) {
        $scope.setPageSize(pageSize);
      });

      $scope.$watchCollection('pageItems', function() {
        if (initializing) {
          return;
        }
        render();
      });

      $scope.$watch('filter', function(filter) {
        $scope.setFilter(filter);
      });

      $element.bind('$destroy', function onDestroyDatatable() {
        if (select2) {
          select2.select2('destroy');
        }

        destroyRows();

        dataTable.remove();
      });
    },
    controller: 'DatatableCtrl'
  };
}

DatatableDirective.$inject = ['l10n', '$timeout', '$parse'];

export { DatatableDirective as scDatatableDirective };
