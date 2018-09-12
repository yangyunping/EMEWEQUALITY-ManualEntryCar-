using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMEWEQUALITY.WatchDog
{
    public interface IWatchDog
    {
        string GetWatchDogKey();
    }


    public class WatchDogProxy
    {
        private Dictionary<string, string> _dicWatchDog = new Dictionary<string, string>();
        public WatchDogProxy()
        {
            Init();
        }

        private void Init()
        {
            _dicWatchDog.Add("JiangXiPaper", "EMEWEQUALITY.WatchDog.JiangxiWatchDogOpr");
            _dicWatchDog.Add("ChongQingPaper", "EMEWEQUALITY.WatchDog.ChongqingWatchDogOpr");
        }

        public IWatchDog GetWatchDog(string company)
        {
            if (_dicWatchDog.Keys.Contains(company))
            {
                string oprName = _dicWatchDog[company];
                return (IWatchDog)Activator.CreateInstance(Type.GetType(oprName));
            }
            return null;
        }     
    }


    public class JiangxiWatchDogOpr : IWatchDog
    {
        public string GetWatchDogKey()
        {
            return "LWJXEmewe";
        }
    }

    public class ChongqingWatchDogOpr :IWatchDog
    {
        public string GetWatchDogKey()
        {
            return "LWCQEmewe";
        }
    }
}
