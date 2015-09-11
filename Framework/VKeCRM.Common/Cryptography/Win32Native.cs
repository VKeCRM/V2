using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using VKeCRM.Common.Exceptions;

namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public class Win32Native
    {
        private Win32Native()
        {
        }

        #region P/Invoke Stubs

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hProvider"></param>
        /// <param name="pszContainer"></param>
        /// <param name="pszProvider"></param>
        /// <param name="dwProvType"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptAcquireContext(out IntPtr hProvider, string pszContainer, string pszProvider, eProvType dwProvType, eCryptFlags dwFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hProvider"></param>
        /// <param name="dwFlagsMustBeZero"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptReleaseContext(IntPtr hProvider, UInt32 dwFlagsMustBeZero);

        #region CryptGetProvParamAnsiString

        /// <summary>
        /// This function retrieves parameters that govern the operations of a cryptographic service provider (CSP).
        /// </summary>
        /// <param name="hProvider">
        /// [in] HCRYPTPROV handle to a CSP created by a call to <c>CryptAcquireContext</c>.<br/>
        /// </param>
        /// <param name="dwParam">
        /// [in] Specifies the nature of the query.<br/>
        /// </param>
        /// <param name="sb">
        /// [out] Pointer to a buffer that receives the specified parameter data. The form of this data varies 
        /// depending on the dwParam value.<br/>
        /// This parameter can be <see langword="null"/> to set the buffer size for memory allocation purposes.<br/>
        /// </param>
        /// <param name="cch">
        /// [in, out] On input, pointer to a <c>DWORD</c> value that specifies the size, in bytes, 
        /// of the buffer pointed to by the <c>pbData</c> parameter. On output, the function returns the <c>DWORD</c> value 
        /// pointed to by the <c>pdwDataLen</c> parameter containing the number of bytes stored in the buffer.<br/>
        /// When processing the data returned in the buffer, applications must use the actual size of the data 
        /// returned. The actual size may be slightly smaller than the size of the buffer specified on input. 
        /// On input, buffer sizes are usually specified large enough to ensure that the largest possible output data 
        /// will fit in the buffer. On output, the variable pointed to by this parameter is updated to reflect the 
        /// actual size of the data copied to the buffer.<br/> 
        /// <br />
        /// If <c>PP_ENUMALGS</c> or <c>PP_ENUMCONTAINERS</c> is set, the <c>pdwDataLen</c> parameter works somewhat differently. 
        /// If <c>pbData</c> is <see langword="null" /> or the value pointed to by <c>pdwDataLen</c> is too small, the value returned in this 
        /// parameter is the size of the largest item in the enumeration list instead of the size of the item 
        /// currently being read.<br/> 
        /// <br />
        /// If <c>PP_ENUMCONTAINERS</c> is set, the first call to the function returns the size of the maximum key container 
        /// allowed by the current provider. This is in contrast to other possible behaviors, like returning the 
        /// length of the longest existing container or the length of the current container. Subsequent enumerating 
        /// calls will not change the <c>dwDataLen</c> parameter. For each enumerated container, the caller can determine 
        /// the length of the null-terminated string programmatically, if desired. If one of the enumeration values 
        /// is read and the <c>pbData</c> parameter is <see langword="null" />, the <c>CRYPT_FIRST</c> flag must be specified for the size 
        /// information to be correctly retrieved.<br/>
        /// </param>
        /// <param name="dwFlags">
        /// [in] Bitmask of flags.<br/>
        /// If <c>PP_ENUMALGS</c> or <c>PP_ENUMCONTAINERS</c> is set and the <c>CRYPT_FIRST</c> flag is specified in <c>dwFlags</c>, 
        /// the functions returns the first item in the enumeration list; otherwise, the functions returns the next 
        /// item in the list.<br/>
        /// <br />
        /// If <c>PP_ENUMCONTAINERS</c> is set, the <c>CRYPT_MACHINE_KEYSET</c> flag in <c>dwFlags</c> can be specified to enumerate the 
        /// key containers associated with a computer rather than the key containers associated with the current user.<br/>
        /// </param>
        /// <returns>
        /// <b><see langword="true"/></b> indicates success. <b><see langword="false"/></b> indicates failure. 
        /// To get extended error information, call the GetLastError function. The error values prefaced by NTE are 
        /// generated by the particular CSP you are using.<br/>
        /// <br />
        /// See <see cref="eErrors">eErrors</see> enumeration.
        /// </returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "CryptGetProvParam", ExactSpelling = true)]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptGetProvParamAnsiString(IntPtr hProvider, eProvParam dwParam, StringBuilder sb, ref UInt32 cch, eCryptPosition dwFlags);

        #endregion // CryptGetProvParamAnsiString

        #region SHObjectProperties

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndParent">[in] The <c>HWND</c> of the window that will be the parent of the dialog box.</param>
        /// <param name="dwType">[in] A flag value that specifies the type of object.
        /// <list type="table">
        /// <item>
        /// <term>SHOP_PRINTERNAME</term>
        /// <description><paramref name="szObject"/> contains the friendly name of a printer.</description>
        /// </item>
        /// <item>
        /// <term>SHOP_FILEPATH</term>
        /// <description><paramref name="szObject"/> contains a fully qualified file name.</description>
        /// </item>
        /// <item>
        /// <term>SHOP_VOLUMEGUID</term>
        /// <description><paramref name="szObject"/> contains a volume GUID.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="szObject">[in] An Unicode string that contains the object name. The contents of the 
        /// string are determined by which of the first three flags are set in <paramref name="dwType"/>.</param>
        /// <param name="szPage">[in] An Unicode string that contains the name of the property sheet page 
        /// to be initially opened. Set this parameter to <see langword="null"/> to specify the default page.
        /// </param>
        /// <returns>
        /// Returns <see langword="true"/> if the Properties command is successfully invoked, or <see langword="false"/> otherwise.
        /// </returns>
        /// <remarks>
        /// Note  This function is available through Microsoft Windows XP Service Pack 2 (SP2) 
        /// and Windows Server 2003. It might be altered or unavailable in subsequent versions of Windows.
        /// </remarks>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [return : MarshalAs(UnmanagedType.Bool)]
        public static extern bool SHObjectProperties(IntPtr hwndParent, eShOPType dwType, string szObject, string szPage);

        #endregion // SHObjectProperties

        #endregion // P/Invoke Stubs

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string[] EnumerateKeyContainers()
        {
            IntPtr providerHandle;
            if (!CryptAcquireContext(out providerHandle, null, null, eProvType.PROV_RSA_FULL, eCryptFlags.CRYPT_VERIFYCONTEXT | eCryptFlags.CRYPT_MACHINE_KEYSET)) throw new CryptoAquireContextException();

            try
            {
                List<string> list = new List<string>();
                eCryptPosition flags = eCryptPosition.CRYPT_FIRST;
                UInt32 cch = 1024; // arbitrary, but reasonable max length
                StringBuilder containerName = new StringBuilder((int) cch, (int) cch);
                while (CryptGetProvParamAnsiString(providerHandle, eProvParam.PP_ENUMCONTAINERS, containerName, ref cch, flags))
                {
                    list.Add(containerName.ToString());
                    flags = eCryptPosition.CRYPT_NEXT;
                }

                int lastWin32Error = Marshal.GetLastWin32Error();
                if (((int) eErrors.ERROR_NO_MORE_ITEMS) != lastWin32Error) throw new CryptoGetProviderParamException(lastWin32Error);

                return list.ToArray();
            }
            finally
            {
                if (!CryptReleaseContext(providerHandle, 0)) throw new CryptoReleaseContextException();
            }
        }

        /// <summary>
        /// Removes a key from the CSP.
        /// </summary>
        /// <param name="keyContainerName">The name of the key to remove.</param>
        /// <exception cref="CryptoAquireContextException" />
        public static void DeleteKeyContainer(string keyContainerName)
        {
            IntPtr providerHandle;
            if (!CryptAcquireContext(out providerHandle, keyContainerName, null, eProvType.PROV_RSA_FULL, eCryptFlags.CRYPT_MACHINE_KEYSET | eCryptFlags.CRYPT_DELETEKEYSET)) throw new CryptoAquireContextException();
        }

        /// <summary>
        /// Determines if a key exists in the CSP.
        /// </summary>
        /// <param name="keyContainerName">The name of the key to verify.</param>
        /// <returns>
        /// <b><see langword="true" /></b> if the key exists. Otherwise <b><see langword="false" /></b>.
        /// </returns>
        /// <exception cref="CryptoAquireContextException">
        /// A problem occurred aquiring a context for a key container.
        /// </exception>
        /// <exception cref="CryptoReleaseContextException">
        /// A problem occurred releasing the context for the open key container.
        /// </exception>
        public static bool KeyContainerExists(string keyContainerName)
        {
            IntPtr providerHandle;
            if (CryptAcquireContext(out providerHandle, keyContainerName, null, eProvType.PROV_RSA_FULL, eCryptFlags.CRYPT_MACHINE_KEYSET))
            {
                if (!CryptReleaseContext(providerHandle, 0)) throw new CryptoReleaseContextException();

                return true;
            }
            else
            {
                int win32ErrorCode = Marshal.GetLastWin32Error();
                if (((int) eErrors.NTE_BAD_KEYSET) == win32ErrorCode) return false;

                throw new CryptoAquireContextException(win32ErrorCode);
            }
        }
    }
}