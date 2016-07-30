var FollowingService = function () {
    var createFollowing = function (followeeId, done, faile) {
        $.post("/api/followings", { followeeId: followeeId })
            .done(done)
            .fail(faile);
    };

    var deletefollowing = function (followeeId, done, fail) {
        $.ajax()
            .done(done)
            .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deletefollowing: deletefollowing
    }
}();