using System.Threading.Tasks;
using Core;

namespace Service.Profile
{
    public interface IProfileService : IService
    {
        void SetSore(int score);
        int GetScore();
        Task<bool> Save();
        Task<bool> Load();
    }
}