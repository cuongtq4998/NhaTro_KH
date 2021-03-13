using System;
using FireSharp.Interfaces;

namespace NhaTroKH.Service
{
    public class Iconfig
    {
        public Iconfig()
        {
        } 
        //config firebase
        public IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "mJh3ttHegM5JEw4I8KKbreCWxcmVjIrcM5I9fhjx",
            BasePath = "https://nhatro-271ce.firebaseio.com/"

        };
        
    }
}
