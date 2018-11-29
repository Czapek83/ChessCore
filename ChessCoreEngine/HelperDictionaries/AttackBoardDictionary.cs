using System;
using System.Collections.Generic;
using System.Text;

namespace ChessEngine.Engine
{
    public class AttackBoardDictionary : DictionaryBase<bool>
    {
        public AttackBoardDictionary():base(64)
        {

        }
    }
}
