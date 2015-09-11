using System;
using System.Runtime.Serialization;

namespace VKeCRM.Common.Lookups
{
	/// <summary>
	/// Base class for the custom Lookup classes
	/// </summary>
	[DataContract]
    [Serializable]
	public abstract class LookupBase<T> : ILookup, IComparable<T> where T : LookupBase<T>
	{
		#region Declarations
		[DataMember]
		private short _code;
		private string _description;
		private string _displayName;
		private bool _isActive;
		private int _sortRank;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates object of the LookupValueBase type.
		/// </summary>
		/// <param name="code">Cannot be null or empty string(if it is a string)</param>
		/// <param name="displayName">Cannot be null or empty string</param>
		/// <param name="description"></param>
		protected LookupBase(short code, string displayName, string description, bool isActive, int sortRank)
		{
			_code = code;

			if (!string.IsNullOrEmpty(displayName))
			{
				_displayName = displayName;
			}
			else
			{
				throw new ArgumentNullException("displayName");
			}

			_description = (description != null ? description : string.Empty);
			_isActive = isActive;
			_sortRank = sortRank;
		} 
		#endregion

		#region Properties
		/// <summary>
		/// Gets the Code for the Lookup object.
		/// </summary>
        [DataMember]
		public short Code
		{
			get { return _code; }
            set { _code = value; }
		}

		/// <summary>
		/// Gets the Description of the Lookup object.
		/// </summary>
		public string Description
		{
			get { return _description; }
		}

		/// <summary>
		/// Gets the Display Name of the Lookup object.
		/// This field can be bind to the presentation layer controls.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName; }
		}

		/// <summary>
		/// Shows whether this Lookup object is active. 
		/// </summary>
		public bool IsActive
		{
			get { return _isActive; }
		}

		/// <summary>
		/// Gets the Sort Rank of the Lookup object
		/// </summary>
		public int SortRank
		{
			get { return _sortRank; }
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// Checks if this Lookup object is equal to another Lookup object of the same type.
		/// </summary>
		/// <param name="obj">Object to compare</param>
		/// <returns>True if obj is same type as instance and its Code value is
		/// the same as this instance's; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (System.Object.ReferenceEquals(obj, this))
				return true;

			return this.Equals(obj as T);
		}

		/// <summary>
		/// Checks if this Lookup object is equal to another Lookup object of the same type.
		/// </summary>
		/// <param name="obj">Lookup object to compare.</param>
		/// <returns>True if obj's Code value is the same as this instance's, otherwise, false.</returns>
		public bool Equals(T obj)
		{
			//We need to convert obj to Object type, so that we will not use the overridden operator ==.
			if ((object)obj == null)
				return false;

			if (System.Object.ReferenceEquals(obj, this))
				return true;

			return this.Code.Equals(obj.Code);
		}

		/// <summary>
		/// Checks if two Lookup objects are equal.
		/// </summary>
		/// <param name="a">Lookup object to compare.</param>
		/// <param name="b">Lookup object to compare.</param>
		/// <returns>True if the two Lookup objects' Codes value are the same, otherwise, false.</returns>
		public static bool operator ==(LookupBase<T> a, LookupBase<T> b)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			// Return true if the fields match:
			return a.Code == b.Code;
		}

		/// <summary>
		/// Checks if two Lookup objects are equal.
		/// </summary>
		/// <param name="a">Lookup object to compare.</param>
		/// <param name="b">Lookup object to compare.</param>
		/// <returns>True if the two Lookup objects' Codes value are not the same, otherwise, false.</returns>
		public static bool operator !=(LookupBase<T> a, LookupBase<T> b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Gets hash code for this object.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _code.GetHashCode();
		}

		/// <summary>
		/// Gets the string representation of this object.
		/// </summary>
		/// <returns>Returns Display Name property.</returns>
		public override string ToString()
		{
			return _displayName;
		} 
		#endregion

		#region IComparable<T> Members
		/// <summary>
		/// Compare this Lookup object to another Lookup object of the same type
		/// by Code property.
		/// </summary>
		/// <param name="other"></param>
		/// <returns>Returns 0 if both objects has same Code,
		/// less then 0 if this object's Code is less and greater then 0 if this 
		/// object's Code is greater then provided object's Code.</returns>
		public int CompareTo(T other)
		{
			if(other == null)
				throw new ArgumentNullException("CompareTo");

			return this.Code.CompareTo(other.Code);
		}

		#endregion

		#region Abstract Methods
		protected abstract T GetLookup(short code);
		#endregion

		#region Serilization Events
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
            T copy = GetLookup(_code);
            _description = copy.Description;
            _displayName = copy.DisplayName;
            _isActive = copy.IsActive;
            _sortRank = copy.SortRank;
		}
		#endregion
	}
}
