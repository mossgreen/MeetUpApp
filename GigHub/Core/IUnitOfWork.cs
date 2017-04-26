﻿using GigHub.Core.Dtos;
using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        ICategoryRepository Categories { get; }
        IFollowingRepository Followings { get; }
        IApplicationUserRepository Users { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }
        void Complete();
    }
}