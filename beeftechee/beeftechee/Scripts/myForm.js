
$(document).ready(function () {
    var $input = $("input");
    $($input).focus(function () {

        $(this).css("border-color", "#FF7E4B");

    });

    $($input).focusout(function () {

        $(this).css("border-color", "#CED4DA");

    });

});
