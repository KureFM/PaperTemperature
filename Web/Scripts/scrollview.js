$(window).ready(function () {
    relocateNav();
    //禁用滚动条
    // document.documentElement.style.overflow = 'hidden';

    // 添加鼠标滚轮事件
    addMouseWheelHandler(mouseWheelHandler);

    //键盘按键事件
    $(document).keydown(function (e) {
        e = e || event;
        switch (e.keyCode) {
            case 32:    //空格
            case 34:    //Page Down
            case 39:    //右方向
            case 40:    //下方向
                nextPage();
                break;
            case 33:    //Page Up
            case 37:    //左方向
            case 38:    //上方向
                prevPage();
                break;
            case 35:    //End
                mouseWheelTo(7);
            case 36:    //Home
                mouseWheelTo(0);
            default:
                break;
        }
    });


    // 导航点点击事件
    $('.nav_link').each(function (index, el) {
        $(el).click(function (event) {
            // event.preventDefault();
            scrollToCenter('.scroll_unit:eq(' + index + ')', 1000);
        });
    });
    // 第一图的下箭头
    $('.next_button_a').click(function (event) {
        mouseWheelTo(1);
    });
});

// unit位置关联导航点
$(window).scroll(function (event) {
    $('.scroll_unit').each(function (index, el) {
        if (isOffestInWindow(elOffset(el).center)) {
            $('.dot:lt(' + index + ')').removeClass('active');
            $('.dot:gt(' + index + ')').removeClass('active');
            $('.dot:eq(' + index + ')').addClass('active');
        }
    });
});



$(window).resize(function () {
    relocateNav();
});


function relocateNav() {
    $('.scorll_nav').css('top', ($(window).height() - $('.scorll_nav').height()) / 2);
}

var __lock = true;
var scrollDelay = 750; //ms

function lockRun(handler) {
    if (__lock) {
        __lock = false;
        handler();
        setTimeout(function () {
            __lock = true;
        }, scrollDelay);
    } else {
        console.log('lock is locked!');
    }
}

// 闪动导航标题1秒
function flashHeadline(index) {
    if (index >= 0 && index <= 7) {
        setTimeout(function () {
            $('.headline:eq(' + index + ')').attr('id', 'flash_headline');
        }, scrollDelay / 2);
        setTimeout(function () {
            $('.headline:eq(' + index + ')').removeAttr('id');
        }, 1000 + scrollDelay / 2);
    }
};

function mouseWheelTo(nextIndex) {
    lockRun(function () {
        scrollToCenter('.scroll_unit:eq(' + nextIndex + ')', scrollDelay);
        flashHeadline(nextIndex);
    });
    console.log('scroll to ' + nextIndex);
};

function mouseWheelToOffset(offset) {
    lockRun(function () {
        if ($(document).scrollTop() >= offset + 1 || $(document).scrollTop() <= offset - 1) {
            scrollToValue(offset, scrollDelay);
            console.log('scroll to ' + offset + 'px');
        }
    });
};

function prevPage() {
    mouseWheelHandler({ 'wheelDelta': 1 });
};

function nextPage(currentIndex) {
    mouseWheelHandler({ 'wheelDelta': -1 });
}

function mouseWheelHandler(e) {
    // cross-browser wheel delta
    e = e || window.event;
    var value = e.wheelDelta || -e.deltaY || -e.detail;
    var delta = Math.max(-1, Math.min(1, value));
    var currentIndex = parseInt($(".dot.active").text()) - 1;

    console.log(currentIndex);

    // 检查参数
    if (currentIndex < 0 || currentIndex > 7) {
        console.log('error, current element index is ' + currentIndex);
        return false;
    }

    console.log('wheel delta is ' + delta);

    // 文档头部可见，第一图下边缘不可见，直接滚动到第一页顶部
    if (delta < 0 && $(document).scrollTop() < $('.scroll_view').offset().top && docOffset().bottom < elOffset('.scroll_unit:eq(0)').bottom) {
        mouseWheelTo(0);
        // mouseWheelToOffset($('.scroll_view').offset().top);
        return;
    }

    if (currentIndex == 0) {
        // 在第一页，只能向下滚动
        if (delta < 0) {
            console.log('mwt:' + currentIndex);
            mouseWheelTo(currentIndex + 1);
        } else if (delta > 0) {
            // 可向上滚至页面顶端
            mouseWheelToOffset(0);
        }
    } else if (currentIndex == 7) {
        // 在最后一页，只能向上滚动
        if (delta > 0) {
            mouseWheelTo(currentIndex - 1);
        }
    } else {
        //处于1-6页，可以随意滚动
        if (delta < 0) {
            mouseWheelTo(currentIndex + 1);
        } else if (delta > 0) {
            mouseWheelTo(currentIndex - 1);
        }
    }
};


// window.addEventListener("mousewheel", function(event) {
//     event.returnValue = false;
// });
// document.onkeydown = function(e) {
//     e = e || event;
//     alert(e.keyCode);
// }
