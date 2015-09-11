$(function () {
    var urlList = ["Workspace", "Default", "MyStudy", "TaskList", "InstitutionStudy", "CIRBStudy"];

    urlList.forEach(function (url) {
        if (parseInt(window.location.href.search(url)) >= 0) {
            switch (url) {
            case "Workspace":
                $("#Menu_MyStudy").addClass("menu-active");
                break;
            case "TaskList":
                $("#Menu_MyTask").addClass("menu-active");
                break;
            default:
                $("#Menu_" + url).addClass("menu-active");
            }
        }
    });
});
