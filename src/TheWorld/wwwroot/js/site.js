//site.js

(function () {

    //var elem = $("#username");
    //elem.text("Lich King");

    //var main = $("#main");

    //main.on("mouseenter", function () {
    //    main.css("background-color", "#888");
    //});

    //main.on("mouseleave", function () {
    //    main.css("background-color", "");
    //});

    var sidebarAndWrapper = $("#sidebar,#wrapper");

    $("#sidebarToggle").on("click", function () {
        sidebarAndWrapper.toggleClass("hide-sidebar");

        var icon = $("#sidebarToggle i.fa");
        if (sidebarAndWrapper.hasClass("hide-sidebar")) {
            $("#sidebar").css("left", "-250px");
            $("#sidebar").css("transition", "left ease 0.35s");

            $("#wrapper").css("margin-left", "0");
            $("#wrapper").css("transition", "margin-left ease 0.35s");

            icon.removeClass("fa-angle-left");
            icon.addClass("fa-angle-right");
        }
        else {
            $("#sidebar").css("left", "0px");
            $("#wrapper").css("margin-left", "250px");            
            
            icon.removeClass("fa-angle-right");
            icon.addClass("fa-angle-left");
        }
    });
    
})()