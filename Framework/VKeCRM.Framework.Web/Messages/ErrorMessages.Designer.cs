﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VKeCRM.Framework.Web.Messages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VKeCRM.Framework.Web.Security.Providers.Messages.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The config attribute &apos;endpointName&apos; is missing or empty for the VKeCRMMembershipProvider..
        /// </summary>
        internal static string MissingVKeCRMMembershipProviderEndpointName {
            get {
                return ResourceManager.GetString("MissingVKeCRMMembershipProviderEndpointName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The config attribute &apos;endpointName&apos; is missing or empty for the VKeCRMRoleProvider..
        /// </summary>
        internal static string MissingVKeCRMRoleProviderEndpointName {
            get {
                return ResourceManager.GetString("MissingVKeCRMRoleProviderEndpointName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A error occurred while attempting to communicate with the MembershipProviderService..
        /// </summary>
        internal static string UnableToCommunicateWithMembershipProviderService {
            get {
                return ResourceManager.GetString("UnableToCommunicateWithMembershipProviderService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while attempting to communicate with the RoleProviderService..
        /// </summary>
        internal static string UnableToCommunicateWithRoleProviderService {
            get {
                return ResourceManager.GetString("UnableToCommunicateWithRoleProviderService", resourceCulture);
            }
        }
    }
}