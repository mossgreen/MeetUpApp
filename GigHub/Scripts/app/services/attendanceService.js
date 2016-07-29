var AttendanceService = function () {

    //now we have a clean service which knows nothing about UI
    //so it can be reused by multiple controllers
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
                    .done(done)
                    .fail(fail);
    };

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"

        })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();