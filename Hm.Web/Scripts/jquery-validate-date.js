$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || Globalize.parseDate(value, "dd/MM/yyyy hh:mm tt") !== null || Globalize.parseDate(value, "dd/MM/yyyy") !== null || Globalize.parseDate(value, "dd/MM/yyyy hh:mm") !== null;
    }
});