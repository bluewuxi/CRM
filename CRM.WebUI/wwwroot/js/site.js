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

CRM.convertActivityStatus =
    function (value, row, index) {
        switch (value) {
            case 1:
                return "Event";
            case 2:
                return "OpenTask";
            case 3:
                return "ClosedTask";
            default:
                return "Unknown";
        }
    };

CRM.convertApplicationStatus =
    function (value, row, index) {
        switch (value) {
            case 0:
                return "Draft";
            case 1:
                return "Applied";
            case 2:
                return "Reserved";
            case 3:
                return "Accepted";
            case 4:
                return "AcceptedWithCondition";
            default:
                return "Closed";
        }
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

CRM.convertEnrollmentStatus =
    function (value, row, index) {
        switch (value) {
            case 0:
                return "Activate";
            case 1:
                return "Closed";
            default:
                return "Unknown";
        }
    };


CRM.formatDate =
    function (value, row, index) {
        if (value!==undefined && value !== null && value !== "")
            return value.toString().substring(0, 10);
        else
            return value;
    };

CRM.formatDatetime =
    function (value, row, index) {
        if (value !== undefined && value !== null && value !== "")
            return value.toString().substring(0,19).replace(/T/g, " ");
        else
            return value;
    };

CRM.getHeight =
    function () {
        return $(window).height() - 80;
    };

CRM.setCookie =
    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + encodeURIComponent(cvalue) + ";" + expires + ";path=/";
    };

CRM.getCookie =
    function getCookie(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return decodeURIComponent(c.substring(name.length, c.length));
            }
        }
        return "";
    };

CRM.saveFilter =
    function (key) {
        $user = $("#crm-user");
        if ($user != null)
        {
            key = $user.val() + "-" + key;
        }
        var items = [];
        $("#side-menu .crm-query").each(function (index, elem) {
            items.push({ "Field": elem.id, "Value": elem.value });
        });
        localStorage.setItem(key, JSON.stringify(items));
    };

CRM.getFilter =
    function (key) {
        $user = $("#crm-user");
        if ($user !== null) {
            key = $user.val() + "-" + key;
        }
        v = localStorage.getItem(key);
        if (v == null) v = "";
        return v;
    };

CRM.restoreFilter =
    function (key) {
        var filter = CRM.getFilter(key);
        if (filter == null || filter == "") { return; }
        filter = JSON.parse(filter);
        for (var i = 0, len = filter.length; i < len; i++) {
            if (filter[i] != null) $("#side-menu #" + filter[i].field).val(filter[i].value);
        }
    };

CRM.savePagination =
    function (key,page) {
        $user = $("#crm-user");
        if ($user !== null) {
            key = $user.val() + "-" + key +"-page";
        }
        var item = JSON.stringify(page); 
        localStorage.setItem(key, item);
    };

CRM.getPagination =
    function (key) {
        $user = $("#crm-user");
        if ($user !== null) {
            key = $user.val() + "-" + key + "-page";
        }
        v = localStorage.getItem(key);
        if (v == null)
        {
            var defaultPage = {};
            defaultPage.pageNumber = 1;
            defaultPage.pageSize = 10;
            return defaultPage;
        }
        else {
            return JSON.parse(v);
        }
    };

CRM.savePagination =
    function (key, page) {
        $user = $("#crm-user");
        if ($user != null) {
            key = $user.val() + "-" + key + "-page";
        }
        var items = [];
        localStorage.setItem(key, page);
    };

CRM.getSort =
    function (key) {
        $user = $("#crm-user");
        if ($user !== null) {
            key = $user.val() + "-" + key + "-sort";
        }
        return localStorage.getItem(key);
    };

CRM.saveSort =
    function (key, sort) {
        $user = $("#crm-user");
        if ($user != null) {
            key = $user.val() + "-" + key + "-sort";
        }
        var items = [];
        localStorage.setItem(key, page);
    };
