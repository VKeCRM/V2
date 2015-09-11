using System;
using System.Security.Cryptography;
using System.Text;
using VKeCRM.Common.IO;

namespace VKeCRM.Common.Security.Encryption
{
    public class SymmetricEncryptionKey
    {
        private String _SaltValue = "Medi$y$";
        private String _HashAlgorithm = "SHA1";
        private String _InitVectorValue = "Medi$y$00Medi$y$";
        private Int32 _KeyIteration = 2;
        private Int32 _KeySize = 256;

        private Byte[] _InitVector;
        private Byte[] _Salt;
        private Byte[] _Key;
        private PasswordDeriveBytes _Password;

        #region Properties
            public Byte[] InitVector
            {
                get { return this._InitVector; }
            }

            public Byte[] Key
            {
                get { return this._Key; }
            }
        #endregion

        #region Constructor
            public SymmetricEncryptionKey(String key)
            {
                this._InitVector = this._InitVectorValue.ToByteArray(Encoding.UTF8);
                this._Salt = this._SaltValue.ToByteArray(Encoding.UTF8);
                this._Password = new PasswordDeriveBytes(key, this._Salt, this._HashAlgorithm, this._KeyIteration);
                this._Key = this._Password.GetBytes(this._KeySize / 8);
            }
        #endregion
    }
}
