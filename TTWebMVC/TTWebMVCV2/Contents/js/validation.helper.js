$(function ($) {
    $.validator.addMethod('togglerequiredfield', function (value, element, params) {
        let isToggleOpen = params["expectedvalue"] === value;
        var targetField = $(params["targetfieldname"]);
        targetField.toggleClass(params['hiddenclass'], !isToggleOpen);
        return true;
    });

    $.validator.unobtrusive.adapters.add('togglerequiredfield', ['expectedvalue', 'targetfieldname', 'hiddenclass'], function (options) {
        options.rules['togglerequiredfield'] = options.params;
        options.messages['togglerequiredfield'] = options.message;
    });
}(jQuery));