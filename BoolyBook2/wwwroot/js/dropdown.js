var dropdown = $('.adt-menu');
var item = $('.adt-item');

item.on('click', function () {
    item.toggleClass('collapse-1');

    if (dropdown.hasClass('dropped')) {
        dropdown.toggleClass('dropped');
    } else {
        setTimeout(function () {
            dropdown.toggleClass('dropped');
        }, 150);
    }
})