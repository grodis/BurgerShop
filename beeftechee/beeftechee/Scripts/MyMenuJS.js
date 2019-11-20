var count = parseInt($("#cartSpan").text());
$(window).on('resize', function () {
    if ($(window).width() < 768) {
        $('.addtoCartButton').addClass("text-center");
        $('.addtoCartButton').removeClass("text-right");
    } else {
        $('.addtoCartButton').addClass("text-right");
        $('.addtoCartButton').removeClass("text-center");
    }
})

$(function () {
    $(document).ajaxError(function (e, xhr) {
        if (xhr.status == 403) {
            var response = $.parseJSON(xhr.responseText);
            window.location = response.LogOnUrl;
        }
    });

    //This is for the custom burger to display on the modal the total price of the burger 
    //while the user chooses ingredients
    $("select").on('change', function () {
        var priceBread = getPrice($(".mySelectBread option:selected").text());
        var priceMeat = getPrice($(".mySelectMeat option:selected").text());
        var priceCheese = getPrice($(".mySelectCheese option:selected").text());
        var priceSauce = getPrice($(".mySelectSauce option:selected").text());
        var priceVeggie = getPrice($(".mySelectVeggie option:selected").text());

        var total = (priceBread * 100 + priceMeat * 100 + priceCheese * 100 + priceSauce * 100 + priceVeggie * 100) / 100;
        total = total.toString();
        total = total.replace(".", ",");

        $("#totalPriceCustomBurger").html(total + "€");
    });
    function getPrice(inputText) {
        if (inputText == 'None')
            return 0;
        inputText = inputText.split(" ");
        var price = inputText[inputText.length - 1];
        price = price.split("€");
        price = parseFloat(price[0].replace(",", "."));
        return price;
    }


});

$(".myFormBurger").on('submit', function (event) {
    event.preventDefault();
    var id = parseInt($(this).children("#BurgerId").val());
    $.ajax({
        type: 'POST',
        data: { BurgerId: id },
        //url: '@Url.Action("AddToCartBurger", "Cart")',
        url: 'https://localhost:44390/Cart/AddToCartBurger',
        success: function (data) {
            count = count + 1;
            $("#cartSpan").text(count);
            $("#cartSpan").animate({ backgroundColor: 'white', borderColor: 'white', color: "#FF6426" })
                .delay(100)
                .animate({ backgroundColor: "#FF6426", borderColor: "#FF6426" , color: 'white' });
        }
    });
});

$(".myFormDrink").on('submit', function (event) {
    event.preventDefault();
    var id = parseInt($(this).children("#DrinkId").val());
    $.ajax({
        type: 'POST',
        data: { DrinkId: id },
        //url: '@Url.Action("AddToCartDrink", "Cart")',
        url: 'https://localhost:44390/Cart/AddToCartDrink',
        success: function (data) {
            count = count + 1;
            $("#cartSpan").text(count);
            $("#cartSpan").animate({ fontSize: '14px' });
            $("#cartSpan").animate({ fontSize: '12px' });

        }
    });
});
