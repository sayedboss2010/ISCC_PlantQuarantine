//var $windowWidth = $(window).width(); var $rightSidebarControl = $(".right-sidebar");
//var $navigationControl = $(".menu-control");
//var $leftNavigation = $("#left-navigation");
//var $minWrapper = $("#min-wrapper");
//var $navigation = $("ul.mainNav");
//var $breadcrumb3 = $("#brc1");
//jQuery(document).ready(function (a) {
//    call_navigation(); if ($windowWidth > 1025) { onDesktop() }
//    else {
//        if ($windowWidth < 500) { onPhoneDefault() }
//        else { if ($windowWidth < 1025) { onTabletDefault() } }
//    } minimize_left_menu_hover_Display(); right_box_display(); phone_nav_control(); layout_change_color_start(); plugin_load_for_layout(); panel_change_start(); dropdown_top_nav_bar(); dropDownMenuControl(); left_bar_minimize()
//});





//function call_navigation() { $navigation.multiAccordion({ multiAccordion: true, speed: 500, closedSign: '<i class="fas fa-ellipsis-h"></i>', openedSign: '<i class="fas fa-ellipsis-h"></i>' }) }



//function minimize_left_menu_hover_Display() {
//    $("ul.mainNav li").hover(function () { if ($($leftNavigation).hasClass("active")) { $(this).children("ul").addClass("open") } },
//        function () { if ($($leftNavigation).hasClass("active")) { $(this).children("ul").removeClass("open"); $(this).children("ul").removeAttr("style") } })
//}

//function dropDownMenuControl() { $("ul.mainNav li").children("ul").removeAttr("style") } function changeMenuSizeTriger() { $(window).trigger("resize") }



//function left_bar_minimize() {
//    $($navigationControl).click(function () {
//        if ($navigation.hasClass("active")) { $leftNavigation.removeClass("active"); $navigation.removeClass("active"); $minWrapper.removeClass("active"); changeMenuSizeTriger() }
//        else { $navigation.addClass("active"); $minWrapper.addClass("active"); $leftNavigation.addClass("active"); $navigation.find("ul").removeAttr("style"); changeMenuSizeTriger() }
//    })
//    ///////////////////////menna////////////////
//    $($navigationControl).click(function () {
//        if ($breadcrumb3.hasClass("active")) { $leftNavigation.removeClass("active"); $breadcrumb3.removeClass("active"); $minWrapper.removeClass("active"); changeMenuSizeTriger() }
//        else { $breadcrumb3.addClass("active"); $minWrapper.addClass("active"); $leftNavigation.addClass("active"); $breadcrumb3.find("ul").removeAttr("style"); changeMenuSizeTriger() }
//    })
//    ////////////////////////////
//}

