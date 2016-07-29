var GigsController = function () {

    var init = function () {
        /*clicking the Going button will trager a http post
                with content of GigId, which comes from this button's attribute
                it will be passed as an Attendance Dto to controllers/api/AttendanceController,
                database will save this Attendance with current user's id and artist id.*/
        $(".js-toggle-attendance").click(function (e) {
            var button = $(e.target);

            if (button.hasClass("btn-default")) {
                $.post("/api/attendances", { gigId: button.attr("data-gig-id") })
                    .done(function () {
                        button
                            .removeClass("btn-default")
                            .addClass("btn-info")
                            .text("Going");
                    })
                    .fail(function () {
                        alert("Something failed!");
                    });
            } else {
                $.ajax({
                    url: "/api/attendances/" + button.attr("data-gig-id"),
                    method: "DELETE"

                }).done(function () {
                    button
                        .removeClass("btn-info")
                    .addClass("btn-default")
                    .text("Going?");
                })
                .fail(function () {

                    alert("btn-info failed to remove. ");
                });
            }

        });

    };

    return {
        init:init
    }

}();


