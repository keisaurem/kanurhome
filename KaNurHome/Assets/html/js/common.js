$(function () {
    $("#content_main").css("padding-top", $("#content_top").height() + 10);
});


/* スクロールされた際に#content_topをトグルするように設定 */
var startPos;
var toggleBlock = false;
function setScrollContentTop() {
    $(window).scroll(function () {
        var tmpPos = $(this).scrollTop();
        if (tmpPos > startPos && $(window).scrollTop() >= 100) {
            $("#content_top").animate({ height: "hide" }, 150);
        } else if (tmpPos < startPos) {
            $("#content_top").animate({ height: "show" }, 150);
        }
        startPos = tmpPos;
    });
}

/* .toggle_button処理 */
function animateToggleButton($target, on_callback, off_callback) {
    if ($target.css("opacity") < 1) {
        $target.animate({
            opacity: 1
        }, 200, on_callback);
    } else {
        $target.animate({
            opacity: 0.3
        }, 200, off_callback);
    }
    return false;
}

/* リップル処理 */
function animateRipple($target, e, callback) {
    var x = e.offsetX;
    var y = e.offsetY;
    var $effect = $target.find(".ripple_effect");
    var w = $target.width();
    var h = $target.height();

    $effect.css({
        width: w,
        height: w
    });
    $effect.css({
        left: x - w / 2,
        top: y - w / 2
    });

    if (!$effect.hasClass("is_show")) {
        $effect.addClass("is_show");
        setTimeout(function () {
            $effect.removeClass("is_show");
            callback();
        }, 750);
    }

    return false;
}