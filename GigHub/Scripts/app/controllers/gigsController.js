

var GigsController = function (attendanceService) {

    var button;

    var init = function (container) {

        
        /*clicking the Going button will trager a http post
         with content of GigId, which comes from this button's attribute
         it will be passed as an Attendance Dto to controllers/api/AttendanceController,
         database will save this Attendance with current user's id and artist id.*/

        
        //$(".js-toggle-attendance").click(toggleAttendance);

        /*this method is unefficient. if there are 10 buttons, 
           there could be 10 "toggleAttendance" instance in memory,
           so the following method is much better.*/
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };




    //using toggleClass means if you have this class, it will be removed
    //otherwise it will be added.
    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed");
    };

    return {
        init: init
    }

}(AttendanceService);



