
//open the corresponding menu

$(document).on("click", ".g-menu-item", function (e) {
    var menu_id = $(this).data("count");
    sessionStorage.setItem("menu_id", menu_id);
})
$(function () {
    var current_menu_id = sessionStorage.getItem("menu_id");
    var url = document.location.href.toString();
    var arrUrl = url.split("/");
    var $navItem = $(".nav-sidebar").find(".nav-item");
    if (current_menu_id) {
        $navItem.each(function (index, ele) {
            var menu_id = $(ele).find("a").data("count");
            if (current_menu_id == menu_id) {
                $(ele).find(".nav-link").addClass("active");
                $(ele).parent().prev().addClass("active");
                $(ele).parent().parent().addClass("menu-open");
            }
        })
    } else {
        $navItem.each(function (index, ele) {
            var navUrl = $(ele).find("a").prop("href");
            var arrNavUrl = navUrl.split("/");
            if (arrUrl[arrUrl.length - 1] === arrNavUrl[arrNavUrl.length - 1]) {
                $(ele).find(".nav-link").addClass("active");
                $(ele).parent().prev().addClass("active");
                $(ele).parent().parent().addClass("menu-open");
            }
        })
    }   
})
//record collasp menu
$(document).on("click", ".g-menu-collasp", function (e) {
    var width = $(window).width();
    if (width >= 993) {
        var is_collasp_menu = sessionStorage.getItem("is_collasp_menu") === null ? false : JSON.parse(sessionStorage.getItem("is_collasp_menu"));
        console.log(is_collasp_menu);
        sessionStorage.setItem("is_collasp_menu", !is_collasp_menu);
    }
})
$(function () {
    //init tooltip
      //init tooltip
    //var bootstrapTooltip = $.fn.tooltip.noConflict();
    //$.fn.bstooltip = bootstrapTooltip;
    //$('[data-toggle="tooltip"]').bstooltip();

    //$('[data-toggle="tooltip"]').tooltip();


    var width = $(window).width();
    var is_collasp_menu = JSON.parse(sessionStorage.getItem("is_collasp_menu")) || false;
    if (width >= 993) {
        if (is_collasp_menu) {  
            $("body").addClass("sidebar-collapse");
        }
    }
})

