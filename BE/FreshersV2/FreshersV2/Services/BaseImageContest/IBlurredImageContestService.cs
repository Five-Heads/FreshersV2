﻿using FreshersV2.Data.Models.BlurredImageGame;
using FreshersV2.Models.BlurredImage;

namespace FreshersV2.Services.BaseImageContest
{
    public interface IBlurredImageContestService
    {
        Task CreateContest(CreateBlurredImageContestRequestModel model);

        Task<BlurredImageContest?> GetUpcomingContest();

        Task AddUserToUpcomingContest(string userId);

        Task ChangeStatus(int status);

        Task<List<string>> GetUpcomingContestUsers();

        Task AddUsersPointsToLeaderboard(int round, string userId);
    }
}
