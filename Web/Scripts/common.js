// 平滑滚动到scrollValue处
function scrollToValue(scrollValue, delay, delayExecute) {
    delay = delay || 0;
    delayExecute = delayExecute || 0;
    //避免因取整误差导致频繁滚动
    if ($(document).scrollTop() >= scrollValue + 1 || $(document).scrollTop() <= scrollValue - 1) {
        disableMouseWheel();
        setTimeout(function() {
            $("html,body").animate({
                scrollTop: scrollValue
            }, delay, 'swing');
        }, delayExecute);
        setTimeout(enableMouseWheel, delay + delayExecute);
    };
};

// 平滑滚动到elObj处中间
function scrollToCenter(el, delay, delayExecute) {
    var winObj = $(window);
    var docObj = $(document);
    var elObj = $(el);
    console.log(elOffset(el).center);
    console.log(winObj.height());
    var scrollValue = elOffset(el).center - winObj.height() / 2;
    var minScrollValue = 0;
    var maxScrollValue = docObj.height() - winObj.height();

    // var elCenterOffset = Math.round(elObj.offset().top + elObj.height() / 2);
    // var elBottomOffset = Math.round(elObj.offset().top + elObj.height());
    // var scrollValue = Math.round(elCenterOffset - windowHeight / 2);

    scrollToValue(scrollValue, delay, delayExecute);

    return scrollValue;
};

// 滚动到elObj顶部
function scrollToTop(el, delay, delayExecute) {
    var scrollValue = Math.round($(el).offset().top);
    if ($(document).scrollTop() < scrollValue) {
        setTimeout(function() {
            $("html,body").animate({
                scrollTop: scrollValue
            }, delay, 'swing');
        }, delayExecute);
    };
    return scrollValue;
};


function elOffset(el) {
    var elObj = $(el);
    var top = elObj.offset().top;
    var bottom = top + elObj.height();
    return {
        'top': top,
        'center': (top + bottom) / 2,
        'bottom': bottom
    };
}

function docOffset() {
    var docObj = $(document);
    var top = docObj.scrollTop();
    var bottom = docObj.scrollTop() + $(window).height();
    return {
        'top': top,
        'center': (top + bottom) / 2,
        'bottom': bottom
    };
};

//
function isOffestInEl(el, offest) {
    var _elOffset = elOffset(el);
    if (offest <= _elOffset.bottom && offest >= _elOffset.top) {
        return true;
    } else {
        return false;
    }
}

function isOffestInWindow(offest) {
    var _docOffset = docOffset();
    if (offest <= _docOffset.bottom && offest >= _docOffset.top) {
        return true;
    } else {
        return false;
    }
}


function disableHandler(e) {
    e.returnValue = false;
}

function disableMouseWheel() {
    addMouseWheelHandler(disableHandler);
};

function enableMouseWheel() {
    removeMouseWheelHandler(disableHandler);
};

/**
 * Removes the auto scrolling action fired by the mouse wheel and trackpad.
 * After this function is called, the mousewheel and trackpad movements won't scroll through sections.
 */
function removeMouseWheelHandler(handler) {
    if (document.addEventListener) {
        document.removeEventListener('mousewheel', handler, false); //IE9, Chrome, Safari, Oper
        document.removeEventListener('wheel', handler, false); //Firefox
        document.removeEventListener('DOMMouseScroll', handler, false); //old Firefox
    } else {
        document.detachEvent('onmousewheel', handler); //IE 6/7/8
    }
};

/**
 * Adds the auto scrolling action for the mouse wheel and trackpad.
 * After this function is called, the mousewheel and trackpad movements will scroll through sections
 */
function addMouseWheelHandler(handler) {
    if (document.addEventListener) {
        document.addEventListener('mousewheel', handler, false); //IE9, Chrome, Safari, Oper
        document.addEventListener('wheel', handler, false); //Firefox
        document.addEventListener('DOMMouseScroll', handler, false); //Old Firefox
    } else {
        document.attachEvent('onmousewheel', handler); //IE 6/7/8
    }
};

function createNav(links) {
    $('.nav');
};


function format(src){
    if (arguments.length == 0) return null;
    var args = Array.prototype.slice.call(arguments, 1);
    return src.replace(/\{(\d+)\}/g, function(m, i){
        return args[i];
    });
};