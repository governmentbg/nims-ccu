// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('money', ['$filter', '$compile', function ($filter, $compile) {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {

                // ***** UPDATE ON BLUR *****
                if (!ngModel.$options) {
                    ngModel.$options = {};
                }
                ngModel.$options.updateOn = "blur";
                ngModel.$options.updateOnDefault = false;
                // ***** END UPDATE ON BLUR *****

                // ***** MIN/MAX VALUE *****
                var minVal = attrs.minValue ? parseFloat(attrs.minValue) : null;
                var maxVal = attrs.maxValue ? parseFloat(attrs.maxValue) : 2000000000;
                var defaultValue = attrs.nullDefaultValue ? null : '0.00';
                // ***** END MIN/MAX VALUE *****

                // ***** HELPER FUNCTIONS *****
                function decimalRex(dChar) {
                    return RegExp("-?\\d|\\" + dChar, 'g');
                }

                function clearRex(dChar) {
                    return RegExp("((\\" + dChar + ")|(^-?[0-9]{1,}\\" + dChar + "?))&?[0-9]*", 'g');
                }

                function decimalSepRex(dChar) {
                    return RegExp("\\" + dChar, "g");
                }

                function clearMoneyValue(value) {
                    value = String(value);
                    var dSeparator = ',';
                    var clear = null;

                    if (value.match(decimalSepRex(dSeparator))) {
                        clear = value.match(decimalRex(dSeparator))
                            .join("").match(clearRex(dSeparator));
                        clear = clear ? clear[0].replace(dSeparator, ".") : null;
                    }
                    else if (value.match(decimalSepRex("."))) {
                        clear = value.match(decimalRex("."))
                            .join("").match(clearRex("."));
                        clear = clear ? clear[0] : null;
                    }
                    else {
                        clear = value.match(/-?\d/g);
                        clear = clear ? clear.join("") : null;
                    }

                    return clear;
                }

                function formatSpaces(num) {
                    if (!num)
                        return;

                    var str = num.toString().split('.');
                    if (str[0].length >= 4) {
                        str[0] = str[0].replace(/(\d)(?=(\d{3})+$)/g, '$1 ');
                    }

                    if (str.length == 1) {
                        str[1] = '00';
                    }
                    else if (str[1].length == 1) {
                        str[1] += '0';
                    }

                    return str.join('.');
                }

                // ***** END HELPER FUNCTIONS *****

                // ***** PARSERS/FORMATTERS *****
                scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                    ngModel.$setViewValue(formatSpaces(ngModel.$modelValue || defaultValue));
                    ngModel.$render();
                });

                ngModel.$parsers.push(function (viewValue) {
                    var cVal = clearMoneyValue(viewValue);
                    var rVal = parseFloat(cVal);

                    if (minVal && rVal < minVal) {
                        rVal = minVal;
                    }

                    if (maxVal && rVal > maxVal) {
                        rVal = maxVal;
                    }

                    return isNaN(rVal) ? defaultValue : rVal.toFixed(2) || defaultValue;
                });
                // ***** END PARSERS/FORMATTERS *****

                // ***** FOCUS LOGIC *****
                var focusedElement;
                element.on('click', function () {
                    if (focusedElement != this) {
                        this.select();
                        focusedElement = this;
                    }
                });
                element.on('blur', function () {
                    focusedElement = null;

                    var parsers = ngModel.$parsers,
                        idx = parsers.length,
                        value = ngModel.$viewValue;

                    while (idx--) {
                        value = formatSpaces(parsers[idx](value) || defaultValue);
                    }

                    if (ngModel.$viewValue !== value) {
                        ngModel.$viewValue = value;
                        ngModel.$render();
                    }
                });

                element.bind('$destroy', function () {
                    element.unbind('blur');
                });
                // ***** END FOCUS LOGIC *****
            }
        }
    }]);
}(angular));