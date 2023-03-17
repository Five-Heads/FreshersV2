using FreshersV2.Data.Models.VoteImageGame;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshersV2.Services.ImageVote
{
    public interface IImageVoteService
    {
        Task CreateContest(string name, int maxParticipants, int voteTime, int drawTime);
        Task CreateRound(string contestId, int roundNumber);
        Task CastVote(string contestId, int roundId, string userId, string imageId);
        Task SaveImage(string contestId, int roundId, string userId, string imageBase64);

        Task<List<VoteImage>> GetRoundImages(string contestId, int roundId);
        Task<VoteImageContest> GetContest(string contestId);
        Task<int> GetNextRoundId(string contestId);
        Task<List<UserContest>> GetDrawingUsers(string contestId);
    }
}
