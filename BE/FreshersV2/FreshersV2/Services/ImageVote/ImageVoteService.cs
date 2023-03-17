using FreshersV2.Data.Models.VoteImageGame;

namespace FreshersV2.Services.ImageVote
{
    public class ImageVoteService : IImageVoteService
    {
        public Task CastVote(string contestId, int roundId, string userId, string imageId)
        {
            throw new NotImplementedException();
        }

        public Task CreateContest(string name, int maxParticipants, int voteTime, int drawTime)
        {
            throw new NotImplementedException();
        }

        public Task CreateRound(string contestId, int roundNumber)
        {
            throw new NotImplementedException();
        }

        public Task<VoteImageContest> GetContest(string contestId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VoteImage>> GetRoundImages(string contestId, int roundId)
        {
            throw new NotImplementedException();
        }

        public Task SaveImage(string contestId, int roundId, string userId, string imageBase64)
        {
            throw new NotImplementedException();
        }
    }
}
