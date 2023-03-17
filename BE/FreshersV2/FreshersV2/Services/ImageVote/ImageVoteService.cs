using FreshersV2.Data.Models.VoteImageGame;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshersV2.Services.ImageVote
{
    public class ImageVoteService : IImageVoteService
    {
        public Task CastVote(string contestId, int roundId, string userId, string imageId)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateContest(string name, int maxParticipants, int voteTime, int drawTime)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateRound(string contestId, int roundNumber)
        {
            throw new System.NotImplementedException();
        }

        public Task<VoteImageContest> GetContest(string contestId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserContest>> GetDrawingUsers(string contestId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetNextRoundId(string contestId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<VoteImage>> GetRoundImages(string contestId, int roundId)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveImage(string contestId, int roundId, string userId, string imageBase64)
        {
            throw new System.NotImplementedException();
        }
    }
}
