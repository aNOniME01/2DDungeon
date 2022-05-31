using System;

namespace WPFDungeon
{
    class RegistAndLogIn
    {
        static Random rnd = new Random();

        public static int[] Register(string AccName, string AccPssw)
        {
            char[] LegalCharacters = LoggedData.charac;

            int[] AccError = new int[2] { 0, 0 }; //0 = evrything is fine || 0< = error 
                                                  //----------------------------------------------------
                                                  //----------------------------------------------------
            /*
                {0,x} = The AccName is ok
                {1,x} = The AccName is alredy used
                {2,x} = The AccName is too short or long
                {3,x} = The AccName contains unuseable characters

                {x,0} = The AccPassword is ok
                {x,1} = The AccPassword is too short or long
                {x,2} = The AccPassword contains unuseable characters
            */
            //----------------------------------------------------
            //----------------------------------------------------

            #region AccName creation
            //AccName is alredy used
            if (SQLOperations.IsInDatabase("UserName", "Player", AccName))
            {
                AccError[0] = 1;
            }
            //AccName is too short or long
            if (AccName.Length < 4 || AccName.Length > 20)
            {
                AccError[0] = 2;
            }
            bool IsLegal = false;
            //AccName contains unusable characters
            for (int i = 0; i < AccName.Length; i++)
            {
                for (int j = 0; j < LegalCharacters.Length; j++)
                {
                    if (char.ToLower(AccName[i]) == LegalCharacters[j])
                    {
                        IsLegal = true;
                    }
                }
                if (!IsLegal)
                {
                    i = AccName.Length;
                    AccError[0] = 3;
                }
            }
            #endregion

            #region AccPassword creation
            IsLegal = false;
            //The AccPassword is too short or long
            if (AccPssw.Length < 8 || AccPssw.Length > 16)
            {
                AccError[1] = 1;
            }
            //The AccPassword contains unuseable characters
            for (int i = 0; i < AccPssw.Length; i++)
            {
                for (int j = 0; j < LegalCharacters.Length; j++)
                {
                    if (char.ToLower(AccPssw[i]) == LegalCharacters[j])
                    {
                        IsLegal = true;
                    }
                }
                if (!IsLegal)
                {
                    i = AccName.Length;
                    AccError[1] = 2;
                }
            }
            #endregion

            //Logging Information
            if (AccError[0] == 0 && AccError[1] == 0)
            {
                SQLOperations.CreatePlayer(AccName,AccPssw);
                LoggedData.Log(AccName);
            }

            return AccError;
        }
        public static int[] LoggingIn(string AccName, string AccPssw)
        {

            int[] AccError = new int[2] { 0, 0 };


            #region AccName validation checking            
            if (!SQLOperations.IsInDatabase("UserName","Player",AccName))
            {
                AccError[0] = 1;
            }
            #endregion


            #region AccPassword validation checking
            if (AccError[0] != 1 && SQLOperations.CheckPassword(AccName) != AccPssw)
            {
                AccError[1] = 1;
            }
            #endregion

            //Logging Information
            if (AccError[0] == 0 && AccError[1] == 0)
            {
                LoggedData.Log(AccName);
            }

            return AccError;
        }
    }
}
