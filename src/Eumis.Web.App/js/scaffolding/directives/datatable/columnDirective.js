/*
Usage: <sc-column model-name="property"
        type="string|numeric|date"
        sortable="true|false"
        visible="true|false"
        class=""
        [title]
        has-content="true|false">
      </sc-column>
*/

function ColumnDirective() {
  return {
    restrict: 'E',
    require: '^scDatatable',
    transclude: true,
    link: function(scope, iElement, iAttrs, scDatatable1, childTranscludeFn) {
      scDatatable1.addColumn({
        transcludeFn: childTranscludeFn,
        hasContent: iAttrs.hasContent ? true : false,
        type: iAttrs.type,
        data: iAttrs.data,
        title: iAttrs.title,
        sortable: iAttrs.sortable,
        visible: iAttrs.visible,
        defaultValue: iAttrs.defaultValue,
        width: iAttrs.width || null,
        class: iAttrs['class'],
        colGroup: iAttrs.colGroup
      });
    }
  };
}

ColumnDirective.$inject = [];

export { ColumnDirective as scColumnDirective };
