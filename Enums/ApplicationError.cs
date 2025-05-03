using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Enums
{
    public enum ApplicationError
    {
        NotIdentified = 1000,
        FileNotSelectedToEncrypt = 1001,
        DataToEncryptIsNull = 1002,
        EncTempPathIsNull = 1003,
        EncLauncherError = 1004,
        EncTempFileResultIsFail = 1005,
        EnFailToPickFile = 1006,
        DecNotTxtUploaded = 1007,
        DecFailToPickFile = 1008,
        FileNotSelectedToDecrypt = 1009,
        DecIntCollWrongRetrieved = 1010,
        DecIntCollWrongRetrievedExc = 1011,
        DecDecryptionFail = 1012,
        DataToDecryptIsInvalid = 1013,
        DecTempFileResultIsFail = 1014,
        DecTempPathIsNull = 1015,
        DecLauncherError = 1016,
        DecReadFileExtensionExc = 1017,
        DecInputExtensionIsTooLong = 1018,
        DecInputExtensionIsEmpty = 1019,
        DecInputExtensionIsNotNumOrLet = 1020
    }
}
