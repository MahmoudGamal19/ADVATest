function GetManegerName(elm) {
    var Elm = $(elm).val();
    var Elm2 = elm.options[elm.selectedIndex].text;
    $("#ManegerName").val(Elm2);
}