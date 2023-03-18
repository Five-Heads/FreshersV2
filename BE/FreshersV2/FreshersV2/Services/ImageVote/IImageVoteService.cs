using FreshersV2.Data.Models.VoteImageGame;
using FreshersV2.Models.VoteImage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshersV2.Services.ImageVote
{
    public interface IImageVoteService
    {
        Task CreateContest(string name, int maxParticipants, int voteTime, int drawTime, List<string> words);
        Task CreateRound(int contestId, int roundNumber, string word, List<string> drawingUsersHubIds);
        Task CastVote(int contestId, int roundId, string userId, int imageId);
        Task<int> CreateVoteRound(int contestId, int currentRoundId, VoteImage image1, VoteImage image2);
        Task SaveImage(int contestId, int roundId, string userId, string imageBase64);

        Task<List<VoteImage>> GetRoundImages(int contestId, int roundId);
        Task<VoteImageContest> GetContest(int contestId);
        Task<List<UserContest>> GetInitialDrawingUsers(int contestId, int currentRoundId);
        List<ContestResponseModel> AllContests();
        Task<RoundVote> GetRoundVote(int voteRoundId);
    }
}
