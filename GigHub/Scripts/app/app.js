var GigsController = function () {

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

        if (button.hasClass("btn-default")) {
            $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                .done(done)
                .fail(fail);
        } else {
            $.ajax({
                url: "/api/attendances/" + button.attr("data-gig-id"),
                method: "DELETE"

            }).done(done)
            .fail(fail);
        }
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
        init:init
    }

}();


