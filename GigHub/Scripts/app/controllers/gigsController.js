

var GigsController = function (attendanceService) {

    var button;

    var init = function () {
        /*clicking the Going button will trager a http post
                with content of GigId, which comes from this button's attribute
                it will be passed as an Attendance Dto to controllers/api/AttendanceController,
                database will save this Attendance with current user's id and artist id.*/
        $(".js-toggle-attendance").click(toggleAttendance);
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

        button.toggleClass("btn-info").toggleClass("btn-default");
    };

    var fail = function () {
        alert("btn-info failed to toggle. ");
    };

    return {
        init: init
    }

}(AttendanceService);


