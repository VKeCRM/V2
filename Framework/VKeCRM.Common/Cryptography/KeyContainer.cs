//-----------------------------------------------------------------------
// <copyright file="KeyContainer.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using VKeCRM.Common.Cryptography.Collections;
using VKeCRM.Common.Exceptions;

namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// Manages a key container for a given Cryptography Service Provider.
    /// </summary>
    public class KeyContainer : RsaCryptoProvider
    {
        #region Static Methods
        #endregion Static Methods

        #region Constants

        /// <summary>
        /// Key flags constant
        /// </summary>
        private const CspProviderFlags RSA_KEY_FLAGS = CspProviderFlags.NoPrompt | CspProviderFlags.UseMachineKeyStore | CspProviderFlags.UseArchivableKey; // exportable

        #endregion // Constants

        #region Private Fields

        /// <summary>
        /// The name of the key container in the CSP.
        /// </summary>
        /// <see cref="ContainerName"/>
        private string _containerName = string.Empty;

        /// <summary>
        /// RSA Key size
        /// </summary>
        private RsaKeySize _keySize = RsaKeySize.Bits512;

        /// <summary>
        /// Private file name
        /// </summary>
        private string _privateFileName = string.Empty;

        /// <summary>
        /// Public file name
        /// </summary>
        private string _publicFileName = string.Empty;

        #endregion // Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KeyContainer class
        /// </summary>
        /// <param name="containerName">Name of container</param>
        public KeyContainer(string containerName)
            : this(containerName, RsaKeySize.Bits1024)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KeyContainer class
        /// </summary>
        /// <param name="containerName">Name of Container</param>
        /// <param name="keySize">Size of Key</param>
        public KeyContainer(string containerName, RsaKeySize keySize)
            : this(containerName, keySize, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the KeyContainer class
        /// </summary>
        /// <param name="containerName">Name of container</param>
        /// <param name="keySize">Size of Key</param>
        /// <param name="publicFileName">Public file name</param>
        /// <param name="privateFileName">Private file name</param>
        public KeyContainer(string containerName, RsaKeySize keySize, string publicFileName, string privateFileName)
        {
            ContainerName = containerName;
            KeySize = keySize;
            PublicFileName = publicFileName;
            PrivateFileName = privateFileName;
        }

        #endregion // Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the Private File Name
        /// </summary>
        public string PrivateFileName
        {
            get 
            { 
                return _privateFileName; 
            }

            set 
            { 
                _privateFileName = value; 
            }
        }

        /// <summary>
        /// Gets or sets the Public File name
        /// </summary>
        public string PublicFileName
        {
            get 
            { 
                return _publicFileName; 
            }

            set 
            { 
                _publicFileName = value; 
            }
        }

        /// <summary>
        /// Gets the file name that contains the unique key
        /// </summary>
        public string UniqueKeyContainerFileName
        {
            get
            {
                RSACryptoServiceProvider csp = Load();
                string commonAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string machineKeyPath = Path.Combine(commonAppDataPath, @"Microsoft\Crypto\RSA\MachineKeys");
                return Path.Combine(machineKeyPath, csp.CspKeyContainerInfo.UniqueKeyContainerName);
            }
        }

        /// <summary>
        /// Gets or sets the name of the key container in the CSP.
        /// </summary>
        /// <value>The ContainerName property gets/sets the _containerName data member.</value>
        /// <remarks>The name is required for all operations that operate on a key container in the CSP. It is also required to create a new key container that is to be persisted in the CSP.</remarks>
        public string ContainerName
        {
            get 
            { 
                return _containerName; 
            }

            set 
            { 
                _containerName = value; 
            }
        }

        /// <summary>
        /// Gets or sets RSA key size
        /// </summary>
        public RsaKeySize KeySize
        {
            get 
            { 
                return _keySize; 
            }

            set 
            { 
                _keySize = value; 
            }
        }

        /// <summary>
        /// Returns the native key containers
        /// </summary>
        /// <returns>Returns a string of Key containers</returns>
        public static string[] GetInstalledKeyContainerNames()
        {
            return Win32Native.EnumerateKeyContainers();
        }

        /// <summary>
        /// To get a list of installed key containers
        /// </summary>
        /// <returns>Returns a list of key containers</returns>
        public static List<KeyContainer> GetInstalledKeyContainers()
        {
            string[] names = GetInstalledKeyContainerNames();
            List<KeyContainer> containers = new List<KeyContainer>(names.Length);
            foreach (string name in names)
            {
                containers.Add(new KeyContainer(name));
            }

            return containers;
        }

        /// <summary>
        /// To load the key containers
        /// </summary>
        /// <returns>Returns an instance of the RSA crypto Service provider collection</returns>
        public static RSACryptoServiceProviderCollection LoadInstalledKeyContainers()
        {
            string[] names = GetInstalledKeyContainerNames();
            RSACryptoServiceProviderCollection containers = new RSACryptoServiceProviderCollection(names.Length);
            foreach (string name in names)
            {
                containers.Add(new KeyContainer(name).Load());
            }

            return containers;
        }

        #endregion Public Properties

        #region Static Helper Methods

        /// <summary>
        /// Create a Key
        /// </summary>
        /// <param name="containerName">Name of container</param>
        /// <param name="strength">Key strength</param>
        /// <param name="persistKeyInCsp">A boolean value to indicate whether to persist the Key in the CryptoServiceProvider</param>
        /// <returns>Returns the CryptoServiceProvider</returns>
        public static RSACryptoServiceProvider CreateKey(string containerName, RsaKeySize strength, bool persistKeyInCsp)
        {
            CspParameters cspParams = new CspParameters((int)eProvType.PROV_RSA_FULL, null, containerName);
            cspParams.KeyNumber = (int)KeyNumber.Exchange;
            cspParams.Flags = RSA_KEY_FLAGS;

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider((int)strength, cspParams);
            csp.PersistKeyInCsp = persistKeyInCsp;

            return csp;
        }

        /// <summary>
        /// Generate Key pairs
        /// </summary>
        /// <param name="containerName">Name of container</param>
        /// <param name="strength">Key strength</param>
        /// <param name="persistKeyInCsp">A boolean value to indicate whether to persist the Key in the CryptoServiceProvider</param>
        /// <returns>Returns the CryptoServiceProvider</returns>
        public static RSACryptoServiceProvider GenerateKeyPairs(string containerName, RsaKeySize strength, bool persistKeyInCsp)
        {
            return CreateKey(containerName, strength, persistKeyInCsp);
        }

        /// <summary>
        /// Overloaded method to generate key pairs
        /// </summary>
        /// <param name="privateKeyFilename">Private key file name</param>
        /// <param name="publicKeyFilename">Public key file name</param>
        /// <returns>Returns the CryptoServiceProvider</returns>
        public static RSACryptoServiceProvider GenerateKeyPairs(string privateKeyFilename, string publicKeyFilename)
        {
            return GenerateKeyPairs(privateKeyFilename, publicKeyFilename, RsaKeySize.Bits2048);
        }

        /// <summary>
        /// Overloaded method to generate key pairs
        /// </summary>
        /// <param name="privateKeyFilename">Private Key Filename</param>
        /// <param name="publicKeyFilename">Public Key Filename</param>
        /// <param name="strength">Key Strength</param>
        /// <returns>Returns the CryptoServiceProvider</returns>
        public static RSACryptoServiceProvider GenerateKeyPairs(string privateKeyFilename, string publicKeyFilename, RsaKeySize strength)
        {
            return GenerateKeyPairs(privateKeyFilename, publicKeyFilename, string.Empty, strength, false, true);
        }

        /// <summary>
        /// Overloaded method to generate key pairs
        /// </summary>
        /// <param name="privateKeyFilename">Private Key Filename</param>
        /// <param name="publicKeyFilename">Public Key Filename</param>
        /// <param name="containerName">Name of container</param>
        /// <param name="strength">Key Strength</param>
        /// <param name="persistKeyInCsp">A boolean value to indicate whether to persist the Key in the CryptoServiceProvider</param>
        /// <returns>Returns the CryptoServiceProvider</returns>
        public static RSACryptoServiceProvider GenerateKeyPairs(string privateKeyFilename, string publicKeyFilename, string containerName, RsaKeySize strength, bool persistKeyInCsp)
        {
            return GenerateKeyPairs(privateKeyFilename, publicKeyFilename, containerName, strength, persistKeyInCsp, true);
        }

        /// <summary>
        /// Overloaded method to generate key pairs
        /// </summary>
        /// <param name="privateKeyFilename">Private Key Filename</param>
        /// <param name="publicKeyFilename">Public Key Filename</param>
        /// <param name="containerName">Name of container</param>
        /// <param name="strength">Key Strength</param>
        /// <param name="persistKeyInCsp">A boolean value to indicate whether to persist the Key in the CryptoServiceProvider</param>
        /// <param name="overwrite">A boolean value to indicate whether to over write or not</param>
        /// <returns>Returns the CryptoServiceProvider</returns>
        public static RSACryptoServiceProvider GenerateKeyPairs(string privateKeyFilename, string publicKeyFilename, string containerName, RsaKeySize strength, bool persistKeyInCsp, bool overwrite)
        {
            if (string.IsNullOrEmpty(privateKeyFilename))
            {
                throw new PrivateKeyFilenameRequiredException();
            }

            if (string.IsNullOrEmpty(publicKeyFilename))
            {
                throw new PublicKeyFilenameRequiredException();
            }

            if (!overwrite && File.Exists(privateKeyFilename))
            {
                throw new PrivateKeyFileExistsException(privateKeyFilename);
            }

            if (!overwrite && File.Exists(publicKeyFilename))
            {
                throw new PublicKeyFileExistsException(publicKeyFilename);
            }

            RSACryptoServiceProvider csp = CreateKey(containerName, strength, persistKeyInCsp);

            WriteKeyToXml(csp.ToXmlString(true), privateKeyFilename);
            WriteKeyToXml(csp.ToXmlString(false), publicKeyFilename);

            return csp;
        }

        #endregion // Public Properties

        #region ACL Methods

        /// <summary>
        /// To grant read access
        /// </summary>
        /// <param name="identity">identity to grant read access</param>
        public void GrantReadAccess(string identity)
        {
            if (!Exists())
            {
                throw new KeyDoesNotExistsException(ContainerName);
            }

            // Get the unique filename associated and 
            string path = UniqueKeyContainerFileName;

            FileSecurity fileSecurity = new FileSecurity(path, AccessControlSections.Access);
            fileSecurity.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.Read, AccessControlType.Allow));

            File.SetAccessControl(path, fileSecurity);
        }

        /// <summary>
        /// Tpo revoke read access
        /// </summary>
        /// <param name="identity">Identity to revoke read access</param>
        public void RevokeReadAccess(string identity)
        {
            if (!Exists())
            {
                throw new KeyDoesNotExistsException(ContainerName);
            }

            // Get the unique filename associated and 
            string path = UniqueKeyContainerFileName;

            FileSecurity fileSecurity = new FileSecurity(path, AccessControlSections.Access);
            fileSecurity.RemoveAccessRule(new FileSystemAccessRule(identity, FileSystemRights.ReadPermissions, AccessControlType.Allow));

            File.SetAccessControl(path, fileSecurity);
        }

        /// <summary>
        /// To show explorer property sheet
        /// </summary>
        /// <param name="parentHwnd">Parent window</param>
        public void ShowExplorerPropertySheet(IntPtr parentHwnd)
        {
            if (!Win32Native.SHObjectProperties(parentHwnd, eShOPType.SHOP_FILEPATH, UniqueKeyContainerFileName, "Security"))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
        }

        #endregion // ACL Methods

        #region Create Methods

        /// <summary>
        /// Create RSA Crypto Service Provider
        /// </summary>
        /// <returns>RSA Crypto Service Provider</returns>
        public RSACryptoServiceProvider Create()
        {
            return GenerateKeyPairs(PrivateFileName, PublicFileName, KeySize);
        }

        /// <summary>
        /// Overloaded method to create RSA Crypto Service Provider
        /// </summary>
        /// <param name="persistKeyInCsp">A boolean value to indicate whether to persist the Key in the CryptoServiceProvider</param>
        /// <returns>RSA Crypto Service Provider</returns>
        public RSACryptoServiceProvider Create(bool persistKeyInCsp)
        {
            return GenerateKeyPairs(ContainerName, KeySize, persistKeyInCsp);
        }

        /// <summary>
        /// Overloaded method to create RSA Crypto Service Provider
        /// </summary>
        /// <param name="persistKeyInCsp">A boolean value to indicate whether to persist the Key in the CryptoServiceProvider</param>
        /// <param name="overwrite">A boolean value to indicate whether to over write or not</param>
        /// <returns>RSA Crypto Service Provider</returns>
        public RSACryptoServiceProvider Create(bool persistKeyInCsp, bool overwrite)
        {
            return GenerateKeyPairs(PrivateFileName, PublicFileName, ContainerName, KeySize, persistKeyInCsp, overwrite);
        }

        #endregion // Create Methods

        #region Import/Export Methods

        /// <summary>
        /// Import RSA Crypto Service Provider
        /// </summary>
        /// <returns>RSA Crypto Service Provider</returns>
        public RSACryptoServiceProvider Import()
        {
            return Import(false);
        }

        /// <summary>
        /// Overloaded method to import RSA Crypto Service Provider
        /// </summary>
        /// <param name="importPrivate">A boolean value to indicate whether to import from a private file or not</param>
        /// <returns>RSA Crypto Service Provider</returns>
        public RSACryptoServiceProvider Import(bool importPrivate)
        {
            if (Exists())
            {
                throw new KeyExistsException(ContainerName);
            }

            CspParameters cspParams = new CspParameters((int)eProvType.PROV_RSA_FULL, null, ContainerName);
            cspParams.KeyNumber = (int)KeyNumber.Exchange;
            cspParams.Flags = RSA_KEY_FLAGS;

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(cspParams);
            csp.FromXmlString(ReadKeyFromXmlFile(importPrivate ? PrivateFileName : PublicFileName));
            KeySize = ConvertToRSAKeySize(csp.KeySize);
            return csp;
        }

        /// <summary>
        /// Exports the container's public key to an Xml file.
        /// </summary>
        public void Export()
        {
            Export(false);
        }

        /// <summary>
        /// Exports the container's public key to an Xml file. Optionally,
        /// the public/private key is exported to a separate file Xml file.
        /// </summary>                                                                  
        /// <param name="exportPrivate">
        /// If true, a separate Xml file containing both the public and private key is generated.
        /// </param>
        public void Export(bool exportPrivate)
        {
            RSACryptoServiceProvider csp = Load();

            WriteKeyToXml(PublicFileName, csp.ToXmlString(false));
            if (exportPrivate)
            {
                WriteKeyToXml(PrivateFileName, csp.ToXmlString(true));
            }
        }

        #endregion // Import/Export Methods

        #region Miscellaneous Public Methods

        /// <summary>
        /// Deletes named container from the store.
        /// </summary>
        /// <remarks>Throws KeyDoesNotExist exception if the named container does not exist in the store.</remarks>
        public void Delete()
        {
            Win32Native.DeleteKeyContainer(ContainerName);
        }

        /// <summary>
        /// Determines is the named container already exists in the store.
        /// </summary>
        /// <remarks>Throws KeyDoesNotExist exception if the named container does not exist in the store.</remarks>
        /// <returns>Retuns a boolean value to indicate if the Key container exists or not</returns>
        public bool Exists()
        {
            return Win32Native.KeyContainerExists(ContainerName);
        }

        #endregion // Miscellaneous Public Methods

        #region Open/Load Methods

        /// <summary>
        /// Loads named container from the store.
        /// </summary>
        /// <returns>A new RSACryptoServiceProvider instance initialized retrieved from the cryptography store.</returns>
        public override RSACryptoServiceProvider Load()
        {
            if (!Exists())
            {
                throw new KeyDoesNotExistsException(ContainerName);
            }

            CspParameters cspParams = new CspParameters((int)eProvType.PROV_RSA_FULL, null, ContainerName);
            cspParams.KeyNumber = (int)KeyNumber.Exchange;
            cspParams.Flags = RSA_KEY_FLAGS;

            RSACryptoServiceProvider result = new RSACryptoServiceProvider(cspParams);
            KeySize = ConvertToRSAKeySize(result.KeySize);
            return result;
        }

        /// <summary>
        /// Loads container from an Xml file.
        /// </summary>
        /// <returns>A new RSACryptoServiceProvider instance initialized using the public key parameters 
        /// from the specified public key file name.</returns>
        /// <remarks>Throws FileNotFound exception if the public key file cannot be found.</remarks>
        public override RSACryptoServiceProvider Open()
        {
            return Open(false);
        }

        /// <summary>
        /// Loads container from an Xml file.
        /// </summary>
        /// <param name="openPrivateKey">
        /// If true, the parameters are loaded from the specified private key file.
        /// </param>
        /// <returns>A new RSACryptoServiceProvider instance initialized using the parameters 
        /// from the either the public or the public/private key file name.</returns>
        /// <remarks>Throws FileNotFound exception if the public key file cannot be found.</remarks>
        public override RSACryptoServiceProvider Open(bool openPrivateKey)
        {
            string xml = ReadKeyFromXmlFile(openPrivateKey ? PrivateFileName : PublicFileName);
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.FromXmlString(xml);
            return csp;
        }

        #endregion // Open/Load Methods

        #region Internal Static Helper Methods
        /// <summary>
        /// Read the key from an XML file
        /// </summary>
        /// <param name="filename">Name of file to read</param>
        /// <returns>Returns an XML string</returns>
        internal static string ReadKeyFromXmlFile(string filename)
        {
            string xmlString = string.Empty;

            if (File.Exists(filename))
            {
                using (StreamReader streamReader = new StreamReader(filename, true))
                {
                    xmlString = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }

            return xmlString;
        }

        /// <summary>
        /// Write the key to an XML file
        /// </summary>
        /// <param name="filename">Name of file to write to</param>
        /// <param name="xml">XML string to write in file</param>
        internal static void WriteKeyToXml(string filename, string xml)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filename, false))
                {
                    streamWriter.Write(xml);
                    streamWriter.Close();
                }
            }
            catch (System.Exception)
            {
                // May want to log exception here

                // Rethrow the exeception
                throw;
            }
        }
        #endregion
    }
}