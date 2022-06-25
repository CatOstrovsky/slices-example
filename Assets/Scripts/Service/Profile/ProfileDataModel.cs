using Plugins.Common;

namespace Service.Profile
{
    public class ProfileDataModel : ModelBase
    {
        public Observable<int> totalScore = new Observable<int>();
    }
}