$(window).ready(function () {
    $('.picunit a').each(function (i, el) {
        var imgurl = $('.picdetailurl span:eq(' + i + ')').text();
        console.log(imgurl);
        $(el).click(function (event) {
            document.documentElement.style.overflow = 'hidden';
            imgtag = format('<img src="{0}" alt="" />', imgurl);
            console.log(imgtag);
            $('.picscroll').append(imgtag);
            $('.picdetail').css('width', $(window).width());
            $('.picdetail').css('height', $(window).height());
            $('.picdetail').show(500);
            event.preventDefault();
        });
    });
    $('.picdetail').click(function (event) {
        disableDetail(event);
    });
    $('.picarticle').click(function (event) {
        event.stopPropagation();
    });
    $('.close').click(function (event) {
        disableDetail(event);
    });
});


function disableDetail(event) {
    $('.picdetail').hide(500);
    document.documentElement.style.overflow = 'auto';
    event.preventDefault();
    $('.picscroll').empty();
}