//function onDesktop() { }
//function onTabletDefault() { $navigation.addClass("active"); $minWrapper.addClass("active"); $leftNavigation.addClass("active"); $navigation.slideDown() }
//function onTablet() { $navigation.addClass("active"); $minWrapper.addClass("active"); $leftNavigation.addClass("active") }
//function onPhoneDefault() { $navigation.addClass("mobile"); $navigation.css("display", "none"); $leftNavigation.css("width", "100%"); $navigationControl.removeClass("spinIn").addClass("spinOut"); $navigationControl.removeClass("active"); $leftNavigation.children("ul").removeClass("active"); $leftNavigation.removeClass("active"); $($minWrapper).css("paddingLeft", "0") }
//function onPhone() {
//    $($navigation).addClass("mobile"); $($navigation).css("display", "none"); $($leftNavigation).animate({ width: "100%" }, 100,
//        function () { $navigationControl.removeClass("spinIn").addClass("spinOut"); $navigationControl.removeClass("active"); $leftNavigation.children("ul").removeClass("active"); $leftNavigation.removeClass("active") }); $($minWrapper).animate({ paddingRight: "0" }, 100,
//            function () { })
//} var resizeId; $(window).resize(function () { clearTimeout(resizeId); resizeId = setTimeout(doneResizingLayout, 500) });
//function doneResizingLayout() {
//    var a = $(window).width(); if ($windowWidth != a) {
//        if (a < 500) { onPhone() }
//        else { if (a < 1025) { $leftNavigation.removeAttr("style"); $minWrapper.removeAttr("style"); $leftNavigation.removeAttr("style"); $navigation.removeAttr("style"); $navigation.removeClass("mobile"); onTablet() } else { if (a > 1025) { desktopResize() } } } $windowWidth = a
//    } else { $windowWidth = a }
//} function desktopResize() { $leftNavigation.removeAttr("style"); $minWrapper.removeAttr("style"); $leftNavigation.removeAttr("style"); $navigation.removeAttr("style"); $navigation.removeClass("mobile"); onDesktop() } function right_box_display() {
//    $rightSidebarControl.click(function () {
//        $("#setting-tab a:first").tab("show"); $("#right-wrapper").animate({ left: "0" }, 500,
//            function () { })
//    }); var a = $(".right-sidebar-setting"); a.click(function () { $("#setting-tab a:last").tab("show"); $("#right-wrapper").animate({ left: "0" }, 500, function () { }) }); $("#right-wrapper .close-right-wrapper a").click(function () { $("#right-wrapper").animate({ left: "-280px" }, 500, function () { }) })
//} function phone_nav_control() { $(".phone-nav-control").click(function () { if ($navigation.is(":hidden")) { $navigation.slideDown() } else { $navigation.slideUp() } }) } function layout_change_color_start() { var a = $("body"); $(".change-color-box ul li ").click(function () { a.removeClass("black-color"); a.removeClass("blue-color"); a.removeClass("deep-blue-color"); a.removeClass("red-color"); a.removeClass("light-green-color"); a.removeClass("default"); $(".change-color-box ul li ").removeClass("active"); if ($(this).hasClass("active")) { } else { var c = $(this).attr("class"); a.addClass(c); $(this).addClass("active") } }); var b = $("#change-color"); $("#change-color-control a").click(function () { if ($(this).hasClass("active")) { $(this).removeClass("active"); $(b).animate({ left: "-200px" }, 500, function () { }) } else { $(this).addClass("active"); $(b).animate({ left: "0" }, 500, function () { }) } }) } function panel_change_start() {
//    $(".panel-control li a.close-panel").click(function () { var a = $(this).parents(".panel"); a.animate({ opacity: 0.1 }, 1000, function () { $(this).remove() }) }); $(".panel-control li a.minus").click(function () {
//        var a = $(this).parents(".panel").children(".panel-body"); if ($(this).hasClass("active")) {
//            a.slideDown(200); $(this).children("i").removeClass("fa-square-o");
//            $(this).children("i").addClass("fa-minus"); $(this).removeClass("active")
//        }
//        else { a.slideUp(200); $(this).children("i").removeClass("fa-minus"); $(this).children("i").addClass("fa-square-o"); $(this).addClass("active") }
//    })
//} function dropdown_top_nav_bar() {
//    $(".dropdown").on("show.bs.dropdown",
//        function (a) { $(this).find(".dropdown-menu").first().stop(true, true).slideDown(500, function () { }) }); $(".dropdown").on("hide.bs.dropdown",
//            function (a) { $(this).find(".dropdown-menu").first().stop(true, true).slideUp(500, function () { }) })
//} function plugin_load_for_layout() {
//    try {
//        $(".nano").nanoScroller({ preventPageScrolling: true, alwaysVisible: true, scroll: "top" }); $(".nano-chat").nanoScroller({ preventPageScrolling: true, alwaysVisible: true, scroll: "bottom" }); $(".progress-bar").progressbar({ display_text: "fill" }); $(".easyPieChart").easyPieChart({
//            barColor: $redActive, scaleColor: $redActive, easing: "easeOutBounce", onStep:
//                function (i, h, e) { $(this.el).find(".easyPiePercent").text(Math.round(e)) }
//        }); var f = window.chart = $(".easyPieChart").data("easyPieChart"); $(".js_update").on("click", function () { f.update(Math.random() * 200 - 100) }); var d = Array.prototype.slice.call(document.querySelectorAll(".js-switch")); d.forEach(function (e) { var h = new Switchery(e) }); var c = Array.prototype.slice.call(document.querySelectorAll(".js-switch-red")); c.forEach(function (h) { var e = new Switchery(h, { color: $redActive }) }); var b = Array.prototype.slice.call(document.querySelectorAll(".js-switch-light-green")); b.forEach(function (h) { var e = new Switchery(h, { color: $lightGreen }) }); var a = Array.prototype.slice.call(document.querySelectorAll(".js-switch-light-blue")); a.forEach(function (e) { var h = new Switchery(e, { color: $lightBlueActive }) })
//    } catch (g) { }
//} function detectIE() { var c = window.navigator.userAgent; var b = c.indexOf("MSIE "); var a = c.indexOf("Trident/"); if (b > 0) { return parseInt(c.substring(b + 5, c.indexOf(".", b)), 10) } if (a > 0) { var d = c.indexOf("rv:"); return parseInt(c.substring(d + 3, c.indexOf(".", d)), 10) } return false };




