// Usage: <sc-textarea
//          ng-model="<model_name>"
//          rows="<number_of_rows>"
//          cols="<number_of_cols>">
//        </sc-textarea>

function TextareaDirective() {
  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    template: '<textarea class="form-control input-md"></textarea>'
  };
}

export { TextareaDirective as scTextareaDirective };
