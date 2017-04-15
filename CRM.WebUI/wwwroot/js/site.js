/*!
 * Start Bootstrap - SB Admin 2 v3.3.7+1 (http://startbootstrap.com/template-overviews/sb-admin-2)
 * Copyright 2013-2016 Start Bootstrap
 * Licensed under MIT (https://github.com/BlackrockDigital/startbootstrap/blob/gh-pages/LICENSE)
 */
//$(function () {
//    $('#side-menu').metisMenu();
//});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function () {
    $(window).bind("load resize", function () {
        var topOffset = 50;
        var width = this.window.innerWidth > 0 ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        var height = (this.window.innerHeight > 0 ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", height + "px");
        }
    });

    var url = window.location;
    // var element = $('ul.nav a').filter(function() {
    //     return this.href == url;
    // }).addClass('active').parent().parent().addClass('in').parent();
    var element = $('ul.nav a').filter(function () {
        return this.href === url;
    }).addClass('active').parent();

    while (element.is('li')) {
            element = element.parent().addClass('in').parent();
    }
});

var CRM = {};

CRM.convertAccountType =
    function (value, row, index) {
        switch (value) {
            case 0:
                return "Agent";
            case 1:
                return "Institute";
            default:
                return "Other";
        }
    };

CRM.convertActivityType =
    function (value, row, index) {
        switch (value) {
            case 1:
                return "OutboundCall";
            case 2:
                return "InboundCall";
            case 3:
                return "Message";
            case 4:
                return "Meeting";
            case 5:
                return "Email";
            case 6:
                return "Visit";
            default:
                return "Other";
        }
    };

CRM.formatDate =
    function (value, row, index) {
        if (value !== null && value !== "")
            return value.toString().substring(0, 10);
        else
            return value;
    };

CRM.formatDatetime =
    function (value, row, index) {
        return value;
    };

CRM.convertAccountType =
    function (value, row, index) {
        switch (value) {
            case 0:
                return "Agent";
            case 1:
                return "Institute";
            default:
                return "Other";
        }
    };

CRM.getHeight =
    function () {
        return $(window).height() - 80;
    };
