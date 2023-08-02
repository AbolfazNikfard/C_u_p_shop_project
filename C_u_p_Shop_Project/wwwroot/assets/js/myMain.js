$(".ec-gl-btn").on("click", "button", function () {
    $(this).addClass("active").siblings().removeClass("active")
});
$("body").on("click", ".ec_qtybtn", function () {
    let t = $(this),
        o = t.parent().find("input").val();
    let s = o;
    if ("+" === t.text() && o<100)
        s = parseFloat(o) + 1;
    else if ("-" === t.text() && o > 1)
        s = parseFloat(o) - 1;

    t.parent().find("input").val(s)
});
$(document).on("click", ".btn-grid", function (t) {
    let o = $(".shop-pro-inner"),
        s = $(".pro-gl-content");
    t.preventDefault(), o.removeClass("list-view"), s.removeClass("width-100")
});

$(document).on("click", ".btn-list", function (t) {
    let o = $(".shop-pro-inner"),
        s = $(".pro-gl-content");
    t.preventDefault(), o.addClass("list-view"), s.addClass("width-100")
});

$(document).ready(function () {
    $(".product_page .ec-sidebar-block .ec-sb-block-content ul li ul").addClass("ec-cat-sub-dropdown"),
        $(".product_page .ec-sidebar-block .ec-sidebar-block-item").click(function () {
            let t = $(this).closest(".ec-sb-block-content").find(".ec-cat-sub-dropdown");
            t.slideToggle("slow"),
                $(".ec-cat-sub-dropdown").not(t).slideUp("slow")
        })
});

$(".ec-search-bar").focus(function () {
    $(".ec-search-tab").addClass("active")
});

$(".ec-search-bar").focusout(function () {
    setTimeout(function () {
        $(".ec-search-tab").removeClass("active")
    }, 100)
});

$(document).ready(function () {
    $(".ec-faq-wrapper .ec-faq-block .ec-faq-content").addClass("ec-faq-dropdown"),
        $(".ec-faq-block .ec-faq-title ").click(function () {
            let t = $(this).closest(".ec-faq-wrapper .ec-faq-block").find(".ec-faq-dropdown");
            t.slideToggle("slow"),
                $(".ec-faq-dropdown").not(t).slideUp("slow")
        })
});
{
    let o = $(".ec-side-toggle");
    let s = $(".ec-side-cart");
    let i = $(".mobile-menu-toggle");
    o.on("click", function (t) {
        console.log("clicked");
        t.preventDefault();
        let o = $(this),
            s = o.attr("href");
        $(".ec-side-cart-overlay").fadeIn(),
            $(s).addClass("ec-open"),
            o.parent().hasClass("mobile-menu-toggle") && (o.addClass("close"),
                $(".ec-side-cart-overlay").fadeOut())
    });
    $(".ec-side-cart-overlay").on("click", function (t) {
        $(".ec-side-cart-overlay").fadeOut(),
            s.removeClass("ec-open"),
            i.find("a").removeClass("close")
    });
    $(".ec-close").on("click", function (t) {
        t.preventDefault(),
            $(".ec-side-cart-overlay").fadeOut(),
            s.removeClass("ec-open"),
            i.find("a").removeClass("close")
    });
}
$(document).ready(function () {
    $(".header-top-lan li").click(function () {
        $(this).addClass("active").siblings().removeClass("active")
    }), $(".header-top-curr li").click(function () {
        $(this).addClass("active").siblings().removeClass("active")
    })
});
$("body").on("click", ".ec-pro-content .remove", function () {
    console.log('CLICKEDDDD');
    let t = $(".eccart-pro-items li").length;
    $(this).closest("li").remove(),
        1 == t && $(".eccart-pro-items").html('<li><p class="emp-cart-msg">Your cart is empty!</p></li>');
    let o = $(".cart-count-lable").html();
    o--, $(".cart-count-lable").html(o), t--
}),
    (n = $(".ec-menu-content, .overlay-menu")).find(".sub-menu").parent().prepend('<span class="menu-toggle"></span>'),
    n.on("click", "li a, .menu-toggle", function (t) {
        var o = $(this);
        ("#" === o.attr("href") || o.hasClass("menu-toggle")) && (t.preventDefault(), o.siblings("ul:visible").length ? (o.parent("li").removeClass("active"), o.siblings("ul").slideUp(), o.parent("li").find("li").removeClass("active"), o.parent("li").find("ul:visible").slideUp()) : (o.parent("li").addClass("active"), o.closest("li").siblings("li").removeClass("active").find("li").removeClass("active"), o.closest("li").siblings("li").find("ul:visible").slideUp(), o.siblings("ul").slideDown()))
    });
new Swiper(".ec-slider.swiper-container", {
    loop: !0,
    speed: 2e3,
    effect: "slide",
    autoplay: {
        delay: 7e3,
        disableOnInteraction: !1
    },
    pagination: {
        el: ".swiper-pagination",
        clickable: !0
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev"
    }
});
