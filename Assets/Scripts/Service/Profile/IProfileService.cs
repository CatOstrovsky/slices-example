using System.Threading.Tasks;
using Core;

namespace Managers
{
    public interface IProfileService : IService
    {
        void SetSore(int score);
        int GetScore();
        Task<bool> Save();
        Task<bool> Load();
    }
}