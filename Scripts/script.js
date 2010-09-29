$(document).ready(function () {
    $('.button').append('<span class="hover"></span>').each(function () {
        var $span = $('> span.hover', this).css('opacity', 0);
        $(this).hover(
         function () { $span.stop().fadeTo(800, 1); }
        ,
         function () { $span.stop().fadeTo(850, 0); }
         );
    });
});