///////////////////////
var $windowWidth = $(window).width();
var $breadcrumb3 = $("#brc1");
var $rightSidebarControl = $(".right-sidebar");
var $navigationControl = $(".menu-control");
var $leftNavigation = $("#left-navigation");
var $minWrapper = $("#min-wrapper");
var $navigation = $("ul.mainNav");
jQuery(document).ready(function (a) {
    call_navigation();
    if ($windowWidth > 1025) { onDesktop() }
    else {
        if ($windowWidth < 500) { onPhoneDefault() }
        else { if ($windowWidth < 1025) { onTabletDefault() } }
    } minimize_left_menu_hover_Display(); right_box_display(); phone_nav_control(); layout_change_color_start(); plugin_load_for_layout(); panel_change_start(); dropdown_top_nav_bar(); dropDownMenuControl(); left_bar_minimize()
});
function call_navigation() { $navigation.multiAccordion({ multiAccordion: true, speed: 500, closedSign: '<i class="fas fa-ellipsis-h"></i>', openedSign: '<i class="fas fa-ellipsis-h"></i>' }) } function minimize_left_menu_hover_Display() {
    $("ul.mainNav li").hover(function () {
        if ($($leftNavigation).hasClass("active"))
        { $(this).first("ul").addClass("open") }


    }, function () { if ($($leftNavigation).hasClass("active")) { $(this).children("ul").removeClass("open"); $(this).children("ul").removeAttr("style") } })
} function dropDownMenuControl() { $("ul.mainNav li").children("ul").removeAttr("style") } function changeMenuSizeTriger() { $(window).trigger("resize") }
function left_bar_minimize() {
    $($navigationControl).click(function () {
        if ($navigation.hasClass("active")) { $leftNavigation.removeClass("active"); $navigation.removeClass("active"); $minWrapper.removeClass("active"); changeMenuSizeTriger() }
        else {
            $navigation.addClass("active");
            $minWrapper.addClass("active");
            $leftNavigation.addClass("active");
            $breadcrumb3.addClass("active");
            $navigation.find("ul").removeAttr("style"); changeMenuSizeTriger()
        }
    })
} function onDesktop() { } function onTabletDefault()
{
    $navigation.addClass("active");
    $minWrapper.addClass("active");
    $leftNavigation.addClass("active");
    $navigation.slideDown()
    $breadcrumb3.addClass("active");
}
function onTablet()
{
    $navigation.addClass("active");
    $minWrapper.addClass("active");
    $leftNavigation.addClass("active")
    $breadcrumb3.addClass("active");
}
function onPhoneDefault() {
    $navigation.addClass("mobile"); $navigation.css("display", "none");
    $leftNavigation.css("width", "100%");
    $navigationControl.removeClass("spinIn").addClass("spinOut");
    $navigationControl.removeClass("active");
    $leftNavigation.children("ul").removeClass("active");
    $leftNavigation.removeClass("active"); $($minWrapper).css("paddingLeft", "0")
} function onPhone()
{
    $($navigation).addClass("mobile");
    $($navigation).css("display", "none");
    $($leftNavigation).animate({ width: "100%" }, 100, function () {
        $navigationControl.removeClass("spinIn").addClass("spinOut");
        $navigationControl.removeClass("active"); $leftNavigation.children("ul").removeClass("active");
        $leftNavigation.removeClass("active")
    }); $($minWrapper).animate({ paddingRight: "0" }, 100, function () { })
} var resizeId;
$(window).resize(function ()
{ clearTimeout(resizeId); resizeId = setTimeout(doneResizingLayout, 500) });
function doneResizingLayout() {
    var a = $(window).width();
    if ($windowWidth != a) {
        if (a < 500) { onPhone() } else {
            if (a < 1025) { $leftNavigation.removeAttr("style"); $minWrapper.removeAttr("style"); $leftNavigation.removeAttr("style"); $navigation.removeAttr("style"); $navigation.removeClass("mobile"); onTablet() }
            else { if (a > 1025) { desktopResize() } }
        } $windowWidth = a
    } else { $windowWidth = a }
} function desktopResize() { $leftNavigation.removeAttr("style"); $minWrapper.removeAttr("style"); $leftNavigation.removeAttr("style"); $navigation.removeAttr("style"); $navigation.removeClass("mobile"); onDesktop() } function right_box_display() {
    $rightSidebarControl.click(function () { $("#setting-tab a:first").tab("show"); $("#right-wrapper").animate({ left: "0" }, 500, function () { }) });
    var a = $(".right-sidebar-setting"); a.click(function () { $("#setting-tab a:last").tab("show"); $("#right-wrapper").animate({ left: "0" }, 500, function () { }) }); $("#right-wrapper .close-right-wrapper a").click(function () { $("#right-wrapper").animate({ left: "-280px" }, 500, function () { }) })
} function phone_nav_control() { $(".phone-nav-control").click(function () { if ($navigation.is(":hidden")) { $navigation.slideDown() } else { $navigation.slideUp() } }) } function layout_change_color_start() {
    var a = $("body"); $(".change-color-box ul li ").click(function () {
        a.removeClass("black-color"); a.removeClass("blue-color"); a.removeClass("deep-blue-color"); a.removeClass("red-color"); a.removeClass("light-green-color"); a.removeClass("default"); $(".change-color-box ul li ").removeClass("active");
        if ($(this).hasClass("active")) { }
        else { var c = $(this).attr("class"); a.addClass(c); $(this).addClass("active") }
    }); var b = $("#change-color"); $("#change-color-control a").click(function () { if ($(this).hasClass("active")) { $(this).removeClass("active"); $(b).animate({ left: "-200px" }, 500, function () { }) } else { $(this).addClass("active"); $(b).animate({ left: "0" }, 500, function () { }) } })
} function panel_change_start() {
    $(".panel-control li a.close-panel").click(function () { var a = $(this).parents(".panel"); a.animate({ opacity: 0.1 }, 1000, function () { $(this).remove() }) }); $(".panel-control li a.minus").click(function () {
        var a = $(this).parents(".panel").children(".panel-body"); if ($(this).hasClass("active")) {
            a.slideDown(200); $(this).children("i").removeClass("fa-square-o");
            $(this).children("i").addClass("fa-minus"); $(this).removeClass("active")
        } else { a.slideUp(200); $(this).children("i").removeClass("fa-minus"); $(this).children("i").addClass("fa-square-o"); $(this).addClass("active") }
    })
    $($navigationControl).click(function () {
        if ($breadcrumb3.hasClass("active")) { $leftNavigation.removeClass("active"); $breadcrumb3.removeClass("active"); $minWrapper.removeClass("active"); changeMenuSizeTriger() }
        else { $breadcrumb3.addClass("active"); $minWrapper.addClass("active"); $leftNavigation.addClass("active"); $breadcrumb3.find("ul").removeAttr("style"); changeMenuSizeTriger() }
    })
} function dropdown_top_nav_bar() { $(".dropdown").on("show.bs.dropdown", function (a) { $(this).find(".dropdown-menu").first().stop(true, true).slideDown(500, function () { }) })
    ; $(".dropdown").on("hide.bs.dropdown", function (a) { $(this).find(".dropdown-menu").first().stop(true, true).slideUp(500, function () { }) })
} function plugin_load_for_layout() {
    try {
        $(".nano").nanoScroller({ preventPageScrolling: true, alwaysVisible: true, scroll: "top" });
        $(".nano-chat").nanoScroller({ preventPageScrolling: true, alwaysVisible: true, scroll: "bottom" });
        $(".progress-bar").progressbar({ display_text: "fill" });
        $(".easyPieChart").easyPieChart({ barColor: $redActive, scaleColor: $redActive, easing: "easeOutBounce", onStep: function (i, h, e) { $(this.el).find(".easyPiePercent").text(Math.round(e)) } });
        var f = window.chart = $(".easyPieChart").data("easyPieChart"); $(".js_update").on("click", function () { f.update(Math.random() * 200 - 100) }); var d = Array.prototype.slice.call(document.querySelectorAll(".js-switch"));
        d.forEach(function (e) { var h = new Switchery(e) }); var c = Array.prototype.slice.call(document.querySelectorAll(".js-switch-red")); c.forEach(function (h) { var e = new Switchery(h, { color: $redActive }) }); var b = Array.prototype.slice.call(document.querySelectorAll(".js-switch-light-green"));
        b.forEach(function (h) { var e = new Switchery(h, { color: $lightGreen }) });
        var a = Array.prototype.slice.call(document.querySelectorAll(".js-switch-light-blue"));
        a.forEach(function (e) { var h = new Switchery(e, { color: $lightBlueActive }) })
    } catch (g) { }
}
function detectIE()
{
    var c = window.navigator.userAgent;
    var b = c.indexOf("MSIE ");
    var a = c.indexOf("Trident/");
    if (b > 0) { return parseInt(c.substring(b + 5, c.indexOf(".", b)), 10) }
    if (a > 0) {
        var d = c.indexOf("rv:");
        return parseInt(c.substring(d + 3, c.indexOf(".", d)), 10)
    } return false
};