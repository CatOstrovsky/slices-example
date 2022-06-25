using Plugins.Common;

namespace Managers
{
    public class ProfileDataModel : ModelBase
    {
        public Observable<int> totalScore = new Observable<int>();
    }
}